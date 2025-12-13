/******************************************************************************
 * Compilation:  javac VietnameseBoyerMoore.java
 * Execution:    java VietnameseBoyerMoore "mẫu" "văn bản"
 * Dependencies: VietnameseAlphabet.java, Alphabet.java, BoyerMoore.java
 *
 * Mô tả: Mở rộng BoyerMoore để hỗ trợ Tiếng Việt.
 ******************************************************************************/

public class VietnameseBoyerMoore extends BoyerMoore {
    private final Alphabet alphabet; // Bảng chữ cái Tiếng Việt
    private int[] right;             // Mảng Bad-character cục bộ (ghi đè lớp cha)
    private char[] pattern;          
    private String pat;              

    /**
     * Khởi tạo với chuỗi mẫu, sử dụng VietnameseAlphabet.
     */
    public VietnameseBoyerMoore(String pat) {
        super(new char[0], 0); // Gọi constructor rỗng của cha
        this.alphabet = VietnameseAlphabet.VIETNAMESE_ALPHABET;
        this.R = alphabet.radix();
        this.pat = pat;
        this.pattern = pat.toCharArray();
        initialize();
    }

    /**
     * Xây dựng bảng nhảy bước (Skip Table) cho Tiếng Việt.
     */
    private void initialize() {
        // Tạo mảng kích thước R (số lượng ký tự trong bảng chữ cái VN)
        right = new int[R];
        for (int c = 0; c < R; c++) {
            right[c] = -1;
        }
        
        // Duyệt qua mẫu để điền vị trí xuất hiện
        for (int j = 0; j < pat.length(); j++) {
            char c = pat.charAt(j);
            if (alphabet.contains(c)) {
                // Ánh xạ ký tự Unicode -> Index số nguyên
                right[alphabet.toIndex(c)] = j;
            }
        }
    }

    /**
     * Ghi đè phương thức search để xử lý văn bản Tiếng Việt.
     */
    @Override
    public int search(String txt) {
        int m = pat.length();
        int n = txt.length();
        int skip;
        
        for (int i = 0; i <= n - m; i += skip) {
            skip = 0;
            // Quét từ Phải sang Trái
            for (int j = m - 1; j >= 0; j--) {
                char textChar = txt.charAt(i + j);
                
                // Nếu có ký tự không khớp
                if (pat.charAt(j) != textChar) {
                    if (alphabet.contains(textChar)) {
                        // Trường hợp 1: Ký tự văn bản nằm trong bảng chữ cái
                        // Tính bước nhảy dựa trên bảng right[] đã ánh xạ
                        skip = Math.max(1, j - right[alphabet.toIndex(textChar)]);
                    } else {
                        // Trường hợp 2: Ký tự lạ (không có trong bảng chữ cái)
                        // Nhảy qua toàn bộ đoạn này vì ký tự lạ chắc chắn không khớp với gì trong mẫu
                        skip = j + 1; 
                    }
                    break;
                }
            }
            if (skip == 0) return i; // Tìm thấy
        }
        return n; // Không tìm thấy
    }

    /**
     * Hàm Main chạy kiểm thử.
     */
    public static void main(String[] args) {
        // Ví dụ chạy mặc định nếu không có tham số dòng lệnh
        String pat = args.length > 0 ? args[0] : "chào";
        String txt = args.length > 1 ? args[1] : "xin chào thế giới";

        // Kiểm tra xem mẫu có hợp lệ không
        Alphabet alphabet = VietnameseAlphabet.VIETNAMESE_ALPHABET;
        for (char c : pat.toCharArray()) {
            if (!alphabet.contains(c)) {
                System.out.println("Lỗi: Mẫu chứa ký tự không được hỗ trợ: " + c);
                return;
            }
        }

        System.out.println("Văn bản: " + txt);
        System.out.println("Mẫu tìm: " + pat);

        // Khởi tạo và tìm kiếm
        VietnameseBoyerMoore bm = new VietnameseBoyerMoore(pat);
        int offset = bm.search(txt);

        // In kết quả
        if (offset < txt.length()) {
            System.out.println("-> Tìm thấy tại vị trí: " + offset);
            System.out.print("Minh họa: ");
            System.out.println(txt);
            System.out.print("          ");
            for (int i = 0; i < offset; i++) System.out.print(" ");
            System.out.println(pat);
        } else {
            System.out.println("-> Không tìm thấy.");
        }
    }
}