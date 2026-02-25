#include <stdio.h>
#include <stdlib.h>
#include <string.h>

int fileToMatrix(char* name, int** matrix) {
    FILE* f;
    f = fopen(name, "r");
    if (!f) return 1;
    
    char buffer[256];
        // Заполнение массива символами 'z'
    for (int i = 0; i < sizeof(buffer); i++) {
        buffer[i] = '+';
    }

    // Добавление завершающего нуля для корректной работы со строками
    buffer[sizeof(buffer) - 1] = '\0';
    printf("%s", buffer);
    printf("\n");
    
    
    int i,j;
    while (fgets(buffer, sizeof(buffer), f) != NULL){
        i = 0;
        while (i<20) { // Пока не встретим нулевой символ
            printf("buffer[%2d] = (%c)", i, buffer[i]);
            printf("\n");
            i++;
        }
    
        char* piece = strtok(buffer, " \t\n");
        printf("%s", piece);
        printf("\n");
        for (i=0;i<3;i++) {
             piece = strtok(NULL, " \t\n");
             printf("%s", piece);
             printf("\n");
        }
        //printf("%s", buffer);
    }
    
    fclose(f);
    return 0;
}


int main(void) {
	int** matrix;
	int m, n, i, j;
	m = 4;
	n = 4;
	matrix = (int**)malloc(m * sizeof(int*));
	if (matrix == NULL) return 1;
	for (i=0;i<n;i++) {
	    matrix[i] = (int*)malloc(n * sizeof(int));
	    if (matrix[i] == NULL) return 1;
	}
	
	fileToMatrix("home1Test.txt", matrix);
	return 0;
}