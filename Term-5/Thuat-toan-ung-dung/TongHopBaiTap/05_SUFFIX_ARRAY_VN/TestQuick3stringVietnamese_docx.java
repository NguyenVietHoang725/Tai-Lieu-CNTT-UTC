/******************************************************************************
 *  Compilation:  javac TestQuick3stringVietnamese_docx.java
 *  Execution:    java TestQuick3stringVietnamese_docx
 *  Dependencies: StdOut.java Quick3stringVietnamese.java Alphabet.java VietnameseAlphabet.java org.apache.poi.xwpf.usermodel.XWPFDocument
 *
 *  Reads a list of strings from a .docx file and sorts them using 3-way radix quicksort
 *  with the Vietnamese alphabet.
 *
 ******************************************************************************/

import java.io.*;
import java.util.*;
import org.apache.poi.xwpf.usermodel.XWPFDocument;
import org.apache.poi.xwpf.usermodel.XWPFParagraph;

/**
 * Lớp {@code TestQuick3stringVietnamese_docx} đọc danh sách chuỗi từ file .docx và sắp xếp chúng
 * sử dụng thuật toán 3-way radix quicksort với bảng chữ cái tiếng Việt.
 *
 * @author (your name)
 * @version 1.0
 */
public class TestQuick3stringVietnamese_docx {
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
        } catch (IOException e) {
            System.err.println("Lỗi khi đọc file .docx: " + e.getMessage());
            return;
        }
        
        // Chuyển danh sách thành mảng chuỗi
        String[] a = lines.toArray(new String[0]);
        int n = a.length;
        
        // Sắp xếp mảng sử dụng Quick3stringVietnamese
        Quick3stringVietnamese sorter = new Quick3stringVietnamese();
        sorter.sort(a);
        
        // In kết quả
        for (int i = 0; i < n; i++) {
            StdOut.println(a[i]);
        }
    }
}