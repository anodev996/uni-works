/* Badulin Ilya, group 424-1.
First year, second semester.
Practice 6.

Variant: Unsorted linked list */

#ifndef HOMEWORK_3_H
#define HOMEWORK_3_H

#include <stdbool.h>

// |
// | practice 6 code
// V
typedef struct
{
    int **adj_matrix; // матрица смежности
    int num_vertices;
} Graph;

Graph *read_graph_from_file(const char *filename);

void free_graph(Graph *g);

void bipartition_graph(Graph *g, int *v1, int *v2, int *v1_size, int *v2_size);

void print_partition(const int *v1, int v1_size, const int *v2, int v2_size);

void print_graph_edges(Graph *g);

// |
// | utils.c
// v
static inline void fputc_nl(FILE* stream, size_t count)
{
	size_t i;
	for (i=0; i<count; ++i) fputc('\n', stream);
}

static inline void nl(size_t count)
{
	fputc_nl(stdout, count);
}

void initiate_locale(const char* locale_desired, bool print_on_success);

void print_and_exit(int exit_code);

void create_int_array_dynamic(int** array, size_t size);

void print_int_array(int* array, size_t size, size_t columns);

int print_file(const char* filename, const size_t line_number);

void input_one_size_t(size_t* input);

int f_get_str_strict(FILE* stream, char* buffer, size_t buffer_size);

#endif