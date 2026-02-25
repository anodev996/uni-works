package app.hierarchy;

public class Triangle extends Geometry {
    private int numberOfAngles;

    public Triangle() {
        super(true, true);
        numberOfAngles = 3;
    }

    public int getNumberOfAngles() {
        return numberOfAngles;
    }
    @Override
    public String description() {
        return super.description() + String.format("Number of angles: %d\n", getNumberOfAngles());
    }
}
