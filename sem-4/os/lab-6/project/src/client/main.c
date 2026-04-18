#include <errno.h>
#include <signal.h>
#include <stdbool.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <sys/neutrino.h>
#include <time.h>
#include <unistd.h>

#include "protocol.h"

pid_t server_pid_global = 0;

// Client signal handler: redirects the signal to the server
void client_sig_handler(int signo)
{
    if (server_pid_global > 0) {
        kill(server_pid_global, signo);
    }
}

bool get_ca_info(const char *filename, unsigned int *nd, unsigned int *pid, unsigned int *chid)
{
    FILE *file = fopen(filename, "r");
    if (file == NULL)
        return false;

    if (fscanf(file, "%d %d %d", nd, pid, chid) != 3) {
        fclose(file);
        return false;
    }

    fclose(file);
    return true;
}

int main(void)
{
    unsigned int nd = 0, pid = 0, chid = 0;

    if (get_ca_info(FILENAME, &nd, &pid, &chid) == false) {
        printf("Cannot connect. Is server running?\n");
        exit(EXIT_FAILURE);
    };

    server_pid_global = (pid_t)pid;

    int coid = ConnectAttach(nd, pid, chid, 0, 0);
    if (coid == -1) {
        perror("ConnectAttach error");
        exit(EXIT_FAILURE);
    }

    // --- SETUP SIGNALS AND TIMERS ---

    struct sigaction act;
    sigemptyset(&act.sa_mask);
    act.sa_flags = 0;
    act.sa_handler = client_sig_handler;
    sigaction(TIMER1_SIG, &act, NULL);
    sigaction(TIMER2_SIG, &act, NULL);

    // Timer 1 (3 seconds)
    struct sigevent event1;
    timer_t timer1;
    SIGEV_SIGNAL_INIT(&event1, TIMER1_SIG);
    timer_create(CLOCK_REALTIME, &event1, &timer1);

    struct itimerspec itime1;
    itime1.it_value.tv_sec = 3;
    itime1.it_value.tv_nsec = 0;
    itime1.it_interval.tv_sec = 3;
    itime1.it_interval.tv_nsec = 0;
    timer_settime(timer1, 0, &itime1, NULL);

    // Timer 2 (4.44 seconds)
    struct sigevent event2;
    timer_t timer2;
    SIGEV_SIGNAL_INIT(&event2, TIMER2_SIG);
    timer_create(CLOCK_REALTIME, &event2, &timer2);

    struct itimerspec itime2;
    itime2.it_value.tv_sec = 4;
    itime2.it_value.tv_nsec = 440000000;
    itime2.it_interval.tv_sec = 4;
    itime2.it_interval.tv_nsec = 440000000;
    timer_settime(timer2, 0, &itime2, NULL);

    printf("Client started with 2 timers (3s & 4.44s). Enter 'q' to quit.\n");

    char input[256];

    // Main loop waiting for 'q' to quit
    for (;;) {
        if (fgets(input, sizeof(input), stdin) == NULL) {
            // fgets interrupted by timer signal
            if (errno == EINTR) {
                clearerr(stdin);
                continue;
            }
            break;
        }

        if (strcmp(input, "q\n") == 0) {
            break;
        }
    }

    // Cleanup timers
    timer_delete(timer1);
    timer_delete(timer2);

    return 0;
}
