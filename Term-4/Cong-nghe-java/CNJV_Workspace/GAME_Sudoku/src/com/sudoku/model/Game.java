package com.sudoku.model;

import java.util.HashSet;
import java.util.Set;
import java.util.Stack;

import com.sudoku.interfaces.IValidate;

public class Game implements IValidate {

	// Attributes
	private Node[][] board;
	private static final int SIZE = 9;
	private Stack<Move> undoStack = new Stack<>();
	private Stack<Move> redoStack = new Stack<>();

	// Constructor
	public Game(int[][] initialBoard) {
		board = new Node[SIZE][SIZE];
		for (int i = 0; i < SIZE; i++) {
			for (int j = 0; j < SIZE; j++) {
				boolean isFixed = false;
				board[i][j] = new Node(i, j, initialBoard[i][j], isFixed);
			}
		}
	}
	
	// Method : Update value from a move
	public void updateValue(Move move) {
		if (validate(move)) {
			board[move.getRow()][move.getCol()].setValue(move.getNewValue());
		}
		
		// Push move to undoStack to archive 
		undoStack.push(move);
		// 
		redoStack.clear();
	}

	// Method : Check valid/invalid move
	@Override
	public boolean validate(Move move) {
		return validateNode(move) && validateRowAndCol(move) && validateZone(move);
	}
	
	// Sub method : Check node from move
	public boolean validateNode(Move move) {
		if (board[move.getRow()][move.getCol()].getValue() != 0) {
			return false;
		}

		return true;
	}
	
	// Sub method : Check row and column from move
	public boolean validateRowAndCol(Move move) {
		for (int i = 0; i < SIZE; i++) {
			if (board[move.getRow()][i].getValue() == move.getNewValue()
					|| board[i][move.getCol()].getValue() == move.getNewValue()) {
				return false;
			}
		}

		return true;
	}
	
	// Sub method : Check zone 3x3 from move
	public boolean validateZone(Move move) {
		int startRow = (move.getRow() / 3) * 3;
		int startCol = (move.getCol() / 3) * 3;
		
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				if (board[startRow + i][startCol + i].getValue() == move.getNewValue()) {
					return false;
				}
			}
		}
		
		return true;
	}
	
	public void undo() {
		if (undoStack.isEmpty()) 
			return;
		
		Move lastMove = undoStack.pop();
		board[lastMove.getRow()][lastMove.getCol()].setValue(lastMove.getPrevValue());
		redoStack.push(lastMove);
	}
	
	public void redo() {
		if (redoStack.isEmpty())  
			return;
		
		Move move = redoStack.pop();
		board[move.getRow()][move.getCol()].setValue(move.getNewValue());
		undoStack.push(move);
	}
	
	public Set<Integer> hint(int row, int col) {
        Set<Integer> used = new HashSet<>();

        if (board[row][col].getValue() != 0) {
            return Set.of();
        }

        for (int i = 0; i < SIZE; i++) {
            int val = board[row][i].getValue();
            if (val != 0) used.add(val);
        }

        for (int i = 0; i < SIZE; i++) {
            int val = board[i][col].getValue();
            if (val != 0) used.add(val);
        }

        int startRow = (row / 3) * 3;
        int startCol = (col / 3) * 3;
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                int val = board[startRow + i][startCol + j].getValue();
                if (val != 0) used.add(val);
            }
        }

        Set<Integer> hintSet = new HashSet<>();
        for (int i = 1; i <= 9; i++) {
            if (!used.contains(i)) {
                hintSet.add(i);
            }
        }

        return hintSet;
    }
	
	public boolean isSolved() {
		for (int i = 0; i < SIZE; i++) {
			for (int j = 0; j < SIZE; j++) {
				if (board[i][j].getValue() == 0) {
					return false;
				}
			}
		}
		return true;
	}

	public Node[][] getBoard() {
		return board;
	}

	public Node getNode(int row, int col) {
		return board[row][col];
	}
	
	public int getSIZE() {
		return SIZE;
	}
}
