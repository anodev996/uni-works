#ifndef LAB8_H
#define LAB8_H

#include <stdbool.h>

// |
// | core.c
// V


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