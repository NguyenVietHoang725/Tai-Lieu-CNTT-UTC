package com.battleship.view.panels.challenge;

import javax.swing.*;
import java.awt.*;

import com.battleship.utils.ViewConstants;
import com.battleship.view.components.common.ImageBackgroundPanel;

public class ChallengePlayPanel extends JPanel {
    private ChallengeInfoAttackPanel infoPanel;
    private ChallengeBoardPanel boardPanel;

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
        infoPanel = new ChallengeInfoAttackPanel(font, leftPanelWidth);
        contentPanel.add(infoPanel, BorderLayout.WEST);
        contentPanel.setBorder(BorderFactory.createEmptyBorder(20, 20, 20, 20));

        // Bên phải: Board
        boardPanel = new ChallengeBoardPanel(font, cellSize);
        contentPanel.add(boardPanel, BorderLayout.CENTER);
        contentPanel.setBorder(BorderFactory.createEmptyBorder(0, 0, 20, 20));

        // Dùng JLayeredPane để overlay content lên background
        JLayeredPane layeredPane = new JLayeredPane();
        layeredPane.setLayout(null);

        bgPanel.setBounds(0, 0, 1280, 720);
        contentPanel.setBounds(20, 20, 1240, 680);
        
        layeredPane.add(bgPanel, JLayeredPane.DEFAULT_LAYER);
        layeredPane.add(contentPanel, JLayeredPane.PALETTE_LAYER);

        this.add(layeredPane, BorderLayout.CENTER);

        this.setPreferredSize(new Dimension(1280, 720));
    }

    public ChallengeInfoAttackPanel getInfoPanel() {
        return infoPanel;
    }

    public ChallengeBoardPanel getBoardPanel() {
        return boardPanel;
    }
}