// Badulin Ilya, group 424-1.
// Year I, second semester.
// Laboratory class 6.
// // // // // // // // // // // // // // // // // // // // // // // // // //
#include <stdlib.h>
#include <stdio.h>
#include "lab6.h"

#define GREET_FILE_PATH "./README.md"
#define TABLE_PATH "./__laboratory6_related/sort_complexity.csv"

int main(void)
{
    print_file(GREET_FILE_PATH); NL2;
    
	fprintf(stdout, "Trying to create %s...", TABLE_PATH); NL2;

    complexity(TABLE_PATH); NL2;

    fprintf(stdout, "ALL DONE!\nUwU"); NL2;
    
    exit_messsage(0);
}