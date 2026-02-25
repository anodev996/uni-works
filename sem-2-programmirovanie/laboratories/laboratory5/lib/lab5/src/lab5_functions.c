#include <stdlib.h>
#include <stdio.h>
#include <errno.h>
#include <string.h>
#include <time.h>
#include "lab5.h"

void merge_two_array(int* array_whole, int* array_left, unsigned int size_left, int* array_right, unsigned int size_right)
{
    unsigned int i=0, j=0, k=0;

    while(j < size_left && k < size_right)
    {
        if(array_left[j] < array_right[k])
        {
            array_whole[i] = array_left[j];
            i++; j++;
        }
        else
        {
            array_whole[i] = array_right[k];
            i++; k++;
        }
    }

    if(j<size_left)
    {
        for(;j<size_left;i++,j++) array_whole[i] = array_left[j];
    }
    else
    {
        for(;k<size_right;i++,k++) array_whole[i] = array_right[k];
    }
}

void merge_sort(int* array_whole, unsigned int size_whole)
{
    if(size_whole > 1)
    {
        unsigned int size_left = size_whole/2;
        unsigned int size_right = size_left;
        if(size_whole % 2 != 0) size_right += 1;

        int* array_left = create_int_array_dynamic(size_left);
        int* array_right = create_int_array_dynamic(size_right);

        unsigned int i, j;
        for(i=0; i<size_left; i++) array_left[i] = array_whole[i];
        for(j=0; j<size_right; i++,j++) array_right[j] = array_whole[i];

        fprintf(stdout, " left  | ");
        print_array(array_left, size_left, 10); NL;
        fprintf(stdout, " right | ");
        print_array(array_right, size_right, 10); NL2;

        merge_sort(array_left, size_left);
        merge_sort(array_right, size_right);

        merge_two_array(array_whole, array_left, size_left, array_right, size_right);

        free(array_left);
        free(array_right);
    }
}

void complexity_merge_two_array(int* array_whole, int* array_left, unsigned int size_left, int* array_right, unsigned int size_right, unsigned int* compares, unsigned int* assignments)
{
    unsigned int i=0, j=0, k=0;

    (*compares)++;
    while(j < size_left && k < size_right)
    {
        (*compares)++;
        if(array_left[j] < array_right[k])
        {
            (*assignments)++;
            array_whole[i] = array_left[j];
            i++; j++;
        }
        else
        {
            (*assignments)++;
            array_whole[i] = array_right[k];
            i++; k++;
        }
    }

    (*compares)++;
    if(j<size_left)
    {
        (*compares)++;
        for(;j<size_left;i++,j++)
        {
            (*assignments)++;
            array_whole[i] = array_left[j];
            (*compares)++;
        }
    }
    else
    {
        (*compares)++;
        for(;k<size_right;i++,k++) 
        {
            (*assignments)++;
            array_whole[i] = array_right[k];
            (*compares)++;
        }
    }

    (*compares)++;
}

void complexity_merge_sort(int* array_whole, unsigned int size_whole, unsigned int* compares, unsigned int* assignments)
{
    (*compares)++;
    if(size_whole > 1)
    {
        unsigned int size_left = size_whole/2;
        unsigned int size_right = size_left;
        if(size_whole % 2 != 0) size_right += 1;

        int* array_left = create_int_array_dynamic(size_left);
        int* array_right = create_int_array_dynamic(size_right);

        unsigned int i, j;
        (*compares)++;
        for(i=0; i<size_left; i++)
        {
            (*assignments)++;
            array_left[i] = array_whole[i];
            (*compares)++;
        }
        (*compares)++;
        for(j=0; j<size_right; i++,j++)
        {
            (*assignments)++;
            array_right[j] = array_whole[i];
            (*compares)++;
        }

        complexity_merge_sort(array_left, size_left, compares, assignments);
        complexity_merge_sort(array_right, size_right, compares, assignments);

        complexity_merge_two_array(array_whole, array_left, size_left, array_right, size_right, compares, assignments);

        free(array_left);
        free(array_right);
    }
}

void complexity(char* filename)
{
	FILE* f = fopen(filename, "w");
	if(f == NULL)
    {
        fprintf(stderr, "ERROR complexity(): %s", strerror(errno));
        exit_messsage(1);
    }
	
	fprintf(stdout, "complexity() iterations:"); NL;
	
	fprintf(f,"%s;%s\n", "number of elements", "compares+assignments");
	
    unsigned int i, size = 100, compares, assignments;
	while (size < 10001)
	{	
		fprintf(stdout, "# ");

		compares = 0;
        assignments = 0;
		
        int* array = create_int_array_dynamic(size);
        srand(time(NULL));
        for(i=0;i<size;i++) array[i] = rand()%199-99; //from (0...198) - 99
        
        complexity_merge_sort(array, size, &compares, &assignments);

		fprintf(f,"%u;%u\n", size, (compares+assignments));

		size += 100;
		
		free(array);
	}
	
	fclose(f);
}