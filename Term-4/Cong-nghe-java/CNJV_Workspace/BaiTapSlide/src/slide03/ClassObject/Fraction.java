package slide03.ClassObject;

public class Fraction {
	
	// Attributes
	private int t;
	private int m;
	
	// Default Constructor
	public Fraction() {
		this.t = 0;
		this.m = 1;
	}
	
	// Parameter Constructor
	public Fraction(int t, int m) {
		if (m == 0) {
			System.out.println("Invalid value. Set m to 1!");
		} else {
			this.t = t;
			this.m = m;
		}
	}
	
	// Getters and Setters
	public int getT() {
		return t;
	}

	public void setT(int t) {
		this.t = t;
	}

	public int getM() {
		return m;
	}

	public void setM(int m) {
		this.m = m;
	}

	// toString Method
	@Override
	public String toString() {
		return "Fraction [t=" + t + ", m=" + m + "]";
	}
	
	// Other Methods
	
	
}
