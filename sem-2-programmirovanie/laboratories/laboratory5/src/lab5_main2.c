#include <stdlib.h>
#include <stdio.h>
#include "lab5.h"

#define GREET_FILE_PATH "./README.md"
#define TABLE_PATH "./__laboratory5_related/lab5_main2_table.csv"

int main(void)
{
    print_file(GREET_FILE_PATH); NL2;
    
	fprintf(stdout, "Trying to create %s...", TABLE_PATH); NL2;
    complexity(TABLE_PATH); NL2;
    fprintf(stdout, "ALL DONE!\nUwU"); NL2;
    
    exit_messsage(0);
}