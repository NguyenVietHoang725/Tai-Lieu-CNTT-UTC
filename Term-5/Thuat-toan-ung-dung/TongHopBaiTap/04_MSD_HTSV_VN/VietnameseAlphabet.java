/**
 * Lớp định nghĩa thứ tự bảng chữ cái Tiếng Việt.
 * Kế thừa từ Alphabet để dùng trong thuật toán MSD.
 */
public class VietnameseAlphabet extends Alphabet {

    // Instance duy nhất (Singleton pattern) để truy cập toàn cục
    public static final VietnameseAlphabet VIETNAMESE_ALPHABET = new VietnameseAlphabet();

    public VietnameseAlphabet() {
        super(
            // 1. Khoảng trắng và các ký tự điều khiển
            "\t\n\r \u00A0" + 

            // 2. Dấu câu và ký hiệu đặc biệt
            "!\"#$%&'()*+,-./:;<=>?@[]^_`{|}~\\" + 
            
            // 3. Chữ số
            "0123456789" + 

            // 4. BẢNG CHỮ CÁI TIẾNG VIỆT (Quan trọng nhất)
            // Sắp xếp theo thứ tự từ điển: A -> Ă -> Â -> B -> ... -> Đ ...
            // Mỗi dòng bao gồm cả chữ hoa và chữ thường đi kèm
            "AÀÁẢÃẠ" + "aàáảãạ" +
            "ĂẰẮẲẴẶ" + "ăằắẳẵặ" +
            "ÂẦẤẨẪẬ" + "âầấẩẫậ" +
            "B" + "b" +
            "C" + "c" +
            "D" + "d" +
            "Đ" + "đ" +           // Chữ Đ đứng sau D
            "EÈÉẺẼẸ" + "eèéẻẽẹ" +
            "ÊỀẾỂỄỆ" + "êềếểễệ" +
            "F" + "f" +           // Thêm F để hỗ trợ tên nước ngoài/viết tắt
            "G" + "g" +
            "H" + "h" +
            "IÌÍỈĨỊ" + "iìíỉĩị" +
            "J" + "j" +
            "K" + "k" +
            "L" + "l" +
            "M" + "m" +
            "N" + "n" +
            "OÒÓỎÕỌ" + "oòóỏõọ" +
            "ÔỒỐỔỖỘ" + "ôồốổỗộ" +
            "ƠỜỚỞỠỢ" + "ơờớởỡợ" +
            "P" + "p" +
            "Q" + "q" +
            "R" + "r" +
            "S" + "s" +
            "T" + "t" +
            "UÙÚỦŨỤ" + "uùúủũụ" +
            "ƯỪỨỬỮỰ" + "ưừứửữự" +
            "V" + "v" +
            "W" + "w" +
            "X" + "x" +
            "YỲÝỶỸỴ" + "yỳýỷỹỵ" +
            "Z" + "z"
            // Lưu ý: Chuỗi này quyết định thứ tự sort.
            // Ví dụ: 'd' có index nhỏ hơn 'đ', nên "Dũng" sẽ đứng trước "Đức".
        );
    }
}