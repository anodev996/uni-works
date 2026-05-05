#!/usr/bin/env python3

from core import check_balance, get_examples


def main():
    print("Check brackets balance. () [] {}")

    while True:
        print("\nOptions:")
        print("1) enter text")
        print("2) use example")
        print("q) quit")

        choice = input("> ").strip().lower()

        if choice == "q":
            break
        elif choice == "1":
            text = input("Text: ").strip()

            if text:
                result = check_balance(text)
                print(f"Balanced: {result}")
        elif choice == "2":
            examples = get_examples()

            print("\nExamples:")

            for key, ex in examples.items():
                print(f"{key} {ex['text']}")

            ex_choice = input("> ").strip()

            if ex_choice in examples:
                result = check_balance(examples[ex_choice]["text"])
                print(f"Balanced: {result}")
            else:
                print("Invalid choice")
        else:
            print("Invalid choice")


if __name__ == "__main__":
    main()
