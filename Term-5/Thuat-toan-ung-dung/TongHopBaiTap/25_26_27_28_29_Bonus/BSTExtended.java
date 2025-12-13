/******************************************************************************
 * Compilation:  javac BSTExtended.java
 * Execution:    java BSTExtended
 * Dependencies: StdOut.java, Queue.java
 *
 * Mô tả: Mở rộng BST với các phương thức tìm kiếm theo phạm vi (Range Search).
 ******************************************************************************/

import java.util.NoSuchElementException;

public class BSTExtended<Key extends Comparable<Key>, Value> {
    protected Node root;

    protected class Node {
        private Key key;
        protected Value val;
        private Node left, right;
        private int size;

        public Node(Key key, Value val, int size) {
            this.key = key;
            this.val = val;
            this.size = size;
        }
    }

    public BSTExtended() {
    }

    public boolean isEmpty() {
        return size() == 0;
    }

    public int size() {
        return size(root);
    }

    private int size(Node x) {
        if (x == null) return 0;
        else return x.size;
    }

    public void put(Key key, Value val) {
        if (key == null) throw new IllegalArgumentException("calls put() with a null key");
        root = put(root, key, val);
    }

    private Node put(Node x, Key key, Value val) {
        if (x == null) return new Node(key, val, 1);
        int cmp = key.compareTo(x.key);
        if      (cmp < 0) x.left  = put(x.left,  key, val);
        else if (cmp > 0) x.right = put(x.right, key, val);
        else              x.val   = val;
        x.size = 1 + size(x.left) + size(x.right);
        return x;
    }

    public Value get(Key key) {
        return get(root, key);
    }

    private Value get(Node x, Key key) {
        if (key == null) throw new IllegalArgumentException("calls get() with a null key");
        if (x == null) return null;
        int cmp = key.compareTo(x.key);
        if      (cmp < 0) return get(x.left, key);
        else if (cmp > 0) return get(x.right, key);
        else              return x.val;
    }

    // ===========================================================================
    // BÀI TẬP 25: RANGE SEARCH
    // ===========================================================================

    /**
     * 1. size(Key lo, Key hi) - Đếm số phần tử trong khoảng [lo, hi]
     */
    public int size(Key lo, Key hi) {
        if (lo == null || hi == null) 
            throw new IllegalArgumentException("arguments cannot be null");
        
        Queue<Node> queue = search(root, lo, hi);
        return queue.size();
    }

    /**
     * 2. search(Key lo, Key hi) - Trả về Queue<Node> chứa các node trong [lo, hi]
     * Phương thức public
     */
    public Queue<Node> search(Key lo, Key hi) {
        if (lo == null || hi == null) 
            throw new IllegalArgumentException("arguments cannot be null");
        
        return search(root, lo, hi);
    }

    /**
     * Phương thức đệ quy bổ trợ: search(Node x, Key lo, Key hi)
     * - Đệ quy kiểm tra cây con trái, đưa vào queue
     * - Lấy gốc kiểm tra, nếu thỏa mãn đưa vào queue
     * - Đệ quy kiểm tra cây con phải, đưa vào queue
     */
    private Queue<Node> search(Node x, Key lo, Key hi) {
        Queue<Node> queue = new Queue<Node>();
        
        if (x == null) return queue;
        
        int cmplo = lo.compareTo(x.key);
        int cmphi = hi.compareTo(x.key);
        
        // 1. Đệ quy cây con trái (nếu lo < x.key)
        if (cmplo < 0) {
            Queue<Node> leftQueue = search(x.left, lo, hi);
            // Chuyển tất cả node từ leftQueue vào queue
            while (!leftQueue.isEmpty()) {
                queue.enqueue(leftQueue.dequeue());
            }
        }
        
        // 2. Kiểm tra node gốc (nếu lo <= x.key <= hi)
        if (cmplo <= 0 && cmphi >= 0) {
            queue.enqueue(x);
        }
        
        // 3. Đệ quy cây con phải (nếu hi > x.key)
        if (cmphi > 0) {
            Queue<Node> rightQueue = search(x.right, lo, hi);
            // Chuyển tất cả node từ rightQueue vào queue
            while (!rightQueue.isEmpty()) {
                queue.enqueue(rightQueue.dequeue());
            }
        }
        
        return queue;
    }

    // ===========================================================================
    // HÀM MAIN KIỂM TRA
    // ===========================================================================
    public static void main(String[] args) {
        BSTExtended<String, Integer> bst = new BSTExtended<>();
        
        // Tạo cây BST
        String[] keys = {"S", "E", "A", "R", "C", "H", "X", "M", "P", "L"};
        for (int i = 0; i < keys.length; i++) {
            bst.put(keys[i], i);
        }
        
        // Test 1: size(lo, hi)
        String lo = "E";
        String hi = "P";
        System.out.println("TEST 1: size(\"" + lo + "\", \"" + hi + "\")");
        System.out.println("Kết quả: " + bst.size(lo, hi) + " phần tử\n");
        
        // Test 2: search(lo, hi)
        System.out.println("TEST 2: search(\"" + lo + "\", \"" + hi + "\")");
        Queue<BSTExtended<String, Integer>.Node> result = bst.search(lo, hi);
        
        System.out.print("Các node trong khoảng [" + lo + ", " + hi + "]: ");
        for (BSTExtended<String, Integer>.Node node : result) {
            System.out.print(node.key + " ");
        }
        System.out.println("\n");
        
        // Test 3: Khoảng khác
        lo = "A";
        hi = "M";
        System.out.println("TEST 3: search(\"" + lo + "\", \"" + hi + "\")");
        result = bst.search(lo, hi);
        
        System.out.print("Các node trong khoảng [" + lo + ", " + hi + "]: ");
        for (BSTExtended<String, Integer>.Node node : result) {
            System.out.print(node.key + " ");
        }
        System.out.println();
    }
}