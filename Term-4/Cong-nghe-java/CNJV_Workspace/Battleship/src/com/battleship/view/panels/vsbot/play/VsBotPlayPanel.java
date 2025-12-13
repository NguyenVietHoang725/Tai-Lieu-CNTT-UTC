//package com.battleship.view.panels.vsbot.play;
//
//import java.awt.BorderLayout;
//import java.awt.Component;
//import java.awt.Dimension;
//import java.awt.Font;
//
//import javax.swing.Box;
//import javax.swing.BoxLayout;
//import javax.swing.JButton;
//import javax.swing.JLabel;
//import javax.swing.JPanel;
//import javax.swing.SwingConstants;
//
//import javax.swing.*;
//
//import com.battleship.view.components.board.GameBoardPanel;
//
//import java.awt.*;
//
//public class VsBotPlayPanel extends JPanel {
//	public VsBotPlayPanel(Font font) {
//        setLayout(new BorderLayout());
//        setOpaque(false);
//
//        // Panel chứa 2 bảng chơi
//        JPanel boardsPanel = new JPanel();
//        boardsPanel.setLayout(new BoxLayout(boardsPanel, BoxLayout.X_AXIS));
//        boardsPanel.setOpaque(false);
//
//        // Bảng người chơi
//        JPanel playerPanel = new JPanel(new BorderLayout());
//        playerPanel.setOpaque(false);
//        JLabel playerLabel = new JLabel("Player", SwingConstants.CENTER);
//        playerLabel.setFont(font.deriveFont(Font.BOLD, 16f));
//        playerPanel.add(playerLabel, BorderLayout.NORTH);
//        playerPanel.add(new GameBoardPanel("Bảng người chơi", 10), BorderLayout.CENTER);
//
//        // Bảng challenge
//        JPanel challengePanel = new JPanel(new BorderLayout());
//        challengePanel.setOpaque(false);
//        JLabel challengeLabel = new JLabel("Challenge", SwingConstants.CENTER);
//        challengeLabel.setFont(font.deriveFont(Font.BOLD, 16f));
//        challengePanel.add(challengeLabel, BorderLayout.NORTH);
//        challengePanel.add(new GameBoardPanel("Bảng challenge", 10), BorderLayout.CENTER);
//
//        boardsPanel.add(Box.createHorizontalGlue());
//        boardsPanel.add(playerPanel);
//        boardsPanel.add(Box.createRigidArea(new Dimension(40, 0))); // khoảng cách giữa 2 bảng
//        boardsPanel.add(challengePanel);
//        boardsPanel.add(Box.createHorizontalGlue());
//
//        add(boardsPanel, BorderLayout.CENTER);
//
//        // Panel chứa 4 button tấn công
//        JPanel attackPanel = new JPanel();
//        attackPanel.setLayout(new BoxLayout(attackPanel, BoxLayout.X_AXIS));
//        attackPanel.setOpaque(false);
//
//        String[] attackNames = {"Normal Attack", "Missile", "Bomb", "Special"};
//        for (String name : attackNames) {
//            JButton btn = new JButton(name);
//            btn.setFont(font.deriveFont(15f));
//            btn.setAlignmentY(Component.CENTER_ALIGNMENT);
//            btn.setFocusable(false);
//            btn.setPreferredSize(new Dimension(120, 36));
//            btn.setMaximumSize(new Dimension(120, 36));
//            btn.setMinimumSize(new Dimension(120, 36));
//            btn.setToolTipText("Attack type: " + name);
//            attackPanel.add(btn);
//            attackPanel.add(Box.createRigidArea(new Dimension(20, 0)));
//        }
//
//        attackPanel.add(Box.createHorizontalGlue());
//        add(attackPanel, BorderLayout.SOUTH);
//    }
//}
