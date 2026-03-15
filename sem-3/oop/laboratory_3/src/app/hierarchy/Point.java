package app.hierarchy;

public class Point extends Geometry {
    private double x, y;

    public Point(double x, double y) {
        super(false, false);
        setCoordinates(x, y);
    }
    public Point() {
        super(false, false);
        setCoordinates(0, 0);
    }

    public double getX() {
        return x;
    }
    public double getY() {
        return y;
    }
    public void setCoordinates(double x, double y) {
        this.x = x;
        this.y = y;
    }
    @Override
    public String description() {
        return super.description() + String.format("Coordinates: (%.2f ; %.2f)\n", getX(), getY());
    }
}