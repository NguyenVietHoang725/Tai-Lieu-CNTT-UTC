package slide06.exercise02.view;

public class AppView {
	
	public void showMessage01(String message) {
		System.out.println(message);
	}
	
	public void showMessage02(String message) {
		System.out.print(message);
	}
	
	public void showSelectAction() {
		showMessage01("Select action (1/2/3): ");
		showMessage01("1. Scan.");
		showMessage01("2. Remove.");
		showMessage01("3. Close.");
	
	}
}
