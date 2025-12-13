package slide04.Excercise01;

public class Main {

	public static void main(String[] args) {
		RandomizeArray random = new RandomizeArray();
		InputHandler ip = new InputHandler();
		
		random.displayArray();
		
		int n = ip.input("Enter n : ");
		int val = random.getNumberAt(n);
		System.out.println(val);
	}

}
