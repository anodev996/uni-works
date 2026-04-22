class Polynomial:
    def __init__(self, coefficients):
        """
        Initializes the polynomial.
        Index of the array corresponds to the degree of x.
        Example: [5, -2, 3] -> 5 - 2*x + 3*x^2
        """
        if not coefficients:
            self.coefficients = [0.0]
        else:
            # Strip trailing zeros to accurately determine the true degree
            coeffs = list(coefficients)
            while len(coeffs) > 1 and coeffs[-1] == 0:
                coeffs.pop()
            # Convert all to float for consistency
            self.coefficients = [float(c) for c in coeffs]
            
        self.degree = len(self.coefficients) - 1

    def evaluate(self, x):
        """Evaluates the polynomial for a given value of x."""
        result = 0.0
        for i, coef in enumerate(self.coefficients):
            result += coef * (x ** i)
        return result

    def __add__(self, other):
        """Adds two polynomials and returns a new Polynomial object."""
        max_len = max(len(self.coefficients), len(other.coefficients))
        new_coeffs = [0.0] * max_len
        
        for i in range(max_len):
            val1 = self.coefficients[i] if i < len(self.coefficients) else 0.0
            val2 = other.coefficients[i] if i < len(other.coefficients) else 0.0
            new_coeffs[i] = val1 + val2
            
        return Polynomial(new_coeffs)

    def __sub__(self, other):
        """Subtracts another polynomial and returns a new Polynomial object."""
        max_len = max(len(self.coefficients), len(other.coefficients))
        new_coeffs = [0.0] * max_len
        
        for i in range(max_len):
            val1 = self.coefficients[i] if i < len(self.coefficients) else 0.0
            val2 = other.coefficients[i] if i < len(other.coefficients) else 0.0
            new_coeffs[i] = val1 - val2
            
        return Polynomial(new_coeffs)

    def __mul__(self, other):
        """Multiplies two polynomials and returns a new Polynomial object."""
        new_coeffs = [0.0] * (self.degree + other.degree + 1)
        
        for i, c1 in enumerate(self.coefficients):
            for j, c2 in enumerate(other.coefficients):
                new_coeffs[i + j] += c1 * c2
                
        return Polynomial(new_coeffs)

    def __str__(self):
        """Returns a formatted string description of the polynomial."""
        if self.degree == 0 and self.coefficients == 0.0:
            return "0"
            
        terms = []
        for i in range(self.degree, -1, -1):
            coef = self.coefficients[i]
            if coef == 0:
                continue
                
            # Format each term properly based on its degree
            if i == 0:
                term = f"{coef}"
            elif i == 1:
                term = f"{coef}*x"
            else:
                term = f"{coef}*x^{i}"
                
            terms.append(term)
            
        # Join terms and fix appearance of negative coefficients
        result = " + ".join(terms)
        result = result.replace("+ -", "- ")
        return result
    