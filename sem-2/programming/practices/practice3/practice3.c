/*
practice 3 | group 424-1 | variant 1 | Badulin Ilya

Task:
Write two functions. One is recursive, second is usual.

Variant 1:
Find factorial of natural number {n}.
*/

#include <stdlib.h>
#include <stdio.h>
#include <limits.h>

#define GREETMESSAGE "practice 3 | group 424-1 | variant 1 | Badulin Ilya"
#define NL printf("\n")

void exitMsg(void)
{
    printf("Press ENTER to exit...");
    getchar();
}

unsigned long long usualFactorial(unsigned long long n)
{
    unsigned long long result=1;
    for(n;n>0;n--)
    {
        if( (result*n) < result )
        {
            printf("unsigned long long overflow"); NL;NL;
            exitMsg();
            exit(2);
        }
        
        result *= n;
    }

    return result;
}

unsigned long long recursiveFactorial(unsigned long long n)
{
    if (n == 1) return 1;
    else if (n == 2) return 2;

    // KAK NORMALNO OTLOVIT' PEREPOLNENIE TUT ??

    n *= recursiveFactorial(n-1);
    return n;
}

int main(void)
{
    NL;
    printf(GREETMESSAGE); NL;NL;

    unsigned long long n;
    char buffer[32];
    int i;
    for(i=0;i<sizeof(buffer)-1;i++) buffer[i] = '\0';
    //for(i=0;i<sizeof(buffer)-1;i++) printf("{%c} ", buffer[i]);
    //NL;NL;

    printf("Enter natural number {n} (no spaces allowed): ");
    fgets(buffer, sizeof(buffer), stdin); NL;
    //for(i=0;i<sizeof(buffer)-1;i++) printf("{%c} ", buffer[i]);
    //NL;NL;

    for(i=0;i<sizeof(buffer);i++)
    {
        if( !( buffer[0] != '\n' && buffer[0] != '0' &&
               ( (buffer[i] > 47 && buffer[i] < 58 ) || buffer[i] == '\n' || buffer[i] == '\0') ) )
        {
            printf("Invalid input."); NL;NL;
            exitMsg();
            exit(1);
        }
    }
    
    sscanf(buffer, "%llu", &n);
    if (n>ULLONG_MAX-1)
    {
        printf("{n} is too big."); NL;NL;
        exitMsg();
        exit(1);
    }
    
    printf("=== USUAL ======= function output"); NL;
    printf("Factorial of {%llu} is: %llu", n, usualFactorial(n)); NL;NL;

    printf("=== RECURSIVE === function output"); NL;
    printf("Factorial of {%llu} is: %llu", n, recursiveFactorial(n)); NL;NL;

    exitMsg();
    return 0;
}