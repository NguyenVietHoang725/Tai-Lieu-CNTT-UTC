package com.battleship.controller;

import com.battleship.view.MainFrame;
import com.battleship.controller.menu.MenuController;
import com.battleship.controller.challenge.ChallengeController;
// import các controller khác nếu có

public class AppController {
    private MainFrame mainFrame;
    private MenuController menuController;
    private ChallengeController challengeController;
    // ... các controller khác

    public void start() {
        mainFrame = new MainFrame(); // truyền this để MainFrame gọi lại khi cần
        showMenu();
        mainFrame.setVisible(true);
    }

    public void showMenu() {
        if (menuController == null) {
            menuController = new MenuController(this, mainFrame);
        }
        mainFrame.setContentPane(menuController.getMenuPanel());
        mainFrame.revalidate();
        mainFrame.repaint();
    }

    public void startChallengeMode() {
        if (challengeController == null) {
            challengeController = new ChallengeController(this, mainFrame);
        }
        mainFrame.setContentPane(challengeController.getPlayPanel());
        mainFrame.revalidate();
        mainFrame.repaint();
        challengeController.startGame();
    }

    // ... các phương thức chuyển chế độ khác, ví dụ:
    // public void startVsBotMode() { ... }
    // public void showSettings() { ... }
    // public void showRules() { ... }
}