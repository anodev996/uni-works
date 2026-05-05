#!/usr/bin/env python3

def read_integers(filename):
    """Doc."""
    numbers = []
    try:
        with open(filename, 'r') as f:
            for line in f:
                try:
                    numbers.append(int(line.strip()))
                except ValueError:
                    pass
    except FileNotFoundError:
        print(f"File {filename} not found.")
    return numbers


def write_integers(filename, numbers):
    """Doc."""
    with open(filename, 'w') as f:
        for num in numbers:
            f.write(f"{num}\n")


def remove_negatives_before_positives(numbers):
    """Doc."""
    if len(numbers) < 2:
        return numbers.copy()
    
    result = []
    i = 0
    while i < len(numbers):
        if i < len(numbers) - 1 and numbers[i] < 0 and numbers[i + 1] > 0:
            i += 1  # skip negative number
        else:
            result.append(numbers[i])
            i += 1
    
    return result


def process_file(filename):
    """Doc."""
    numbers = read_integers(filename)
    if not numbers:
        return False
    
    processed = remove_negatives_before_positives(numbers)
    write_integers(filename + "_processed", processed)
    return True


def create_test_file(filename, numbers):
    """Doc."""
    write_integers(filename, numbers)
