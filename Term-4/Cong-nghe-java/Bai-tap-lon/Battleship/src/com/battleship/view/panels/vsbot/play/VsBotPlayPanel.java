package com.battleship.view.panels.vsbot.play;

import javax.swing.*;
import java.awt.*;

import com.battleship.utils.ViewConstants;
import com.battleship.view.components.common.ImageBackgroundPanel;

public class VsBotPlayPanel extends JPanel {
    private VsBotInfoPanel infoPanel;
    private VsBotPlayerBoardPanel playerBoardPanel;
    private VsBotBotBoardPanel botBoardPanel;
    private String difficulty;


    public VsBotPlayPanel(Font font, int cellSize, String difficulty) {
        this.difficulty = difficulty;
        setLayout(new BorderLayout());


        // Background
        String bgPath = switch (difficulty.toLowerCase()) {
            case "medium" -> ViewConstants.VSBOT_MEDIUM_BG_IMG;
            case "hard" -> ViewConstants.VSBOT_HARD_BG_IMG;
            default -> ViewConstants.VSBOT_EASY_BG_IMG;
        };
        ImageBackgroundPanel bgPanel = new ImageBackgroundPanel(bgPath);
        setOpaque(false);
        this.add(bgPanel, BorderLayout.CENTER);


        // Layered content
        JPanel contentPanel = new JPanel(new BorderLayout());
        contentPanel.setOpaque(false);


        // Panel chứa hai bảng
        JPanel boardsPanel = new JPanel(new GridLayout(1, 2, 32, 0));
        boardsPanel.setOpaque(false);
        
        // Khởi tạo các panel
        infoPanel = new VsBotInfoPanel(font);
        playerBoardPanel = new VsBotPlayerBoardPanel(font, cellSize);
        botBoardPanel = new VsBotBotBoardPanel(font, cellSize, infoPanel);


        // Thêm các panel vào layout
        boardsPanel.add(playerBoardPanel);
        boardsPanel.add(botBoardPanel);
        boardsPanel.setBorder(BorderFactory.createEmptyBorder(20, 20, 20, 20));


        // Thêm boardsPanel vào contentPanel
        contentPanel.add(boardsPanel, BorderLayout.CENTER);
        contentPanel.add(infoPanel, BorderLayout.SOUTH);
        contentPanel.setBorder(BorderFactory.createEmptyBorder(20, 20, 20, 20));


        // Dùng JLayeredPane để overlay content lên background
        JLayeredPane layeredPane = new JLayeredPane();
        layeredPane.setLayout(null);


        bgPanel.setBounds(0, 0, 1280, 720);
        contentPanel.setBounds(0, 0, 1280, 720);


        layeredPane.add(bgPanel, JLayeredPane.DEFAULT_LAYER);
        layeredPane.add(contentPanel, JLayeredPane.PALETTE_LAYER);


        this.add(layeredPane, BorderLayout.CENTER);


        // Đảm bảo resize đúng
        this.addComponentListener(new java.awt.event.ComponentAdapter() {
            public void componentResized(java.awt.event.ComponentEvent evt) {
                Dimension size = getSize();
                bgPanel.setBounds(0, 0, size.width, size.height);
                contentPanel.setBounds(0, 0, size.width, size.height);
                layeredPane.setBounds(0, 0, size.width, size.height);
            }
        });
    }


    public VsBotInfoPanel getInfoPanel() { 
        return infoPanel; 
    }
    
    public VsBotPlayerBoardPanel getPlayerBoardPanel() { 
        return playerBoardPanel; 
    }
    
    public VsBotBotBoardPanel getBotBoardPanel() { 
        return botBoardPanel; 
    }
}



