package app;

import app.core.Order;
import java.io.BufferedReader;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.util.Iterator;
import java.util.List;

public class IOHandler {

    public static void printGreet() {
        System.out.print("\nOOP | lab 7 | variant 2 | Badulin Ilya | 424-1\n\n");
    }

    public static void saveOrderListToFile(List<Order> orderList, String fileName) throws IOException {
        
        try (FileWriter fileWriter = new FileWriter(fileName)) {

            Iterator<Order> iterator = orderList.iterator();
            while (iterator.hasNext()) {
                fileWriter.write(iterator.next().toStringRaw() + System.lineSeparator());
            }
        }
    }

    public static void printFile(String fileName) throws IOException {

        try (BufferedReader bufferedReader = new BufferedReader(new FileReader(fileName))) {

            System.out.printf("==== %s BEGIN ====\n", fileName);
            String line;
            while ((line = bufferedReader.readLine()) != null) {
                System.out.println(line);
            }
            System.out.printf("==== %s END ======\n", fileName);
        }
    }

    public static void loadOrderListFromFile(List<Order> orderList, String fileName) throws IOException {

        try (BufferedReader bufferedReader = new BufferedReader(new FileReader(fileName))) {
                
            String line;
            String[] currentOrder = new String[5];
            int index = 0;
            
            while ((line = bufferedReader.readLine()) != null) {
                
                if (line.trim().isEmpty()) {
                    
                    if (index == 5) {
                        orderList.add(new Order(
                            currentOrder[0],
                            currentOrder[1],
                            currentOrder[2],
                            currentOrder[3],
                            Integer.parseInt(currentOrder[4])
                        ));
                    }
                    index = 0;
                    continue;
                }
                
                if (index < 5) {
                    currentOrder[index] = line.trim();
                    index++;
                }
            }
            
            if (index == 5) {
                orderList.add(new Order(
                    currentOrder[0],
                    currentOrder[1],
                    currentOrder[2],
                    currentOrder[3],
                    Integer.parseInt(currentOrder[4])
                ));
            }
        }
    }

    public static void printOrderList(List<Order> orderList) {
        System.out.printf("==== %s ====\n", orderList.getClass());

        Iterator<Order> iterator = orderList.iterator();
        while (iterator.hasNext()) {
            System.out.print(iterator.next() + "\n");
        }
        System.out.print("====================================");
    }
}
