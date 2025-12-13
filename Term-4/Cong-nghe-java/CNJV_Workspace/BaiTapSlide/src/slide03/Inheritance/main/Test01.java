package slide03.Inheritance.main;

import java.io.File;

public class Test01 {

	public static void main(String[] args) {
		String inputFile = "input/input1.txt";
		File file = new File(inputFile);
		System.out.println(file.getAbsolutePath());
		System.out.println(file.exists()); 

	}

}
