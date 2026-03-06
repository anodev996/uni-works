#!/usr/bin/env python3
"""Stores program constants"""

# Main program constants
T1_A: float = -1.0  # For secant_method left x-axis border
T1_B: float = 1.0  # For secant_method right x-axis border
T2_A: float = -100.0  # For golden_section_search left x-axis border
T2_B: float = 100.0  # For golden_section_search right x-axis border
EPS: float = 0.00001
ITERATION_MAX: int = 50
# Auxillary program constants
EPS_DIVISION: float = 1e-15
GR_X1: float = 0.382  # For x1, division of the x-axis by golden ratio
GR_X2: float = 0.618  # Same for x2

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
CLR_ACCENT: str = BOLD + LIGHT_YELLOW  # Color accent for reuse
MSG_1: str = f"{CLR_ACCENT}Variant:{RESET} #5 (Secant method)"
MSG_2: str = f"{CLR_ACCENT}Variant:{RESET} #5 (Golden-section search)"
MSG_WARN: str = (f"{CLR_ACCENT}Warning:{RESET} Method didn't find root "
                 + f"in {ITERATION_MAX} iterations")
MSG_WRONG_MODULE: str = ("Wrong module was ran, you should run "
                         + f"{CLR_ACCENT}main.py{RESET}, exiting...")
DELIM: str = DIM + CLR_ACCENT + "\"'" * 36 + RESET

if __name__ == "__main__":
    print(MSG_WRONG_MODULE)
