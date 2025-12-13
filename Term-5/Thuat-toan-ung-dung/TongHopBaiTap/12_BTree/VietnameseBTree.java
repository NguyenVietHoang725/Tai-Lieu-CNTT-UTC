import java.util.Random;

/**
 * Lớp VietnameseBTree mô phỏng quản lý điểm sinh viên.
 * Dữ liệu: 17 sinh viên Việt Nam.
 * Điểm số: Random (0.0 - 10.0).
 */
public class VietnameseBTree extends BTree<String, Double> {
    
    // Danh sách 17 tên sinh viên theo đề bài
    private static final String[] NAMES = {
        "An", "Anh", "Ánh", "Ba", "Bình", "Bính", "Lan", "Lân", "Lanh",
        "Quang", "Quảng", "Quỳnh", "Quân", "Thai", "Thành", "Thắng", "Thông"
    };
    
    private static final Random RANDOM = new Random();

    /**
     * Constructor: Khởi tạo và nạp dữ liệu ngay lập tức.
     */
    public VietnameseBTree() {
        super();
        initialize();
    }

    /**
     * Nạp dữ liệu vào cây.
     */
    private void initialize() {
        System.out.println("Đang nạp dữ liệu vào B-Tree (M=4)...");
        for (String name : NAMES) {
            // Sinh điểm ngẫu nhiên, làm tròn 2 chữ số thập phân
            double score = Math.round(RANDOM.nextDouble() * 10.0 * 100.0) / 100.0;
            
            // Chèn vào cây
            put(name, score);
            // System.out.println("Đã thêm: " + name + " - " + score); // Bật dòng này nếu muốn xem quá trình thêm
        }
        System.out.println("Hoàn tất nạp " + NAMES.length + " sinh viên.\n");
    }

    /**
     * Hàm Main để chạy kiểm thử.
     */
    public static void main(String[] args) {
        // 1. Khởi tạo cây và nạp dữ liệu
        VietnameseBTree tree = new VietnameseBTree();

        // 2. In thông số cơ bản
        System.out.println("=== THÔNG TIN CÂY ===");
        System.out.println("Tổng số sinh viên (Size): " + tree.size());
        System.out.println("Chiều cao cây (Height):   " + tree.height());
        System.out.println();

        // 3. Tra cứu thử nghiệm
        System.out.println("=== TRA CỨU ĐIỂM SỐ ===");
        String[] testNames = {"An", "Bình", "Quỳnh", "Thành", "TênLạ"};
        for (String name : testNames) {
            Double score = tree.get(name);
            System.out.printf(" - %-10s: %s\n", name, (score != null ? score : "Không tìm thấy"));
        }
        System.out.println();

        // 4. In cấu trúc cây (Visualizing the B-Tree)
        System.out.println("=== CẤU TRÚC CÂY B-TREE (M=4) ===");
        // BTree.toString() đã được viết để in cây dạng thụt đầu dòng (indentation)
        // Cách đọc: Các phần tử cùng mức thụt đầu dòng nằm trong cùng một node (hoặc node anh em).
        // Dấu ngoặc (Key) biểu thị khóa phân cách giữa các nhánh con.
        System.out.println(tree);
    }
}