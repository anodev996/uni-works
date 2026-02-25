// Badulin Ilya, group 424-1.
// Year I, second semester.
// Laboratory class 6.
//
#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include <errno.h>
#include <time.h>
#include "lab6.h"

int get_digit(int number, int digit_number)
{
	number = number >> digit_number;
	int digit = number & 1;

	return digit;
}

void binary_quicksort(int* array, int arr_index_first, int arr_index_last, int digits_count)
{
    if(digits_count < 0 || arr_index_first == arr_index_last) return;

	int i = arr_index_first, j = arr_index_last, temp;
	while(i<j)
	//while(i<=j)
	{
		while(get_digit(array[i], digits_count) == 0) i++;

		while(get_digit(array[j], digits_count) == 1) j--;

		//while (i <= arr_index_last && get_digit(array[i], digits_count) == 0) i++;

        //while (j >= arr_index_first && get_digit(array[j], digits_count) == 1) j--;

		if(i<=j)
		//if(i<j)
		{
			temp = array[i];
			array[i] = array[j];
			array[j] = temp;
			//
			//i++; j--;
		}
	}
	
	binary_quicksort(array, arr_index_first, j, digits_count-1);

	binary_quicksort(array, i, arr_index_last, digits_count-1);
}

void complexity_binary_quicksort(int* array, int arr_index_first, int arr_index_last, int digits_count, unsigned int* compares, unsigned int* assignments)
{
	(*compares)++;
    if(digits_count < 0 || arr_index_first == arr_index_last) return;

	int i = arr_index_first, j = arr_index_last, temp;
	(*compares)++;
	while(i<j)
	//while(i<=j)
	{
		(*compares)++;
		while(get_digit(array[i], digits_count) == 0)
		{
			 i++;
			 (*compares)++;
		}

		(*compares)++;
		while(get_digit(array[j], digits_count) == 1)
		{
			j--;
			(*compares)++;
		}

		//while (i <= arr_index_last && get_digit(array[i], digits_count) == 0) i++;

        //while (j >= arr_index_first && get_digit(array[j], digits_count) == 1) j--;

		(*compares)++;
		if(i<=j)
		//if(i<j)
		{
			temp = array[i];
			array[i] = array[j];
			array[j] = temp;
			//
			//i++; j--;

			(*assignments)++;
		}

		(*compares)++;
	}
	
	binary_quicksort(array, arr_index_first, j, digits_count-1);

	binary_quicksort(array, i, arr_index_last, digits_count-1);
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
	
	fprintf(f,"%s,%s\n", "number of elements", "compares+assignments");
	
    unsigned int i, j, size = 100, compares, assignments;
	while (size < 10001)
	{	
		fprintf(stdout, "# ");

		compares = 0;
        assignments = 0;

        int* array = create_int_array_dynamic(size);

		for(i=0;i<5;i++)
		{
			srand(time(NULL));
			for(j=0;j<size;j++) array[j] = rand() % 200; //0...199
			
			complexity_binary_quicksort(array, 0, size-1, 7, &compares, &assignments);
		}

		fprintf(f,"%u,%u\n", size, (compares+assignments) / 5);

		size += 100;
		
		free(array);
	}
	
	fclose(f);
}

void debug_print(int* array, int arr_index_first, int arr_index_last, int digits_count)
{
	int i, j;
	for(i=arr_index_first; i<arr_index_last+1; i++)
	{
		fprintf(stdout, "arr[%d] == ", i);

		for(j=3;j>=0;j--)
		{
			if(j != digits_count) fprintf(stdout, "%d", get_digit(array[i], j));
			else fprintf(stdout, "|%d", get_digit(array[i], j));
		}

		fprintf(stdout, " == %d", array[i]); NL2;
	}
}

void debug_binary_quicksort(int* array, int arr_index_first, int arr_index_last, int digits_count)
{
    if(digits_count < 0)
	{
		//
		fprintf(stdout, "RETURNED!"); NL2;

		return;
	}

	int i = arr_index_first, j = arr_index_last, temp;
	while(i<j)
	{
		//
		debug_print(array, arr_index_first, arr_index_last, digits_count);

		while(get_digit(array[i], digits_count) == 0) i++;

		while(get_digit(array[j], digits_count) == 1) j--;

		if(i<=j)
		{
			temp = array[i];
			array[i] = array[j];
			array[j] = temp;

			//
			fprintf(stdout, "SWAP!"); NL;
			debug_print(array, arr_index_first, arr_index_last, digits_count); NL;
		}
	}
	
	if(arr_index_first != j)
	{
		//
		fprintf(stdout, "NEW RUN!\nsubarray with 0"); NL2;

		binary_quicksort(array, arr_index_first, j, digits_count-1);
	}

	if(i != arr_index_last)
	{
		//
		fprintf(stdout, "NEW RUN!\nsubarray with 1"); NL2;

		binary_quicksort(array, i, arr_index_last, digits_count-1);
	}
}