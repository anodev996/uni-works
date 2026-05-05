#!/usr/bin/env python3
# task 2.2.3(a)

MATRIX_NUM = 3


def print_list_matrix(matrix_num, list_n, list_matrix):

    for matrix_index in range(matrix_num):

        print(f"\nmatrix #{matrix_index + 1}:")

        for line in range(list_n[matrix_index]):

            list_f_str = [
                f"{elem:6.2f}" for elem in list_matrix[matrix_index][line]
            ]
            print(" ".join(list_f_str))
            # print(list_f_str)


"""
list_n = []
list_m = []
list_matrix = []
for i in range(MATRIX_NUM):

    n, m = map(int, input(f"enter n(lines) m(colunmns) of matrix #{i + 1}: ").split())
    matrix = [list( map( float, input(f"[{line}]: ").split() ) ) for line in range(n)]
    list_n.append(n)
    list_m.append(m)
    list_matrix.append(matrix)
"""
list_n = [2, 3, 4]
list_m = [2, 2, 3]
list_matrix = [
    [[1, 2], [3, 4]],
    [[0, 2], [3, -5], [-3, 0]],
    [[1, 2, 3], [1, 2, 3], [1, 2, 3], [1, 2, 3.162]],
]


print("===BEFORE===")
print_list_matrix(MATRIX_NUM, list_n, list_matrix)


for matrix_index in range(MATRIX_NUM):

    n = list_n[matrix_index]
    m = list_m[matrix_index]
    square_of_last_elem = list_matrix[matrix_index][n - 1][m - 1] ** 2
    if square_of_last_elem == 0:
        print(
            f"\nmatrix #{matrix_index + 1}: Last number of the array can't be 0!\n"
            + "           Skipping that matrix...\n"
        )
        continue

    for line in range(n):
        for elem in range(m):
            list_matrix[matrix_index][line][elem] /= square_of_last_elem


print("===AFTER====")
print_list_matrix(MATRIX_NUM, list_n, list_matrix)
