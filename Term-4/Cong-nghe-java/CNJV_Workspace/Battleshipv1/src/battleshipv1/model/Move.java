package com.battleshipv1.model;

import com.battleshipv1.enums.NodeStatus;

public class Move {

	// Attributes
	private int row, col;
	private NodeStatus prevValue;
	private NodeStatus newValue;

	// Constructor
	public Move(int row, int col, NodeStatus prevValue, NodeStatus newValue) {
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

	public NodeStatus getPrevValue() {
		return prevValue;
	}

	public void setPrevValue(NodeStatus prevValue) {
		this.prevValue = prevValue;
	}

	public NodeStatus getNewValue() {
		return newValue;
	}

	public void setNewValue(NodeStatus newValue) {
		this.newValue = newValue;
	}

}
