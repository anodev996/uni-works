#!/usr/bin/env python3
"""Vichislitelnaya matematika.  Laboratoy work #2, variant #5."""
from config import (
    A, B, EPS,
    ITERATION_MAX,
    MSG_1, MSG_WARN, DELIM
)
import task_1 as t1
#import task_2 as t2


def main() -> None:
    """Run public functions from task_1 and task_2."""
    print(f"{DELIM}\n"
          + f"{MSG_1}")

    success, root, iteration = t1.secant_method(A, B, EPS, ITERATION_MAX)

    if success:
        print(f"{DELIM}\n"
                + f"root: {root:.10f}\n"
                + f"epsilon: {EPS:.10f}\n"
                + f"iterations done: {iteration}\n"
                + f"{DELIM}")
    else:
        print(f"{DELIM}\n"
            + f"{MSG_WARN}\n"
            + f"{DELIM}\n"
            + f"last approximation: {root:.10f}\n"
            + f"epsilon: {EPS:.10f}\n"
            + f"iterations done: {iteration}\n"
            + f"{DELIM}")


if __name__ == "__main__":
    main()
