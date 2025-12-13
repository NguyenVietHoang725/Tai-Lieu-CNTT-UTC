/******************************************************************************
 * Compilation:  javac LineIntersection.java
 * Execution:    java LineIntersection
 * Dependencies: BSTExtended.java, Point2D.java, Interval1D.java
 *
 * Bài tập 26: Tìm các điểm giao cắt giữa đoạn ngang và đoạn dọc
 * Sử dụng thuật toán Sweep Line với BST
 ******************************************************************************/

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

public class LineIntersection {
    
    // Lớp đại diện cho một đoạn thẳng
    static class Segment {
        Point2D p1, p2;  // 2 điểm đầu cuối
        boolean isHorizontal;  // true nếu là đoạn ngang
        
        public Segment(Point2D p1, Point2D p2) {
            this.p1 = p1;
            this.p2 = p2;
            this.isHorizontal = (p1.y() == p2.y());
        }
        
        public double getY() {
            return p1.y();  // y của đoạn ngang
        }
        
        public double getX() {
            return p1.x();  // x của đoạn dọc
        }
        
        public double getMinX() {
            return Math.min(p1.x(), p2.x());
        }
        
        public double getMaxX() {
            return Math.max(p1.x(), p2.x());
        }
        
        public double getMinY() {
            return Math.min(p1.y(), p2.y());
        }
        
        public double getMaxY() {
            return Math.max(p1.y(), p2.y());
        }
    }
    
    // Lớp đại diện cho một sự kiện trong thuật toán quét
    static class Event implements Comparable<Event> {
        double x;  // tọa độ x của sự kiện
        int type;  // 0: bắt đầu đoạn ngang, 1: đoạn dọc, 2: kết thúc đoạn ngang
        Segment segment;
        
        public Event(double x, int type, Segment segment) {
            this.x = x;
            this.type = type;
            this.segment = segment;
        }
        
        @Override
        public int compareTo(Event other) {
            if (this.x != other.x) {
                return Double.compare(this.x, other.x);
            }
            // Ưu tiên: bắt đầu -> dọc -> kết thúc
            return Integer.compare(this.type, other.type);
        }
    }
    
    /**
     * Tìm tất cả các điểm giao cắt giữa đoạn ngang và đoạn dọc
     * @param horizontal danh sách các đoạn ngang
     * @param vertical danh sách các đoạn dọc
     * @return danh sách các điểm giao cắt
     */
    public static List<Point2D> findIntersections(List<Segment> horizontal, List<Segment> vertical) {
        List<Point2D> intersections = new ArrayList<>();
        List<Event> events = new ArrayList<>();
        
        // Tạo các sự kiện cho đoạn ngang
        for (Segment h : horizontal) {
            events.add(new Event(h.getMinX(), 0, h));  // Bắt đầu
            events.add(new Event(h.getMaxX(), 2, h));  // Kết thúc
        }
        
        // Tạo các sự kiện cho đoạn dọc
        for (Segment v : vertical) {
            events.add(new Event(v.getX(), 1, v));
        }
        
        // Sắp xếp các sự kiện theo tọa độ x
        Collections.sort(events);
        
        // BST lưu trữ các đoạn ngang đang hoạt động (key = y, value = segment)
        BSTExtended<Double, Segment> activeSegments = new BSTExtended<>();
        
        // Xử lý từng sự kiện
        for (Event event : events) {
            if (event.type == 0) {
                // Bắt đầu đoạn ngang: thêm vào BST
                activeSegments.put(event.segment.getY(), event.segment);
                
            } else if (event.type == 2) {
                // Kết thúc đoạn ngang: xóa khỏi BST (set value = null)
                activeSegments.put(event.segment.getY(), null);
                
            } else {
                // Đoạn dọc: tìm tất cả đoạn ngang giao với nó
                Segment v = event.segment;
                double yMin = v.getMinY();
                double yMax = v.getMaxY();
                
                // Tìm tất cả đoạn ngang có y trong khoảng [yMin, yMax]
                Queue<BSTExtended<Double, Segment>.Node> nodes = 
                    activeSegments.search(yMin, yMax);
                
                // Kiểm tra từng đoạn ngang
                for (BSTExtended<Double, Segment>.Node node : nodes) {
                    if (node.val != null) {  // Đoạn còn active
                        Segment h = node.val;
                        double x = v.getX();
                        double y = h.getY();
                        
                        // Kiểm tra x của đoạn dọc có nằm trong khoảng của đoạn ngang không
                        if (x >= h.getMinX() && x <= h.getMaxX()) {
                            intersections.add(new Point2D(x, y));
                        }
                    }
                }
            }
        }
        
        return intersections;
    }
    
    // Hàm main để test
    public static void main(String[] args) {
        System.out.println("=== BÀI TẬP 26: TÌM GIAO ĐIỂM ĐOẠN NGANG VÀ DỌC ===\n");
        
        // Tạo các đoạn ngang
        List<Segment> horizontal = new ArrayList<>();
        horizontal.add(new Segment(new Point2D(1, 2), new Point2D(5, 2)));
        horizontal.add(new Segment(new Point2D(2, 4), new Point2D(6, 4)));
        horizontal.add(new Segment(new Point2D(1, 5), new Point2D(4, 5)));
        
        // Tạo các đoạn dọc
        List<Segment> vertical = new ArrayList<>();
        vertical.add(new Segment(new Point2D(3, 1), new Point2D(3, 6)));
        vertical.add(new Segment(new Point2D(5, 3), new Point2D(5, 5)));
        
        System.out.println("Đoạn ngang:");
        for (int i = 0; i < horizontal.size(); i++) {
            Segment h = horizontal.get(i);
            System.out.println("  H" + (i+1) + ": " + h.p1 + " -> " + h.p2);
        }
        
        System.out.println("\nĐoạn dọc:");
        for (int i = 0; i < vertical.size(); i++) {
            Segment v = vertical.get(i);
            System.out.println("  V" + (i+1) + ": " + v.p1 + " -> " + v.p2);
        }
        
        // Tìm giao điểm
        List<Point2D> intersections = findIntersections(horizontal, vertical);
        
        System.out.println("\nCác điểm giao cắt (" + intersections.size() + " điểm):");
        for (Point2D p : intersections) {
            System.out.println("  " + p);
        }
        
        System.out.println("\n=== TEST 2: Ví dụ phức tạp hơn ===\n");
        
        // Test case 2
        List<Segment> h2 = new ArrayList<>();
        h2.add(new Segment(new Point2D(0, 1), new Point2D(8, 1)));
        h2.add(new Segment(new Point2D(1, 3), new Point2D(7, 3)));
        h2.add(new Segment(new Point2D(2, 5), new Point2D(9, 5)));
        
        List<Segment> v2 = new ArrayList<>();
        v2.add(new Segment(new Point2D(2, 0), new Point2D(2, 6)));
        v2.add(new Segment(new Point2D(5, 2), new Point2D(5, 6)));
        v2.add(new Segment(new Point2D(8, 0), new Point2D(8, 4)));
        
        System.out.println("Đoạn ngang: " + h2.size() + " đoạn");
        System.out.println("Đoạn dọc: " + v2.size() + " đoạn");
        
        List<Point2D> intersections2 = findIntersections(h2, v2);
        
        System.out.println("\nSố điểm giao cắt: " + intersections2.size());
        System.out.println("Chi tiết:");
        for (Point2D p : intersections2) {
            System.out.println("  " + p);
        }
    }
}