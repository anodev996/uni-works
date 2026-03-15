#include <stdlib.h>
#include <stdio.h>
#include <limits.h>
#include <time.h>

#define GREETFILENAME "../readme.txt"
#define NL printf("\n")
#define FILENAME "lab3_prog2_table.csv"

void exitMsg(int errorCode);

void printFile(char* fileName);

unsigned int getOneNaturalNum(unsigned int n);

void printArray(int* array, unsigned int size, unsigned int columns);

int* cocktailSort(int* array, unsigned int size, unsigned int* comparesPlusSwaps)
{
    int i, j, temp, swapFlag=0;
    unsigned int compares=0, swaps=0;

    i=0;
    while(1)
    {
        for(j=0; j < size - i - 1; j++)
        {
            compares++;

            if(array[j] > array[j+1])
            {   
                swapFlag = 1;

                temp = array[j];
                array[j] = array[j+1];
                array[j+1] = temp;

                swaps++;
            }
        }
        if(swapFlag==0) break;
        swapFlag=0;

        for(j=size - i - 1; j > i; j--)
        {
            compares++;

            if(array[j] < array[j-1])
            {
                swapFlag = 1;

                temp = array[j];
                array[j] = array[j-1];
                array[j-1] = temp;

                swaps++;
            }
        }
        if(swapFlag==0) break;
        swapFlag=0;

        i++;
    }

    *comparesPlusSwaps = compares + swaps;

    return array;
}

void checkComplexity(char* filename)
{
	FILE* f = fopen(filename, "w");
	if(f == NULL)
	{
		printf("open file error"); NL;
		exitMsg(1);
	}
	
	printf("checkComplexity iterations:"); NL;
	
	fprintf(f,"%s;%s\n", "elems", "compares+swaps");
	
	int i, j;
    unsigned int elems = 100, comparesPlusSwaps;
	while (elems < 10001)
	{	
		printf("~ ");
		comparesPlusSwaps = 0;
		
		int* array = (int*)malloc(sizeof(int) * elems);
		for(i=0;i<5;i++)
		{
			for(j=0;j<elems;j++)
		    {
		    	array[j] = (int)(rand()%199-99); //from (0...198) - 99 
			}
			
            cocktailSort(array, elems, &comparesPlusSwaps);
		}
		
		fprintf(f,"%u;%u\n", elems, (comparesPlusSwaps / 5));

		elems += 100;
		
		free(array);
	}
	NL;
	
	fclose(f);
	return;
}

int main(void)
{
    printFile(GREETFILENAME); NL;NL;
    
	printf("Trying to open %s...", FILENAME); NL;NL;
	
	checkComplexity(FILENAME);

    exitMsg(0);
    return 0;
}

/*
==============================================================================
==============================================================================
==============================================================================
*/

void exitMsg(int errorCode)
{
    printf("Press ENTER to exit...");
    getchar();
    if(errorCode != 0) exit(errorCode);
}

void printFile(char* fileName)
{
    FILE* f = fopen(fileName, "r");
    if(f == NULL)
    {
        printf("file opening error");NL;NL;
        exitMsg(1);
    }

    char buffer[256];
    while(fgets(buffer, sizeof(buffer), f) != NULL)
    {
        fputs(buffer, stdout);
    }

    fclose(f);
}

unsigned int getOneNaturalNum(unsigned int n)
{
    char buffer[32];
    int i;
    for(i=0;i<sizeof(buffer)-1;i++) buffer[i] = '\0';

    fgets(buffer, sizeof(buffer), stdin); NL;

    for(i=0;i<sizeof(buffer);i++)
    {
        if( !( buffer[0] != '\n' && buffer[0] != '0' &&
               ( (buffer[i] > 47 && buffer[i] < 58 ) || buffer[i] == '\n' || buffer[i] == '\0') ) )
        {
            printf("Invalid input."); NL;NL;
            exitMsg(2);
        }
    }
    
    sscanf(buffer, "%u", &n);
    if (n>UINT_MAX-1)
    {
        printf("{n} is too big."); NL;NL;
        exitMsg(2);
    }

    return n;
}

void printArray(int* array, unsigned int size, unsigned int columns)
{
    int i;
    for(i=1;i-1<size;i++)
    {
        printf("%2d ", array[i-1]);
        
        if( i % columns == 0) NL;
    }
    if( (i-1) % columns != 0) NL;
}