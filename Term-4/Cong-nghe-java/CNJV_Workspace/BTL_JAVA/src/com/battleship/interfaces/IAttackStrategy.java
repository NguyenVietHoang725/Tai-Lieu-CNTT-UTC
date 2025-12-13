package com.battleship.interfaces;

import java.util.List;

import com.battleship.model.board.Board;
import com.battleship.model.board.Node;

/**
 * Interface biểu diễn chiến lược tấn công trong trò chơi
 *
 * @author Nguyen Viet Hoang
 * @version 1.0
 * @since 2025-04-27
 */

public interface IAttackStrategy {
	/**
	 * Lấy danh sách các ô cần tấn công
	 *
	 * @param x Tọa độ x của ô
	 * @param y Tọa độ y của ô
	 * @param board Bảng trò chơi
	 * @return Danh sách các ô cần tấn công
	 */
	List<Node> getAttackPoints(int x, int y, Board board);
}
