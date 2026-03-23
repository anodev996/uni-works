#pragma once

#include <stdbool.h>
#include <unistd.h>

#define BUFFER_SIZE 512
#define GEN_MIN_VALUE -99.999
#define GEN_MAX_VALUE 99.999
#define GEN_ARR_SIZE 10

void *process_file(void *arg);

bool generate_file(const char *file_fullname, const unsigned int arr_size, const float min_value,
                   const float max_value);
