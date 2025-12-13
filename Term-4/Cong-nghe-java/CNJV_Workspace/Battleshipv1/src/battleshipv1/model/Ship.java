package com.battleshipv1.model;

import java.util.Arrays;

import com.battleshipv1.enums.NodeStatus;

public class Ship {

	// Attributes
	private int size;
	private Node[] nodes;
	
	private boolean isSunk;

	// Constructor
	public Ship(Node[] nodes) {
		this.nodes = nodes;
		this.size = nodes.length;
		this.isSunk = false;
	}

	// Getters and Setters
	public int getSize() {
		return size;
	}

	public void setSize(int size) {
		this.size = size;
	}

	public Node[] getNodes() {
		return nodes;
	}

	public void setNodes(Node[] nodes) {
		this.nodes = nodes;
	}

	public boolean isSunk() {
		return isSunk;
	}

	public void setSunk(boolean isSunk) {
		this.isSunk = isSunk;
	}

	// Other Methods
	public void updateStatus() {
		for (Node node : nodes) {
			if (node.getStatus() != NodeStatus.HIT) {
				isSunk = false;
				return;
			}
		}
		isSunk = true;
	}
	
	public boolean wasSunkNow() {
	    return isSunk && Arrays.stream(nodes).anyMatch(node -> node.getStatus() == NodeStatus.HIT);
	}
}
