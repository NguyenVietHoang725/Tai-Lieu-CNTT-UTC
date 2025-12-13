package com.battleship.view.components.dialog;

import com.battleship.utils.ResourceLoader;
import com.battleship.utils.ViewConstants;
import com.battleship.view.components.buttons.CustomButton;
import com.battleship.view.components.common.ImageBackgroundPanel;

import javax.swing.*;
import java.awt.*;

public class GameOverDialog {

    public static void showDialog(JFrame parentFrame, String message, Runnable onReplay, Runnable onMenu) {
        int dialogWidth = ViewConstants.dialogType1Width;
        int dialogHeight = ViewConstants.dialogType1Height;

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
        ImageBackgroundPanel background = new ImageBackgroundPanel(ViewConstants.GAMEOVER_DIALOG_BG);
        background.setBounds(0, 0, dialogWidth, dialogHeight);
        layeredPane.add(background, JLayeredPane.DEFAULT_LAYER);

        // Font
        Font font = ResourceLoader.loadFont(ViewConstants.FONT_PATH, 36f); // tăng kích cỡ font

        // Label
        JLabel titleLabel = new JLabel(message, SwingConstants.CENTER);
        titleLabel.setFont(font);
        titleLabel.setForeground(Color.WHITE);
        titleLabel.setBounds(0, 70, dialogWidth, 40); // đẩy xuống thấp hơn
        layeredPane.add(titleLabel, JLayeredPane.PALETTE_LAYER);

        // Replay button
        CustomButton replayBtn = new CustomButton(
            ViewConstants.GAMEOVER_REPLAY_BTN,
            ViewConstants.GAMEOVER_REPLAY_HOVER_BTN,
            ViewConstants.GAMEOVER_REPLAY_PRESSED_BTN,
            192, 48
        );
        replayBtn.setBounds((dialogWidth - 192) / 2, 135, 192, 48);
        replayBtn.addActionListener(e -> {
            dialog.dispose();
            onReplay.run();
        });
        layeredPane.add(replayBtn, JLayeredPane.MODAL_LAYER);

        // Menu button
        CustomButton menuBtn = new CustomButton(
            ViewConstants.GAMEOVER_MENU_BTN,
            ViewConstants.GAMEOVER_MENU_HOVER_BTN,
            ViewConstants.GAMEOVER_MENU_PRESSED_BTN,
            192, 48
        );
        menuBtn.setBounds((dialogWidth - 192) / 2, 190, 192, 48);
        menuBtn.addActionListener(e -> {
            dialog.dispose();
            onMenu.run();
        });
        layeredPane.add(menuBtn, JLayeredPane.MODAL_LAYER);

        // Apply
        dialog.setContentPane(layeredPane);
        dialog.setVisible(true);
    }
}
