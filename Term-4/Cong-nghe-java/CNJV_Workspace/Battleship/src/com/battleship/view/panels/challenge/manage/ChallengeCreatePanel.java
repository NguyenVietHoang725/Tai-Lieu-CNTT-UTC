package com.battleship.view.panels.challenge.manage;

import javax.swing.*;
import java.awt.*;

public class ChallengeCreatePanel extends JPanel {
	public ChallengeCreatePanel(Font font) {
        setOpaque(false);
        setLayout(new BoxLayout(this, BoxLayout.Y_AXIS));
        setBorder(BorderFactory.createEmptyBorder(30, 30, 30, 30));

        // Tiêu đề
        JLabel title = new JLabel("Quản lý màn Challenge");
        title.setFont(font.deriveFont(Font.BOLD, 20f));
        title.setAlignmentX(Component.LEFT_ALIGNMENT);
        add(title);

        add(Box.createRigidArea(new Dimension(0, 20)));

        // Button New
        JButton newButton = new JButton("Tạo màn mới");
        newButton.setFont(font.deriveFont(16f));
        newButton.setAlignmentX(Component.LEFT_ALIGNMENT);
        newButton.setToolTipText("Chọn file màn chơi mới");
        add(newButton);

        add(Box.createRigidArea(new Dimension(0, 20)));

        // 3 slot button
        for (int i = 1; i <= 3; i++) {
            JButton slotBtn = new JButton("Slot " + i);
            slotBtn.setFont(font.deriveFont(16f));
            slotBtn.setAlignmentX(Component.LEFT_ALIGNMENT);
            slotBtn.setToolTipText("Lưu màn vào slot " + i);
            add(slotBtn);
            add(Box.createRigidArea(new Dimension(0, 10)));
        }

        add(Box.createRigidArea(new Dimension(0, 20)));

        // Thông báo trạng thái
        JTextArea notification = new JTextArea("Thông báo trạng thái...");
        notification.setFont(font.deriveFont(16f));
        notification.setEditable(false);
        notification.setLineWrap(true);
        notification.setWrapStyleWord(true);
        notification.setBackground(new Color(240, 240, 255));
        notification.setAlignmentX(Component.LEFT_ALIGNMENT);
        notification.setMaximumSize(new Dimension(220, 60));
        add(notification);

        add(Box.createVerticalGlue());
    }
}
