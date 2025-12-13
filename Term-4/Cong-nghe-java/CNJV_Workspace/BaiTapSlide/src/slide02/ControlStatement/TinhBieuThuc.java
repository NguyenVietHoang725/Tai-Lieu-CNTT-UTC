package slide02.ControlStatement;

public class TinhBieuThuc {

	public static void main(String[] args) {
		int n = 3;
		double x = 2.5;

		double S = 1, tu = 1, mau = 1, gtN = 1;

		for (int i = 1; i <= n; i++) {
			gtN *= (n - i + 1);
			tu = gtN * Math.pow(x, i);
			mau *= i;
			S += tu / mau;
		}

		System.out.println("S = " + S);
	}

}
