package com.battleship.view.utils;

import java.awt.Font;
import java.awt.Image;
import java.io.InputStream;
import java.net.URL;

import javax.swing.ImageIcon;

public class ResourceLoader {
	public static Image loadImage(String path) {
	    URL url = ResourceLoader.class.getResource(path);
	    if (url == null) {
			throw new RuntimeException("Resource not found: " + path);
		}
	    return new ImageIcon(url).getImage();
	}

	public static ImageIcon loadImageIcon(String path) {
        URL url = ResourceLoader.class.getResource(path);
        System.out.println("DEBUG: Loading GIF: " + path + " => " + url);
        if (url == null) {
            throw new RuntimeException("Resource not found: " + path);
        }
        return new ImageIcon(url);
    }
	
	public static Font loadFont(String path, float size) {
        try {
            InputStream is = ResourceLoader.class.getResourceAsStream(path);
            if (is == null) {
                throw new RuntimeException("Font resource not found: " + path);
            }
            Font font = Font.createFont(Font.TRUETYPE_FONT, is);
            return font.deriveFont(size);
        } catch (Exception e) {
            e.printStackTrace();
            return new Font("Segoe UI", Font.PLAIN, (int) size); // fallback
        }
    }
}
