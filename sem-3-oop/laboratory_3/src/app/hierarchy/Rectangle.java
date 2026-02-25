package app.hierarchy;

public class Rectangle extends Quadrangle {
    private double sideShort;
    private double sideLong;

    public Rectangle(double sideShort, double sideLong) {
        super();
        setSideShort(sideShort);
        setSideLong(sideLong);
    }
    public Rectangle() {
        super();
        setSideShort(0);
        setSideLong(0);
    }

    public double getSideShort() {
        return sideShort;
    }
    public void setSideShort(double sideShort) {
        this.sideShort = sideShort;
    }
    public void setSideLong(double sideLong) {
        this.sideLong = sideLong;
    }
    public double getSideLong() {
        return sideLong;
    }
    public double calculatePerimeter() {
        return (sideShort * 2) + (sideLong * 2);
    }
    @Override
    public String description() {
        return super.description() + String.format("Length of the short side: %.2f\nLength of the long side: %.2f\nPerimeter: %.2f\n", 
        getSideShort(), getSideShort(), calculatePerimeter());
    }
} 