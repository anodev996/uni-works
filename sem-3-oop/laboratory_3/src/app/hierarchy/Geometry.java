package app.hierarchy;

public class Geometry {
    private boolean hasArea;
    private boolean hasAngles;

    public Geometry (boolean hasArea, boolean hasAngles) {
        setHasArea(hasArea);
        setHasAngles(hasAngles);
    }
    public Geometry() {
        setHasArea(false);
        setHasAngles(false);
    }

    public boolean getHasArea() {
        return hasArea;
    }
    public void setHasArea(boolean hasArea) {
        this.hasArea = hasArea;
    }
    public boolean getHasAngles() {
        return hasAngles;
    }
    public void setHasAngles(boolean hasAngles) {
        this.hasAngles = hasAngles;
    }
    public String description() {
        return String.format("(%s)\nHas area: %b\nHas angles: %b\n", getClass(), getHasArea(), getHasAngles());
    }
}
