package com.battleship.view.panels.challenge.manage;

import javax.swing.*;

import java.util.List;
import com.battleship.view.utils.ResourceLoader;
import com.battleship.view.utils.ViewConstants;

import java.awt.*;

public class ChallengeHistoryPanel extends JPanel {
	public ChallengeHistoryPanel(List<ChallengeHistoryItem> history) {
		setLayout(new BoxLayout(this, BoxLayout.Y_AXIS));
		setOpaque(false);

		Font font = ResourceLoader.loadFont(ViewConstants.FONT_PATH, 14f);

		for (ChallengeHistoryItem item : history) {
			add(new ChallengeHistoryRowPanel(item, font));
			add(Box.createRigidArea(new Dimension(0, 10)));
		}
	}
}
