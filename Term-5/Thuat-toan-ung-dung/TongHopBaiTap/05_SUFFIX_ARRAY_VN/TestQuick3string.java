import java.io.*;
import java.util.*;

public class TestQuick3string {
    public static void main(String[] args) { 
        try {
            File file = new File("hotensinhvien.txt");
            if (!file.exists()) {
                System.out.println("Lỗi: Thiếu file hotensinhvien.txt");
                return;
            }

            Scanner scanner = new Scanner(file, "UTF-8");
            List<String> list = new ArrayList<>();
            while (scanner.hasNextLine()) {
                String line = scanner.nextLine().trim();
                // Bỏ qua dòng trống và dòng chứa meta data 
                if (!line.isEmpty() && !line.startsWith("[")) {
                    list.add(line);
                }
            }
            scanner.close();

            String[] a = list.toArray(new String[0]);
            
            // Sử dụng bản QuickSort cho Tiếng Việt
            Quick3stringVietnamese sorter = new Quick3stringVietnamese();
            sorter.sort(a);
            
            System.out.println("--- KẾT QUẢ QUICK 3-WAY STRING SORT (TIẾNG VIỆT) ---");
            for (String s : a) {
                System.out.println(s);
            }

        } catch (FileNotFoundException e) {
            e.printStackTrace();
        }
    }
}