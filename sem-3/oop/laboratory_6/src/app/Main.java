package app;

import app.core.Computer;
import app.core.ComputerQueueUtility;
import java.util.LinkedList;
import java.util.Queue;

public class Main {
    public static void main(String[] args) {
        IOHandler.printGreet();

        Queue<Computer> computerQueue = new LinkedList<>();
        computerQueue.add(new Computer("LenovoB590", 8));
        computerQueue.add(new Computer("Dell XPS 15", 16));
        computerQueue.add(new Computer("MacBook Pro", 32));
        computerQueue.add(new Computer("Lenovo ThinkPad", 8));
        computerQueue.add(new Computer("Asus ROG", 64));

        ComputerQueueUtility.printOut(computerQueue);
        ComputerQueueUtility.printOverallMemory(computerQueue);
        computerQueue.remove();
        ComputerQueueUtility.printOut(computerQueue);
        ComputerQueueUtility.printOverallMemory(computerQueue);
    }   
}
