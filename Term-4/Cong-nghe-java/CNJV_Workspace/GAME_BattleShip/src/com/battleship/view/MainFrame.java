package com.battleship.view;

import java.awt.Dimension;

import javax.swing.JFrame;
import javax.swing.WindowConstants;

public class MainFrame extends JFrame {
	private CardPanel cardPanel;

	public MainFrame() {
		setTitle("Battle Ship Game");
		setDefaultCloseOperation(WindowConstants.EXIT_ON_CLOSE);
		setPreferredSize(new Dimension(1280, 720));
		setMinimumSize(new Dimension(960, 540));

		cardPanel = new CardPanel();
		add(cardPanel);

		pack();
		setLocationRelativeTo(null);
	}

	public void switchScreen(String name) {
		cardPanel.showScreen(name);
	}

}
