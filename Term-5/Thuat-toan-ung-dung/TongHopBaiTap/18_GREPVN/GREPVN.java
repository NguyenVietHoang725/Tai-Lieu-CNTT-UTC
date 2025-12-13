import java.io.File; // Thêm thư viện để kiểm tra file

/**
 * Bài 18: Chương trình GREP hỗ trợ Tiếng Việt (Generalized Regular Expression Parser).
 * * Chức năng:
 * 1. Nhận vào một biểu thức chính quy (Regex) và tên file.
 * 2. Đọc từng dòng của file.
 * 3. Sử dụng NFAVN để kiểm tra xem dòng đó có chứa chuỗi con khớp với Regex không.
 * 4. In ra các dòng thỏa mãn.
 */
public class GREPVN {

    // Không cho phép khởi tạo đối tượng
    private GREPVN() { }

    /**
     * Hàm main thực thi chương trình.
     * @param args tham số dòng lệnh: [0] = Regex, [1] = Tên file
     */
    public static void main(String[] args) {
        // 1. Kiểm tra tham số đầu vào
        if (args.length < 2) {
            System.err.println("Cách dùng: java GREPVN <regex> <filename>");
            System.err.println("Ví dụ: java GREPVN \"(Nguyễn|Trần)\" tinyL.txt");
            return;
        }

        String pattern = args[0];
        String fileName = args[1];

        // 2. Bao regex trong (.* ... .*) để tìm kiếm dạng substring (chuỗi con)
        // Nếu không có bước này, NFA sẽ so khớp toàn bộ dòng (exact match)
        String regexp = "(.*" + pattern + ".*)";

        try {
            // 3. Khởi tạo bộ máy NFA Tiếng Việt
            // NFAVN sẽ chuyển đổi các ký tự Unicode trong regex sang index tương ứng
            NFAVN nfa = new NFAVN(regexp);

            // 4. Đọc file sử dụng thư viện In (algs4) hoặc Scanner chuẩn
            // Ở đây dùng In để tương thích với giáo trình, nhưng có thể thay bằng Scanner
            In in = new In(fileName);

            System.out.println("Đang tìm kiếm mẫu: " + pattern);
            System.out.println("Trong file: " + fileName);
            System.out.println("-----------------------------");

            while (in.hasNextLine()) {
                String line = in.readLine();
                
                // 5. Kiểm tra và in kết quả
                try {
                    if (nfa.recognizes(line)) {
                        System.out.println(line);
                    }
                } catch (IllegalArgumentException e) {
                    // Bỏ qua các dòng chứa ký tự lạ không có trong bảng chữ cái VN
                    // (Ví dụ: ký tự tab, icon, hoặc ngôn ngữ khác)
                }
            }
            
        } catch (IllegalArgumentException e) {
            System.err.println("Lỗi Regex hoặc File: " + e.getMessage());
        } catch (Exception e) {
            System.err.println("Không thể đọc file: " + fileName);
        }
    }
}