package com.caro.utils;

import java.util.InputMismatchException;
import java.util.Scanner;

public class InputHandler {
	private Scanner scn = new Scanner(System.in);

	public int[] getInput() throws InputMismatchException {
		try {
			int row = scn.nextInt();
			int col = scn.nextInt();

			return new int[] { row - 1, col - 1 };
		} catch (InputMismatchException e) {
			throw new InputMismatchException("Invalid input. Please enter integers only.");
		}
	}

	public void clear() {
		scn.nextLine(); // Clear the invalid input from the buffer
	}

	public void close() {
		scn.close();
	}
}
