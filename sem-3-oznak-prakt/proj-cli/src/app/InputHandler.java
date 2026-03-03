package app;

import java.util.Scanner;
import app.functions.*;

public class InputHandler {
    private Scanner scanner;
    private Function func1;
    private Function func2;
    private Function func3;
    private Function func4;

    public InputHandler() {
        scanner = new Scanner(System.in);
        func1 = new CubicFunction();
        func2 = new QuadraticFunction();
        func3 = new ExponentialFunction();
        func4 = new TrigonometricFunction();
    }

    public Function chooseFunction() throws IllegalStateException {
         System.out.print(String.format("1) %s\n2) %s\n3) %s\n4) %s\n\n",
         func1.printOut(), func2.printOut(), func3.printOut(), func4.printOut()));

        int choise = 0;
        boolean valid = false;
        while (!valid) {
            System.out.print("Enter function number: ");

            if (scanner.hasNextInt()) {
                choise = scanner.nextInt();
                if (choise >= 1 && choise <= 4 ) {
                    valid = true;
                } else {
                    System.out.print("The input isn't valid. Try again...\n");
                }
            } else {
                System.out.print("The input isn't valid. Try again...\n");
                scanner.next();
            }
        }
        
        switch (choise) {
            case 1: return func1;
            case 2: return func2;
            case 3: return func3;
            case 4: return func4;
            default: throw new IllegalStateException("chooseFunction() input error");
        }
    }

    public double chooseA() {
        double a = 0;
        boolean valid = false;
        while (!valid) {
            System.out.print("Enter 'a': ");

            if (scanner.hasNextDouble()) {
                a = scanner.nextDouble();
                valid = true;
            } else {
                System.out.print("The input isn't valid. Try again...\n");
                scanner.next();
            }
        }
        return a;
    }

    public double chooseB() {
        double b = 0;
        boolean valid = false;
        while (!valid) {
            System.out.print("Enter 'b': ");

            if (scanner.hasNextDouble()) {
                b = scanner.nextDouble();
                valid = true;
            } else {
                System.out.print("The input isn't valid. Try again...\n");
                scanner.next();
            }
        }
        return b;
    }

    public double chooseEpsilon() {
        double epsilon = 0;
        boolean valid = false;
        while (!valid) {
            System.out.print("Enter 'epsilon' (> 0): ");

            if (scanner.hasNextDouble()) {
                epsilon = scanner.nextDouble();
                if (epsilon > 0) {
                    valid = true;
                } else {
                    System.out.print("The input isn't valid. Try again...\n");
                }
            } else {
                System.out.print("The input isn't valid. Try again...\n");
                scanner.next();
            }
        }
        return epsilon;
    }
}