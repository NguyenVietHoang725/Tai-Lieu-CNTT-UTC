public class Quick3stringVietnamese extends Quick3string {

    // Khởi tạo với bảng chữ cái Tiếng Việt
    public Quick3stringVietnamese() {
        super(VietnameseAlphabet.VIETNAMESE_ALPHABET);
    }

    // Override: Chuyển ký tự sang Index trong bảng Tiếng Việt
    @Override
    protected int charAt(String s, int d) {
        if (d >= s.length()) return -1;
        char c = s.charAt(d);
        // Nếu ký tự có trong bảng thì lấy index, không thì trả về -1
        return alphabet.contains(c) ? alphabet.toIndex(c) : -1;
    }

    // Override: So sánh dựa trên Index Tiếng Việt
    @Override
    protected boolean less(String v, String w, int d) {
        for (int i = d; i < Math.min(v.length(), w.length()); i++) {
            int vIndex = charAt(v, i);
            int wIndex = charAt(w, i);
            if (vIndex < wIndex) return true;
            if (vIndex > wIndex) return false;
        }
        return v.length() < w.length();
    }
}