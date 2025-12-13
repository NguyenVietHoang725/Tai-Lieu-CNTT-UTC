package slide03.Inheritance.Paint2D;

public class Line2D extends Shape2D{
	
	// Attributes
	private Point2D start, end;
	
	// Constructor
	public Line2D(Point2D start, Point2D end) {
		this.start = start;
		this.end = end;
	}

	// Getters and Setters
	public Point2D getStart() {
		return start;
	}

	public void setStart(Point2D start) {
		this.start = start;
	}

	public Point2D getEnd() {
		return end;
	}

	public void setEnd(Point2D end) {
		this.end = end;
	}

	@Override
	public String toString() {
		return "Line2D [start=" + start + ", end=" + end + "]";
	}
	
	// Other Methods
	public double area() {
		return 0;
	}

	public double perimeter() {
		return 0;
	}

	public double distance() {
		double dx = end.getX() - start.getX();
		double dy = end.getY() - start.getY();
		return Math.sqrt(dx * dx + dy * dy);
	}

	public void move(double dx, double dy) {
		this.start.move(dx, dy);
		this.end.move(dx, dy);
	}

	public void rotate(double alpha) {
		this.start.rotate(alpha);
		this.end.rotate(alpha);
	}

	public void zoom(double ratio) {
		this.start.zoom(ratio);
		this.end.zoom(ratio);
	}
}
