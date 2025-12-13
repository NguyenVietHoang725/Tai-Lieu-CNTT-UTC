package com.battleship.model.attack;

import java.util.ArrayList;
import java.util.List;

import com.battleship.interfaces.IAttackStrategy;
import com.battleship.model.board.Board;
import com.battleship.model.board.Node;

/**
 * Lớp "SingleAttackStrategy" biểu diễn chiến lược tấn công đơn trong trò chơi
 *
 * @author Nguyen Viet Hoang
 * @version 1.0
 * @since 2025-04-27
 */

public class SingleAttackStrategy implements IAttackStrategy {

	/**
	 * Hàm lấy danh sách các ô cần tấn công
	 *
	 * @param x Tọa độ x của ô
	 * @param y Tọa độ y của ô
	 * @param board Bảng trò chơi
	 * @return Danh sách các ô cần tấn công
	 */
	@Override
	public List<Node> getAttackPoints(int x, int y, Board board) {
		List<Node> result = new ArrayList<>();
		if (board.isValidCoordinate(x, y)) {
			result.add(board.getNode(x, y));
		}
		return result;
	}

}
