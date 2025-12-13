package com.battleship.view.components.slider;

import javax.swing.*;
import javax.swing.plaf.basic.BasicSliderUI;
import java.awt.*;

public class CustomSliderUI extends BasicSliderUI {
    private final Image trackImage;
    private final Image thumbNormal;
    private final Image thumbHover;
    private final Image thumbPressed;

    private boolean isHovering = false;
    private boolean isPressed = false;

    public CustomSliderUI(JSlider slider, Image trackImage, Image thumbNormal, Image thumbHover, Image thumbPressed) {
        super(slider);
        this.trackImage = trackImage;
        this.thumbNormal = thumbNormal;
        this.thumbHover = thumbHover;
        this.thumbPressed = thumbPressed;

        slider.addMouseListener(new java.awt.event.MouseAdapter() {
            @Override
            public void mousePressed(java.awt.event.MouseEvent e) {
                isPressed = true;
                slider.repaint();
            }

            @Override
            public void mouseReleased(java.awt.event.MouseEvent e) {
                isPressed = false;
                slider.repaint();
            }

            @Override
            public void mouseEntered(java.awt.event.MouseEvent e) {
                isHovering = true;
                slider.repaint();
            }

            @Override
            public void mouseExited(java.awt.event.MouseEvent e) {
                isHovering = false;
                slider.repaint();
            }
        });
    }

    @Override
    public void paintTrack(Graphics g) {
        int trackY = trackRect.y + (trackRect.height - trackImage.getHeight(null)) / 2;
        g.drawImage(trackImage, trackRect.x, trackY, trackRect.width, trackImage.getHeight(null), null);
    }

    @Override
    public void paintThumb(Graphics g) {
        Image img = thumbNormal;
        if (isPressed) {
            img = thumbPressed;
        } else if (isHovering) {
            img = thumbHover;
        }
        g.drawImage(img, thumbRect.x, thumbRect.y, img.getWidth(null), img.getHeight(null), null);
    }
}
