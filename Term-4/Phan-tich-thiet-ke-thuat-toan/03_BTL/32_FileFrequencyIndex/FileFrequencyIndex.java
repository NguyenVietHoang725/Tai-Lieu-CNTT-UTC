import java.io.File;
import java.io.IOException;

/**
 * FileFrequencyIndex:
 * Chỉ mục tần suất xuất hiện từ trong nhiều file.
 * Key: từ
 * Value: bảng tần suất xuất hiện theo file (fileName -> count)
 */
public class FileFrequencyIndex {

    public static void main(String[] args) throws IOException {
        if (args.length == 0) {
            System.out.println("Usage: java FileFrequencyIndex file1.txt file2.txt ...");
            return;
        }

        // ST từ -> ST fileName -> số lần xuất hiện
        ST<String, ST<String, Integer>> index = new ST<>();

        // Đọc từng file
        for (String filename : args) {
            File file = new File(filename);
            In in = new In(file);

            // Đọc từng từ trong file
            while (!in.isEmpty()) {
                String word = in.readString().toLowerCase();

                // Lấy bảng tần suất của từ này, nếu chưa có thì tạo mới
                ST<String, Integer> fileFreq = index.get(word);
                if (fileFreq == null) {
                    fileFreq = new ST<>();
                    index.put(word, fileFreq);
                }

                // Cập nhật số lần xuất hiện trong file hiện tại
                if (fileFreq.contains(filename)) {
                    fileFreq.put(filename, fileFreq.get(filename) + 1);
                } else {
                    fileFreq.put(filename, 1);
                }
            }
        }

        // Ví dụ: in ra tần suất xuất hiện của một số từ nhất định
        // hoặc bạn có thể thay bằng nhập từ khóa từ StdIn để tra cứu
        String[] queries = {"it", "the", "business"};

        for (String query : queries) {
            if (index.contains(query)) {
                System.out.println("Word: " + query);
                ST<String, Integer> fileFreq = index.get(query);
                for (String fileName : fileFreq.keys()) {
                    System.out.println("  " + fileName + ": " + fileFreq.get(fileName));
                }
            } else {
                System.out.println("Word: " + query + " not found in any file.");
            }
        }
    }
}
