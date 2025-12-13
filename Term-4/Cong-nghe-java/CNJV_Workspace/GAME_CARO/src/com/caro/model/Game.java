package com.caro.model;

public class Game {
	// Attributes
	private Node[][] board;
	private int size;
	
	// Constructor
	public Game(int size) {
		this.size = size;
		board = new Node[size][size];
		for (int i = 0; i < size; i++) {
			for (int j = 0; j < size; j++) {
				board[i][j] = new Node(i, j);
			}
		}
	}
	
	public boolean makeMove(int x, int y, char player) {
		if (board[x][y].getState() == ' ') {
			board[x][y].setState(player);
			return true;
		}
		return false;
	}
	
	public char checkWin() {
		int winCondition = 5; // Number of consecutive marks needed to win

	    // Check horizontal, vertical, and two diagonal directions
	    for (int i = 0; i < size; i++) {
	        for (int j = 0; j < size; j++) {
	            char player = board[i][j].getState();
	            if (player == ' ') continue;

	            // Check horizontal (left to right)
	            if (j + winCondition <= size) {
	                if (checkDirection(i, j, 0, 1, player)) return player;
	            }

	            // Check vertical (top to bottom)
	            if (i + winCondition <= size) {
	                if (checkDirection(i, j, 1, 0, player)) return player;
	            }

	            // Check diagonal (top-left to bottom-right)
	            if (i + winCondition <= size && j + winCondition <= size) {
	                if (checkDirection(i, j, 1, 1, player)) return player;
	            }

	            // Check anti-diagonal (top-right to bottom-left)
	            if (i + winCondition <= size && j - winCondition + 1 >= 0) {
	                if (checkDirection(i, j, 1, -1, player)) return player;
	            }
	        }
	    }
	    return ' '; // No winner yet
    }
	
	// Helper method to check consecutive marks in a direction
	private boolean checkDirection(int x, int y, int dx, int dy, char player) {
	    for (int k = 1; k < 5; k++) { // Check next 4 positions
	        int newX = x + k * dx;
	        int newY = y + k * dy;
	        if (board[newX][newY].getState() != player) {
	            return false;
	        }
	    }
	    return true;
	}
	
	public Node[][] getBoard() {
		return board;
	}
}
