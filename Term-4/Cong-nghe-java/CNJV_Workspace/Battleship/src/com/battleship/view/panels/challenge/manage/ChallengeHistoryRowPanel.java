package com.battleship.view.panels.challenge.manage;

import javax.swing.*;

import com.battleship.view.components.buttons.CustomButton;
import com.battleship.view.utils.ViewConstants;

import java.awt.*;

public class ChallengeHistoryRowPanel extends JPanel {
	public ChallengeHistoryRowPanel(ChallengeHistoryItem item, Font font) {
        setLayout(new BoxLayout(this, BoxLayout.X_AXIS));
        setOpaque(false);

        // Chọn PNG cho button lớn theo trạng thái thắng/thua
        String[] btnImages = item.isWin ? ViewConstants.CHALLENGE_WIN_BUTTON_IMAGES : ViewConstants.CHALLENGE_LOSE_BUTTON_IMAGES;
        CustomButton bigBtn = new CustomButton(
            btnImages[0], btnImages[1], null, 192, 32
        );

        // Overlay text lên button lớn
        JPanel overlayPanel = new JPanel();
        overlayPanel.setLayout(new OverlayLayout(overlayPanel));
        overlayPanel.setOpaque(false);
        overlayPanel.setPreferredSize(new Dimension(192, 32));
        overlayPanel.setMaximumSize(new Dimension(192, 32));
        overlayPanel.add(bigBtn);

        String displayText = String.format("%s - %d/%d - %s/%s",
            item.levelName, item.shotsUsed, item.shotsMax, item.timeUsed, item.timeMax);

        JLabel textLabel = new JLabel(displayText);
        textLabel.setFont(font);
        textLabel.setForeground(Color.BLACK);
        textLabel.setAlignmentX(0.5f);
        textLabel.setAlignmentY(0.5f);
        overlayPanel.add(textLabel);

        add(overlayPanel);
        add(Box.createRigidArea(new Dimension(8, 0)));

        // Button nhỏ icon trạng thái
        String[] iconBtnImages = item.isWin ? ViewConstants.CHALLENGE_WIN_BUTTON_IMAGES : ViewConstants.CHALLENGE_LOSE_BUTTON_IMAGES;
        CustomButton statusBtn = new CustomButton(
            iconBtnImages[0], iconBtnImages[1], null, 32, 32
        );
        add(statusBtn);
    }
}
