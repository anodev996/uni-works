# vichislitelnaya matematika
# variant 5
import task_1 as t1

A: float = -1.0
B: float = 1.0
EPS: float = 0.00001
ITERATION_MAX: int = 10

MSG_1: str = "Variant: #5 (Secant method)"
MSG_2: str = "Variant: #5 (Golden-section search)"
MSG_3: str = f"Note: Method didn't find root in {ITERATION_MAX} iterations"
DELIM: str = "-" * len(MSG_3)


print(f"{DELIM}\n{MSG_1}")

success, root, iteration = t1.secant_method(A, B, EPS, ITERATION_MAX)

if success:
    print(f"{DELIM}\nroot: {root:.10f}\n" +
          f"epsilon: {EPS:.10f}\niterations done: {iteration}\n{DELIM}")
else:
    print(f"{DELIM}\n{MSG_3}\n{DELIM}\nlast approximation: {root:.10f}\n" +
          f"epsilon: {EPS:.10f}\niterations done: {iteration}\n{DELIM}")


'''
print(f"{DELIM}\n{MSG_2}")

result = t1.secant_method(A, B, EPS, ITERATION_MAX)

print(f"{DELIM}\nroot: {root:.10f}\neps: {EPS:.10f}\n" +
      f"iterations: {iteration}\n{DELIM}")
'''