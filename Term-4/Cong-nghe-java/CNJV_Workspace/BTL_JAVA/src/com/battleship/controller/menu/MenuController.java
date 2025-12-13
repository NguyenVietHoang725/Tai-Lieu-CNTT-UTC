package com.battleship.controller.menu;

import com.battleship.controller.BaseController;
import com.battleship.controller.AppController;
import com.battleship.view.MainFrame;
import com.battleship.view.panels.menu.MainMenuPanel;
import com.battleship.view.panels.menu.MenuButtonsPanel;

import javax.swing.*;
import java.awt.Component;

public class MenuController extends BaseController {
    private final AppController appController;
    private final MainMenuPanel menuPanel;

    public MenuController(AppController appController, MainFrame mainFrame) {
        super(mainFrame);
        this.appController = appController;
        this.menuPanel = new MainMenuPanel();
        initListeners();
    }

    private void initListeners() {
        // Lấy panel chứa các button
        MenuButtonsPanel btnPanel = findMenuButtonsPanel(menuPanel);
        if (btnPanel == null) return;

        // Giả sử thứ tự các button: Challenge, VsBot, Rule, Setting, Quit
        int btnIdx = 0;
        for (Component comp : btnPanel.getComponents()) {
            if (comp instanceof com.battleship.view.components.buttons.CustomButton) {
                com.battleship.view.components.buttons.CustomButton btn = (com.battleship.view.components.buttons.CustomButton) comp;
                int idx = btnIdx;
                btn.addActionListener(e -> handleMenuButton(idx));
                btnIdx++;
            }
        }
    }

    private void handleMenuButton(int idx) {
        switch (idx) {
            case 0: // Challenge
                appController.startChallengeMode();
                break;
            case 1: // VsBot
//                appController.startVsBotMode();
                break;
            case 2: // Rule
                showMessage("Chức năng xem luật chơi sẽ được cập nhật!");
                break;
            case 3: // Setting
                showMessage("Chức năng cài đặt sẽ được cập nhật!");
                break;
            case 4: // Quit
                System.exit(0);
                break;
        }
    }

    // Tìm MenuButtonsPanel trong MainMenuPanel
    private MenuButtonsPanel findMenuButtonsPanel(MainMenuPanel menuPanel) {
        for (Component comp : menuPanel.getComponents()) {
            if (comp instanceof JPanel) {
                JPanel panel = (JPanel) comp;
                for (Component sub : panel.getComponents()) {
                    if (sub instanceof MenuButtonsPanel) {
                        return (MenuButtonsPanel) sub;
                    }
                }
            }
        }
        return null;
    }

    @Override
    public void switchScreen(String screenName) {
        mainFrame.switchScreen(screenName);
    }

    public MainMenuPanel getMenuPanel() {
        return menuPanel;
    }
}