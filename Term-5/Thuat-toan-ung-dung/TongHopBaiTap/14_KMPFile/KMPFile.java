import java.io.*;
import org.apache.poi.xwpf.usermodel.XWPFDocument;
import org.apache.poi.xwpf.usermodel.XWPFParagraph;

/**
 * Lớp KMPFile mở rộng thuật toán KMP để hỗ trợ tìm kiếm trên file .docx.
 * * Đặc điểm:
 * 1. Đọc văn bản từ file Word sử dụng Apache POI.
 * 2. Chuyển đổi ký tự Tiếng Việt sang chỉ số (Index) của VietnameseAlphabet.
 * 3. Tìm kiếm mẫu dài (~30 ký tự) chính xác.
 */
public class KMPFile extends KMP {
    private final Alphabet alphabet;

    /**
     * Khởi tạo KMPFile với chuỗi mẫu và bảng chữ cái.
     * @param pat Chuỗi mẫu (Pattern)
     * @param alphabet Bảng chữ cái sử dụng (VietnameseAlphabet)
     */
    public KMPFile(String pat, Alphabet alphabet) {
        // Chuyển chuỗi mẫu sang mảng chỉ số và gọi constructor của lớp cha KMP
        super(toIndexedCharArray(pat, alphabet), alphabet.radix());
        this.alphabet = alphabet;
    }

    /**
     * Hàm tiện ích: Chuyển đổi String -> mảng char chứa chỉ số (Index).
     * Điều này giúp KMP xử lý được các ký tự Tiếng Việt nằm ngoài bảng mã ASCII.
     */
    private static char[] toIndexedCharArray(String s, Alphabet alphabet) {
        char[] indexed = new char[s.length()];
        for (int i = 0; i < s.length(); i++) {
            indexed[i] = (char) alphabet.toIndex(s.charAt(i));
        }
        return indexed;
    }

    /**
     * Đọc toàn bộ nội dung văn bản từ file .docx.
     * @param filePath Đường dẫn file
     * @return Nội dung văn bản (đã chuẩn hóa khoảng trắng)
     */
    private String readDocxFile(String filePath) throws IOException {
        StringBuilder textBuilder = new StringBuilder();
        try (FileInputStream fis = new FileInputStream(new File(filePath));
             XWPFDocument document = new XWPFDocument(fis)) {
            
            // Duyệt qua từng đoạn văn (Paragraph)
            for (XWPFParagraph para : document.getParagraphs()) {
                String text = para.getText().trim();
                if (!text.isEmpty()) {
                    textBuilder.append(text).append(" ");
                }
            }
        }
        // Thay thế nhiều khoảng trắng liên tiếp bằng 1 dấu cách duy nhất
        return textBuilder.toString().replaceAll("\\s+", " ");
    }

    /**
     * Thực hiện tìm kiếm mẫu trong file .docx.
     * @param filePath Đường dẫn file
     * @return Vị trí bắt đầu (index) của mẫu trong file, hoặc -1 nếu lỗi/không tìm thấy.
     */
    public int searchInFile(String filePath) {
        try {
            // 1. Đọc file
            String text = readDocxFile(filePath);
            
            // 2. Chuyển văn bản sang dạng chỉ số
            char[] indexedText = toIndexedCharArray(text, alphabet);
            
            // 3. Gọi thuật toán tìm kiếm của lớp cha (KMP)
            return super.search(indexedText);
            
        } catch (IOException e) {
            System.err.println("Lỗi đọc file .docx: " + e.getMessage());
            return -1;
        }
    }

    /**
     * Hàm main chạy thử nghiệm.
     */
    public static void main(String[] args) {
        // Cấu hình tham số đầu vào
        // Mẫu tìm kiếm dài > 30 ký tự, trích từ file vanban.docx
        String pat = "Thuật toán KMP dùng tìm kiếm chuỗi"; 
        String filePath = "vanban.docx";

        // Kiểm tra file tồn tại
        if (!new File(filePath).exists()) {
            System.out.println("Không tìm thấy file: " + filePath);
            return;
        }

        System.out.println("Đang tìm kiếm mẫu: \"" + pat + "\"");
        System.out.println("Trong file: " + filePath + "...");

        // Khởi tạo và tìm kiếm
        KMPFile kmp = new KMPFile(pat, VietnameseAlphabet.VIETNAMESE_ALPHABET);
        int offset = kmp.searchInFile(filePath);

        // Hiển thị kết quả
        if (offset >= 0 && offset < Integer.MAX_VALUE) { // Integer.MAX_VALUE là giá trị trả về khi không tìm thấy của bản KMP gốc (hoặc N)
             try {
                // Đọc lại text để hiển thị minh họa
                String text = kmp.readDocxFile(filePath);
                
                // Kiểm tra lại logic "Không tìm thấy" của KMP gốc (trả về N)
                if (offset > text.length()) {
                     System.out.println("-> Kết quả: Không tìm thấy mẫu trong văn bản.");
                     return;
                }

                System.out.println("\n-> TÌM THẤY tại vị trí: " + offset);
                System.out.println("--------------------------------------------------");
                
                // In đoạn văn bản chứa mẫu (lấy ngữ cảnh 20 ký tự trước và sau)
                int start = Math.max(0, offset - 20);
                int end = Math.min(text.length(), offset + pat.length() + 20);
                String snippet = text.substring(start, end);
                
                System.out.println("Ngữ cảnh: \"..." + snippet + "...\"");
                
                // Minh họa căn chỉnh (Alignment)
                // Lưu ý: Chỉ in một đoạn ngắn để tránh tràn màn hình console
                System.out.println("\nMinh họa căn chỉnh:");
                String displayTxt = text.substring(offset, Math.min(text.length(), offset + 50));
                System.out.println("Text:    " + displayTxt + "...");
                System.out.println("Pattern: " + pat);
                
            } catch (IOException e) {
                e.printStackTrace();
            }
        } else {
            System.out.println("-> Kết quả: Không tìm thấy mẫu hoặc có lỗi xảy ra.");
        }
    }
}