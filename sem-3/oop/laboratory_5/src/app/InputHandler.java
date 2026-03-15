package app;

import java.io.InputStream;
import java.util.Scanner;

public class InputHandler {
    private Scanner scanner;

    public void printGreet() {
        System.out.print("\nOOP | lab 5 | variant 2 | Badulin Ilya | 424-1\n\n");
    }

    public int inputSize(InputStream inputStream) {
        System.out.print("Enter the size of the square matrix (>= 2): ");

        this.scanner = new Scanner(inputStream);
        int size = scanner.nextInt();

        if (size < 2) {
            System.out.print("Invalid size\nExiting programm...");
            System.exit(1);
        }

        return size;
    }

    public int[][] inputMatrix(InputStream inputStream, int size) {
        System.out.print("\nEnter the matrix values row by row\n");

        this.scanner = new Scanner(inputStream);
        int[][] matrix = new int[size][size];

        for (int i=0; i<size; i++) {
            System.out.printf("    matrix[%d]: ", i);

            for (int j=0; j<size; j++) {
                matrix[i][j] = scanner.nextInt();
            }
            scanner.nextLine();
        }

        return matrix;
    }

    public void printSquareMatrix(int size, int[][] matrix) {
        System.out.print("\nYour matrix:\n");

        for (int i=0; i<size; i++) {
            System.out.printf("    Row %d: ", i+1);

            for (int j=0; j<size; j++) {
                System.out.printf("%2d ", matrix[i][j]);
            }
            System.out.println();
        }
        System.out.println();
    }
}
