/* Badulin Ilya, group 424-1.
First year, second semester.
Practice 6.

Variant: Unsorted linked list */

#include <stdlib.h>
#include <stdio.h>
#include <errno.h>
#include <string.h>
#include <limits.h>

#include <locale.h>
#include <stdbool.h>

#include "practice6.h"

// |
// | practice 6 code
// V
Graph *read_graph_from_file(const char *filename)
{
    FILE *f = fopen(filename, "r");
    if (!f)
    {
        perror("Failed to open file");
        return NULL;
    }

    int n;
    fscanf(f, "%d", &n);

    Graph *g = malloc(sizeof(Graph));
    g->num_vertices = n;
    g->adj_matrix = malloc(n * sizeof(int *));
    for (int i = 0; i < n; i++)
        g->adj_matrix[i] = malloc(n * sizeof(int));

    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < n; j++)
        {
            fscanf(f, "%d", &g->adj_matrix[i][j]);
        }
    }

    fclose(f);
    return g;
}

void free_graph(Graph *g)
{
    for (int i = 0; i < g->num_vertices; i++)
        free(g->adj_matrix[i]);
    free(g->adj_matrix);
    free(g);
}

void bipartition_graph(Graph *g, int *v1, int *v2, int *v1_size, int *v2_size)
{
    int visited[g->num_vertices];
    int level[g->num_vertices];
    for (int i = 0; i < g->num_vertices; i++)
    {
        visited[i] = 0;
        level[i] = -1;
    }

    int queue[g->num_vertices];
    int front = 0, rear = 0;

    // Начинаем с вершины 0
    int start = 0;
    queue[rear++] = start;
    visited[start] = 1;
    level[start] = 0;

    while (front < rear)
    {
        int curr = queue[front++];
        for (int i = 0; i < g->num_vertices; i++)
        {
            if (g->adj_matrix[curr][i] && !visited[i])
            {
                visited[i] = 1;
                level[i] = level[curr] + 1;
                queue[rear++] = i;
            }
        }
    }

    // Заполнение V1 и V2 по уровням
    *v1_size = 0;
    *v2_size = 0;
    for (int i = 0; i < g->num_vertices; i++)
    {
        if (level[i] % 2 == 0)
            v1[(*v1_size)++] = i;
        else
            v2[(*v2_size)++] = i;
    }
}

void print_partition(const int *v1, int v1_size, const int *v2, int v2_size)
{
    printf("V1:\033[1;31m ");
    for (int i = 0; i < v1_size; i++)
        printf("%d ", v1[i]+1);
    printf("\033[1;0m\n");

    printf("V2:\033[1;33m ");
    for (int i = 0; i < v2_size; i++)
        printf("%d ", v2[i]+1);
    printf("\033[1;0m\n");
}

void print_graph_edges(Graph *g)
{
    for (int i = 0; i < g->num_vertices; i++)
    {
        for (int j = i + 1; j < g->num_vertices; j++)
        {
            if (g->adj_matrix[i][j])
                printf("%d %d\n", i, j);
        }
    }
}

// |
// | utils.c
// v
void initiate_locale(const char* locale_desired, bool print_on_success)
{
    const char* locale_current;
    const char* msg_fmt = "Current locale: '%s'";

    locale_current = setlocale(LC_ALL, locale_desired);
    if (locale_current)
    {
        if (print_on_success)
        {
            fprintf(stdout, msg_fmt, locale_current);
        }
        return;
    }

    fprintf(stderr,
            "Warning: locale '%s' is not available. "
            "Falling back to environment locale.\n\n",
            locale_desired);

    locale_current = setlocale(LC_ALL, "");
    if (locale_current)
    {
        fprintf(stdout, msg_fmt, locale_current);
    }
    else
    {
        fprintf(stderr,
                "Error: failed to set any locale. "
                "Unicode may not work properly.");
    }
}

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
        fprintf(stderr,
                "ERROR create_int_array_dynamic(): %s",
                strerror(errno));
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
            if (i % columns == 0) fputc('\n', stdout);
            fprintf(stdout, "%3d ", array[i]);
        }
    }
}

int print_file(const char* filename, const size_t line_number)
{
    FILE* fp = fopen(filename, "r");
    if(fp == NULL)
    {
        fprintf(stderr, "ERROR print_file(): %s", strerror(errno));
        return EXIT_FAILURE;
    }

    fprintf(stdout, "%s\n", filename);
                                  
    size_t i=0;
    char buffer[512];
    while(fgets(buffer, sizeof(buffer), fp) != NULL)
    {
        fprintf(stdout, "%2zu | %s", ++i, buffer);

        if (i == line_number) break;
    }

    if (i != line_number) fprintf(stdout, "\n\t< file text end >");
    else fprintf(stdout, "\t< printed first %zu lines >", line_number);

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
        if( !( buffer[0] != '\n'
               && ( (buffer[i] > 47 && buffer[i] < 58 ) 
                    || buffer[i] == '\n' 
                    || buffer[i] == '\0') ) )
        {
            fprintf(stderr, "Invalid input.\n\n");
            print_and_exit(EXIT_FAILURE);
        }
    }
    
    sscanf(buffer, "%zu", input);
    if (*input > UINT_MAX-1)
    {
        fprintf(stderr, "The entered number is too big.\n\n");
        print_and_exit(EXIT_FAILURE);
    }
}

//size of the buffer have to be bigger by 2 then maximum expected string length
int f_get_str_strict(FILE* stream, char* buffer, size_t buffer_size)
{
    if (buffer_size < 3)
    {
        fprintf(stdout, "f_get_str_strict(): buffer < 3\n\n");
        print_and_exit(EXIT_FAILURE);
    }

    //assign '\0' to last and penultimate elements of the buffer 
    buffer[buffer_size-1] = '\0';
    buffer[buffer_size-2] = '\0';

    //prepare %<buffer_size - 1>s for fscanf()
    char format[32];
    snprintf(format, sizeof(format), "%%%ds", (int)(buffer_size-1));

    int check = fscanf(stream, format, buffer);

    //if penultimate element of buffer is overwritten, consider it as overflow
    if(buffer[buffer_size-2] != '\0')
    {
        fprintf(stdout, "f_get_str_strict(): string buffer overflow\n\n");
        print_and_exit(EXIT_FAILURE);
    }

    return check;
}