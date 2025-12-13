package com.battleship.view.panels.challenge;

import com.battleship.enums.CellState;
import com.battleship.utils.ResourceLoader;
import com.battleship.utils.ViewConstants;
import com.battleship.view.components.board.GameBoardPanel;

import javax.swing.*;
import javax.swing.border.Border;
import javax.swing.border.TitledBorder;
import java.awt.*;

public class ChallengeBoardPanel extends JPanel {
    private GameBoardPanel boardPanel;

    public ChallengeBoardPanel(Font font, int cellSize) {
        setLayout(new BorderLayout());
        setOpaque(false);

        setBorder(BorderFactory.createCompoundBorder(
            BorderFactory.createTitledBorder(
                BorderFactory.createLineBorder(Color.WHITE, 3, true),
                "CHALLENGE BOARD",
                TitledBorder.CENTER,
                TitledBorder.TOP,
                font.deriveFont(Font.BOLD, 18f),
                Color.WHITE
            ),
            BorderFactory.createEmptyBorder(12, 12, 12, 12) // padding bên trong viền
        ));

        // Style cho header
        Color headerColor = new Color(255, 215, 0); // Gold
        Font headerFont = font.deriveFont(Font.BOLD, 22f);
        Border headerBorder = BorderFactory.createMatteBorder(2, 2, 1, 1, Color.WHITE);
        Border cellBorder = BorderFactory.createMatteBorder(1, 1, 2, 2, Color.WHITE);

        ImageIcon normalIcon = new ImageIcon(ResourceLoader.loadImage(ViewConstants.CELL_NORMAL_IMG)
                .getScaledInstance(cellSize, cellSize, Image.SCALE_SMOOTH));
        ImageIcon hitIcon = new ImageIcon(ResourceLoader.loadImage(ViewConstants.CELL_HIT_IMG)
                .getScaledInstance(cellSize, cellSize, Image.SCALE_SMOOTH));
        ImageIcon missIcon = new ImageIcon(ResourceLoader.loadImage(ViewConstants.CELL_MISS_IMG)
                .getScaledInstance(cellSize, cellSize, Image.SCALE_SMOOTH));
        ImageIcon hoverIcon = new ImageIcon(ResourceLoader.loadImage(ViewConstants.CELL_HOVER_IMG)
                .getScaledInstance(cellSize, cellSize, Image.SCALE_SMOOTH));
        ImageIcon shipIcon = new ImageIcon(ResourceLoader.loadImage(ViewConstants.CELL_SHIP_IMG)
                .getScaledInstance(cellSize, cellSize, Image.SCALE_SMOOTH));

        boardPanel = new GameBoardPanel(
            "",
            10,
            normalIcon, missIcon, hoverIcon, hitIcon, shipIcon,
            cellSize,
            headerFont,
            headerColor,
            headerBorder,
            cellBorder,
            null 
        );

        add(boardPanel, BorderLayout.CENTER);
    }

    
    public void setCellState(int row, int col, CellState state) {
        boardPanel.setCellState(row, col, state);
    }

    public JButton getButton(int row, int col) {
        return boardPanel.getButton(row, col);
    }
}