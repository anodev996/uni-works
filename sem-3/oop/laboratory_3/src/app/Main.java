package app;

import app.hierarchy.*;

public class Main {
    public static void main(String[] args) {
        System.out.print("\nOOP | lab 3 | variant 2\nBadulin Ilya | 424-1\n\n");

        Geometry geometry = new Geometry();
        Geometry quadrangle = new Quadrangle();
        Geometry triangle = new Triangle();
        Geometry point = new Point(5, 3);
        Geometry circle = new Circle(4);
        Geometry rectangle = new Rectangle(1,3);
        Geometry quadrate = new Quadrate(5);
        Geometry rhombus = new Rhombus(2);

        Main.polymorphismDemo(geometry);
        Main.polymorphismDemo(quadrangle);
        Main.polymorphismDemo(triangle);
        Main.polymorphismDemo(point);
        Main.polymorphismDemo(circle);
        Main.polymorphismDemo(rectangle);
        Main.polymorphismDemo(quadrate);
        Main.polymorphismDemo(rhombus);
    }

    private static void polymorphismDemo(Geometry geometry) {
        if (geometry instanceof Point) {
            System.out.print("|\n| Point\n|\nv\n");
        }

        System.out.println(geometry.description());
    }
}
