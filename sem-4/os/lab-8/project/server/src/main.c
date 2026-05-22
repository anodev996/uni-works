#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <sys/dispatch.h>
#include <sys/neutrino.h>
#include <time.h>
#include <unistd.h>

#include "protocol.h"

// Structure to store client data
typedef struct {
    int phone;              // Phone number
    pid_t pid;              // Client PID
    int coid;               // Connection ID to reply to the client
    int scoid;              // Server-side connection ID (to identify sender)
    int active_call_with;   // Target phone number (0 if free)
    time_t call_start_time; // Call start timestamp
} mt_client_t;

mt_client_t clients[MAX_MT];
int client_count = 0;

// Temporary storage for PIDs during 2-step registration
int temp_pid = {0};

int main(void)
{
    // 1. Register BSS in the OS namespace
    name_attach_t *attach = name_attach(NULL, BSS_NAME, 0);
    if (attach == NULL) {
        perror("name_attach error");
        return EXIT_FAILURE;
    }

    printf("BSS Server started (PID: %d). Waiting for MT registration...\n", getpid());

    while (1) {
        struct _pulse pulse;

        // 2. Wait for incoming messages/pulses
        int rcvid = MsgReceive(attach->chid, &pulse, sizeof(pulse), NULL);

        if (rcvid == -1)
            continue;

        // If rcvid == 0, it means we received a pulse
        if (rcvid == 0) {
            int scoid = pulse.scoid; // Unique client-server connection ID

            switch (pulse.code) {
            // Client sent its PID
            case MT_REG_PID:
                if (scoid < 1024) {
                    temp_pid[scoid] = pulse.value.sival_int;
                }
                break;

            // Client sent its phone number
            case MT_REG_PHONE: {
                int phone = pulse.value.sival_int;
                int pid = temp_pid[scoid];

                // Look for the client in the namespace to reply
                char mt_name;
                sprintf(mt_name, "mt_%d", phone);
                int reply_coid = name_open(mt_name, 0);

                if (reply_coid == -1) {
                    printf("BSS: Error! Cannot find MT %d in namespace.\n", phone);
                    break;
                }

                if (client_count >= MAX_MT) {
                    // Reject the 65th subscriber
                    printf("BSS: Access denied for subscriber %d (limit 64 reached).\n", phone);
                    MsgSendPulse(reply_coid, getprio(0), BSS_REG_REJECT, 0);
                    name_close(reply_coid);
                } else {
                    // Successful registration
                    clients[client_count].phone = phone;
                    clients[client_count].pid = pid;
                    clients[client_count].scoid = scoid;
                    clients[client_count].coid = reply_coid;
                    clients[client_count].active_call_with = 0;
                    client_count++;

                    printf("BSS: New MT registered - Phone: %d, PID: %d\n", phone, pid);
                    MsgSendPulse(reply_coid, getprio(0), BSS_REG_OK, 0);
                }
                break;
            }

            // Handle outgoing call
            case MT_CALL: {
                int target_phone = pulse.value.sival_int;

                // Find caller (by scoid)
                int sender_idx = -1;
                for (int i = 0; i < client_count; i++) {
                    if (clients[i].scoid == scoid)
                        sender_idx = i;
                }

                // Find target
                int target_idx = -1;
                for (int i = 0; i < client_count; i++) {
                    if (clients[i].phone == target_phone)
                        target_idx = i;
                }

                if (sender_idx != -1 && target_idx != -1 &&
                    clients[target_idx].active_call_with == 0) {
                    // Target found and free - connect them
                    clients[sender_idx].active_call_with = target_phone;
                    clients[sender_idx].call_start_time = time(NULL);

                    clients[target_idx].active_call_with = clients[sender_idx].phone;
                    clients[target_idx].call_start_time = time(NULL);

                    printf("BSS: Connection established %d <> %d\n", clients[sender_idx].phone,
                           target_phone);

                    // Send "OK" to caller and "INCOMING" to target
                    MsgSendPulse(clients[sender_idx].coid, getprio(0), BSS_CALL_OK, target_phone);
                    MsgSendPulse(clients[target_idx].coid, getprio(0), BSS_INCOMING,
                                 clients[sender_idx].phone);
                } else if (sender_idx != -1) {
                    // Target not found or busy
                    printf("BSS: Call from %d to %d failed (busy or offline).\n",
                           clients[sender_idx].phone, target_phone);
                    MsgSendPulse(clients[sender_idx].coid, getprio(0), BSS_CALL_FAIL, 0);
                }
                break;
            }

            // Handle call termination
            case MT_HANGUP: {
                int sender_idx = -1;
                for (int i = 0; i < client_count; i++) {
                    if (clients[i].scoid == scoid)
                        sender_idx = i;
                }

                if (sender_idx != -1 && clients[sender_idx].active_call_with != 0) {
                    int target_phone = clients[sender_idx].active_call_with;

                    // Calculate call duration
                    time_t duration = time(NULL) - clients[sender_idx].call_start_time;
                    printf("BSS: Call %d <> %d ended. Session duration: %ld sec.\n",
                           clients[sender_idx].phone, target_phone, duration);

                    // Free the caller
                    clients[sender_idx].active_call_with = 0;

                    // Find and free the peer
                    for (int i = 0; i < client_count; i++) {
                        if (clients[i].phone == target_phone) {
                            clients[i].active_call_with = 0;
                            // Send pulse to peer that caller hung up
                            MsgSendPulse(clients[i].coid, getprio(0), BSS_HANGUP, 0);
                            break;
                        }
                    }
                }
                break;
            }
            }
        }
    }

    return EXIT_SUCCESS;
}
