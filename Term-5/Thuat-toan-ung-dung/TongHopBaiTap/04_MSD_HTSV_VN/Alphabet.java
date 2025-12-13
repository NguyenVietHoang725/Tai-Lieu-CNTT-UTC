import java.util.Arrays;

public class Alphabet {
    // Các hằng số bảng chữ cái cơ bản (giữ nguyên để tham khảo)
    public static final Alphabet BINARY = new Alphabet("01");
    public static final Alphabet DECIMAL = new Alphabet("0123456789");
    public static final Alphabet UNICODE16 = new Alphabet(65536);

    protected char[] alphabet;     // Mảng chứa các ký tự theo thứ tự
    protected int[] inverse;       // Mảng tra ngược: Ký tự -> Chỉ số (Index)
    protected final int R;         // Tổng số ký tự trong bảng (Radix)

    // Constructor khởi tạo từ chuỗi ký tự
    public Alphabet(String alpha) {
        // Kiểm tra trùng lặp ký tự
        boolean[] unicode = new boolean[Character.MAX_VALUE];
        for (int i = 0; i < alpha.length(); i++) {
            char c = alpha.charAt(i);
            if (unicode[c])
                throw new IllegalArgumentException("Lỗi: Ký tự lặp lại '" + c + "'");
            unicode[c] = true;
        }

        alphabet = alpha.toCharArray();
        R = alpha.length();
        inverse = new int[Character.MAX_VALUE];
        Arrays.fill(inverse, -1);

        // Tạo bảng tra ngược index
        for (int c = 0; c < R; c++)
            inverse[alphabet[c]] = c;
    }

    // Constructor cho cơ số R (0 đến R-1)
    public Alphabet(int radix) {
        this.R = radix;
        alphabet = new char[R];
        inverse = new int[R];
        for (int i = 0; i < R; i++) {
            alphabet[i] = (char) i;
            inverse[i] = i;
        }
    }
    
    // Constructor mặc định (Extended ASCII)
    public Alphabet() {
        this(256);
    }

    // Kiểm tra ký tự có nằm trong bảng chữ cái không
    public boolean contains(char c) {
        return inverse[c] != -1;
    }

    public int radix() { return R; }

    // Chuyển ký tự -> chỉ số (dùng để sort)
    public int toIndex(char c) {
        if (c >= inverse.length || inverse[c] == -1) {
            // Nếu gặp ký tự lạ, ném lỗi hoặc có thể xử lý tùy ý
            throw new IllegalArgumentException("Ký tự không có trong bảng chữ cái: " + c);
        }
        return inverse[c];
    }

    // Chuyển chỉ số -> ký tự (dùng để in ra)
    public char toChar(int index) {
        if (index < 0 || index >= R) throw new IllegalArgumentException("Index nằm ngoài phạm vi");
        return alphabet[index];
    }
}