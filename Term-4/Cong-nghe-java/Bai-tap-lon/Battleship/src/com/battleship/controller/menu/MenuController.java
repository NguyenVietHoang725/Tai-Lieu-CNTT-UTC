package com.battleship.controller.menu;

import com.battleship.controller.BaseController;
import com.battleship.controller.AppController;
import com.battleship.view.MainFrame;
import com.battleship.view.components.buttons.CustomButton;
import com.battleship.view.panels.menu.MainMenuPanel;
import com.battleship.view.panels.menu.MenuButtonsPanel;

import javax.swing.*;
import java.awt.Component;
import java.awt.Container;

public class MenuController extends BaseController {
    private final AppController appController;
    private final MainMenuPanel menuPanel;

    public MenuController(AppController appController, MainFrame mainFrame) {
        super(mainFrame);
        this.appController = appController;
        this.menuPanel = new MainMenuPanel();
        System.out.println("MenuController: MainMenuPanel created " + this.menuPanel);
        initListeners();
    }

    private void initListeners() {
        // Lấy panel chứa các button
        MenuButtonsPanel btnPanel = findMenuButtonsPanel(menuPanel);
        System.out.println("MenuController: btnPanel = " + btnPanel);
        if (btnPanel == null) return;

        // Giả sử thứ tự các button: Challenge, VsBot, Rule, Setting, Quit
        int btnIdx = 0;
        for (Component comp : btnPanel.getComponents()) {
            if (comp instanceof CustomButton) {
                CustomButton btn = (CustomButton) comp;
                int idx = btnIdx;
                btn.addActionListener(e -> handleMenuButton(idx));
                btnIdx++;
            }
        }
    }

    private void handleMenuButton(int idx) {
    	System.out.println("Menu button pressed: " + idx);
        switch (idx) {
            case 0: // Challenge
                appController.startChallengeMode();
                break;
            case 1: // VsBot
                appController.startVsBotMode();
                break;
            case 2: // Rule
            	com.battleship.view.components.dialog.RulesDialog.showDialog(mainFrame);
                break;
            case 3: // Setting
            	com.battleship.view.components.dialog.SettingsDialog.showDialog(mainFrame);
                break;
            case 4: // Quit
                System.exit(0);
                break;
        }
    }

    // Tìm MenuButtonsPanel trong MainMenuPanel
    private MenuButtonsPanel findMenuButtonsPanel(Component parent) {
        if (parent instanceof MenuButtonsPanel) {
            return (MenuButtonsPanel) parent;
        }
        if (parent instanceof Container) {
            for (Component child : ((Container) parent).getComponents()) {
                MenuButtonsPanel found = findMenuButtonsPanel(child);
                if (found != null) return found;
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