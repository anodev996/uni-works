#!/usr/bin/env python3

def insert_ordered_ascending(lst: list[int | float], value: int | float) -> None:
    index = 0

    while index < len(lst) and lst[index] < value:
        index += 1

    lst.insert(index, value)


def insert_ordered_descending(lst: list[int | float], value: int | float) -> None:
    index = 0

    while index < len(lst) and lst[index] > value:
        index += 1

    lst.insert(index, value)


def display_list(lst: list) -> None:
    print(f"Список: {lst}")
