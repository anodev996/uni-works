"""Doc."""

import math
from typing import Callable, List, Tuple


def target_function(x: List[float]) -> float:
    """
    Функция Розенброка: f(x) = (1 - x_1)^2 + 100 * (x_2 - x_1^2)^2
    Глобальный минимум достигается в точке [1.0, 1.0], где f(x) = 0.
    """
    # return float((1.0 - x[0]) ** 2 + 100.0 * (x[1] - x[0] ** 2) ** 2)
    return ((x[0] - 7) ** 2) + ((x[1] + 2) ** 2) - 13


def golden_section_search(
    func: Callable[[float], float], a: float, b: float, tol: float = 1e-5
) -> float:
    """
    Метод золотого сечения для одномерной оптимизации.
    """
    gr = (math.sqrt(5) - 1) / 2  # золотое сечение ≈ 0.618

    c = b - gr * (b - a)
    d = a + gr * (b - a)

    for _ in range(100):  # Ограничиваем количество итераций
        if abs(c - d) < tol:
            break

        fc = func(c)
        fd = func(d)

        if fc < fd:
            b = d
        else:
            a = c

        c = b - gr * (b - a)
        d = a + gr * (b - a)

    return (a + b) / 2


def minimize_scalar(
    func: Callable[[List[float]], float],
    x: List[float],
    direction: List[float],
    tol: float = 1e-5,
) -> float:
    """
    Одномерная минимизация вдоль заданного направления.
    """

    def line_func(alpha: float) -> float:
        point = [x[i] + alpha * direction[i] for i in range(len(x))]
        return func(point)

    # Находим границы для alpha
    alpha = 0.0
    step = 0.5  # Уменьшаем начальный шаг
    max_step = 10.0  # Максимальный шаг
    min_step = -10.0  # Минимальный шаг

    f0 = line_func(alpha)

    # Ищем в положительном направлении
    alpha_pos = alpha + step
    iterations = 0
    max_iterations = 50

    try:
        f_pos = line_func(alpha_pos)

        if f_pos < f0:
            # Расширяем интервал в положительную сторону
            while (
                f_pos < line_func(alpha_pos + step)
                and alpha_pos < max_step
                and iterations < max_iterations
            ):
                step *= 1.5
                alpha_pos += step
                iterations += 1
            b = min(alpha_pos + step, max_step)
            a = max(alpha - step, min_step)
        else:
            # Ищем в отрицательном направлении
            alpha_neg = alpha - step
            f_neg = line_func(alpha_neg)
            if f_neg < f0:
                while (
                    f_neg < line_func(alpha_neg - step)
                    and alpha_neg > min_step
                    and iterations < max_iterations
                ):
                    step *= 1.5
                    alpha_neg -= step
                    iterations += 1
                a = max(alpha_neg - step, min_step)
                b = min(alpha + step, max_step)
            else:
                # Минимум рядом с 0, используем небольшой интервал
                a = -1.0
                b = 1.0
    except (OverflowError, ValueError):
        # Если переполнение, используем маленький интервал
        a = -1.0
        b = 1.0

    # Защита от вырожденного интервала
    if abs(b - a) < 1e-10:
        return 0.0

    try:
        result = golden_section_search(line_func, a, b, tol)
        return result
    except (OverflowError, ValueError):
        return 0.0


def powell_minimize(
    func: Callable[[List[float]], float],
    x0: List[float],
    tol: float = 1e-5,
    max_iter: int = 100,
) -> Tuple[List[float], float, List[List[float]]]:
    """
    Реализация метода сопряженных направлений Пауэлла в строгом
    соответствии с алгоритмом N+1 одномерных поисков.
    """
    n: int = len(x0)
    x: List[float] = [float(xi) for xi in x0]

    # Матрица направлений, изначально состоит из единичных ортов
    directions: List[List[float]] = [
        [1.0 if i == j else 0.0 for j in range(n)] for i in range(n)
    ]
    history: List[List[float]] = [[xi for xi in x]]

    for iteration in range(max_iter):
        p_curr: List[float] = [xi for xi in x]

        # Шаг 2 (начало): Поиск вдоль s^(N) (последнее направление)
        d_n: List[float] = directions[-1]
        alpha = minimize_scalar(func, p_curr, d_n, tol)
        p_1: List[float] = [p_curr[i] + alpha * d_n[i] for i in range(n)]

        p_curr = [xi for xi in p_1]

        # Шаг 2 (продолжение): Последовательный поиск по всем N направлениям
        for i in range(n):
            d: List[float] = directions[i]
            alpha = minimize_scalar(func, p_curr, d, tol)
            p_curr = [p_curr[j] + alpha * d[j] for j in range(n)]

        # Шаг 3: Определение нового сопряженного направления
        new_dir: List[float] = [p_curr[i] - p_1[i] for i in range(n)]

        # Защита от вырождения направления
        norm = math.sqrt(sum(di**2 for di in new_dir))
        if norm > 1e-8:
            new_dir = [di / norm for di in new_dir]

            # Шаг 4: Обновление базиса
            directions.pop(0)
            directions.append(new_dir)

        history.append([xi for xi in p_curr])

        # Проверка условия остановки по расстоянию между точками
        dist = math.sqrt(sum((p_curr[i] - x[i]) ** 2 for i in range(n)))

        # Дополнительная проверка по значению функции
        f_val = func(p_curr)

        if dist < tol or (iteration > 0 and abs(f_val - func(x)) < tol):
            return p_curr, f_val, history

        x = [xi for xi in p_curr]

    return x, func(x), history
