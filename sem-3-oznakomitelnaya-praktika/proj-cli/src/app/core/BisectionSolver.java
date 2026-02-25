package app.core;

import app.functions.Function;
import app.exceptions.InvalidRangeException;
import java.lang.Math;

public class BisectionSolver {
    private int iterationCounter = 0;
    public int getCounter() {
        return iterationCounter;
    }

    public double solve(Function function, double a, double b, double epsilon) throws InvalidRangeException {
        iterationCounter = 0;

        double fa = function.evaluate(a);
        double fb = function.evaluate(b);

        if (fa * fb > 0) {
            throw new InvalidRangeException("You entered invalid range (a, b), try again.");
        }
        
        while (Math.abs(b-a) > epsilon) {
            double c = (a + b) / 2;
            double fc = function.evaluate(c);

            iterationCounter++;

            if (fc == 0) return c;

            if (fa * fc < 0) {
                b = c;
                fb = fc;
            } else {
                a = c;
                fa = fc;
            }

        }
        return (a + b) / 2;
    }
}