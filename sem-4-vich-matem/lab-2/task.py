# vichislitelnaya matematika
# variant 5

'''
import math

a = -1
b = 1


def f(x):
    return 4 * x + math.e ** x

def f_prime(x):
    return 4 + math.e ** x

def f_double_prime(x):
    return math.e ** x

def initial_guess(a, b):
    if f(a) * f_double_prime(a) > 0:
        return a
    else:
        return b

x0 = initial_guess(a, b)
x1 = initial_guess(b, a)

def delta_xn(x0, x1):
    return ( f(x1) - f(x0) ) / ( x1 - x0 )

def iteration_formula(x0, x1):
    return x1 - ( f(x1) / delta_xn(x0, x1) ) 

eps = 0.00001
iteraton = 0
iteration_max = 100
root = 0
while abs( f( iteration_formula(x0, x1) ) ) < eps:
    root = iteration_formula(x0, x1)
    iteration =+ 1

print("_________\nvariant 5\n=========")
print("root: {:.10f}\neps: {:.10f}\niterations: {}\n".format(root, eps, iteration))
'''

import math

a = -1
b = 1

def f(x):
    return 4 * x + math.e ** x

def f_prime(x):
    return 4 + math.e ** x

def f_double_prime(x):
    return math.e ** x

def initial_guess(a, b):
    if f(a) * f_double_prime(a) > 0:
        return a
    else:
        return b

x0 = initial_guess(a, b)
x1 = initial_guess(b, a)

def delta_xn(x0, x1):
    # Проверка на деление на ноль
    if abs(x1 - x0) < 1e-15:  # если разница очень мала
        return f_prime(x1)  # используем производную как предел
    return (f(x1) - f(x0)) / (x1 - x0)

def iteration_formula(x0, x1):
    # Проверка знаменателя перед делением
    delta = delta_xn(x0, x1)
    if abs(delta) < 1e-15:  # если дельта близка к нулю
        return x1 - f(x1) / f_prime(x1)  # метод Ньютона как запасной вариант
    return x1 - (f(x1) / delta)

eps = 0.00001
iteration = 0
max_iterations = 100  # защита от бесконечного цикла

# Выполняем итерации до достижения точности
for iteration in range(max_iterations):
    x2 = iteration_formula(x0, x1)
    
    if abs(x2 - x1) < eps:  # проверка сходимости
        break
        
    if abs(f(x2)) < eps:  # проверка значения функции
        break
    
    # Обновляем значения для следующей итерации
    x0, x1 = x1, x2
    iteration += 1

root = x2

print("_________\nvariant 5\n=========")
print("root: {:.10f}\neps: {:.10f}\niterations: {}\n".format(root, eps, iteration))
