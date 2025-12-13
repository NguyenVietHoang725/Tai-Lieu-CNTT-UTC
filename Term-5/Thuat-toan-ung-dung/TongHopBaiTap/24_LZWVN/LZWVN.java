import java.io.*;
import java.nio.file.*;
import java.util.HashSet;

public class LZWVN {
    private static final VietnameseAlphabet ALPHABET = VietnameseAlphabet.VIETNAMESE_ALPHABET;
    private static final int R = ALPHABET.radix();  // Kích thước bảng chữ cái
    private static final int L = 65536;            // Số lượng codewords = 2^16
    private static final int W = 16;               // Độ rộng codeword (16 bit)

    // Không instantiate
    private LZWVN() { }

    /**
     * Nén file input (UTF-8) và ghi ra file output (binary).
     */
    public static void compress(String inputFile, String outputFile) {
        try {
            // Đọc file input với UTF-8
            String input = new String(Files.readAllBytes(Paths.get(inputFile)), "UTF-8");
            
            // Kiểm tra input có ký tự ngoài bảng chữ cái không
            HashSet<Character> alphabetSet = new HashSet<>();
            for (int i = 0; i < ALPHABET.radix(); i++) {
                alphabetSet.add(ALPHABET.toChar(i));
            }
            for (char c : input.toCharArray()) {
                if (!alphabetSet.contains(c)) {
                    throw new IllegalArgumentException("Input contains character not in VietnameseAlphabet: " + c);
                }
            }

            BinaryOut bout = new BinaryOut(outputFile);
            TST<Integer> st = new TST<Integer>();

            // Khởi tạo symbol table với các ký tự từ VietnameseAlphabet
            for (int i = 0; i < R; i++) {
                st.put("" + ALPHABET.toChar(i), i);
            }

            int code = R + 1;  // Codeword cho EOF

            while (input.length() > 0) {
                String s = st.longestPrefixOf(input);  // Tìm prefix dài nhất
                bout.write(st.get(s), W);              // Ghi codeword
                int t = s.length();
                if (t < input.length() && code < L) {  // Thêm codeword mới
                    st.put(input.substring(0, t + 1), code++);
                }
                input = input.substring(t);
            }
            bout.write(R, W);  // Ghi EOF
            bout.close();
        } catch (IOException e) {
            throw new RuntimeException("Error during compression: " + e.getMessage());
        }
    }

    /**
     * Giải nén file input (binary) và ghi ra file output (UTF-8).
     */
    public static void expand(String inputFile, String outputFile) {
        try {
            BinaryIn bin = new BinaryIn(inputFile);
            // Ghi output với UTF-8
            BufferedWriter writer = Files.newBufferedWriter(Paths.get(outputFile), java.nio.charset.StandardCharsets.UTF_8);

            String[] st = new String[L];
            int i; // Next available codeword value

            // Khởi tạo symbol table
            for (i = 0; i < R; i++) {
                st[i] = "" + ALPHABET.toChar(i);
            }
            st[i++] = "";  // Lookahead cho EOF

            int codeword = bin.readInt(W);
            if (codeword == R) return;  // Empty input
            String val = st[codeword];

            while (true) {
                writer.write(val);  // Ghi string ra file
                codeword = bin.readInt(W);
                if (codeword == R) break;
                String s = st[codeword];
                if (i == codeword) s = val + val.charAt(0); // Special case hack
                if (i < L) st[i++] = val + s.charAt(0);
                val = s;
            }
            writer.close();
        } catch (IOException e) {
            throw new RuntimeException("Error during decompression: " + e.getMessage());
        }
    }

    /**
     * Main method để xử lý command-line arguments.
     */
    public static void main(String[] args) {
        if (args.length != 3) {
            throw new IllegalArgumentException("Usage: java LZWVN [-/+] [input file] [output file]");
        }

        String mode = args[0];
        String inputFile = args[1];
        String outputFile = args[2];

        // Kiểm tra file input
        File file = new File(inputFile);
        if (!file.exists() || !file.isFile()) {
            throw new IllegalArgumentException("Input file does not exist: " + inputFile);
        }

        try {
            if (mode.equals("-")) {
                compress(inputFile, outputFile);
                System.out.println("Compression completed: " + outputFile);
            } else if (mode.equals("+")) {
                expand(inputFile, outputFile);
                System.out.println("Decompression completed: " + outputFile);
            } else {
                throw new IllegalArgumentException("Illegal command line argument: must be '-' or '+'");
            }
        } catch (Exception e) {
            System.err.println("Error processing files: " + e.getMessage());
        }
    }
}