package app.functions;

import java.lang.Math;

public class TrigonometricFunction implements Function {
    @Override
    public double evaluate(double x) {
        return Math.sin(x) - 0.5;
    }

    @Override
    public String printOut() {
        return "f(x) = sin(x) - 0.5";
    }
}