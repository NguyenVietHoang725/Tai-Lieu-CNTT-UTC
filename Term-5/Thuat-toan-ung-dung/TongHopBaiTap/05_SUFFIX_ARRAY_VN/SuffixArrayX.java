public class SuffixArrayX {
    private static final int CUTOFF = 5;

    protected final char[] text;       // Mảng ký tự của văn bản gốc
    protected final int[] index;       // Mảng chỉ số (kết quả suffix array)
    protected final int n;             // Độ dài
    protected final Alphabet alphabet; // Bảng chữ cái tuỳ chỉnh

    public SuffixArrayX(String text, Alphabet alphabet) {
        this.alphabet = alphabet;
        n = text.length();
        // Thêm ký tự null sentinel vào cuối để đánh dấu kết thúc
        String s = text + '\0'; 
        this.text = s.toCharArray();
        this.index = new int[n];
        for (int i = 0; i < n; i++)
            index[i] = i;

        sort(0, n - 1, 0);
    }

    // Lấy giá trị index của ký tự tại vị trí pos + offset
    protected int charAt(int pos, int offset) {
        // Đảm bảo không đọc quá mảng text (vốn đã có \0 ở cuối nên an toàn)
        char c = text[pos + offset]; 
        if (c == '\0') return -1; // Sentinel nhỏ nhất
        if (alphabet == null) return c; 
        return alphabet.toIndex(c); // Quan trọng: chuyển sang index Tiếng Việt
    }

    // QuickSort 3-way
    protected void sort(int lo, int hi, int d) {
        if (hi <= lo + CUTOFF) {
            insertion(lo, hi, d);
            return;
        }

        int lt = lo, gt = hi;
        int v = charAt(index[lo], d);
        int i = lo + 1;
        while (i <= gt) {
            int t = charAt(index[i], d);
            if (t < v) exch(lt++, i++);
            else if (t > v) exch(i, gt--);
            else i++;
        }

        sort(lo, lt - 1, d);
        if (v > 0) sort(lt, gt, d + 1);
        sort(gt + 1, hi, d);
    }

    protected void insertion(int lo, int hi, int d) {
        for (int i = lo; i <= hi; i++)
            for (int j = i; j > lo && less(index[j], index[j - 1], d); j--)
                exch(j, j - 1);
    }

    protected boolean less(int i, int j, int d) {
        if (i == j) return false;
        i += d; j += d;
        while (i < n && j < n) {
            int ci = charAt(i, 0);
            int cj = charAt(j, 0);
            if (ci < cj) return true;
            if (ci > cj) return false;
            i++; j++;
        }
        return i > j;
    }

    protected void exch(int i, int j) {
        int swap = index[i]; index[i] = index[j]; index[j] = swap;
    }

    public int length() { return n; }
    
    // Trả về vị trí bắt đầu của hậu tố thứ i trong mảng đã sắp xếp
    public int index(int i) { return index[i]; }

    // Tính Longest Common Prefix giữa hậu tố rank i và rank i-1
    public int lcp(int i) {
        if (i < 1 || i >= n) throw new IllegalArgumentException();
        return lcp(index[i], index[i - 1]);
    }

    protected int lcp(int i, int j) {
        int length = 0;
        while (i < n && j < n) {
            if (text[i] != text[j]) return length;
            i++; j++; length++;
        }
        return length;
    }
    
    // Tìm rank của một chuỗi query (Binary Search)
    public int rank(String query) {
        int lo = 0, hi = n - 1;
        while (lo <= hi) {
            int mid = lo + (hi - lo) / 2;
            int cmp = compare(query, index[mid]);
            if (cmp < 0) hi = mid - 1;
            else if (cmp > 0) lo = mid + 1;
            else return mid;
        }
        return lo;
    }

    protected int compare(String query, int i) {
        int m = query.length();
        int j = 0;
        while (i < n && j < m) {
            int qc = (alphabet == null) ? query.charAt(j) : alphabet.toIndex(query.charAt(j));
            int tc = charAt(i, 0);
            if (qc != tc) return qc - tc;
            i++; j++;
        }
        if (i < n) return -1;
        if (j < m) return +1;
        return 0;
    }
}