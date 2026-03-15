#include <stdlib.h>
#include <stdio.h>
#include <limits.h>
#include <time.h>

#define GREET_FILE_NAME "../readme.txt"
#define NL printf("\n")
#define TABLE_NAME "lab4_prog2_table.csv"

void exitMsg(int errorCode);

void printFile(char* fileName);

unsigned int getOneNaturalNum(unsigned int n);

void printArray(int* array, unsigned int size, unsigned int columns);

unsigned int partition(int* array, unsigned int left, unsigned int right, unsigned int* comparesAndSwaps)
{
    int v = array[ (left+right)/2 ];

    unsigned int i = left, j = right;

    (*comparesAndSwaps)++;
    while (i <= j)
    {
        (*comparesAndSwaps)++;
        while (array[i] < v)
        {
            i++;
            (*comparesAndSwaps)++;
        }

        (*comparesAndSwaps)++;
        while (array[j] > v)
        {
            j--;
            (*comparesAndSwaps)++;
        }

        (*comparesAndSwaps)++;
        if (i >= j) break;
        
        (*comparesAndSwaps)+=3;
        array[i] = array[i]^array[j];
        array[j] = array[j]^array[i];
        array[i] = array[i]^array[j];
        
        /*
        int temp = array[i];
        array[i] = array[j];
        array[j] = temp;
        */

        i++;
        j--;

        (*comparesAndSwaps)++;
    }

    return j;
}

int* quickSort(int* array, unsigned int left, unsigned int right, unsigned int* comparesAndSwaps)
{
    (*comparesAndSwaps)++;
    if (left < right)
    {
        unsigned int q = partition(array, left, right, comparesAndSwaps);
        quickSort(array, left, q, comparesAndSwaps);
        quickSort(array, q+1, right, comparesAndSwaps);
    }

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
	
	fprintf(f,"%s;%s\n", "elems", "comparesAndSwaps");
	
    unsigned int i, j, elems = 100, comparesAndSwaps;
	while (elems < 10001)
	{	
		printf("# ");

		comparesAndSwaps = 0;
		
		int* array = (int*)malloc(sizeof(int) * elems);

		for(i=0;i<5;i++)
		{
            srand(time(NULL));
			for(j=0;j<elems;j++)
		    {
		    	array[j] = (int)(rand()%199-99); //from (0...198) - 99
			}
			
            quickSort(array, 0, elems-1, &comparesAndSwaps);
		}
		
		fprintf(f,"%u;%u\n", elems, (comparesAndSwaps / 5));

		elems += 100;
		
		free(array);
	}
	NL;
	
	fclose(f);
	return;
}

int main(void)
{
    printFile(GREET_FILE_NAME); NL;NL;
    
	printf("Trying to open %s...", TABLE_NAME); NL;NL;
	
	checkComplexity(TABLE_NAME);

    exitMsg(0);
    return 0;
}

//============================================================================
//============================================================================
//============================================================================

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
        perror(fileName);
        return;
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
        printf("%3d ", array[i-1]);
        
        if( i % columns == 0) NL;
    }
    if( (i-1) % columns != 0) NL;
}