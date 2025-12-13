import java.io.*;
import org.apache.poi.xwpf.usermodel.XWPFDocument;
import org.apache.poi.xwpf.usermodel.XWPFParagraph;

/**
 * Chương trình kiểm thử: Đọc hai file báo cáo .docx và tìm đoạn trùng lặp.
 * Sử dụng thư viện Apache POI để đọc file Word.
 */
public class TestLCS {

    private TestLCS() { }

    public static void main(String[] args) {
        // Cấu hình tên file trực tiếp trong code để dễ chạy
        String file1Path = "baocao1.docx";
        String file2Path = "baocao2.docx";

        System.out.println("Đang đọc file " + file1Path + " và " + file2Path + "...");

        // --- BƯỚC 1: Đọc nội dung file báo cáo 1 ---
        StringBuilder text1Builder = new StringBuilder();
        try (FileInputStream fis = new FileInputStream(new File(file1Path));
             XWPFDocument document = new XWPFDocument(fis)) {
            for (XWPFParagraph para : document.getParagraphs()) {
                String text = para.getText().trim();
                if (!text.isEmpty()) {
                    text1Builder.append(text).append(" ");
                }
            }
        } catch (IOException e) {
            System.err.println("Lỗi đọc file " + file1Path + ": " + e.getMessage());
            return;
        }

        // --- BƯỚC 2: Đọc nội dung file báo cáo 2 ---
        StringBuilder text2Builder = new StringBuilder();
        try (FileInputStream fis = new FileInputStream(new File(file2Path));
             XWPFDocument document = new XWPFDocument(fis)) {
            for (XWPFParagraph para : document.getParagraphs()) {
                String text = para.getText().trim();
                if (!text.isEmpty()) {
                    text2Builder.append(text).append(" ");
                }
            }
        } catch (IOException e) {
            System.err.println("Lỗi đọc file " + file2Path + ": " + e.getMessage());
            return;
        }

        // --- BƯỚC 3: Chuẩn hóa dữ liệu ---
        // Xóa khoảng trắng thừa để so sánh chính xác nội dung
        String text1 = text1Builder.toString().trim().replaceAll("\\s+", " ");
        String text2 = text2Builder.toString().trim().replaceAll("\\s+", " ");

        System.out.println("Độ dài văn bản 1: " + text1.length() + " ký tự.");
        System.out.println("Độ dài văn bản 2: " + text2.length() + " ký tự.");
        System.out.println("Đang phân tích tìm điểm tương đồng...");

        // --- BƯỚC 4: Tìm xâu con chung dài nhất ---
        String lcs = LongestCommonSubstring.lcs(text1, text2);

        // --- BƯỚC 5: Xuất kết quả ---
        System.out.println("\n=== KẾT QUẢ PHÂN TÍCH TRÙNG LẶP ===");
        if (lcs.isEmpty()) {
            System.out.println("Không tìm thấy đoạn văn bản chung nào.");
        } else {
            System.out.println("Độ dài đoạn trùng lặp: " + lcs.length() + " ký tự.");
            System.out.println("\n--- Nội dung đoạn trùng lặp ---");
            // Cắt ngắn nếu quá dài khi in ra màn hình console
            if (lcs.length() > 200) {
                 System.out.println(lcs.substring(0, 200) + " ... [còn tiếp]");
            } else {
                 System.out.println(lcs);
            }
            System.out.println("--------------------------------");
        }
    }
}