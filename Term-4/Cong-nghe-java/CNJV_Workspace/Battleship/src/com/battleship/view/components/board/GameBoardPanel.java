package com.battleship.view.components.board;

import javax.swing.*;
import javax.swing.border.TitledBorder;
import java.awt.*;


public class GameBoardPanel extends JPanel {
    private JButton[][] buttons;
    private int size;
    private ImageIcon normalIcon, missIcon, hoverIcon, hitIcon;

    public GameBoardPanel(String title, int size,
                          ImageIcon normalIcon, ImageIcon missIcon,
                          ImageIcon hoverIcon, ImageIcon hitIcon) {
        setOpaque(false);
        this.size = size;
        this.buttons = new JButton[size][size];
        this.normalIcon = normalIcon;
        this.missIcon = missIcon;
        this.hoverIcon = hoverIcon;
        this.hitIcon = hitIcon;

        setLayout(new GridBagLayout());

        if (title != null && !title.isEmpty()) {
            setBorder(BorderFactory.createTitledBorder(
                BorderFactory.createLineBorder(Color.GRAY, 2, true),
                title,
                TitledBorder.CENTER,
                TitledBorder.TOP,
                new Font("Arial", Font.BOLD, 16)
            ));
        }

        GridBagConstraints gbc = new GridBagConstraints();
        gbc.fill = GridBagConstraints.BOTH;
        gbc.weightx = 1.0;
        gbc.weighty = 1.0;

        // Header cột (A, B, C, ...)
        for (int col = 0; col <= size; col++) {
            gbc.gridx = col;
            gbc.gridy = 0;
            if (col == 0) {
                add(new JLabel(""), gbc); // góc trống
            } else {
                JLabel label = new JLabel(String.valueOf((char) ('A' + col - 1)), SwingConstants.CENTER);
                label.setFont(new Font("Segoe UI", Font.BOLD, 13));
                add(label, gbc);
            }
        }

        // Header hàng (1, 2, 3, ...) và các button
        for (int row = 1; row <= size; row++) {
            for (int col = 0; col <= size; col++) {
                gbc.gridx = col;
                gbc.gridy = row;
                if (col == 0) {
                    JLabel label = new JLabel(String.valueOf(row), SwingConstants.CENTER);
                    label.setFont(new Font("Segoe UI", Font.BOLD, 13));
                    add(label, gbc);
                } else {
                    buttons[row - 1][col - 1] = new JButton();
                    buttons[row - 1][col - 1].setPreferredSize(new Dimension(32, 32));
                    buttons[row - 1][col - 1].setIcon(normalIcon); // mặc định là normal
                    buttons[row - 1][col - 1].setBorder(null);
                    buttons[row - 1][col - 1].setContentAreaFilled(false);
                    buttons[row - 1][col - 1].setFocusPainted(false);
                    add(buttons[row - 1][col - 1], gbc);
                }
            }
        }
    }

    public int getBoardSize() {
        return size;
    }

    public JButton getButton(int x, int y) {
        return buttons[x][y];
    }
    
    //sử dụng khi đã kết hợp với phần model
//    public void setCellState(int x, int y, CellState state) {
//        switch (state) {
//            case NORMAL:
//                buttons[x][y].setIcon(normalIcon);
//                break;
//            case MISS:
//                buttons[x][y].setIcon(missIcon);
//                break;
//            case HOVER:
//                buttons[x][y].setIcon(hoverIcon);
//                break;
//            case HIT:
//                buttons[x][y].setIcon(hitIcon);
//                break;
//        }
//    }

    public void resetBoard() {
        for (int i = 0; i < size; i++) {
            for (int j = 0; j < size; j++) {
                //setCellState(i, j, CellState.NORMAL);
            	buttons[i][j].setIcon(normalIcon);               
            	buttons[i][j].setEnabled(true);
            }
        }
    }
}
