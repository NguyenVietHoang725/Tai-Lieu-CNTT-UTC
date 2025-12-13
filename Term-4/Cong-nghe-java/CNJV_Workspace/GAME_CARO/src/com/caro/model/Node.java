package com.caro.model;

public class Node {
	// Attributes
	private int x, y;
	private char state;
	
	// Constructor
	public Node(int x, int y) {
		this.x = x;
		this.y = y;
		this.state = ' ';
	}
	
	// Getters and Setters
	public int getX() {
		return x;
	}

	public void setX(int x) {
		this.x = x;
	}

	public int getY() {
		return y;
	}

	public void setY(int y) {
		this.y = y;
	}

	public char getState() {
		return state;
	}

	public void setState(char state) {
		this.state = state;
	}
	
	
} 
