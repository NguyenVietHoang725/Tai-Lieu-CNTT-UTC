package com.battleship.view.panels.menu;

import java.awt.Component;
import java.awt.Dimension;
import java.awt.Font;

import javax.swing.Box;
import javax.swing.BoxLayout;
import javax.swing.JButton;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.OverlayLayout;

import com.battleship.view.utils.ResourceLoader;
import com.battleship.view.utils.ViewConstants;

public class MainMenuPanel extends JPanel {
	public MainMenuPanel() {
        setLayout(new OverlayLayout(this));

        // Background GIF
        JLabel bgLabel = new JLabel(ResourceLoader.loadIcon(ViewConstants.BG_GIF_PATH));
        bgLabel.setAlignmentX(0.5f);
        bgLabel.setAlignmentY(0.5f);
        add(bgLabel);

        // Foreground panel
        JPanel fgPanel = new JPanel();
        fgPanel.setOpaque(false);
        fgPanel.setLayout(new BoxLayout(fgPanel, BoxLayout.Y_AXIS));
        fgPanel.setAlignmentX(0.5f);
        fgPanel.setAlignmentY(0.5f);

        JLabel logoLabel = new JLabel(ResourceLoader.loadIcon(ViewConstants.LOGO_PATH, 80, 80));
        logoLabel.setAlignmentX(Component.CENTER_ALIGNMENT);
        fgPanel.add(Box.createVerticalStrut(60));
        fgPanel.add(logoLabel);
        fgPanel.add(Box.createVerticalStrut(30));

        String[] btnNames = {"CHALLENGE MODE", "VS. BOT MODE", "RULES", "SETTING", "QUIT"};
        for (int i = 0; i < btnNames.length; i++) {
            JButton btn = new JButton(btnNames[i]);
            btn.setAlignmentX(Component.CENTER_ALIGNMENT);
            btn.setMaximumSize(new Dimension((i < 2) ? 260 : 220, (i < 2) ? 60 : 48));
            btn.setPreferredSize(new Dimension((i < 2) ? 260 : 220, (i < 2) ? 60 : 48));
            btn.setFont(new Font("Segoe UI", (i < 2) ? Font.BOLD : Font.PLAIN, (i < 2) ? 20 : 16));
            btn.setBackground(ViewConstants.BUTTON_COLOR);
            btn.setForeground(ViewConstants.TEXT_COLOR);
            btn.setFocusPainted(false);
            btn.setBorderPainted(false);
            fgPanel.add(btn);
            fgPanel.add(Box.createVerticalStrut(18));
        }
        fgPanel.add(Box.createVerticalGlue());

        add(fgPanel);
    }
}
