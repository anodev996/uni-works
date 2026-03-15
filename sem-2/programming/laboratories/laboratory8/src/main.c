/*Badulin Ilya, group 424-1.
First year, second semester.
Laboratory class 8.*/

#include <stdlib.h>
#include <stdio.h>
#include <time.h>
#include "lab8.h"

#define GREET_FILE_PATH "./README.md"

int main(void)
{
    print_file(GREET_FILE_PATH); NL2;

    int n;
    printf("Enter the number of tree elements: ");
    scanf("%d", &n);

    Node *root = NULL;
    srand(time(NULL));

    printf("Insertion order:"); NL;
    for (int i = 0; i < n; i++)
    {
        int key = (rand() % 20)+1;
        printf("%d ", key);
        root = insert_random(root, key);
    }
    NL;

    printf("Tree (keys with subtree sizes):"); NL;
    print_tree(root, 0); NL2;


    printf("Tree (after balancing):"); NL;
    balanced(&root);
    print_tree(root, 0);

    free_tree(root);

    print_and_exit(EXIT_SUCCESS); NL;
}