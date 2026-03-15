package core;

public class CityOneRoomApartment extends OneRoomApartment {
    private String cityName;

    public String getCityName() {
        return cityName;
    }

    public void setCityName(String cityName) {
        if ( cityName == null || cityName.trim().isEmpty() ) {
            throw new IllegalArgumentException("City name is empty");
        }
        this.cityName = cityName;
    }

    public CityOneRoomApartment(Room room, int kitchenArea, int floor, String cityName) {
        super(room, kitchenArea, floor);
        setCityName(cityName);
    }
    public CityOneRoomApartment() {
        super();
        setCityName("Unknown");
    }

    public void cleanUp() {
        System.out.println("Object of CityOneRoomApartment: resources are released");
        /*
         *  SOME CODE
         */
    }

    @Override
    public String toString() {
        return String.format("City Name: %s\nRoom Area: %d\nKitchen Area: %d\nFloor: %d",
                             getCityName(), getRoom().getRoomArea(), getKitchenArea(), getFloor());
    }

}