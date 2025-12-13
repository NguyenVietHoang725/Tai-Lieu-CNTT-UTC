package com.battleship.view;

import java.awt.CardLayout;
import java.awt.Font;

import javax.swing.JPanel;

import com.battleship.view.panels.challenge.manage.ChallengeManagePanel;
import com.battleship.view.panels.challenge.play.ChallengePlayPanel;
import com.battleship.view.panels.menu.MainMenuPanel;
import com.battleship.view.panels.vsbot.play.VsBotPlayPanel;
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
    	add(new ChallengeManagePanel(), ViewConstants.CHALLENGE_MANAGE);
    	add(new ChallengePlayPanel(font), ViewConstants.CHALLENGE_PLAY);
//    	add(new VsBotPlayPanel(font), ViewConstants.VSBOT_PLAY);
//        cardsPanel.add(new VsBotScreen(), VSBOT);
//        cardsPanel.add(new RulePanel(), RULE);
//        cardsPanel.add(new PlaceShipScreen(), PLACE_SHIP);
//        cardsPanel.add(new ShowShipScreen(), SHOW_SHIP);

    	showScreen(ViewConstants.MAIN_MENU);
    }

    public void showScreen(String name) {
        cardLayout.show(this, name);
    }
}
