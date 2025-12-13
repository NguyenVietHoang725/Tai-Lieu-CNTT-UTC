package slide04.Excercise02;

import java.util.Scanner;

public class ExecuteString {
	
	private Scanner scn;
	private String str;

	public ExecuteString(String str) {
		this.scn = new Scanner(System.in);
		this.str = str;
	}
	
	public void input() {
		this.str = scn.nextLine();
	}
	
	public void display() {
		System.out.println(str);
	}
	
	public boolean validate() throws InvalidNumberException {
		boolean check = true;
		
		for (int i = 0; i < str.length(); i++) {
			char c = str.charAt(i);
			if (Character.isLetter(c)) {
				check = false;
				break;
			}
		}
		
		return check;
	}
	
	public int countNum() {
		String[] parts = str.trim().split("\\s+");
	    int count = 0;
	    for (String part : parts) {
	        if (!part.isEmpty()) {
	            count++;
	        }
	    }
	    return count;
	}
	
	public void close() {
		scn.close();
	}
}
