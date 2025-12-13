package slide04.Excercise02;

public class Main {

	public static void main(String[] args) {
		ExecuteString str = new ExecuteString("");
		
		System.out.print("Nhập chuỗi số cách nhau bởi dấu cách: ");
		str.input();
		
		try {
			if (str.validate()) {
				int count = str.countNum();
				System.out.print("true, co " + count + " so.");
			} else {
				throw new InvalidNumberException("false");
			}
		} catch (InvalidNumberException e) {
			System.out.println(e.getMessage());
		} finally {
			str.close();
		}
		
	}

}
