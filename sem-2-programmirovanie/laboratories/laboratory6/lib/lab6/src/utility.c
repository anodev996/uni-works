// Badulin Ilya, group 424-1.
// Year I, second semester.
// Laboratory class 6.
// // // // // // // // // // // // // // // // // // // // // // // // // //
#include <stdlib.h>
#include <stdio.h>
#include <errno.h>
#include <string.h>
#include <limits.h>
#include "lab6.h"

#define NL fputc('\n', stdout)
#define NL2 fputs("\n\n", stdout)

void exit_messsage(int exit_code)
{
    fprintf(stdout, "Press ENTER to exit...");
    getchar();
    exit(exit_code);
}

int* create_int_array_dynamic(unsigned int size)
{
    int* array = (int*)malloc(size * sizeof(int));
    if(array == NULL)
    {
        fprintf(stderr, "ERROR create_int_array_dynamic(): %s", strerror(errno));
        exit_messsage(1);
    }

    return array;
}

void print_int_array(int* array, unsigned int size, unsigned int columns)
{
    unsigned int i;

    if(size<=columns)
    {
        for(i=0; i<size; ++i) fprintf(stdout, "%3d ", array[i]);
    }
    else if(size>columns)
    {
        for(i=0;i<columns;i++) fprintf(stdout, "%3d ", array[i]);

        for(;i<size;i++)
        {
            if(i%columns==0) NL;
            fprintf(stdout, "%3d ", array[i]);
        }
    }
}

void print_file(const char* filename)
{
    FILE* f = fopen(filename, "r");
    if(f == NULL)
    {
        fprintf(stderr, "ERROR print_file(): %s", strerror(errno));
        return;
    }

    char buffer[256];
    while(fgets(buffer, sizeof(buffer), f) != NULL) fputs(buffer, stdout);

    fclose(f);
}

void input_one_unsigned_int(unsigned int* input)
{
    char buffer[32];
    unsigned long long i;
    for(i=0;i<sizeof(buffer)-1;i++) buffer[i] = '\0';

    fgets(buffer, sizeof(buffer), stdin); NL;

    for(i=0;i<sizeof(buffer);i++)
    {
        if( !( buffer[0] != '\n' && ( (buffer[i] > 47 && buffer[i] < 58 ) || buffer[i] == '\n' || buffer[i] == '\0') ) )
        {
            fprintf(stderr, "Invalid input."); NL2;
            exit_messsage(1);
        }
    }
    
    sscanf(buffer, "%u", input);
    if (*input > UINT_MAX-1)
    {
        fprintf(stderr, "The entered number is too big."); NL2;
        exit_messsage(1);
    }
}