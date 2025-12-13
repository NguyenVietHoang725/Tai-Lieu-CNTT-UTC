package Sudoku.Model;

public class Game {
	private Node[][] board;
	private static final int SIZE = 9;
	
	public Game(int[][] initialBoard) {
		board = new Node[SIZE][SIZE];
		for (int i = 0; i < SIZE; i++) {
            for (int j = 0; j < SIZE; j++) {
                board[i][j] = new Node(i, j, initialBoard[i][j]);
            }
        }
	}
	
	public boolean isValidMove(int row, int col, int num) {
		if (board[row][col].getValue() != 0) return false;

        // Check row & column
        for (int i = 0; i < SIZE; i++) {
            if (board[row][i].getValue() == num || board[i][col].getValue() == num) return false;
        }

        // Check zone
        int startRow = (row / 3) * 3;
        int startCol = (col / 3) * 3;
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                if (board[startRow + i][startCol + j].getValue() == num) return false;
            }
        }
        return true;
	}
	
	public void makeMove(int row, int col, int num) {
		if (isValidMove(row, col, num)) {
            board[row][col].setValue(num);
        }
	}
	
	public boolean isSolved() {
		for (int i = 0; i < SIZE; i++) {
            for (int j = 0; j < SIZE; j++) {
                if (board[i][j].getValue() == 0) return false;
            }
        }
        return true;
	}
	
	public Node[][] getBoard() {
		return board;
	}
}
