/******************************************************************************
 *  Compilation:  javac SuffixArrayXVN.java
 *  Dependencies: SuffixArrayX.java Alphabet.java VietnameseAlphabet.java
 *
 *  A data type that extends {@code SuffixArrayX} to support Vietnamese alphabet
 *  for suffix array sorting.
 *
 ******************************************************************************/

/**
 * The {@code SuffixArrayXVN} class extends {@code SuffixArrayX} to support
 * sorting suffixes using the Vietnamese alphabet.
 */
public class SuffixArrayXVN extends SuffixArrayX {

    /**
     * Initializes a suffix array for the given {@code text} string using the Vietnamese alphabet.
     * @param text the input string
     */
    public SuffixArrayXVN(String text) {
        super(text, VietnameseAlphabet.VIETNAMESE_ALPHABET); // Gọi constructor của lớp cha với VietnameseAlphabet
    }
}

/******************************************************************************
 *  Copyright 2002-2025, Robert Sedgewick and Kevin Wayne.
 *  Modified for Vietnamese support.
 *
 *  This file is part of algs4.jar, which accompanies the textbook
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