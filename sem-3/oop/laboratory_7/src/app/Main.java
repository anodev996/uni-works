package app;

import app.core.Order;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

public class Main {

    public static void main(String[] args) {
        
        try {
            IOHandler.printGreet();

            System.out.print("Creating `orderList` with orders...\n\n");
            List<Order> orderList = new ArrayList<>();
            orderList.add(new Order("ABC-12", "Chair", "DEF-32", "2025-07-11-02:14", 2));
            orderList.add(new Order("ABC-78", "USB Mouse", "DEF-87", "2025-05-09-12:14", 2));
            orderList.add(new Order("ABC-23", "USB Keyboard", "DEF-37", "2025-02-22-15:14", 1));
            orderList.add(new Order("ABC-54", "Refrigerator", "DEF-64", "2025-01-30-23:14", 1));

            System.out.print("Saving `orderList` to `Orders.txt`...\n\n");
            IOHandler.saveOrderListToFile(orderList, "Orders.txt");

            System.out.print("Printing `Orders.txt` contents...\n\n");
            IOHandler.printFile("Orders.txt");

            System.out.print("\nCreating empty `orderList2`...\n\n");
            List<Order> orderList2 = new ArrayList<>();

            System.out.print("Loading orders from `Orders.txt` to `orderList2`...\n\n");
            IOHandler.loadOrderListFromFile(orderList2, "Orders.txt");

            System.out.print("Printing `orderList2`...\n\n");
            IOHandler.printOrderList(orderList2);

        } catch (IOException e) {
            System.err.print(e.getMessage() + "\n");
            System.out.print("Exiting program...\n");
            System.exit(1);
        }
    }
}
