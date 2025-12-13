import java.io.*;

public class LZW {
    private static final int R = 256;        // number of input chars
    private static final int L = 4096;       // number of codewords = 2^W
    private static final int W = 12;         // codeword width

    // Do not instantiate.
    private LZW() { }

    /**
     * Compresses the input file and writes to the output file using LZW compression.
     * @param inputFile  the input file path
     * @param outputFile the output file path
     */
    public static void compress(String inputFile, String outputFile) {
        BinaryIn bin = new BinaryIn(inputFile);
        BinaryOut bout = new BinaryOut(outputFile);

        String input = bin.readString();
        TST<Integer> st = new TST<Integer>();

        // Initialize symbol table with all 1-character strings
        for (int i = 0; i < R; i++)
            st.put("" + (char) i, i);

        int code = R + 1;  // R is codeword for EOF

        while (input.length() > 0) {
            String s = st.longestPrefixOf(input);  // Find max prefix match
            bout.write(st.get(s), W);              // Write s's encoding
            int t = s.length();
            if (t < input.length() && code < L)    // Add new codeword to symbol table
                st.put(input.substring(0, t + 1), code++);
            input = input.substring(t);            // Scan past s in input
        }
        bout.write(R, W);                        // Write EOF
        bout.close();
    }

    /**
     * Expands the input file and writes to the output file using LZW decompression.
     * @param inputFile  the input file path
     * @param outputFile the output file path
     */
    public static void expand(String inputFile, String outputFile) {
        BinaryIn bin = new BinaryIn(inputFile);
        BinaryOut bout = new BinaryOut(outputFile);

        String[] st = new String[L];
        int i; // next available codeword value

        // Initialize symbol table with all 1-character strings
        for (i = 0; i < R; i++)
            st[i] = "" + (char) i;
        st[i++] = "";                        // (unused) lookahead for EOF

        int codeword = bin.readInt(W);
        if (codeword == R) return;           // Expanded message is empty
        String val = st[codeword];

        while (true) {
            bout.write(val);                   // Write current string
            codeword = bin.readInt(W);
            if (codeword == R) break;         // End of input
            String s = st[codeword];
            if (i == codeword) s = val + val.charAt(0); // Special case hack
            if (i < L) st[i++] = val + s.charAt(0);    // Add new codeword
            val = s;
        }
        bout.close();
    }

    /**
     * Main method to handle command-line arguments for compression or expansion.
     * @param args command-line arguments: [-/+] [input file] [output file]
     */
    public static void main(String[] args) {
        if (args.length != 3) {
            throw new IllegalArgumentException("Usage: java LZW [-/+] [input file] [output file]");
        }

        String mode = args[0];
        String inputFile = args[1];
        String outputFile = args[2];

        // Check if input file exists
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