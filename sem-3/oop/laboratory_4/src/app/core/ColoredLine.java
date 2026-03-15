package app.core;

public class ColoredLine extends Line implements Picture {
    private String color;

    public ColoredLine(double x, double y, double x2, double y2, String color) {
        super(x, y, x2, y2);
        this.color = color;
    }
    public ColoredLine() {
        super();
        this.color = "black";
    }

    @Override
    public String getColor() {
        return color;
    }
    @Override
    public void setColor(String color) {
        this.color = color;
    }
    @Override
    public void printInfo() {
        System.out.print(String.format("(%s)\nx  y : (%.1f ; %.1f)\nx1 y1: (%.1f ; %.1f)\nlength: %.1f\ncolor: %s\n\n",
        getClass(), getX(), getY(), getX2(), getY2(), calculateLength(), getColor()));
    }
}
