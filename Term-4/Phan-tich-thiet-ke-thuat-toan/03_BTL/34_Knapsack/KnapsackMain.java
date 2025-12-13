import java.util.*;

public class KnapsackMain {
    public static void main(String[] args) {
        if (args.length != 2) {
            System.out.println("Usage: java KnapsackMain <number_of_items> <max_weight>");
            return;
        }

        int N = Integer.parseInt(args[0]);
        int W = Integer.parseInt(args[1]);

        Random rand = new Random();
        List<Item> items = new ArrayList<>();

        for (int i = 1; i <= N; i++) {
            int profit = rand.nextInt(1001); // 0..1000
            int weight = 1 + rand.nextInt(W); // 1..W
            items.add(new Item(i, profit, weight));
        }

        boolean[] take = KnapsackSolver.solve(items, W);
        KnapsackSolver.printResult(items, take);
    }
}