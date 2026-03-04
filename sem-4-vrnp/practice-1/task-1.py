# task 1.2.1(e)
import math

def f(a, x):
    return math.cos(x) * math.log( abs(2 - math.exp( -1 * abs(a + x) ) ) )


a, x = map(float, input( "enter a, x (integers): ").split())


print( "y = {}".format( f(a, x) ) )
