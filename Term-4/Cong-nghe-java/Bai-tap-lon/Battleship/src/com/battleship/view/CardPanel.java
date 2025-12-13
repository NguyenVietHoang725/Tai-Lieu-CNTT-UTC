package com.battleship.view;

import java.awt.CardLayout;
import java.awt.Component;
import java.awt.Dimension;

import javax.swing.JPanel;

public class CardPanel extends JPanel {
    private CardLayout cardLayout;

    public CardPanel() {
        cardLayout = new CardLayout();
        setLayout(cardLayout);
    }

    public void showScreen(String name) {
        System.out.println("CardPanel: showScreen " + name);
        cardLayout.show(this, name);
    }

    public void setPanel(String name, Component panel) {
        for (Component comp : getComponents()) {
            if (comp.getName() != null && comp.getName().equals(name)) {
                remove(comp);
                break;
            }
        }

        panel.setName(name);

        // Bổ sung: đảm bảo panel có kích thước chuẩn
        panel.setPreferredSize(new Dimension(1280, 720));

        this.add(panel, name);
        revalidate();
        repaint();
    }
}