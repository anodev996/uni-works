/* run command
 * javac IsSymmetric.java && java IsSymmetric
 */

import java.util.Scanner;

public class IsSymmetric {
    public static void main(String[] args) {

        //welcome message
        System.out.println("Please enter [positive integer 4 digit number]...");

        //create Scanner obj
        Scanner input = new Scanner(System.in);
        //try to read an int number from input stream
        //if exception occured - exit the program
        int intNum = 0;
        try {
            intNum = input.nextInt();
        } catch (Exception e) {
            System.out.println("An error occured, exiting the program...");
            input.close();
            System.exit(1);
        }
        input.close();

        //check number if it has four digits and is pairwise symmetrical
        if (intNum < 1000 || intNum > 9999) {
            System.out.println("An error occured, exiting the program...");
            System.exit(1);
        } else if (intNum/100 != intNum%100) {
            System.out.println("The entered number IS NOT pairwise symmetrical");
        } else {
            System.out.println("The entered number IS pairwise symmetrical");
        }
    }
}