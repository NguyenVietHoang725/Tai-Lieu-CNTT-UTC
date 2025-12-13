/******************************************************************************
 * Compilation:  javac IntervalSearchTree.java
 * Execution:    java IntervalSearchTree
 * Dependencies: Queue.java, Interval1D.java
 *
 * Bài tập 28: Interval Search Tree với intersect search
 * Cây tìm kiếm khoảng dựa trên BST
 ******************************************************************************/

import java.util.NoSuchElementException;

public class IntervalSearchTree<Value> {
    
    private Node root;  // Gốc của cây
    
    // Node của Interval Search Tree
    private class Node {
        Interval1D interval;  // Khoảng [lo, hi]
        Value value;          // Giá trị liên kết
        Node left, right;     // Con trái, con phải
        int size;             // Số node trong cây con
        double max;           // Giá trị max lớn nhất trong cây con
        
        public Node(Interval1D interval, Value value) {
            this.interval = interval;
            this.value = value;
            this.size = 1;
            this.max = interval.max();
        }
    }
    
    /**
     * Khởi tạo cây rỗng
     */
    public IntervalSearchTree() {
        root = null;
    }
    
    /**
     * Kiểm tra cây rỗng
     */
    public boolean isEmpty() {
        return root == null;
    }
    
    /**
     * Trả về số lượng khoảng trong cây
     */
    public int size() {
        return size(root);
    }
    
    private int size(Node x) {
        if (x == null) return 0;
        return x.size;
    }
    
    /**
     * PUT - Thêm khoảng vào cây
     * @param interval khoảng cần thêm
     * @param value giá trị liên kết
     */
    public void put(Interval1D interval, Value value) {
        if (interval == null) throw new IllegalArgumentException("Interval cannot be null");
        if (value == null) {
            delete(interval);
            return;
        }
        root = put(root, interval, value);
    }
    
    private Node put(Node x, Interval1D interval, Value value) {
        if (x == null) return new Node(interval, value);
        
        int cmp = interval.min() < x.interval.min() ? -1 : 
                  interval.min() > x.interval.min() ? 1 : 0;
        
        if (cmp < 0) {
            x.left = put(x.left, interval, value);
        } else if (cmp > 0) {
            x.right = put(x.right, interval, value);
        } else {
            // Cùng điểm bắt đầu, so sánh điểm kết thúc
            if (interval.max() < x.interval.max()) {
                x.left = put(x.left, interval, value);
            } else if (interval.max() > x.interval.max()) {
                x.right = put(x.right, interval, value);
            } else {
                // Khoảng trùng nhau, cập nhật value
                x.value = value;
            }
        }
        
        // Cập nhật size và max
        x.size = 1 + size(x.left) + size(x.right);
        x.max = max3(x.interval.max(), 
                     x.left == null ? Double.NEGATIVE_INFINITY : x.left.max,
                     x.right == null ? Double.NEGATIVE_INFINITY : x.right.max);
        
        return x;
    }
    
    private double max3(double a, double b, double c) {
        return Math.max(a, Math.max(b, c));
    }
    
    /**
     * GET - Lấy giá trị của khoảng
     * @param interval khoảng cần tìm
     * @return giá trị hoặc null nếu không tìm thấy
     */
    public Value get(Interval1D interval) {
        if (interval == null) throw new IllegalArgumentException("Interval cannot be null");
        return get(root, interval);
    }
    
    private Value get(Node x, Interval1D interval) {
        if (x == null) return null;
        
        if (x.interval.equals(interval)) return x.value;
        
        int cmp = interval.min() < x.interval.min() ? -1 : 
                  interval.min() > x.interval.min() ? 1 : 0;
        
        if (cmp < 0) {
            return get(x.left, interval);
        } else if (cmp > 0) {
            return get(x.right, interval);
        } else {
            // Cùng min, so sánh max
            if (interval.max() < x.interval.max()) {
                return get(x.left, interval);
            } else if (interval.max() > x.interval.max()) {
                return get(x.right, interval);
            } else {
                return x.value;
            }
        }
    }
    
    /**
     * DELETE - Xóa khoảng khỏi cây
     */
    public void delete(Interval1D interval) {
        if (interval == null) throw new IllegalArgumentException("Interval cannot be null");
        root = delete(root, interval);
    }
    
    private Node delete(Node x, Interval1D interval) {
        if (x == null) return null;
        
        if (x.interval.equals(interval)) {
            // Tìm thấy node cần xóa
            if (x.left == null) return x.right;
            if (x.right == null) return x.left;
            
            // Node có 2 con: thay bằng min của cây con phải
            Node t = x;
            x = min(t.right);
            x.right = deleteMin(t.right);
            x.left = t.left;
        } else {
            int cmp = interval.min() < x.interval.min() ? -1 : 
                      interval.min() > x.interval.min() ? 1 : 0;
            
            if (cmp < 0) {
                x.left = delete(x.left, interval);
            } else if (cmp > 0) {
                x.right = delete(x.right, interval);
            } else {
                if (interval.max() < x.interval.max()) {
                    x.left = delete(x.left, interval);
                } else {
                    x.right = delete(x.right, interval);
                }
            }
        }
        
        x.size = 1 + size(x.left) + size(x.right);
        x.max = max3(x.interval.max(), 
                     x.left == null ? Double.NEGATIVE_INFINITY : x.left.max,
                     x.right == null ? Double.NEGATIVE_INFINITY : x.right.max);
        return x;
    }
    
    private Node min(Node x) {
        if (x.left == null) return x;
        return min(x.left);
    }
    
    private Node deleteMin(Node x) {
        if (x.left == null) return x.right;
        x.left = deleteMin(x.left);
        x.size = 1 + size(x.left) + size(x.right);
        x.max = max3(x.interval.max(), 
                     x.left == null ? Double.NEGATIVE_INFINITY : x.left.max,
                     x.right == null ? Double.NEGATIVE_INFINITY : x.right.max);
        return x;
    }
    
    /**
     * INTERSECT - Tìm TẤT CẢ các khoảng giao với khoảng cho trước
     * @param lo điểm bắt đầu
     * @param hi điểm kết thúc
     * @return queue chứa tất cả các khoảng giao với [lo, hi]
     */
    public Queue<Interval1D> intersect(double lo, double hi) {
        Interval1D interval = new Interval1D(lo, hi);
        Queue<Interval1D> queue = new Queue<>();
        intersect(root, interval, queue);
        return queue;
    }
    
    /**
     * INTERSECT - Tìm TẤT CẢ các khoảng giao với khoảng cho trước
     * @param interval khoảng tìm kiếm
     * @return queue chứa tất cả các khoảng giao với interval
     */
    public Queue<Interval1D> intersect(Interval1D interval) {
        if (interval == null) throw new IllegalArgumentException("Interval cannot be null");
        Queue<Interval1D> queue = new Queue<>();
        intersect(root, interval, queue);
        return queue;
    }
    
    /**
     * Hàm đệ quy bổ trợ tìm tất cả khoảng giao nhau
     */
    private void intersect(Node x, Interval1D interval, Queue<Interval1D> queue) {
        if (x == null) return;
        
        // Kiểm tra node hiện tại có giao với interval không
        if (x.interval.intersects(interval)) {
            queue.enqueue(x.interval);
        }
        
        // Duyệt cây con trái nếu có khả năng chứa khoảng giao
        // Cây trái có khả năng giao nếu max của cây trái >= interval.min
        if (x.left != null && x.left.max >= interval.min()) {
            intersect(x.left, interval, queue);
        }
        
        // Duyệt cây con phải nếu có khả năng chứa khoảng giao
        // Cây phải luôn có khả năng giao nếu interval.max >= x.interval.min
        if (x.right != null && interval.max() >= x.interval.min()) {
            intersect(x.right, interval, queue);
        }
    }
    
    /**
     * SEARCH - Tìm MỘT khoảng giao với khoảng cho trước (thuật toán gốc)
     * @param interval khoảng tìm kiếm
     * @return một khoảng giao hoặc null
     */
    public Interval1D search(Interval1D interval) {
        if (interval == null) throw new IllegalArgumentException("Interval cannot be null");
        return search(root, interval);
    }
    
    private Interval1D search(Node x, Interval1D interval) {
        if (x == null) return null;
        
        if (x.interval.intersects(interval)) {
            return x.interval;
        }
        
        // Nếu cây trái có thể chứa khoảng giao
        if (x.left != null && x.left.max >= interval.min()) {
            return search(x.left, interval);
        }
        
        // Ngược lại tìm cây phải
        return search(x.right, interval);
    }
    
    /**
     * Trả về tất cả các khoảng trong cây theo thứ tự
     */
    public Queue<Interval1D> intervals() {
        Queue<Interval1D> queue = new Queue<>();
        intervals(root, queue);
        return queue;
    }
    
    private void intervals(Node x, Queue<Interval1D> queue) {
        if (x == null) return;
        intervals(x.left, queue);
        queue.enqueue(x.interval);
        intervals(x.right, queue);
    }
    
    /**
     * In cấu trúc cây
     */
    public void printTree() {
        System.out.println("Cấu trúc Interval Search Tree:");
        printTree(root, "", true);
    }
    
    private void printTree(Node x, String prefix, boolean isTail) {
        if (x == null) return;
        
        System.out.println(prefix + (isTail ? "└── " : "├── ") + 
                          x.interval + " (max=" + x.max + ")");
        
        if (x.left != null || x.right != null) {
            if (x.left != null) {
                printTree(x.left, prefix + (isTail ? "    " : "│   "), x.right == null);
            }
            if (x.right != null) {
                printTree(x.right, prefix + (isTail ? "    " : "│   "), true);
            }
        }
    }
    
    // Main để test
    public static void main(String[] args) {
        System.out.println("=== BÀI TẬP 28: INTERVAL SEARCH TREE ===\n");
        
        IntervalSearchTree<String> ist = new IntervalSearchTree<>();
        
        // Thêm các khoảng
        System.out.println("Thêm các khoảng vào cây:");
        ist.put(new Interval1D(15, 20), "A");
        ist.put(new Interval1D(10, 30), "B");
        ist.put(new Interval1D(17, 19), "C");
        ist.put(new Interval1D(5, 20), "D");
        ist.put(new Interval1D(12, 15), "E");
        ist.put(new Interval1D(30, 40), "F");
        ist.put(new Interval1D(8, 12), "G");
        
        Queue<Interval1D> allIntervals = ist.intervals();
        int count = 1;
        for (Interval1D interval : allIntervals) {
            System.out.println("  " + count++ + ". " + interval + 
                             " -> " + ist.get(interval));
        }
        
        System.out.println("\nTổng số khoảng: " + ist.size());
        System.out.println();
        ist.printTree();
        
        // TEST 1: Tìm tất cả khoảng giao với [14, 16]
        System.out.println("\n=== TEST 1: intersect(14, 16) ===");
        Queue<Interval1D> result1 = ist.intersect(14, 16);
        System.out.println("Khoảng tìm kiếm: [14.0, 16.0]");
        System.out.println("Các khoảng giao nhau (" + result1.size() + " khoảng):");
        for (Interval1D interval : result1) {
            System.out.println("  " + interval + " -> " + ist.get(interval));
        }
        
        // TEST 2: Tìm tất cả khoảng giao với [6, 7]
        System.out.println("\n=== TEST 2: intersect(6, 7) ===");
        Queue<Interval1D> result2 = ist.intersect(6, 7);
        System.out.println("Khoảng tìm kiếm: [6.0, 7.0]");
        System.out.println("Các khoảng giao nhau (" + result2.size() + " khoảng):");
        for (Interval1D interval : result2) {
            System.out.println("  " + interval + " -> " + ist.get(interval));
        }
        
        // TEST 3: Tìm tất cả khoảng giao với [22, 25]
        System.out.println("\n=== TEST 3: intersect(22, 25) ===");
        Queue<Interval1D> result3 = ist.intersect(22, 25);
        System.out.println("Khoảng tìm kiếm: [22.0, 25.0]");
        System.out.println("Các khoảng giao nhau (" + result3.size() + " khoảng):");
        for (Interval1D interval : result3) {
            System.out.println("  " + interval + " -> " + ist.get(interval));
        }
        
        // TEST 4: Tìm một khoảng giao (search)
        System.out.println("\n=== TEST 4: search([6, 7]) ===");
        Interval1D query = new Interval1D(6, 7);
        Interval1D found = ist.search(query);
        if (found != null) {
            System.out.println("Tìm thấy một khoảng giao: " + found);
        } else {
            System.out.println("Không tìm thấy khoảng giao");
        }
        
        // TEST 5: Get và Delete
        System.out.println("\n=== TEST 5: GET và DELETE ===");
        Interval1D testInterval = new Interval1D(12, 15);
        System.out.println("get(" + testInterval + ") = " + ist.get(testInterval));
        
        System.out.println("\nXóa khoảng " + testInterval);
        ist.delete(testInterval);
        System.out.println("Số khoảng còn lại: " + ist.size());
        System.out.println("get(" + testInterval + ") = " + ist.get(testInterval));
    }
}