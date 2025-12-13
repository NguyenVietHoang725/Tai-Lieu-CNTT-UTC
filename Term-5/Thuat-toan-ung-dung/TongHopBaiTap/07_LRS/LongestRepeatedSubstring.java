/**
 * Lớp tiện ích tìm xâu con lặp lại dài nhất (LRS) trong một chuỗi văn bản.
 */
public class LongestRepeatedSubstring {

    // Không cho phép khởi tạo
    private LongestRepeatedSubstring() { }

    /**
     * Tìm xâu con lặp lại dài nhất trong chuỗi text.
     * * @param text Chuỗi đầu vào
     * @return Xâu con lặp lại dài nhất (trả về chuỗi rỗng nếu không có)
     */
    public static String lrs(String text) {
        int n = text.length();
        
        // 1. Tạo mảng hậu tố (Suffix Array)
        SuffixArray sa = new SuffixArray(text);
        
        String lrs = "";
        
        // 2. Duyệt qua mảng hậu tố đã sắp xếp
        // So sánh các hậu tố liền kề nhau (i và i-1) bằng hàm lcp (Longest Common Prefix)
        for (int i = 1; i < n; i++) {
            int length = sa.lcp(i); // Độ dài tiền tố chung của suffix[i] và suffix[i-1]
            
            // Nếu tìm thấy đoạn lặp dài hơn kết quả hiện tại thì cập nhật
            if (length > lrs.length()) {
                // Lấy chuỗi con từ vị trí index của hậu tố
                lrs = text.substring(sa.index(i), sa.index(i) + length);
            }
        }
        return lrs;
    }
}