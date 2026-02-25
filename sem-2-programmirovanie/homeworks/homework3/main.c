/* Badulin Ilya, group 424-1.
First year, second semester.
Homework 3.

Variant: Unsorted linked list */

#include <stdlib.h>
#include <stdio.h>
#include "homework3.h"
    //debug
    //#include <string.h>

#define GREET_FILE_PATH "./main.c"

int main(void)
{
/*#ifdef _WIN32
    system("chcp 65001 > nul");
#endif*/

    print_file(GREET_FILE_PATH, 5); nl(2);

    PriorityQueue* head = NULL;

    while (1)
    {
        fprintf(stdout,
            "Enter:\n"
            "'1' to create  a new linked list from a file,\n"
            "'2' to add     a new element to current list,\n"
            "'3' to delete  an element with max value,\n"
            "'4' to delete  an element with specific value,\n"
            "'5' to find    an element with specific value,\n"
            "'6' to change  an element's value.\n\n"
            "To quit the program enter any other symbol.\n\n"
            "Your input: ");

        char input[8];
        fgets(input, sizeof(input), stdin);

        switch (input[0])
        {
            case '1':
            {
                char filename[64];
                fprintf(stdout, "Enter filename: ");
                fgets(filename, sizeof(filename), stdin);
                sscanf(filename, "%s", filename);

                pq_destroy(&head);
                pq_create(filename, &head); nl(1);
                pq_print(head); nl(2);
                break;
            }

            case '2':
            {
                int value;
                fprintf(stdout, "Enter new value: ");
                scanf("%d", &value); getchar();
                pq_enqueue(&head, value, value);
                pq_print(head); nl(2);
                break;
            }

            case '3':
            {
                int removed = pq_dequeue_max(&head);
                if (removed != -1)
                    fprintf(stdout, "Removed max: %d\n", removed);
                pq_print(head); nl(2);
                break;
            }

            case '4':
            {
                int val;
                fprintf(stdout, "Enter value to delete: ");
                scanf("%d", &val); getchar();
                if (pq_remove(&head, val))
                    fprintf(stdout, "Element removed.\n");
                else
                    fprintf(stdout, "Element not found.\n");
                pq_print(head); nl(2);
                break;
            }

            case '5':
            {
                int val;
                fprintf(stdout, "Enter value to find: ");
                scanf("%d", &val); getchar();
                PriorityQueue* found = pq_find(head, val);
                if (found)
                    fprintf(stdout, "Found element with value: %d\n", found->data);
                else
                    fprintf(stdout, "Element not found.\n");
                nl(2);
                break;
            }

            case '6':
            {
                int old_val, new_val;
                fprintf(stdout, "Enter value to change: ");
                scanf("%d", &old_val);
                fprintf(stdout, "Enter new value: ");
                scanf("%d", &new_val); getchar();
                if (pq_update(head, old_val, new_val))
                    fprintf(stdout, "Element updated.\n");
                else
                    fprintf(stdout, "Element not found.\n");
                pq_print(head); nl(2);
                break;
            }

            default:
                pq_destroy(&head);
                return 0;
        }
    }

    return 0;
}