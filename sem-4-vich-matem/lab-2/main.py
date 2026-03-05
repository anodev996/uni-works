# vichislitelnaya matematika
# variant 5
import task_1 as t1

A = -1
B = 1
EPS = 0.00001
ITERATION_MAX = 100

MSG_1 = "Variant 5: Secant method"
MSG_2 = "Variant 5: Golden-section search"
DELIM = "-" * len(MSG_2)


print(f"{DELIM}\n{MSG_1}")
result = []
result = t1.secant_method(A, B, EPS, ITERATION_MAX)


print(f"{DELIM}\nroot: {result[0]:.10f}\neps: {EPS:.10f}\n" +
      f"iterations: {result[1]}\n{DELIM}")
