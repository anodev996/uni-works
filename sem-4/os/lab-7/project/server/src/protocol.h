#pragma once

#define SERVER_INFO_FILE "/tmp/server_info"
#define MAX_MSG_LEN 256

typedef struct {
    char text[MAX_MSG_LEN];
} client_msg_t;

typedef struct {
    char text[MAX_MSG_LEN];
} server_msg_t;
