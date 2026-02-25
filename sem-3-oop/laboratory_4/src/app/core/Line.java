package app.core;

import java.lang.Math;

public class Line extends Point {
    private double x2, y2;

    public Line(double x, double y, double x2, double y2) {
        super(x, y);
        this.x2 = x2;
        this.y2 = y2;
    }
    public Line() {
        super();
        setX2(0);
        setY2(0);
    }

    public double getX2() {
        return x2;
    }
    public double getY2() {
        return y2;
    }
    public void setX2(double x2) {
        this.x2 = x2;
    } 
    public void setY2(double y2) {
        this.y2 = y2;
    }
    public double calculateLength() {
        return Math.sqrt( Math.pow((getX2() - getX()), 2) + Math.pow((getY2() - getY()), 2) );
    }
    @Override
    public String toString() {
        return (String.format("(%s)\nx  y : (%.1f ; %.1f)\nx1 y1: (%.1f ; %.1f)\nlength: %.1f\ncolor: %s\n\n",
        getClass(), getX(), getY(), getX2(), getY2(), calculateLength(), getColor()));
    }
}
