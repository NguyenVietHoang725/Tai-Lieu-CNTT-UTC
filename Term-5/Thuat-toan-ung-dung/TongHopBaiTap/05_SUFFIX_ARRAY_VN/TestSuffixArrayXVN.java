/******************************************************************************
 *  Compilation:  javac TestSuffixArrayXVN.java
 *  Execution:    java TestSuffixArrayXVN
 *  Dependencies: SuffixArrayXVN.java VietnameseAlphabet.java SuffixArrayX.java Alphabet.java StdOut.java
 *                org.apache.poi.xwpf.usermodel.XWPFDocument
 *                org.apache.poi.xwpf.usermodel.XWPFParagraph
 *
 *  A test class that reads strings from a .docx file and constructs suffix arrays
 *  for each string using SuffixArrayXVN, sorting according to Vietnamese alphabet rules.
 *
 ******************************************************************************/

import java.io.*;
import java.util.*;
import org.apache.poi.xwpf.usermodel.XWPFDocument;
import org.apache.poi.xwpf.usermodel.XWPFParagraph;

/**
 * Lớp {@code TestSuffixArrayXVN} đọc danh sách chuỗi từ file .docx và xây dựng
 * mảng hậu tố cho mỗi chuỗi sử dụng {@code SuffixArrayXVN}, sắp xếp theo quy tắc
 * bảng chữ cái tiếng Việt.
 */
public class TestSuffixArrayXVN {
    public static void main(String[] args) throws IOException {
        // Đường dẫn tới file .docx
        String filePath = "test_vietnamese.docx";
        
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
            StdOut.println("Lỗi khi đọc file .docx: " + e.getMessage());
            return;
        }
        
        // Xử lý từng chuỗi
        for (String text : lines) {
            try {
                // Tạo mảng hậu tố cho chuỗi
                SuffixArrayXVN suffixArray = new SuffixArrayXVN(text);
                
                // In thông tin về chuỗi
                StdOut.println("\nChuỗi: \"" + text + "\"");
                StdOut.println("  i ind lcp rnk select");
                StdOut.println("---------------------------");
                
                // In danh sách các hậu tố
                for (int i = 0; i < suffixArray.length(); i++) {
                    int index = suffixArray.index(i);
                    String ith = "\"" + text.substring(index, Math.min(index + 50, text.length())) + "\"";
                    int rank = suffixArray.rank(text.substring(index));
                    if (i == 0) {
                        StdOut.printf("%3d %3d %3s %3d %s\n", i, index, "-", rank, ith);
                    } else {
                        int lcp = suffixArray.lcp(i);
                        StdOut.printf("%3d %3d %3d %3d %s\n", i, index, lcp, rank, ith);
                    }
                }
            } catch (IllegalArgumentException e) {
                StdOut.println("Lỗi khi xử lý chuỗi \"" + text + "\": " + e.getMessage());
            }
        }
    }
}

/******************************************************************************
 *  Copyright 2025, based on algs4.jar by Robert Sedgewick and Kevin Wayne.
 *
 *  This file is part of a test suite for Vietnamese suffix array processing.
 *
 *  Licensed under the GNU General Public License, version 3 or later.
 ******************************************************************************/