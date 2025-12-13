package com.battleship.view;

import java.awt.CardLayout;
import java.awt.Font;

import javax.swing.JPanel;

import com.battleship.view.panels.challenge.ChallengePlayPanel;
import com.battleship.view.panels.menu.MainMenuPanel;
import com.battleship.view.panels.vsbot.play.VsBotPlayPanel;
//import com.battleship.view.panels.vsbot.play.VsBotPlayPanel;
import com.battleship.view.utils.ResourceLoader;
import com.battleship.view.utils.ViewConstants;

public class CardPanel extends JPanel {
	private CardLayout cardLayout;

	public CardPanel() {
		cardLayout = new CardLayout();
    	setLayout(cardLayout);
    	
    	// Load font một lần, dùng cho các panel cần font
        Font font = ResourceLoader.loadFont(ViewConstants.FONT_PATH, 16f);
    	
    	add(new MainMenuPanel(), ViewConstants.MAIN_MENU);
    	add(new ChallengePlayPanel(font, 500, 780,  50), ViewConstants.CHALLENGE_PLAY);
    	add(new VsBotPlayPanel(font, 50), ViewConstants.VSBOT_PLAY);

    	showScreen(ViewConstants.VSBOT_PLAY);
    }

    public void showScreen(String name) {
        cardLayout.show(this, name);
    }
}
