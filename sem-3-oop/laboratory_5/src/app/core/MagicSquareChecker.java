package app.core;

public class MagicSquareChecker {

    public boolean isMagicSquare (int size, int[][] matrix) {
        int reference = 0;
        for (int i=0; i<size; i++) {
            reference += matrix[0][i];
        }

        for (int i=1; i<size; i++) {
            int sum = 0;
            for (int j=0; j<size; j++) {
                sum += matrix[i][j];
            }
            if (reference != sum) {
                return false;
            }
        }

        for (int j=0; j<size; j++) {
            int sum = 0;
            for (int i=0; i<size; i++) {
                sum += matrix[i][j];
            }
            if (reference != sum) {
                return false;
            }
        }

        int sum = 0;
        for (int i=0; i<size; i++) {
            sum += matrix[i][i];
        }
        if (reference != sum) {
            return false;
        }

        sum = 0;
        for (int i=size-1; i>=0; i--) {
            sum += matrix[i][i];
        }
        if (reference != sum) {
            return false;
        }

        return true;
    }
    
}
