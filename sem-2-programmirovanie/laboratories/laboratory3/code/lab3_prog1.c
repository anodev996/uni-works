#include <stdlib.h>
#include <stdio.h>
#include <limits.h>
#include <time.h>

#define GREETFILENAME "../readme.txt"
#define NL printf("\n")

void exitMsg(int errorCode);

void printFile(char* fileName);

unsigned int getOneNaturalNum(unsigned int n);

void printArray(int* array, unsigned int size, unsigned int columns);

int* cocktailSort(int* array, unsigned int size)
{
    int i, j, temp, switchFlag=0;

    i=0;
    while(i<size-1)
    {

        for(j=0; j < size - i - 1; j++)
        {
            if(array[j] > array[j+1])
            {
                switchFlag = 1;

                temp = array[j];
                array[j] = array[j+1];
                array[j+1] = temp;
            }
        }
        if(switchFlag==0) break;
        switchFlag=0;

        printf("~~( %d )~~> array[%d]", array[j], size - i - 1); NL;NL;
        printArray(array, size, 10); NL;

        for(j=size - i - 1; j > i; j--)
        {
            if(array[j] < array[j-1])
            {
                switchFlag = 1;

                temp = array[j];
                array[j] = array[j-1];
                array[j-1] = temp;
            }
        }
        if(switchFlag==0) break;
        switchFlag=0;

        printf("array[%d] <~~( %d )~~", i, array[j]); NL;NL;
        printArray(array, size, 10); NL;

        printf("=== ITERATION %d DONE ===", i+1); NL;NL;NL;

        i++;
    }

    return array;
}

int main(void)
{
    printFile(GREETFILENAME); NL;NL;

    unsigned int size;

    printf("Enter the size of an array to sort. Natural number {n}, no spaces allowed: ");
    size = getOneNaturalNum(size);

    int* array = (int*)malloc(sizeof(int)*size);
    int* arrayToSort = (int*)malloc(sizeof(int)*size);
    if(array == NULL || arrayToSort == NULL)
    {
        printf("Memory allocation error."); NL;
        exitMsg(1)
    }
    
    srand(time(NULL));
    int i;
    for(i=0;i<size;i++)
    {
        array[i] = (int)(rand()%199-99); //from (0...198) - 99
        //array[i] = (int)(rand()%19-9);

        arrayToSort[i] = array[i];
    }

    //int array2[9] = {4,2,3,8,9,5,1,6,7};
    //int arrayToSort2[9] = {4,2,3,8,9,5,1,6,7};
    
    printf("Original array:"); NL;
    printArray(array, size, 10); NL;

    printf("Cocktail sort's work:"); NL;NL;
    cocktailSort(arrayToSort, size); NL;

    printf("Original array:"); NL;
    printArray(array, size, 10); NL;

    printf("Array after cocktail sort:"); NL;
    printArray(arrayToSort, size, 10); NL;

    free(array);
    free(arrayToSort);

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
        perror(fileName);
        fclose(f);
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