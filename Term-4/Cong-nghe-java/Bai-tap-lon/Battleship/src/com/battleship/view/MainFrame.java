package com.battleship.view;

import javax.swing.*;

public class MainFrame extends JFrame {
    private CardPanel cardPanel;

    public MainFrame(Object appController) {
        super("Battle Ship Game");
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setSize(1280, 740);
        setLocationRelativeTo(null);
        setResizable(false);

        cardPanel = new CardPanel();
        setContentPane(cardPanel);
    }

    public void switchScreen(String name) {
        cardPanel.showScreen(name);
    }

    public CardPanel getCardPanel() {
        return cardPanel;
    }
}