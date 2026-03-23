#include "data.h"

#include <stdio.h>
#include <stdlib.h>
#include <string.h>

static float *read_data_file(const char *filename, int *out_size)
{
    // Idiom: open file with check
    FILE *file = fopen(filename, "r");
    if (file == NULL) {
        perror("Ошибка открытия файла");
        return NULL;
    }

    // Idiom: parse array size in file with check
    int arr_size = 0;
    if (fscanf(file, "%d", &arr_size) != 1 || arr_size <= 0) {
        fprintf(stderr, "Ошибка: неверный формат размера в файле %s\n", filename);
        fclose(file);
        return NULL;
    }

    // Idiom: allocate memory with check
    float *array = malloc(arr_size * sizeof(float));
    if (array == NULL) {
        perror("Ошибка выделения памяти");
        fclose(file);
        return NULL;
    }

    // Idiom: put numbers into array with check
    for (int i = 0; i < arr_size; i++) {
        if (fscanf(file, "%f", &array[i]) != 1) {
            fprintf(stderr, "Ошибка: недостаточно чисел или неверный формат в %s\n", filename);
            free(array);
            fclose(file);
            return NULL;
        }
    }

    fclose(file);
    *out_size = arr_size;
    return array;
}

static void insertion_sort(float *array, int size)
{
    for (int i = 1; i < size; i++) {
        float key = array[i];
        int j = i - 1;

        while (j >= 0 && array[j] > key) {
            array[j + 1] = array[j];
            j = j - 1;
        }

        array[j + 1] = key;
    }
}

static bool write_data_file(const char *filename, float *array, int arr_size)
{
    FILE *file = fopen(filename, "w");
    if (file == NULL) {
        perror("Ошибка открытия файла");
        return false;
    }

    if (fprintf(file, "%d\n", arr_size) < 0) {
        perror("Ошибка записи размера массива");
        fclose(file);
        return false;
    }

    for (int i = 0; i < arr_size; i++) {
        if (fprintf(file, "%.3f ", array[i]) < 0) {
            perror("Ошибка записи элемента массива");
            fclose(file);
            return false;
        }
    }

    if (fclose(file) != 0) {
        perror("Ошибка сохранения файла");
        return false;
    }

    return true;
}

/*
static bool print_file(const char *filename)
{
    FILE *file = fopen(filename, "r");
    if (file == NULL) {
        perror("Ошибка открытия файла");
        return false;
    }

    char buffer[BUFFER_SIZE];
    while (fgets(buffer, BUFFER_SIZE, file) != NULL) {
        printf("%s", buffer);
    }

    fseek(file, -1, SEEK_END);
    if (fgetc(file) != '\n') {
        printf("\n");
    }

    fclose(file);
    return true;
}
*/

void *process_file(void *arg)
{
    char *filename = (char *)arg;
    int arr_size = 0;

    float *array = read_data_file(filename, &arr_size);
    if (array == NULL) {
        printf("Пропуск файла %s из-за ошибки чтения.\n", filename);
        return NULL;
    }

    printf("begin processing: %s\n", filename);
    insertion_sort(array, arr_size);

    char out_filename[BUFFER_SIZE];
    snprintf(out_filename, sizeof(out_filename), "%s_sorted", filename);

    write_data_file(out_filename, array, arr_size);

    free(array);
    printf("done  processing: %s -> %s\n", filename, out_filename);
    return NULL;
}

static float generate_random_float(const float min_value, const float max_value)
{
    float scale = rand() / (float)RAND_MAX;
    return min_value + scale * (max_value - min_value);
}

static void randomize_array(float *array, const unsigned int arr_size, const float min_value,
                            const float max_value)
{
    for (unsigned int i = 0; i < arr_size; i++) {
        array[i] = generate_random_float(min_value, max_value);
    }
}

bool generate_file(const char *file_fullname, const unsigned int arr_size, const float min_value,
                   const float max_value)
{
    float array[arr_size];
    randomize_array(array, arr_size, min_value, max_value);

    if (!write_data_file(file_fullname, array, arr_size)) {
        return false;
    }

    printf("generated new file: %s\n", file_fullname);
    return true;
}
