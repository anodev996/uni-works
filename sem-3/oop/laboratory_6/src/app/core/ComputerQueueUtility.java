package app.core;

import java.util.Iterator;
import java.util.Queue;

public class ComputerQueueUtility {

    public static void printOut(Queue<Computer> computerQueue) {

        System.out.print("\n     HEAD\n      |\n      V\n");

        Iterator<Computer> iterator = computerQueue.iterator();
        while (iterator.hasNext()) {
            System.out.print(iterator.next());

            if (iterator.hasNext()) {
                System.out.println("      |\n      |");
            }
        }
    }

    public static void printOverallMemory (Queue<Computer> computerQueue) {
        
        int memoryTotal = 0;
        Iterator<Computer> iterator = computerQueue.iterator();
        while (iterator.hasNext()) {
            memoryTotal += iterator.next().getMemoryCapacity();
        }

        System.out.printf("\nOverall RAM capacity in queue: %d Gb\n\n", memoryTotal);
    }
}
