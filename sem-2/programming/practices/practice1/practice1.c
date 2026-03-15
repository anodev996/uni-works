// practice1 | 424-1 | variant 1 | Badulin Ilya

#include <stdio.h>
#include <stdlib.h>
#include <time.h>


int createBinaryFileWithNumbs(int n, int m, char* name){
	
	FILE* f = fopen(name, "wb");
	if (f == NULL) return 1;
	
	
	printf("file [%s]", name);
	printf("\n");
	
	int i, z;
	for(i=0;i<n;i++) {
		z = -9 + rand() % 19;
		fwrite(&z,sizeof(int),1,f);
		
		if (i>=m && i%m == 0) printf("\n");
		printf("%4d",z);
	}
	printf("\n");
	
	fclose(f);
	return 0;
}


int allInOne(int n, int m, char* name1, char* name2, char* name3) {
	
	createBinaryFileWithNumbs(n, m, name1);
	createBinaryFileWithNumbs(n, m, name2);
	
	FILE *f1 = fopen(name1,"rb");
	if (f1==NULL) {
		return 1;
	}
	FILE *f2 = fopen(name2,"rb");
	if (f2==NULL) {
		return 1;
	}
	FILE *f3 = fopen(name3, "wb");
		if (f3==NULL) {
		return 1;
	}
	
	printf("\n");
	printf("file [%s]", name3);
	printf("\n");

	int i = 0, a, b, max;
	while( fread(&a, sizeof(int), 1, f1) == 1 && fread(&b, sizeof(int), 1, f2) == 1 ) {
		
		if (a >= b) max = a;
		else max = b;

		fwrite(&max, sizeof(int), 1, f3);
		
		if (i>=m && i%m == 0) printf("\n");
		printf("%4d",max);
		i++;
	}
	printf("\n");
	
	fclose(f1);
	fclose(f2);
	fclose(f3);

	if ( remove(name1) != 0 ) return 2;
	if ( remove(name2) != 0 ) return 2;
	
	return 0;
}





int main(void) {
	printf("\n");
	printf("practice1 | 424-1 | variant 1 | Badulin Ilya");
	printf("\n");
	printf("\n");
	
	char fName1[30] = "FILE1";
	char fName2[30] = "FILE2";
	char fName3[30] = "result.bin";
	
	int check, n,m;
	

	printf("enter '1' to enter file name");
	printf("\n");
	printf("enter '2' for default file name: '%s'", fName3);
	printf("\n");
	
	while(1) {
		
		if (scanf("%d",&check) != 1) {
			//while(getchar() != "\n"[0]); // "\n" == char* ; "\n" == {'\n', '\0'}
			while(getchar() != '\n');
			//check = 0; // no need. If the input is wrong scanf() won't put it in the variable.
			continue;
		}

		if (check == 1) {
			printf("enter result file name: ");
			scanf("%s",fName3);
			break;
		}
		else if (check == 2) {
			break;
		}
		else {
			while(getchar() != "\n"[0]); // "\n" == char* ; "\n" == {'\n', '\0'}
			// while(getchar() != '\n');
			//check = 0; //same
		}
	}

	
	printf("enter quantity of random numbs: ");
	scanf("%d",&n);
	printf("enter number of columns to print: ");
	scanf("%d",&m);
	
	srand(time(NULL));
	allInOne(n,m, fName1, fName2, fName3);
	
	system("PAUSE");
	return 0;
}
