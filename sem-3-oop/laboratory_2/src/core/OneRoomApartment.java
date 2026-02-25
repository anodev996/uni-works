package core;

public class OneRoomApartment {
    private Room room;
    private int kitchenArea;
    private int floor;

    public Room getRoom() {
        return room;
    }
    public int getKitchenArea() {
        return kitchenArea;
    }
    public int getFloor() {
        return floor;
    }

    public void setRoom(Room room) {
        if (room == null) {
            throw new IllegalArgumentException("Room is null");
        }
        this.room = room;
    }
    public void setKitchenArea(int kitchenArea) {
        if (kitchenArea < 0) {
            throw new IllegalArgumentException("Kitchen area is negative");
        }
        this.kitchenArea = kitchenArea;
    }
    public void setFloor(int floor) {
        if (floor < 1) {
            throw new IllegalArgumentException("Floor area is below 1");
        }
        this.floor = floor;

    }

    public OneRoomApartment(Room room, int kitchenArea, int floor) {
        setRoom(room);
        setKitchenArea(kitchenArea);
        setFloor(floor);
    }
    public OneRoomApartment() {
        this(new Room(), 0, 1);
    }

}