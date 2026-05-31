#!/usr/bin/env python3
"""Doc."""

from typing import List
from core import target_function, powell_minimize


def main() -> None:
    """Основная функция программы, обрабатывающая ввод пользователя и вывод результатов."""

    print("=== Метод сопряженных направлений Пауэлла ===")
    print("Целевая функция: Функция Розенброка")
    print("Известный минимум: x* = [1.0, 1.0]\n")

    try:
        x1_input: str = input("Введите начальную точку x1: ")
        x2_input: str = input("Введите начальную точку x2: ")
        x0: List[float] = [float(x1_input), float(x2_input)]

        tol_input: str = input("Введите точность (нажмите Enter для 1e-5): ")
        tol: float = float(tol_input) if tol_input.strip() else 1e-5

        max_iter_input: str = input(
            "Введите максимальное число итераций (нажмите Enter для 100): "
        )
        max_iter: int = int(max_iter_input) if max_iter_input.strip() else 100

    except ValueError:
        print("\n[Ошибка] Пожалуйста, вводите только числа.")
        return

    opt_x: List[float]
    opt_f: float
    history: List[List[float]]

    try:
        opt_x, opt_f, history = powell_minimize(
            func=target_function, x0=x0, tol=tol, max_iter=max_iter
        )
    except Exception as e:
        print(f"\n[Ошибка] Произошла ошибка при оптимизации: {e}")
        return

    print("\n=== РЕЗУЛЬТАТЫ ===")
    print(f"Найдена точка минимума: [{opt_x[0]:.6f}, {opt_x[1]:.6f}]")
    print(f"Значение функции f(x): {opt_f:.8f}")
    print(f"Количество итераций: {len(history) - 1}")

    if len(history) <= 20:
        print("\n=== ИСТОРИЯ ИТЕРАЦИЙ ===")
        for i, point in enumerate(history):
            print(
                f"Итерация {i}: [{point[0]:.6f}, {point[1]:.6f}], f = {target_function(point):.8f}"
            )


if __name__ == "__main__":
    main()
