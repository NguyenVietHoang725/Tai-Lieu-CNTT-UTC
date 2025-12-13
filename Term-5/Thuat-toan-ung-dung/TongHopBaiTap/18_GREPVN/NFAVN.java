/******************************************************************************
 *  Compilation:  javac NFAVN.java
 *  Execution:    java NFAVN regexp text
 *  Dependencies: NFA.java VietnameseAlphabet.java Stack.java Bag.java Digraph.java DirectedDFS.java
 *
 *  A class that extends NFA to support Vietnamese regular expressions and text,
 *  using the VietnameseAlphabet for character mapping.
 *
 ******************************************************************************/

public class NFAVN extends NFA {

    private final VietnameseAlphabet alphabet;

    /**
     * Initializes the NFAVN from the specified regular expression using the Vietnamese alphabet.
     *
     * @param regexp the regular expression (using characters from VietnameseAlphabet)
     */
    public NFAVN(String regexp) {
        super(convertToIndices(regexp));
        this.alphabet = VietnameseAlphabet.VIETNAMESE_ALPHABET;
    }

    /**
     * Converts a string to a regular expression string using indices from VietnameseAlphabet.
     * Metacharacters (|, *, (, )) are preserved as is.
     *
     * @param s the input string
     * @return the converted string with characters replaced by their indices
     */
    private static String convertToIndices(String s) {
        VietnameseAlphabet alpha = VietnameseAlphabet.VIETNAMESE_ALPHABET;
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < s.length(); i++) {
            char c = s.charAt(i);
            if (c == '(' || c == ')' || c == '*' || c == '|') {
                sb.append(c);
            } else if (alpha.contains(c)) {
                int index = alpha.toIndex(c);
                sb.append((char) index);
            } else {
                throw new IllegalArgumentException("Character " + c + " not in Vietnamese alphabet");
            }
        }
        return sb.toString();
    }

    /**
     * Returns true if the text is matched by the regular expression.
     * The text is converted to indices based on the Vietnamese alphabet before matching.
     *
     * @param txt the text to match
     * @return true if the text is matched by the regular expression, false otherwise
     */
    @Override
    public boolean recognizes(String txt) {
        for (int i = 0; i < txt.length(); i++) {
            char c = txt.charAt(i);
            if (!alphabet.contains(c)) {
                throw new IllegalArgumentException("Text contains character '" + c + "' not in Vietnamese alphabet");
            }
        }
        String convertedTxt = convertToIndices(txt);
        return super.recognizes(convertedTxt);
    }

    /**
     * Unit tests the {@code NFAVN} data type.
     *
     * @param args the command-line arguments (regexp, text)
     */
    public static void main(String[] args) {
        String regexp = "(" + args[0] + ")";
        String txt = args[1];
        NFAVN nfa = new NFAVN(regexp);
        StdOut.println(nfa.recognizes(txt));
    }
}