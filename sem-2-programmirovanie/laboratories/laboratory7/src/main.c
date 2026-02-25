/*Badulin Ilya, group 424-1.
First year, second semester.
Laboratory class 7.*/
#include <stdlib.h>
#include <stdio.h>
#include <time.h>
#include "lab7.h"

#include <string.h>
#include <errno.h>

#define GREET_FILE_PATH "./README.md"

int main(void)
{
    print_file(GREET_FILE_PATH); NL2;

    //input
    size_t size;
    fprintf(stdout, "Enter number of BST elements: ");
    input_one_size_t(&size); NL;
    if (size == 0)
    {
        fprintf(stdout, "Invalid input.");
        print_and_exit(EXIT_FAILURE);
    }

    //create & fill array
    int* array;
    create_int_array_dynamic(&array, size);
    srand(time(NULL));
    size_t i;
    for (i=0;i<size;i++) array[i] = rand()%20+1;

    //print out array
    fprintf(stdout, "Array of random numbers to insert in BST:"); NL;
    print_int_array(array, size, 10); NL2;

    //insert in BST
    Node* root = NULL;
    for (i=0;i<size;i++) bst_insert(&root, array[i]);

    //print out BST
    fprintf(stdout, "BST elements:"); NL;
    bst_print(root); NL2;

    //free memory from array and BST
    free(array);
    bst_free(root);

    print_and_exit(EXIT_SUCCESS);
}