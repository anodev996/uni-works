/* Badulin Ilya, group 424-1.
First year, second semester.
Laboratory class 8.

Variant: */

#include <stdlib.h>
#include <stdio.h>
#include <time.h>
#include "lab8.h"

#define GREET_FILE_PATH "./src/main.c"

int main(void)
{
#ifdef _WIN32
    system("chcp 65001 > nul");
#endif

    print_file(GREET_FILE_PATH, 5); nl(2);
    
    print_and_exit(EXIT_SUCCESS);
}