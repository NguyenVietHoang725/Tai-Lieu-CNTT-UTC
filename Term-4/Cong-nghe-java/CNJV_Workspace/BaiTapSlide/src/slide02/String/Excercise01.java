package slide02.String;

public class Excercise01 {
	
	public static void main(String[] args) {
		 String input = "Lap trinh Java khong don gian";

	        String removed = input.replace("a", "");

	        String reversed = new StringBuilder(removed).reverse().toString();

	        System.out.println("New string: " + reversed);
	}

}
