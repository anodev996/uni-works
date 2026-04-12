#include <dirent.h> // For directory operations
#include <errno.h>  // For EOK
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <sys/neutrino.h>
#include <unistd.h>

#include "protocol.h"

int main(void)
{
    // 1. Create a channel
    int chid = ChannelCreate(0);
    if (chid == -1) {
        perror("ChannelCreate error");
        return EXIT_FAILURE;
    }

    // 2. Write ND, PID, and CHID to a file so the client can find us
    FILE *info_file = fopen(FILENAME, "w");
    if (info_file == NULL) {
        perror("Error creating server info file");
        return EXIT_FAILURE;
    }
    fprintf(info_file, "%d %d %d\n", 0, getpid(), chid);
    fclose(info_file);

    printf("Server Started. Hit \'Ctrl + C\' to end the process.\n");

    char message[BUFFER_SIZE];
    char reply[BUFFER_SIZE];

    while (1) {
        // Receive a message
        int rcvid = MsgReceive(chid, message, sizeof(message), NULL);
        if (rcvid == -1)
            continue;

        // Remove the newline character sent by the client (from fgets)
        message[strcspn(message, "\n")] = 0;
        printf("Received command: \"%s\"\n", message);

        memset(reply, 0, sizeof(reply));
        int reply_status = EOK;
        int reply_size = 0;

        // Parse the first part of the command
        char *cmd = strtok(message, " ");

        if (cmd == NULL) {
            strcpy(reply, "Empty command.\n");
            reply_size = strlen(reply) + 1;
        } else if (strcmp(cmd, "help") == 0) {
            strcpy(reply, "Available commands:\n"
                          " list <dir> -- show directory contents\n"
                          " get <file> -- get file\n"
                          " help -- get list of all available commands\n");
            reply_size = strlen(reply) + 1;
        } else if (strcmp(cmd, "list") == 0 || strcmp(cmd, "dir") == 0) {
            char *path = strtok(NULL, " ");
            if (path == NULL)
                path = "."; // Current directory by default

            DIR *dir = opendir(path);
            if (dir != NULL) {
                struct dirent *ent;
                while ((ent = readdir(dir)) != NULL) {
                    strcat(reply, ent->d_name);
                    strcat(reply, "\n");
                }
                closedir(dir);
            } else {
                strcpy(reply, "Error: directory not found.\n");
            }
            reply_size = strlen(reply) + 1;
        } else if (strcmp(cmd, "get") == 0) {
            char *filename = strtok(NULL, " ");
            if (filename != NULL) {
                FILE *f = fopen(filename, "rb");
                if (f != NULL) {
                    // Read the file directly into the reply buffer (simplified for lab)
                    reply_size = fread(reply, 1, sizeof(reply), f);
                    reply_status = reply_size; // Send size to client as status!
                    fclose(f);
                } else {
                    strcpy(reply, "Error: file not found.\n");
                    reply_status = -1; // Signal error to client
                    reply_size = strlen(reply) + 1;
                }
            } else {
                strcpy(reply, "Error: specify a filename.\n");
                reply_status = -1;
                reply_size = strlen(reply) + 1;
            }
        } else {
            strcpy(reply, "Wrong command, enter \"help\" to get list of all available commands.\n");
            reply_size = strlen(reply) + 1;
        }

        // Return the response to the client
        MsgReply(rcvid, reply_status, reply, reply_size);
    }

    return 0;
}
