package com.battleship.view.panels.challenge;

import com.battleship.view.components.board.GameBoardPanel;
import com.battleship.view.utils.ResourceLoader;
import com.battleship.view.utils.ViewConstants;
import java.awt.*;
import javax.swing.*;
import javax.swing.border.Border;
import javax.swing.border.TitledBorder;

public class ChallengeBoardPanel extends JPanel {
    private GameBoardPanel boardPanel;

    public ChallengeBoardPanel(Font font, int cellSize) {
        setLayout(new BorderLayout());
        setOpaque(false);

        // Border ngoài cùng với padding bên trong
        Border outerBorder = BorderFactory.createCompoundBorder(
            BorderFactory.createTitledBorder(
                BorderFactory.createLineBorder(Color.WHITE, 3, true),
                "CHALLENGE BOARD",
                TitledBorder.CENTER,
                TitledBorder.TOP,
                font.deriveFont(Font.BOLD, 18f),
                Color.WHITE
            ),
            BorderFactory.createEmptyBorder(12, 12, 12, 12)
        );

        add(Box.createVerticalStrut(10), BorderLayout.NORTH);

        // Style cho header
        Color headerColor = new Color(255, 215, 0); // Gold
        Font headerFont = font.deriveFont(Font.BOLD, 22f);

        // Border cho header
        Border headerBorder = BorderFactory.createMatteBorder(2, 2, 1, 1, Color.WHITE);

        // Border cho cell (ô bàn cờ)
        Border cellBorder = BorderFactory.createMatteBorder(1, 1, 2, 2, Color.WHITE);

        ImageIcon normalIcon = new ImageIcon(
            ResourceLoader.loadImage(ViewConstants.CHALLENGE_CELL_NORMAL_IMG)
                .getScaledInstance(cellSize, cellSize, Image.SCALE_SMOOTH)
        );

        boardPanel = new GameBoardPanel(
            "", // Không cần title ở đây, đã có outerBorder
            10,
            normalIcon, null, null, null,
            cellSize,
            headerFont,
            headerColor,
            headerBorder,
            cellBorder,
            outerBorder
        );

        add(boardPanel, BorderLayout.CENTER);
    }

    public JButton getButton(int row, int col) {
        return boardPanel.getButton(row, col);
    }
}