#include "protocol.h"

int server_coid = -1;

int connect_to_server()
{
    if (server_coid != -1)
        return 1;

    unsigned int nd, pid, chid;
    FILE *f = fopen(SERVER_INFO_FILE, "r");
    if (!f)
        return 0;

    fscanf(f, "%d %d %d", &nd, &pid, &chid);
    fclose(f);

    server_coid = ConnectAttach(nd, pid, chid, 0, 0);
    return (server_coid != -1);
}

int send_cb(PtWidget_t *widget, ApInfo_t *apinfo, PtCallbackInfo_t *cbinfo)
{
    char *input_text;
    client_msg_t req;
    server_msg_t res;

    if (!connect_to_server()) {
        PtSetResource(ABW_text_output, Pt_ARG_TEXT_STRING, "Error: server isn't running", 0);
        return Pt_CONTINUE;
    }

    PtGetResource(ABW_text_input, Pt_ARG_TEXT_STRING, &input_text, 0);

    if (input_text == NULL || strlen(input_text) == 0) {
        return Pt_CONTINUE;
    }

    memset(&req, 0, sizeof(req));
    strncpy(req.text, input_text, MAX_MSG_LEN - 1);

    int status = MsgSend(server_coid, &req, sizeof(req), &res, sizeof(res));

    if (status == -1) {
        PtSetResource(ABW_text_output, Pt_ARG_TEXT_STRING, "Error: connection lost", 0);
        server_coid = -1;
    } else {
        PtSetResource(ABW_text_output, Pt_ARG_TEXT_STRING, res.text, 0);
    }

    return Pt_CONTINUE;
}
