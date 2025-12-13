package com.battleship.view.main;

import javax.swing.SwingUtilities;

import com.battleship.view.MainFrame;

public class GameLauncher {
	public static void main(String[] args) {
        SwingUtilities.invokeLater(() -> {
            MainFrame mainFrame = new MainFrame();
            mainFrame.setVisible(true);
        });
    }
}
