import java.io.*;
import java.util.*;

public class TestMSD {
    public static void main(String[] args) { 
        try {
            // --- BƯỚC 1: ĐỌC DỮ LIỆU ---
            File file = new File("hotensinhvien.txt");
            if (!file.exists()) {
                System.out.println("Lỗi: Không tìm thấy file hotensinhvien.txt");
                return;
            }

            Scanner scanner = new Scanner(file);
            List<String> lines = new ArrayList<>();
            
            System.out.println("--- Dữ liệu gốc ---");
            // Đọc từng dòng (Vì họ tên có khoảng trắng nên dùng nextLine())
            while (scanner.hasNextLine()) {
                String line = scanner.nextLine().trim();
                // Lọc bỏ dòng trống và các thẻ meta như nếu có
                if (!line.isEmpty() && !line.startsWith("[")) { 
                    lines.add(line);
                    System.out.println(line);
                }
            }
            scanner.close();

            // Chuyển List sang mảng String[] để đưa vào thuật toán MSD
            String[] a = lines.toArray(new String[0]);
            
            // --- BƯỚC 2: SẮP XẾP BẰNG MSD ---
            // Sắp xếp theo thứ tự từ điển (A -> Z)
            MSD.sort(a);
            
            // --- BƯỚC 3: IN KẾT QUẢ ---
            System.out.println("\n--- Kết quả sau khi sắp xếp (MSD Sort) ---");
            for (String s : a) {
                System.out.println(s);
            }

        } catch (FileNotFoundException e) {
            System.out.println("Lỗi đọc file: " + e.getMessage());
        }
    }
}