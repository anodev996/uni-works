#!/usr/bin/env python3
"""Docstring."""
import math

# __all__ = [""]


def _f(x: float) -> float:
    return math.sin(x)


def calc_definite_integral(
    a: float, b: float, n: int, eps: float, max_iter: int
) -> float:
    "Doc."
    h: float = (b - a) / n
    x: float = a
    i: float = 0.0

    for _ in range(1, n + 1):
        i += h * _f(x)
        x += h

    return i
