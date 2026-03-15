#include <stdio.h>
#include <stdlib.h>
#include <time.h>

#define GREETMESSAGE printf("lab 2. program 2 | group 424-1 | variant 1 | Badulin Ilya")
#define NL printf("\n")
#define FILENAME "lab2_prog2_table.csv"

void exitMsg(void)
{
	printf("press 'ENTER' to exit..");
	getchar();
}

int selectionSort(int* array, int size)
{
	int compares = 0, swaps = 0;
	
	int i, j, min, minIndex;
	for (i=0; i<size-1; i++)
	{
		min=array[i];
		minIndex=i;
		for (j=i+1; j<size; j++)
		{
			compares++;
			if(min>array[j])
			{
				min=array[j];
				minIndex=j;
				
				swaps++;
			}
		}
		array[minIndex] = array[i];
		array[i] = min;
		
		swaps++;
	}
	return compares + swaps;
}

void checkComplexity(char* filename)
{
	FILE* f = fopen(filename, "w");
	if(f == NULL)
	{
		printf("open file error");NL;
		exitMsg();
		exit(1);
	}
	
	printf("checkComplexity iterations:");NL;
	
	fprintf(f,"%s;%s\n", "elems", "compares+swaps");
	
	int i, j, elems = 100, total;
	while (elems < 10001)
	{	
		printf("~ ");
		total = 0;
		
		int* arr = (int*)malloc(sizeof(int) * elems);
		for(i=0;i<5;i++)
		{
			for(j=0;j<elems;j++)
		    {
		    	arr[j] = (int)(rand()%199-99); //from (0...198) - 99 
			}
			
			total += selectionSort(arr, elems);
		}
		
		fprintf(f,"%d;%d\n", elems, (total / 5));
		
		elems += 100;
		
		free(arr);
	}
	NL;
	
	fclose(f);
	return;
}

int main(void)
{
	NL;
	GREETMESSAGE;NL;
	
	
	NL;
	printf("Trying to open %s...", FILENAME);NL;
	
	NL;
	checkComplexity(FILENAME);

	NL;
	exitMsg();
    return 0;
}
