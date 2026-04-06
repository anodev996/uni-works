#!/usr/bin/env python3
"""Docstring."""
import math
from typing import List, Tuple, Callable

# __all__ = [""]


def target_function(x: float) -> float:
    """
    Исходная функция f(x). Выбираем f(x) = sin(x).
    """
    return math.sin(x)


def generate_arrays(
    a: float, b: float, n: int, func: Callable[[float], float]
) -> Tuple[List[float], List[float]]:
    """
    Генерация узлов интерполяции x и значений функции y.
    Логика полностью повторяет фрагмент из задания.
    """
    x_vals = [0.0] * (n + 1)
    y_vals = [0.0] * (n + 1)

    # Исправлено: добавлены индексы
    x_vals = a
    y_vals = func(x_vals)

    h = (b - a) / n

    for i in range(1, n + 1):
        x_vals[i] = x_vals[i - 1] + h
        y_vals[i] = func(x_vals[i])

    return x_vals, y_vals


def lagrange_interpolation(
    x_vals: List[float], y_vals: List[float], x_star: float
) -> float:
    """
    Вычисление значения функции в точке x_star с помощью полинома Лагранжа.
    """
    result = 0.0
    n = len(x_vals)

    for i in range(n):
        term = y_vals[i]
        for j in range(n):
            if i != j:
                term *= (x_star - x_vals[j]) / (x_vals[i] - x_vals[j])
        result += term

    return result


def solve_tridiagonal(
    A: List[float], B: List[float], C: List[float], F: List[float]
) -> List[float]:
    """
    Метод прогонки для решения СЛАУ с трехдиагональной матрицей.
    Необходим для поиска коэффициентов кубического сплайна.
    """
    n = len(F)
    c_prime = [0.0] * n
    d_prime = [0.0] * n

    # Исправлено: добавлены индексы
    c_prime = C / B
    d_prime = F / B

    for i in range(1, n):
        denom = B[i] - A[i] * c_prime[i - 1]
        if i < n - 1:
            c_prime[i] = C[i] / denom
        d_prime[i] = (F[i] - A[i] * d_prime[i - 1]) / denom

    x_res = [0.0] * n
    x_res[-1] = d_prime[-1]

    for i in range(n - 2, -1, -1):
        x_res[i] = d_prime[i] - c_prime[i] * x_res[i + 1]

    return x_res


def cubic_spline_interpolation(
    x_vals: List[float], y_vals: List[float], x_star: float
) -> float:
    """
    Построение естественного кубического сплайна и вычисление значения в точке x_star.
    """
    n = len(x_vals) - 1
    h = [x_vals[i + 1] - x_vals[i] for i in range(n)]

    # Инициализация векторов для метода прогонки (решаем систему Ac = F)
    A = [0.0] * (n + 1)
    B = [1.0] * (
        n + 1
    )  # Единицы на концах для естественного сплайна (c_0 = 0, c_n = 0)
    C = [0.0] * (n + 1)
    F = [0.0] * (n + 1)

    for i in range(1, n):
        A[i] = h[i - 1]
        B[i] = 2.0 * (h[i - 1] + h[i])
        C[i] = h[i]
        F[i] = 3.0 * (
            (y_vals[i + 1] - y_vals[i]) / h[i]
            - (y_vals[i] - y_vals[i - 1]) / h[i - 1]
        )

    # Находим коэффициенты 'c'
    c = solve_tridiagonal(A, B, C, F)

    # Находим остальные коэффициенты 'a', 'b', 'd'
    a = y_vals[:-1]
    b = [0.0] * n
    d = [0.0] * n

    for i in range(n):
        b[i] = (y_vals[i + 1] - y_vals[i]) / h[i] - h[i] * (
            c[i + 1] + 2.0 * c[i]
        ) / 3.0
        d[i] = (c[i + 1] - c[i]) / (3.0 * h[i])

    # Определяем, в какой интервал попадает искомая точка x_star
    idx = 0
    if x_star >= x_vals[-1]:
        idx = n - 1
    else:
        for i in range(n):
            if x_vals[i] <= x_star <= x_vals[i + 1]:
                idx = i
                break

    dx = x_star - x_vals[idx]

    # Вычисляем значение сплайна: S(x) = a + b*dx + c*dx^2 + d*dx^3
    return a[idx] + b[idx] * dx + c[idx] * (dx**2) + d[idx] * (dx**3)
