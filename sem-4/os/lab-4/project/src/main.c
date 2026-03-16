#include <pthread.h>
#include <stdlib.h>

// #include "core.h"
#include "data.h"

int main(int argc, char **argv)
{
    int num_files = argc - 1;
    int thread_idx;
    pthread_t *thread_ids = malloc(sizeof(pthread_t) * num_files);

    // Можно и &process_file (это как с &arr[0] )
    for (thread_idx = 0; thread_idx < num_files; thread_idx++) {
        pthread_create(&thread_ids[thread_idx], NULL, process_file, argv[thread_idx + 1]);
    }

    for (thread_idx = 0; thread_idx < num_files; thread_idx++) {
        pthread_join(thread_ids[thread_idx], NULL);
    }

    free(thread_ids);
    return 0;
}
