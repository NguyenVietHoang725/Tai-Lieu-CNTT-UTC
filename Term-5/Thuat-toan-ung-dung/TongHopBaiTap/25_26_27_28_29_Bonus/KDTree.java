// Java Program to Construct K-D Tree
import java.util.Arrays;

class KDTree {
    // Dimension of the space
    private static final int K = 2;

    static class Node {
        int[] point; // Array to store k dimensional point
        Node left, right; // Left and right child

        public Node(int[] arr)
        {
            // Copy the point array
            this.point = Arrays.copyOf(arr, K);
            // Initialize children
            this.left = this.right = null;
        }
    }

    Node root; // Root of the K-D Tree

    KDTree()
    {
        root = null; // Initialize the root
    }

    // Recursive method to insert a new point in the subtree
    // rooted with the given node
    Node insertRec(Node root, int[] point, int depth)
    {
        // Base case: If the tree is empty, return a new
        // node
        if (root == null) {
            return new Node(point);
        }

        // Calculate current dimension (cd) of comparison
        int cd = depth % K;

        // Compare the new point with the root on current
        // dimension and decide the left or right subtree
        if (point[cd] < root.point[cd]) {
            root.left
                = insertRec(root.left, point, depth + 1);
        }
        else {
            root.right
                = insertRec(root.right, point, depth + 1);
        }

        return root;
    }

    // Method to insert a new point in the K-D Tree
    void insert(int[] point)
    {
        root = insertRec(root, point, 0);
    }

    // Recursive method to search a point in the subtree
    // rooted with the given node
    boolean searchRec(Node root, int[] point, int depth)
    {
        // Base case: The tree is empty or the point is
        // present at root
        if (root == null) {
            return false;
        }
        if (Arrays.equals(root.point, point)) {
            return true;
        }

        // Calculate current dimension (cd)
        int cd = depth % K;

        // Compare the point with the root on current
        // dimension and decide the left or right subtree
        if (point[cd] < root.point[cd]) {
            return searchRec(root.left, point, depth + 1);
        }
        else {
            return searchRec(root.right, point, depth + 1);
        }
    }

    // Method to search a point in the K-D Tree
    boolean search(int[] point)
    {
        return searchRec(root, point, 0);
    }

    // Recursive method for range search in the subtree
    // rooted with the given node
    void rangeSearchRec(Node root, int[] lower, int[] upper,
                        int depth)
    {
        if (root == null) {
            return;
        }

        // Check if the point of root is in range
        boolean inside = true;
        for (int i = 0; i < K; i++) {
            if (root.point[i] < lower[i]
                || root.point[i] > upper[i]) {
                inside = false;
                break;
            }
        }

        // If the point is in range, print it
        if (inside) {
            System.out.println(Arrays.toString(root.point));
        }

        // Calculate current dimension (cd)
        int cd = depth % K;

        // Check subtrees if they can have points within the
        // range
        if (lower[cd] <= root.point[cd]) {
            rangeSearchRec(root.left, lower, upper,
                           depth + 1);
        }
        if (upper[cd] >= root.point[cd]) {
            rangeSearchRec(root.right, lower, upper,
                           depth + 1);
        }
    }

    // Method for range search
    void rangeSearch(int[] lower, int[] upper)
    {
        rangeSearchRec(root, lower, upper, 0);
    }

    public static void main(String[] args)
    {
        KDTree tree = new KDTree();

        int[][] points
            = { { 3, 6 }, { 17, 15 }, { 13, 15 }, { 6, 12 },
                { 9, 1 }, { 2, 7 },   { 10, 19 } };

        // Insert points into the K-D Tree
        for (int[] point : points) {
            tree.insert(point);
        }

        // Search for specific points in the K-D Tree
        System.out.println(
            tree.search(new int[] { 10, 19 })); // true
        System.out.println(
            tree.search(new int[] { 12, 19 })); // false

        // Range search for points within the specified
        // range
        int[] lower = { 0, 0 };
        int[] upper = { 10, 10 };

        // Print points within range
        tree.rangeSearch(lower, upper);
               /* tree.rangeSearch(lower, upper);*/
    }
}