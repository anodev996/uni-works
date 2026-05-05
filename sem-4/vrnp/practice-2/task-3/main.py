#!/usr/bin/env python3

import os
import core as c

def main():
    filename = "test_numbers"
    test_numbers = [5, -2, 3, -4, -1, 6, -7, 8]
    
    c.create_test_file(filename, test_numbers)
    print(f" created   : '{filename}' with {test_numbers}")
    
    c.process_file(filename)
    print(f" processed : '{filename}'")

    filename = "test_numbers_processed"
    result = c.read_integers(filename)
    print(f" created   : '{filename}' with {result}")
    
if __name__ == "__main__":
    main()
