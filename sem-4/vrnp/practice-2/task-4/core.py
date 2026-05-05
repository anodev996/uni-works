#!/usr/bin/env python3

def get_hardcoded_data():
    """Returns a ready list of TVs."""
    return [
        {"brand": "Samsung", "price": "500", "diagonal": "50", "country": "Korea"},
        {"brand": "LG", "price": "450", "diagonal": "45", "country": "Korea"},
        {"brand": "Sony", "price": "700", "diagonal": "55", "country": "Japan"},
        {"brand": "TCL", "price": "300", "diagonal": "40", "country": "China"}
    ]

def save_to_file(tv_list, filename="tvs.txt"):
    """Saves the list of TVs to a text file."""
    try:
        with open(filename, "w", encoding="utf-8") as file:
            for tv in tv_list:
                line = f"{tv['brand']} | {tv['price']}$ | {tv['diagonal']}\" | {tv['country']}\n"
                file.write(line)
        return True
    except Exception as e:
        print(f"Error: {e}")
        return False
