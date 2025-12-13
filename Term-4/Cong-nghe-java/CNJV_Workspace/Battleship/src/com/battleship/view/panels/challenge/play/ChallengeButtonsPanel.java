package com.battleship.view.panels.challenge.play;

//import của phần button là ảnh
import com.battleship.view.components.buttons.CustomButton;
import com.battleship.view.utils.ViewConstants;
import java.awt.*;
import javax.swing.*;


//code button là ảnh 
public class ChallengeButtonsPanel extends JPanel {
    public ChallengeButtonsPanel(Font font) {
        setOpaque(false);
        setLayout(new BoxLayout(this, BoxLayout.Y_AXIS));
        setBorder(BorderFactory.createEmptyBorder(30, 30, 30, 30));
        setPreferredSize(new Dimension(260, 0));

        // Thông tin lượt bắn và thời gian
        JPanel infoPanel = new JPanel();
        infoPanel.setOpaque(false);
        infoPanel.setLayout(new BoxLayout(infoPanel, BoxLayout.Y_AXIS));

        JLabel shotsLabel = new JLabel("Max Shots: 30", SwingConstants.CENTER);
        shotsLabel.setFont(font.deriveFont(Font.BOLD, 15f));
        shotsLabel.setAlignmentX(Component.CENTER_ALIGNMENT);

        JLabel timeLabel = new JLabel("Time Left: 02:00", SwingConstants.CENTER);
        timeLabel.setFont(font.deriveFont(Font.BOLD, 15f));
        timeLabel.setAlignmentX(Component.CENTER_ALIGNMENT);

        infoPanel.add(shotsLabel);
        infoPanel.add(Box.createRigidArea(new Dimension(0, 10)));
        infoPanel.add(timeLabel);

        add(infoPanel);
        add(Box.createRigidArea(new Dimension(0, 20)));

        // Thông tin các nút (text, ảnh on, hover, off, width, height)
//        String[] btnTexts = {"Save", "Back", "Normal Attack", "Missile", "Bomb", "Special"};
        String[] btnOnImages = ViewConstants.CHALLENGE_ON_BUTTON_IMAGES;
        String[] btnHoverImages = ViewConstants.CHALLENGE_HOVER_BUTTON_IMAGES;
        String[] btnOffImages = ViewConstants.CHALLENGE_OFF_BUTTON_IMAGES;
        int[] btnWidths = {192, 192, 192, 192};
        int[] btnHeights = {96, 96, 96, 96};

        // Panel chứa các nút
        JPanel btnPanel = new JPanel();
        btnPanel.setOpaque(false);
        btnPanel.setLayout(new GridLayout(3, 2, 10, 10)); // 3 hàng 2 cột

        for (int i = 0; i < btnOnImages.length; i++) {
            CustomButton btn = new CustomButton(
                btnOnImages[i], btnHoverImages[i], btnOffImages[i], btnWidths[i], btnHeights[i]
            );
           //btn.setText(btnTexts[i]);
            btn.setFont(font.deriveFont(15f));
            btnPanel.add(btn);
        }
        add(btnPanel);
        add(Box.createVerticalGlue());
    }
}
