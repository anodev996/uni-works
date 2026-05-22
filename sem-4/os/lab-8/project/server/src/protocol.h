#pragma once
#include <sys/neutrino.h>

#define BSS_NAME "bss_server"
#define MAX_MT 64

// Pulse codes client --> server
#define MT_REG_PID (_PULSE_CODE_MINAVAIL + 1)   // Registration - send PID
#define MT_REG_PHONE (_PULSE_CODE_MINAVAIL + 2) // Registration - send phone number
#define MT_CALL (_PULSE_CODE_MINAVAIL + 3)      // Call request
#define MT_HANGUP (_PULSE_CODE_MINAVAIL + 4)    // End call / Hang up

// Server --> client
#define BSS_REG_OK (_PULSE_CODE_MINAVAIL + 5)     // Registration successful
#define BSS_REG_REJECT (_PULSE_CODE_MINAVAIL + 6) // Registration denied
#define BSS_CALL_OK (_PULSE_CODE_MINAVAIL + 7)    // Call successful (subscriber found)
#define BSS_CALL_FAIL (_PULSE_CODE_MINAVAIL + 8)  // Call failed (busy or offline)
#define BSS_INCOMING (_PULSE_CODE_MINAVAIL + 9)   // Incoming call (value contains caller's phone)
#define BSS_HANGUP (_PULSE_CODE_MINAVAIL + 10)    // Hung up from end side

// Structure for text messages between clients
#define CHAT_MSG_TYPE 0x7777

typedef struct {
    uint16_t type; // Message type to distinguish from system messages
    char text;     // Message text payload
} chat_msg_t;
