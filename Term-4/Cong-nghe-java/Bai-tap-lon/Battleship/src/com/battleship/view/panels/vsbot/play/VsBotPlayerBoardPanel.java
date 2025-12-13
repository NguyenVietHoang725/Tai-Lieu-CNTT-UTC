package com.battleship.view.panels.vsbot.play;

import com.battleship.enums.CellState;
import com.battleship.model.board.Board;
import com.battleship.model.board.Node;
import com.battleship.utils.ResourceLoader;
import com.battleship.utils.ViewConstants;
import com.battleship.view.components.board.GameBoardPanel;

import javax.swing.*;
import javax.swing.border.Border;
import javax.swing.border.TitledBorder;
import java.awt.*;

public class VsBotPlayerBoardPanel extends JPanel {
    private GameBoardPanel boardPanel;

    public VsBotPlayerBoardPanel(Font font, int cellSize) {
        setLayout(new BorderLayout());
        setOpaque(false);

        Border outerBorder = BorderFactory.createCompoundBorder(
            BorderFactory.createTitledBorder(
                BorderFactory.createLineBorder(Color.WHITE, 3, true),
                "YOUR SHIPS",
                TitledBorder.CENTER,
                TitledBorder.TOP,
                font.deriveFont(Font.BOLD, 18f),
                Color.WHITE
            ),
            BorderFactory.createEmptyBorder(12, 12, 12, 12)
        );

        add(Box.createVerticalStrut(10), BorderLayout.NORTH);

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
            "", // No title here, already in outerBorder
            10,
            normalIcon, missIcon, hoverIcon, hitIcon, shipIcon,
            cellSize,
            headerFont,
            headerColor,
            headerBorder,
            cellBorder,
            outerBorder
        );

        add(boardPanel, BorderLayout.CENTER);
    }
    
    public void updatePlayerBoard(Board board) {
        for (int x = 0; x < 10; x++) {
            for (int y = 0; y < 10; y++) {
                Node node = board.getNode(x, y);
                if (node.isHasShip() && node.isHit()) {
                    boardPanel.setCellState(x, y, CellState.HIT);
                } else if (node.isHasShip()) {
                    boardPanel.setCellState(x, y, CellState.SHIP);
                } else if (node.isHit()) {
                    boardPanel.setCellState(x, y, CellState.MISS);
                } else {
                    boardPanel.setCellState(x, y, CellState.NORMAL);
                }
            }
        }
    }

    public JButton getButton(int row, int col) {
        return boardPanel.getButton(row, col);
    }

    public void setCellState(int row, int col, com.battleship.enums.CellState state) {
        boardPanel.setCellState(row, col, state);
    }
}