
/**
 * Write a description of class KnapsackApp here.
 *
 * @author (your name)
 * @version (a version number or a date)
 */
import java.util.Arrays;
import java.util.List;
public class KnapsackApp
{
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
