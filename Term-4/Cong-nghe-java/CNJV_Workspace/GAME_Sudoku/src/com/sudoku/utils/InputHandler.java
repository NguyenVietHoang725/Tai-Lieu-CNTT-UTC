package com.sudoku.utils;

import java.util.Scanner;

import com.sudoku.interfaces.IValidate;
import com.sudoku.model.Game;
import com.sudoku.model.Move;

public class InputHandler implements IValidate {
	
	// Attributes
	private Scanner scn;
	private Game game;
	
	// Constructor
	public InputHandler(Game game) {
		super();
		this.scn = new Scanner(System.in);
		this.game = game;
	}
	
	public Move getMove() {
		int row = scn.nextInt();
		int col = scn.nextInt();
		int newVal = scn.nextInt();
		int prevVal = game.getNode(row - 1, col - 1).getValue();

		return new Move(row - 1, col - 1, prevVal, newVal);
	}

	public int getAction() {
		int action = scn.nextInt();

		return action;
	}
	
	public int[] getHintInput() {
	    int row = scn.nextInt();
	    int col = scn.nextInt();

	    return new int[] { row - 1, col - 1 };
	}

	@Override
	public boolean validate(Move move) {
		if (move.getRow() < 0 || move.getRow() > 8 || move.getCol() < 0 || move.getCol() > 8 || move.getNewValue() < 1
				|| move.getNewValue() > 9) {
			return false;
		}
		return true;
	}
	
	
}
