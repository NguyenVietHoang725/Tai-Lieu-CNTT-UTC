public class TestAlphabet {
    public static void main(String[] args) {
        // 1. Khởi tạo bảng mã UNICODE 16 (Hỗ trợ 65536 ký tự)
        // Tiếng Việt nằm trong dải Basic Multilingual Plane này.
        Alphabet unicode16 = Alphabet.UNICODE16;

        // 2. Chuỗi đầu vào tiếng Việt
        String vnText = "Xin chào! Tôi tên là Nguyễn Việt Hoàng. Rất vui khi được gặp bạn";

        try {
            // 3. Mã hóa: Chuyển chuỗi tiếng Việt sang mảng các chỉ số (indices)
            int[] indices = unicode16.toIndices(vnText);

            // 4. Giải mã: Chuyển ngược lại từ mảng chỉ số sang chuỗi
            String decodedText = unicode16.toChars(indices);

            // 5. In kết quả kiểm chứng
            System.out.println("Vietnamese Text: " + vnText);
            System.out.println("UNICODE16 Text : " + decodedText);

            // Kiểm tra logic:
            if (vnText.equals(decodedText)) {
                System.out.println("=> Kết quả: TRÙNG KHỚP (Hệ thống hỗ trợ tốt tiếng Việt)");
            } else {
                System.out.println("=> Kết quả: KHÔNG TRÙNG KHỚP");
            }

        } catch (IllegalArgumentException e) {
            System.out.println("Lỗi: " + e.getMessage());
        }
    }
}