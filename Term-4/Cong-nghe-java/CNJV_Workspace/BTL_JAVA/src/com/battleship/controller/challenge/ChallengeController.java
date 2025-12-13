package com.battleship.controller.challenge;

import com.battleship.enums.AttackType;
import com.battleship.model.logic.ChallengeModeLogic;
import com.battleship.model.board.Node;
import com.battleship.view.panels.challenge.ChallengePlayPanel;
import com.battleship.view.panels.challenge.ChallengeInfoAttackPanel;
import com.battleship.view.panels.challenge.ChallengeBoardPanel;

import javax.swing.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.List;

public class ChallengeController {
    private final ChallengeModeLogic gameLogic;
    private final ChallengePlayPanel playPanel;
    private final ChallengeInfoAttackPanel infoPanel;
    private final ChallengeBoardPanel boardPanel;

    private AttackType selectedAttackType = AttackType.SINGLE;
    private Timer timer;

    public ChallengeController(ChallengeModeLogic gameLogic, ChallengePlayPanel playPanel,
                              ChallengeInfoAttackPanel infoPanel, ChallengeBoardPanel boardPanel) {
        this.gameLogic = gameLogic;
        this.playPanel = playPanel;
        this.infoPanel = infoPanel;
        this.boardPanel = boardPanel;

        initListeners();
        startTimer();
        updateInfoPanel();
    }

    private void initListeners() {
        // Lắng nghe các nút tấn công
        for (AttackType type : AttackType.values()) {
            if (type == AttackType.SINGLE) continue; // SINGLE là mặc định
            int idx = type.ordinal() - 1; // Giả sử thứ tự nút trùng với enum (bạn có thể map lại nếu cần)
            infoPanel.getAttackButton(idx).addActionListener(e -> {
                selectedAttackType = type;
                // Có thể highlight nút được chọn nếu muốn
            });
        }

        // Lắng nghe click trên các ô bàn cờ
        for (int i = 0; i < 10; i++) {
            for (int j = 0; j < 10; j++) {
                int x = i, y = j;
                boardPanel.getButton(x, y).addActionListener(e -> handleAttack(x, y));
            }
        }
    }

    private void handleAttack(int x, int y) {
        if (gameLogic.isGameOver()) return;
        List<Node> attacked = gameLogic.attack(selectedAttackType, x, y);
        // Cập nhật hiệu ứng hit/miss cho các node bị bắn
        for (Node node : attacked) {
            // TODO: Cập nhật icon cho node (hit/miss) trên view
            // Ví dụ: boardPanel.getButton(node.getX(), node.getY()).setIcon(...);
        }
        updateInfoPanel();
        if (gameLogic.isGameOver()) {
            showGameOver();
        }
    }

    private void updateInfoPanel() {
        infoPanel.setShots(gameLogic.getShotsLeft());
        infoPanel.setTime(formatTime(gameLogic.getTimeLeftSeconds()));
        // Cập nhật số đạn đặc biệt nếu muốn
    }

    private void startTimer() {
        timer = new Timer(1000, new ActionListener() {
            public void actionPerformed(ActionEvent e) {
                gameLogic.tickTime();
                updateInfoPanel();
                if (gameLogic.isGameOver()) {
                    timer.stop();
                    showGameOver();
                }
            }
        });
        timer.start();
    }

    private void showGameOver() {
        String message = gameLogic.isPlayerWin() ? "You win!" : "You lose!";
        JOptionPane.showMessageDialog(playPanel, message, "Game Over", JOptionPane.INFORMATION_MESSAGE);
        // Có thể disable toàn bộ bàn cờ hoặc chuyển về menu
    }

    private String formatTime(int seconds) {
        int min = seconds / 60;
        int sec = seconds % 60;
        return String.format("%02d:%02d", min, sec);
    }
}