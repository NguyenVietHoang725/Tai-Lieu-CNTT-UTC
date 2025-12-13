package slide04.Excercise01;

import java.util.Scanner;

public class InputHandler {
	
	// Attributes
	private Scanner scn;
	
	public InputHandler() {
		this.scn = new Scanner(System.in);
	}
	
	public int input(String prompt) {
		System.out.print(prompt);
		return scn.nextInt();
	}
	
	public void close() {
		scn.close();
	}
}
