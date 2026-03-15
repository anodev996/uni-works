package app.core;

public class Computer {
    private String computerModel;
    private int memoryCapacity;

    public Computer(String computerModel, int memoryCapacity) {
        this.computerModel = computerModel;
        this.memoryCapacity = memoryCapacity;
    }

    public String getComputerModel() {
        return computerModel;
    }
    public int getMemoryCapacity() {
        return memoryCapacity;
    }
    public void setComputerModel(String computerModel) {
        this.computerModel = computerModel;
    }
    public void setMemoryCapacity(int memoryCapacity) {
        this.memoryCapacity = memoryCapacity;
    }
    @Override public String toString() {
        return String.format("Computer model : %s\nRAM capacity   : %d Gb\n",
        getComputerModel(), getMemoryCapacity());
    }
}