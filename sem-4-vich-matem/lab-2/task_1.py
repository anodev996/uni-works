import math


def f(x: float) -> float:
    return 4 * x + math.e ** x


def f_prime(x: float) -> float:
    return 4 + math.e ** x


def f_double_prime(x: float) -> float:
    return math.e ** x


def initial_guess(a: float, b: float) -> float:
    if f(a) * f_double_prime(a) > 0:
        return a
    return b


def delta_xn(x0: float, x1: float) -> float:
    # Проверка на деление на ноль
    if abs(x1 - x0) < 1e-15:  # если разница очень мала
        return f_prime(x1)  # используем производную как предел
    return (f(x1) - f(x0)) / (x1 - x0)


def iteration_formula(x0: float, x1: float) -> float:
    # Проверка знаменателя перед делением
    delta: float = delta_xn(x0, x1)
    if abs(delta) < 1e-15:  # если дельта близка к нулю
        return x1 - f(x1) / f_prime(x1)  # метод Ньютона как запасной вариант
    return x1 - (f(x1) / delta)


def secant_method(a: float, b: float, eps: float,
                  iteration_max: int) -> tuple[bool, float, int]:
    x0: float = initial_guess(a, b)
    x1: float = initial_guess(b, a)
    x2: float = x1
    success: bool = True
    root: float = 0.0

    for iteration in range(1, iteration_max + 1):
        x2 = iteration_formula(x0, x1)
        
        if abs(x2 - x1) < eps or abs(f(x2)) < eps:
            root = x2
            return (success, root, iteration)

        x0, x1 = x1, x2

    success = False
    root = x2
    return (success, root, iteration)