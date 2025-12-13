package com.battleship.view.panels.challenge;

import javax.swing.*;
import java.awt.*;
import com.battleship.view.components.common.ImageBackgroundPanel;
import com.battleship.view.utils.ViewConstants;

public class ChallengePlayPanel extends JPanel {
	public ChallengePlayPanel(Font font, int leftPanelWidth, int boardPanelSize, int cellSize) {
        setLayout(new BorderLayout());

        // Background
        ImageBackgroundPanel bgPanel = new ImageBackgroundPanel(ViewConstants.CHALLENGE_BG_IMG_PATH);
        setOpaque(false);
        this.add(bgPanel, BorderLayout.CENTER);

        // Layered content
        JPanel contentPanel = new JPanel(new BorderLayout());
        contentPanel.setOpaque(false);

        // Bên trái: Info & Attack
        ChallengeInfoAttackPanel infoAttackPanel = new ChallengeInfoAttackPanel(font, leftPanelWidth);
        contentPanel.add(infoAttackPanel, BorderLayout.WEST);
        contentPanel.setBorder(BorderFactory.createEmptyBorder(20, 20, 20, 20));

        // Bên phải: Board
        ChallengeBoardPanel boardPanel = new ChallengeBoardPanel(font, cellSize);
        contentPanel.add(boardPanel, BorderLayout.CENTER);
        contentPanel.setBorder(BorderFactory.createEmptyBorder(20, 20, 20, 20));

        // Dùng JLayeredPane để overlay content lên background
        JLayeredPane layeredPane = new JLayeredPane();
        layeredPane.setLayout(null);

        bgPanel.setBounds(0, 0, 1280, 720);
        contentPanel.setBounds(0, 0, 1280, 720);

        layeredPane.add(bgPanel, JLayeredPane.DEFAULT_LAYER);
        layeredPane.add(contentPanel, JLayeredPane.PALETTE_LAYER);

        this.add(layeredPane, BorderLayout.CENTER);

        // Đảm bảo resize đúng
        this.addComponentListener(new java.awt.event.ComponentAdapter() {
            public void componentResized(java.awt.event.ComponentEvent evt) {
                Dimension size = getSize();
                bgPanel.setBounds(0, 0, size.width, size.height);
                contentPanel.setBounds(0, 0, size.width, size.height);
                layeredPane.setBounds(0, 0, size.width, size.height);
            }
        });
    }
}
