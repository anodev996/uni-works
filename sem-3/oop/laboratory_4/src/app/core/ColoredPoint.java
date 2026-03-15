package app.core;

public class ColoredPoint extends Point {
    private String color;

    public ColoredPoint(double x, double y, String color) {
        super(x, y);
        setColor(color);
    }
    public ColoredPoint() {
        super();
        setColor("black");
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
    public String toString() {
        return String.format("(%s)\nx  y : (%.1f ; %.1f)\ncolor: %s\n\n",
        getClass(), getX(), getY(), getColor());
    }
}
