from core import Polynomial

def get_poly_from_user(name):
    """Doc."""
    print(f"\nsetting polynomial {name}")
    print("enter coefficients separated by spaces, from lowest degree to highest.")
    print("example: '5 -2 3' <=> 5 - 2*x + 3*x^2")
    user_input = input("coefficients: ")
    
    try:
        coeffs = [float(x) for x in user_input.split()]
        if not coeffs:
            return Polynomial([0.0])
        return Polynomial(coeffs)
    except ValueError:
        print("Error: Invalid input. Creating a zero polynomial instead.")
        return Polynomial([0.0])

def run_menu():
    """Doc."""
    poly_a = Polynomial([0.0])
    poly_b = Polynomial([0.0])
    
    while True:

        print("- menu -")
        print("1 set polynomial A")
        print("2 set polynomial B")
        print("3 print polynomials")
        print("4 evaluate polynomial A for x")
        print("5 add A + B")
        print("6 subtract A - B")
        print("7 multiply A * B")
        print("0 exit")
        print()
        
        choice = input("enter your choice: ")
        
        if choice == '1':
            poly_a = get_poly_from_user("A")
            print(f"Polynomial A is now: {poly_a}")
            
        elif choice == '2':
            poly_b = get_poly_from_user("B")
            print(f"Polynomial B is now: {poly_b}")
            
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
            break
            
        else:
            print("\nInvalid choice.")


if __name__ == "__main__":
    run_menu()
