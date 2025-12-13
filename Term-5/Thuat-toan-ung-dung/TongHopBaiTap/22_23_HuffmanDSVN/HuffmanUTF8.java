/******************************************************************************
 *  HuffmanUTF8.java
 *  Nén và giải nén dữ liệu nhị phân (đặc biệt phù hợp với file UTF-8)
 *  Sử dụng BinaryIn / BinaryOut của thư viện algs4.
 *
 *  Biên dịch:
 *      javac HuffmanUTF8.java
 *
 *  Chạy thử (nén -> giải nén):
 *      java HuffmanUTF8
 *      # Hoặc tùy chỉnh đường dẫn file trong hàm main
 ******************************************************************************/

import java.util.*;

public class HuffmanUTF8 {

    // Kích thước bảng ký tự 1 byte
    private static final int R = 256;

    // Node của cây Huffman
    private static class Node implements Comparable<Node> {
        private final byte b;
        private final int freq;
        private final Node left, right;

        Node(byte b, int freq, Node left, Node right) {
            this.b = b;
            this.freq = freq;
            this.left = left;
            this.right = right;
        }

        boolean isLeaf() { return left == null && right == null; }

        public int compareTo(Node that) {
            return this.freq - that.freq;
        }
    }

    /* ======================= NÉN ======================= */
    public static void compress(BinaryIn in, BinaryOut out) {
        // Đọc toàn bộ byte vào mảng
        List<Byte> list = new ArrayList<>();
        while (!in.isEmpty()) list.add(in.readByte());
        byte[] input = new byte[list.size()];
        for (int i = 0; i < list.size(); i++) input[i] = list.get(i);

        // Tần suất xuất hiện
        int[] freq = new int[R];
        for (byte b : input) freq[b & 0xFF]++;

        // Xây cây Huffman
        Node root = buildTrie(freq);

        // Bảng mã
        String[] st = new String[R];
        buildCode(st, root, "");

        // Ghi cấu trúc cây và chiều dài
        writeTrie(root, out);
        out.write(input.length);      // 32-bit

        // Ghi dữ liệu nén
        for (byte b : input) {
            String code = st[b & 0xFF];
            for (char c : code.toCharArray())
                out.write(c == '1');
        }
        out.close();
    }

    /* ======================= GIẢI NÉN ======================= */
    public static void expand(BinaryIn in, BinaryOut out) {
        Node root = readTrie(in);
        int length = in.readInt();
        for (int i = 0; i < length; i++) {
            Node x = root;
            while (!x.isLeaf()) {
                x = in.readBoolean() ? x.right : x.left;
            }
            out.write(x.b & 0xFF, 8);    // ghi đúng 1 byte
        }
        out.close();
    }

    /* ======================= HỖ TRỢ ======================= */

    // Tạo cây Huffman từ bảng tần suất
    private static Node buildTrie(int[] freq) {
        PriorityQueue<Node> pq = new PriorityQueue<>();
        for (int i = 0; i < R; i++)
            if (freq[i] > 0)
                pq.add(new Node((byte) i, freq[i], null, null));

        if (pq.size() == 1) { // Trường hợp chỉ có 1 ký tự
            if (freq[0] == 0) pq.add(new Node((byte)0, 0, null, null));
            else pq.add(new Node((byte)1, 0, null, null));
        }

        while (pq.size() > 1) {
            Node left  = pq.poll();
            Node right = pq.poll();
            Node parent = new Node((byte)0, left.freq + right.freq, left, right);
            pq.add(parent);
        }
        return pq.poll();
    }

    // Ghi cấu trúc cây Huffman xuống output
    private static void writeTrie(Node x, BinaryOut out) {
        if (x.isLeaf()) {
            out.write(true);
            out.write(x.b & 0xFF, 8);  // ghi 1 byte
            return;
        }
        out.write(false);
        writeTrie(x.left, out);
        writeTrie(x.right, out);
    }

    // Đọc lại cây Huffman từ input
    private static Node readTrie(BinaryIn in) {
        if (in.readBoolean())
            return new Node(in.readByte(), -1, null, null);
        return new Node((byte)0, -1, readTrie(in), readTrie(in));
    }

    // Tạo bảng mã bit cho từng byte
    private static void buildCode(String[] st, Node x, String s) {
        if (!x.isLeaf()) {
            buildCode(st, x.left,  s + '0');
            buildCode(st, x.right, s + '1');
        } else {
            st[x.b & 0xFF] = s;
        }
    }

    /* ======================= MAIN DEMO ======================= */
    public static void main(String[] args) {
        // Ví dụ: nén rồi giải nén cùng lúc
        BinaryIn in1  = new BinaryIn("10sv.txt");
        BinaryOut out1 = new BinaryOut("hufcompress3.bin");
        compress(in1, out1);

        BinaryIn in2  = new BinaryIn("hufcompress3.bin");
        BinaryOut out2 = new BinaryOut("hufexpand3.txt");
        expand(in2, out2);

        System.out.println("Đã nén -> giải nén xong.");
    }
}
