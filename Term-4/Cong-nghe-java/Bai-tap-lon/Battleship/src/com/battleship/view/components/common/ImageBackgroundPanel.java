package com.battleship.view.components.common;

import javax.swing.*;

import com.battleship.utils.ResourceLoader;

import java.awt.*;

public class ImageBackgroundPanel extends JPanel {
	private final ImageIcon imageIcon;
    private final JLabel imageLabel;

    /**
     * @param imageResourcePath Đường dẫn tới file ảnh trong resource, ví dụ "/images/background.png"
     */
    public ImageBackgroundPanel(String imageResourcePath) {
        setOpaque(false);
        setLayout(new BorderLayout());

        // Load ảnh tĩnh từ resource
        Image image = ResourceLoader.loadImage(imageResourcePath);
        imageIcon = new ImageIcon(image);

        // JLabel để hiển thị ảnh, override paintComponent để scale ảnh cho vừa panel
        imageLabel = new JLabel(imageIcon) {
            @Override
            protected void paintComponent(Graphics g) {
                super.paintComponent(g);
                if (imageIcon != null && imageIcon.getImage() != null) {
                    Image img = imageIcon.getImage();
                    g.drawImage(img, 0, 0, getWidth(), getHeight(), this);
                }
            }
        };
        imageLabel.setOpaque(false);
        add(imageLabel, BorderLayout.CENTER);
    }

    @Override
    public void doLayout() {
        super.doLayout();
        // Đảm bảo imageLabel luôn fill hết panel
        if (imageLabel != null) {
            imageLabel.setSize(getSize());
        }
    }
}
