package com.battleship.view.utils;

import java.awt.Image;

import javax.swing.ImageIcon;

public class ResourceLoader {
	public static ImageIcon loadIcon(String path) {
        java.net.URL url = ResourceLoader.class.getResource(path);
        return url != null ? new ImageIcon(url) : null;
    }
    public static ImageIcon loadIcon(String path, int width, int height) {
        ImageIcon icon = loadIcon(path);
        if (icon != null) {
            Image img = icon.getImage().getScaledInstance(width, height, Image.SCALE_SMOOTH);
            return new ImageIcon(img);
        }
        return null;
    }
}
