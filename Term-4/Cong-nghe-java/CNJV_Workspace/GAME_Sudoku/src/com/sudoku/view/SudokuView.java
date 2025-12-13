package com.sudoku.view;

import com.sudoku.model.Node;

public class SudokuView {
	
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

	public void showMessage(String message) {
		System.out.println(message);
	}
	
	public void showSelectAction() {
		showMessage("Select action (1/2/3/4): ");
		showMessage("1. Enter a move.");
		showMessage("2. Undo.");
		showMessage("3. Redo.");
		showMessage("4. Get hint.");
		showMessage("Enter your action: ");
	}
}
