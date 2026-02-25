package app.functions;

import java.lang.Math;

public class ExponentialFunction implements Function {
    @Override
    public double evaluate(double x) {
        return Math.exp(x) - 2;
    }

    @Override
    public String printOut() {
        return "f(x) = e^x - 2";
    }
}