import java.util.Arrays;

public class Alphabet {
    // Các bảng chữ cái cơ bản thường dùng
    public static final Alphabet BINARY = new Alphabet("01");
    public static final Alphabet OCTAL = new Alphabet("01234567");
    public static final Alphabet DECIMAL = new Alphabet("0123456789");
    public static final Alphabet HEXADECIMAL = new Alphabet("0123456789ABCDEF");
    public static final Alphabet DNA = new Alphabet("ACGT");
    public static final Alphabet LOWERCASE = new Alphabet("abcdefghijklmnopqrstuvwxyz");
    public static final Alphabet UPPERCASE = new Alphabet("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
    public static final Alphabet PROTEIN = new Alphabet("ACDEFGHIKLMNPQRSTVWY");
    public static final Alphabet BASE64 = new Alphabet("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/");
    public static final Alphabet ASCII = new Alphabet(128);
    public static final Alphabet EXTENDED_ASCII = new Alphabet(256);
    
    // QUAN TRỌNG: Bảng mã Unicode 16 bit (65536 ký tự), bao gồm tiếng Việt
    public static final Alphabet UNICODE16 = new Alphabet(65536);

    private char[] alphabet;     // Mảng lưu các ký tự
    private int[] inverse;       // Mảng ánh xạ ngược: ký tự -> chỉ số (index)
    private final int R;         // Cơ số (số lượng ký tự trong bảng)

    // Constructor tạo bảng chữ cái từ chuỗi s
    public Alphabet(String alpha) {
        // Code kiểm tra trùng lặp (đã lược bỏ để ngắn gọn)
        alphabet = alpha.toCharArray();
        R = alpha.length();
        inverse = new int[Character.MAX_VALUE];
        Arrays.fill(inverse, -1);

        // Xây dựng bảng tra ngược
        for (int c = 0; c < R; c++)
            inverse[alphabet[c]] = c;
    }

    // Constructor tạo bảng mã chuẩn Unicode với kích thước radix
    public Alphabet(int radix) {
        this.R = radix;
        alphabet = new char[R];
        inverse = new int[R];

        // Gán ký tự i tương ứng với chỉ số i (Standard mapping)
        for (int i = 0; i < R; i++) {
            alphabet[i] = (char) i;
            inverse[i] = i;
        }
    }

    // Kiểm tra ký tự c có nằm trong bảng chữ cái không
    public boolean contains(char c) {
        return inverse[c] != -1;
    }

    // Trả về số lượng ký tự (cơ số)
    public int radix() {
        return R;
    }

    // Số bit cần thiết để biểu diễn 1 chỉ số (log2 của R)
    public int lgR() {
        int lgR = 0;
        for (int t = R - 1; t >= 1; t /= 2)
            lgR++;
        return lgR;
    }

    // CHỨC NĂNG CHÍNH: Chuyển ký tự sang chỉ số (index)
    public int toIndex(char c) {
        if (c >= inverse.length || inverse[c] == -1) {
            throw new IllegalArgumentException("Character " + c + " not in alphabet");
        }
        return inverse[c];
    }

    // CHỨC NĂNG CHÍNH: Chuyển chỉ số sang ký tự
    public char toChar(int index) {
        if (index < 0 || index >= R) {
            throw new IllegalArgumentException("index must be between 0 and " + R);
        }
        return alphabet[index];
    }

    // Chuyển cả chuỗi String sang mảng số nguyên (Mã hóa)
    public int[] toIndices(String s) {
        char[] source = s.toCharArray();
        int[] target = new int[s.length()];
        for (int i = 0; i < source.length; i++)
            target[i] = toIndex(source[i]);
        return target;
    }

    // Chuyển mảng số nguyên về lại chuỗi String (Giải mã)
    public String toChars(int[] indices) {
        StringBuilder s = new StringBuilder(indices.length);
        for (int i = 0; i < indices.length; i++)
            s.append(toChar(indices[i]));
        return s.toString();
    }
}