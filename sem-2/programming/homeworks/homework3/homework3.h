/* Badulin Ilya, group 424-1.
First year, second semester.
Homework 3.

Variant: Unsorted linked list*/

#ifndef HOMEWORK_3_H
#define HOMEWORK_3_H

#include <stdbool.h>

// |
// | homework 3 code
// V
typedef struct PriorityQueue
{
	int priority;
	int data;
	struct PriorityQueue* next;
}PriorityQueue;

void pq_enqueue(PriorityQueue** head, int priority, int data);

void pq_create(const char* filename, PriorityQueue** head);

int pq_dequeue_max(PriorityQueue** head);

bool pq_remove(PriorityQueue** head, int value);

PriorityQueue* pq_find(PriorityQueue* head, int value);

bool pq_update(PriorityQueue* head, int old_value, int new_value);

void pq_print(PriorityQueue* head);

void pq_destroy(PriorityQueue** head);

static inline void fputc_nl(FILE* stream, size_t count)
{
	size_t i;
	for (i=0; i<count; ++i) fputc('\n', stream);
}

static inline void nl(size_t count)
{
	fputc_nl(stdout, count);
}

// |
// | utils.c
// v
void initiate_locale(const char* locale_desired, bool print_on_success);

void print_and_exit(int exit_code);

void create_int_array_dynamic(int** array, size_t size);

void print_int_array(int* array, size_t size, size_t columns);

int print_file(const char* filename, const size_t line_number);

void input_one_size_t(size_t* input);

int f_get_str_strict(FILE* stream, char* buffer, size_t buffer_size);

#endif