package app.core;

public class PolyLine extends Line implements Picture {
    private double[][] coordinates;

    public PolyLine(double[][] coordinates) {
        super();
        setCoordinates(coordinates);
    }
    public PolyLine() {
        super();
        setCoordinates(new double[0][2]);
    }

    public double[][] getCoordinates() {
        return coordinates;
    }
    public void setCoordinates(double[][] coordinates) {
        this.coordinates = coordinates;
    }
    public void printCoordinates() {
        System.out.print("coordinates:\n");
        for (int i=0; i < coordinates.length; i++) {
            System.out.print(String.format("    row %d: ", i));
            for (int j=0; j < coordinates[i].length; j++) {
                System.out.print(coordinates[i][j] + " ");
            }
            System.out.println();
        }
        System.out.println();
    }
    @Override
    public void printInfo() {
        System.out.print(String.format("(%s)\nx  y : (%.1f ; %.1f)\nx1 y1: (%.1f ; %.1f)\ncolor: %s\n",
        getClass(), getX(), getY(), getX2(), getY2(), getColor()));

        printCoordinates();
    }
}
