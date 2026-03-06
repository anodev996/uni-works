#!/usr/bin/env python3
"""Docstring."""
from config import (
    GR_X1, GR_X2,
    MSG_WRONG_MODULE
)

__all__ = ["golden_section_search"]


def _f(x: float) -> float:
    return x * x - 9


def golden_section_search(a: float, b: float, eps: float,
                          iteration_max: int) -> tuple[bool, float, int]:
    """Docstring."""
    x1: float = a + (b - a) * GR_X1
    x2: float = a + (b - a) * GR_X2
    y1: float = _f(x1)
    y2: float = _f(x2)
    iteration: int = 0

    for iteration in range(1, iteration_max + 1):
        if y1 < y2:
            b, x2, y2 = x2, x1, y1
            x1 = a + (b - a) * GR_X1
            y1 = _f(x1)
        else:
            a, x1, y1 = x1, x2, y2
            x2 = a + (b - a) * GR_X2
            y2 = _f(x2)

        if abs(b - a) < eps:
            return (True, ((a + b) / 2), iteration)

    return (False, ((a + b) / 2), iteration)


if __name__ == "__main__":
    print(MSG_WRONG_MODULE)
