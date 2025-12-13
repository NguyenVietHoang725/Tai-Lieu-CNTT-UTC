package slide02.Array;

public class Main {

	public static void main(String[] args) {
		int[] arr = { 1, -2, -4, 2, 3, 1, 5, 10, -7 };
		
		// Đếm số phần tử dương không chia hết cho 3 trong dãy
		int count1 = 0;
		for (int i = 0; i < arr.length; i++) {
			if (arr[i] % 3 != 0 && arr[i] > 0) {
				count1++;
			}
		}
		System.out.println("So phan tu khong chia het cho 3 trong day la: " + count1);
		
		// Tính tổng các phần tử nằm trong khoảng [-5, 25] và trung bình cộng của chúng
		int sum = 0, count2 = 0;
		double avg = 0;
		for (int i = 0; i < arr.length; i++) {
			if (arr[i] >= -5 && arr[i] < 25) {
				count2++;
				sum += arr[i];
			}
		}
		avg = (double)sum / count2;
		System.out.println("Cac phan tu nam trong khoang [-5, 25] co:");
		System.out.println("Tong = " + sum);
		System.out.printf("Trung binh cong = %.2f%n", avg);
		
		// Xác định phần tử lớn nhất trong dãy chia hết cho 3
		int max = Integer.MIN_VALUE;
		boolean check = false;
		for (int i = 0; i < arr.length; i++) {
			if (arr[i] > max && arr[i] % 3 == 0) {
				max = arr[i];
				check = true;
			}
		}
		if (check) System.out.println("Phan tu lon nhat trong day chia het cho 3 la : " + max);
		else System.out.println("Khong ton tai phan tu chia het cho 3 trong day!");
		
		// Sắp xếp dãy số để các phần tử có giá trị tuyệt đối tăng dần
		for (int i = 0; i < arr.length; i++) {
			
		}
	}

}
