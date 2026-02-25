package app.hierarchy;

public class Quadrangle extends Geometry {
    private int numberOfAngles;

    public Quadrangle() {
        super(true, true);
        numberOfAngles = 4;
    }
    
    public int getNumberOfAngles() {
        return numberOfAngles;
    }
    @Override
    public String description() {
        return super.description() + String.format("Number of angles: %d\n", getNumberOfAngles());
    }
}
