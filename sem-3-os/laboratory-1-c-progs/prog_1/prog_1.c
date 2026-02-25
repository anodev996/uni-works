#include <stdio.h>
#include <stdlib.h>
#include <time.h>

#define SIZE 60000

int* selection_sort(int* array, int size);

int main(void)
{
	int* arr = (int*)malloc(sizeof(int) * SIZE);
    
    srand(time(NULL));
    for(int i = 0; i < SIZE; i++)
    {
    	arr[i] = (int)(rand() % 100); 
	}
   
    selection_sort(arr, SIZE);
    free(arr);
    printf("DONE\n");
    return 0;
}

int* selection_sort(int* array, int size)
{
	int i;
	int j;
	int min;
	int min_index;
	
	for (i = 0; i < size - 1; i++)
	{
		min = array[i];
		min_index = i;
		for (j = i + 1; j < size; j++)
		{
			if(min > array[j])
			{
				min = array[j];
				min_index = j;
			}
		}
		array[min_index] = array[i];
		array[i] = min;
	}

	return array;
}