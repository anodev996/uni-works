/*Badulin Ilya, group 424-1.
First year, second semester.
Laboratory class 8.*/

#include <stdlib.h>
#include <stdio.h>
#include "lab8.h"


/* Вставляем элемент как обычно, но с маленьким шансом поднимаем его 
вверх через ротации. Тем самым балансируя.

Дерево в среднем получается глубиной O(log n). */

int size(Node *h)
{
	if (h != NULL) return h->n;
	else return 0;
}

Node *create_node(int key)
{
    Node *node = malloc(sizeof(Node));

    node->key = key;
    node->n = 1;
    node->left = node->right = NULL;

    return node;
}

//нужно для балансоровки дерева при вставке в корень
void rotate_r(Node **h)
{
    Node *x = (*h)->left;
    int k = (*h)->n;

    (*h)->left = x->right;
    x->right = *h;
    *h = x;

    (*h)->n = k;
    (*h)->right->n = 1 + size((*h)->right->left) + size((*h)->right->right);
}

//нужно для балансоровки дерева при вставке в корень
void rotate_l(Node **h)
{
    Node *x = (*h)->right;
    int k = (*h)->n;

    (*h)->right = x->left;
    x->left = *h;
    *h = x;

    (*h)->n = k;
    (*h)->left->n = 1 + size((*h)->left->left) + size((*h)->left->right);
}

Node *insert_root(Node *h, int key)
{
    if (!h) return create_node(key);

    if (key < h->key)
    {
        h->left = insert_root(h->left, key);
        rotate_r(&h);
    }
    else
    {
        h->right = insert_root(h->right, key);
        rotate_l(&h);
    }

    return h;
}

//функция рандомно вызывает insert_root(), чем меньше поддеревьев у ноды, тем
//выше шанс, что её подминут в корень
Node *insert_random(Node *h, int key)
{
    if (!h) return create_node(key);
	//			 основной рандом, при n=1 шанс 50/50
    if (rand() % (h->n + 1) == 0) return insert_root(h, key);

    if (key < h->key) h->left = insert_random(h->left, key);
    else h->right = insert_random(h->right, key);

    h->n = 1 + size(h->left) + size(h->right);

    return h;
}

void partition(Node **R, int k)
{
    if (*R == NULL)
        return;

    int T = size((*R)->left);

    if (T > k)
    {
        partition(&(*R)->left, k);
        rotate_r(R);
    }
    else if (T < k)
    {
        partition(&(*R)->right, k - T - 1);
        rotate_l(R);
    }
    // если T == k, то нужный элемент уже в корне — ничего не делаем
}

//балансоровка
// 1 поднимаем средний элемент в корень
// 2 балансируем левое поддерево
// 3 балансируем правое поддерево
void balanced(Node **h)
{
    if (*h == NULL || (*h)->n == 1) return;

    partition(h, (*h)->n / 2); 
    balanced(&(*h)->left);     
    balanced(&(*h)->right);    
}

//верх - правая ветка, низ - левая ветка
//левая сторона - ближе к корню, правая - ближе к дну
void print_tree(Node *h, int depth)
{
    if (!h) return;

    print_tree(h->right, depth + 1);

    for (int i = 0; i < depth; i++)
        printf("   ");

    printf("%d (%d)\n", h->key, h->n);

    print_tree(h->left, depth + 1);
}

void free_tree(Node *h)
{
    if (!h) return;

    free_tree(h->left);
    free_tree(h->right);
    free(h);
}