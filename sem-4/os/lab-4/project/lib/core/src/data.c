#include "data.h"

#include <stdio.h>
#include <unistd.h>

void *process_file(void *arg)
{
    printf("Работаю...\n");
    sleep(2);
    return NULL;
}
