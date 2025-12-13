package com.battleship.view;

import com.battleship.controller.AppController;

public class GameLauncher {
    public static void main(String[] args) {
        javax.swing.SwingUtilities.invokeLater(() -> {
            AppController app = new AppController();
            app.start();
        });
    }
}