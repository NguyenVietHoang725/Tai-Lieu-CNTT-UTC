/******************************************************************************
 * Compilation:  javac NFAVN.java
 * Execution:    java NFAVN "regex" "text"
 * Dependencies: NFA.java, VietnameseAlphabet.java
 *
 * Mô tả: Mở rộng NFA để hỗ trợ biểu thức chính quy với ký tự Tiếng Việt.
 ******************************************************************************/

public class NFAVN extends NFA {

    private final VietnameseAlphabet alphabet;

    /**
     * Khởi tạo NFAVN với biểu thức chính quy Tiếng Việt.
     * @param regexp Biểu thức chính quy
     */
    public NFAVN(String regexp) {
        // 1. Chuyển đổi regex sang dạng chuỗi các chỉ số (char indices)
        // 2. Gọi constructor của lớp cha NFA
        super(convertToIndices(regexp));
        this.alphabet = VietnameseAlphabet.VIETNAMESE_ALPHABET;
    }

    /**
     * Hàm tiện ích: Chuyển đổi chuỗi ký tự Unicode sang chuỗi chỉ số.
     * Giữ nguyên các ký tự đặc biệt của Regex: (, ), *, |
     */
    private static String convertToIndices(String s) {
        VietnameseAlphabet alpha = VietnameseAlphabet.VIETNAMESE_ALPHABET;
        StringBuilder sb = new StringBuilder();
        
        for (int i = 0; i < s.length(); i++) {
            char c = s.charAt(i);
            // Giữ nguyên các toán tử của Regex
            if (c == '(' || c == ')' || c == '*' || c == '|') {
                sb.append(c);
            } 
            // Nếu là ký tự văn bản, chuyển sang index trong bảng chữ cái
            else if (alpha.contains(c)) {
                int index = alpha.toIndex(c);
                sb.append((char) index); // Ép kiểu index thành char để lưu vào chuỗi
            } else {
                // Xử lý ký tự lạ (không hỗ trợ)
                // Có thể ném lỗi hoặc bỏ qua tùy ngữ cảnh
                throw new IllegalArgumentException("Ký tự không hỗ trợ: " + c);
            }
        }
        return sb.toString();
    }

    /**
     * Kiểm tra xem văn bản có khớp với biểu thức chính quy không.
     * @param txt Văn bản cần kiểm tra
     * @return true nếu khớp, false nếu không
     */
    @Override
    public boolean recognizes(String txt) {
        // Kiểm tra tính hợp lệ của văn bản đầu vào
        for (int i = 0; i < txt.length(); i++) {
            char c = txt.charAt(i);
            if (!alphabet.contains(c)) {
                throw new IllegalArgumentException("Văn bản chứa ký tự lạ: " + c);
            }
        }
        
        // Chuyển đổi văn bản sang dạng chỉ số tương ứng với Regex đã chuyển đổi
        String convertedTxt = convertToIndices(txt);
        
        // Gọi phương thức recognizes của lớp cha NFA
        return super.recognizes(convertedTxt);
    }

    /**
     * Hàm Main để kiểm thử.
     */
    public static void main(String[] args) {
        // Ví dụ mặc định nếu không có tham số
        String regexp = "(Hà Nội|Hồ Chí Minh)";
        String txt = "Hà Nội";

        if (args.length >= 2) {
            regexp = "(" + args[0] + ")"; // Bao regex trong ngoặc đơn để đảm bảo cú pháp
            txt = args[1];
        }

        System.out.println("Regex: " + regexp);
        System.out.println("Text:  " + txt);

        try {
            NFAVN nfa = new NFAVN(regexp);
            boolean result = nfa.recognizes(txt);
            
            System.out.println("-> Kết quả: " + (result ? "KHỚP" : "KHÔNG KHỚP"));
            
        } catch (IllegalArgumentException e) {
            System.err.println("Lỗi: " + e.getMessage());
        }
    }
}