package com.battleship.view.components.buttons;

import java.awt.Dimension;
import java.awt.Image;

import javax.swing.ImageIcon;
import javax.swing.JButton;

import com.battleship.utils.ResourceLoader;

public class CustomButton extends JButton {
	public CustomButton(String iconPath, String hoverPath, String pressedPath, int width, int height) {
        super(new ImageIcon(ResourceLoader.loadImage(iconPath)
                .getScaledInstance(width, height, Image.SCALE_SMOOTH)));
        setBorderPainted(false);
        setContentAreaFilled(false);
        setFocusPainted(false);
        setOpaque(false);
        setPreferredSize(new Dimension(width, height));
        setMaximumSize(new Dimension(width, height));
        setMinimumSize(new Dimension(width, height));

        if (hoverPath != null) {
            setRolloverIcon(new ImageIcon(ResourceLoader.loadImage(hoverPath)
                .getScaledInstance(width, height, Image.SCALE_SMOOTH)));
        }
        if (pressedPath != null) {
            setPressedIcon(new ImageIcon(ResourceLoader.loadImage(pressedPath)
                .getScaledInstance(width, height, Image.SCALE_SMOOTH)));
        }
    }
}
