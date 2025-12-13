package com.battleship.interfaces;

import com.battleship.model.board.Board;

/**
 * Interface biểu diễn chiến lược tấn công của bot
 *
 * @author Nguyen Viet Hoang
 * @version 1.0
 * @since 2025-04-27
 */

public interface IBotAttackStrategy {
	/**
	 * Chọn ô tấn công
	 *
	 * @param opponentBoard Bảng trò chơi của đối thủ
	 * @return Mảng chứa tọa độ ô tấn công
	 */
	int[] chooseAttack(Board opponentBoard);
}
