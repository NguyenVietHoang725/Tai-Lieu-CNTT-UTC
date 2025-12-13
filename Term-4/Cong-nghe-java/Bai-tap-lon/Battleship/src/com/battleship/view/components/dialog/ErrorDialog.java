package com.battleship.view.components.dialog;

import com.battleship.utils.ViewConstants;
import com.battleship.utils.ResourceLoader;
import com.battleship.view.components.buttons.CustomButton;
import com.battleship.view.components.common.ImageBackgroundPanel;

import javax.swing.*;
import java.awt.*;

public class ErrorDialog {
    public static void showDialog(JFrame parentFrame, String errorMessage, Runnable onBack) {
        int dialogWidth = 360;
        int dialogHeight = 240;

        // Dialog setup
        JDialog dialog = new JDialog(parentFrame, true);
        dialog.setUndecorated(true);
        dialog.setSize(dialogWidth, dialogHeight);
        dialog.setLocationRelativeTo(parentFrame);
        dialog.setLayout(null);

        // LayeredPane
        JLayeredPane layeredPane = new JLayeredPane();
        layeredPane.setLayout(null);
        layeredPane.setBounds(0, 0, dialogWidth, dialogHeight);

        // Background
        ImageBackgroundPanel background = new ImageBackgroundPanel(ViewConstants.ERROR_DIALOG_BG);
        background.setBounds(0, 0, dialogWidth, dialogHeight);
        layeredPane.add(background, JLayeredPane.DEFAULT_LAYER);

        // Font
        Font font = ResourceLoader.loadFont(ViewConstants.FONT_PATH, 20f); // Tăng font size lên 28f

        // Error Label
        JLabel messageLabel = new JLabel("<html><div style='text-align: center;'>" + errorMessage + "</div></html>", SwingConstants.CENTER);
        messageLabel.setFont(font);
        messageLabel.setForeground(Color.WHITE);
        messageLabel.setBounds(40, 50, dialogWidth - 80, 60); // Tăng padding trái/phải
        layeredPane.add(messageLabel, JLayeredPane.PALETTE_LAYER);

        // Back Button
        CustomButton backButton = new CustomButton(
            ViewConstants.ERROR_BACK_BTN,
            ViewConstants.ERROR_BACK_HOVER_BTN,
            ViewConstants.ERROR_BACK_PRESSED_BTN,
            160, 44
        );
        backButton.setBounds((dialogWidth - 160) / 2, 130, 160, 44); // Điều chỉnh vị trí y
        backButton.addActionListener(e -> {
            dialog.dispose();
            onBack.run();
        });
        layeredPane.add(backButton, JLayeredPane.MODAL_LAYER);

        // Apply
        dialog.setContentPane(layeredPane);
        dialog.setVisible(true);
    }
}