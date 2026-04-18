#include <errno.h>
#include <signal.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <sys/neutrino.h>
#include <unistd.h>

#include "protocol.h"

// Signal handler for timer signals from the client
void server_sig_handler(int signo)
{
    if (signo == TIMER1_SIG) {
        printf("[Server] Received signal from Timer 1 (Signal %d)\n", signo);
    } else if (signo == TIMER2_SIG) {
        printf("[Server] Received signal from Timer 2 (Signal %d)\n", signo);
    }
}

int main(void)
{
    int chid = ChannelCreate(0);
    if (chid == -1) {
        perror("ChannelCreate error");
        return EXIT_FAILURE;
    }

    FILE *info_file = fopen(FILENAME, "w");
    if (info_file == NULL) {
        perror("Error creating server info file");
        return EXIT_FAILURE;
    }
    fprintf(info_file, "%d %d %d\n", 0, getpid(), chid);
    fclose(info_file);

    // Register signal handlers
    struct sigaction act;
    sigemptyset(&act.sa_mask);
    act.sa_flags = 0;
    act.sa_handler = server_sig_handler;
    sigaction(TIMER1_SIG, &act, NULL);
    sigaction(TIMER2_SIG, &act, NULL);

    printf("Server started. Waiting for signals. Press Ctrl+C to exit.\n");

    char message[256];

    while (1) {
        // Block and wait. MsgReceive will be interrupted by our signals.
        int rcvid = MsgReceive(chid, message, sizeof(message), NULL);

        if (rcvid == -1) {
            if (errno == EINTR) {
                // System call interrupted by signal (this is expected behavior)
                continue;
            }
            perror("MsgReceive error");
            continue;
        }

        // If any actual message arrives (e.g. from ConnectAttach), just reply with EOK
        MsgReply(rcvid, EOK, NULL, 0);
    }

    return 0;
}
