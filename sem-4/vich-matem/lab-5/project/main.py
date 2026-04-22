#!/usr/bin/env python3
import core

def print_file_content(filename):
    """Выводит сырое содержимое файла на экран"""
    print(f"\n=== Содержимое файла '{filename}' ===")
    with open(filename, 'r') as f:
        print(f.read().strip())
    print("======================================\n")

def load_slau(filename):
    """Считывает матрицу A и вектор b из файла"""
    A = []
    b = []
    with open(filename, 'r') as f:
        lines = f.readlines()
        # Считываем 5 строк матрицы A
        for i in range(5):
            A.append([float(val) for val in lines[i].split()])
        # Считываем 6-ю строку для вектора b
        b = [float(val) for val in lines[5].split()]
    return A, b

def mat_vec_mult(A, x):
    """Умножение матрицы на вектор (для проверки)"""
    n = len(A)
    res = [0.0] * n
    for i in range(n):
        res[i] = sum(A[i][j] * x[j] for j in range(n))
    return res

def format_list(lst, decimals=3):
    """Округление элементов списка для красивого вывода"""
    return [round(val, decimals) for val in lst]

def print_matrix(M, decimals=3):
    """Красивый вывод двумерного массива"""
    for row in M:
        print("  " + "  ".join(f"{val:8.{decimals}f}" for val in row))

if __name__ == "__main__":
    filename = "input.txt"
    try:
        # 1. Вывод содержимого файла ДО вычислений
        print_file_content(filename)
        
        # 2. Загрузка данных в переменные
        A, b = load_slau(filename)
        
        # 3. Ввод точности пользователем
        eps = float(input("Введите точность (eps), например 0.001: "))
        
        # 4. Решение задач лабораторной (используем копии матриц A и b)
        A_copy1 = [row[:] for row in A]
        x_g = core.gauss_method(A_copy1, b[:])
        
        x_i, it_i = core.iterative_solver(A, b, eps, 'simple')
        x_s, it_s = core.iterative_solver(A, b, eps, 'seidel')
        
        A_copy2 = [row[:] for row in A]
        inv_A = core.get_inverse(A_copy2)
        
        # 5. Вывод результатов
        print(f"\nМетод Гаусса: {format_list(x_g)}")
        print(f"Простая итерация ({it_i} шаг.): {format_list(x_i)}")
        print(f"Метод Зейделя ({it_s} шаг.): {format_list(x_s)}")
        print("Обратная матрица:")
        print_matrix(inv_A)
        
        # 6. Проверка решения: умножаем исходную матрицу A на полученный вектор x_g
        b_calc = mat_vec_mult(A, x_g)
        residual = max(abs(b_calc[i] - b[i]) for i in range(len(b)))
        print(f"\nМакс. погрешность проверки (A*x - b): {residual:.2e}")
        
    except FileNotFoundError:
        print(f"Ошибка: Файл '{filename}' не найден. Убедитесь, что он лежит в той же папке.")
    except Exception as e:
        print(f"Произошла ошибка: {e}")
        