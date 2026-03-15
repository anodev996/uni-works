#include <stdio.h>
#include <stdlib.h>
//#include <time.h>

#define GREETMESSAGE printf("laboratory class 1 | group 424-1 | variant 1 | Badulin Ilya")
#define NL printf("\n")
//#define ERASELINEABOVE printf("\033[F\033[K")

#define FILENAME "input.txt"

typedef struct Node {
    int data;
    struct Node* next;
} Node;

Node* createNode(int data)
{
    Node* newNode = (Node*)malloc(sizeof(Node));
    if (newNode == NULL)
    {
        printf("function createNode: memory allocation error");
        NL;
        exit(1);
    }

    newNode->data = data;
    newNode->next = NULL;
    return newNode;
}

void appendNode(Node** head, int data) 
{
	Node* newNode = createNode(data);
	
	if (*head == NULL) 
    {
		*head = newNode;
		return;
	}
	
    Node* temp = *head;
    while (temp->next != NULL)
    {
        temp = temp->next;
    }
    //меняем связь ТОЛЬКО у текущего последнего элемента
    // (связываем с новым элементом)
    temp->next = newNode;
}

// using (const) to prevent modifying "input.txt". Because modifying such string will invoke an error.
void readFileCreateList(Node** head, const char* filename) 
{
    FILE* fInput = fopen(filename, "r");
    if (fInput == NULL)
    {
        NL;
        perror("ERROR");
        NL;
        printf("press 'ENTER' to exit");
        getchar();
        exit(1);
    }

    int num;
    while(fscanf(fInput, "%d", &num) == 1)
    {
        appendNode(head, num);
    }

    fclose(fInput);
}

void removeBetween(Node** head, int E) 
{
    if (*head == NULL) return;

    Node* first = NULL;
	Node* last = NULL;
    Node* temp = *head;
    
    while (temp != NULL) 
    {
        if ( temp->data == E) 
        {
            if (first == NULL) 
            {
                first = temp; 
            }

            last = temp; 
        }

        temp = temp->next;
    }
    
    if (first == NULL || first == last || first->next == last) return;
    
    temp = first->next;
    while (temp != last) 
    {
        Node* toDelete = temp;
        temp = temp->next;
        free(toDelete); 
    }
    
    first->next = last;
    return;
}

void printList(Node* head) 
{
    Node* temp = head;

    printf("(Head) %d --> ", temp->data);
    temp = temp->next;
    while (temp != NULL)
    {
        printf("%d --> ", temp->data);

        temp = temp->next;
    }
    printf("NULL");
}

void freeList(Node** head) 
{
    Node* temp;

    while (*head != NULL) 
    {
        temp = *head;
        *head = (*head)->next;
        free(temp);
    }
}

void waitUntilInput() 
{
    printf("press 'ENTER' to exit..");

    while (getchar() != '\n');
    
    getchar();
}

int getInt(char* message)
{
    int num;
    while (1)
    {
        printf("%s", message);

        if (scanf("%d", &num) != 1) 
        {
            if (feof(stdin)) 
            {
                NL;
                printf("EOF detected. Exiting...");
                NL;
                exit(1);
            }
        
            while(getchar() != '\n');

            NL;
            continue;
        }

        else return num;
    }
}

int main(void) 
{
	NL;
	GREETMESSAGE;
	NL;
	
    Node* head = NULL;

    NL;
    printf("trying to open '%s'...", FILENAME);
    NL;
    readFileCreateList(&head, FILENAME);

    NL;
	printf("original list:");
	NL;
    printList(head);
    NL;

    NL;
    int E = getInt("Enter E: ");

    removeBetween(&head, E);
    
    NL;
	printf("redacted list:");
	NL;
    printList(head);
    NL;
    
    freeList(&head);

    NL;
    waitUntilInput();
    return 0;
}