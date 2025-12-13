import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;

/**
 * Chương trình kiểm thử: Đọc file source code, tìm đoạn lặp và gợi ý tạo hàm.
 */
public class TestCodeRefactoring {
    public static void main(String[] args) {
        String fileName = "program.c";
        
        // 1. Đọc toàn bộ nội dung file code
        String text;
        try {
            // Lưu ý: Files.readString yêu cầu Java 11 trở lên. 
            // Nếu dùng Java cũ hơn, hãy dùng new String(Files.readAllBytes(Paths.get(fileName)))
            text = Files.readString(Path.of(fileName));
            System.out.println("Đã đọc file: " + fileName);
        } catch (IOException e) {
            System.out.println("Lỗi: Không thể đọc file " + fileName);
            return;
        }

        // 2. Chuẩn hóa chuỗi (Code Normalization)
        // Rất quan trọng: Thay thế tất cả khoảng trắng, xuống dòng, tab thành 1 dấu cách đơn.
        // Lý do: Để thuật toán không bị ảnh hưởng bởi việc thụt đầu dòng hay xuống dòng khác nhau
        // mà tập trung vào nội dung logic của code.
        String cleanedText = text.replaceAll("\\s+", " ");

        // 3. Tìm xâu con lặp dài nhất (LRS)
        String result = LongestRepeatedSubstring.lrs(cleanedText);

        System.out.println("\n=== KẾT QUẢ PHÂN TÍCH ===");
        System.out.println("Độ dài đoạn lặp lớn nhất: " + result.length() + " ký tự");
        
        // In đoạn mã lặp ra (cắt ngắn nếu quá dài để dễ nhìn trong console)
        String displayStr = result.length() > 100 ? result.substring(0, 100) + "..." : result;
        System.out.println("Nội dung đoạn lặp (đã chuẩn hóa): \n[" + displayStr + "]");

        // 4. Phân tích và gợi ý
        if (isLikelyCodeBlock(result)) {
            System.out.println("\n=> PHÁT HIỆN: Có đoạn logic lặp lại đáng kể.");
            System.out.println("=> GỢI Ý: Bạn nên tách đoạn code này thành một hàm riêng.");
            
            suggestFunctionSkeleton(result);
        } else {
            System.out.println("\n=> Không phát hiện đoạn code lặp nào đủ lớn hoặc có cấu trúc rõ ràng.");
        }
    }

    // Kiểm tra sơ bộ xem chuỗi lặp có giống một khối lệnh C không
    // (Chứa dấu ngoặc nhọn hoặc các từ khóa vòng lặp)
    private static boolean isLikelyCodeBlock(String substring) {
        return (substring.contains("{") && substring.contains("}")) 
            || substring.contains("for") 
            || substring.contains("while")
            || substring.contains("if");
    }

    // In ra khung hàm gợi ý
    private static void suggestFunctionSkeleton(String substring) {
        System.out.println("\n--- Khung hàm gợi ý (Pseudo-code) ---");
        System.out.println("void extractedMethod(...) {");
        // Giả lập việc format lại code từ chuỗi đã clean
        System.out.println("    " + substring.replace("}", "}\n    ").replace("{", "{\n        "));
        System.out.println("}");
        System.out.println("-------------------------------------");
    }
}