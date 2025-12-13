package slide06.exercise02.controller;

import java.util.Random;

import slide06.exercise02.interfaces.IController;
import slide06.exercise02.model.VirussDetection;
import slide06.exercise02.utils.InputHandler;
import slide06.exercise02.view.AppView;

public class AppController implements IController {

    private VirussDetection model;
    private AppView view;
    private InputHandler input;

    // Constructor
    public AppController() {
        this.view = new AppView();
        this.input = new InputHandler();
    }

    @Override
    public void init() {
        view.showMessage01("Welcome to Virus Detection Application!");
    }

    @Override
    public void start() {
        while (true) {
            view.showSelectAction();
            int choice = input.getAction();

            switch (choice) {
                case 1 -> handleScan();
                case 2 -> handleRemove();
                case 3 -> {
                    close();
                    return;
                }
                default -> view.showMessage01("Invalid choice. Please select 1, 2, or 3.");
            }
        }
    }

    private void handleScan() {
        view.showMessage02("Enter folder path: ");
        String path = input.getFolderPath();
        try {
            model = new VirussDetection(path);
            view.showMessage01("Scanning folder: " + path);
            model.scan(model.getFolder());
            view.showMessage01("Scan complete.");
            view.showMessage01("Total .exe files: " + model.getExeCounter());
            view.showMessage01("Total .bat files: " + model.getBatCounter());
        } catch (IllegalArgumentException e) {
            view.showMessage01("Error: " + e.getMessage());
        }
    }

    private void handleRemove() {
    	if (model == null) {
            view.showMessage01("You need to scan before removing files!");
            return;
        }

        Random random = new Random();

        int removedExe = random.nextInt(model.getExeCounter() + 1); 
        int removedBat = random.nextInt(model.getBatCounter() + 1); 

        view.showMessage01("Simulating virus removal...");
        view.showMessage01("Removed " + removedExe + " .exe files.");
        view.showMessage01("Removed " + removedBat + " .bat files.");
    }

    @Override
    public void close() {
        view.showMessage01("Goodbye!");
        System.exit(0);
    }
}
