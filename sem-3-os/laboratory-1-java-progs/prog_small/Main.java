public class Main {

    public static void main(String[] args) {
        
        int sum = 0;


        System.out.printf("%nprog_small start:%n", sum);
        try {
            Thread.sleep(1000);
        } catch (InterruptedException e) {
            e.printStackTrace();
        }

        for (int i = 0; i < 20_000; i++) {
            sum += i;
            System.out.printf("sum == %d%n", sum);
        }
    }
}