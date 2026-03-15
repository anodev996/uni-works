/*Badulin Ilya, group 424-1.
First year, second semester.
Practice class 5.*/
#ifndef PRAC5_H
#define PRAC5_H

#define NL fputc('\n', stdout)
#define NL2 fputs("\n\n", stdout)

void exit_messsage(int exit_code);

int* create_int_array_dynamic(unsigned int size);

void print_int_array(int* array, unsigned int size, unsigned int columns);

void print_file(const char* filename);

void input_one_unsigned_int(unsigned int* input);

int stream_get_string_strict(FILE* stream, char* buffer, size_t buffer_size);


int file_count_words(const char* filename, int max_word_len);

#endif