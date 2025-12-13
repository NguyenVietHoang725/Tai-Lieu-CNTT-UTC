/******************************************************************************
 * Compilation:  javac TwoDTree.java
 * Execution:    java TwoDTree
 * Dependencies: Point2D.java, Interval1D.java, Interval2D.java, Queue.java
 *
 * Bài tập 27: 2D-Tree với Range Search và Nearest Neighbor
 ******************************************************************************/

public class TwoDTree {
    
    private Node root;
    private int size;
    
    // Node của 2D-Tree
    private class Node {
        Point2D point;      // Điểm tại node
        Node left, right;   // Con trái, con phải
        boolean isVertical; // true: chia theo x, false: chia theo y
        
        public Node(Point2D point, boolean isVertical) {
            this.point = point;
            this.isVertical = isVertical;
        }
    }
    
    public TwoDTree() {
        root = null;
        size = 0;
    }
    
    public boolean isEmpty() {
        return size == 0;
    }
    
    public int size() {
        return size;
    }
    
    /**
     * Thêm một điểm vào 2D-Tree
     */
    public void insert(Point2D p) {
        if (p == null) throw new IllegalArgumentException("Point cannot be null");
        root = insert(root, p, true);
    }
    
    private Node insert(Node node, Point2D p, boolean isVertical) {
        if (node == null) {
            size++;
            return new Node(p, isVertical);
        }
        
        // Kiểm tra trùng điểm
        if (node.point.equals(p)) return node;
        
        // So sánh theo chiều hiện tại
        int cmp;
        if (node.isVertical) {
            cmp = Double.compare(p.x(), node.point.x());
        } else {
            cmp = Double.compare(p.y(), node.point.y());
        }
        
        // Chèn vào cây con trái hoặc phải
        if (cmp < 0) {
            node.left = insert(node.left, p, !node.isVertical);
        } else {
            node.right = insert(node.right, p, !node.isVertical);
        }
        
        return node;
    }
    
    /**
     * PHẦN 1: Range Search - Tìm các điểm trong hình chữ nhật
     * @param rect hình chữ nhật tìm kiếm (a <= x <= b, c <= y <= d)
     * @return danh sách các điểm nằm trong hình chữ nhật
     */
    public Queue<Point2D> rangeSearch(Interval2D rect) {
        if (rect == null) throw new IllegalArgumentException("Rectangle cannot be null");
        
        Queue<Point2D> queue = new Queue<>();
        rangeSearch(root, rect, queue);
        return queue;
    }
    
    private void rangeSearch(Node node, Interval2D rect, Queue<Point2D> queue) {
        if (node == null) return;
        
        // Kiểm tra điểm hiện tại có trong hình chữ nhật không
        if (rect.contains(node.point)) {
            queue.enqueue(node.point);
        }
        
        // Quyết định có duyệt cây con trái không
        if (node.isVertical) {
            // Chia theo x: cây trái có x < node.x
            // Chỉ duyệt trái nếu rect có thể chứa điểm có x < node.x
            if (node.left != null) {
                Interval1D xInterval = getXInterval(rect);
                if (xInterval.min() < node.point.x()) {
                    rangeSearch(node.left, rect, queue);
                }
            }
            // Duyệt phải nếu rect có thể chứa điểm có x >= node.x
            if (node.right != null) {
                Interval1D xInterval = getXInterval(rect);
                if (xInterval.max() >= node.point.x()) {
                    rangeSearch(node.right, rect, queue);
                }
            }
        } else {
            // Chia theo y: cây trái có y < node.y
            if (node.left != null) {
                Interval1D yInterval = getYInterval(rect);
                if (yInterval.min() < node.point.y()) {
                    rangeSearch(node.left, rect, queue);
                }
            }
            if (node.right != null) {
                Interval1D yInterval = getYInterval(rect);
                if (yInterval.max() >= node.point.y()) {
                    rangeSearch(node.right, rect, queue);
                }
            }
        }
    }
    
    // Helper method để lấy x interval từ Interval2D
    private Interval1D getXInterval(Interval2D rect) {
        String str = rect.toString();
        String xPart = str.split(" x ")[0];
        String[] bounds = xPart.replace("[", "").replace("]", "").split(", ");
        return new Interval1D(Double.parseDouble(bounds[0]), Double.parseDouble(bounds[1]));
    }
    
    // Helper method để lấy y interval từ Interval2D
    private Interval1D getYInterval(Interval2D rect) {
        String str = rect.toString();
        String yPart = str.split(" x ")[1];
        String[] bounds = yPart.replace("[", "").replace("]", "").split(", ");
        return new Interval1D(Double.parseDouble(bounds[0]), Double.parseDouble(bounds[1]));
    }
    
    /**
     * PHẦN 2: Nearest Neighbor - Tìm điểm gần nhất
     * @param queryPoint điểm cần tìm láng giềng gần nhất
     * @return điểm gần nhất trong cây
     */
    public Point2D nearest(Point2D queryPoint) {
        if (queryPoint == null) throw new IllegalArgumentException("Query point cannot be null");
        if (isEmpty()) return null;
        
        return nearest(root, queryPoint, root.point);
    }
    
    private Point2D nearest(Node node, Point2D query, Point2D champion) {
        if (node == null) return champion;
        
        // Cập nhật champion nếu tìm thấy điểm gần hơn
        double champDist = query.distanceSquaredTo(champion);
        double currDist = query.distanceSquaredTo(node.point);
        
        if (currDist < champDist) {
            champion = node.point;
            champDist = currDist;
        }
        
        // Xác định nên tìm cây con nào trước (cây gần query hơn)
        Node first, second;
        if (node.isVertical) {
            if (query.x() < node.point.x()) {
                first = node.left;
                second = node.right;
            } else {
                first = node.right;
                second = node.left;
            }
        } else {
            if (query.y() < node.point.y()) {
                first = node.left;
                second = node.right;
            } else {
                first = node.right;
                second = node.left;
            }
        }
        
        // Tìm trong cây con gần hơn trước
        champion = nearest(first, query, champion);
        champDist = query.distanceSquaredTo(champion);
        
        // Chỉ tìm cây con xa hơn nếu có khả năng chứa điểm gần hơn (pruning)
        double splitDist;
        if (node.isVertical) {
            splitDist = Math.pow(query.x() - node.point.x(), 2);
        } else {
            splitDist = Math.pow(query.y() - node.point.y(), 2);
        }
        
        if (splitDist < champDist) {
            champion = nearest(second, query, champion);
        }
        
        return champion;
    }
    
    /**
     * In tất cả các điểm trong cây
     */
    public void printTree() {
        System.out.println("Cấu trúc 2D-Tree:");
        printTree(root, "", true);
    }
    
    private void printTree(Node node, String prefix, boolean isTail) {
        if (node == null) return;
        
        System.out.println(prefix + (isTail ? "└── " : "├── ") + 
                          node.point + " [" + (node.isVertical ? "X-split" : "Y-split") + "]");
        
        if (node.left != null || node.right != null) {
            if (node.left != null) {
                printTree(node.left, prefix + (isTail ? "    " : "│   "), node.right == null);
            }
            if (node.right != null) {
                printTree(node.right, prefix + (isTail ? "    " : "│   "), true);
            }
        }
    }
    
    // Hàm main để test
    public static void main(String[] args) {
        System.out.println("=== BÀI TẬP 27: 2D-TREE ===\n");
        
        TwoDTree tree = new TwoDTree();
        
        // Thêm các điểm
        Point2D[] points = {
            new Point2D(7, 2),
            new Point2D(5, 4),
            new Point2D(2, 3),
            new Point2D(4, 7),
            new Point2D(9, 6),
            new Point2D(3, 1),
            new Point2D(8, 5)
        };
        
        System.out.println("Thêm điểm vào 2D-Tree:");
        for (int i = 0; i < points.length; i++) {
            tree.insert(points[i]);
            System.out.println("  " + (i+1) + ". " + points[i]);
        }
        System.out.println("\nTổng số điểm: " + tree.size());
        
        System.out.println();
        tree.printTree();
        
        // TEST 1: Range Search
        System.out.println("\n=== TEST 1: RANGE SEARCH ===");
        Interval1D xInterval = new Interval1D(2.0, 6.0);
        Interval1D yInterval = new Interval1D(1.0, 5.0);
        Interval2D rect = new Interval2D(xInterval, yInterval);
        
        System.out.println("Hình chữ nhật: x " + xInterval + ", y " + yInterval);
        Queue<Point2D> inRect = tree.rangeSearch(rect);
        
        System.out.println("Các điểm trong hình chữ nhật (" + inRect.size() + " điểm):");
        for (Point2D p : inRect) {
            System.out.println("  " + p);
        }
        
        // TEST 2: Nearest Neighbor
        System.out.println("\n=== TEST 2: NEAREST NEIGHBOR ===");
        Point2D queryPoint = new Point2D(6.0, 3.0);
        System.out.println("Điểm truy vấn: " + queryPoint);
        
        Point2D nearest = tree.nearest(queryPoint);
        System.out.println("Điểm gần nhất: " + nearest);
        System.out.println("Khoảng cách: " + String.format("%.3f", queryPoint.distanceTo(nearest)));
        
        // TEST 3: Nearest với điểm xa
        System.out.println("\n=== TEST 3: NEAREST (điểm xa) ===");
        Point2D queryPoint2 = new Point2D(10.0, 8.0);
        System.out.println("Điểm truy vấn: " + queryPoint2);
        
        Point2D nearest2 = tree.nearest(queryPoint2);
        System.out.println("Điểm gần nhất: " + nearest2);
        System.out.println("Khoảng cách: " + String.format("%.3f", queryPoint2.distanceTo(nearest2)));
        
        // So sánh với tất cả điểm
        System.out.println("\nKiểm tra tất cả khoảng cách:");
        for (Point2D p : points) {
            double dist = queryPoint2.distanceTo(p);
            String marker = p.equals(nearest2) ? " <-- NEAREST" : "";
            System.out.println("  " + p + ": " + String.format("%.3f", dist) + marker);
        }
    }
}