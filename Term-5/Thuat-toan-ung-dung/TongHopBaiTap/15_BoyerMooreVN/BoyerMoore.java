/******************************************************************************
 * Compilation:  javac BoyerMoore.java
 * Mô tả: Thuật toán Boyer-Moore tìm kiếm chuỗi (Quy tắc Bad Character).
 ******************************************************************************/

public class BoyerMoore {
    protected int R;         // Cơ số (Kích thước bảng chữ cái)
    protected int[] right;   // Mảng nhảy bước (Bad-character skip array)
    
    // Các biến lưu trữ mẫu
    private char[] pattern;  
    private String pat;      

    /**
     * Khởi tạo với chuỗi mẫu (Mặc định ASCII 256).
     */
    public BoyerMoore(String pat) {
        this.R = 256;
        this.pat = pat;

        // Khởi tạo mảng right[]: Mặc định là -1 (không xuất hiện)
        right = new int[R];
        for (int c = 0; c < R; c++)
            right[c] = -1;
            
        // Ghi nhận vị trí xuất hiện phải nhất của ký tự trong mẫu
        for (int j = 0; j < pat.length(); j++)
            right[pat.charAt(j)] = j;
    }

    // Constructor hỗ trợ mảng char và cơ số R (để lớp con dùng)
    public BoyerMoore(char[] pattern, int R) {
        this.R = R;
        this.pattern = new char[pattern.length];
        for (int j = 0; j < pattern.length; j++)
            this.pattern[j] = pattern[j];

        right = new int[R];
        for (int c = 0; c < R; c++)
            right[c] = -1;
        for (int j = 0; j < pattern.length; j++)
            right[pattern[j]] = j;
    }

    /**
     * Tìm kiếm mẫu trong văn bản txt.
     * @return index tìm thấy hoặc n nếu không thấy.
     */
    public int search(String txt) {
        int m = pat.length();
        int n = txt.length();
        int skip;
        
        // Vòng lặp duyệt văn bản
        for (int i = 0; i <= n - m; i += skip) {
            skip = 0;
            // Duyệt mẫu từ PHẢI sang TRÁI
            for (int j = m-1; j >= 0; j--) {
                if (pat.charAt(j) != txt.charAt(i+j)) {
                    // Nếu không khớp: Tính bước nhảy dựa trên ký tự xấu trong văn bản
                    // Công thức: max(1, j - vị trí phải nhất của ký tự đó trong mẫu)
                    skip = Math.max(1, j - right[txt.charAt(i+j)]);
                    break;
                }
            }
            if (skip == 0) return i; // Tìm thấy (skip = 0 nghĩa là khớp hết)
        }
        return n; // Không tìm thấy
    }

    // Phương thức search cho mảng char (tương tự String)
    public int search(char[] text) {
        int m = pattern.length;
        int n = text.length;
        int skip;
        for (int i = 0; i <= n - m; i += skip) {
            skip = 0;
            for (int j = m-1; j >= 0; j--) {
                if (pattern[j] != text[i+j]) {
                    skip = Math.max(1, j - right[text[i+j]]);
                    break;
                }
            }
            if (skip == 0) return i;
        }
        return n;
    }
}