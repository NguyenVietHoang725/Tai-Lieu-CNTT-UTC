import java.util.*;

class KnapsackSolver {
    public static boolean[] solve(List<Item> items, int maxWeight) {
        int N = items.size();
        int[][] dp = new int[N + 1][maxWeight + 1];
        boolean[][] sol = new boolean[N + 1][maxWeight + 1];

        for (int n = 1; n <= N; n++) {
            Item item = items.get(n - 1);
            for (int w = 1; w <= maxWeight; w++) {
                int option1 = dp[n - 1][w];
                int option2 = Integer.MIN_VALUE;
                if (item.weight <= w) {
                    option2 = item.profit + dp[n - 1][w - item.weight];
                }

                dp[n][w] = Math.max(option1, option2);
                sol[n][w] = option2 > option1;
            }
        }

        boolean[] take = new boolean[N + 1];
        for (int n = N, w = maxWeight; n > 0; n--) {
            if (sol[n][w]) {
                take[n] = true;
                w -= items.get(n - 1).weight;
            } else {
                take[n] = false;
            }
        }
        return take;
    }

    public static void printResult(List<Item> items, boolean[] take) {
        System.out.printf("%-6s%-9s%-9s%-6s\n", "item", "profit", "weight", "take");
        for (int i = 1; i <= items.size(); i++) {
            Item item = items.get(i - 1);
            System.out.printf("%-6d%-9d%-9d%-6b\n", item.id, item.profit, item.weight, take[i]);
        }
    }
}