import java.io.*;
import java.util.NoSuchElementException;

/**
 * Lớp {@code VietnameseTST} kế thừa từ {@code TST} để hỗ trợ thứ tự bảng chữ cái tiếng Việt.
 * Sử dụng {@code VietnameseAlphabet} để so sánh ký tự theo quy tắc tiếng Việt.
 */
public class VietnameseTST<Value> extends TST<Value> {
    private final Alphabet alphabet; // Bảng chữ cái tiếng Việt

    /**
     * Khởi tạo TST với bảng chữ cái tiếng Việt.
     */
    public VietnameseTST() {
        super();
        this.alphabet = VietnameseAlphabet.VIETNAMESE_ALPHABET;
    }

    /**
     * Ghi đè get để so sánh ký tự theo VietnameseAlphabet.
     */
    @Override
    public Value get(String key) {
        if (key == null) {
            throw new IllegalArgumentException("calls get() with null argument");
        }
        if (key.length() == 0) throw new IllegalArgumentException("key must have length >= 1");
        Node<Value> x = get(root, key, 0);
        if (x == null) return null;
        return x.val;
    }

    /**
     * Ghi đè phương thức get nội bộ để sử dụng VietnameseAlphabet.
     */
    @Override
    protected Node<Value> get(Node<Value> x, String key, int d) {
        if (x == null) return null;
        char c = key.charAt(d);
        int cmp = Integer.compare(alphabet.toIndex(c), alphabet.toIndex(x.c));
        if (cmp < 0) return get(x.left, key, d);
        else if (cmp > 0) return get(x.right, key, d);
        else if (d < key.length() - 1) return get(x.mid, key, d + 1);
        else return x;
    }

    /**
     * Ghi đè put để kiểm tra ký tự và so sánh theo VietnameseAlphabet.
     */
    @Override
    public void put(String key, Value val) {
        if (key == null) {
            throw new IllegalArgumentException("calls put() with null key");
        }
        // Kiểm tra tất cả ký tự trong key thuộc bảng chữ cái
        for (int i = 0; i < key.length(); i++) {
            alphabet.toIndex(key.charAt(i)); // Ném exception nếu không thuộc
        }
        if (!contains(key)) n++;
        root = put(root, key, val, 0);
    }

    /**
     * Ghi đè phương thức put nội bộ để sử dụng VietnameseAlphabet.
     */
    @Override
    protected Node<Value> put(Node<Value> x, String key, Value val, int d) {
        char c = key.charAt(d);
        if (x == null) {
            x = new Node<Value>();
            x.c = c;
        }
        int cmp = Integer.compare(alphabet.toIndex(c), alphabet.toIndex(x.c));
        if (cmp < 0) x.left = put(x.left, key, val, d);
        else if (cmp > 0) x.right = put(x.right, key, val, d);
        else if (d < key.length() - 1) x.mid = put(x.mid, key, val, d + 1);
        else x.val = val;
        return x;
    }

    /**
     * Ghi đè longestPrefixOf để so sánh theo VietnameseAlphabet.
     */
    @Override
    public String longestPrefixOf(String query) {
        if (query == null) {
            throw new IllegalArgumentException("calls longestPrefixOf() with null argument");
        }
        if (query.length() == 0) return null;
        int length = 0;
        Node<Value> x = root;
        int i = 0;
        while (x != null && i < query.length()) {
            char c = query.charAt(i);
            int cmp = Integer.compare(alphabet.toIndex(c), alphabet.toIndex(x.c));
            if (cmp < 0) x = x.left;
            else if (cmp > 0) x = x.right;
            else {
                i++;
                if (x.val != null) length = i;
                x = x.mid;
            }
        }
        return query.substring(0, length);
    }

    /**
     * Ghi đè keysThatMatch để so sánh theo VietnameseAlphabet.
     */
    @Override
    public Iterable<String> keysThatMatch(String pattern) {
        Queue<String> queue = new Queue<String>();
        collect(root, new StringBuilder(), 0, pattern, queue);
        return queue;
    }

    /**
     * Ghi đè phương thức collect nội bộ cho keysThatMatch để sử dụng VietnameseAlphabet.
     */
    @Override
    protected void collect(Node<Value> x, StringBuilder prefix, int i, String pattern, Queue<String> queue) {
        if (x == null) return;
        char pc = pattern.charAt(i);
        boolean isWildcard = (pc == '.');
        int pcIndex = isWildcard ? -1 : alphabet.toIndex(pc); // -1 cho wildcard
        int xcIndex = alphabet.toIndex(x.c);

        if (isWildcard || pcIndex < xcIndex) collect(x.left, prefix, i, pattern, queue);
        if (isWildcard || pcIndex == xcIndex) {
            if (i == pattern.length() - 1 && x.val != null) queue.enqueue(prefix.toString() + x.c);
            if (i < pattern.length() - 1) {
                collect(x.mid, prefix.append(x.c), i + 1, pattern, queue);
                prefix.deleteCharAt(prefix.length() - 1);
            }
        }
        if (isWildcard || pcIndex > xcIndex) collect(x.right, prefix, i, pattern, queue);
    }
}