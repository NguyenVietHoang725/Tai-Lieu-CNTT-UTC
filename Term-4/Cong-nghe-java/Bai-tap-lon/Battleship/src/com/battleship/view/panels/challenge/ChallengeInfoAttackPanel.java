package com.battleship.view.panels.challenge;

import javax.swing.*;
import javax.swing.border.TitledBorder;
import java.awt.*;

import com.battleship.utils.ViewConstants;
import com.battleship.view.components.buttons.CustomToggleButton;

public class ChallengeInfoAttackPanel extends JPanel {
    private JLabel timeLabel;
    private JLabel shotsLabel;
    private JLabel shipsLabel;
    private JLabel[] attackLabels;
    private CustomToggleButton[] attackButtons;
    private ButtonGroup attackButtonGroup;

    public ChallengeInfoAttackPanel(Font font, int preferredWidth) {
        setLayout(new BoxLayout(this, BoxLayout.Y_AXIS));
        setOpaque(false);
        setBorder(BorderFactory.createCompoundBorder(
        	    BorderFactory.createTitledBorder(
        	        BorderFactory.createLineBorder(Color.WHITE, 3, true),
        	        "INFO & ATTACK",
        	        TitledBorder.CENTER,
        	        TitledBorder.TOP,
        	        font.deriveFont(Font.BOLD, 18f),
        	        Color.WHITE
        	    ),
        	    BorderFactory.createEmptyBorder(12, 12, 12, 12)
        	));

        setPreferredSize(new Dimension(preferredWidth, 0));

        JPanel infoPanel = new JPanel();
        infoPanel.setLayout(new BoxLayout(infoPanel, BoxLayout.Y_AXIS));
        infoPanel.setOpaque(false);
        infoPanel.setBorder(BorderFactory.createCompoundBorder(
            BorderFactory.createTitledBorder(
                BorderFactory.createLineBorder(Color.WHITE, 1, true),
                "INFO",
                TitledBorder.CENTER,
                TitledBorder.TOP,
                font.deriveFont(Font.BOLD, 15f),
                Color.WHITE
            ),
            BorderFactory.createEmptyBorder(20, 10, 20, 10)
        ));


        timeLabel = new JLabel("Time Left: 02:00", SwingConstants.CENTER);
        timeLabel.setFont(font.deriveFont(Font.BOLD, 18f));
        timeLabel.setForeground(Color.WHITE);
        timeLabel.setAlignmentX(Component.CENTER_ALIGNMENT);


        shotsLabel = new JLabel("Shots Left: 30", SwingConstants.CENTER);
        shotsLabel.setFont(font.deriveFont(Font.BOLD, 18f));
        shotsLabel.setForeground(Color.WHITE);
        shotsLabel.setAlignmentX(Component.CENTER_ALIGNMENT);


        // Thêm label cho ships remaining
        shipsLabel = new JLabel("Ships Remaining: 5", SwingConstants.CENTER);
        shipsLabel.setFont(font.deriveFont(Font.BOLD, 18f));
        shipsLabel.setForeground(Color.WHITE);
        shipsLabel.setAlignmentX(Component.CENTER_ALIGNMENT);


        infoPanel.add(Box.createVerticalStrut(18));
        infoPanel.add(timeLabel);
        infoPanel.add(Box.createVerticalStrut(24));
        infoPanel.add(shotsLabel);
        infoPanel.add(Box.createVerticalStrut(24));
        infoPanel.add(shipsLabel);
        infoPanel.add(Box.createVerticalStrut(18));

        // Attack panel
        JPanel attackPanel = new JPanel(new GridLayout(2, 2, 16, 16));
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
        String[] btnOnImages = ViewConstants.CHALLENGE_ATK_BUTTON_IMAGES;
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
        add(Box.createVerticalStrut(24)); 
    }

    public void setTime(String time) {
        timeLabel.setText("Time Left: " + time);
    }

    public void setShots(int shots) {
        shotsLabel.setText("Shots Left: " + shots);
    }

    public void setAttackCount(int cross, int random, int diamond) {
        attackLabels[1].setText("Cross: " + cross);
        attackLabels[2].setText("Random: " + random);
        attackLabels[3].setText("Diamond: " + diamond);
    }
    
    public void setShipsRemaining(int ships) {
        shipsLabel.setText("Ships Remaining: " + ships);
    }

    public CustomToggleButton getAttackButton(int idx) {
        return attackButtons[idx];
    }
}