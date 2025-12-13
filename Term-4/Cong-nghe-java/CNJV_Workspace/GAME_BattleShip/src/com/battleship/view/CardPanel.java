package com.battleship.view;

import java.awt.BorderLayout;
import java.awt.CardLayout;

import javax.swing.JPanel;

import com.battleship.view.panels.challenge.ChallengeScreen;
import com.battleship.view.panels.menu.MainMenuPanel;
import com.battleship.view.utils.ViewConstants;

public class CardPanel extends JPanel {

	private CardLayout cardLayout;
	private JPanel cardsPanel;

	public CardPanel() {
		setLayout(new BorderLayout());

		cardLayout = new CardLayout();
		cardsPanel = new JPanel(cardLayout);

		cardsPanel.add(new MainMenuPanel(), ViewConstants.MAIN_MENU);
		cardsPanel.add(new ChallengeScreen(), ViewConstants.CHALLENGE);

		add(cardsPanel, BorderLayout.CENTER);

		showScreen(ViewConstants.MAIN_MENU);
	}

	public void showScreen(String name) {
		cardLayout.show(cardsPanel, name);
	}

}
