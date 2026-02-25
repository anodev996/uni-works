package app;

import app.core.MagicSquareChecker;

public class Main {
    public static void main(String[] args) {
        InputHandler inputHandler = new InputHandler();
        inputHandler.printGreet();

        int size = inputHandler.inputSize(System.in);
        int[][] matrix = inputHandler.inputMatrix(System.in, size);
        inputHandler.printSquareMatrix(size, matrix);

        MagicSquareChecker magicSquareChecker = new MagicSquareChecker();
        if (magicSquareChecker.isMagicSquare(size, matrix) == true) {
            System.out.print("Is this matrix a magic square? - YES");
        } else {
            System.out.print("Is this matrix a magic square? - NO");
        }
    }
}
