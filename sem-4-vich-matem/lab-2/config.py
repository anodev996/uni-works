#!/usr/bin/env python3
"""Stores program constants"""

# Main program constantsmain program
A: float = -1.0
B: float = 1.0
EPS: float = 0.00001
EPS_DIVISION: float = 1e-15
ITERATION_MAX: int = 3

# Esc sequences for coloration
RESET = "\033[0m"  # Reset color
# Styles
BOLD = "\033[1m"
DIM = "\033[2m"
ITALIC = "\033[3m"
UNDERLINE = "\033[4m"
BLINK = "\033[5m"
REVERSE = "\033[7m"  # Revert colors
HIDDEN = "\033[8m"  # Invisible text
# Standart text colors
BLACK = "\033[30m"
RED = "\033[31m"
GREEN = "\033[32m"
YELLOW = "\033[33m"
BLUE = "\033[34m"
MAGENTA = "\033[35m"
CYAN = "\033[36m"
WHITE = "\033[37m"  # White (more like grey)
# Light text colors
LIGHT_BLACK = "\033[90m"  # Dark-grey
LIGHT_RED = "\033[91m"
LIGHT_GREEN = "\033[92m"
LIGHT_YELLOW = "\033[93m"
LIGHT_BLUE = "\033[94m"
LIGHT_MAGENTA = "\033[95m"
LIGHT_CYAN = "\033[96m"
LIGHT_WHITE = "\033[97m"  # Light-white

# Text output
CLR_ACCNT: str = LIGHT_CYAN  # Color accent for reuse
MSG_1: str = f"{BOLD}{CLR_ACCNT}Variant:{RESET} #5 (Secant method)"
MSG_2: str = "Variant: #5 (Golden-section search)"
MSG_WARN: str = (f"{BOLD}{CLR_ACCNT}Warning:{RESET} Method didn't find root "
                 + f"in {ITERATION_MAX} iterations")
DELIM: str = DIM + CLR_ACCNT + "-" * len(MSG_WARN) + RESET
