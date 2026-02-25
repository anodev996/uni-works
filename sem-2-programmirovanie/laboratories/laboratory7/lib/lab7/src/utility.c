/*Badulin Ilya, group 424-1.
First year, second semester.
Laboratory class 7.*/
#include <stdlib.h>
#include <stdio.h>
#include <errno.h>
#include <string.h>
#include <limits.h>
#include "lab7.h"

void print_and_exit(int exit_code)
{
    fprintf(stdout, "Press ENTER to exit...");
    getchar();
    exit(exit_code);
}

void create_int_array_dynamic(int** array, size_t size)
{
    *array = (int*)malloc(size * sizeof(int));
    if (array == NULL)
    {
        fprintf(stderr, "ERROR create_int_array_dynamic(): %s", strerror(errno));
        print_and_exit(EXIT_FAILURE);
    }
}

void print_int_array(int* array, size_t size, size_t columns)
{
    size_t i;
    if (size <= columns)
    {
        for (i=0; i<size; ++i) fprintf(stdout, "%3d ", array[i]);
    }
    else if (size > columns)
    {
        for (i=0;i<columns;i++) fprintf(stdout, "%3d ", array[i]);

        for (;i<size;i++)
        {
            if (i % columns == 0) NL;
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

void input_one_size_t(size_t* input)
{
    char buffer[32] = {0};

    fgets(buffer, sizeof(buffer), stdin);

    size_t i;
    for(i=0;i<sizeof(buffer);i++)
    {
        if( !( buffer[0] != '\n' && ( (buffer[i] > 47 && buffer[i] < 58 ) || buffer[i] == '\n' || buffer[i] == '\0') ) )
        {
            fprintf(stderr, "Invalid input."); NL2;
            print_and_exit(EXIT_FAILURE);
        }
    }
    
    sscanf(buffer, "%zu", input);
    if (*input > UINT_MAX-1)
    {
        fprintf(stderr, "The entered number is too big."); NL2;
        print_and_exit(EXIT_FAILURE);
    }
}

//size of buffer have to be bigger by 2 then maximum string length
int stream_get_string_strict(FILE* stream, char* buffer, size_t buffer_size)
{
    if (buffer_size < 3)
    {
        fprintf(stdout, "strict_fscanf(): buffer < 3");
        return 0;
    }

    //assign '\0' to last and penultimate elements of buffer 
    buffer[buffer_size-1] = '\0';
    buffer[buffer_size-2] = '\0';

    //prepare %<buffer_size - 1>s for fscanf()
    char format[32];
    snprintf(format, sizeof(format), "%%%ds", (int)(buffer_size-1));

    int check = fscanf(stream, format, buffer);

    //if penultimate element of buffer is overwritten, consider it as overflow
    if(buffer[buffer_size-2] != '\0')
    {
        fprintf(stdout, "strict_fscanf(): string buffer overflow"); NL;
        return 0;
    }

    return check;
}