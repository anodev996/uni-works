#!/usr/bin/env python3
"""Docstring."""
from config import (
    EPS,
    MSG_WARN, MSG_WRONG_MODULE, DELIM
)


def out(method: str, success: bool, root: float, iteration: int) -> None:
    """Docstring."""
    print(f"\n{method}")

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
    print(MSG_WRONG_MODULE)
