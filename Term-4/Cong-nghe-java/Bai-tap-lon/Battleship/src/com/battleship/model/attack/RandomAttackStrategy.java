package com.battleship.model.attack;

import java.util.ArrayList;
import java.util.HashSet;
import java.util.List;
import java.util.Random;
import java.util.Set;

import com.battleship.interfaces.IAttackStrategy;
import com.battleship.model.board.Board;
import com.battleship.model.board.Node;

/**
 * Lớp "RandomAttackStrategy" biểu diễn chiến lược tấn công ngẫu nhiên trong trò chơi
 *
 * @author Nguyen Viet Hoang
 * @version 1.0
 * @since 2025-04-27
 */

public class RandomAttackStrategy implements IAttackStrategy {
	private static final int EXTRA_RANDOM_SHOTS = 3; // Số lượng tấn công ngẫu nhiên

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
		Set<String> visited = new HashSet<>(); // Tập hợp các ô đã tấn công
		int size = board.getBoardSize(); // Kích thước bảng trò chơi

		if (board.isValidCoordinate(x, y)) {
			result.add(board.getNode(x, y)); // Thêm ô cần tấn công vào danh sách
			visited.add(x + "," + y); // Thêm ô đã tấn công vào tập hợp
		}

		Random rand = new Random();
		int count = 0;
		while (count < EXTRA_RANDOM_SHOTS) {
			int rx = rand.nextInt(size); // Tạo tọa độ x ngẫu nhiên
			int ry = rand.nextInt(size); // Tạo tọa độ y ngẫu nhiên
			String key = rx + "," + ry; // Tạo khóa cho ô đã tấn công
			if (!visited.contains(key)) { // Kiểm tra xem ô đã tấn công hay chưa
				result.add(board.getNode(rx, ry)); // Thêm ô cần tấn công vào danh sách
				visited.add(key); // Thêm ô đã tấn công vào tập hợp
				count++; // Tăng số lượng tấn công
			}
		}
		return result; // Trả về danh sách các ô cần tấn công
	}

}
