#include <stdio.h>

#include <stdlib.h>

#include <unistd.h>

#include <string.h>

#include <sys/neutrino.h>

#include "ablibs.h"

#include "abimport.h"

#include "proto.h"

int send(PtWidget_t *widget, Apinfo_t *apinfo, PtCallbackInfo_t *cbinfo ){

char *text;

char output [256];

int pid, chid, coid;

FILE *f

FILE *log:

f = fopen("/home/student/7/server.txt", "r");

fscanf(f, "%d %d", &pid, &chid);

close(f);

coid ConnectAttach(0, pid, chid, 0, 0);

PtGetResource(ABW_in, Pt_ARG_TEXT_STRING, &text, 0);

MsgSend(coid, text, strlen(text) + 1, output, sizeof(output));

PtSetResource(ABW_out, Pt_ARG_TEXT_STRING, output, 0);

return( Pt_CONTINUE );

}