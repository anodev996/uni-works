package app.functions;

public class QuadraticFunction implements Function {
    @Override
    public double evaluate(double x) {
        return x*x + 2*x - 3;
    }

    @Override
    public String printOut() {
        return "f(x) = x^2 + 2x - 3";
    }
}