package slide04.Excercise01;

import java.util.Arrays;
import java.util.Random;

public class RandomizeArray {
	
	// Attributes
	private static final int LIMIT = 100;
	private int SIZE;
	private Random random;
	private int[] arr;
	
	// Default Constructor
	public RandomizeArray() {
		this.random = new Random();
		SIZE = random.nextInt(LIMIT);
		this.arr = new int[SIZE];
		generateArray();
	}
	
	// Method 1: Generate a Random Array
	public void generateArray() {
		for (int i = 0; i  < SIZE; i++) {
			arr[i] = random.nextInt();
		}
	}
	
	// Method 2: Get Created Random Array
	public int[] getArray() {
		return Arrays.copyOf(arr, arr.length);
	}
	
	// Method 3: Display Array
	public void displayArray() {
		System.out.println("Randomize array : ");
		for (int i = 0; i < arr.length; i++) {
			if (i != arr.length - 1) {
				System.out.print(arr[i] + ", ");
			} else {
				System.out.println(arr[i] + ".");
			}
		}
	}
	
	// Method 4: Get Number at Index
	public int getNumberAt(int index) {
		if (index >= 0 && index < SIZE) {
			return arr[index];
		}
		throw new IndexOutOfBoundsException("Invalid index: " + index + ". Valid range: 0-" + arr.length);
	}
	
	// Method 5: Resize Array
	public void resize(int newSize) {
        if (newSize <= 0) {
            throw new IllegalArgumentException("Size must be positive");
        }
        this.arr = new int[newSize];
        generateArray();
    }
}	
