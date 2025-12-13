package com.battleship.view.panels.menu;

import java.awt.BorderLayout;

import javax.swing.Box;
import javax.swing.BoxLayout;
import javax.swing.JLayeredPane;
import javax.swing.JPanel;

public class MainMenuPanel extends JLayeredPane {
	private final MenuBackgroundPanel backgroundPanel;
    private final JPanel contentPanel;

    public MainMenuPanel() {
        setLayout(null); // Dùng absolute layout cho JLayeredPane

        // Nền động
        backgroundPanel = new MenuBackgroundPanel();
        backgroundPanel.setBounds(0, 0, 1280, 720); // Đặt kích thước phù hợp với cửa sổ của bạn
        add(backgroundPanel, JLayeredPane.DEFAULT_LAYER);

        // Panel chứa tiêu đề và nút
        contentPanel = new JPanel(new BorderLayout());
        contentPanel.setOpaque(false);
        contentPanel.setBounds(0, 0, 1280, 720);

        // Nút menu ở giữa
        JPanel centerPanel = new JPanel();
        centerPanel.setOpaque(false);
        centerPanel.setLayout(new BoxLayout(centerPanel, BoxLayout.Y_AXIS));
        centerPanel.add(Box.createVerticalGlue());
        MenuButtonsPanel btnPanel = new MenuButtonsPanel();
        centerPanel.add(btnPanel);
        centerPanel.add(Box.createVerticalGlue());
        contentPanel.add(centerPanel, BorderLayout.CENTER);

        add(contentPanel, JLayeredPane.PALETTE_LAYER);
    }

    @Override
    public void setBounds(int x, int y, int width, int height) {
        super.setBounds(x, y, width, height);
        if (backgroundPanel != null) {
			backgroundPanel.setBounds(0, 0, width, height);
		}
        if (contentPanel != null) {
			contentPanel.setBounds(0, 0, width, height);
		}
    }
}
