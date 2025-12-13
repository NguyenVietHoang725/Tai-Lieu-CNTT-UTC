package com.battleship.model.attack;

import java.util.ArrayList;
import java.util.List;

import com.battleship.interfaces.IAttackStrategy;
import com.battleship.model.board.Board;
import com.battleship.model.board.Node;

/**
 * Lớp "DiamondAttackStrategy" biểu diễn chiến lược tấn công hình thoi trong trò chơi
 *
 * @author Nguyen Viet Hoang
 * @version 1.0
 * @since 2025-04-27
 */

public class DiamondAttackStrategy implements IAttackStrategy {
	private static final int[][] DELTAS = {
	        {0,0}, {0,1}, {0,2}, {0,-1}, {0,-2},
	        {1,0}, {2,0}, {-1,0}, {-2,0},
	        {1,1}, {1,-1}, {-1,1}, {-1,-1}
	}; // Các ô cần tấn công

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
		List<Node> result = new ArrayList<>(); // Danh sách các ô cần tấn công
        for (int[] d : DELTAS) {
            int nx = x + d[0], ny = y + d[1]; // Tọa độ của ô cần tấn công
            if (board.isValidCoordinate(nx, ny)) { // Kiểm tra xem ô có hợp lệ hay không
                result.add(board.getNode(nx, ny)); // Thêm ô cần tấn công vào danh sách
            }
        }
        return result; // Trả về danh sách các ô cần tấn công
	}

}
