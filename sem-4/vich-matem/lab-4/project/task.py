#!/usr/bin/env python3
import math

def f(x):
    """
    Исходная функция f(x). Выбирается самостоятельно.
    Для примера возьмем синус.
    """
    return math.sin(x)

def generate_data(a, b, n):
    """
    Формирует исходные массивы x и y 
    через динамическое добавление элементов.
    """
    h = (b - a) / n
    
    # Инициализируем массивы сразу с нулевыми (первыми) элементами
    x = [a]
    y = [f(a)]
    
    # Цикл начинаем с 1 до n включительно
    for i in range(1, n + 1):
        # Вычисляем следующий x на основе предыдущего
        next_x = x[i-1] + h
        
        # Добавляем новые значения в конец списков
        x.append(next_x)
        y.append(f(next_x))
        
    return x, y

def lagrange_polynomial(x_vals, y_vals, x_target):
    """
    Вычисляет значение функции в точке интерполяции с помощью полинома Лагранжа.
    """
    n = len(x_vals)
    result = 0.0
    for i in range(n):
        term = y_vals[i]
        for j in range(n):
            if i != j:
                term *= (x_target - x_vals[j]) / (x_vals[i] - x_vals[j])
        result += term
    return result

def cubic_spline(x_vals, y_vals, x_target):
    """
    Проводит кубическую сплайн-интерполяцию.
    Используется метод прогонки для нахождения коэффициентов.
    """
    n = len(x_vals) - 1
    h = [0.0] + [x_vals[i] - x_vals[i-1] for i in range(1, n + 1)]
    
    # a_i = y_i
    a = y_vals.copy()
    
    # Инициализация массивов для метода прогонки
    alpha = [0.0] * (n + 1)
    beta = [0.0] * (n + 1)
    c = [0.0] * (n + 1)
    
    # Прямой ход метода прогонки
    for i in range(1, n):
        A = h[i]
        C = 2.0 * (h[i] + h[i+1])
        B = h[i+1]
        F = 6.0 * ((y_vals[i+1] - y_vals[i]) / h[i+1] - (y_vals[i] - y_vals[i-1]) / h[i])
        
        z = (A * alpha[i] + C)
        alpha[i+1] = -B / z
        beta[i+1] = (F - A * beta[i]) / z
        
    # Обратный ход метода прогонки
    for i in range(n - 1, 0, -1):
        c[i] = alpha[i+1] * c[i+1] + beta[i+1]
        
    # Вычисление коэффициентов b и d
    b = [0.0] * (n + 1)
    d = [0.0] * (n + 1)
    for i in range(1, n + 1):
        d[i] = (c[i] - c[i-1]) / h[i]
        b[i] = h[i] * (2.0 * c[i] + c[i-1]) / 6.0 + (y_vals[i] - y_vals[i-1]) / h[i]
        
    # Поиск интервала, в который попадает точка x_target
    for i in range(1, n + 1):
        if x_vals[i-1] <= x_target <= x_vals[i]:
            dx = x_target - x_vals[i]
            # Формула S_i(x)
            return a[i] + b[i]*dx + (c[i] / 2.0)*(dx**2) + (d[i] / 6.0)*(dx**3)
            
    # Если точка вышла за пределы (экстраполяция)
    if x_target < x_vals:
        dx = x_target - x_vals[1]
        return a[1] + b[1]*dx + (c[1] / 2.0)*(dx**2) + (d[1] / 6.0)*(dx**3)
    else:
        dx = x_target - x_vals[-1]
        return a[-1] + b[-1]*dx + (c[-1] / 2.0)*(dx**2) + (d[-1] / 6.0)*(dx**3)

def absolute_error(exact, approx):
    """
    Определяет абсолютную погрешность.
    """
    return abs(exact - approx)
