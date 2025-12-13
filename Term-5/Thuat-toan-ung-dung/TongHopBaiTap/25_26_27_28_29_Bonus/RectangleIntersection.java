/******************************************************************************
 * Compilation:  javac RectangleIntersection.java
 * Execution:    java RectangleIntersection
 * Dependencies: Queue.java, Interval1D.java, Interval2D.java, 
 *               IntervalSearchTree.java
 *
 * Bài tập 29: Tìm tất cả các cặp hình chữ nhật giao cắt nhau
 * Sử dụng thuật toán Sweep Line với Interval Search Tree
 ******************************************************************************/

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

public class RectangleIntersection {
    
    /**
     * Lớp đại diện cho một cặp chỉ số (i, j)
     */
    public static class Pair {
        public final int i;
        public final int j;
        
        public Pair(int i, int j) {
            // Đảm bảo i < j để tránh trùng lặp
            if (i < j) {
                this.i = i;
                this.j = j;
            } else {
                this.i = j;
                this.j = i;
            }
        }
        
        @Override
        public boolean equals(Object obj) {
            if (!(obj instanceof Pair)) return false;
            Pair other = (Pair) obj;
            return this.i == other.i && this.j == other.j;
        }
        
        @Override
        public int hashCode() {
            return 31 * i + j;
        }
        
        @Override
        public String toString() {
            return "(" + i + ", " + j + ")";
        }
    }
    
    /**
     * Lớp sự kiện cho thuật toán quét
     */
    private static class Event implements Comparable<Event> {
        double x;           // Tọa độ x của sự kiện
        int type;           // 0: bắt đầu, 1: kết thúc
        int rectIndex;      // Chỉ số hình chữ nhật
        Interval1D yInterval; // Khoảng y của hình chữ nhật
        
        public Event(double x, int type, int rectIndex, Interval1D yInterval) {
            this.x = x;
            this.type = type;
            this.rectIndex = rectIndex;
            this.yInterval = yInterval;
        }
        
        @Override
        public int compareTo(Event other) {
            if (this.x != other.x) {
                return Double.compare(this.x, other.x);
            }
            // Ưu tiên xử lý sự kiện bắt đầu trước kết thúc
            return Integer.compare(this.type, other.type);
        }
    }
    
    /**
     * Tìm tất cả các cặp hình chữ nhật giao cắt nhau
     * @param rectangles mảng các hình chữ nhật
     * @return Queue chứa các cặp (i, j) với i < j là chỉ số các hình chữ nhật giao nhau
     */
    public static Queue<Pair> findIntersections(Interval2D[] rectangles) {
        if (rectangles == null) throw new IllegalArgumentException("Rectangles array cannot be null");
        
        int n = rectangles.length;
        Queue<Pair> result = new Queue<>();
        
        if (n <= 1) return result;
        
        // Tạo danh sách các sự kiện
        List<Event> events = new ArrayList<>();
        
        for (int i = 0; i < n; i++) {
            Interval2D rect = rectangles[i];
            Interval1D xInterval = getXInterval(rect);
            Interval1D yInterval = getYInterval(rect);
            
            // Thêm sự kiện bắt đầu và kết thúc cho mỗi hình chữ nhật
            events.add(new Event(xInterval.min(), 0, i, yInterval));
            events.add(new Event(xInterval.max(), 1, i, yInterval));
        }
        
        // Sắp xếp các sự kiện theo tọa độ x
        Collections.sort(events);
        
        // Interval Search Tree lưu các hình chữ nhật đang "active"
        // Key: y-interval, Value: index của hình chữ nhật
        IntervalSearchTree<Integer> activeRects = new IntervalSearchTree<>();
        
        // Xử lý từng sự kiện
        for (Event event : events) {
            if (event.type == 0) {
                // Sự kiện bắt đầu: Tìm tất cả hình chữ nhật active giao với nó
                Queue<Interval1D> intersecting = activeRects.intersect(event.yInterval);
                
                for (Interval1D yInterval : intersecting) {
                    Integer otherIndex = activeRects.get(yInterval);
                    if (otherIndex != null) {
                        // Thêm cặp giao nhau
                        result.enqueue(new Pair(event.rectIndex, otherIndex));
                    }
                }
                
                // Thêm hình chữ nhật hiện tại vào active set
                activeRects.put(event.yInterval, event.rectIndex);
                
            } else {
                // Sự kiện kết thúc: Xóa hình chữ nhật khỏi active set
                activeRects.delete(event.yInterval);
            }
        }
        
        return result;
    }
    
    /**
     * Phiên bản đơn giản hơn: Kiểm tra từng cặp (brute force)
     * Dùng để so sánh kết quả
     */
    public static Queue<Pair> findIntersectionsBruteForce(Interval2D[] rectangles) {
        Queue<Pair> result = new Queue<>();
        int n = rectangles.length;
        
        for (int i = 0; i < n; i++) {
            for (int j = i + 1; j < n; j++) {
                if (rectangles[i].intersects(rectangles[j])) {
                    result.enqueue(new Pair(i, j));
                }
            }
        }
        
        return result;
    }
    
    // Helper methods để lấy x và y interval
    private static Interval1D getXInterval(Interval2D rect) {
        String str = rect.toString();
        String xPart = str.split(" x ")[0];
        String[] bounds = xPart.replace("[", "").replace("]", "").split(", ");
        return new Interval1D(Double.parseDouble(bounds[0]), Double.parseDouble(bounds[1]));
    }
    
    private static Interval1D getYInterval(Interval2D rect) {
        String str = rect.toString();
        String yPart = str.split(" x ")[1];
        String[] bounds = yPart.replace("[", "").replace("]", "").split(", ");
        return new Interval1D(Double.parseDouble(bounds[0]), Double.parseDouble(bounds[1]));
    }
    
    /**
     * In danh sách các hình chữ nhật
     */
    private static void printRectangles(Interval2D[] rectangles) {
        for (int i = 0; i < rectangles.length; i++) {
            Interval1D xInterval = getXInterval(rectangles[i]);
            Interval1D yInterval = getYInterval(rectangles[i]);
            System.out.println("  R" + i + ": x=" + xInterval + ", y=" + yInterval);
        }
    }
    
    /**
     * In các cặp giao nhau
     */
    private static void printPairs(Queue<Pair> pairs, Interval2D[] rectangles) {
        System.out.println("Các cặp hình chữ nhật giao nhau (" + pairs.size() + " cặp):");
        
        if (pairs.isEmpty()) {
            System.out.println("  (không có)");
            return;
        }
        
        for (Pair p : pairs) {
            System.out.println("  R" + p.i + " và R" + p.j);
            Interval1D xi = getXInterval(rectangles[p.i]);
            Interval1D yi = getYInterval(rectangles[p.i]);
            Interval1D xj = getXInterval(rectangles[p.j]);
            Interval1D yj = getYInterval(rectangles[p.j]);
            System.out.println("    R" + p.i + ": x=" + xi + ", y=" + yi);
            System.out.println("    R" + p.j + ": x=" + xj + ", y=" + yj);
        }
    }
    
    // Main để test
    public static void main(String[] args) {
        System.out.println("=== BÀI TẬP 29: TÌM CẶP HÌNH CHỮ NHẬT GIAO NHAU ===\n");
        
        // TEST 1: Ví dụ đơn giản
        System.out.println("=== TEST 1: Ví dụ cơ bản ===");
        Interval2D[] rects1 = {
            new Interval2D(new Interval1D(1, 4), new Interval1D(1, 3)),  // R0
            new Interval2D(new Interval1D(3, 6), new Interval1D(2, 5)),  // R1
            new Interval2D(new Interval1D(5, 8), new Interval1D(1, 4)),  // R2
            new Interval2D(new Interval1D(2, 5), new Interval1D(3, 6))   // R3
        };
        
        System.out.println("Các hình chữ nhật:");
        printRectangles(rects1);
        
        System.out.println("\nKết quả (Sweep Line Algorithm):");
        Queue<Pair> result1 = findIntersections(rects1);
        printPairs(result1, rects1);
        
        System.out.println("\nKiểm tra (Brute Force):");
        Queue<Pair> check1 = findIntersectionsBruteForce(rects1);
        printPairs(check1, rects1);
        
        // TEST 2: Nhiều hình chữ nhật hơn
        System.out.println("\n=== TEST 2: Trường hợp phức tạp hơn ===");
        Interval2D[] rects2 = {
            new Interval2D(new Interval1D(0, 3), new Interval1D(0, 3)),   // R0
            new Interval2D(new Interval1D(2, 5), new Interval1D(1, 4)),   // R1
            new Interval2D(new Interval1D(4, 7), new Interval1D(2, 5)),   // R2
            new Interval2D(new Interval1D(1, 4), new Interval1D(3, 6)),   // R3
            new Interval2D(new Interval1D(6, 9), new Interval1D(1, 4)),   // R4
            new Interval2D(new Interval1D(3, 6), new Interval1D(4, 7))    // R5
        };
        
        System.out.println("Số lượng hình chữ nhật: " + rects2.length);
        
        Queue<Pair> result2 = findIntersections(rects2);
        printPairs(result2, rects2);
        
        // TEST 3: Không có giao nhau
        System.out.println("\n=== TEST 3: Không có giao nhau ===");
        Interval2D[] rects3 = {
            new Interval2D(new Interval1D(0, 1), new Interval1D(0, 1)),
            new Interval2D(new Interval1D(2, 3), new Interval1D(2, 3)),
            new Interval2D(new Interval1D(4, 5), new Interval1D(4, 5))
        };
        
        System.out.println("Các hình chữ nhật:");
        printRectangles(rects3);
        
        Queue<Pair> result3 = findIntersections(rects3);
        printPairs(result3, rects3);
        
        // TEST 4: Tất cả giao nhau
        System.out.println("\n=== TEST 4: Tất cả giao nhau ===");
        Interval2D[] rects4 = {
            new Interval2D(new Interval1D(1, 5), new Interval1D(1, 5)),
            new Interval2D(new Interval1D(2, 6), new Interval1D(2, 6)),
            new Interval2D(new Interval1D(3, 7), new Interval1D(3, 7))
        };
        
        System.out.println("Các hình chữ nhật:");
        printRectangles(rects4);
        
        Queue<Pair> result4 = findIntersections(rects4);
        printPairs(result4, rects4);
        
        // TEST 5: So sánh hiệu suất
        System.out.println("\n=== TEST 5: So sánh hiệu suất ===");
        int n = 100;
        Interval2D[] largeTest = new Interval2D[n];
        for (int i = 0; i < n; i++) {
            double x1 = Math.random() * 100;
            double x2 = x1 + Math.random() * 20;
            double y1 = Math.random() * 100;
            double y2 = y1 + Math.random() * 20;
            largeTest[i] = new Interval2D(new Interval1D(x1, x2), new Interval1D(y1, y2));
        }
        
        System.out.println("Kiểm tra với " + n + " hình chữ nhật...");
        
        long start1 = System.currentTimeMillis();
        Queue<Pair> sweepResult = findIntersections(largeTest);
        long time1 = System.currentTimeMillis() - start1;
        
        long start2 = System.currentTimeMillis();
        Queue<Pair> bruteResult = findIntersectionsBruteForce(largeTest);
        long time2 = System.currentTimeMillis() - start2;
        
        System.out.println("Sweep Line: " + sweepResult.size() + " cặp, thời gian: " + time1 + "ms");
        System.out.println("Brute Force: " + bruteResult.size() + " cặp, thời gian: " + time2 + "ms");
        System.out.println("Kết quả khớp: " + (sweepResult.size() == bruteResult.size()));
    }
}