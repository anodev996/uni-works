#include <stdbool.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <sys/neutrino.h>

#include "protocol.h"

bool get_ca_info(const char *filename, unsigned int *nd, unsigned int *pid, unsigned int *chid)
{
    FILE *file = fopen(filename, "r");
    if (file == NULL) {
        perror("Error opening server file");
        return false;
    }

    if (fscanf(file, "%d %d %d", nd, pid, chid) != 3) {
        fprintf(stderr, "Error: invalid nd/pid/chid format in file \"%s\"\n", filename);
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
        exit(EXIT_FAILURE);
    };

    int coid = ConnectAttach(nd, pid, chid, 0, 0);
    if (coid == -1) {
        perror("ConnectAttach error");
        exit(EXIT_FAILURE);
    }

    printf("Client started. Enter \'q\' to quit.\n");

    char smsg[BUFFER_SIZE];
    char rmsg[BUFFER_SIZE];
    for (;;) {
        printf("command> ");
        if (fgets(smsg, sizeof(smsg), stdin) == NULL)
            break;

        if (strcmp(smsg, "q\n") == 0) {
            break;
        }

        // Save a copy of the input string without '\n' to understand which command was sent
        char cmd_copy[BUFFER_SIZE];
        strcpy(cmd_copy, smsg);
        cmd_copy[strcspn(cmd_copy, "\n")] = 0;

        // Send the message. 'status' will receive the value the server passed as the 2nd arg to
        // MsgReply
        int status = MsgSend(coid, smsg, strlen(smsg) + 1, rmsg, sizeof(rmsg));

        if (status == -1) {
            // If the server returned -1 (e.g., file not found) or network broke
            printf("Server response: %s\n", rmsg);
        } else {
            // Check if it was a 'get' command
            if (strncmp(cmd_copy, "get ", 4) == 0) {
                char *filename = cmd_copy + 4; // Filename starts after "get "
                char out_name[256];            // Fixed buffer size for filename

                // To avoid overwriting the original file, save with a suffix
                snprintf(out_name, sizeof(out_name), "%s_downloaded", filename);

                FILE *f = fopen(out_name, "wb");
                if (f != NULL) {
                    // Write 'status' bytes from the reply buffer to the file
                    fwrite(rmsg, 1, status, f);
                    fclose(f);
                    printf("File successfully downloaded as \"%s\" (%d bytes).\n", out_name,
                           status);
                } else {
                    perror("Error creating local file");
                }
            } else {
                // It was help, list, or something else. Just print the text from the buffer
                printf("%s", rmsg);
            }
        }
    }

    return 0;
}
