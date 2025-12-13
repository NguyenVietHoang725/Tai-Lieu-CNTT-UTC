package com.caro.view;

import com.caro.model.Node;

public class CaroView {
	public void displayBoard(Node[][] board) {
        for (Node[] row : board) {
            for (Node node : row) {
                System.out.print("[" + node.getState() + "]");
            }
            System.out.println();
        }
    }

    public void displayMessage(String message) {
        System.out.println(message);
    }
}
