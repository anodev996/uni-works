"""Doc."""

from typing import Callable, List, Tuple
import numpy as np
from scipy.optimize import minimize_scalar


def target_function(x: np.ndarray) -> float:
    """
    Функция Розенброка: f(x) = (1 - x_1)^2 + 100 * (x_2 - x_1^2)^2
    Глобальный минимум достигается в точке [1.0, 1.0], где f(x) = 0.

    Тестовая целевая функция: f(x) = (x_1 - 2)^2 + (x_2 - 1)^2
    Глобальный минимум достигается в точке [2.0, 1.0], где f(x) = 0.
    """
    # return float((x[0] - 2.0) ** 2 + (x[1] - 1.0) ** 2)
    return float((1.0 - x[0]) ** 2 + 100.0 * (x[1] - x[0] ** 2) ** 2)


def powell_minimize(
    func: Callable[[np.ndarray], float],
    x0: np.ndarray,
    tol: float = 1e-5,
    max_iter: int = 100,
) -> Tuple[np.ndarray, float, List[np.ndarray]]:
    """
    Реализация метода сопряженных направлений Пауэлла в строгом
    соответствии с алгоритмом N+1 одномерных поисков.
    """
    n: int = len(x0)
    x: np.ndarray = np.array(x0, dtype=float)

    # Матрица направлений, изначально состоит из единичных ортов (s^1 ... s^N)
    directions: np.ndarray = np.eye(n)
    history: List[np.ndarray] = [x.copy()]

    for _ in range(max_iter):
        p_curr: np.ndarray = x.copy()

        # Шаг 2 (начало): Поиск вдоль s^(N) (направление используется как при первом, так и последнем поиске)
        d_n: np.ndarray = directions[-1]
        res = minimize_scalar(lambda lam, p=p_curr, d=d_n: func(p + lam * d))
        p_1: np.ndarray = p_curr + float(res.x) * d_n

        p_curr = p_1.copy()

        # Шаг 2 (продолжение): Последовательный поиск по всем N направлениям
        for i in range(n):
            d: np.ndarray = directions[i]
            res = minimize_scalar(lambda lam, p=p_curr, d=d: func(p + lam * d))
            p_curr = p_curr + float(res.x) * d

        # Шаг 3: Определение нового сопряженного направления
        # Строится между точкой после N+1 поисков и точкой после первого поиска
        new_dir: np.ndarray = p_curr - p_1

        # Защита от вырождения направления
        if np.linalg.norm(new_dir) > 1e-8:
            new_dir = new_dir / np.linalg.norm(new_dir)  # Нормировка

            # Шаг 4: Обновление базиса (сдвигаем старые, заменяем s^(N) сопряженным направлением)
            directions = np.roll(directions, -1, axis=0)
            directions[-1] = new_dir

        history.append(p_curr.copy())

        # Проверка условия остановки по расстоянию между точками
        if np.linalg.norm(p_curr - x) < tol:
            return p_curr, func(p_curr), history

        x = p_curr

    return x, func(x), history
