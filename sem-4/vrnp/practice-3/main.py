from core import Polynomial

def get_poly_from_user(name):
    """Helper function to cleanly gather coefficients from the user."""
    print(f"\n--- Setting Polynomial {name} ---")
    print("Enter coefficients separated by spaces, from lowest degree to highest.")
    print("Example: '5 -2 3' means 5 - 2*x + 3*x^2")
    user_input = input("Coefficients: ")
    
    try:
        coeffs = [float(x) for x in user_input.split()]
        if not coeffs:
            return Polynomial()
        return Polynomial(coeffs)
    except ValueError:
        print("Error: Invalid input. Creating a zero polynomial instead.")
        return Polynomial()

def run_menu():
    """Main loop for the polynomial testing menu."""
    poly_a = Polynomial([0.0])
    poly_b = Polynomial([0.0])
    
    while True:

        print("="*30)
        print("1. Set Polynomial A")
        print("2. Set Polynomial B")
        print("3. Print Polynomials A and B")
        print("4. Evaluate Polynomial A for x")
        print("5. Add (A + B)")
        print("6. Subtract (A - B)")
        print("7. Multiply (A * B)")
        print("0. Exit")
        print("="*30)
        
        choice = input("Enter your choice: ")
        
        if choice == '1':
            poly_a = get_poly_from_user("A")
            print(f"Success! Polynomial A is now: {poly_a}")
            
        elif choice == '2':
            poly_b = get_poly_from_user("B")
            print(f"Success! Polynomial B is now: {poly_b}")
            
        elif choice == '3':
            print(f"\nPolynomial A (Degree {poly_a.degree}): {poly_a}")
            print(f"Polynomial B (Degree {poly_b.degree}): {poly_b}")
            
        elif choice == '4':
            try:
                x_val = float(input("\nEnter a value for x: "))
                result = poly_a.evaluate(x_val)
                print(f"P({x_val}) = {result}")
            except ValueError:
                print("Error: Please enter a valid number.")
                
        elif choice == '5':
            result = poly_a + poly_b
            print(f"\n(A + B) = {result}")
            
        elif choice == '6':
            result = poly_a - poly_b
            print(f"\n(A - B) = {result}")
            
        elif choice == '7':
            result = poly_a * poly_b
            print(f"\n(A * B) = {result}")
            
        elif choice == '0':
            print("\nExiting the program. Goodbye!")
            break
            
        else:
            print("\nInvalid choice. Please enter a number between 0 and 7.")


if __name__ == "__main__":
    run_menu()