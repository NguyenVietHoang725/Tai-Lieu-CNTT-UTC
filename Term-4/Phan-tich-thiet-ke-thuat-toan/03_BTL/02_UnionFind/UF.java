/******************************************************************************
 *  Compilation:  javac UF.java
 *  Execution:    java UF < input.txt
 *  Dependencies: StdIn.java StdOut.java
 *  Data files:   https://algs4.cs.princeton.edu/15uf/tinyUF.txt
 *                https://algs4.cs.princeton.edu/15uf/mediumUF.txt
 *                https://algs4.cs.princeton.edu/15uf/largeUF.txt
 *
 *  Weighted quick-union by rank with path compression by halving.
 *
 *  % java UF < tinyUF.txt
 *  4 3
 *  3 8
 *  6 5
 *  9 4
 *  2 1
 *  5 0
 *  7 2
 *  6 1
 *  2 components
 *
 ******************************************************************************/
import java.io.*;

/**
 * Lớp {@code UF} (Union-Find) – còn được gọi là Disjoint Set Union (DSU) – dùng để quản lý các tập hợp rời rạc.
 * Hỗ trợ kiểm tra hai phần tử có cùng thuộc một tập hợp không và hợp nhất các tập hợp chứa hai phần tử đó.
 *
 * Cấu trúc này đặc biệt hữu ích trong các thuật toán đồ thị như Kruskal (Minimum Spanning Tree), kiểm tra chu trình,...
 *
 * Thuật toán sử dụng:
 * - Quick-Union có trọng số dựa trên độ sâu cây (rank).
 * - Tối ưu hóa bằng nén đường đi (path compression by halving).
 *
 * Độ phức tạp gần như O(1) cho mỗi thao tác trong thực tế.
 */
public class UF {

    private int[] parent;   // parent[i] là nút cha của i trong cây đại diện tập
    private byte[] rank;    // rank[i] là độ sâu (gần đúng) của cây gốc i
    private int count;      // số lượng tập hợp rời rạc hiện tại

    /**
     * Khởi tạo cấu trúc UF với n phần tử, mỗi phần tử ban đầu là một tập riêng biệt.
     * @param n số lượng phần tử
     * @throws IllegalArgumentException nếu n < 0
     */
    public UF(int n) {
        if (n < 0) throw new IllegalArgumentException("Số lượng phần tử không hợp lệ: " + n);
        count = n;
        parent = new int[n];
        rank = new byte[n];
        for (int i = 0; i < n; i++) {
            parent[i] = i;  // mỗi phần tử là gốc của chính nó
            rank[i] = 0;
        }
    }

    /**
     * Tìm gốc đại diện của phần tử p. Dùng path compression để tăng hiệu suất.
     * @param p phần tử cần tìm gốc
     * @return gốc đại diện của tập chứa p
     * @throws IllegalArgumentException nếu p nằm ngoài giới hạn
     */
    public int find(int p) {
        validate(p);
        while (p != parent[p]) {
            parent[p] = parent[parent[p]]; // Nén đường đi bằng cách rút ngắn 1 cấp cha
            p = parent[p];
        }
        return p;
    }

    /**
     * Hợp nhất hai tập chứa p và q.
     * @param p phần tử đầu tiên
     * @param q phần tử thứ hai
     * @throws IllegalArgumentException nếu p hoặc q nằm ngoài giới hạn
     */
    public void union(int p, int q) {
        int rootP = find(p);
        int rootQ = find(q);
        if (rootP == rootQ) return; // đã cùng tập

        // Hợp nhất dựa trên rank: cây thấp hơn nối vào cây cao hơn
        if      (rank[rootP] < rank[rootQ]) parent[rootP] = rootQ;
        else if (rank[rootP] > rank[rootQ]) parent[rootQ] = rootP;
        else {
            parent[rootQ] = rootP;
            rank[rootP]++;
        }

        count--; // giảm số lượng tập hợp sau khi hợp nhất
    }

    /**
     * Kiểm tra xem hai phần tử có cùng thuộc một tập không.
     * @param p phần tử đầu tiên
     * @param q phần tử thứ hai
     * @return true nếu p và q cùng tập, ngược lại false
     */
    public boolean connected(int p, int q) {
        return find(p) == find(q);
    }

    /**
     * Trả về số lượng tập hợp rời rạc hiện tại.
     * @return số tập hợp
     */
    public int count() {
        return count;
    }

    // Kiểm tra chỉ số đầu vào có hợp lệ không
    private void validate(int p) {
        int n = parent.length;
        if (p < 0 || p >= n)
            throw new IllegalArgumentException("Chỉ số " + p + " không hợp lệ (0.." + (n - 1) + ")");
    }

    /**
     * Đọc dữ liệu từ file input và thực hiện hợp nhất các cặp phần tử.
     * In ra các cặp được hợp nhất và số tập hợp còn lại.
     *
     * @param args không dùng trong chương trình này
     * @throws IOException nếu lỗi đọc file
     */
    public static void main(String[] args) throws IOException {
        System.setIn(new FileInputStream(new File("tinyUF.txt")));
        int n = StdIn.readInt();
        UF uf = new UF(n);
        while (!StdIn.isEmpty()) {
            int p = StdIn.readInt();
            int q = StdIn.readInt();
            if (uf.connected(p, q)) continue;
            uf.union(p, q);
            StdOut.println(p + " " + q);
        }
        StdOut.println(uf.count() + " components");
    }
}