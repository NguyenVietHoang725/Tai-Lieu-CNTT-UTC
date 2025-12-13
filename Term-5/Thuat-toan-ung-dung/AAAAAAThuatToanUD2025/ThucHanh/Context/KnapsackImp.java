/******************************************************************************
 *  Compilation:  javac CopyOfKnapsack.java
 *  Execution:    java CopyOfKnapsack N W
 *
 *  Generates an instance of the 0/1 knapsack problem with N items
 *  and maximum weight W and solves it in time and space proportional
 *  to N * W using dynamic programming.
 *
 *  For testing, the inputs are generated at random with weights between 0
 *  and W, and profits between 0 and 1000.
 *
 *  %  java CopyOfKnapsack 6 2000 
 *  item    profit  weight  take
 *  1       874     580     true
 *  2       620     1616    false
 *  3       345     1906    false
 *  4       369     1942    false
 *  5       360     50      true
 *  6       470     294     true
 *
 ******************************************************************************/
import java.util.Arrays;
import java.util.List;
//import java.util.Iterator;
public class KnapsackImp {
    private int W;
    private int realweight; 
    private Bag<Item> bag = new Bag<Item>();  
    private int totalprofit; 
    
    public KnapsackImp(List<Item> items, int trongluong){
        int N =  items.size();   // number of items
        W =  trongluong;   // maximum weight of knapsack

        int[] profit = new int[N+1];
        int[] weight = new int[N+1];

        // generate random instance, items 1..N
        for (int n = 1; n <= N; n++) {
            profit[n] = items.get(n-1).profit();
            weight[n] = items.get(n-1).weight();
        }

        // opt[n][w] = max profit of packing items 1..n with weight limit w
        // sol[n][w] = does opt solution to pack items 1..n with weight limit w include item n?
        int[][] opt = new int[N+1][W+1];
        boolean[][] sol = new boolean[N+1][W+1];

        for (int n = 1; n <= N; n++) {
            for (int w = 1; w <= W; w++) {

                // don't take item n
                int option1 = opt[n-1][w];

                // take item n
                int option2 = Integer.MIN_VALUE;
                if (weight[n] <= w) option2 = profit[n] + opt[n-1][w-weight[n]];

                // select better of two options
                opt[n][w] = Math.max(option1, option2);
                sol[n][w] = (option2 > option1);
            }
        }

        // determine which items to take
        boolean[] take = new boolean[N+1];
        for (int n = N, w = W; n > 0; n--) {
            if (sol[n][w]) {
                take[n] = true;
                realweight += weight[n];
                totalprofit += profit[n];
                bag.add(items.get(n-1));
                w = w - weight[n];
            }
            else {
                take[n] = false;
            }
        }

        // print results
        //StdOut.println("item" + "\t" + "profit" + "\t" + "weight" + "\t" + "take");
        //for (int n = 1; n <= N; n++) {
        //    StdOut.println(n + "\t" + profit[n] + "\t" + weight[n] + "\t" + take[n]);
    }
    public int maxweight(){
        return W;
    }
    public int totalprofit(){
        return totalprofit;
    }
    public int realweight(){
        return realweight;
    }
    public Bag<Item> bag(){
        return bag;
    }
    
    
    public static void main(String[] args)
    {
        List<Item> items = Arrays.asList(   new Item("thuoc", 1, 1 ),
                                            new Item("vo", 6, 2 ),
                                            new Item("hop but", 18, 5 ),
                                            new Item("sach", 22, 6 ),
                                            new Item("ao", 28, 7 ));
        
        KnapsackImp balo =  new KnapsackImp(items, 11);     
        
        StdOut.println("Knapsack: " +  "profit: "  + balo.totalprofit() + "," + " maxweight: "  + balo.maxweight() + "," + " real weight: " + balo.realweight());
        StdOut.println("item" + "\t" + "profit" + "\t" + "weight");
        for (Item item: balo.bag()){
        StdOut.println( item.name() + "\t" + item.profit() + "\t" + item.weight());
        //    StdOut.println(n + "\t" + profit[n] + "\t" + weight[n] + "\t" + take[n]);
        }
    
    }

}
