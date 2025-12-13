
import java.util.*;

class IntervalST {
    
    // A utility function to create a new Interval1 Search Tree Node
    static Node newNode(Interval1 i) {
        Node temp = new Node();
        temp.i = new Interval1(i.low, i.high);
        temp.max = i.high;
        temp.left = temp.right = null;
        return temp;
    }
    
    // A utility function to insert a new Interval1 Search Tree Node
    // This is similar to BST Insert.  Here the low value of Interval1
    // is used tomaintain BST property
    static Node insert(Node root, Interval1 i) {
        
        // Base case: Tree is empty, new node becomes root
        if (root == null)
            return newNode(i);
            
        // Get low value of Interval1 at root
        int l = root.i.low;
        
        // If root's low value is smaller, 
        // then new Interval1 goes to left subtree
        if (i.low < l)
            root.left = insert(root.left, i);
            
        // Else, new node goes to right subtree.
        else
            root.right = insert(root.right, i);
            
        // Update the max value of this ancestor if needed
        if (root.max < i.high)
            root.max = i.high;
        return root;
    }
    
    // A utility function to check if given two Interval1s overlap
    static boolean isOverlapping(Interval1 i1, Interval1 i2) {
        if (i1.low <= i2.high && i2.low <= i1.high)
            return true;
        return false;
    }
    
    // The main function that searches a given 
    // Interval1 i in a given Interval1 Tree.
    static Interval1 overlapSearch(Node root, Interval1 i) {
        
        // Base Case, tree is empty
        if (root == null) return null;
        
        // If given Interval1 overlaps with root
        if (isOverlapping(root.i, i))
            return root.i;
            
        // If left child of root is present and max of left child is
        // greater than or equal to given Interval1, then i may
        // overlap with an Interval1 is left subtree
        if (root.left != null && root.left.max >= i.low)
            return overlapSearch(root.left, i);
            
        // Else Interval1 can only overlap with right subtree
        return overlapSearch(root.right, i);
    }
    
    static void inorder(Node root) {
        if (root == null) return;
        inorder(root.left);
        System.out.println("[" + root.i.low + ", " + root.i.high + "]" + " max = " + root.max);
        inorder(root.right);
    }
    
    public static void main(String[] args) {
        Interval1[] ints = { new Interval1(15, 20), new Interval1(10, 30), new Interval1(17, 19),
            new Interval1(5, 20), new Interval1(12, 15), new Interval1(30, 40)
        };
        
        int n = ints.length;
        
        Node root = null;
        for (int i = 0; i < n; i++)
            root = insert(root, ints[i]);
        System.out.println("Inorder traversal of constructed Interval1 Tree is");
        inorder(root);
        
        Interval1 x = new Interval1(6, 7);
        System.out.print("\nSearching for Interval1 [" + x.low + "," + x.high + "]");
        
        Interval1 res = overlapSearch(root, x);
        if (res == null)
            System.out.println("\nNo Overlapping Interval1");
        else
            System.out.println("\nOverlaps with [" + res.low + ", " + res.high + "]");
    }
}