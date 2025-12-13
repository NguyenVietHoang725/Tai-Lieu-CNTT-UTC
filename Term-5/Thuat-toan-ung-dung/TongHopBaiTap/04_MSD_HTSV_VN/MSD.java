/**
 * Lớp thuật toán MSD Radix Sort hỗ trợ Tiếng Việt.
 */
public class MSD {
    // Lấy cơ số R từ bảng chữ cái tiếng Việt đã định nghĩa
    private static final int R = VietnameseAlphabet.VIETNAMESE_ALPHABET.radix(); 
    private static final int CUTOFF = 15; // Ngưỡng dùng Insertion Sort

    private MSD() {}

    /**
     * Hàm chính để gọi sắp xếp
     */
    public static void sort(String[] a) {
        int n = a.length;
        String[] aux = new String[n];
        sort(a, 0, n - 1, 0, aux);
    }

    // Hàm lấy chỉ số (index) của ký tự thứ d trong chuỗi s
    // Sử dụng VietnameseAlphabet để tra cứu index
    private static int charAt(String s, int d) {
        if (d >= s.length()) return -1; // Hết chuỗi thì trả về -1
        char c = s.charAt(d);
        if (VietnameseAlphabet.VIETNAMESE_ALPHABET.contains(c)) {
            return VietnameseAlphabet.VIETNAMESE_ALPHABET.toIndex(c);
        } else {
            // Nếu gặp ký tự lạ không có trong bảng định nghĩa, xếp nó xuống cuối
            return R; 
        }
    }

    // Đệ quy MSD sort
    private static void sort(String[] a, int lo, int hi, int d, String[] aux) {
        // Nếu đoạn cần sort nhỏ, dùng Insertion Sort cho nhanh
        if (hi <= lo + CUTOFF) {
            insertion(a, lo, hi, d);
            return;
        }

        // 1. Đếm tần suất (Frequency counts)
        int[] count = new int[R + 2];
        for (int i = lo; i <= hi; i++) {
            int c = charAt(a[i], d);
            count[c + 2]++;
        }

        // 2. Cộng dồn (Transform counts to indices)
        for (int r = 0; r < R + 1; r++) {
            count[r + 1] += count[r];
        }

        // 3. Phân phối (Distribute)
        for (int i = lo; i <= hi; i++) {
            int c = charAt(a[i], d);
            aux[count[c + 1]++] = a[i];
        }

        // 4. Sao chép lại (Copy back)
        for (int i = lo; i <= hi; i++) {
            a[i] = aux[i - lo];
        }

        // 5. Đệ quy cho các nhóm con
        for (int r = 0; r < R; r++) {
            sort(a, lo + count[r], lo + count[r + 1] - 1, d + 1, aux);
        }
    }

    // Insertion sort (được tinh chỉnh cho Tiếng Việt)
    private static void insertion(String[] a, int lo, int hi, int d) {
        for (int i = lo; i <= hi; i++) {
            for (int j = i; j > lo && less(a[j], a[j - 1], d); j--) {
                exch(a, j, j - 1);
            }
        }
    }

    // Hàm so sánh 2 chuỗi theo quy tắc Tiếng Việt
    private static boolean less(String v, String w, int d) {
        for (int i = d; i < Math.min(v.length(), w.length()); i++) {
            char vc = v.charAt(i);
            char wc = w.charAt(i);
            
            int orderV = VietnameseAlphabet.VIETNAMESE_ALPHABET.contains(vc) ? 
                         VietnameseAlphabet.VIETNAMESE_ALPHABET.toIndex(vc) : R;
            int orderW = VietnameseAlphabet.VIETNAMESE_ALPHABET.contains(wc) ? 
                         VietnameseAlphabet.VIETNAMESE_ALPHABET.toIndex(wc) : R;
            
            if (orderV < orderW) return true;
            if (orderV > orderW) return false;
        }
        return v.length() < w.length();
    }

    private static void exch(String[] a, int i, int j) {
        String temp = a[i];
        a[i] = a[j];
        a[j] = temp;
    }
}