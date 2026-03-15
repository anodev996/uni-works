/* 
build
javac -d out src/app/*.java src/core/*.java

run
java -cp out app.Main

build & run
javac -d out src/app/*.java src/core/*.java && \
java -cp out app.Main
 */

//
/* */
/** */

package app;

import core.Room;
import core.CityOneRoomApartment;

public class Main {
    public static void main(String[] args) {
        System.out.println("\nOOP | Laboratory 2 | Variant 2\nBadulin Ilya | 424-1\n");
        
        Room room1 = new Room(20);
        Room room2 = new Room(25);
        
        CityOneRoomApartment apartment1 = new CityOneRoomApartment(room1, 10, 5, "Moscow");
        CityOneRoomApartment apartment2 = new CityOneRoomApartment(room2, 12, 3, "Saint Petersburg");
        CityOneRoomApartment apartment3 = new CityOneRoomApartment();
        
        System.out.println("Apartment 1:");
        System.out.println(apartment1);
        System.out.println("\nApartment 2:");
        System.out.println(apartment2);
        System.out.println("\nDefault Apartment:");
        System.out.println(apartment3 + "\n");
        
        try {
            CityOneRoomApartment invalid = new CityOneRoomApartment(null, 10, 5, "City");
        } catch (IllegalArgumentException e) {
            System.out.println("Exception: " + e.getMessage());
        }
        
        try {
            CityOneRoomApartment invalid = new CityOneRoomApartment(room1, -5, 3, "City");
        } catch (IllegalArgumentException e) {
            System.out.println("Exception: " + e.getMessage());
        }
        
        try {
            CityOneRoomApartment invalid = new CityOneRoomApartment(room1, 10, 0, "City");
        } catch (IllegalArgumentException e) {
            System.out.println("Exception: " + e.getMessage());
        }
        
        try {
            CityOneRoomApartment invalid = new CityOneRoomApartment(room1, 10, 5, "");
        } catch (IllegalArgumentException e) {
            System.out.println("Exception: " + e.getMessage());
        }
    }
}