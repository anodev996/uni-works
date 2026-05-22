#!/usr/bin/env python3
"""Doc."""

from typing import List
import numpy as np
from core import target_function, powell_minimize


def main() -> None:
    """Основная функция программы, обрабатывающая ввод пользователя и вывод результатов."""

    print("=== Метод сопряженных направлений Пауэлла ===")
    print("Целевая функция: Функция Розенброка")
    print("Известный минимум: x* = [1.0, 1.0]\n")
    # print("Целевая функция: f(x) = (x_1 - 2)^2 + (x_2 - 1)^2")
    # print("Известный минимум: x* = [2.0, 1.0]\n")

    try:
        x1_input: str = input("Введите начальную точку x1: ")
        x2_input: str = input("Введите начальную точку x2: ")
        x0: np.ndarray = np.array([float(x1_input), float(x2_input)])

        tol_input: str = input("Введите точность (нажмите Enter для 1e-5): ")
        tol: float = float(tol_input) if tol_input.strip() else 1e-5

    except ValueError:
        print("\n[Ошибка] Пожалуйста, вводите только числа.")
        return

    opt_x: np.ndarray
    opt_f: float
    history: List[np.ndarray]

    opt_x, opt_f, history = powell_minimize(
        func=target_function, x0=x0, tol=tol
    )

    print("\n=== РЕЗУЛЬТАТЫ ===")
    print(f"Найдена точка минимума: [{opt_x[0]:.6f}, {opt_x[1]:.6f}]")
    print(f"Значение функции f(x): {opt_f:.8f}")
    print(f"Количество итераций: {len(history) - 1}")


if __name__ == "__main__":
    main()
