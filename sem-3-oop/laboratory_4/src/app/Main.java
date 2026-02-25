package app;

import app.core.*;

public class Main {
    public static void main(String[] args) {
        System.out.print("\nOOP | lab 4 | variant 2\nBadulin Ilya | 424-1\n\n");

        ColoredPoint coloredPoint = new ColoredPoint(3, 7.6, "white");
        System.out.print(coloredPoint);
        coloredPoint.setX(0);
        coloredPoint.setY(10);
        coloredPoint.setColor("pink");
        System.out.print(coloredPoint);

        Line line = new Line();
        System.out.print(line);
        line.setX(9);
        line.setY(4);
        line.setX2(2.3);
        line.setY2(3);
        System.out.print(line);

        ColoredLine coloredLine = new ColoredLine(3.2, 1.4, 5.9, 9.0,"blue");
        coloredLine.printInfo();
        coloredLine.setX(9);
        coloredLine.setY(4);
        coloredLine.setX2(2.3);
        coloredLine.setY2(3);
        coloredLine.setColor("red");
        Picture coloredLineI = coloredLine;
        coloredLineI.printInfo();

        PolyLine polyLine = new PolyLine( new double[][] {{1,2}, {5,5}} );
        polyLine.printInfo();
        polyLine.setX(9);
        polyLine.setY(4);
        polyLine.setX2(2.3);
        polyLine.setY2(3);
        polyLine.setCoordinates( new double[][] {{3,3}, {0.2,0.9}, {1.2, 3.3}} );
        Picture polyLineI = polyLine;
        polyLineI.printInfo();
    }
}
