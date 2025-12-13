/******************************************************************************
 * Compilation:  javac -cp ".;poi-ooxml-5.x.x.jar;poi-5.x.x.jar;commons-compress.jar;xmlbeans.jar" KWIK.java
 * Execution:    java -cp ... KWIK cotich.docx 20
 * Dependencies: StdIn.java StdOut.java SuffixArrayXVN.java VietnameseAlphabet.java
 * Thư viện Apache POI (để đọc file .docx)
 *
 * Mô tả: Tìm kiếm từ khóa trong ngữ cảnh (Keyword-in-Context search)
 * hỗ trợ Tiếng Việt, đọc dữ liệu trực tiếp từ file .docx.
 *
 ******************************************************************************/

import java.io.*;
import java.util.*;
import org.apache.poi.xwpf.usermodel.XWPFDocument;
import org.apache.poi.xwpf.usermodel.XWPFParagraph;

/**
 * Lớp {@code KWIK} cung cấp một client sử dụng {@link SuffixArrayXVN} để tính toán
 * và tìm kiếm tất cả các lần xuất hiện của một từ khóa trong văn bản từ file .docx,
 * kèm theo ngữ cảnh xung quanh (context).
 * * Thuật toán sử dụng Mảng hậu tố (Suffix Array) để đạt tốc độ tìm kiếm tối ưu.
 */
public class KWIK {

    // Constructor private để ngăn khởi tạo đối tượng
    private KWIK() { }

    /**
     * Hàm main thực hiện các bước:
     * 1. Đọc văn bản từ file .docx (tham số dòng lệnh thứ nhất).
     * 2. Đọc số nguyên k (tham số dòng lệnh thứ hai) quy định độ dài ngữ cảnh.
     * 3. Xây dựng Suffix Array cho văn bản Tiếng Việt.
     * 4. Nhận từ khóa từ bàn phím và in ra các đoạn văn bản chứa từ khóa đó.
     *
     * @param args tham số dòng lệnh: [0] đường dẫn file .docx, [1] độ dài ngữ cảnh (context)
     */
    public static void main(String[] args) {
        // --- BƯỚC 1: Kiểm tra tham số đầu vào ---
        if (args.length < 2) {
            System.err.println("Cách dùng: java KWIK <file.docx> <độ_dài_ngữ_cảnh>");
            return;
        }

        String filePath = args[0];
        int context;
        try {
            context = Integer.parseInt(args[1]);
        } catch (NumberFormatException e) {
            System.err.println("Lỗi: Tham số thứ hai phải là một số nguyên (ví dụ: 20).");
            return;
        }

        // --- BƯỚC 2: Đọc dữ liệu từ file .docx ---
        System.out.println("Đang đọc file " + filePath + "...");
        StringBuilder textBuilder = new StringBuilder();
        
        try (FileInputStream fis = new FileInputStream(new File(filePath));
             XWPFDocument document = new XWPFDocument(fis)) {
            
            // Lấy danh sách các đoạn văn (paragraphs) từ file Word
            List<XWPFParagraph> paragraphs = document.getParagraphs();
            for (XWPFParagraph para : paragraphs) {
                String paraText = para.getText().trim();
                if (!paraText.isEmpty()) {
                    // Nối các đoạn văn lại, ngăn cách bằng dấu cách
                    textBuilder.append(paraText).append(" ");
                }
            }
        } catch (IOException e) {
            System.err.println("Lỗi khi đọc file .docx: " + e.getMessage());
            return;
        }

        // Chuẩn hóa văn bản: Thay thế mọi khoảng trắng thừa (tab, newline) bằng 1 dấu cách duy nhất
        // Điều này giúp việc tìm kiếm không bị ảnh hưởng bởi định dạng dòng
        String text = textBuilder.toString().replaceAll("\\s+", " ");
        int n = text.length();

        System.out.println("Đã đọc xong " + n + " ký tự.");
        System.out.println("Đang xây dựng Suffix Array (Vui lòng đợi)...");

        // --- BƯỚC 3: Xây dựng Suffix Array (Hỗ trợ Tiếng Việt) ---
        // Sử dụng SuffixArrayXVN để sắp xếp các hậu tố theo chuẩn bảng chữ cái VN
        SuffixArrayXVN sa = new SuffixArrayXVN(text);
        
        System.out.println("Hoàn tất! Hãy nhập từ khóa cần tìm (Nhấn Ctrl+C để thoát):");
        System.out.println("-----------------------------------------------------------");

        // --- BƯỚC 4: Vòng lặp tìm kiếm ---
        // Sử dụng StdIn để đọc từ khóa từ console (hoặc có thể thay bằng Scanner)
        while (StdIn.hasNextLine()) {
            String query = StdIn.readLine().trim();
            if (query.isEmpty()) continue;

            boolean found = false;
            
            // sa.rank(query): Tìm vị trí bắt đầu của hậu tố khớp với query trong mảng đã sắp xếp
            for (int i = sa.rank(query); i < n; i++) {
                int from1 = sa.index(i);
                int to1 = Math.min(n, from1 + query.length());
                
                // Kiểm tra xem hậu tố tại vị trí này có thực sự bắt đầu bằng từ khóa không
                // Nếu không trùng khớp nữa nghĩa là đã hết vùng chứa từ khóa (vì mảng đã sort)
                if (!query.equals(text.substring(from1, to1))) break;
                
                found = true;

                // Tính toán vị trí bắt đầu và kết thúc của ngữ cảnh (Context)
                // Đảm bảo không vượt quá biên của chuỗi văn bản
                int from2 = Math.max(0, sa.index(i) - context);
                int to2 = Math.min(n, sa.index(i) + context + query.length());
                
                // In ra đoạn văn bản ngữ cảnh
                // Có thể làm đẹp bằng cách in đậm từ khóa hoặc đánh dấu [...]
                StdOut.println("[" + from1 + "] ..." + text.substring(from2, to2) + "...");
            }
            
            if (!found) {
                StdOut.println("Không tìm thấy từ khóa: \"" + query + "\"");
            }
            StdOut.println("-----------------------------------------------------------");
        }
    }
}