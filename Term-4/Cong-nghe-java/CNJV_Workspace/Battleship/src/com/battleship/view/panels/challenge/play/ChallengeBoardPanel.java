package com.battleship.view.panels.challenge.play;

import com.battleship.view.components.board.GameBoardPanel;
import com.battleship.view.utils.ResourceLoader;
import com.battleship.view.utils.ViewConstants;
import java.awt.*;
import javax.swing.*;

public class ChallengeBoardPanel extends JPanel {
    private final GameBoardPanel boardPanel;

    public ChallengeBoardPanel() {
        setOpaque(false);
        setLayout(new BorderLayout());

        // Load icon cho các trạng thái từ resource
        ImageIcon normalIcon = ResourceLoader.loadGif(ViewConstants.CHALLENGE_CELL_NORMAL_IMG);
        ImageIcon missIcon = ResourceLoader.loadGif(ViewConstants.CHALLENGE_CELL_MISS_IMG);
        ImageIcon hoverIcon = ResourceLoader.loadGif(ViewConstants.CHALLENGE_CELL_HOVER_IMG);
        ImageIcon hitIcon = ResourceLoader.loadGif(ViewConstants.CHALLENGE_CELL_HIT_IMG);

        boardPanel = new GameBoardPanel("Board", 10, normalIcon, missIcon, hoverIcon, hitIcon);
        boardPanel.setPreferredSize(new Dimension(400, 400));
        add(boardPanel, BorderLayout.CENTER);
    }

    public GameBoardPanel getBoardPanel() {
        return boardPanel;
    }
}
