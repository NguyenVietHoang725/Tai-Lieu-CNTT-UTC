package com.battleship.view.components.dialog;


import com.battleship.controller.setting.SoundManager;
import com.battleship.model.setting.SoundSettings;
import com.battleship.view.components.buttons.CustomButton;
import com.battleship.view.components.common.ImageBackgroundPanel;
import com.battleship.view.components.slider.CustomSliderUI;
import com.battleship.utils.ResourceLoader;
import com.battleship.utils.ViewConstants;


import javax.swing.*;
import java.awt.*;


public class SettingsDialog {
    public static void showDialog(JFrame parentFrame) {
        int dialogWidth = ViewConstants.dialogType1Width;
        int dialogHeight = ViewConstants.dialogType1Height;
        
        // Dialog setup
        JDialog dialog = new JDialog(parentFrame, "Settings", true);
        dialog.setUndecorated(true);
        dialog.setSize(dialogWidth, dialogHeight);
        dialog.setLocationRelativeTo(parentFrame);
        dialog.setLayout(null);


        // LayeredPane
        JLayeredPane layeredPane = new JLayeredPane();
        layeredPane.setLayout(null);
        layeredPane.setBounds(0, 0, dialogWidth, dialogHeight);


        // Background
        ImageBackgroundPanel bgPanel = new ImageBackgroundPanel(ViewConstants.SETTING_DIALOG_BG);
        bgPanel.setBounds(0, 0, dialogWidth, dialogHeight);
        layeredPane.add(bgPanel, JLayeredPane.DEFAULT_LAYER);


        // Font - tăng kích thước font lên 2 lần
        Font font = ResourceLoader.loadFont(ViewConstants.FONT_PATH, 24f); // Tăng từ 12f lên 24f


        // Get current settings
        SoundSettings settings = SoundManager.getSettings();


        // Calculate component positions - điều chỉnh khoảng cách
        int startX = (dialogWidth - 480) / 2; // Tăng từ 240 lên 480
        int startY = (dialogHeight - 270) / 2; // Tăng từ 135 lên 270


        // BGM Section
        JLabel bgmLabel = new JLabel("BGM:");
        bgmLabel.setFont(font);
        bgmLabel.setForeground(Color.WHITE);
        bgmLabel.setBounds(startX + 32, startY + 75, 80, 40); // Tăng kích thước và khoảng cách


        JCheckBox bgmCheck = new JCheckBox();
        bgmCheck.setSelected(settings.isMusicEnabled());
        bgmCheck.setOpaque(false);
        bgmCheck.setBounds(startX + 120, startY + 80, 32, 32); // Tăng kích thước checkbox
        bgmCheck.setIcon(new ImageIcon(ResourceLoader.loadImage(ViewConstants.CHECKBOX_OFF)));
        bgmCheck.setSelectedIcon(new ImageIcon(ResourceLoader.loadImage(ViewConstants.CHECKBOX_ON)));


        JSlider bgmSlider = new JSlider(0, 100, (int)(settings.getMusicVolume() * 100));
        bgmSlider.setBounds(startX + 180, startY + 80, 260, 32); // Tăng kích thước slider
        bgmSlider.setOpaque(false);
        bgmSlider.setUI(new CustomSliderUI(
            bgmSlider,
            ResourceLoader.loadImage(ViewConstants.SLIDER_TRACK),
            ResourceLoader.loadImage(ViewConstants.SLIDER_THUMB_NORMAL),
            ResourceLoader.loadImage(ViewConstants.SLIDER_THUMB_HOVER),
            ResourceLoader.loadImage(ViewConstants.SLIDER_THUMB_PRESSED)
        ));


        // SFX Section
        JLabel sfxLabel = new JLabel("SFX:");
        sfxLabel.setFont(font);
        sfxLabel.setForeground(Color.WHITE);
        sfxLabel.setBounds(startX + 32, startY + 125, 80, 40); // Tăng khoảng cách giữa các phần


        JCheckBox sfxCheck = new JCheckBox();
        sfxCheck.setSelected(settings.isSfxEnabled());
        sfxCheck.setOpaque(false);
        sfxCheck.setBounds(startX + 120, startY + 130, 32, 32); // Tăng kích thước checkbox
        sfxCheck.setIcon(new ImageIcon(ResourceLoader.loadImage(ViewConstants.CHECKBOX_OFF)));
        sfxCheck.setSelectedIcon(new ImageIcon(ResourceLoader.loadImage(ViewConstants.CHECKBOX_ON)));


        JSlider sfxSlider = new JSlider(0, 100, (int)(settings.getSfxVolume() * 100));
        sfxSlider.setBounds(startX + 180, startY + 130, 260, 32); // Tăng kích thước slider
        sfxSlider.setOpaque(false);
        sfxSlider.setUI(new CustomSliderUI(
            sfxSlider,
            ResourceLoader.loadImage(ViewConstants.SLIDER_TRACK),
            ResourceLoader.loadImage(ViewConstants.SLIDER_THUMB_NORMAL),
            ResourceLoader.loadImage(ViewConstants.SLIDER_THUMB_HOVER),
            ResourceLoader.loadImage(ViewConstants.SLIDER_THUMB_PRESSED)
        ));


        // Buttons Section
        CustomButton applyBtn = new CustomButton(
            ViewConstants.BTN_APPLY_NORMAL,
            ViewConstants.BTN_APPLY_HOVER,
            ViewConstants.BTN_APPLY_PRESSED,
            144, 48 // Tăng kích thước button từ 72x24 lên 144x48
        );
        applyBtn.setBounds(startX + 64, startY + 190, 144, 48);
        applyBtn.addActionListener(e -> {
            boolean musicEnabled = bgmCheck.isSelected();
            boolean sfxEnabled = sfxCheck.isSelected();
            float musicVolume = bgmSlider.getValue() / 100f;
            float sfxVolume = sfxSlider.getValue() / 100f;


            settings.setMusicEnabled(musicEnabled);
            settings.setMusicVolume(musicVolume);
            settings.setSfxEnabled(sfxEnabled);
            settings.setSfxVolume(sfxVolume);


            if (!musicEnabled) {
                SoundManager.stopBGM();
            } else {
                if (SoundManager.isBgmPlaying()) {
                    SoundManager.updateBgmVolume();
                } else {
                    SoundManager.playBGM(SoundManager.getCurrentBgmPath());
                }
            }


            SoundManager.updateSfxVolume();
        });


        CustomButton cancelBtn = new CustomButton(
            ViewConstants.BTN_CANCEL_NORMAL,
            ViewConstants.BTN_CANCEL_HOVER,
            ViewConstants.BTN_CANCEL_PRESSED,
            144, 48 // Tăng kích thước button từ 72x24 lên 144x48
        );
        cancelBtn.setBounds(startX + 256, startY + 190, 144, 48);
        cancelBtn.addActionListener(e -> dialog.dispose());


        // Add all components to layered pane
        layeredPane.add(bgmLabel, JLayeredPane.PALETTE_LAYER);
        layeredPane.add(bgmCheck, JLayeredPane.PALETTE_LAYER);
        layeredPane.add(bgmSlider, JLayeredPane.PALETTE_LAYER);
        layeredPane.add(sfxLabel, JLayeredPane.PALETTE_LAYER);
        layeredPane.add(sfxCheck, JLayeredPane.PALETTE_LAYER);
        layeredPane.add(sfxSlider, JLayeredPane.PALETTE_LAYER);
        layeredPane.add(applyBtn, JLayeredPane.MODAL_LAYER);
        layeredPane.add(cancelBtn, JLayeredPane.MODAL_LAYER);


        // Apply
        dialog.setContentPane(layeredPane);
        dialog.setVisible(true);
    }
}



