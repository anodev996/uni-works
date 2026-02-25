package app.functions;

import java.lang.Math;

public class CubicFunction implements Function {
    @Override
    public double evaluate(double x) {
        return Math.pow(x, 3) - 9*x*x + 27*x - 28;
    }

    @Override
    public String printOut() {
        return "f(x) = x^3 - 9(x^2) + 27x - 28";
    }
}