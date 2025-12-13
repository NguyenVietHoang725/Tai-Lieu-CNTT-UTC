package com.battleship.view.panels.challenge.manage;

import javax.swing.*;

import com.battleship.view.utils.ResourceLoader;
import com.battleship.view.utils.ViewConstants;

import java.awt.*;

import java.util.Arrays;
import java.util.List;

public class ChallengeManagePanel extends JPanel {
	public ChallengeManagePanel() {
		setLayout(new BorderLayout());
		setOpaque(false);
		
		// Load font
        Font font = ResourceLoader.loadFont(ViewConstants.FONT_PATH, 16f);

        // Panel bên trái: tạo/lưu màn
        ChallengeCreatePanel createPanel = new ChallengeCreatePanel(font);
        add(createPanel, BorderLayout.WEST);
		
		// Dummy data
		List<ChallengeHistoryItem> history = Arrays.asList(
				new ChallengeHistoryItem("Level 1", 20, 30, "1:30", "2:00", true),
				new ChallengeHistoryItem("Level 2", 25, 30, "1:50", "2:00", false));
		ChallengeHistoryPanel historyPanel = new ChallengeHistoryPanel(history);

		add(historyPanel, BorderLayout.CENTER);
	}
}
