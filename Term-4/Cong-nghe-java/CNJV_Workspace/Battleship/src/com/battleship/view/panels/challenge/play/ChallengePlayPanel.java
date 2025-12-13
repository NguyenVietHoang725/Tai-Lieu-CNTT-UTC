package com.battleship.view.panels.challenge.play;

import java.awt.*;
import javax.swing.*;
import javax.swing.border.EmptyBorder;

public class ChallengePlayPanel extends JLayeredPane {
    private final ChallengeBackgroundPanel backgroundPanel;
    private final JPanel contentPanel;

    public ChallengePlayPanel(Font font) {
        setLayout(null); // Absolute layout cho JLayeredPane

        // Nền background
        backgroundPanel = new ChallengeBackgroundPanel();
        backgroundPanel.setBounds(0, 0, 1280, 720); // Kích thước mặc định, có thể thay đổi
        add(backgroundPanel, JLayeredPane.DEFAULT_LAYER);

        // Panel chứa các thành phần chức năng
        contentPanel = new JPanel(new BorderLayout());
        contentPanel.setOpaque(false);
        contentPanel.setBounds(0, 0, 1280, 720);

        // Bên phải: Bàn chơi
        JPanel rightPanel = new JPanel(new BorderLayout());
        rightPanel.setOpaque(false);
        JLabel boardLabel = new JLabel("Challenge Board", SwingConstants.CENTER);
        boardLabel.setFont(font.deriveFont(Font.BOLD, 16f));
        rightPanel.add(boardLabel, BorderLayout.NORTH);
        rightPanel.setBorder(new EmptyBorder(20, 20, 20, 20));

        ChallengeBoardPanel challengeBoardPanel = new ChallengeBoardPanel();
        rightPanel.add(challengeBoardPanel, BorderLayout.CENTER);

        contentPanel.add(rightPanel, BorderLayout.CENTER);

        // Bên trái: Các nút chức năng
        ChallengeButtonsPanel buttonsPanel = new ChallengeButtonsPanel(font);
        contentPanel.add(buttonsPanel, BorderLayout.WEST);

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