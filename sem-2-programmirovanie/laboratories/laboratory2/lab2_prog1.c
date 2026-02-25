#include <stdio.h>
#include <stdlib.h>
#include <time.h>

#define GREETMESSAGE printf("lab 2. program 1 | group 424-1 | variant 1 | Badulin Ilya")
#define NL printf("\n")
#define COLUMNS 10

void exitMsg(void)
{
	printf("press 'ENTER' to exit..");
	getchar();
}

void printArr(int* array, int size)
{
	int i;
	for(i=1;i-1<size;i++)
    {
    	printf("%3d ", array[i-1]);
    	
    	if(i%COLUMNS == 0) NL;
	}
	if(i%COLUMNS != 0) NL;
}

int* selectionSort(int* array, int size)
{
	int i, j, min, minIndex;
	
	for (i=0; i<size-1; i++)
	{
		min=array[i];
		minIndex=i;
		for (j=i+1; j<size; j++)
		{
			if(min>array[j])
			{
				min=array[j];
				minIndex=j;
			}
		}
		array[minIndex] = array[i];
		array[i] = min;
	}
	
	//printf("min: %d", min);
	
	return array;
}

int main(void)
{
	NL;
	GREETMESSAGE;
	NL;

	NL;
	printf("Enter a size of an array: ");
	int size;
	if(scanf("%d", &size) != 1)
	{
        printf("{n} is too big."); NL;NL;
        exitMsg();
        exit(1);
	}

	
	int* arr = (int*)malloc(sizeof(int)*size);
    
    srand(time(NULL));
    int i;
    for(i=0;i<size;i++)
    {
    	arr[i] = (int)(rand()%199-99); //from (0...198) - 99
	}
	
	printf("original array:");NL;
    printArr(arr, size);NL;
    
    printf("array after selection sort:");NL;
    printArr(selectionSort(arr, size), size);
    
    free(arr);
    
    NL;
	exitMsg();
    return 0;
}
