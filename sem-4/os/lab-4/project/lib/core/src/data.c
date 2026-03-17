#include "data.h"

#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>

float *read_data_file(const char *filename, int *out_size)
{
    // Idiom: open file with check
    FILE *file = fopen(filename, "r");
    if (file == NULL) {
        perror("Ошибка открытия файла");
        return NULL;
    }

    // Idiom: parse array size in file with check
    int size = 0;
    if (fscanf(file, "%d", &size) != 1 || size <= 0) {
        fprintf(stderr, "Ошибка: неверный формат размера в файле %s\n", filename);
        fclose(file);
        return NULL;
    }

    // Idiom: allocate memory with check
    float *array = malloc(size * sizeof(float));
    if (array == NULL) {
        perror("Ошибка выделения памяти");
        fclose(file);
        return NULL;
    }

    // Idiom: put numbers into array with check
    for (int i = 0; i < size; i++) {
        if (fscanf(file, "%f", &array[i]) != 1) {
            fprintf(stderr, "Ошибка: недостаточно чисел или неверный формат в %s\n", filename);
            free(array);
            fclose(file);
            return NULL;
        }
    }

    fclose(file);
    *out_size = size;
    return array;
}

// Maybe void* ?
float *insertion_sort(float *array, int size)
{
    //
}

FILE *write_data_file(const char *filename, float *array, int size)
{
    //
}

void *process_file(void *arg)
{
    char *filename = (char *)arg;
    int size = 0;
    float *array = read_data_file(filename, &size);
    insertion_sort(array, size);
    write_data_file(filename, array, size);
    return NULL;
}
