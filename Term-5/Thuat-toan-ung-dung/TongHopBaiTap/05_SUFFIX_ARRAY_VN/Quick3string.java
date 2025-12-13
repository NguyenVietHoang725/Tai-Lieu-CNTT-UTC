import java.util.Random;

public class Quick3string {
    protected static final int CUTOFF = 15; 
    protected final Alphabet alphabet;      

    public Quick3string(Alphabet alphabet) {
        this.alphabet = alphabet;
    }

    public void sort(String[] a) {
        // Shuffle để đảm bảo hiệu suất (tránh trường hợp xấu nhất)
        // Ở đây dùng code đơn giản thay vì StdRandom để giảm phụ thuộc
        Random rand = new Random();
        for (int i = 0; i < a.length; i++) {
            int r = i + rand.nextInt(a.length - i);
            String temp = a[i]; a[i] = a[r]; a[r] = temp;
        }
        sort(a, 0, a.length - 1, 0);
    }

    // Lấy ký tự thứ d, trả về -1 nếu hết chuỗi
    // ĐỂ PROTECTED ĐỂ LỚP CON GHI ĐÈ
    protected int charAt(String s, int d) {
        if (d >= s.length()) return -1;
        return s.charAt(d);
    }

    protected void sort(String[] a, int lo, int hi, int d) {
        if (hi <= lo + CUTOFF) {
            insertion(a, lo, hi, d);
            return;
        }

        int lt = lo, gt = hi;
        int v = charAt(a[lo], d);
        int i = lo + 1;
        while (i <= gt) {
            int t = charAt(a[i], d);
            if (t < v) exch(a, lt++, i++);
            else if (t > v) exch(a, i, gt--);
            else i++;
        }

        sort(a, lo, lt - 1, d);
        if (v >= 0) sort(a, lt, gt, d + 1);
        sort(a, gt + 1, hi, d);
    }

    protected void insertion(String[] a, int lo, int hi, int d) {
        for (int i = lo; i <= hi; i++)
            for (int j = i; j > lo && less(a[j], a[j - 1], d); j--)
                exch(a, j, j - 1);
    }

    protected void exch(String[] a, int i, int j) {
        String temp = a[i]; a[i] = a[j]; a[j] = temp;
    }

    // So sánh 2 chuỗi bắt đầu từ ký tự d
    // ĐỂ PROTECTED ĐỂ LỚP CON GHI ĐÈ LOGIC SO SÁNH
    protected boolean less(String v, String w, int d) {
        for (int i = d; i < Math.min(v.length(), w.length()); i++) {
            if (charAt(v, i) < charAt(w, i)) return true;
            if (charAt(v, i) > charAt(w, i)) return false;
        }
        return v.length() < w.length();
    }
}