package app.hierarchy;

public class Rhombus extends Quadrangle {
    private double sideLength;

    public Rhombus(double sideLength) {
        super();
        setSideLength(sideLength);
    }
    public Rhombus() {
        super();
        setSideLength(0);
    }

    public double getSideLength() {
        return sideLength;
    }
    public void setSideLength(double sideLength) {
        this.sideLength = sideLength;
    }
    public double calculatePerimeter() {
        return sideLength * 4;
    }
    @Override
    public String description() {
        return super.description() + String.format("Side length: %.2f\nPerimeter: %.2f\n", 
        getSideLength(), calculatePerimeter());
    }
} 