package com.battleshipv1.model;

import com.battleshipv1.enums.NodeStatus;

public class Node {
	
	// Attributes
	private int x;
	private int y;
	private NodeStatus status;
	
	// Constructor
	public Node(int x, int y) {
		super();
		this.x = x;
		this.y = y;
		this.status = NodeStatus.EMPTY;
	}

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

	public NodeStatus getStatus() {
		return status;
	}

	public void setStatus(NodeStatus status) {
		this.status = status;
	}
	
	// Other Methods
	public boolean isOccupied() {
		return status == NodeStatus.SHIP;
	}
}
