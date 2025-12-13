/******************************************************************************
 * Compilation:  javac LongestCommonSubstring.java
 * Dependencies: SuffixArrayXVN.java VietnameseAlphabet.java
 *
 * Mô tả: Tìm xâu con chung dài nhất (LCS) giữa hai chuỗi văn bản,
 * sử dụng bảng chữ cái Tiếng Việt để xử lý chính xác.
 ******************************************************************************/

public class LongestCommonSubstring {

    // Constructor private: Ngăn không cho khởi tạo đối tượng
    private LongestCommonSubstring() { }

    /**
     * Tính độ dài tiền tố chung dài nhất (Longest Common Prefix - LCP)
     * giữa hậu tố bắt đầu tại p của chuỗi s và hậu tố bắt đầu tại q của chuỗi t.
     */
    private static String lcp(String s, int p, String t, int q) {
        int n = Math.min(s.length() - p, t.length() - q);
        for (int i = 0; i < n; i++) {
            if (s.charAt(p + i) != t.charAt(q + i))
                return s.substring(p, p + i);
        }
        return s.substring(p, p + n);
    }

    /**
     * So sánh hai hậu tố: s[p..] và t[q..]
     * Trả về -1 nếu s < t, +1 nếu s > t, 0 nếu bằng nhau.
     */
    private static int compare(String s, int p, String t, int q) {
        int n = Math.min(s.length() - p, t.length() - q);
        for (int i = 0; i < n; i++) {
            if (s.charAt(p + i) != t.charAt(q + i))
                return s.charAt(p + i) - t.charAt(q + i);
        }
        if      (s.length() - p < t.length() - q) return -1;
        else if (s.length() - p > t.length() - q) return +1;
        else                                      return  0;
    }

    /**
     * CHỨC NĂNG CHÍNH: Tìm xâu con chung dài nhất của s và t.
     * Thuật toán:
     * 1. Xây dựng Suffix Array cho s và t riêng biệt.
     * 2. Duyệt song song hai mảng hậu tố (tương tự thuật toán Merge Sort).
     * 3. Tại mỗi bước, tính LCP của hai hậu tố đang xét.
     * 4. Cập nhật kết quả nếu tìm thấy LCP dài hơn.
     *
     * @param  s Chuỗi thứ nhất
     * @param  t Chuỗi thứ hai
     * @return Xâu con chung dài nhất (trả về chuỗi rỗng nếu không có)
     */
    public static String lcs(String s, String t) {
        // Xây dựng mảng hậu tố hỗ trợ Tiếng Việt
        SuffixArrayXVN suffix1 = new SuffixArrayXVN(s);
        SuffixArrayXVN suffix2 = new SuffixArrayXVN(t);

        String lcs = "";
        int i = 0, j = 0;
        
        // Vòng lặp duyệt đồng thời hai mảng hậu tố
        while (i < s.length() && j < t.length()) {
            int p = suffix1.index(i); // Vị trí hậu tố thứ i của chuỗi s
            int q = suffix2.index(j); // Vị trí hậu tố thứ j của chuỗi t
            
            // Tìm đoạn giống nhau giữa 2 hậu tố này
            String x = lcp(s, p, t, q);
            
            // Nếu đoạn giống nhau dài hơn kết quả hiện tại -> Cập nhật
            if (x.length() > lcs.length()) {
                lcs = x;
            }
            
            // Điều hướng con trỏ i, j dựa trên thứ tự từ điển
            if (compare(s, p, t, q) < 0) i++;
            else                         j++;
        }
        return lcs;
    }
}