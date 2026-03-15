/*Badulin Ilya, group 424-1.
First year, second semester.
Laboratory class 8.*/

#ifndef LAB8_H
#define LAB8_H

#define NL fputc('\n', stdout)
#define NL2 fputs("\n\n", stdout)

void print_and_exit(int exit_code);

void create_int_array_dynamic(int** array, size_t size);

void print_int_array(int* array, size_t size, size_t columns);

void print_file(const char* filename);

void input_one_size_t(size_t* input);

int stream_get_string_strict(FILE* stream, char* buffer, size_t buffer_size);


typedef struct Node {
    int key;
    int n;
    struct Node *left, *right;
} Node;

Node *insert_random(Node *h, int key);

void print_tree(Node *h, int depth);

void free_tree(Node *h);

void balanced(Node **h);

#endif