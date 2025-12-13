import java.io.*;
import java.util.*;

public class TestLSD {
    public static void main(String[] args) { 
        try {
            // --- BƯỚC 1: ĐỌC DỮ LIỆU ---
            File file = new File("masinhvien.txt");
            if (!file.exists()) {
                System.out.println("Lỗi: Không tìm thấy file masinhvien.txt");
                return; // Dừng chương trình nếu không có file
            }

            Scanner scanner = new Scanner(file);
            List<Integer> listMSV = new ArrayList<>();

            System.out.println("--- Đang đọc file masinhvien.txt ---");
            while (scanner.hasNext()) {
                // Đọc từng token (từng chữ/số tách nhau bởi khoảng trắng hoặc xuống dòng)
                String token = scanner.next();
                
                // Kiểm tra xem token có phải là số không để tránh lỗi
                if (token.matches("\\d+")) {
                    // Chuyển chuỗi String sang số nguyên int
                    // (Để phục vụ yêu cầu sort số nguyên 32-bit)
                    listMSV.add(Integer.parseInt(token));
                }
            }
            scanner.close();

            // Chuyển từ List sang mảng int[] để đưa vào hàm sort
            int n = listMSV.size();
            int[] a = new int[n];
            for (int i = 0; i < n; i++) {
                a[i] = listMSV.get(i);
            }

            // In dữ liệu trước khi sắp xếp
            System.out.println("\n[Dữ liệu gốc]:");
            for (int msv : a) System.out.println(msv);

            // --- BƯỚC 2: GỌI THUẬT TOÁN LSD (SỐ NGUYÊN) ---
            // Gọi hàm sort(int[]) của lớp LSD
            LSD.sort(a);

            // --- BƯỚC 3: IN KẾT QUẢ ---
            System.out.println("\n[Kết quả sau khi sắp xếp LSD]:");
            for (int msv : a) {
                System.out.println(msv);
            }

        } catch (FileNotFoundException e) {
            System.out.println("Lỗi đọc file: " + e.getMessage());
        } catch (NumberFormatException e) {
            System.out.println("Lỗi dữ liệu: File chứa ký tự không phải số.");
        }
    }
}