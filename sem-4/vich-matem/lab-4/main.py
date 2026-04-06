#!/usr/bin/env python3
"""Vichislitelnaya matematika.  Laboratory work #4"""
from task import *


def main() -> None:
    """Doc."""
    try:
        a = float(input("Введите начало отрезка a: "))
        b = float(input("Введите конец отрезка b: "))
        n = int(input("Введите количество разбиений n: "))
        if n <= 0:
            print("Ошибка: Количество разбиений должно быть больше 0.")
            return
        x_star = float(input("Введите точку интерполяции x_star: "))
    except ValueError:
        print("Ошибка ввода. Пожалуйста, вводите только числа.")
        return

    # Получение массивов
    x_vals, y_vals = generate_arrays(a, b, n, target_function)

    print(f"\nУзлы интерполяции (x): {[round(x, 4) for x in x_vals]}")
    print(f"Значения функции (y):  {[round(y, 4) for y in y_vals]}\n")

    # Точное значение
    exact_val = target_function(x_star)

    # 1. Формула Лагранжа
    lagrange_val = lagrange_interpolation(x_vals, y_vals, x_star)
    lagrange_err = abs(exact_val - lagrange_val)

    # 2. Кубический сплайн
    spline_val = cubic_spline_interpolation(x_vals, y_vals, x_star)
    spline_err = abs(exact_val - spline_val)

    # Вывод результатов
    print(f"Точное значение f({x_star}) = {exact_val:.8f}")
    print("-" * 60)
    print(f"Полином Лагранжа:  {lagrange_val:.8f}")
    print(f"Абсолютная погрешность (Лагранж): {lagrange_err:.8e}")
    print("-" * 60)
    print(f"Кубический сплайн: {spline_val:.8f}")
    print(f"Абсолютная погрешность (Сплайн):  {spline_err:.8e}")


if __name__ == "__main__":
    main()
