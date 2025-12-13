import java.io.*;
import org.apache.poi.xwpf.usermodel.XWPFDocument;
import org.apache.poi.xwpf.usermodel.XWPFParagraph;
import java.util.ArrayList;
import java.util.List;

/**
 * Bài 11: Quản lý Bảng điểm Sinh viên (Nested TST).
 * Đọc dữ liệu từ file msvbd.docx và thực hiện các thao tác quản lý điểm.
 */
public class TestStudentTST {
    
    // Constructor private
    private TestStudentTST() { }

    public static void main(String[] args) {
        // Cấu hình file dữ liệu
        String filePath = args.length > 0 ? args[0] : "msvbd.docx";
        
        // Khởi tạo cấu trúc TST lồng nhau
        StudentTST manager = new StudentTST();
        
        System.out.println("Đang đọc dữ liệu từ file " + filePath + "...");

        // --- BƯỚC 1: Đọc file và Nạp dữ liệu ---
        try (FileInputStream fis = new FileInputStream(new File(filePath));
             XWPFDocument document = new XWPFDocument(fis)) {
            
            for (XWPFParagraph para : document.getParagraphs()) {
                String line = para.getText().trim();
                
                // Bỏ qua dòng trống hoặc dòng chứa metadata
                if (!line.isEmpty() && !line.startsWith("[")) {
                    // Dữ liệu mẫu: "231234698 Toán_rời_rạc 8.7"
                    // Tách bằng khoảng trắng
                    String[] parts = line.split("\\s+");
                    
                    if (parts.length >= 3) {
                        String msv = parts[0].trim();
                        String monHoc = parts[1].trim();
                        try {
                            Double diem = Double.parseDouble(parts[2].trim());
                            
                            // Nạp vào hệ thống
                            manager.putStudent(msv, monHoc, diem);
                            
                        } catch (NumberFormatException e) {
                            System.err.println("Lỗi định dạng điểm: " + line);
                        }
                    }
                }
            }
        } catch (IOException e) {
            System.err.println("Lỗi đọc file: " + e.getMessage());
            return;
        }

        System.out.println("Đã nạp dữ liệu xong. Số lượng sinh viên: " + manager.size());
        System.out.println("--------------------------------------------------\n");

        // --- BƯỚC 2: In Bảng điểm chi tiết và Tính GPA ---
        System.out.println("=== BẢNG ĐIỂM CHI TIẾT CỦA TỪNG SINH VIÊN ===");
        
        // Duyệt qua tất cả Mã sinh viên (Keys của Outer TST)
        for (String msv : manager.keys()) {
            System.out.println("Sinh viên: " + msv);
            
            Iterable<String> subjects = manager.getSubjects(msv);
            double totalScore = 0;
            int count = 0;
            
            // Duyệt qua tất cả Môn học của SV đó (Keys của Inner TST)
            for (String subject : subjects) {
                Double grade = manager.getGrade(msv, subject);
                System.out.printf("   - %-20s: %.1f\n", subject, grade);
                
                totalScore += grade;
                count++;
            }
            
            // Tính điểm trung bình
            if (count > 0) {
                double gpa = totalScore / count;
                System.out.printf("   => Điểm trung bình (GPA): %.2f\n", gpa);
            }
            System.out.println("----------------------------------------");
        }

        // --- BƯỚC 3: Tra cứu thử nghiệm ---
        // Thử tìm điểm của một sinh viên cụ thể
        String testMSV = "231230701"; 
        String testMon = "Toán_rời_rạc";
        Double diemTraCuu = manager.getGrade(testMSV, testMon);
        
        System.out.println("\n[Tra cứu nhanh]");
        if (diemTraCuu != null) {
            System.out.println("Điểm môn " + testMon + " của SV " + testMSV + " là: " + diemTraCuu);
        } else {
            System.out.println("Không tìm thấy dữ liệu cho SV " + testMSV + " môn " + testMon);
        }
        
        // --- BƯỚC 4: Tìm kiếm theo mã (Prefix Search) ---
        String prefix = "2312309";
        System.out.println("\n[Tìm kiếm nhóm] Các sinh viên có mã bắt đầu bằng '" + prefix + "':");
        for (String msv : manager.keysWithPrefix(prefix)) {
            // Chỉ in mã sinh viên ra để liệt kê
            System.out.print(msv + ", ");
        }
        System.out.println();
    }
}