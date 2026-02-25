package app.core;

public abstract class Point {
    private double x, y;

    public Point(double x, double y) {
        setX(x);
        setY(y);
    }
    public Point() {
        setX(0);
        setY(0);
    }

    public double getX() {
        return x;
    }
    public double getY() {
        return y;
    }
    public void setX(double x) {
        this.x = x;
    } 
    public void setY(double y) {
        this.y = y;
    }
    public String getColor() {
        return "NoColor";
    }
    public void setColor(String color) {

    }
    @Override
    public String toString() {
        return "object description";
    }
}
