package com.battleship.controller;

import javax.swing.JOptionPane;

import com.battleship.interfaces.IController;
import com.battleship.view.MainFrame;

public abstract class BaseController implements IController {
    protected MainFrame mainFrame;

    public BaseController(MainFrame mainFrame) {
        this.mainFrame = mainFrame;
    }

    @Override
    public void showMessage(String message) {
        JOptionPane.showMessageDialog(mainFrame, message);
    }

    @Override
    public abstract void switchScreen(String screenName);
}