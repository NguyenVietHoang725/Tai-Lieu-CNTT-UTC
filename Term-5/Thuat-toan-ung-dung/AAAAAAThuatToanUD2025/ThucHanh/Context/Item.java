
/**
 * Write a description of class Item here.
 *
 * @author (your name)
 * @version (a version number or a date)
 */
public class Item
{
    // instance variables - replace the example below with your own
    private String name;
    private int profit;
    private int weight;

    /**
     * Constructor for objects of class Item
     */
    public Item(String ten, int giatri, int khoiluong)
    {
       name = ten;
       profit = giatri;
       weight = khoiluong;
    }

    /**
     * An example of a method - replace this comment with your own
     *
     * @param  y  a sample parameter for a method
     * @return    the sum of x and y
     */
    public int profit() {
        // put your code here
        return profit;
    }
    public int weight() {
        // put your code here
        return weight;
    }
    public String name() {
        // put your code here
        return name;
    }
}
