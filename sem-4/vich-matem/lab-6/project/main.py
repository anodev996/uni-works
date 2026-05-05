#!/usr/bin/env python3
import numpy as np
import matplotlib.pyplot as plt
import pandas as pd
import math
from core import adams_4_solver


def f1(x, y):
    """Правая часть дифференциального уравнения для Примера 1"""
    return x * y**3 - y

def f2(x, y):
    """Правая часть дифференциального уравнения для Примера 2"""
    return 4.3 * x + 2 * y + np.exp(x * y)

def solve_and_display(f, x0, y0, x_end, n, title):
    """Вспомогательная функция для решения, вывода таблицы и графика"""
    print(f"--- {title} ---")
    x, y = adams_4_solver(f, x0, y0, x_end, n)
    
    df = pd.DataFrame({'x': x, 'y': y})
    print(df.to_string(index=False))
    print("\n")
    
    plt.figure(figsize=(8, 5))
    plt.plot(x, y, marker='', linestyle='-', color='g', label='Метод Адамса 4')
    plt.title(title)
    plt.xlabel('x')
    plt.ylabel('y')
    plt.grid(True)
    plt.legend()
    plt.show()

if __name__ == "__main__":
    n_steps = 10
    
    solve_and_display(f1, x0=0.0, y0=1.0, x_end=1.0, n=n_steps, title="Пример 1: y' = x*y^3 - y")
    
    solve_and_display(f2, x0=0.0, y0=0.0, x_end=0.5, n=n_steps, title="Пример 2: y' = 4.3x + 2y + e^{xy}")
