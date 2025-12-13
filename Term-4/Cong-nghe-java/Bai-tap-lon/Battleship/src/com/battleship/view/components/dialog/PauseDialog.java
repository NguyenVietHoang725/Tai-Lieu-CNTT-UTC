package com.battleship.view.components.dialog;

import com.battleship.utils.ViewConstants;
import com.battleship.utils.ResourceLoader;
import com.battleship.view.components.buttons.CustomButton;
import com.battleship.view.components.common.ImageBackgroundPanel;

import javax.swing.*;
import java.awt.*;

public class PauseDialog {
	public static void showDialog(JFrame parentFrame, Runnable onResume, Runnable onSetting, Runnable onMainMenu) {
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
		ImageBackgroundPanel background = new ImageBackgroundPanel(ViewConstants.PAUSE_DIALOG_BG);
		background.setBounds(0, 0, dialogWidth, dialogHeight);
		layeredPane.add(background, JLayeredPane.DEFAULT_LAYER);

		// Font
		Font font = ResourceLoader.loadFont(ViewConstants.FONT_PATH, 32f);

		// Resume Button
		CustomButton resumeBtn = new CustomButton(ViewConstants.PAUSE_RESUME_BTN, ViewConstants.PAUSE_RESUME_HOVER_BTN,
				ViewConstants.PAUSE_RESUME_PRESSED_BTN, 192, 44);
		resumeBtn.setBounds((dialogWidth - 192) / 2, 70, 192, 44);
		resumeBtn.addActionListener(e -> {
			dialog.dispose();
			onResume.run();
		});
		layeredPane.add(resumeBtn, JLayeredPane.MODAL_LAYER);

		// Setting Button
		CustomButton settingBtn = new CustomButton(ViewConstants.PAUSE_SETTING_BTN,
				ViewConstants.PAUSE_SETTING_HOVER_BTN, ViewConstants.PAUSE_SETTING_PRESSED_BTN, 192, 44);
		settingBtn.setBounds((dialogWidth - 192) / 2, 120, 192, 44);
		settingBtn.addActionListener(e -> {
			dialog.dispose();
			onSetting.run();
		});
		layeredPane.add(settingBtn, JLayeredPane.MODAL_LAYER);

		// Main Menu Button
		CustomButton mainMenuBtn = new CustomButton(ViewConstants.PAUSE_MAINMENU_BTN,
				ViewConstants.PAUSE_MAINMENU_HOVER_BTN, ViewConstants.PAUSE_MAINMENU_PRESSED_BTN, 192, 44);
		mainMenuBtn.setBounds((dialogWidth - 192) / 2, 170, 192, 44);
		mainMenuBtn.addActionListener(e -> {
			dialog.dispose();
			onMainMenu.run();
		});
		layeredPane.add(mainMenuBtn, JLayeredPane.MODAL_LAYER);

		// Apply
		dialog.setContentPane(layeredPane);
		dialog.setVisible(true);
	}
}