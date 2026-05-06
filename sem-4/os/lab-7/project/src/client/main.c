#include <sys/neutrino.h>

#include <sys/iomsg.h>

#include <stdio.h>

#include <string.h>

#include <errno.h>

#include <process.h>

#include <stdlib.h>

int main(void) {

int chid, rcvid, pid;

char mess [512];

FILE *f;

struct timespec ts;

pid = getpid();

chid = ChannelCreate(0);

f = fopen("server.txt", "w");

fprintf(f, "%d %d\n", pid, chid);

fclose(f);

printf("Server: PID = %d CHID = %d\n", pid, chid);

while (1) {

rcvid = MsgReceive(chid, mess, sizeof(mess), NULL);

if (rcvid <= 0) continue;

printf("From 1 widget: %s\n\n", mess);

ts.tv sec = 4;

ts.tv_nsec = 750000000;

nanosleep(&ts, NULL);

printf("To 2 widget: %s\n\n", mess);

MsgReply(rcvid, EOK, mess, strlen(mess) + 1);

}
