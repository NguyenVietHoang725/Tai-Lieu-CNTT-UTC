package com.battleship.model.logic;

import com.battleship.enums.AttackType;
import com.battleship.model.attack.AttackInventory;
import com.battleship.model.attack.AttackLogic;
import com.battleship.model.board.Board;
import com.battleship.model.loader.ChallengeBoardLoader;
import com.battleship.model.player.Player;
import com.battleship.model.ship.Ship;
import com.battleship.model.board.Node;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

/**
 * Logic cho chế độ Challenge
 */
public class ChallengeModeLogic extends GameLogic {
	// --- THUỘC TÍNH ---
	private final Board board;
	private final AttackInventory attackInventory;
	private final AttackLogic attackLogic;
	private final int maxShots;
	private final int maxTimeSeconds;

	private int shotsUsed = 0;
	private int timeLeftSeconds;
	private boolean gameOver = false;
	private boolean playerWin = false;

	// Danh sách tàu vừa bị chìm ở lượt bắn này
	private List<Ship> sunkShipsLastTurn = new ArrayList<>();

	// --- HÀM KHỞI TẠO ---
	public ChallengeModeLogic(Player player, ChallengeBoardLoader loader) throws IOException {
		super(player);
		this.board = player.getBoard();
		this.maxShots = loader.getMaxShots();
		this.maxTimeSeconds = loader.getMaxTimeSeconds();

		// Khởi tạo kho đạn với số lượng từ loader
		this.attackInventory = new AttackInventory();
		// Cập nhật số lượng đạn đặc biệt theo file
		attackInventory.addAttack(AttackType.CROSS,
				loader.getCrossCount() - attackInventory.getAttackCount(AttackType.CROSS));
		attackInventory.addAttack(AttackType.RANDOM,
				loader.getRandomCount() - attackInventory.getAttackCount(AttackType.RANDOM));
		attackInventory.addAttack(AttackType.DIAMOND,
				loader.getDiamondCount() - attackInventory.getAttackCount(AttackType.DIAMOND));

		this.attackLogic = new AttackLogic(board, attackInventory);
		this.timeLeftSeconds = maxTimeSeconds;
	}

	// --- PHƯƠNG THỨC GAME ---
	@Override
	public void startGame() {
		shotsUsed = 0;
		timeLeftSeconds = maxTimeSeconds;
		gameOver = false;
		playerWin = false;
		// Có thể reset lại trạng thái board và attackInventory nếu cần
	}

	@Override
	public boolean isGameOver() {
		return gameOver;
	}

	@Override
	public boolean isPlayerWin() {
		return playerWin;
	}

	/**
	 * Thực hiện tấn công tại vị trí (x, y) với loại đạn type. Trả về danh sách các
	 * node bị bắn trúng (để View cập nhật hiệu ứng).
	 */
	public List<Node> attack(AttackType type, int x, int y) {
		if (gameOver)
			return List.of();
		if (shotsUsed >= maxShots) {
			gameOver = true;
			return List.of();
		}
		if (!attackLogic.canAttack(type, x, y)) {
			return List.of();
		}

		List<Node> attacked = attackLogic.attack(type, x, y);

		shotsUsed++;
		checkGameStatus();
		return attacked;
	}

	// Trả về danh sách tàu vừa bị chìm ở lượt bắn này
	public List<Ship> getSunkShipsLastTurn() {
		return sunkShipsLastTurn;
	}

	private void checkGameStatus() {
		if (board.allShipsSunk()) {
			gameOver = true;
			playerWin = true;
		} else if (shotsUsed >= maxShots || timeLeftSeconds <= 0) {
			gameOver = true;
			playerWin = false;
		}
	}

	public void tickTime() {
		if (timeLeftSeconds > 0) {
			timeLeftSeconds--;
			checkGameStatus();
		}
	}

	// --- GETTER ---
	public int getShotsUsed() {
		return shotsUsed;
	}

	public int getShotsLeft() {
		return maxShots - shotsUsed;
	}

	public int getAttackCount(AttackType type) {
		return attackInventory.getAttackCount(type);
	}

	public int getTimeLeftSeconds() {
		return timeLeftSeconds;
	}

	public Board getBoard() {
		return board;
	}

	public boolean hasAttack(AttackType selectedAttackType) {
		return attackInventory.hasAttack(selectedAttackType);
	}
	
	public int setShipsRemaining() {
	    return board.getRemainingShips();
	}

}