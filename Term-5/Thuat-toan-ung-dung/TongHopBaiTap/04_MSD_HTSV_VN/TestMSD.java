import java.io.*;
import java.util.*;

public class TestMSD {
    public static void main(String[] args) { 
        try {
            System.out.println("Đang đọc file hotensinhvien.txt...");
            File file = new File("hotensinhvien.txt");
            if (!file.exists()) {
                System.out.println("Lỗi: Không tìm thấy file hotensinhvien.txt");
                return;
            }

            // Dùng Scanner đọc file, hỗ trợ UTF-8
            Scanner scanner = new Scanner(file, "UTF-8");
            List<String> lines = new ArrayList<>();
            
            while (scanner.hasNextLine()) {
                String line = scanner.nextLine().trim();
                // Lọc bỏ dòng trống và các thẻ meta như nếu có
                if (!line.isEmpty() && !line.startsWith("[")) {
                    lines.add(line);
                }
            }
            scanner.close();

            String[] a = lines.toArray(new String[0]);
            
            System.out.println("--- Trước khi sắp xếp ---");
            for(String s : a) System.out.println(s);

            // GỌI THUẬT TOÁN MSD
            MSD.sort(a);
            
            System.out.println("\n--- Sau khi sắp xếp (Quy tắc Tiếng Việt) ---");
            for (String s : a) {
                System.out.println(s);
            }

        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}