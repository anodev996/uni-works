/*Badulin Ilya, group 424-1.
First year, second semester.
Practice class 5.*/
#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include <errno.h>
#include "prac5.h"

int file_count_words(const char* filename, int max_word_len)
{
    FILE* fp = fopen(filename, "r");
    if(fp == NULL)
    {
        fprintf(stderr, "ERROR file_count_words(): %s", strerror(errno)); NL2;
        exit_messsage(1);
    }

	char* buffer = (char*)malloc(max_word_len * sizeof(char) + 2);
	if(buffer == NULL)
    {
        fprintf(stderr, "ERROR file_count_words(): %s", strerror(errno)); NL2;
        exit_messsage(1);
    }
	buffer[max_word_len+1] = '\0';
	buffer[max_word_len] = '\0';

	int count = 0;

	char format[32];
	snprintf(format, sizeof(format), "%%%ds", max_word_len+1);

	while(fscanf(fp, format, buffer) != EOF)
	{
		if(buffer[max_word_len] != '\0') return -1;

		else if(strlen(buffer) > 1 && buffer[0] == buffer[strlen(buffer)-1]) count++;
	}

    fclose(fp);
	free(buffer);

	return count;
}