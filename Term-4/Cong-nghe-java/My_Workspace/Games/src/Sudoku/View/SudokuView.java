package Sudoku.View;

import java.util.Scanner;

import Sudoku.Model.Node;

public class SudokuView {
    private Scanner scanner;

    public SudokuView() {
        scanner = new Scanner(System.in);
    }

    public void displayBoard(Node[][] board) {
        System.out.println("Sudoku Board:");
        for (int i = 0; i < 9; i++) {
            if (i % 3 == 0) System.out.println("+-------+-------+-------+");
            for (int j = 0; j < 9; j++) {
                if (j % 3 == 0) System.out.print("| ");
                System.out.print((board[i][j].getValue() == 0 ? "." : board[i][j].getValue()) + " ");
            }
            System.out.println("|");
        }
        System.out.println("+-------+-------+-------+");
    }

    public int[] getMove() {
        System.out.println("Enter ur move (x, y, val) : ");
        int row = scanner.nextInt();
        int col = scanner.nextInt();
        int num = scanner.nextInt();
        return new int[]{row, col, num};
    }

    public void showMessage(String message) {
        System.out.println(message);
    }
}
