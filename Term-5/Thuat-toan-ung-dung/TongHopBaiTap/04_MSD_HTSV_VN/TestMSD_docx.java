import java.io.*;
import java.util.*;
import org.apache.poi.xwpf.usermodel.XWPFDocument;
import org.apache.poi.xwpf.usermodel.XWPFParagraph;

/**
 * Lớp {@code TestMSD_docx} đọc danh sách chuỗi từ file .docx và sắp xếp chúng
 * sử dụng thuật toán MSD radix sort.
 *
 * @author (your name)
 * @version (a version number or a date)
 */
public class TestMSD_docx {
    public static void main(String[] args) throws IOException {
        // Đường dẫn tới file .docx
        String filePath = "hotensinhvien.docx";
        
        // Đọc file .docx
        ArrayList<String> lines = new ArrayList<>();
        try (FileInputStream fis = new FileInputStream(new File(filePath));
             XWPFDocument document = new XWPFDocument(fis)) {
            // Lấy danh sách các đoạn văn bản (paragraphs)
            List<XWPFParagraph> paragraphs = document.getParagraphs();
            for (XWPFParagraph para : paragraphs) {
                String text = para.getText().trim();
                if (!text.isEmpty()) {
                    lines.add(text);
                }
            }
        }
        
        // Chuyển danh sách thành mảng chuỗi
        String[] a = lines.toArray(new String[0]);
        int n = a.length;
        
        // Sắp xếp mảng sử dụng MSD radix sort
        MSD.sort(a);
        
        // In kết quả
        for (int i = 0; i < n; i++) {
            StdOut.println(a[i]);
        }
    }
}
