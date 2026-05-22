#include "client_logic.h"
#include "protocol.h"
#include <stdio.h>
#include <string.h>
#include <sys/dispatch.h>
#include <sys/neutrino.h>

// Called by PhAB when data arrives client's channel
int background_msg_handler(int fd, void *data, unsigned mode)
{
    union {
        struct _pulse pulse;
        chat_msg_t chat;
    } msg;

    int rcvid = MsgReceive(attach->chid, &msg, sizeof(msg), NULL);
    if (rcvid == -1)
        return Pt_CONTINUE;

    if (rcvid == 0) {
        // If pulse from server received
        switch (msg.pulse.code) {
        case BSS_REG_OK:
            console_print("BSS: Registration successful!");
            update_fsm(STATE_REGISTERED);
            break;
        case BSS_REG_REJECT:
            console_print("BSS: Access denied! No free slots.");
            // Detach
            name_detach(attach, 0);
            attach = NULL;
            update_fsm(STATE_UNREGISTERED);
            break;
        case BSS_CALL_OK:
            console_print("BSS: Subscriber answered. Connection established.");
            update_fsm(STATE_IN_CALL);
            break;
        case BSS_CALL_FAIL:
            console_print("BSS: Subscriber offline or busy.");
            update_fsm(STATE_REGISTERED);
            break;
        case BSS_INCOMING: {
            char buf;
            target_phone = msg.pulse.value.sival_int;
            sprintf(buf, "INCOMING CALL from: %d", target_phone);
            console_print(buf);

            // Show caller's number in text field
            char num_str;
            sprintf(num_str, "%d", target_phone);
            PtSetResource(ABW_txt_pers_num, Pt_ARG_TEXT_STRING, num_str, 0);

            update_fsm(STATE_RINGING);
            break;
        }
        case BSS_HANGUP:
            console_print("BSS: Peer hung up.");
            update_fsm(STATE_REGISTERED);
            break;
        }
    } else {
        // If text message from another client received (MsgSend)
        if (msg.chat.type == CHAT_MSG_TYPE) {
            char buf;
            sprintf(buf, "[Person]: %s", msg.chat.text);

            // Add to chat window
            PtTextModify(ABW_mtxt_incom_msg, 0, 0, -1, buf, strlen(buf));
            PtTextModify(ABW_mtxt_incom_msg, 0, 0, -1, "\n", 1);

            // Must reply to unblock sender
            MsgReply(rcvid, EOK, NULL, 0);
        } else {
            MsgError(rcvid, ENOSYS);
        }
    }
    return Pt_CONTINUE;
}
