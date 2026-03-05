import math

def f(x):

    return 4 * x + math.e ** x

def f_prime(x):

    return 4 + math.e ** x

def f_double_prime(x):

    return math.e ** x

def initial_guess(a, b):

    if f(a) * f_double_prime(a) > 0:
        return a
    
    return b

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

'''
def delta_xn(x0, x1):

    return ( f(x1) - f(x0) ) / ( x1 - x0 )

def iteration_formula(x0, x1):

    return x1 - ( f(x1) / delta_xn(x0, x1) ) 

while abs( f( iteration_formula(x0, x1) ) ) < EPS:

    root = iteration_formula(x0, x1)
    iteration =+ 1
'''

def secant_method(a, b, eps, iteration_max):

    x0 = initial_guess(a, b)
    x1 = initial_guess(b, a)

    for iteration in range(iteration_max):

        x2 = iteration_formula(x0, x1)
        
        if abs(x2 - x1) < eps or abs(f(x2)) < eps:
            break
        
        # Обновляем значения для следующей итерации
        x0, x1 = x1, x2
        iteration += 1

    root = x2
    return [root, iteration]
