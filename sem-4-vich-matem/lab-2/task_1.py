#!/usr/bin/env python3
# coding:utf-8
import math
from config import EPS_DIVISION

__all__ = ["secant_method"]


def _f(x: float) -> float:
    return 4 * x + math.e ** x


def _f_prime(x: float) -> float:
    return 4 + math.e ** x


def _f_double_prime(x: float) -> float:
    return math.e ** x


def _initial_guess(a: float, b: float) -> float:
    if _f(a) * _f_double_prime(a) > 0:
        return a
    return b


def _delta_xn(x0: float, x1: float) -> float:
    # Проверка на деление на ноль
    if abs(x1 - x0) < EPS_DIVISION:  # если разница очень мала
        return _f_prime(x1)  # используем производную как предел
    return (_f(x1) - _f(x0)) / (x1 - x0)


def _iteration_formula(x0: float, x1: float) -> float:
    # Проверка знаменателя перед делением
    delta: float = _delta_xn(x0, x1)
    # Если дельта близка к нулю - метод Ньютона как запасной вариант
    if abs(delta) < EPS_DIVISION:
        return x1 - _f(x1) / _f_prime(x1)
    return x1 - (_f(x1) / delta)


def secant_method(a: float, b: float, eps: float,
                  iteration_max: int) -> tuple[bool, float, int]:
    x0: float = _initial_guess(a, b)
    x1: float = _initial_guess(b, a)
    x2: float = x1

    for iteration in range(1, iteration_max + 1):
        x2 = _iteration_formula(x0, x1)
        
        if abs(x2 - x1) < eps or abs(_f(x2)) < eps:
            return (True, x2, iteration)

        x0, x1 = x1, x2

    return (False, x2, iteration)