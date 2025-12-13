package slide03.Inheritance.Paint2D;

public class Point2D extends Shape2D {
	
	// Attributes
	private double x;
	private double y;
	
	// Constructor
	public Point2D(double x, double y) {
		super();
		this.x = x;
		this.y = y;
	}
	
	// Getters and Setters
	public double getX() {
		return x;
	}

	public void setX(double x) {
		this.x = x;
	}

	public double getY() {
		return y;
	}

	public void setY(double y) {
		this.y = y;
	}
	
	@Override
	public String toString() {
		return "Point2D [x=" + x + ", y=" + y + "]";
	}
	
	// Other Methods
	public double area() {
		return 0;
	}
	
	public double perimeter() {
		return 0;
	}
	
	public double distance() {
		return Math.sqrt(this.x * this.x + this.y * this.y);
	}
	
	public void move(double dx, double dy){
		this.x += dx;
		this.y += dy;
	}
	
	public void rotate(double alpha){
		double radian = Math.toRadians(alpha);
        double tempX = this.x;
        double tempY = this.y;

        this.x = tempX * Math.cos(radian) - tempY * Math.sin(radian);
        this.y = tempX * Math.sin(radian) + tempY * Math.cos(radian);
	}
	
	public void zoom(double ratio){
		this.x = this.x * ratio; 
        this.y = this.y * ratio;
	}
}
