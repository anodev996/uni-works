/* Badulin Ilya, group 424-1.
First year, second semester.
Homework 4.

Variant 1: Boyer–Moore string-search algorithm. */

#include <stdlib.h>
#include <stdio.h>
#include <errno.h>
#include <string.h>
#include <limits.h>
#include <time.h>

//#include <wctype.h>
//#include <wchar.h>
//#include <locale.h>

// |
// | utility.h
// V
#define NL putc('\n', stdout) //In terminal print new line.
#define NL2 fputs("\n\n", stdout) //In terminal print new line x2.

void print_and_exit(int exit_code);

void create_int_array_dynamic(int** array, size_t size);

void print_int_array(int* array, size_t size, size_t columns);

int print_file(const char* filename);

void input_one_size_t(size_t* input);

int stream_get_string_strict(FILE* stream, char* buffer, size_t buffer_size);

// |
// | homework 4 code
// V
void boyer_moore_string_search(const char* filename, const char* substring)
{  
    //Open file for reading.

    FILE* fp = fopen(filename, "r");
    if(fp == NULL)
    {
        fprintf(stderr, "ERROR boyer_moore_string_search(): %s", strerror(errno)); NL2;
        print_and_exit(EXIT_FAILURE);
    }

    //Get substring length.

    size_t i, j, k;
    size_t substring_length = 0;

    for (i=0; i<sizeof(substring); i++)
    {
        if (substring[i] == '\0') break;
        substring_length++;
    }

            //debug
            //fprintf(stdout, "%zu", substring_length);

    //Make shift table from substring (alphabet is ASCII == 128 symbols)
    //by getting the shift size for each symbol in ASCII table.

    char shift_table[128];

    /* ОКАЗЫВАЕТСЯ, НАДО БЫЛО СНАЧАЛА ИЗ ПОДСТРОКИ СДЕЛАТЬ ШАБЛОН,
    А ЗАТЕМ УЖЕ РАБОТАТЬ С НИМ. ТО ЕСТЬ, ТАК ЖЕ, НАЙТИ ЕГО ДЛИНУ И Т.Д.
    
    ЭТО БЫ СИЛЬНО УПРОСТИЛО КОД И ДАЖЕ МОЖНО БЫЛО БЫ СДЕЛАТЬ ПОДДЕРЖКУ
    КИРИЛЛИЦЫ (НЕ ASCII АЛФАВИТ (128 СИМВОЛОВ), А УЖЕ BMP (65536 СИМВОЛОВ)) 

    for (i=0; i<sizeof(shift_table); i++) shift_table[i] = substring_length;

    for (i=0; i<substring_length-1; i++) shift_table[ (int)substring[i] ] = substring_length - i - 1;

    substring / substring_length - ДОЛЖНЫ БЫТЬ ДЛЯ ШАБЛОНА, 
    А НЕ ДЛЯ ВВЕДЁННОЙ ПОДСТРОКИ. */

    for (i=0; i<sizeof(shift_table); i++) 
    {
        shift_table[i] = substring_length; 
        for (j=substring_length; j!=0; j--) 
        {
            if (substring[j-1] == (char)i)
            {
                shift_table[i] = substring_length - j;
                break;
            }
        }

        //The shift size for the last symbol in substring
        //always have to be == substring_length.

        if (shift_table[i] == 0) shift_table[i] = substring_length;
    }

            //debug
            /*
            for (i=0; i<128; i++)
            {
                fprintf(stdout,
                        "symbol '%c' decimal %3d shift %d",
                        i,
                        i,
                        shift_table[i]); NL;
            }
            */

    //Read file line by line, search substrings in each line.

    size_t line_number = 0, string_length=0, flag_search_success = 0;
    char buffer[512];

    while(fgets(buffer, sizeof(buffer), fp) != NULL)
    {
        //Count current line number for output.

        line_number++;

        //In every cycle get string length of the line.

        for (string_length=0; string_length<sizeof(buffer); string_length++) if (buffer[string_length]=='\0') break;

        //If string length < substring length, there is nothing to search for...

        if (string_length < substring_length) continue; 

        //Boyer–Moore string-search algorithm. Uses the shift table.

        for (i=substring_length-1; i<string_length; )
        {
            for (j=substring_length, k=i; j!=0; j--, k--)
            {
                if (buffer[k] != substring[j-1])
                {
                    i += shift_table[ (int)buffer[k] ] - (substring_length - j);
                    break;
                }
            }

            if (j == 0)
            {
                fprintf(stdout,
                        "FOUND!\tLn: %2zu\tCol: %3zu",
                        line_number,
                        k+2); NL;

                i += substring_length;

                flag_search_success = 1;
            }
        }
    }

    //If didn't find any substrings, print out the message.

    if(!flag_search_success)
    {
        fprintf(stdout, "No such substring in the file :\\"); NL;
    }

    fclose(fp);
}

int main(void)
{
            //debug
            //fprintf(stdout, "sizeof(char) is %zu bytes", sizeof(char)); NL2;
            //fprintf(stdout, "sizeof(wchar_t) is %zu bytes", sizeof(wchar_t)); NL2;

    system("chcp 65001 > nul");

            //debug
            //#define DEFAULT_FILENAME "./input_short.txt"

    //Create buffer for input.

    char buffer[512];

    //Input filename.

    char filename[64];
    fprintf(stdout, "Enter filename: ");
    fgets(buffer, sizeof(buffer), stdin); NL;
    sscanf(buffer, "%63s", filename);

            //debug
            //strncpy(filename, DEFAULT_FILENAME, 64);
            //for (int i=0;i<64;i++) fprintf(stdout, "'%d' ", filename[i]);
            //NL2;

    //Print out file.

    int check = print_file(filename); NL2;
    if(check) print_and_exit(EXIT_FAILURE);
    
    //Input substring.

    char substring[512];
    fprintf(stdout, "Enter substring to search for (alphabet is 128 symbols of ASCII): ");
    fgets(buffer, sizeof(buffer), stdin); NL;
    sscanf(buffer, "%511s", substring);

            //debug
            //strncpy(substring, "arcu", 256);

    //Invoke homework 4 function.

    fprintf(stdout, "Ln - line\nCol - column / start position of the substring in the line"); NL;
    boyer_moore_string_search(filename, substring); NL;

    //Exit with 0 code.

    print_and_exit(EXIT_SUCCESS);
}

//____________________________________________________________________________
//____________________________________________________________________________
//____________________________________________________________________________

// |
// | utility.c
// V
void print_and_exit(int exit_code)
{
    fprintf(stdout, "Press ENTER to exit...");
    getchar();
    exit(exit_code);
}

void create_int_array_dynamic(int** array, size_t size)
{
    *array = (int*)malloc(size * sizeof(int));
    if (array == NULL)
    {
        fprintf(stderr, "ERROR create_int_array_dynamic(): %s", strerror(errno));
        print_and_exit(EXIT_FAILURE);
    }
}

void print_int_array(int* array, size_t size, size_t columns)
{
    size_t i;
    if (size <= columns)
    {
        for (i=0; i<size; ++i) fprintf(stdout, "%3d ", array[i]);
    }
    else if (size > columns)
    {
        for (i=0;i<columns;i++) fprintf(stdout, "%3d ", array[i]);

        for (;i<size;i++)
        {
            if (i % columns == 0) NL;
            fprintf(stdout, "%3d ", array[i]);
        }
    }
}

int print_file(const char* filename)
{
    FILE* fp = fopen(filename, "r");
    if(fp == NULL)
    {
        fprintf(stderr, "ERROR print_file(): %s", strerror(errno));
        return EXIT_FAILURE;
    }

    size_t i = 0;
    char buffer[512];

    fprintf(stdout, "\t< FILE TEXT BEGIN >"); NL;
    while(fgets(buffer, sizeof(buffer), fp) != NULL)
    {
        fprintf(stdout, "%2zu |", ++i);
        fputs(buffer, stdout);
    }
    NL;
    fprintf(stdout, "\t<  FILE TEXT END  >");

    fclose(fp);
    return EXIT_SUCCESS;
}

void input_one_size_t(size_t* input)
{
    char buffer[32] = {0};

    fgets(buffer, sizeof(buffer), stdin);

    size_t i;
    for(i=0;i<sizeof(buffer);i++)
    {
        if( !( buffer[0] != '\n' && ( (buffer[i] > 47 && buffer[i] < 58 ) || buffer[i] == '\n' || buffer[i] == '\0') ) )
        {
            fprintf(stderr, "Invalid input."); NL2;
            print_and_exit(EXIT_FAILURE);
        }
    }
    
    sscanf(buffer, "%zu", input);
    if (*input > UINT_MAX-1)
    {
        fprintf(stderr, "The entered number is too big."); NL2;
        print_and_exit(EXIT_FAILURE);
    }
}

//size of buffer have to be bigger by 2 then maximum string length
int stream_get_string_strict(FILE* stream, char* buffer, size_t buffer_size)
{
    if (buffer_size < 3)
    {
        fprintf(stdout, "strict_fscanf(): buffer < 3");
        return 0;
    }

    //assign '\0' to last and penultimate elements of buffer 
    buffer[buffer_size-1] = '\0';
    buffer[buffer_size-2] = '\0';

    //prepare %<buffer_size - 1>s for fscanf()
    char format[32];
    snprintf(format, sizeof(format), "%%%ds", (int)(buffer_size-1));

    int check = fscanf(stream, format, buffer);

    //if penultimate element of buffer is overwritten, consider it as overflow
    if(buffer[buffer_size-2] != '\0')
    {
        fprintf(stdout, "strict_fscanf(): string buffer overflow"); NL;
        return 0;
    }

    return check;
}