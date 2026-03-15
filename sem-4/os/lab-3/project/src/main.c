/* Build and run
   gcc src/main.c -o build/prog && ./build/prog

   Create .iso for QNX
   genisoimage -o ~/Downloads/qnx_data.iso \
   -J -R -V "QNX_DATA" ./src  */
#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>

#define PID_FNAME "/tmp/child_pid"
#define MAX_LEN 128

int main()
{
    printf("(parent): Hello, PID=%d\n", getpid());

    FILE *fp = fopen(PID_FNAME, "r");
    if (fp) {
        char buf[MAX_LEN];
        char *end_ptr;
        char cmd[MAX_LEN];

        while (fgets(buf, sizeof(buf), fp)) {
            long pid_orphan = strtol(buf, &end_ptr, 10);
            snprintf(cmd, sizeof(cmd), "kill -9 %ld", pid_orphan);
            system(cmd);
            printf("(parent): Orphan child PID=%ld killed successfully!\n",
                   pid_orphan);
        };

        fclose(fp);
        remove(PID_FNAME);
        printf("(parent): Bye\n");
        return 0;
    };

    if (fork() == 0) {
        printf("(child ): Hello, PID=%d\n", getpid());

        fp = fopen(PID_FNAME, "w");
        fprintf(fp, "%d", getpid());
        fclose(fp);

        char spinner[] = "|/-\\";
        int i = 0;
        while (1) {
            usleep(200000);
            printf("\r(child ): I'm still here %c", spinner[i++ % 4]);
            fflush(stdout);
        }
    }

    printf("(parent): Bye\n");
    return 0;
}
