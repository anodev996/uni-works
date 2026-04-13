#!/usr/bin/env python3
import task

def main():
    print("--- Аппроксимация функций (Четный вариант) ---")
    
    # Ввод данных с клавиатуры
    try:
        a = float(input("Введите границу a: "))
        b = float(input("Введите границу b: "))
        n = int(input("Введите количество отрезков n: "))
        x_target = float(input("Введите точку интерполяции x: "))
    except ValueError:
        print("Ошибка ввода. Пожалуйста, вводите числа.")
        return

    # Получение узлов и значений функции
    x_vals, y_vals = task.generate_data(a, b, n)
    
    print("\nТаблица значений функции:")
    for i in range(len(x_vals)):
        print(f"x[{i}] = {x_vals[i]:.4f} \t y[{i}] = {y_vals[i]:.4f}")

    # Точное значение
    exact_val = task.f(x_target)
    print(f"\nТочное значение функции f({x_target}) = {exact_val:.6f}")

    # 1. Приближение полиномом Лагранжа
    lagrange_val = task.lagrange_polynomial(x_vals, y_vals, x_target)
    err_lagrange = task.absolute_error(exact_val, lagrange_val)
    print("-" * 45)
    print(f"Значение (Полином Лагранжа): {lagrange_val:.6f}")
    print(f"Абсолютная погрешность:      {err_lagrange:.6e}")

    # 2. Приближение кубическим сплайном
    spline_val = task.cubic_spline(x_vals, y_vals, x_target)
    err_spline = task.absolute_error(exact_val, spline_val)
    print("-" * 45)
    print(f"Значение (Кубический сплайн): {spline_val:.6f}")
    print(f"Абсолютная погрешность:       {err_spline:.6e}")

if __name__ == "__main__":
    main()
