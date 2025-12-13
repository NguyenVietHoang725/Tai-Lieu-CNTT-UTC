package com.battleship.controller.vsbot;

import com.battleship.model.player.Player;
import com.battleship.utils.AppConstants;
import com.battleship.model.player.Bot;
import com.battleship.model.board.Board;
import com.battleship.model.board.Node;
import com.battleship.model.loader.BotBoardLoader;
import com.battleship.view.components.dialog.GameOverDialog;
import com.battleship.view.components.dialog.PauseDialog;
import com.battleship.view.components.dialog.SettingsDialog;
import com.battleship.view.panels.vsbot.play.VsBotPlayPanel;
import com.battleship.enums.CellState;
import com.battleship.controller.AppController;
import com.battleship.controller.setting.SoundManager;

import javax.swing.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.MouseListener;

public class VsBotController {
	private Player player;
	private Bot bot;
	private VsBotPlayPanel playPanel;
	private AppController appController;
	private boolean isPlayerTurn = true;
	private boolean gameEnded = false;

	public VsBotController(Player player, Bot bot, VsBotPlayPanel playPanel, AppController appController) {
		this.player = player;
		this.bot = bot;
		this.playPanel = playPanel;
		this.appController = appController;
		setupPauseKey();

		initGame();
	}

	private void initGame() {
		// Cập nhật bảng người chơi với tàu đã đặt
		playPanel.getPlayerBoardPanel().updatePlayerBoard(player.getBoard());

		// Load bảng cho bot - Sửa lại cách gọi loadBoard
		BotBoardLoader loader = new BotBoardLoader();
		String[] files = AppConstants.VSBOT_FILE_PATHS;
		String file = files[new java.util.Random().nextInt(files.length)];
		try {
			String filePath = getClass().getResource(file).getPath();
			Board botBoard = loader.loadBoard(filePath);
			bot.setBoard(botBoard); // Set board mới cho bot
		} catch (Exception e) {
			e.printStackTrace();
		}

		updateTurnLabel();
		updateStatusLabel("Playing");

		// Gắn sự kiện click cho bảng bot
		for (int i = 0; i < 10; i++) {
			for (int j = 0; j < 10; j++) {
				int x = i, y = j;
				playPanel.getBotBoardPanel().getButton(x, y).addActionListener(e -> {
					System.out.println("[DEBUG] Clicked button: x(row)=" + x + ", y(col)=" + y);
					if (isPlayerTurn && !gameEnded) {
						playerAttack(x, y);
					}
				});
			}
		}
	}

	private void playerAttack(int x, int y) {
		System.out.println("[DEBUG] playerAttack: x(row)=" + x + ", y(col)=" + y);
		Node node = bot.getBoard().getNode(x, y);

		// Kiểm tra nếu ô đã bị tấn công
		if (node.isHit()) {
			JOptionPane.showMessageDialog(playPanel, "This cell has already been attacked!", "Warning",
					JOptionPane.WARNING_MESSAGE);
			return;
		}

		// Phát SFX cho tấn công
		SoundManager.playSFX(AppConstants.SFX_SINGLE_ATK);

		// Đánh dấu ô đã bị tấn công
		node.setHit(true);

		// Cập nhật trạng thái ô trên bảng
		playPanel.getBotBoardPanel().setCellState(x, y, node.isHasShip() ? CellState.HIT : CellState.MISS);

		// Cập nhật thông tin tấn công gần nhất
		playPanel.getInfoPanel().updateLastAttack(x, y);

		// Cập nhật số tàu còn lại của bot
		playPanel.getInfoPanel().updateRemainingShips(bot.getBoard().getRemainingShips());

		// Xóa listener để không hover/click lại và không đổi lại icon
		JButton btn = playPanel.getBotBoardPanel().getButton(x, y);
		for (MouseListener ml : btn.getMouseListeners()) {
			btn.removeMouseListener(ml);
		}
		for (ActionListener al : btn.getActionListeners()) {
			btn.removeActionListener(al);
		}

		// Xử lý kết quả tấn công
		if (node.isHasShip()) {
			// Nếu trúng tàu
			if (bot.getBoard().isShipSunkAt(x, y)) {
				// Nếu đánh chìm tàu
				playPanel.getInfoPanel().updateStatus("You sunk a ship! Bot's turn.");

				// Kiểm tra thắng
				if (bot.getBoard().allShipsSunk()) {
					gameEnded = true;
					playPanel.getInfoPanel().updateStatus("You win!");
					showGameOverDialog("Congratulations! You win!");
					return;
				}

				// Chuyển lượt cho bot
				isPlayerTurn = false;
				playPanel.getInfoPanel().updateTurn(false);
				botTurn();
			} else {
				// Nếu chỉ trúng tàu nhưng chưa chìm
				playPanel.getInfoPanel().updateStatus("Hit! Shoot again!");
			}
		} else {
			// Nếu trượt
			playPanel.getInfoPanel().updateStatus("Miss! Bot's turn.");
			isPlayerTurn = false;
			playPanel.getInfoPanel().updateTurn(false);
			botTurn();
		}
	}

	private void botTurn() {
		Timer timer = new Timer(700, e -> {
			int[] move = bot.chooseAttack(player.getBoard());
			int x = move[0], y = move[1];

			Node node = player.getBoard().getNode(x, y);
			node.setHit(true);

			// Phát SFX cho tấn công
			SoundManager.playSFX(AppConstants.SFX_SINGLE_ATK);

			// Cập nhật thông tin tấn công gần nhất
			playPanel.getInfoPanel().updateLastAttack(x, y);

			if (node.isHasShip()) {
				playPanel.getPlayerBoardPanel().setCellState(x, y, CellState.HIT);
				if (player.getBoard().isShipSunkAt(x, y)) {
					playPanel.getInfoPanel().updateStatus("Bot sunk your ship! Your turn.");
					if (player.getBoard().allShipsSunk()) {
						gameEnded = true;
						playPanel.getInfoPanel().updateStatus("Bot wins!");
						showGameOverDialog("You lose! Bot wins!");
						return;
					}
					isPlayerTurn = true;
					playPanel.getInfoPanel().updateTurn(true);
				} else {
					playPanel.getInfoPanel().updateStatus("Bot hit! Bot shoots again!");
					botTurn();
				}
			} else {
				playPanel.getPlayerBoardPanel().setCellState(x, y, CellState.MISS);
				playPanel.getInfoPanel().updateStatus("Bot missed! Your turn.");
				isPlayerTurn = true;
				playPanel.getInfoPanel().updateTurn(true);
			}
		});
		timer.setRepeats(false);
		timer.start();
	}

	private void updateTurnLabel() {
		if (isPlayerTurn) {
			playPanel.getInfoPanel().getTurnLabel().setText("Your Turn");
		} else {
			playPanel.getInfoPanel().getTurnLabel().setText("Bot's Turn");
		}
	}

	private void updateStatusLabel(String status) {
		playPanel.getInfoPanel().getStatusLabel().setText("Status: " + status);
	}

	private void showGameOverDialog(String message) {
		GameOverDialog.showDialog((JFrame) SwingUtilities.getWindowAncestor(playPanel), message,
				() -> appController.replayVsBotMode(), () -> appController.showMenu());
	}

	private void setupPauseKey() {
		playPanel.setFocusable(true);
		playPanel.requestFocusInWindow();
		playPanel.getInputMap(JComponent.WHEN_IN_FOCUSED_WINDOW).put(KeyStroke.getKeyStroke("ESCAPE"), "showPause");
		playPanel.getActionMap().put("showPause", new AbstractAction() {
			@Override
			public void actionPerformed(ActionEvent e) {
				showPauseDialog();
			}
		});
	}

	private void showPauseDialog() {
		PauseDialog.showDialog((JFrame) SwingUtilities.getWindowAncestor(playPanel),
				// Resume
				() -> playPanel.requestFocusInWindow(),
				// Setting
				() -> {
					JFrame parent = (JFrame) SwingUtilities.getWindowAncestor(playPanel);
					SettingsDialog.showDialog(parent);
					showPauseDialog();
				},
				// MainMenu
				() -> appController.showMenu());
	}

}