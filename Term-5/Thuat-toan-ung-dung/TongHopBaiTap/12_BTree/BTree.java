/******************************************************************************
 * Compilation:  javac BTree.java
 * Execution:    java BTree
 *
 * Mô tả: Cấu trúc dữ liệu B-Tree (Cây B).
 * * Đây là một cây tìm kiếm cân bằng đa nhánh.
 * * M = 4: B-Tree bậc 4 (còn gọi là cây 2-3-4).
 * Mỗi nút có tối đa 3 khóa và 4 con.
 * Mỗi nút có tối thiểu 2 con (trừ nút gốc).
 ******************************************************************************/

public class BTree<Key extends Comparable<Key>, Value>  {
    // Bậc của cây B-Tree (Số con tối đa của một nút)
    // M = 4 nghĩa là mỗi nút chứa tối đa 3 phần tử (Key-Value)
    private static final int M = 4;

    private Node root;       // Nút gốc của cây
    private int height;      // Chiều cao của cây (0 là cây chỉ có lá)
    private int n;           // Tổng số cặp key-value trong cây

    // Lớp nội bộ đại diện cho một Nút (Node) trong B-Tree
    private static final class Node {
        private int m;                             // Số lượng con hiện tại
        private Entry[] children = new Entry[M];   // Mảng chứa các phần tử con

        // Tạo một nút mới với k con
        private Node(int k) {
            m = k;
        }
    }

    // Lớp nội bộ đại diện cho một Phần tử (Entry) trong Nút
    // Nếu là nút lá (External): chứa Key và Value.
    // Nếu là nút trong (Internal): chứa Key và tham chiếu đến nút con (next).
    private static class Entry {
        private Comparable key;
        private Object val;
        private Node next;     // Con trỏ đến nút con tiếp theo
        
        public Entry(Comparable key, Object val, Node next) {
            this.key  = key;
            this.val  = val;
            this.next = next;
        }
    }

    /**
     * Khởi tạo cây B-Tree rỗng.
     */
    public BTree() {
        root = new Node(0);
    }

    public int size() { return n; }
    public boolean isEmpty() { return size() == 0; }
    public int height() { return height; }

    /**
     * Lấy giá trị (Value) tương ứng với Key.
     */
    public Value get(Key key) {
        if (key == null) throw new IllegalArgumentException("Key không được null");
        return search(root, key, height);
    }

    // Hàm đệ quy tìm kiếm
    private Value search(Node x, Key key, int ht) {
        Entry[] children = x.children;

        // Nếu là nút lá (chiều cao = 0)
        if (ht == 0) {
            for (int j = 0; j < x.m; j++) {
                if (eq(key, children[j].key)) return (Value) children[j].val;
            }
        }
        // Nếu là nút trong
        else {
            for (int j = 0; j < x.m; j++) {
                // Tìm nhánh con phù hợp để đi xuống
                // (Đi xuống nhánh j nếu key < children[j+1].key hoặc đã là nhánh cuối cùng)
                if (j+1 == x.m || less(key, children[j+1].key))
                    return search(children[j].next, key, ht-1);
            }
        }
        return null;
    }

    /**
     * Thêm cặp Key-Value vào cây.
     */
    public void put(Key key, Value val) {
        if (key == null) throw new IllegalArgumentException("Key không được null");
        
        // Chèn vào cây, trả về nút bị tách (nếu có)
        Node u = insert(root, key, val, height);
        n++;
        
        // Nếu không có nút nào bị tách (u == null), việc chèn hoàn tất
        if (u == null) return;

        // Nếu nút gốc bị tách (cây đầy ở gốc), tạo nút gốc mới
        // Chiều cao cây tăng lên 1
        Node t = new Node(2);
        t.children[0] = new Entry(root.children[0].key, null, root);
        t.children[1] = new Entry(u.children[0].key, null, u);
        root = t;
        height++;
    }

    // Hàm đệ quy chèn phần tử
    private Node insert(Node h, Key key, Value val, int ht) {
        int j;
        Entry t = new Entry(key, val, null);

        // 1. Tìm vị trí cần chèn trong nút h
        // Nếu là nút lá (ht=0)
        if (ht == 0) {
            for (j = 0; j < h.m; j++) {
                if (less(key, h.children[j].key)) break;
            }
        }
        // Nếu là nút trong
        else {
            for (j = 0; j < h.m; j++) {
                if ((j+1 == h.m) || less(key, h.children[j+1].key)) {
                    // Đệ quy xuống nút con
                    Node u = insert(h.children[j++].next, key, val, ht-1);
                    if (u == null) return null;
                    
                    // Nếu nút con bị tách, nhận phần tử được đẩy lên (u) để chèn vào nút hiện tại
                    t.key = u.children[0].key;
                    t.val = null;
                    t.next = u;
                    break;
                }
            }
        }

        // 2. Chèn phần tử t vào vị trí j (dịch chuyển các phần tử phía sau sang phải)
        for (int i = h.m; i > j; i--)
            h.children[i] = h.children[i-1];
        h.children[j] = t;
        h.m++;
        
        // 3. Kiểm tra xem nút có bị đầy không (số con < M)
        if (h.m < M) return null;
        else         return split(h); // Nếu đầy, tách nút làm đôi
    }

    // Hàm tách nút (Split Node) khi nút đầy (có M con)
    private Node split(Node h) {
        Node t = new Node(M/2);
        h.m = M/2;
        for (int j = 0; j < M/2; j++)
            t.children[j] = h.children[M/2+j];
        return t;
    }

    /**
     * Chuyển đổi cây sang chuỗi String để in ra màn hình.
     * Sử dụng thụt đầu dòng (indent) để biểu diễn cấp độ cây.
     */
    public String toString() {
        return toString(root, height, "") + "\n";
    }

    private String toString(Node h, int ht, String indent) {
        StringBuilder s = new StringBuilder();
        Entry[] children = h.children;

        if (ht == 0) { // Nút lá: In Key và Value
            for (int j = 0; j < h.m; j++) {
                s.append(indent + children[j].key + " " + children[j].val + "\n");
            }
        }
        else { // Nút trong: In Key đại diện và đệ quy xuống con
            for (int j = 0; j < h.m; j++) {
                if (j > 0) s.append(indent + "(" + children[j].key + ")\n");
                s.append(toString(children[j].next, ht-1, indent + "     "));
            }
        }
        return s.toString();
    }

    // Các hàm so sánh
    private boolean less(Comparable k1, Comparable k2) {
        return k1.compareTo(k2) < 0;
    }
    private boolean eq(Comparable k1, Comparable k2) {
        return k1.compareTo(k2) == 0;
    }
}