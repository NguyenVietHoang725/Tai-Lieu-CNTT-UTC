/******************************************************************************
 *  Compilation:  javac BinaryOutText.java
 *  Execution:    java BinaryOutText n < file
 *  Dependencies: BinaryStdIn.java
 *  Data file:    https://introcs.cs.princeton.edu/stdlib/abra.txt
 *
 *  Reads in a binary file and writes out the bits, n per line.
 *
 *  % more abra.txt
 *  ABRACADABRA!
 *
 *  % java BinaryOutText 16 < abra.txt
 *  0100000101000010
 *  0101001001000001
 *  0100001101000001
 *  0100010001000001
 *  0100001001010010
 *  0100000100100001
 *  96 bits
 *
 ******************************************************************************/

 

/**
 *  The {@code BinaryOutText} class provides a client for displaying the contents
 *  of a binary file in binary.
 *  <p>
 *  For more full-featured versions, see the Unix utilities
 *  {@code od} (octal dump) and {@code hexdump} (hexadecimal dump).
 *  <p>
 *  For additional documentation,
 *  see <a href="https://algs4.cs.princeton.edu/55compression">Section 5.5</a> of
 *  <i>Algorithms, 4th Edition</i> by Robert Sedgewick and Kevin Wayne.
 *  <p>
 *  See also {@link HexDump} and {@link PictureDump}.
 *
 *  @author Robert Sedgewick
 *  @author Kevin Wayne
 */
import java.io.*;
public class BinaryOutText{

    // Do not instantiate.
    private BinaryOutText() { }

    /**
     * Reads in a sequence of bytes from standard input and writes
     * them to standard output in binary, k bits per line,
     * where k is given as a command-line integer (defaults
     * to 16 if no integer is specified); also writes the number
     * of bits.
     *
     * @param args the command-line arguments
     */
    public static void main(String[] args) throws IOException {
        /*System.setIn(new FileInputStream(new File("tinytinyTale.txt")));*/
        /*System.setIn(new FileInputStream(new File("q32x48.bin")));*/
        System.setIn(new FileInputStream(new File("outbit.bin")));
        int bitsPerLine = 8;
        

        int count;
        for (count = 0; !BinaryStdIn.isEmpty(); count++) {
        if (count != 0 && count % bitsPerLine == 0) StdOut.println();
            if (BinaryStdIn.readBoolean()) StdOut.print(1);
            else                           StdOut.print(0);
        }
        if (bitsPerLine != 0) StdOut.println();
        StdOut.println(count + " bits");
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
