#include <stdio.h>
#include <stdlib.h>
#include <string.h>


// this function creates dynamic matrix of integers m x n size
int** createIntMatrix(int m, int n) {
    int** matrix;
	int i;
	
	matrix = (int**)malloc(m * sizeof(int*));
	if (matrix == NULL) return NULL;
	for (i=0;i<m;i++) {
	    matrix[i] = (int*)malloc(n * sizeof(int));
	    if (matrix[i] == NULL) return NULL;
	}
    return matrix;
}


//this function prints out a dynamic matrix
void printDynamicMatrix(int **matrix, int m, int n) {
	int i, j;
	for (i=0;i<m;i++) {
		//printf("%2d) ",i+1);
        printf("|");
		for (j=0;j<n;j++) {
			printf("%5d |",matrix[i][j]);
		}
		printf("\n");
	}
}


//frees the heap from a matrix
void freeMatrix(int** matrix, int m) {
    int i;
    for ( i = 0; i < m; i++) {
        free(matrix[i]);
    }
    free(matrix);
}


/*
HOMEWORK 1 FUNCTIONS
                |
                V
*/

// reads a text file with a symbolic matrix, converts symbolic numbs to integers, puts them into a matrix 
int fileToMatrix(char* name, int** matrix) {
    FILE* f;
    f = fopen(name, "r");
    if (!f) return 1;
    
    char buffer[128];
    
    int m=0,n;
    while (fgets(buffer, sizeof(buffer), f) != NULL){
        
        
        char* token = strtok(buffer, " \t\n");
        
        n=0;
        while (token != NULL) {
            matrix[m][n] = atoi(token);
            //printf("%5d", matrix[m][n]);
            n++;
            //printf("%s\n", token);
            token = strtok(NULL, " \t\n");
        }
        //printf("\n");
        
        m++;
    }
    
    fclose(f);
    return 0;
}





int** createMinor(int n, int** matrix, int row, int col) {
    int** minor = createIntMatrix(n-1, n-1);
    int r = 0, c = 0;
    int i;
    for (i = 0; i < n; i++) {
        if (i == row) continue;
        c = 0;
        int j;
        for (j = 0; j < n; j++) {
            if (j == col) continue;
            minor[r][c] = matrix[i][j];
            c++;
        }
        r++;
    }
    return minor;
}



int findDeterminant (int n, int** matrix) {
    int det = 0;
    if (n == 1) {
        det = matrix[0][0];
        return det;
    }
    if (n == 2) {
        det = matrix[0][0] * matrix[1][1] - matrix[0][1] * matrix[1][0];
        return det;
    }

    int i;
    for (i=0;i<n;i++) {
        int** minor = createMinor(n, matrix, 0, i);
        if ( i % 2 == 0) {
            det = det + matrix[0][i] * findDeterminant(n-1, minor);
        }
        else {
            det = det - matrix[0][i] * findDeterminant(n-1, minor);
        }
        freeMatrix(minor, n-1);
    }
    return det;
}
    



int findMatrixRank(int n, int** matrix) {
    int k;
    for (k = n; k > 0; k--) {  
        int i;
        for (i = 0; i <= n - k; i++) {
            int j;
            for (j = 0; j <= n - k; j++) {
               
                int** subMatrix = createIntMatrix(k, k);
                for (int r = 0; r < k; r++) {
                    for (int c = 0; c < k; c++) {
                        subMatrix[r][c] = matrix[i + r][j + c];
                    }
                }

                
                if (findDeterminant(k, subMatrix) != 0) {
                    freeMatrix(subMatrix, k);
                    return k;
                }

                freeMatrix(subMatrix, k);
            }
        }
    }
    return 0;
}



int main(void) {
    printf("\n");
    printf("424-1 | Badulin Ilya | Homework 1");
    printf("\n\n");

    int m = 4, n = 4;
    int** matrix = createIntMatrix(m, n);
	fileToMatrix("tests.txt", matrix);
	printDynamicMatrix(matrix, m, n);

    //printf("determinant: %d\n", findDeterminant(n, matrix));

    printf("rank: %d", findMatrixRank(n, matrix) );
    printf("\n");

    //printf("%5d\n", matrix[0][0]);
    freeMatrix(matrix, m);
    //printf("%5d\n", matrix[0][0]);

	system("PAUSE");
	return 0;
}