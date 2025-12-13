package com.battleship.view.panels.vsbot.play;

import java.awt.*;
import javax.swing.*;

public class VsBotPlayPanel extends JPanel {
    private MyBoardPanel myBoardPanel;
    private BotBoardPanel botBoardPanel;
    private AtkButtonsPanel atkButtonsPanel;

    public VsBotPlayPanel(Font font, int cellSize) {
        setLayout(new BorderLayout());
        setOpaque(false);

        // Panel chứa 2 bảng
        JPanel boardsPanel = new JPanel();
        boardsPanel.setLayout(new GridLayout(1, 2, 32, 0)); // 2 bảng, cách nhau 32px
        boardsPanel.setOpaque(false);

        myBoardPanel = new MyBoardPanel(font, cellSize);
        botBoardPanel = new BotBoardPanel(font, cellSize);

        boardsPanel.add(myBoardPanel);
        boardsPanel.add(botBoardPanel);

        // Panel info (nếu có, hoặc có thể là panel rỗng)
        JPanel infoPanel = new JPanel();
        infoPanel.setOpaque(false);
        infoPanel.setLayout(new BoxLayout(infoPanel, BoxLayout.Y_AXIS)); // nếu muốn giống Box

        // Panel nút tấn công
        atkButtonsPanel = new AtkButtonsPanel(font, infoPanel);

        // Thêm các thành phần vào VsBotPlayPanel
        add(boardsPanel, BorderLayout.CENTER);
        add(atkButtonsPanel, BorderLayout.SOUTH);
    }

    // Getter nếu cần
    public MyBoardPanel getMyBoardPanel() {
        return myBoardPanel;
    }
    public BotBoardPanel getBotBoardPanel() {
        return botBoardPanel;
    }
    public AtkButtonsPanel getAtkButtonsPanel() {
        return atkButtonsPanel;
    }
}