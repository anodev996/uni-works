import math

# task 2.1.1(e)
#arr = list(map(int, input("enter sequence of integers: ")))
arr = [1, -5, -9, 10, 0, 123, -5, 5, 7, -1] # тут знак меняется 4 раза

sign = 0 if arr[0] >= 0 else 1 # 0 is plus, 1 is minus
count = 0
for i in range(1, len(arr) - 1):
    if sign == 0 and arr[i] < 0:
        sign = 1
        count += 1
        print("T", end=' ')
    elif sign == 1 and arr[i] >= 0:
        sign = 0
        count += 1
        print("F", end=' ')

print()
print("sign changes {} times".format(count))
#print(len(arr))
