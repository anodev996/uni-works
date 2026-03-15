package app.hierarchy;

public class Circle extends Geometry {
    private double radius;

    public Circle(double radius) {
        super(true, false);
        setRadius(radius);
    }
    public Circle() {
        super(true, false);
        setRadius(0);
    }

    public double getRadius() {
        return radius;
    }
    public void setRadius(double radius) {
        this.radius = radius;
    }
    public double calculateArea() {
        return 3.14159*radius*radius;
    }
    @Override
    public String description() {
        return super.description() + String.format("Radius: %.2f\nArea: %.2f\n", getRadius(), calculateArea());
    }
}