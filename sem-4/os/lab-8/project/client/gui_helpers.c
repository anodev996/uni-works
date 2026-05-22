#include "client_logic.h"
#include <stdio.h>
#include <string.h>

app_state_t current_state = STATE_UNREGISTERED;
int my_phone = 0;
int bss_coid = -1;
int target_phone = 0;
name_attach_t *attach = NULL;

// Helper to print text to GUI console
void console_print(const char *msg)
{
    // Append text to PtMultiText
    PtTextModify(ABW_mtxt_console_out, 0, 0, -1, msg, strlen(msg));
    PtTextModify(ABW_mtxt_console_out, 0, 0, -1, "\n", 1);
}

// Update button appearance and state
void set_btn(PtWidget_t *btn, int is_enabled, const char *text)
{
    if (text) {
        PtSetResource(btn, Pt_ARG_TEXT_STRING, text, 0);
    }
    if (is_enabled) {
        PtSetResource(btn, Pt_ARG_FLAGS, Pt_FALSE, Pt_GHOST | Pt_BLOCKED);
        PtSetResource(btn, Pt_ARG_FILL_COLOR, Pg_GREEN, 0); // Green if active
    } else {
        PtSetResource(btn, Pt_ARG_FLAGS, Pt_TRUE, Pt_GHOST | Pt_BLOCKED);
        PtSetResource(btn, Pt_ARG_FILL_COLOR, Pg_GREY, 0); // Grey if disabled
    }
}

// Update the whole GUI by state
void update_fsm(app_state_t new_state)
{
    current_state = new_state;

    switch (current_state) {
    case STATE_UNREGISTERED:
        set_btn(ABW_btn_register, 1, "Register");
        set_btn(ABW_btn_call_answer, 0, "Call");
        set_btn(ABW_btn_end_call, 0, "End Call");
        set_btn(ABW_btn_send_msg, 0, "Send");

        PtSetResource(ABW_txt_my_num, Pt_ARG_FLAGS, Pt_FALSE, Pt_GHOST | Pt_BLOCKED);
        PtSetResource(ABW_txt_pers_num, Pt_ARG_FLAGS, Pt_TRUE, Pt_GHOST | Pt_BLOCKED);
        break;

    case STATE_REGISTERED:
        set_btn(ABW_btn_register, 0, "Registered");
        set_btn(ABW_btn_call_answer, 1, "Call");
        set_btn(ABW_btn_end_call, 0, "End Call");
        set_btn(ABW_btn_send_msg, 0, "Send");

        PtSetResource(ABW_txt_my_num, Pt_ARG_FLAGS, Pt_TRUE, Pt_GHOST | Pt_BLOCKED);
        PtSetResource(ABW_txt_pers_num, Pt_ARG_FLAGS, Pt_FALSE, Pt_GHOST | Pt_BLOCKED);
        target_phone = 0;
        break;

    case STATE_RINGING:
        set_btn(ABW_btn_call_answer, 1, "Answer");
        set_btn(ABW_btn_end_call, 1, "Reject");
        set_btn(ABW_btn_send_msg, 0, "Send");
        PtSetResource(ABW_txt_pers_num, Pt_ARG_FLAGS, Pt_TRUE, Pt_GHOST | Pt_BLOCKED);
        break;

    case STATE_IN_CALL:
        set_btn(ABW_btn_call_answer, 0, "In Call");
        set_btn(ABW_btn_end_call, 1, "End Call");
        set_btn(ABW_btn_send_msg, 1, "Send");
        PtSetResource(ABW_txt_pers_num, Pt_ARG_FLAGS, Pt_TRUE, Pt_GHOST | Pt_BLOCKED);
        break;
    }
}
