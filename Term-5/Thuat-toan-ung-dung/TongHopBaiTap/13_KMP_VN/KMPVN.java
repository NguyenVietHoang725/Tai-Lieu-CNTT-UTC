/**
 * Lớp KMPVN mở rộng KMP để hỗ trợ tìm kiếm văn bản Tiếng Việt.
 * * Cơ chế: Chuyển đổi ký tự Unicode Tiếng Việt sang chỉ số nguyên (0 -> R-1)
 * dựa trên bảng chữ cái VietnameseAlphabet trước khi đưa vào thuật toán KMP.
 */
public class KMPVN extends KMP {
    private final Alphabet alphabet;

    /**
     * Khởi tạo KMPVN với chuỗi mẫu Tiếng Việt.
     * Sử dụng mặc định VietnameseAlphabet.
     * @param pat Chuỗi mẫu
     */
    public KMPVN(String pat) {
        this(pat, VietnameseAlphabet.VIETNAMESE_ALPHABET);
    }

    /**
     * Constructor chi tiết với bảng chữ cái tùy chọn.
     */
    public KMPVN(String pat, Alphabet alphabet) {
        // 1. Chuyển chuỗi mẫu sang mảng chỉ số (int[]) nhưng lưu dưới dạng char[]
        // 2. Gọi constructor của lớp cha KMP với kích thước R của bảng chữ cái VN
        super(toIndexedCharArray(pat, alphabet), alphabet.radix());
        this.alphabet = alphabet;
    }

    /**
     * Hàm tiện ích: Chuyển đổi String -> char[] chứa index.
     * Ví dụ: 'a' -> 0, 'ă' -> 5, ... (tùy theo định nghĩa trong Alphabet)
     */
    private static char[] toIndexedCharArray(String s, Alphabet alphabet) {
        char[] indexed = new char[s.length()];
        for (int i = 0; i < s.length(); i++) {
            // Lấy index của ký tự trong bảng chữ cái VN và ép kiểu về char
            // (Vì mảng dfa trong KMP dùng char[] làm input giả lập)
            indexed[i] = (char) alphabet.toIndex(s.charAt(i));
        }
        return indexed;
    }

    /**
     * Ghi đè phương thức search để xử lý văn bản đầu vào.
     * Chuyển đổi văn bản sang dạng index trước khi tìm kiếm.
     */
    @Override
    public int search(String txt) {
        return super.search(toIndexedCharArray(txt, alphabet));
    }

    /**
     * Hàm main để chạy kiểm thử (Demo).
     */
    public static void main(String[] args) {
        // Cấu hình dữ liệu đầu vào (Hard-code để dễ chạy thử)
        // Mẫu dài khoảng 15 ký tự
        String pat = "thuật toán KMP"; 
        
        // Văn bản chứa mẫu
        String txt = "Trong khoa học máy tính, thuật toán KMP là một giải thuật tìm kiếm xâu hiệu quả.";

        System.out.println("=== DEMO KMP CHO TIẾNG VIỆT ===");
        System.out.println("Văn bản: " + txt);
        System.out.println("Mẫu tìm: " + pat);

        // Thực hiện tìm kiếm
        KMPVN kmp = new KMPVN(pat);
        int offset = kmp.search(txt);

        // Hiển thị kết quả
        System.out.println("\nKết quả:");
        if (offset < txt.length()) {
            System.out.println(" -> Tìm thấy tại vị trí: " + offset);
            
            // In văn bản và đánh dấu vị trí tìm thấy
            System.out.println("Text:    " + txt);
            System.out.print("Match:   ");
            for (int i = 0; i < offset; i++) {
                System.out.print(" ");
            }
            System.out.println(pat);
        } else {
            System.out.println(" -> Không tìm thấy mẫu trong văn bản.");
        }
    }
}