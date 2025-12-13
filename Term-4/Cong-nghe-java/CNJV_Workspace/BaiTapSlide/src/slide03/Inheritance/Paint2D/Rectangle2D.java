package slide03.Inheritance.Paint2D;

public class Rectangle2D extends Shape2D {
	
	// Attributes
	private Line2D width;
	private Line2D height;
	
	// Constructor
	public Rectangle2D(Line2D width, Line2D height) {
		this.width = width;
		this.height = height;
	}
	
	// Getters and Setters
	public Line2D getWidth() {
		return width;
	}

	public void setWidth(Line2D width) {
		this.width = width;
	}

	public Line2D getHeight() {
		return height;
	}

	public void setHeight(Line2D height) {
		this.height = height;
	}

	@Override
	public String toString() {
		return "Rectangle2D [width=" + width + ", height=" + height + "]";
	}
	
	// Other Methods
	public double area() {
		return this.width.distance() * this.height.distance();
	}
	
	public double perimeter() {
		return (this.width.distance() + this.height.distance()) * 2;
	}
	
	public void move(double dx, double dy) {
		this.width.move(dx, dy);
		this.height.move(dx, dy);
	}
	
	public void rotate(double alpha) {
		this.width.rotate(alpha);
		this.height.rotate(alpha);
	}
	
	public void zoom(double ratio) {
		this.width.zoom(ratio);
		this.height.zoom(ratio);
	}	
}
