package com.battleship.view.components.common;

import java.awt.BorderLayout;
import java.awt.Graphics;

import javax.swing.ImageIcon;
import javax.swing.JLabel;
import javax.swing.JPanel;

import com.battleship.view.utils.ResourceLoader;

public class AnimatedBackgroundPanel extends JPanel {
	private final ImageIcon gifIcon;
    private final JLabel gifLabel;

    /**
     * @param gifResourcePath Đường dẫn tới file GIF trong resource, ví dụ "/images/menu_bg.gif"
     */
    public AnimatedBackgroundPanel(String gifResourcePath) {
        setOpaque(false);
        setLayout(new BorderLayout());

        // Load GIF động từ resource
        gifIcon = ResourceLoader.loadGif(gifResourcePath);

        // JLabel để giữ reference tới ImageIcon (giúp Swing tự động update frame)
        gifLabel = new JLabel(gifIcon) {
            @Override
            protected void paintComponent(Graphics g) {
                super.paintComponent(g);
                if (gifIcon != null && gifIcon.getImage() != null) {
                    g.drawImage(gifIcon.getImage(), 0, 0, getWidth(), getHeight(), this);
                }
            }
        };
        gifLabel.setOpaque(false);
        add(gifLabel, BorderLayout.CENTER);
    }

    @Override
    public void doLayout() {
        super.doLayout();
        // Đảm bảo gifLabel luôn fill hết panel
        if (gifLabel != null) {
            gifLabel.setSize(getSize());
        }
    }
}
