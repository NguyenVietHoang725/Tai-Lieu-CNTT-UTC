/******************************************************************************
 * Compilation:  javac VietnameseRabinKarp.java
 * Execution:    java VietnameseRabinKarp "mẫu" "văn bản"
 * Dependencies: VietnameseAlphabet.java, Alphabet.java, RabinKarp.java
 *
 * Mô tả: Mở rộng thuật toán Rabin-Karp để hỗ trợ Tiếng Việt.
 * Sử dụng bảng chữ cái tùy chỉnh để ánh xạ ký tự Unicode sang số nguyên [0, R-1]
 * trước khi thực hiện phép băm (hashing).
 ******************************************************************************/

import java.math.BigInteger;
import java.util.Random;

public class VietnameseRabinKarp extends RabinKarp {
    private final Alphabet alphabet; // Bảng chữ cái Tiếng Việt
    private String pat;              // Chuỗi mẫu
    private long patHash;            // Giá trị Hash của mẫu
    private int m;                   // Độ dài mẫu
    private long q;                  // Số nguyên tố lớn (để chia lấy dư)
    private int R;                   // Cơ số (Kích thước bảng chữ cái)
    private long RM;                 // R^(M-1) % q

    /**
     * Khởi tạo thuật toán với chuỗi mẫu Tiếng Việt.
     * @param pat Chuỗi mẫu
     */
    public VietnameseRabinKarp(String pat) {
        // Gọi constructor cha với chuỗi rỗng để tránh lỗi, 
        // ta sẽ tự khởi tạo các tham số bên dưới.
        super(""); 
        
        this.alphabet = VietnameseAlphabet.VIETNAMESE_ALPHABET;
        this.R = alphabet.radix();
        this.pat = pat;
        this.m = pat.length();
        this.q = longRandomPrime(); // Chọn số nguyên tố ngẫu nhiên

        // Kiểm tra mẫu có hợp lệ không
        for (char c : pat.toCharArray()) {
            if (!alphabet.contains(c)) {
                throw new IllegalArgumentException("Mẫu chứa ký tự không hỗ trợ: " + c);
            }
        }

        // Tính trước giá trị RM = R^(m-1) % q
        // Dùng để loại bỏ ký tự đầu tiên khi trượt cửa sổ hash (Rolling Hash)
        RM = 1;
        for (int i = 1; i <= m - 1; i++) {
            RM = (R * RM) % q;
        }

        // Tính Hash cho mẫu
        patHash = hash(pat, m);
    }

    /**
     * Hàm tính Hash cho chuỗi key độ dài m.
     * Công thức: (key[0]*R^(m-1) + ... + key[m-1]*R^0) % q
     */
    private long hash(String key, int m) {
        long h = 0;
        for (int j = 0; j < m; j++) {
            char c = key.charAt(j);
            // Quan trọng: Dùng toIndex() để lấy giá trị số của ký tự trong bảng chữ cái VN
            h = (R * h + alphabet.toIndex(c)) % q;
        }
        return h;
    }

    /**
     * Kiểm tra chính xác (Las Vegas check):
     * Khi 2 giá trị Hash bằng nhau, cần so sánh từng ký tự để tránh xung đột Hash.
     */
    private boolean check(String txt, int i) {
        for (int j = 0; j < m; j++) {
            if (pat.charAt(j) != txt.charAt(i + j)) {
                return false;
            }
        }
        return true;
    }

    /**
     * Tìm kiếm mẫu trong văn bản.
     * @param txt Văn bản
     * @return Vị trí tìm thấy đầu tiên, hoặc n nếu không thấy.
     */
    @Override
    public int search(String txt) {
        int n = txt.length();
        if (n < m) return n;

        // 1. Tính Hash cho đoạn văn bản đầu tiên (độ dài m)
        long txtHash = 0;
        for (int j = 0; j < m; j++) {
            char c = txt.charAt(j);
            // Nếu ký tự không có trong bảng chữ cái, gán giá trị mặc định R-1
            int index = alphabet.contains(c) ? alphabet.toIndex(c) : R - 1;
            txtHash = (R * txtHash + index) % q;
        }

        // Kiểm tra ngay tại vị trí 0
        if ((patHash == txtHash) && check(txt, 0)) {
            return 0;
        }

        // 2. Rolling Hash: Trượt cửa sổ qua từng ký tự tiếp theo
        for (int i = m; i < n; i++) {
            // Bước A: Loại bỏ ký tự đầu tiên của cửa sổ cũ (leading digit)
            char leadChar = txt.charAt(i - m);
            int leadIndex = alphabet.contains(leadChar) ? alphabet.toIndex(leadChar) : R - 1;
            
            // Công thức: hash = (hash + q - (RM * leadIndex) % q) % q
            txtHash = (txtHash + q - (RM * leadIndex) % q) % q;

            // Bước B: Thêm ký tự mới vào cuối cửa sổ (trailing digit)
            char trailChar = txt.charAt(i);
            int trailIndex = alphabet.contains(trailChar) ? alphabet.toIndex(trailChar) : R - 1;
            
            // Công thức: hash = (hash * R + trailIndex) % q
            txtHash = (txtHash * R + trailIndex) % q;

            // Bước C: Kiểm tra khớp
            int offset = i - m + 1;
            if ((patHash == txtHash) && check(txt, offset)) {
                return offset;
            }
        }

        return n; // Không tìm thấy
    }

    // Sinh số nguyên tố ngẫu nhiên 31-bit
    private static long longRandomPrime() {
        BigInteger prime = BigInteger.probablePrime(31, new Random());
        return prime.longValue();
    }

    /**
     * Hàm Main để kiểm thử.
     */
    public static void main(String[] args) {
        // Input cứng để demo
        String pat = "chào";
        String txt = "xin chào thế giới";

        // Nếu có tham số dòng lệnh thì dùng tham số
        if (args.length >= 2) {
            pat = args[0];
            txt = args[1];
        }

        System.out.println("Văn bản: " + txt);
        System.out.println("Mẫu tìm: " + pat);

        try {
            VietnameseRabinKarp searcher = new VietnameseRabinKarp(pat);
            int offset = searcher.search(txt);

            // In kết quả
            if (offset < txt.length()) {
                System.out.println("-> Tìm thấy tại vị trí: " + offset);
                System.out.print("Minh họa: ");
                System.out.println(txt);
                System.out.print("          ");
                for (int i = 0; i < offset; i++) {
                    System.out.print(" ");
                }
                System.out.println(pat);
            } else {
                System.out.println("-> Không tìm thấy.");
            }
        } catch (IllegalArgumentException e) {
            System.err.println("Lỗi: " + e.getMessage());
        }
    }
}