import java.io.*;
import java.nio.charset.StandardCharsets;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.HashMap;
import java.util.Map;

/**
 * Phiên bản tối ưu của Huffman cho tiếng Việt
 * Sử dụng VietnameseAlphabet để giảm kích thước cây
 */
public class HuffmanVietnamese extends Huffman {

    /**
     * Nén file với Vietnamese Alphabet
     */
    public static void compressWithAlphabet(String inputFile, String outputFile) throws IOException {
        System.out.println("=== QUY TRÌNH NÉN ===");
        System.out.println("1. Đọc file: " + inputFile);
        
        byte[] bytes = Files.readAllBytes(Paths.get(inputFile));
        String s = new String(bytes, StandardCharsets.UTF_8);
        char[] input = s.toCharArray();
        
        System.out.println("   ✓ " + input.length + " ký tự (" + bytes.length + " bytes)");

        System.out.println("2. Khởi tạo Vietnamese Alphabet...");
        VietnameseAlphabet alphabet = new VietnameseAlphabet();
        int R = alphabet.radix();
        System.out.println("   ✓ Alphabet size: " + R + " ký tự");

        System.out.println("3. Thống kê tần suất...");
        int[] freq = new int[R];
        
        // Đếm tần suất dựa trên index trong alphabet
        for (char c : input) {
            try {
                int idx = alphabet.toIndex(c);
                freq[idx]++;
            } catch (IllegalArgumentException e) {
                System.err.println("   ⚠ Ký tự không trong alphabet: '" + c + "' (U+" + 
                                 String.format("%04X", (int)c) + ")");
                // Có thể thêm vào alphabet mở rộng hoặc bỏ qua
            }
        }
        
        int uniqueChars = 0;
        for (int f : freq) if (f > 0) uniqueChars++;
        System.out.println("   ✓ " + uniqueChars + " ký tự duy nhất trong alphabet");

        System.out.println("4. Xây dựng cây Huffman...");
        Node root = buildTrie(freq);
        System.out.println("   ✓ OK");

        System.out.println("5. Tạo bảng mã...");
        String[] codeTable = new String[R];
        buildCode(codeTable, root, "");
        System.out.println("   ✓ OK");

        System.out.println("6. Ghi file nén...");
        try (BitOutputStream out = new BitOutputStream(
                new BufferedOutputStream(new FileOutputStream(outputFile)))) {
            
            // Ghi thông tin alphabet (để giải nén biết)
            out.writeInt(R); // Kích thước alphabet
            
            // Ghi cây Huffman
            writeTrie(root, out);
            
            // Ghi số ký tự gốc
            out.writeInt(input.length);
            
            // Ghi dữ liệu nén
            for (int i = 0; i < input.length; i++) {
                int idx = alphabet.toIndex(input[i]);
                String code = codeTable[idx];
                
                for (int j = 0; j < code.length(); j++) {
                    out.writeBit(code.charAt(j) == '1');
                }
                
                if ((i + 1) % 50 == 0 || i == input.length - 1) {
                    System.out.print("\r   - " + (i + 1) + "/" + input.length);
                }
            }
            System.out.println();
        }
        
        File outFile = new File(outputFile);
        double ratio = (1.0 - (double)outFile.length() / bytes.length) * 100;
        System.out.println("   ✓ " + outFile.length() + " bytes (giảm " + 
                          String.format("%.1f%%", ratio) + ")");
        System.out.println();
    }

    /**
     * Giải nén file với Vietnamese Alphabet
     */
    public static void expandWithAlphabet(String inputFile, String outputFile) throws IOException {
        System.out.println("=== QUY TRÌNH GIẢI NÉN ===");
        System.out.println("1. Đọc file: " + inputFile);
        
        try (BitInputStream in = new BitInputStream(
                new BufferedInputStream(new FileInputStream(inputFile)))) {
            
            System.out.println("2. Đọc thông tin alphabet...");
            int R = in.readInt();
            System.out.println("   ✓ Alphabet size: " + R);
            
            VietnameseAlphabet alphabet = new VietnameseAlphabet();
            if (alphabet.radix() != R) {
                System.err.println("   ⚠ Cảnh báo: Alphabet size không khớp!");
            }

            System.out.println("3. Đọc cây Huffman...");
            Node root = readTrie(in);
            System.out.println("   ✓ OK");

            System.out.println("4. Giải mã...");
            int length = in.readInt();
            System.out.println("   - " + length + " ký tự");
            
            StringBuilder sb = new StringBuilder(length);
            for (int i = 0; i < length; i++) {
                Node x = root;
                
                while (!x.isLeaf()) {
                    boolean bit = in.readBit();
                    x = bit ? x.right : x.left;
                }
                
                // Chuyển từ index về ký tự
                char c = alphabet.toChar(x.ch);
                sb.append(c);
                
                if ((i + 1) % 50 == 0 || i == length - 1) {
                    System.out.print("\r   - " + (i + 1) + "/" + length);
                }
            }
            System.out.println();

            System.out.println("5. Ghi file...");
            Files.write(Paths.get(outputFile), sb.toString().getBytes(StandardCharsets.UTF_8));
        }
        
        File outFile = new File(outputFile);
        System.out.println("   ✓ " + outFile.length() + " bytes");
        System.out.println();
    }

    // Các helper methods giống HuffmanDSVN
    
    protected static void buildCode(String[] st, Node x, String s) {
        if (x == null) return;
        if (x.isLeaf()) {
            st[x.ch] = s.length() == 0 ? "0" : s;
        } else {
            buildCode(st, x.left, s + '0');
            buildCode(st, x.right, s + '1');
        }
    }

    private static void writeTrie(Node x, BitOutputStream out) throws IOException {
        if (x.isLeaf()) {
            out.writeBit(true);
            out.writeInt(x.ch); // Ghi index trong alphabet
        } else {
            out.writeBit(false);
            writeTrie(x.left, out);
            writeTrie(x.right, out);
        }
    }

    private static Node readTrie(BitInputStream in) throws IOException {
        boolean isLeaf = in.readBit();
        if (isLeaf) {
            int ch = in.readInt();
            return new Node((char)ch, -1, null, null);
        }
        return new Node('\0', -1, readTrie(in), readTrie(in));
    }

    // BitOutputStream & BitInputStream classes
    private static class BitOutputStream implements AutoCloseable {
        private OutputStream out;
        private int buffer, n;

        public BitOutputStream(OutputStream out) {
            this.out = out;
            this.buffer = 0;
            this.n = 0;
        }

        public void writeBit(boolean bit) throws IOException {
            buffer <<= 1;
            if (bit) buffer |= 1;
            n++;
            if (n == 8) {
                out.write(buffer);
                buffer = 0;
                n = 0;
            }
        }

        public void writeInt(int x) throws IOException {
            for (int i = 0; i < 32; i++) {
                writeBit(((x >>> (31 - i)) & 1) == 1);
            }
        }

        public void close() throws IOException {
            if (n > 0) {
                buffer <<= (8 - n);
                out.write(buffer);
            }
            out.flush();
            out.close();
        }
    }

    private static class BitInputStream implements AutoCloseable {
        private InputStream in;
        private int buffer, n;

        public BitInputStream(InputStream in) {
            this.in = in;
            this.buffer = 0;
            this.n = 0;
        }

        public boolean readBit() throws IOException {
            if (n == 0) {
                buffer = in.read();
                if (buffer == -1) throw new EOFException("EOF");
                n = 8;
            }
            n--;
            return ((buffer >> n) & 1) == 1;
        }

        public int readInt() throws IOException {
            int x = 0;
            for (int i = 0; i < 32; i++) {
                x = (x << 1) | (readBit() ? 1 : 0);
            }
            return x;
        }

        public void close() throws IOException {
            in.close();
        }
    }

    public static void main(String[] args) {
        if (args.length < 2) {
            System.out.println("Sử dụng:");
            System.out.println("  java HuffmanVietnamese - <input> <output>");
            System.out.println("  java HuffmanVietnamese + <input> <output>");
            return;
        }

        try {
            String mode = args[0];
            String input = args[1];
            String output = args.length > 2 ? args[2] : 
                           (mode.equals("-") ? input + ".bin" : "output.txt");

            if (mode.equals("-")) {
                compressWithAlphabet(input, output);
            } else if (mode.equals("+")) {
                expandWithAlphabet(input, output);
            } else {
                System.out.println("Lỗi: mode phải là '-' hoặc '+'");
            }
        } catch (Exception e) {
            System.err.println("LỖI: " + e.getMessage());
            e.printStackTrace();
        }
    }
}