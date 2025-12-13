package com.sudoku.model;

public class Move {

	// Attributes
	private int row, col;
	private int prevValue;
	private int newValue;

	// Constructor
	public Move(int row, int col, int prevValue, int newValue) {
		super();
		this.row = row;
		this.col = col;
		this.prevValue = prevValue;
		this.newValue = newValue;
	}

	// Getters and Setters
	public int getRow() {
		return row;
	}

	public void setRow(int row) {
		this.row = row;
	}

	public int getCol() {
		return col;
	}

	public void setCol(int col) {
		this.col = col;
	}

	public int getPrevValue() {
		return prevValue;
	}

	public void setPrevValue(int prevValue) {
		this.prevValue = prevValue;
	}

	public int getNewValue() {
		return newValue;
	}

	public void setNewValue(int newValue) {
		this.newValue = newValue;
	}

}
