/**
 * Lớp MSD (Most Significant Digit Radix Sort)
 * Dùng để sắp xếp chuỗi (String) hoặc số nguyên 32-bit (int).
 * Thuật toán này sử dụng đệ quy, xử lý từ ký tự/byte đầu tiên đến cuối cùng.
 */
public class MSD {
    private static final int BITS_PER_BYTE =   8;
    private static final int BITS_PER_INT  =  32;   
    private static final int R             = 256;   // Kích thước bảng mã (Extended ASCII)
    private static final int CUTOFF        =  15;   // Ngưỡng chuyển sang Insertion Sort

    private MSD() { } 

    /**
     * Sắp xếp mảng String (Sử dụng cho bài toán Họ tên sinh viên).
     */
    public static void sort(String[] a) {
        int n = a.length;
        String[] aux = new String[n];
        sort(a, 0, n-1, 0, aux);
    }

    // Trả về ký tự thứ d của chuỗi s. Nếu d == độ dài chuỗi thì trả về -1 (đánh dấu kết thúc)
    private static int charAt(String s, int d) {
        if (d == s.length()) return -1;
        return s.charAt(d);
    }

    // Hàm đệ quy sắp xếp mảng String
    // a[lo..hi]: Đoạn mảng đang xét
    // d: Ký tự thứ d đang dùng để so sánh
    private static void sort(String[] a, int lo, int hi, int d, String[] aux) {

        // 1. Tối ưu: Dùng Insertion Sort cho mảng con nhỏ
        if (hi <= lo + CUTOFF) {
            insertion(a, lo, hi, d);
            return;
        }

        // 2. Đếm tần suất (Frequency counts)
        int[] count = new int[R+2];
        for (int i = lo; i <= hi; i++) {
            int c = charAt(a[i], d);
            count[c+2]++; // Dịch chỉ số +2 để chứa cả trường hợp -1
        }

        // 3. Cộng dồn để tính vị trí (Transform counts to indices)
        for (int r = 0; r < R+1; r++)
            count[r+1] += count[r];

        // 4. Phân phối dữ liệu vào mảng phụ (Distribute)
        for (int i = lo; i <= hi; i++) {
            int c = charAt(a[i], d);
            aux[count[c+1]++] = a[i];
        }

        // 5. Sao chép ngược lại (Copy back)
        for (int i = lo; i <= hi; i++) 
            a[i] = aux[i - lo];

        // 6. Đệ quy cho các nhóm con (trừ nhóm ký tự kết thúc -1)
        for (int r = 0; r < R; r++)
            sort(a, lo + count[r], lo + count[r+1] - 1, d+1, aux);
    }

    // Insertion sort tối ưu cho chuỗi (bắt đầu so sánh từ ký tự d)
    private static void insertion(String[] a, int lo, int hi, int d) {
        for (int i = lo; i <= hi; i++)
            for (int j = i; j > lo && less(a[j], a[j-1], d); j--)
                exch(a, j, j-1);
    }

    // Hoán đổi vị trí 2 phần tử
    private static void exch(String[] a, int i, int j) {
        String temp = a[i];
        a[i] = a[j];
        a[j] = temp;
    }

    // So sánh chuỗi v và w bắt đầu từ ký tự d
    private static boolean less(String v, String w, int d) {
        for (int i = d; i < Math.min(v.length(), w.length()); i++) {
            if (v.charAt(i) < w.charAt(i)) return true;
            if (v.charAt(i) > w.charAt(i)) return false;
        }
        return v.length() < w.length();
    }


   /**
     * Sắp xếp mảng số nguyên 32-bit (Yêu cầu tìm hiểu thêm của đề bài).
     * Hiện tại giả định số nguyên không âm.
     */
    public static void sort(int[] a) {
        int n = a.length;
        int[] aux = new int[n];
        sort(a, 0, n-1, 0, aux);
    }

    // Hàm đệ quy sắp xếp int
    private static void sort(int[] a, int lo, int hi, int d, int[] aux) {
        if (hi <= lo + CUTOFF) {
            insertion(a, lo, hi, d); // Cần cài đặt thêm hàm insertion cho int
            return;
        }

        int[] count = new int[R+1];
        int mask = R - 1;   
        // Tính vị trí dịch bit: Bắt đầu từ byte cao nhất (trái sang phải)
        int shift = BITS_PER_INT - BITS_PER_BYTE*d - BITS_PER_BYTE;
        
        for (int i = lo; i <= hi; i++) {
            int c = (a[i] >> shift) & mask;
            count[c + 1]++;
        }

        for (int r = 0; r < R; r++)
            count[r+1] += count[r];

        for (int i = lo; i <= hi; i++) {
            int c = (a[i] >> shift) & mask;
            aux[count[c]++] = a[i];
        }

        for (int i = lo; i <= hi; i++) 
            a[i] = aux[i - lo];

        if (d == 4) return; // Đã xử lý hết 4 byte

        if (count[0] > 0)
            sort(a, lo, lo + count[0] - 1, d+1, aux);
        
        for (int r = 0; r < R; r++)
            if (count[r+1] > count[r])
                sort(a, lo + count[r], lo + count[r+1] - 1, d+1, aux);
    }
    
    // Insertion sort cho int (đơn giản hóa)
    private static void insertion(int[] a, int lo, int hi, int d) {
        for (int i = lo; i <= hi; i++)
            for (int j = i; j > lo && a[j] < a[j-1]; j--)
                exch(a, j, j-1);
    }

    private static void exch(int[] a, int i, int j) {
        int temp = a[i];
        a[i] = a[j];
        a[j] = temp;
    }
}