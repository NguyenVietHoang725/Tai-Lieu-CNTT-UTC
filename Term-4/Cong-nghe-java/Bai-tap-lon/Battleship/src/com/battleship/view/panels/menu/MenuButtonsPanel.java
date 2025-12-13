package com.battleship.view.panels.menu;

import java.awt.Component;
import java.awt.Dimension;
import java.awt.Image;

import javax.swing.BorderFactory;
import javax.swing.Box;
import javax.swing.BoxLayout;
import javax.swing.ImageIcon;
import javax.swing.JLabel;
import javax.swing.JPanel;

import com.battleship.utils.ResourceLoader;
import com.battleship.utils.ViewConstants;
import com.battleship.view.components.buttons.CustomButton;

public class MenuButtonsPanel extends JPanel {
	public MenuButtonsPanel() {
        setOpaque(false);
        setLayout(new BoxLayout(this, BoxLayout.Y_AXIS));
        setBorder(BorderFactory.createEmptyBorder(40, 60, 40, 40));

        // Logo nhỏ phía trên các button (128x56)
        ImageIcon logoIcon = new ImageIcon(
            ResourceLoader.loadImage(ViewConstants.LOGO_PATH)
                .getScaledInstance(256, 112, Image.SCALE_SMOOTH)
        );
        JLabel logoLabel = new JLabel(logoIcon);
        logoLabel.setAlignmentX(Component.LEFT_ALIGNMENT);
        add(logoLabel);

        add(Box.createRigidArea(new Dimension(0, 30)));

     // Đường dẫn ảnh button, hover, pressed
        String[] btnOnImages = ViewConstants.MENU_BUTTON_IMAGES;
        String[] btnHoverImages = ViewConstants.MENU_HOVER_BUTTON_IMAGES;
        String[] btnOffImages = ViewConstants.MENU_PRESSED_BUTTON_IMAGES;

        // Kích thước từng button
        int[] btnWidths = {256, 256, 192, 192, 192};
        int[] btnHeights = {64, 64, 48, 48, 48};

        for (int i = 0; i < btnOnImages.length; i++) {
            CustomButton btn = new CustomButton(btnOnImages[i], btnHoverImages[i], btnOffImages[i], btnWidths[i], btnHeights[i]);
            btn.setAlignmentX(Component.LEFT_ALIGNMENT);
            add(btn);
            add(Box.createRigidArea(new Dimension(0, 18)));
        }

        add(Box.createVerticalGlue());
    }
}
