package slide06.exercise02.utils;

import java.util.Scanner;

public class InputHandler {

	// Attributes
	private Scanner scn;

	public InputHandler() {
		super();
		this.scn = new Scanner(System.in);
	}

	public String getFolderPath() {
		String path = scn.nextLine();
		return path;
	}

	public int getAction() {
		int action = scn.nextInt();
		scn.nextLine();

		return action;
	}

}
