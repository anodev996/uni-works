#!/usr/bin/env python3
import math

def gauss_method(A, b):
    n = len(b)
    # Создаем расширенную матрицу M = [A | b]
    M = [A[i][:] + [b[i]] for i in range(n)]
    
    # Прямой ход
    for i in range(n):
        # Нормируем текущую строку
        pivot = M[i][i]
        for j in range(i, n + 1):
            M[i][j] /= pivot
            
        # Обнуляем элементы под главной диагональю
        for k in range(i + 1, n):
            factor = M[k][i]
            for j in range(i, n + 1):
                M[k][j] -= factor * M[i][j]
                
    # Обратный ход
    x = [0.0] * n
    for i in range(n - 1, -1, -1):
        s = sum(M[i][j] * x[j] for j in range(i + 1, n))
        x[i] = M[i][n] - s
        
    return x

def iterative_solver(A, b, eps, method='seidel', max_iter=1000):
    n = len(b)
    x = [0.0] * n  # Начальное приближение
    
    for it in range(1, max_iter + 1):
        x_new = x[:] # Копируем текущий вектор
        for i in range(n):
            if method == 'simple': # Метод простой итерации
                s = sum(A[i][j] * x[j] for j in range(n) if j != i)
                x_new[i] = (b[i] - s) / A[i][i]
            else: # Метод Зейделя
                s1 = sum(A[i][j] * x_new[j] for j in range(i))
                s2 = sum(A[i][j] * x[j] for j in range(i + 1, n))
                x_new[i] = (b[i] - s1 - s2) / A[i][i]
        
        # Проверка сходимости (максимальная разность по модулю)
        max_diff = max(abs(x_new[i] - x[i]) for i in range(n))
        if max_diff < eps:
            return x_new, it
        x = x_new[:]
        
    return x, max_iter

def get_inverse(A):
    n = len(A)
    # Создаем расширенную матрицу M = [A | E]
    M = []
    for i in range(n):
        row = A[i][:] + [1.0 if i == j else 0.0 for j in range(n)]
        M.append(row)
        
    # Прямой и обратный ход Жордана-Гаусса
    for i in range(n):
        pivot = M[i][i]
        for j in range(2 * n):
            M[i][j] /= pivot
            
        for k in range(n):
            if i != k:
                factor = M[k][i]
                for j in range(2 * n):
                    M[k][j] -= factor * M[i][j]
                    
    # Извлекаем правую половину матрицы (это и есть обратная)
    inv_A = [row[n:] for row in M]
    return inv_A
    