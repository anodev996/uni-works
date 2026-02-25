package app;

import app.core.Point;
import app.core.Triangle;
import app.exceptions.TriangleConstructException;

public class Main {

    public static void main(String[] args) {

        try {
            IOHandler.printGreet();
            
            Triangle triangle = new Triangle(
                new Point(0, 0),
                new Point(4, 0),
                new Point(2, 3)
            );
            System.out.println("Triangle created:");
            System.out.println(triangle);

            triangle.move(2, 1);
            System.out.println("After moving by (2,1):");
            System.out.println(triangle);

            triangle.scale(50);
            System.out.println("After scaling by +50%:");
            System.out.println(triangle);

            triangle.rotate(90);
            System.out.println("After rotating by 90°:");
            System.out.println(triangle);

            System.out.println("Attempting to create an invalid triangle:");
            Triangle triangle2 = new Triangle(
                new Point(0, 0),
                new Point(1, 1),
                new Point(2, 2)
            );

        } catch (TriangleConstructException e) {
            System.out.println("Error: " + e.getMessage());
        }
    }
}