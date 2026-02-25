#ifndef LAB5_H
#define LAB5_H

#define NL fputc('\n', stdout)
#define NL2 fputs("\n\n", stdout)

void exit_messsage(int exit_code);

int* create_int_array_dynamic(unsigned int size);

void print_array(int* array, unsigned int size, unsigned int columns);

void print_file(const char* filename);

void input_one_natural(unsigned int* number_natural);


void merge_two_array(int* array_whole, int* array_left, unsigned int size_left, int* array_right, unsigned int size_right);

void merge_sort(int* array_whole, unsigned int size_whole);

void complexity_merge_two_array(int* array_whole, int* array_left, unsigned int size_left, int* array_right, unsigned int size_right, unsigned int* compares, unsigned int* assignments);

void complexity_merge_sort(int* array_whole, unsigned int size_whole, unsigned int* compares, unsigned int* assignments);

void complexity(char* filename);

#endif