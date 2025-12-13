package com.battleship.view.components.board;

import javax.swing.*;
import javax.swing.border.Border;
import javax.swing.border.TitledBorder;

import com.battleship.enums.CellState;

import java.awt.*;

public class GameBoardPanel extends JPanel {
    private JButton[][] buttons;
    private int size;
    private int cellSize; // Lưu lại để dùng cho getPreferredSize
    private ImageIcon normalIcon, missIcon, hoverIcon, hitIcon, shipIcon;

    public GameBoardPanel(
            String title,
            int size,
            ImageIcon normalIcon, ImageIcon missIcon, ImageIcon hoverIcon, ImageIcon hitIcon, ImageIcon shipIcon,
            int cellSize,
            Font headerFont,
            Color headerColor,
            Border headerBorder,
            Border cellBorder,
            Border outerBorder // (titled border)
    ) {
        setOpaque(false);
        this.size = size;
        this.cellSize = cellSize;
        this.buttons = new JButton[size][size];
        this.normalIcon = normalIcon;
        this.missIcon = missIcon;
        this.hoverIcon = hoverIcon;
        this.hitIcon = hitIcon;
        this.shipIcon = shipIcon;

        setLayout(new BorderLayout());

        // Border ngoài cùng
        if (outerBorder != null) {
            setBorder(outerBorder);
        } else if (title != null && !title.isEmpty()) {
            setBorder(BorderFactory.createTitledBorder(
                BorderFactory.createLineBorder(Color.GRAY, 2, true),
                title,
                TitledBorder.CENTER,
                TitledBorder.TOP,
                new Font("Arial", Font.BOLD, 16)
            ));
        }

        JPanel boardPanel = new JPanel(new GridLayout(size + 1, size + 1, 0, 0));
        boardPanel.setOpaque(false);

        for (int row = 0; row <= size; row++) {
            for (int col = 0; col <= size; col++) {
                if (row == 0 && col == 0) {
                    boardPanel.add(new JLabel(""));
                } else if (row == 0) {
                    JLabel label = new JLabel(String.valueOf((char) ('A' + col - 1)), SwingConstants.CENTER);
                    label.setFont(headerFont != null ? headerFont : new Font("Segoe UI", Font.BOLD, 13));
                    label.setForeground(headerColor != null ? headerColor : Color.WHITE);
                    label.setPreferredSize(new Dimension(cellSize, cellSize));
                    if (headerBorder != null) label.setBorder(headerBorder);
                    boardPanel.add(label);
                } else if (col == 0) {
                    JLabel label = new JLabel(String.valueOf(row), SwingConstants.CENTER);
                    label.setFont(headerFont != null ? headerFont : new Font("Segoe UI", Font.BOLD, 13));
                    label.setForeground(headerColor != null ? headerColor : Color.WHITE);
                    label.setPreferredSize(new Dimension(cellSize, cellSize));
                    if (headerBorder != null) label.setBorder(headerBorder);
                    boardPanel.add(label);
                } else {
                    JButton btn = new JButton();
                    btn.setPreferredSize(new Dimension(cellSize, cellSize));
                    btn.setIcon(normalIcon);
                    if (cellBorder != null) btn.setBorder(cellBorder);
                    btn.setContentAreaFilled(false);
                    btn.setFocusPainted(false);
                    buttons[row - 1][col - 1] = btn;
                    boardPanel.add(btn);
                }
            }
        }

        int boardSize = cellSize * (size + 1);
        boardPanel.setPreferredSize(new Dimension(boardSize, boardSize));

        JPanel wrapper = new JPanel(new GridBagLayout());
        wrapper.setOpaque(false);
        wrapper.add(boardPanel);

        add(wrapper, BorderLayout.CENTER);

        // Đảm bảo kích thước cố định cho GameBoardPanel
        setPreferredSize(new Dimension(boardSize, boardSize));
    }

    @Override
    public Dimension getPreferredSize() {
        int boardSize = cellSize * (size + 1);
        return new Dimension(boardSize, boardSize);
    }

    public int getBoardSize() {
        return size;
    }

    // x: row, y: col
    public JButton getButton(int x, int y) {
    	System.out.println("[DEBUG] getButton: x(row)=" + x + ", y(col)=" + y);
        return buttons[x][y];
    }

    // x: row, y: col
    public void setCellState(int x, int y, CellState state) {
    	System.out.println("[DEBUG] GameBoardPanel.setCellState: x(row)=" + x + ", y(col)=" + y + ", state=" + state);
        JButton btn = getButton(x, y);
        switch (state) {
            case HIT: btn.setIcon(hitIcon); break;
            case MISS: btn.setIcon(missIcon); break;
            case SHIP: btn.setIcon(shipIcon); break;
            case NORMAL: btn.setIcon(normalIcon); break;
            case HOVER: btn.setIcon(hoverIcon); break;
        }
    }

    public void resetBoard() {
        for (int i = 0; i < size; i++) {
            for (int j = 0; j < size; j++) {
                buttons[i][j].setIcon(normalIcon);
                buttons[i][j].setEnabled(true);
            }
        }
    }
}