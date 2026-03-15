/*Badulin Ilya, group 424-1.
First year, second semester.
Laboratory class 7.*/
#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include <errno.h>
#include "lab7.h"

void bst_insert(Node** root, int key)
{
	if (*root == NULL)
	{
		*root = (Node*)malloc(sizeof(Node));
		if (*root == NULL)
		{
			fprintf(stderr, "ERROR bst_insert(): %s", strerror(errno)); NL2;
			print_and_exit(EXIT_FAILURE);
		}

		(*root)->data = key;
		(*root)->left = NULL;
		(*root)->right =NULL;

		return;
	}

	Node* current = *root;
	while (current != NULL)
	{
		if (key >= current->data)
		{
			if (current->right == NULL)
			{
				current->right = (Node*)malloc(sizeof(Node));
				if (current->right == NULL)
				{
					fprintf(stderr, "ERROR bst_insert(): %s", strerror(errno)); NL2;
					print_and_exit(EXIT_FAILURE);
				}

				current->right->data = key;
				current->right->left = NULL;
				current->right->right =NULL;

				return;
			}
			
			else
			{
				current = current->right;
			}
		}

		else
		{
			if (current->left == NULL)
			{
				current->left = (Node*)malloc(sizeof(Node));
				if (current->left == NULL)
				{
					fprintf(stderr, "ERROR bst_insert(): %s", strerror(errno)); NL2;
					print_and_exit(EXIT_FAILURE);
				}

				current->left->data = key;
				current->left->left = NULL;
				current->left->right =NULL;

				return;
			}

			else
			{
				current = current->left;
			}
		}
	}
}

void bst_print(Node* root)
{
	if (root == NULL) return;

	bst_print(root->left);
	fprintf(stdout, "%3d ", root->data);
	bst_print(root->right);

	/*prefix (direct) traversal - first the current node is processed, then the left and right subtrees;

	fprintf(stdout, "%3d ", root->data);
	bst_print(root->left);
	bst_print(root->right);

	infix (symmetric) traversal - first the left subtree of the current node is processed, then the root, then the right subtree;

	bst_print(root->left);
	fprintf(stdout, "%3d ", root->data);
	bst_print(root->right);

	postfix (reverse) traversal - first the left and right subtrees of the current node are processed, then the node itself.

	bst_print(root->left);
	bst_print(root->right);
	fprintf(stdout, "%3d ", root->data);*/
}

void bst_free(Node* root)
{
	if (root == NULL) return;

	bst_free(root->left);
	bst_free(root->right);
	free(root);
}