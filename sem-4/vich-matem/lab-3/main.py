#!/usr/bin/env python3
"""Vichislitelnaya matematika.  Laboratory work #3, variant #2."""
import task_1_2 as t1


def main() -> None:
    """Doc."""
    try:
        tolerance = float(input("Введите требуемую точность (например, 0.0001): "))
        max_iterations = int(input("Введите максимальное количество итераций: "))
    except ValueError:
        print("Ошибка: Пожалуйста, введите корректные числовые значения.")
        return

    print("\nРезультаты вычислений:")
    print(f"{'x':<5} | {'f\'\'(x) (Численно)':<20} | {'Итерации':<10}")
    print("-" * 40)

    # Вычисление в заданных точках: x = 1.2 + 0.1 * k, где k = 0, 1, ..., 10
    for k in range(11):
        x = 1.2 + 0.1 * k
        try:
            derivative_val, iters = calc_second_derivative(x, tolerance, max_iterations)
            # Аналитическая производная для f(x) = x^3 + 2x^2 - 44 равна f''(x) = 6x + 4
            # (вы можете использовать её для самопроверки, если захотите)
            print(f"{x:.1f}   | {derivative_val:<20.6f} | {iters:<10}")
        except ValueError:
            print(f"{x:.1f}   | {'-':<20} | {max_iterations:<10}")

if __name__ == "__main__":
    main()
