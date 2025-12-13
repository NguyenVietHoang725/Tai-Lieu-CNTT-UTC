package com.battleshipv1.model;

public class ShipPlacement {
	private Node[] nodes;

	public ShipPlacement(Node[] nodes) {
		super();
		this.nodes = nodes;
	}

	public Node[] getNodes() {
		return nodes;
	}

	public void setNodes(Node[] nodes) {
		this.nodes = nodes;
	}
	
	
}
