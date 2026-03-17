#!/usr/bin/env python3
"""Vichislitelnaya matematika.  Laboratory work #3, variant #2."""
# import task_1_2 as t1
import task_2 as t2

T2_A = 0
T2_B = 1.57
N = 100
EPS = 0.00001
MAX_ITER = 100


def main() -> None:
    """Doc."""

    # for k in range(11):
    # print(f'k = {k:2}  y" = {t1.result(k):.10f}')
    result: float = t2.calc_definite_integral(T2_A, T2_B, N, EPS, MAX_ITER)
    print(f"Area: {result:.10f}")


if __name__ == "__main__":
    main()
