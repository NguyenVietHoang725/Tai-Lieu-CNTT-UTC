/******************************************************************************
 *  Compilation:  javac SuffixArrayX.java
 *  Execution:    java SuffixArrayX < input.txt
 *  Dependencies: StdIn.java StdOut.java Alphabet.java
 *  Data files:   https://algs4.cs.princeton.edu/63suffix/abra.txt
 *
 *  A data type that computes the suffix array of a string using 3-way
 *  radix quicksort, with support for custom alphabets.
 *
 ******************************************************************************/

/**
 *  The {@code SuffixArrayX} class represents a suffix array of a string of
 *  length <em>n</em>. It supports custom alphabets for sorting and is designed
 *  to be extensible via inheritance.
 *  It supports the <em>selecting</em> the <em>i</em>th smallest suffix,
 *  getting the <em>index</em> of the <em>i</em>th smallest suffix,
 *  computing the length of the <em>longest common prefix</em> between the
 *  <em>i</em>th smallest suffix and the <em>i</em>-1st smallest suffix,
 *  and determining the <em>rank</em> of a query string (which is the number
 *  of suffixes strictly less than the query string).
 *  <p>
 *  This implementation uses 3-way radix quicksort to sort the array of suffixes.
 *  For a simpler (but less efficient) implementation of the same API, see
 *  {@link SuffixArray}.
 *  The <em>index</em> and <em>length</em> operations take constant time
 *  in the worst case. The <em>lcp</em> operation takes time proportional to the
 *  length of the longest common prefix.
 *  The <em>select</em> operation takes time proportional
 *  to the length of the suffix and should be used primarily for debugging.
 *  <p>
 *  This implementation uses '\0' as a sentinel and assumes that the character
 *  '\0' does not appear in the text.
 *  <p>
 *  For additional documentation, see <a href="https://algs4.cs.princeton.edu/63suffix">Section 6.3</a> of
 *  <i>Algorithms, 4th Edition</i> by Robert Sedgewick and Kevin Wayne.
 */
public class SuffixArrayX {
    private static final int CUTOFF = 5;   // cutoff to insertion sort (any value between 0 and 12)

    protected final char[] text;   // Đổi thành protected để lớp con truy cập
    protected final int[] index;   // Đổi thành protected để lớp con truy cập
    protected final int n;         // Đổi thành protected để lớp con truy cập
    protected final Alphabet alphabet; // Thêm trường alphabet

    /**
     * Initializes a suffix array for the given {@code text} string with default Unicode ordering.
     * @param text the input string
     */
    public SuffixArrayX(String text) {
        this(text, null); // Gọi constructor mới với alphabet = null (mặc định Unicode)
    }

    /**
     * Initializes a suffix array for the given {@code text} string using the specified {@code alphabet}.
     * @param text the input string
     * @param alphabet the alphabet for character indexing (null for default Unicode ordering)
     */
    public SuffixArrayX(String text, Alphabet alphabet) {
        this.alphabet = alphabet;
        n = text.length();
        text = text + '\0';
        this.text = text.toCharArray();
        this.index = new int[n];
        for (int i = 0; i < n; i++)
            index[i] = i;

        sort(0, n - 1, 0);
    }

    // 3-way string quicksort lo..hi starting at dth character
    protected void sort(int lo, int hi, int d) {
        if (hi <= lo + CUTOFF) {
            insertion(lo, hi, d);
            return;
        }

        int lt = lo, gt = hi;
        int v = charAt(index[lo], d);
        int i = lo + 1;
        while (i <= gt) {
            int t = charAt(index[i], d);
            if (t < v) exch(lt++, i++);
            else if (t > v) exch(i, gt--);
            else i++;
        }

        sort(lo, lt - 1, d);
        if (v > 0) sort(lt, gt, d + 1);
        sort(gt + 1, hi, d);
    }

    // sort from a[lo] to a[hi], starting at the dth character
    protected void insertion(int lo, int hi, int d) {
        for (int i = lo; i <= hi; i++)
            for (int j = i; j > lo && less(index[j], index[j - 1], d); j--)
                exch(j, j - 1);
    }

    // is text[i+d..n) < text[j+d..n) ?
    protected boolean less(int i, int j, int d) {
        if (i == j) return false;
        i += d;
        j += d;
        while (i < n && j < n) {
            int ci = charAt(i, 0);
            int cj = charAt(j, 0);
            if (ci < cj) return true;
            if (ci > cj) return false;
            i++;
            j++;
        }
        return i > j;
    }

    // exchange index[i] and index[j]
    protected void exch(int i, int j) {
        int swap = index[i];
        index[i] = index[j];
        index[j] = swap;
    }

    // Get the index of the character at position pos + offset
    protected int charAt(int pos, int offset) {
        char c = text[pos + offset];
        if (c == '\0') return -1; // Sentinel nhỏ hơn mọi ký tự
        if (alphabet == null) return c; // Mặc định: trả về giá trị Unicode
        return alphabet.toIndex(c); // Sử dụng ánh xạ từ alphabet
    }

    /**
     * Returns the length of the input string.
     * @return the length of the input string
     */
    public int length() {
        return n;
    }

    /**
     * Returns the index into the original string of the <em>i</em>th smallest suffix.
     * @param i an integer between 0 and <em>n</em>-1
     * @return the index into the original string of the <em>i</em>th smallest suffix
     * @throws java.lang.IllegalArgumentException unless {@code 0 <= i < n}
     */
    public int index(int i) {
        if (i < 0 || i >= n) throw new IllegalArgumentException();
        return index[i];
    }

    /**
     * Returns the length of the longest common prefix of the <em>i</em>th
     * smallest suffix and the <em>i</em>-1st smallest suffix.
     * @param i an integer between 1 and <em>n</em>-1
     * @return the length of the longest common prefix
     * @throws java.lang.IllegalArgumentException unless {@code 1 <= i < n}
     */
    public int lcp(int i) {
        if (i < 1 || i >= n) throw new IllegalArgumentException();
        return lcp(index[i], index[i - 1]);
    }

    // longest common prefix of text[i..n) and text[j..n)
    protected int lcp(int i, int j) {
        int length = 0;
        while (i < n && j < n) {
            if (text[i] != text[j]) return length;
            i++;
            j++;
            length++;
        }
        return length;
    }

    /**
     * Returns the <em>i</em>th smallest suffix as a string.
     * @param i the index
     * @return the <em>i</em> smallest suffix as a string
     * @throws java.lang.IllegalArgumentException unless {@code 0 <= i < n}
     */
    public int rank(String query) {
        int lo = 0, hi = n - 1;
        while (lo <= hi) {
            int mid = lo + (hi - lo) / 2;
            int cmp = compare(query, index[mid]);
            if (cmp < 0) hi = mid - 1;
            else if (cmp > 0) lo = mid + 1;
            else return mid;
        }
        return lo;
    }

    // is query < text[i..n) ?
    protected int compare(String query, int i) {
        int m = query.length();
        int j = 0;
        while (i < n && j < m) {
            int qc = (alphabet == null) ? query.charAt(j) : alphabet.toIndex(query.charAt(j));
            int tc = charAt(i, 0);
            if (qc != tc) return qc - tc;
            i++;
            j++;
        }
        if (i < n) return -1;
        if (j < m) return +1;
        return 0;
    }
}

/******************************************************************************
 *  Copyright 2002-2025, Robert Sedgewick and Kevin Wayne.
 *
 *  This file is part of algs4.jar, which accompanies the textbook
 *
 *      Algorithms, 4th edition by Robert Sedgewick and Kevin Wayne,
 *      Addison-Wesley Professional, 2011, ISBN 0-321-57351-X.
 *      http://algs4.cs.princeton.edu
 *
 *  algs4.jar is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  algs4.jar is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with algs4.jar.  If not, see http://www.gnu.org/licenses.
 ******************************************************************************/