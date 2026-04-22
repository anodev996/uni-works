#!/usr/bin/env python3
import core

def get_manual_input():
    """Gets TV data from the user."""
    tvs = []
    while True:
        print("\n--- New TV ---")
        brand = input("Brand: ")
        price = input("Price: ")
        diagonal = input("Diagonal: ")
        country = input("Country: ")
        
        tvs.append({
            "brand": brand,
            "price": price,
            "diagonal": diagonal,
            "country": country
        })
        
        cont = input("Add more? (y/n): ")
        if cont.lower() != 'y':
            break
            
    return tvs

def main():
    print("1. Enter data manually")
    print("2. Use ready data")
    choice = input("Choice (1/2): ")
    
    tv_data = []
    
    if choice == '1':
        tv_data = get_manual_input()
    elif choice == '2':
        tv_data = core.get_hardcoded_data()
    else:
        print("\nBad choice. Exit.")
        return

    # Save to file
    if core.save_to_file(tv_data):
        print("\nDone! Data saved to tvs.txt")

if __name__ == "__main__":
    main()
