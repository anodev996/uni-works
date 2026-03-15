package app.core;

import app.exceptions.TriangleConstructException;

public class Triangle {

    private Point a;
    private Point b;
    private Point c;


    public Triangle(Point a, Point b, Point c) 
                    throws TriangleConstructException {

        validate(a, b, c);
        this.a = a;
        this.b = b;
        this.c = c;
    }


    public void move(double x, double y) {

        a.setX(a.getX() + x);   a.setY(a.getY() + y);
        b.setX(b.getX() + x);   b.setY(b.getY() + y);
        c.setX(c.getX() + x);   c.setY(c.getY() + y);
    }

    public void scale(double percent) throws TriangleConstructException{

        double multiplier = 1 + percent / 100.0;

        Point centroid = calcCentroid();
        double centroidX = centroid.getX();
        double centroidY = centroid.getY();


        a.setX(centroidX + (a.getX() - centroidX) * multiplier);
        a.setY(centroidY + (a.getY() - centroidY) * multiplier);

        b.setX(centroidX + (b.getX() - centroidX) * multiplier);
        b.setY(centroidY + (b.getY() - centroidY) * multiplier);

        c.setX(centroidX + (c.getX() - centroidX) * multiplier);
        c.setY(centroidY + (c.getY() - centroidY) * multiplier);

        validate(a, b, c);
    }

    public void rotate(double angleDegrees) {

        double angleRadians = Math.toRadians(angleDegrees);
        double cos = Math.cos(angleRadians);
        double sin = Math.sin(angleRadians);

        Point centroid = calcCentroid();
        double cx = centroid.getX();
        double cy = centroid.getY();


        rotatePoint(a, cx, cy, cos, sin);
        rotatePoint(b, cx, cy, cos, sin);
        rotatePoint(c, cx, cy, cos, sin);
    }

    public double calcArea(Point a, Point b, Point c) {

        return 0.5 * Math.abs(
            (b.getX() - a.getX()) * (c.getY() - a.getY()) -
            (c.getX() - a.getX()) * (b.getY() - a.getY())
        );
    }

    public Point calcCentroid() {

        double x = (a.getX() + b.getX() + c.getX()) / 3;
        double y = (a.getY() + b.getY() + c.getY()) / 3;

        return new Point(x, y);
    }

    @Override
    public String toString() {

        return String.format("%s%nA: %s%nB: %s%nC: %s%n" +
                             "M: %s - centroid%nArea: %4.1f%n",
                             getClass(), a, b, c,
                             calcCentroid(), calcArea(a, b, c));
    }

    public Point getA() {return a;}

    public Point getB() {return b;}

    public Point getC() {return c;}

    public void setA(Point a) throws TriangleConstructException {

        validate(a, this.b, this.c);
        this.a = a;
    }

    public void setB(Point b) throws TriangleConstructException {

        validate(this.a, b, this.c);
        this.b = b;
    }

    public void setC(Point c) throws TriangleConstructException {

        validate(this.a, this.b, c);
        this.c = c;
    }

    private void rotatePoint(Point p, double cx, double cy, double cos, double sin) {

        double x = p.getX() - cx;
        double y = p.getY() - cy;

        double newX = x * cos - y * sin;
        double newY = x * sin + y * cos;

        
        p.setX(cx + newX);
        p.setY(cy + newY);
    }

    private void validate(Point a, Point b, Point c) 
                          throws TriangleConstructException {
    
        if (calcArea(a, b, c) == 0) {
            throw new TriangleConstructException("Cannot construct " +
                                                 "such triangle");
        }
    }
}