package com.battleship.view.panels.vsbot.play;

import javax.swing.*;
import javax.swing.border.TitledBorder;

import com.battleship.view.components.buttons.CustomToggleButton;
import com.battleship.view.utils.ViewConstants;

import java.awt.*;

public class AtkButtonsPanel extends JPanel {
    private JLabel[] attackLabels;
    private CustomToggleButton[] attackButtons;
    private ButtonGroup attackButtonGroup;

    public AtkButtonsPanel(Font font, JPanel infoPanel) {
        setLayout(new BoxLayout(this, BoxLayout.Y_AXIS));
        setOpaque(false);

        JPanel attackPanel = new JPanel(new GridLayout(1, 4, 16, 16)); // 1 hàng 4 cột
        attackPanel.setOpaque(false);
        attackPanel.setBorder(BorderFactory.createCompoundBorder(
            BorderFactory.createTitledBorder(
                BorderFactory.createLineBorder(Color.WHITE, 1, true),
                "ATTACK",
                TitledBorder.CENTER,
                TitledBorder.TOP,
                font.deriveFont(Font.BOLD, 15f),
                Color.WHITE
            ),
            BorderFactory.createEmptyBorder(30, 10, 30, 10)
        ));

        String[] attackNames = {"Single", "Cross", "Random", "Diamond"};
        String[] btnOnImages = ViewConstants.CHALLENGE_ATK_ON_BUTTON_IMAGES;
        String[] btnHoverImages = ViewConstants.CHALLENGE_ATK_HOVER_BUTTON_IMAGES;
        String[] btnPressedImages = ViewConstants.CHALLENGE_ATK_PRESSED_BUTTON_IMAGES;
        int btnWidth = 160, btnHeight = 88;

        attackLabels = new JLabel[4];
        attackButtons = new CustomToggleButton[4];
        attackButtonGroup = new ButtonGroup();

        for (int i = 0; i < 4; i++) {
            JPanel atkPanel = new JPanel();
            atkPanel.setLayout(new BoxLayout(atkPanel, BoxLayout.Y_AXIS));
            atkPanel.setOpaque(false);

            // Label tên + số lượng
            String labelText = attackNames[i];
            if (i > 0) labelText += ": 0"; // Single không cần số lượng
            JLabel atkLabel = new JLabel(labelText);
            atkLabel.setFont(font.deriveFont(Font.BOLD, 14f));
            atkLabel.setForeground(Color.WHITE);
            atkLabel.setAlignmentX(Component.CENTER_ALIGNMENT);
            attackLabels[i] = atkLabel;

            // Nút toggle
            CustomToggleButton atkBtn = new CustomToggleButton(
                btnOnImages[i], btnHoverImages[i], btnPressedImages[i], btnWidth, btnHeight
            );
            atkBtn.setAlignmentX(Component.CENTER_ALIGNMENT);
            attackButtons[i] = atkBtn;
            attackButtonGroup.add(atkBtn);

            atkPanel.add(atkLabel);
            atkPanel.add(Box.createVerticalStrut(4));
            atkPanel.add(atkBtn);
            attackPanel.add(atkPanel);
        }

        // Mặc định chọn Single
        attackButtons[0].setSelected(true);

        add(Box.createVerticalStrut(20));
        add(infoPanel);
        add(Box.createVerticalStrut(30));
        add(attackPanel);
        add(Box.createVerticalGlue());
    }

    // Getter nếu cần truy cập các button/label từ bên ngoài
    public CustomToggleButton[] getAttackButtons() {
        return attackButtons;
    }
    public JLabel[] getAttackLabels() {
        return attackLabels;
    }
}
