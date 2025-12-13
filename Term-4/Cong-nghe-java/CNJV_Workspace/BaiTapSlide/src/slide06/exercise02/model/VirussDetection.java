package slide06.exercise02.model;

import java.io.File;

public class VirussDetection {
	
	// Attributes
	private File folder;
	private int exeCounter;
	private int batCounter;
	
	// Constructor
	public VirussDetection(String folderPath) {
		this.folder = new File(folderPath);
		if (!this.folder.exists() || !this.folder.isDirectory()) {
			throw new IllegalArgumentException("Folder is not exists or Folder is invalid: " + folderPath);
		}
		this.exeCounter = 0;
		this.batCounter = 0;
	}
	
	// Getters and Setter
	public File getFolder() {
		return folder;
	}

	public void setFolder(File folder) {
		this.folder = folder;
	}

	public int getExeCounter() {
		return exeCounter;
	}

	public void setExeCounter(int exeCounter) {
		this.exeCounter = exeCounter;
	}

	public int getBatCounter() {
		return batCounter;
	}

	public void setBatCounter(int batCounter) {
		this.batCounter = batCounter;
	}
	
	// Other Methods
	public void scan(File folder) {
		File[] files = folder.listFiles();
		
		if (files == null) return;
		
		for (File file : files) {
			if (file.isDirectory()) {
				scan(file);
			} else if (file.isFile()) {
				String name = file.getName().toLowerCase();
				
				if (name.endsWith(".exe")) {
					System.out.println("Detect file .EXE: " + file.getAbsolutePath());
					this.exeCounter++;
				} else if (name.endsWith(".bat")) {
					System.out.println("Detect file .BAT: " + file.getAbsolutePath());
					this.batCounter++;
				}
			}
		}
		
	}

}
