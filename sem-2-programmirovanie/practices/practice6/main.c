/* Badulin Ilya, group 424-1.
First year, second semester.
Practice 6.

Variant: Алгоритм разбиения двудольного графа на доли */

#include <stdlib.h>
#include <stdio.h>
#include "practice6.h"

#define GREET_FILE_PATH "./main.c"
#define MAX_VERTICES 1000

int main(void)
{
#ifdef _WIN32
    system("chcp 65001 > nul");
#endif

    print_file(GREET_FILE_PATH, 5); nl(2);

    char filename[256];
    printf("Enter input file name: ");
    scanf("%255s", filename);

    Graph *g = read_graph_from_file(filename);
    if (!g) return 1;

    int v1[MAX_VERTICES], v2[MAX_VERTICES];
    int v1_size, v2_size;

    bipartition_graph(g, v1, v2, &v1_size, &v2_size);

    printf("\nGraph adjacency matrix:\n");
    for (int i = 0; i < g->num_vertices; i++)
    {
        for (int j = 0; j < g->num_vertices; j++)
        {
            printf("%d ", g->adj_matrix[i][j]);
        }
        printf("\n");
    }

    printf("\nPartition result:\n");
    print_partition(v1, v1_size, v2, v2_size);

    free_graph(g);

    print_and_exit(EXIT_SUCCESS);
}