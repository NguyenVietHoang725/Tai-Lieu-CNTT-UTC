package slide06.exercise01;

import java.io.File;
import java.io.IOException;

public class FileExample {

	public static void main(String[] args) {
		String filePath = "E:\\00_UNIVERSITY\\TERM_04\\Cong_Nghe_Java\\CNJV_Workspace\\BaiTapSlide\\src\\slide06\\exercise01\\example.txt";
        File file = new File(filePath);
		
		try {
			if (file.createNewFile()) {
				System.out.println("Created: " + file.getName());
			} else {
				System.out.println("File exist");
			}
			
			printFileProperties(file);
			
			file.setReadable(false);
			file.setWritable(false);
			
			System.out.println();
			printFileProperties(file);
			
		} catch (IOException e) {
			System.err.println("Error: " + e.getMessage());
		}
		
	}
	
	public static void printFileProperties(File file) {
		System.out.println("File path: " + file.getAbsolutePath());
		System.out.println("File name: " + file.getName());
		System.out.println("Size: " + file.length());
		System.out.println("Is readable: " + file.canRead());
		System.out.println("Is writedable: " + file.canWrite());
		System.out.println("Total space: " + file.getTotalSpace());
		System.out.println("Is directory: " + file.isDirectory());
    }
}
