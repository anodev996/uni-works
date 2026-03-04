# task 1.4.1(e)
import math

#n = int(input("enter n (natural): "))
#sequence = list(map(int, input("enter sequence of integers: ")))
n = 6
sequence = [1, 5, 6, 10, 0, 3]

for i in range(n - 1, -1, -1):
    if sequence[i] % 2 == 1:
        index = i
        break

print("last number of even entry in sequence: {}".format(index + 1))
