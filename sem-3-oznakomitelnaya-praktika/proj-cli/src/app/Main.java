/* Build & run
javac -d out src/app/*.java src/app/core/*.java \
src/app/exceptions/*.java \
src/app/functions/*.java \
&& java -cp out app.Main
 */

package app;

import app.InputHandler;
import app.functions.Function;
import app.core.BisectionSolver;
import app.exceptions.InvalidRangeException;

public class Main {
    public static void main(String[] args) {
        System.out.print("\nOznakomitelnaya praktika | Variant 2 | Bisection method\nBadulin Ilya | 424-1\n\n");

        InputHandler inputHandler = new InputHandler();
        Function function = inputHandler.chooseFunction();
        double a = inputHandler.chooseA();
        double b = inputHandler.chooseB();
        double epsilon = inputHandler.chooseEpsilon();

        BisectionSolver bisectionSolver = new BisectionSolver();
        try {
            double result = bisectionSolver.solve(function, a, b, epsilon);
            System.out.printf("Result: %.2f%nIterations: %d%nf(result) = %.10e%nf(result) = %.20f",
            result, bisectionSolver.getCounter(), function.evaluate(result), function.evaluate(result));
        }
        catch (InvalidRangeException e) {
            System.out.print(e.getMessage());
        }
    }
}