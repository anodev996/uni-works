#include <pthread.h>
#include <stdio.h>
#include <stdlib.h>
#include <time.h>

#include "data.h"

int main(int argc, char **argv)
{
    int num_files = argc - 1;
    srand(time(NULL));
    for (int i = 0; i < num_files; i++) {
        generate_file(argv[i + 1], GEN_ARR_SIZE, GEN_MIN_VALUE, GEN_MAX_VALUE);
    }

    int thread_idx = 0;
    pthread_t *thread_ids = malloc(num_files * sizeof(pthread_t));
    if (thread_ids == NULL) {
        perror("Ошибка выделения памяти");
        return 1;
    }

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
