# task 1.2.1(e)
import math

a = float(input("enter a: "))
x = float(input("enter x: "))

def f(a, x):
    return math.cos(x) * math.log( abs(2 - math.exp( -1 * abs(a + x) ) ) )

print( "y = {}".format( f(a, x) ) )
