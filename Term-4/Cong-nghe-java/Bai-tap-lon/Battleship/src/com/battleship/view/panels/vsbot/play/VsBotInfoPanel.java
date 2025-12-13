package com.battleship.view.panels.vsbot.play;


import javax.swing.*;
import javax.swing.border.TitledBorder;
import java.awt.*;


public class VsBotInfoPanel extends JPanel {
    private JLabel turnLabel;
    private JLabel statusLabel;
    private JLabel lastAttackLabel;
    private JLabel remainingShipsLabel;


    public VsBotInfoPanel(Font font) {
        setLayout(new BoxLayout(this, BoxLayout.X_AXIS));
        setOpaque(false);


        // Turn Panel
        JPanel turnPanel = createInfoPanel("TURN", font);
        turnLabel = new JLabel("Your Turn");
        turnLabel.setFont(font.deriveFont(Font.BOLD, 18f));
        turnLabel.setForeground(Color.WHITE);
        turnLabel.setAlignmentX(Component.CENTER_ALIGNMENT);
        turnPanel.add(turnLabel);


        // Status Panel
        JPanel statusPanel = createInfoPanel("STATUS", font);
        statusPanel.setPreferredSize(new Dimension(400, 58));
        statusPanel.setMaximumSize(new Dimension(400, 58));
        statusLabel = new JLabel("Playing");
        statusLabel.setFont(font.deriveFont(Font.BOLD, 18f));
        statusLabel.setForeground(Color.WHITE);
        statusLabel.setAlignmentX(Component.CENTER_ALIGNMENT);
        statusPanel.add(statusLabel);


        // Last Attack Panel
        JPanel lastAttackPanel = createInfoPanel("LAST ATTACK", font);
        lastAttackLabel = new JLabel("None");
        lastAttackLabel.setFont(font.deriveFont(Font.BOLD, 16f));
        lastAttackLabel.setForeground(Color.WHITE);
        lastAttackLabel.setAlignmentX(Component.CENTER_ALIGNMENT);
        lastAttackPanel.add(lastAttackLabel);


        // Remaining Ships Panel
        JPanel remainingShipsPanel = createInfoPanel("SHIPS", font);
        remainingShipsLabel = new JLabel("5");
        remainingShipsLabel.setFont(font.deriveFont(Font.BOLD, 16f));
        remainingShipsLabel.setForeground(Color.WHITE);
        remainingShipsLabel.setAlignmentX(Component.CENTER_ALIGNMENT);
        remainingShipsPanel.add(remainingShipsLabel);


        // Add all panels with proper spacing
        add(Box.createHorizontalStrut(24));
        add(turnPanel);
        add(Box.createHorizontalStrut(12));
        add(statusPanel);
        add(Box.createHorizontalStrut(12));
        add(lastAttackPanel);
        add(Box.createHorizontalStrut(12));
        add(remainingShipsPanel);
        add(Box.createHorizontalStrut(24));
    }


    private JPanel createInfoPanel(String title, Font font) {
        JPanel panel = new JPanel();
        panel.setLayout(new BoxLayout(panel, BoxLayout.Y_AXIS));
        panel.setOpaque(false);
        panel.setBorder(BorderFactory.createTitledBorder(
            BorderFactory.createLineBorder(Color.WHITE, 1, true),
            title,
            TitledBorder.CENTER,
            TitledBorder.TOP,
            font.deriveFont(Font.BOLD, 15f),
            Color.WHITE
        ));
        panel.add(Box.createVerticalStrut(10));
        return panel;
    }


    // Getters
    public JLabel getTurnLabel() { return turnLabel; }
    public JLabel getStatusLabel() { return statusLabel; }
    public JLabel getLastAttackLabel() { return lastAttackLabel; }
    public JLabel getRemainingShipsLabel() { return remainingShipsLabel; }


    // Helper methods to update information
    public void updateLastAttack(int row, int col) {
        char rowLetter = (char)('A' + row);
        lastAttackLabel.setText(rowLetter + String.valueOf(col + 1));
    }


    public void updateRemainingShips(int count) {
        remainingShipsLabel.setText(String.valueOf(count));
    }


    public void updateTurn(boolean isPlayerTurn) {
        turnLabel.setText(isPlayerTurn ? "Your Turn" : "Bot's Turn");
    }


    public void updateStatus(String status) {
        statusLabel.setText(status);
    }
}



