package core;

public class Room {
    private int roomArea;

    public int getRoomArea() {
        return roomArea;
    }

    public void setRoomArea(int roomArea) {
        if (roomArea < 0) {
            throw new IllegalArgumentException("Room area is negative");
        }
        this.roomArea = roomArea;
    }

    public Room (int roomArea) {
        setRoomArea(roomArea);
    }
    public Room () {
        this(0);
    }
}
