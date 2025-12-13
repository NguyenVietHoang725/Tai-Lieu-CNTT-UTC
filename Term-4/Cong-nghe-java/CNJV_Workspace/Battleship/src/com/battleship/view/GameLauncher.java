package com.battleship.view;

import javax.swing.SwingUtilities;

public class GameLauncher {
	public static void main(String[] args) {
        SwingUtilities.invokeLater(() -> {
            MainFrame mainFrame = new MainFrame();
            mainFrame.setVisible(true);
        });
    }
}
