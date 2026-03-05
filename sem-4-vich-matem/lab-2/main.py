#!/usr/bin/env python3
# coding:utf-8

# Вычислительная математика; вариант 5.
#
# Из 'config.py'в namespace копируются только указанные имена.
from config import (
      A, B, EPS,
      ITERATION_MAX,
      MSG_1, MSG_2, MSG_WARN, DELIM
)
# Из 'task_1.py'в namespace копируются все имена;
# '__all__ = ['func_1', 'func_2']' контролирует импорт с '*',
# используется в библиотеках.
#from task_1 import *
# Из 'task_1.py' в namespace добавляется только 't1' как объект
# (поля=переменные, методы=функции).
import task_1 as t1
import task_2 as t2


def main():
      print(f"{DELIM}\n" +
            f"{MSG_1}")

      success, root, iteration = t1.secant_method(A, B, EPS, ITERATION_MAX)

      if success:
            print(f"{DELIM}\n" +
                  f"root: {root:.10f}\n" +
                  f"epsilon: {EPS:.10f}\n" +
                  f"iterations done: {iteration}\n" +
                  f"{DELIM}")
            
      print(f"{DELIM}\n" +
            f"{MSG_WARN}\n" +
            f"{DELIM}\n" +
            f"last approximation: {root:.10f}\n" +
            f"epsilon: {EPS:.10f}\n" +
            f"iterations done: {iteration}\n" +
            f"{DELIM}")

if __name__ == "__main__":
      main()