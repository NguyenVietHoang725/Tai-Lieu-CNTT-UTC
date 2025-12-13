package com.battleship.view.components.dialog;

import com.battleship.utils.ResourceLoader;
import com.battleship.utils.ViewConstants;
import com.battleship.view.components.buttons.CustomButton;
import com.battleship.view.components.common.ImageBackgroundPanel;

import javax.swing.*;
import java.awt.*;

public class RulesDialog {
    public static void showDialog(JFrame parentFrame) {
        int dialogWidth = 912;  // Tăng kích thước
        int dialogHeight = 501;

        // Dialog setup
        JDialog dialog = new JDialog(parentFrame, true);
        dialog.setUndecorated(true);
        dialog.setSize(dialogWidth, dialogHeight);
        dialog.setLocationRelativeTo(parentFrame);
        dialog.setLayout(null);

        // LayeredPane
        JLayeredPane layeredPane = new JLayeredPane();
        layeredPane.setLayout(null);
        layeredPane.setBounds(0, 0, dialogWidth, dialogHeight);

        // Background
        ImageBackgroundPanel background = new ImageBackgroundPanel(ViewConstants.RULE_DIALOG_BG);
        background.setBounds(0, 0, dialogWidth, dialogHeight);
        layeredPane.add(background, JLayeredPane.DEFAULT_LAYER);

        // Content Panel with GridBagLayout
        JPanel contentPanel = new JPanel(new GridBagLayout());
        contentPanel.setOpaque(false);
        GridBagConstraints gbc = new GridBagConstraints();
        gbc.gridx = 0;
        gbc.gridy = 0;
        gbc.anchor = GridBagConstraints.WEST;
        gbc.insets = new Insets(5, 5, 5, 5);

        // Use system default font for content
        Font font = ResourceLoader.loadFont(ViewConstants.FONT_PATH, 18f);
        String[][] rules = {
            {"1. Objective", "Destroy all enemy ships."},
            {"", ""},
            {"2. Game Modes", "- Single Player: You shoot at a map with randomly placed ships."},
            {"", "- Play with Bot: You and the bot take turns shooting. Whoever destroys all enemy ships first wins."},
            {"", ""},
            {"3. How to Play", "- Each turn, you select one tile on the opponent's map to shoot."},
            {"", "+ Hit: you hit a ship."},
            {"", "+ Miss: you miss."},
            {"", "- In bot mode, the bot will also shoot after your turn."},
            {"", ""},
            {"4. Win – Lose Conditions", "- Win: When you destroy all enemy ships."},
            {"", "- Lose:"},
            {"", "+ Out of allowed shots (Single Player)."},
            {"", "+ Out of time (Single Player)."},
            {"", "+ All your ships are destroyed by the bot (Play with Bot)."}
        };


        for (String[] rule : rules) {
            // Label cho đề mục (nếu có)
            if (!rule[0].isEmpty()) {
                JLabel titleLabel = new JLabel(rule[0]);
                titleLabel.setFont(font);
                titleLabel.setForeground(new Color(255, 215, 0)); // Màu vàng
                contentPanel.add(titleLabel, gbc);
                gbc.gridy++;
            }
           
            // Label cho nội dung
            if (!rule[1].isEmpty()) {
                JLabel contentLabel = new JLabel(rule[1]);
                contentLabel.setFont(font);
                contentLabel.setForeground(Color.WHITE);
                contentPanel.add(contentLabel, gbc);
                gbc.gridy++;
            }
        }

        // Scroll Pane with fixed size
        JScrollPane scrollPane = new JScrollPane(contentPanel);
        scrollPane.setBounds(50, 80, dialogWidth - 100, dialogHeight - 180); // Tăng padding
        scrollPane.setOpaque(false);
        scrollPane.getViewport().setOpaque(false);
        scrollPane.setBorder(null);
        layeredPane.add(scrollPane, JLayeredPane.PALETTE_LAYER);

        // Close button
        CustomButton closeBtn = new CustomButton(
            ViewConstants.RULE_CLOSE_BTN,
            ViewConstants.RULE_CLOSE_HOVER_BTN,
            ViewConstants.RULE_CLOSE_PRESSED_BTN,
            120, 40
        );
//        closeBtn.setText("Close");
//        closeBtn.setFont(ResourceLoader.loadFont(ViewConstants.FONT_PATH, 16f));
        closeBtn.setBounds((dialogWidth - 120) / 2, dialogHeight - 60, 120, 40);
        closeBtn.addActionListener(e -> dialog.dispose());
        layeredPane.add(closeBtn, JLayeredPane.MODAL_LAYER);

        dialog.setContentPane(layeredPane);
        dialog.setVisible(true);
    }
}