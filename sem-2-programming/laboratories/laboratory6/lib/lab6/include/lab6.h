// Badulin Ilya, group 424-1.
// Year I, second semester.
// Laboratory class 6.
// // // // // // // // // // // // // // // // // // // // // // // // // //
#ifndef LAB6_H
#define LAB6_H

#define NL fputc('\n', stdout)
#define NL2 fputs("\n\n", stdout)

void exit_messsage(int exit_code);

int* create_int_array_dynamic(unsigned int size);

void print_int_array(int* array, unsigned int size, unsigned int columns);

void print_file(const char* filename);

void input_one_unsigned_int(unsigned int* input);


void binary_quicksort(int* array, int arr_index_first, int arr_index_last, int digits_count);

void complexity(char* filename);

#endif