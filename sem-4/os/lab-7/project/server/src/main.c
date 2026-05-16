#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <sys/neutrino.h>
#include <unistd.h>

#include "protocol.h"

#define CHANNEL_NUM 0

int main(void)
{
    int chid = ChannelCreate(CHANNEL_NUM);
    if (chid == -1) {
        perror("ChannelCreate error");
        return EXIT_FAILURE;
    }

    FILE *f = fopen(SERVER_INFO_FILE, "w");
    if (f == NULL) {
        perror("Info file creation error");
        return EXIT_FAILURE;
    }
    fprintf(f, "%d %d %d\n", CHANNEL_NUM, getpid(), chid);
    fclose(f);

    printf("Server has started, waiting for clients messages...\n");

    client_msg_t msg_in;
    server_msg_t msg_out;

    for (;;) {
        int rcvid = MsgReceive(chid, &msg_in, sizeof(msg_in), NULL);
        if (rcvid == -1)
            continue;

        printf("Client message:\n%s\n", msg_in.text);

        memset(&msg_out, 0, sizeof(msg_out));
        snprintf(msg_out.text, MAX_MSG_LEN, "Server received message: %s", msg_in.text);

        usleep(4750000);

        MsgReply(rcvid, 0, &msg_out, sizeof(msg_out));
    }

    return 0;
}
