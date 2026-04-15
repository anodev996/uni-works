#!/usr/bin/env python3

import core

def main():
    print("--- for ascending list ---")
    ascending = [2, 5, 8, 12, 20]
    print(f"original  : {ascending}")
    
    for v in [1, 10, 25, 8]:
        core.insert_ordered_ascending(ascending, v)
        print(f"insert {v:3}: {ascending}")

    print("\n--- for descending list ---")
    descending = [99, 50, 33, 20, 5, 0]
    print(f"original  : {descending}")
    
    for v in [100, 44, 2, 50]:
        core.insert_ordered_descending(descending, v)
        print(f"insert {v:3}: {descending}")

if __name__ == "__main__":
    main()
