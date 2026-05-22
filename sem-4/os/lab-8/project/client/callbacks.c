#include "client_globals.h"
#include "protocol.h"
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

// Signature of background handler
extern int background_msg_handler(int, void *, unsigned);

// Register Button
int btn_register_cb(PtWidget_t *widget, ApInfo_t *apinfo, PtCallbackInfo_t *cbinfo)
{
    if (current_state != STATE_UNREGISTERED)
        return Pt_CONTINUE;

    char *num_str;
    PtGetResource(ABW_txt_my_num, Pt_ARG_TEXT_STRING, &num_str, 0);
    my_phone = atoi(num_str);

    if (my_phone <= 0) {
        console_print("Error: enter a valid number.");
        return Pt_CONTINUE;
    }

    // Find server
    bss_coid = name_open(BSS_NAME, 0);
    if (bss_coid == -1) {
        console_print("Error: BSS not found. Start the server.");
        return Pt_CONTINUE;
    }

    // Register in namespace (so BSS and other MTs can find us)
    char my_name;
    sprintf(my_name, "mt_%d", my_phone);
    attach = name_attach(NULL, my_name, 0);

    // BIND OUR CHANNEL TO PhAB GUI LOOP (Async Magic)
    PtAppAddFd(NULL, attach->chid, Pt_FD_READ, background_msg_handler, NULL);

    // Send 2 registration pulses to BSS (as per assignment)
    MsgSendPulse(bss_coid, getprio(0), MT_REG_PID, getpid());
    MsgSendPulse(bss_coid, getprio(0), MT_REG_PHONE, my_phone);

    console_print("Registration request sent...");
    return Pt_CONTINUE;
}

// Call / Answer Button
int btn_call_answer_cb(PtWidget_t *widget, ApInfo_t *apinfo, PtCallbackInfo_t *cbinfo)
{
    if (current_state == STATE_REGISTERED) {
        // --- OUTGOING CALL ---
        char *target_str;
        PtGetResource(ABW_txt_pers_num, Pt_ARG_TEXT_STRING, &target_str, 0);
        target_phone = atoi(target_str);

        console_print("Calling...");
        MsgSendPulse(bss_coid, getprio(0), MT_CALL, target_phone);

    } else if (current_state == STATE_RINGING) {
        // --- ACCEPT INCOMING CALL ---
        console_print("Call accepted.");
        update_fsm(STATE_IN_CALL);
    }
    return Pt_CONTINUE;
}

// 3. End Call Button
int btn_end_call_cb(PtWidget_t *widget, ApInfo_t *apinfo, PtCallbackInfo_t *cbinfo)
{
    if (current_state == STATE_IN_CALL || current_state == STATE_RINGING) {
        MsgSendPulse(bss_coid, getprio(0), MT_HANGUP, 0);
        console_print("Call ended.");
        update_fsm(STATE_REGISTERED);
    }
    return Pt_CONTINUE;
}

// Send Message Button (direct to another client)
int btn_send_msg_cb(PtWidget_t *widget, ApInfo_t *apinfo, PtCallbackInfo_t *cbinfo)
{
    if (current_state != STATE_IN_CALL)
        return Pt_CONTINUE;

    char *msg_text;
    PtGetResource(ABW_mtxt_my_msg, Pt_ARG_TEXT_STRING, &msg_text, 0);

    if (strlen(msg_text) > 0) {
        // Connect directly to peer
        char target_name;
        sprintf(target_name, "mt_%d", target_phone);
        int peer_coid = name_open(target_name, 0);

        if (peer_coid != -1) {
            chat_msg_t chat_req;
            chat_req.type = CHAT_MSG_TYPE;
            strncpy(chat_req.text, msg_text, 255);

            // Send payload
            MsgSend(peer_coid, &chat_req, sizeof(chat_req), NULL, 0);
            name_close(peer_coid);

            // Output to self for history
            char buf;
            sprintf(buf, "[You]: %s", msg_text);
            PtTextModify(ABW_mtxt_incom_msg, 0, 0, -1, buf, strlen(buf));
            PtTextModify(ABW_mtxt_incom_msg, 0, 0, -1, "\n", 1);

            // Clear input field
            PtSetResource(ABW_mtxt_my_msg, Pt_ARG_TEXT_STRING, "", 0);
        } else {
            console_print("Send error: connection to peer lost.");
        }
    }
    return Pt_CONTINUE;
}
