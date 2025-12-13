package slide02.ControlStatement;

public class PhuongTrinhBacHai {

	public static void main(String[] args) {
		double a = 4;
		double b = -4;
		double c = -8;
		
		double delta = b*b - 4*a*c;
		
		double x1, x2;
		
		if (delta < 0) {
			System.out.println("Phuong trinh vo nghiem!");
		} else if (delta == 0) {
			x1 = -b / (2 * a);
			System.out.println("Phuong trinh co nghiem kep x1 = x2 = " + x1);
		} else {
			x1 = (-b + Math.sqrt(delta)) / (2 * a);
			x2 = (-b - Math.sqrt(delta)) / (2 * a);
			System.out.println("Phuong trinh co hai nghiem phan biet: x1 = " + x1 + ", x2 = " + x2);
		}
	}

}
