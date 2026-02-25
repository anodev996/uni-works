~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
Badulin Ilya, group 424-1.
Year I, second semester.
Laboratory class 6.

Task:
1) Make a program that implement an advanced sort function.

2) Make a second program that will make a several tests
...and count overall complexity of the function (compares + swaps)
...and put the tests data into a .csv file.

3) Compare tests data between sorts from laboratory 2 to laboratory 5.

Variant 1 - Binary Quicksort.

Пояснения:
Big O сортировки -- (w * n)
w -- количество бит, требуемых для хранения одного ключа (для 0 и 1 -- 1 бит)
n -- количество ключей (то есть, кол-во эл-ов массива
умноженное на максимальный разряд среди всех эл-ов массива)

У меня, сравнения + перестановки:
100   -- 221
10000 -- 21471
Итого, разница примерно 100 раз.

Если считать по Big O (1 элемент массива == 8 ключей):
100   -- (1 * (100 * 8) )   == ?
10000 -- (1 * (10000 * 8) ) == ?
Итого, разница ровно в 100 раз
~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~