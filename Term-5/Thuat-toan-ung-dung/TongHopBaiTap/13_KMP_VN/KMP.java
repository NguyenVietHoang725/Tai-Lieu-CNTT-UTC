/******************************************************************************
 * Compilation:  javac KMP.java
 * Execution:    java KMP pattern text
 * Dependencies: StdOut.java
 *
 * Mô tả: Thuật toán Knuth-Morris-Pratt (KMP) tìm kiếm chuỗi con.
 * Sử dụng DFA (Deterministic Finite Automaton) để xử lý việc không khớp ký tự.
 ******************************************************************************/

public class KMP {
    private final int R;       // Cơ số (Kích thước bảng chữ cái)
    private final int m;       // Độ dài của chuỗi mẫu (Pattern)
    private int[][] dfa;       // Bảng trạng thái DFA

    /**
     * Khởi tạo KMP và xây dựng DFA từ chuỗi mẫu.
     * Mặc định sử dụng bảng mã Extended ASCII (256 ký tự).
     * @param pat Chuỗi mẫu
     */
    public KMP(String pat) {
        this.R = 256;
        this.m = pat.length();

        // Xây dựng DFA
        dfa = new int[R][m];
        dfa[pat.charAt(0)][0] = 1; // Trạng thái chuyển tiếp cho ký tự đầu tiên
        
        // X là trạng thái "khởi động lại" (Restart State)
        for (int x = 0, j = 1; j < m; j++) {
            // 1. Sao chép các trường hợp không khớp (Mismatch) từ trạng thái X
            for (int c = 0; c < R; c++)
                dfa[c][j] = dfa[c][x];
            
            // 2. Cập nhật trường hợp khớp (Match) cho ký tự hiện tại pat.charAt(j)
            // Chuyển sang trạng thái tiếp theo (j+1)
            dfa[pat.charAt(j)][j] = j+1;
            
            // 3. Cập nhật trạng thái khởi động lại X cho vòng lặp sau
            x = dfa[pat.charAt(j)][x];
        }
    }

    /**
     * Constructor hỗ trợ mảng ký tự và cơ số R tùy chỉnh.
     * Dùng cho các lớp con muốn mở rộng (như KMPVN).
     * * @param pattern Mảng ký tự mẫu (đã chuyển sang dạng index số nguyên)
     * @param R Kích thước bảng chữ cái
     */
    public KMP(char[] pattern, int R) {
        this.R = R;
        this.m = pattern.length;

        // Xây dựng DFA tương tự như trên
        dfa = new int[R][m];
        dfa[pattern[0]][0] = 1;
        for (int x = 0, j = 1; j < m; j++) {
            for (int c = 0; c < R; c++)
                dfa[c][j] = dfa[c][x];
            dfa[pattern[j]][j] = j+1;
            x = dfa[pattern[j]][x];
        }
    }

    /**
     * Tìm kiếm vị trí xuất hiện đầu tiên của mẫu trong văn bản.
     * @param txt Văn bản cần tìm kiếm
     * @return Chỉ số (index) bắt đầu, hoặc độ dài văn bản N nếu không tìm thấy.
     */
    public int search(String txt) {
        int n = txt.length();
        int i, j;
        // i là con trỏ văn bản, j là trạng thái hiện tại của DFA
        for (i = 0, j = 0; i < n && j < m; i++) {
            j = dfa[txt.charAt(i)][j];
        }
        if (j == m) return i - m;    // Tìm thấy (trạng thái j đạt đích m)
        return n;                    // Không tìm thấy
    }

    /**
     * Phương thức search nạp chồng cho mảng ký tự (dùng cho lớp con).
     */
    public int search(char[] text) {
        int n = text.length;
        int i, j;
        for (i = 0, j = 0; i < n && j < m; i++) {
            j = dfa[text[i]][j];
        }
        if (j == m) return i - m;
        return n;
    }
}