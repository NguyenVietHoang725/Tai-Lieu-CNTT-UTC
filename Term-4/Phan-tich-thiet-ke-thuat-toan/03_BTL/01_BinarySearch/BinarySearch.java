import java.util.Arrays;
import java.io.*;

/**
 * Lớp {@code BinarySearch} cung cấp phương thức tĩnh để thực hiện tìm kiếm nhị phân
 * trên một mảng số nguyên đã được sắp xếp theo thứ tự tăng dần.
 * 
 * Mỗi lần lặp, nó loại bỏ một nửa mảng khỏi phạm vi tìm kiếm.
 * 
 * Tìm kiếm nhị phân có độ phức tạp thời gian là O(log n) trong trường hợp xấu nhất.
 * 
 * Tài liệu tham khảo: Algorithms, 4th Edition - Robert Sedgewick và Kevin Wayne.
 */
public class BinarySearch {

    /**
     * Constructor bị ẩn để ngăn việc khởi tạo lớp (vì đây là lớp tiện ích).
     */
    private BinarySearch() { }

    /**
     * Trả về chỉ số của phần tử {@code key} trong mảng {@code a} đã sắp xếp.
     * 
     * Độ phức tạp thời gian:
     * Tốt nhất (Best-case): O(1) — nếu phần tử nằm ở giữa ngay lần đầu.
     * Trung bình & Xấu nhất (Average & Worst-case): O(log n) — giảm kích thước tìm kiếm còn một nửa mỗi vòng lặp.
     * 
     * @param a Mảng số nguyên đã được sắp xếp theo thứ tự tăng dần.
     * @param key Giá trị cần tìm kiếm.
     * @return Vị trí của {@code key} trong mảng nếu tồn tại, ngược lại trả về -1.
     */
    public static int indexOf(int[] a, int key) {
        int lo = 0;
        int hi = a.length - 1;

        while (lo <= hi) {
            // Tìm phần tử ở giữa
            int mid = lo + (hi - lo) / 2;

            if (key < a[mid]) hi = mid - 1; // Loại bỏ nửa phải
            else if (key > a[mid]) lo = mid + 1; // Loại bỏ nửa trái
            else return mid; // Tìm thấy
        }

        return -1; // Không tìm thấy
    }

    /**
     * Phương thức tìm kiếm nhị phân cũ, được giữ lại để tương thích.
     * 
     * Độ phức tạp thời gian: Thực chất là một alias gọi lại indexOf, nên cùng độ phức tạp: O(log n).
     * 
     * @deprecated Đã thay thế bằng {@link #indexOf(int[], int)} để rõ nghĩa hơn.
     * @param key Giá trị cần tìm.
     * @param a Mảng đã sắp xếp theo thứ tự tăng dần.
     * @return Vị trí của {@code key} nếu tồn tại, ngược lại trả về -1.
     */
    @Deprecated
    public static int rank(int key, int[] a) {
        return indexOf(a, key);
    }

    /**
     * Hàm main đọc dữ liệu từ file whitelist và từ đầu vào chuẩn (stdin),
     * sau đó in ra các số không xuất hiện trong whitelist.
     * 
     * @param args Tham số dòng lệnh, trong đó {@code args[0]} là tên file whitelist.
     * @throws IOException Nếu có lỗi khi đọc file.
     */
    public static void main(String[] args) throws IOException {
        // Tùy chỉnh dòng sau nếu cần kiểm thử bằng file khác
        System.setIn(new FileInputStream(new File("tinyAllowList.txt")));

        // Đọc mảng whitelist từ file
        In in = new In(args[0]);
        int[] whitelist = in.readAllInts();
        Arrays.sort(whitelist); // Sắp xếp mảng để thực hiện tìm kiếm nhị phân

        // Đọc từng số nguyên từ đầu vào; in ra nếu không có trong whitelist
        while (!StdIn.isEmpty()) {
            int key = StdIn.readInt();
            if (BinarySearch.indexOf(whitelist, key) == -1)
                StdOut.println(key);
        }
    }
}
