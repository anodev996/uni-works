#!/usr/bin/env python3
"""Vichislitelnaya matematika.  Laboratory work #2, variant #5."""
from config import T1_A, T1_B, T2_A, T2_B, EPS, ITERATION_MAX, MSG_1, MSG_2
import io_handler as io
import task_1 as t1
import task_2 as t2


def main() -> None:
    """Execute and print public functions from task_1 and task_2."""
    io.out(MSG_1, *t1.secant_method(T1_A, T1_B, EPS, ITERATION_MAX))
    io.out(MSG_2, *t2.golden_section_search(T2_A, T2_B, EPS, ITERATION_MAX))


if __name__ == "__main__":
    main()
