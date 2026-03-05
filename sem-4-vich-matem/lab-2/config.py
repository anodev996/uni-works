#!/usr/bin/env python3
# coding:utf-8

A: float = -1.0
B: float = 1.0
EPS: float = 0.00001
EPS_DIVISION: float = 1e-15
ITERATION_MAX: int = 10

MSG_1: str = "Variant: #5 (Secant method)"
MSG_2: str = "Variant: #5 (Golden-section search)"
MSG_WARN: str = f"Warning: Method didn't find root in {ITERATION_MAX} iterations"
DELIM: str = "-" * len(MSG_WARN)