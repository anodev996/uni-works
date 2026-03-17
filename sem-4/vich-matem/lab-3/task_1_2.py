#!/usr/bin/env python3
"""Docstring."""

# __all__ = [""]


def _f(x: float) -> float:
    return x**3 + 2 * x * x - 44


def _f_double_prime(x: float) -> float:
    return 6 * x + 4


def result(k: int) -> float:
    "Doc."
    return _f_double_prime(1.2 + 0.1 * k)
