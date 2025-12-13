import java.io.*;
import org.apache.poi.xwpf.usermodel.XWPFDocument;
import org.apache.poi.xwpf.usermodel.XWPFParagraph;
import java.util.ArrayList;
import java.util.List;

/**
 * Bài 10: Quản lý Họ tên và Điểm trung bình sinh viên sử dụng VietnameseTST.
 * * Chương trình thực hiện:
 * 1. Đọc file .docx chứa danh sách sinh viên.
 * 2. Tách tên và điểm, nạp vào cây VietnameseTST.
 * 3. Thực hiện các thao tác tra cứu và in danh sách đã sắp xếp.
 */
public class TestVNTST {

    // Constructor private
    private TestVNTST() { }

    public static void main(String[] args) {
        // Cấu hình đường dẫn file dữ liệu
        // Bạn có thể truyền từ dòng lệnh hoặc gán trực tiếp
        String filePath = args.length > 0 ? args[0] : "dssv.docx";

        // Khởi tạo cây TST với Key=String (Tên), Value=Double (Điểm)
        VietnameseTST<Double> studentTST = new VietnameseTST<Double>();
        
        // Danh sách phụ để lưu tên gốc (dùng cho việc kiểm tra nhanh nếu cần)
        List<String> names = new ArrayList<>();

        System.out.println("Đang đọc dữ liệu từ file: " + filePath + "...");

        // --- BƯỚC 1: Đọc file và Nạp dữ liệu vào TST ---
        try (FileInputStream fis = new FileInputStream(new File(filePath));
             XWPFDocument document = new XWPFDocument(fis)) {
            
            for (XWPFParagraph para : document.getParagraphs()) {
                String line = para.getText().trim();
                
                // Bỏ qua dòng trống hoặc dòng chứa metadata 
                if (!line.isEmpty() && !line.startsWith("[")) {
                    
                    // KỸ THUẬT TÁCH CHUỖI (REGEX):
                    // Tách chuỗi dựa vào khoảng trắng đứng ngay trước một con số.
                    // Ví dụ: "Nguyễn Văn An 7.92" 
                    // -> Phần 1: "Nguyễn Văn An"
                    // -> Phần 2: "7.92"
                    String[] parts = line.split("\\s+(?=[0-9])"); 
                    
                    if (parts.length >= 2) {
                        String name = parts[0].trim();
                        try {
                            Double grade = Double.parseDouble(parts[1].trim());
                            
                            // Lưu vào danh sách và nạp vào TST
                            names.add(name);
                            
                            // put(Key, Value): Key là Họ tên, Value là Điểm
                            studentTST.put(name, grade);
                            
                        } catch (NumberFormatException e) {
                            System.err.println("Lỗi định dạng điểm số tại dòng: " + line);
                        } catch (IllegalArgumentException e) {
                            System.err.println("Tên chứa ký tự không hợp lệ: " + name);
                        }
                    }
                }
            }
        } catch (IOException e) {
            System.err.println("Không thể đọc file " + filePath + ": " + e.getMessage());
            return;
        }

        System.out.println("Đã nạp thành công " + studentTST.size() + " sinh viên vào hệ thống.\n");

        // --- BƯỚC 2: In danh sách sinh viên (Đã sắp xếp) ---
        // Phương thức keys() của TST sẽ duyệt cây theo thứ tự từ điển.
        // Nhờ VietnameseAlphabet, "Dũng" sẽ đứng trước "Đạt".
        System.out.println("=== DANH SÁCH SINH VIÊN (Sắp xếp A-Z Tiếng Việt) ===");
        System.out.printf("%-30s | %s\n", "Họ và Tên", "Điểm TB");
        System.out.println("---------------------------------------------");
        
        for (String name : studentTST.keys()) {
            System.out.printf("%-30s | %.2f\n", name, studentTST.get(name));
        }
        System.out.println("---------------------------------------------\n");

        // --- BƯỚC 3: Các chức năng tìm kiếm ---

        // 3.1. Tra cứu điểm của một sinh viên cụ thể
        if (!names.isEmpty()) {
            String target = names.get(0); // Lấy thử người đầu tiên trong file gốc
            System.out.println("[Tra cứu] Điểm của '" + target + "': " + studentTST.get(target));
        }

        // 3.2. Tìm kiếm theo Họ (Prefix Search)
        // Ví dụ: Tìm tất cả sinh viên họ "Nguyễn"
        String prefix = "Nguyễn";
        System.out.println("\n[Tìm kiếm] Các sinh viên họ '" + prefix + "':");
        for (String name : studentTST.keysWithPrefix(prefix)) {
            System.out.println(" - " + name + ": " + studentTST.get(name));
        }

        // 3.3. Tìm kiếm Tiền tố dài nhất (Longest Prefix)
        // Giả sử ta nhập thừa một đoạn text phía sau tên, hệ thống vẫn nhận diện được tên đúng dài nhất.
        if (!names.isEmpty()) {
            String query = names.get(0) + " Lớp K65"; // Giả lập dữ liệu nhập dư
            String match = studentTST.longestPrefixOf(query);
            System.out.println("\n[Longest Prefix] Tìm thấy tên trong chuỗi '" + query + "': " + match);
        }
    }
}