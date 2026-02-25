#include <stdio.h>
#include <stdlib.h>
#include <time.h>
//#include <locale.h>

#define GREETMESSAGE printf("homework 2 | group 424-1 | variant 1 | Badulin Ilya")
#define NL printf("\n")

/*
int* makeZerosArr(int n)
{
    int* set;
    set = (int*)malloc(n * sizeof(int));
    if (set == NULL)
    {
        printf("memory allocation error");
        exit(1);
    }

    int i;
    for (i=0;i<n;i++)
    {
        set[i] = 0;
    }
    return set;
}
*/

// Функция генерации случайного множества уникальных чисел
int* makeRandomIntSet(int n) 
{
    if (n > 199) 
    {
        return NULL; // Максимум возможных уникальных чисел от -99 до 99
    }

    int* set = malloc(n * sizeof(int));
    if (!set) 
    {
        return NULL;
    }

    int used[199] = {0}; // Массив для отметки использованных чисел
    int count = 0;

    srand(time(NULL));

    while (count < n) 
    {
        int num = (rand() % 199) - 99; // Генерация числа от -99 до 99
        if (!used[num + 99]) 
        {
            used[num + 99] = 1;
            set[count] = num;
            count++;
        }
    }
    return set;
}

int getInt(char* message)
{
    int num;
    while (1)
    {
        printf("%s", message);

        if (scanf("%d", &num) != 1) 
        {
            if (feof(stdin)) 
            {
                NL;
                printf("EOF detected. Exiting...");
                NL;
                exit(1);
            }
        
            while(getchar() != '\n');

            NL;
            continue;
        }

        else return num;
    }
}

void printSet(int* set, int n)
{
    int i;
    printf("{ ");
    for (i=0;i<n-1;i++)
    {   
        printf("%3d, ", set[i]);
    }
    printf("%3d }", set[n-1]);

    NL;
    return;
}

// Функция сравнения для qsort
int compare(const void* a, const void* b) 
{
    return (*(int*)a - *(int*)b);
}

// Генерация сочетаний в лексикографическом порядке
void generateCombinationsInLexicographicOrder(int* set, int n, int k) 
{
    int* c = (int*)malloc(k * sizeof(int));
    if (!c)
    {
        printf("Memory allocation error");
        NL;
        exit(1);
    }

    for (int i = 0; i < k; i++) 
    {
        c[i] = i;
    }

    int count = 1;
    int i, j;
    while (1) 
    {
        printf("%2d) { ", count);
        count++;
        for (i = 0; i < k - 1; i++) 
        {
            printf( "%3d, ", set[ c[i] ] );
        }
        printf( "%3d  }", set[ c[k - 1] ] );
        NL;

        j = k - 1;

        while (j >= 0 && c[j] == n - k + j) 
        {
            j--;
        }

        if (j < 0) 
        {
            break;
        }

        c[j]++;

        for (i = j + 1; i < k; i++) 
        {
            c[i] = c[i - 1] + 1;
        }
    }

    free(c);
}

int main(void)
{
    //setlocale(LC_ALL, ""); // Устанавливаем локаль для корректного вывода кириллицы

    NL;
	GREETMESSAGE;
	NL;

    NL;
    int n = getInt("Enter n (number of elements) 1 <= n <= 199 : ");
    NL;
    int k = getInt("Enter k (combination size)   1 <= k <= n   : ");

    if (n < 1 || n > 199 || k < 1 || k > n) 
    {
        printf("Invalid input. Ensure 1 ≤ k ≤ n ≤ 199.");
        NL;
        return 1;
    }

    int* set = makeRandomIntSet(n);
    if (!set) 
    {
        printf("Memory allocation error.");
        NL;
        return 2;
    }

    NL;
    printf("random set of integer numbers [-99; 99]:");
    NL;
    printSet(set, n);

    // Сортируем множество перед генерацией сочетаний
    qsort(set, n, sizeof(int), compare);

    NL;
    printf("sorted set of random integer numbers [-99; 99]:");
    NL;
    printSet(set, n);

    NL;
    printf("generation of combinations in lexicographic order from the set above:");
    NL;
    generateCombinationsInLexicographicOrder(set, n, k);

    free(set);
    return 0;
}
