/*Badulin Ilya, group 424-1.
First year, second semester.
Practice class 5.*/
#include <stdlib.h>
#include <stdio.h>
#include "prac5.h"

#define GREET_FILE_PATH "./README.md"

#define TASK_FILE_PATH "./_practice5_related/text.txt"
#define MAX_WORD_LENGH 256

int main(void)
{
    print_file(GREET_FILE_PATH); NL2;

    fprintf(stdout, "Maximum word lengh is set to: %d", MAX_WORD_LENGH); NL2;

    fprintf(stdout, "       <file text begin>"); NL;
    print_file(TASK_FILE_PATH); NL;
    fprintf(stdout, "       <file text end>"); NL2;

    fprintf(stdout, "Number of the special words: %d", file_count_words(TASK_FILE_PATH, MAX_WORD_LENGH)); NL2;

    exit_messsage(0);
}