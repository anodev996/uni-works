#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define GREETMESSAGE printf("practice 2 | group 424-1 | variant 1 | Badulin Ilya")
#define NL printf("\n")

#define FILENAME "prac2_input_eng.txt"

typedef struct {
    char routeId[6];
    char destination[31];
    char date[11];
    char availableSeats[4];
} routeInfo;

routeInfo* loadRoutes(const char* filename, int* count)
{
	printf("trying to open %s....", filename);
	NL;
	
	FILE* f = fopen(filename, "r");
	if (f == NULL)
	{
		printf("file open error.");
		NL;
		exitMsg();
		exit(1);
	}

	int lines = 0;
	char buffer [64];
	while (fgets(buffer, sizeof(buffer), f))
	{
		lines++;
	}

	routeInfo* routes = (routeInfo*)malloc(sizeof(routeInfo) * lines);
	if (routes == NULL)
	{
		printf("memory allocation error.");
		NL;
		exitMsg();
		exit(2);
	}

	fseek(f, 0, SEEK_SET);
	int i = 0;
	while (fgets(buffer, sizeof(buffer), f))
	{
		buffer[strcspn(buffer, "\n")] = '\0';
		sscanf(buffer, "%5s %30s %10s %3s",
		routes[i].routeId,
		routes[i].destination,
		routes[i].date,
		routes[i].availableSeats);

		i++;
	}

	fclose(f);

	*count = lines;
	return routes;
}

void printRoutes(routeInfo* routes, int count)
{
	int i=0;
	while (i < count)
	{
		printf("%2d) %s %s %s %s", 
		i+1,
		routes[i].routeId,
		routes[i].destination,
		routes[i].date,
		routes[i].availableSeats);
		
		NL;
		i++;
	}
}

void seekRoutes(routeInfo* routes, int count, const char* userDestination, const char* userDate)
{
	int i, flag=0;
	for (i=0; i<count; i++)
	{
		if (strcmp(routes[i].destination, userDestination) == 0 && strcmp(routes[i].date, userDate) == 0)
		{
			printf("Route found!\nrouteId|date      |availableSeats|destination\n%-7s|%-10s|%-14s|%-30s", 
			routes[i].routeId,
			routes[i].date,
			routes[i].availableSeats,
			routes[i].destination);

			NL;NL;
			flag = 1;
		}
	}
	if (flag==0)
	{
		printf("No such routes...");
		NL;
	}
}

void exitMsg(void)
{
	printf("press 'ENTER' to exit..");
	getchar();
}

int main(void) 
{
	NL;
	GREETMESSAGE;
	NL;
	
	char buffer[64];
	char destination[31];
	char date[11];

	NL;
	printf("Enter destination and date: ");
	fgets(buffer, sizeof(buffer), stdin);
	buffer[strcspn(buffer, "\n")] = '\0';
	sscanf(buffer, "%30s %10s", destination, date);
	
    NL;
	int count = 0;
    routeInfo* routes = loadRoutes(FILENAME, &count);
	
	NL;
	printRoutes(routes, count);
	
	NL;
	printf("destination: %s || date: %s", destination, date);
	NL;NL;
	seekRoutes(routes, count, destination, date);

    free(routes);
    
	NL;
	exitMsg();
    return 0;
}
