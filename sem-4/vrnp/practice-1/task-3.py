#!/usr/bin/env python3
# task 2.1.1(e)

#arr = list( map( int, input( "enter sequence of integers: ").split() ) )
arr = [1, -5, -9, 10, 0, 123, -6, 5, 7, -1] # тут знак меняется 5 раз


sign = 0 if arr[0] >= 0 else 1 # 0 is plus, 1 is minus
count = 0
for i in range(1, len(arr)):
    
    if sign == 0 and arr[i] < 0:
        sign = 1
        count += 1
    elif sign == 1 and arr[i] >= 0:
        sign = 0
        count += 1


print("sign changes {} times".format(count))
