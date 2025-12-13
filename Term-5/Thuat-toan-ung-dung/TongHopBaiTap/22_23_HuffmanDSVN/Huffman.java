/******************************************************************************
 *  Compilation:  javac Huffman.java
 *  Execution:    java Huffman - < input.txt   (compress)
 *  Execution:    java Huffman + < input.txt   (expand)
 *  Dependencies: BinaryIn.java BinaryOut.java
 *  Data files:   https://algs4.cs.princeton.edu/55compression/abra.txt
 *                https://algs4.cs.princeton.edu/55compression/tinytinyTale.txt
 *                https://algs4.cs.princeton.edu/55compression/medTale.txt
 *                https://algs4.cs.princeton.edu/55compression/tale.txt
 *
 *  Compress or expand a binary input stream using the Huffman algorithm.
 *
 *  % java Huffman - < abra.txt | java BinaryDump 60
 *  010100000100101000100010010000110100001101010100101010000100
 *  000000000000000000000000000110001111100101101000111110010100
 *  120 bits
 *
 *  % java Huffman - < abra.txt | java Huffman +
 *  ABRACADABRA!
 *
 ******************************************************************************/

/**
 *  The {@code Huffman} class provides static methods for compressing
 *  and expanding a binary input using Huffman codes over the 8-bit extended
 *  ASCII alphabet.
 *  <p>
 *  For additional documentation,
 *  see <a href="https://algs4.cs.princeton.edu/55compression">Section 5.5</a> of
 *  <i>Algorithms, 4th Edition</i> by Robert Sedgewick and Kevin Wayne.
 *
 *  @author Robert Sedgewick
 *  @author Kevin Wayne
 */
import java.io.*;

public class Huffman {

    // alphabet size of extended ASCII
    private static final int R = 256;

    // Do not instantiate.
    protected Huffman() { }

    // Huffman trie node
    protected static class Node implements Comparable<Node> {
        protected final char ch;
        protected final int freq;
        protected final Node left, right;

        Node(char ch, int freq, Node left, Node right) {
            this.ch    = ch;
            this.freq  = freq;
            this.left  = left;
            this.right = right;
        }

        // is the node a leaf node?
        protected boolean isLeaf() {
            assert ((left == null) && (right == null)) || ((left != null) && (right != null));
            return (left == null) && (right == null);
        }

        // compare, based on frequency
        public int compareTo(Node that) {
            return this.freq - that.freq;
        }
    }

    /**
     * Reads a sequence of 8-bit bytes from standard input; compresses them
     * using Huffman codes with an 8-bit alphabet; and writes the results
     * to standard output.
     */
    public static void compress(String outputFile) throws IOException {
        // read the input
        String s = BinaryStdIn.readString();
        char[] input = s.toCharArray();

        // tabulate frequency counts
        int[] freq = new int[R];
        for (int i = 0; i < input.length; i++)
            freq[input[i]]++;

        // build Huffman trie
        Node root = buildTrie(freq);

        // build code table
        String[] st = new String[R];
        buildCode(st, root, "");

        // set output stream
        System.setOut(new PrintStream(new FileOutputStream(outputFile)));

        // print trie for decoder
        writeTrie(root);

        // print number of bytes in original uncompressed message
        BinaryStdOut.write(input.length);

        // use Huffman code to encode input
        for (int i = 0; i < input.length; i++) {
            String code = st[input[i]];
            for (int j = 0; j < code.length(); j++) {
                if (code.charAt(j) == '0') {
                    BinaryStdOut.write(false);
                }
                else if (code.charAt(j) == '1') {
                    BinaryStdOut.write(true);
                }
                else throw new IllegalStateException("Illegal state");
            }
        }

        // close output stream
        BinaryStdOut.close();
    }

    // build the Huffman trie given frequencies
    protected static Node buildTrie(int[] freq) {
        // initialize priority queue with singleton trees
        MinPQ<Node> pq = new MinPQ<Node>();
        for (char c = 0; c < R; c++)
            if (freq[c] > 0)
                pq.insert(new Node(c, freq[c], null, null));

        // merge two smallest trees
        while (pq.size() > 1) {
            Node left  = pq.delMin();
            Node right = pq.delMin();
            Node parent = new Node('\0', left.freq + right.freq, left, right);
            pq.insert(parent);
        }
        return pq.delMin();
    }

    // write bitstring-encoded trie to standard output
    protected static void writeTrie(Node x) {
        if (x.isLeaf()) {
            BinaryStdOut.write(true);
            BinaryStdOut.write(x.ch, 8);
            return;
        }
        BinaryStdOut.write(false);
        writeTrie(x.left);
        writeTrie(x.right);
    }

    // make a lookup table from symbols and their encodings
    protected static void buildCode(String[] st, Node x, String s) {
        if (!x.isLeaf()) {
            buildCode(st, x.left,  s + '0');
            buildCode(st, x.right, s + '1');
        }
        else {
            st[x.ch] = s;
        }
    }

    /**
     * Reads a sequence of bits that represents a Huffman-compressed message from
     * standard input; expands them; and writes the results to standard output.
     */
    public static void expand(String outputFile) throws IOException {
        // set output stream
        System.setOut(new PrintStream(new FileOutputStream(outputFile)));

        // read in Huffman trie from input stream
        Node root = readTrie();

        // number of bytes to write
        int length = BinaryStdIn.readInt();

        // decode using the Huffman trie
        for (int i = 0; i < length; i++) {
            Node x = root;
            while (!x.isLeaf()) {
                boolean bit = BinaryStdIn.readBoolean();
                if (bit) x = x.right;
                else     x = x.left;
            }
            BinaryStdOut.write(x.ch, 8);
        }
        BinaryStdOut.close();
    }

    protected static Node readTrie() {
        boolean isLeaf = BinaryStdIn.readBoolean();
        if (isLeaf) {
            return new Node(BinaryStdIn.readChar(), -1, null, null);
        }
        else {
            return new Node('\0', -1, readTrie(), readTrie());
        }
    }

    /**
     * Sample client that calls {@code compress()} or {@code expand()} based on command-line arguments.
     *
     * @param args the command-line arguments
     */
    public static void main(String[] args) throws IOException {
        // Debug: In số lượng tham số và nội dung tham số
        System.out.println("Debug: Number of arguments: " + args.length);
        for (int i = 0; i < args.length; i++) {
            System.out.println("Debug: Argument " + i + ": " + args[i]);
        }

        // Kiểm tra số lượng tham số
        if (args.length != 3) {
            System.err.println("Error: Expected 3 arguments, got " + args.length);
            throw new IllegalArgumentException("Usage: java Huffman [-/+] inputFile outputFile");
        }

        // Debug: In tên tệp đầu vào
        System.out.println("Debug: Attempting to read input file: " + args[1]);
        File inputFile = new File(args[1]);
        if (!inputFile.exists() || !inputFile.isFile()) {
            System.err.println("Error: Input file '" + args[1] + "' does not exist or is not a file");
            throw new IllegalArgumentException("Input file '" + args[1] + "' does not exist or is not a file");
        }

        // Thiết lập luồng đầu vào
        System.setIn(new FileInputStream(inputFile));
        System.out.println("Debug: Input stream set to file: " + args[1]);

        // Debug: In chế độ hoạt động và tệp đầu ra
        System.out.println("Debug: Mode: " + args[0] + ", Output file: " + args[2]);
        if (args[0].equals("-")) {
            System.out.println("Debug: Starting compression...");
            compress(args[2]);
            System.out.println("Debug: Compression completed, output written to: " + args[2]);
        }
        else if (args[0].equals("+")) {
            System.out.println("Debug: Starting decompression...");
            expand(args[2]);
            System.out.println("Debug: Decompression completed, output written to: " + args[2]);
        }
        else {
            System.err.println("Error: Invalid mode '" + args[0] + "'");
            throw new IllegalArgumentException("Illegal command line argument: must be '-' or '+'");
        }
    }
}