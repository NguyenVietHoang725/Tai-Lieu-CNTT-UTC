package slide03.Inheritance.interfaces;

import slide03.Inheritance.Paint2D.Point2D;
import slide03.Inheritance.Paint2D.Rectangle2D;

public interface IShapeO {
	
	double area();
	
	double perimeter();
	
	double distance();
	
	Point2D getCenter();
	
	Rectangle2D getBoundary();
}
