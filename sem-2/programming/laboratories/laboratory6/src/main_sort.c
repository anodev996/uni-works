// Badulin Ilya, group 424-1.
// Year I, second semester.
// Laboratory class 6.
//
#include <stdlib.h>
#include <stdio.h>
#include <time.h>
#include "lab6.h"

#define GREET_FILE_PATH "./README.md"

int main(void)
{
    print_file(GREET_FILE_PATH); NL2;

    unsigned int size;
    fprintf(stdout, "Enter the size of an array to sort.\nMust be an integer number, >1, no spaces allowed: ");
    input_one_unsigned_int(&size);
    //
    //size = 5;

    if(size < 2)
    {
        fprintf(stderr, "Invalid size entered."); NL2;
        exit_messsage(1);
    }

    int* array = create_int_array_dynamic(size);

    unsigned int range_min;
    fprintf(stdout, "Enter the MINIMUM number of the range\nMust be an integer number, >=0, no spaces allowed: ");
    input_one_unsigned_int(&range_min);
    //
    //range_min = 0;

    unsigned int range_max;
    fprintf(stdout, "Enter the MAXIMUM number of the range\nMust be an integer number, >'MINIMUM number of the range', no spaces allowed: ");
    input_one_unsigned_int(&range_max);
    //
    //range_max = 8;

    if(range_min >= range_max)
    {
        fprintf(stderr, "Invalid range entered."); NL2;
        exit_messsage(1);
    }
    
    srand(time(NULL));
    unsigned int i;
    int rand_range = range_max - range_min + 1;
    for(i=0;i<size;i++) array[i] = (rand() % rand_range) + range_min; //from range_min to range_max
    //
    /*array[0] = 4;
    array[1] = 6;
    array[2] = 8;
    array[3] = 7;
    array[4] = 1;*/

    fprintf(stdout, "Original array:"); NL;
    print_int_array(array, size, 10); NL2;

    int digits_count = -1;
    unsigned int num = range_max;
    for(; num>0; digits_count++) num = num >> 1;
    //
    //fprintf(stdout, "range_min: %u", range_min); NL;
    //fprintf(stdout, "range_max: %u", range_max); NL;
    //fprintf(stdout, "digits_count: %d", digits_count); NL2;
    
    binary_quicksort(array, 0, size-1, digits_count);

    fprintf(stdout, "Array after Binary Quicksort:"); NL;
    print_int_array(array, size, 10); NL2;

    free(array);

    exit_messsage(0);
}