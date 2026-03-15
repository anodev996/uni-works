#include <stdlib.h>
#include <stdio.h>
#include <time.h>
#include "lab5.h"

#define GREET_FILE_PATH "./README.md"

/* 
Merge sort's Big O is n*log2(n) all the time. 

*/

int main(void)
{
    print_file(GREET_FILE_PATH); NL2;

    fprintf(stdout, "Enter the size of an array to sort.\nNatural number {n}, no spaces allowed: ");
    unsigned int size;
    input_one_natural(&size);

    int* array = create_int_array_dynamic(size);
    
    srand(time(NULL));
    unsigned int i;
    for(i=0;i<size;i++) array[i] = rand()%199-99; //from (0...198) - 99
    //for(i=0;i<size;i++) array[i] = rand()%19-9; //from (0...18) - 9

    fprintf(stdout, "Original array:"); NL;
    print_array(array, size, 10); NL2;

    merge_sort(array, size);

    fprintf(stdout, "Array after merge sort:"); NL;
    print_array(array, size, 10); NL2;

    free(array);

    exit_messsage(0);
}