package com.battleship.model.botstrategy;

import java.util.ArrayList;
import java.util.List;
import java.util.Random;

import com.battleship.interfaces.IBotAttackStrategy;
import com.battleship.model.board.Board;
import com.battleship.model.board.Node;

/**
 * Lớp "EasyBotAtkStrategy" biểu diễn chiến lược tấn công của bot dễ dàng
 *
 * Cách hoạt động:
 * - Bot chọn ngẫu nhiên một ô chưa bị bắn trên bàn cờ của đối thủ.
 * - Không quan tâm đến kết quả các lần bắn trước đó.
 *
 * Đặc điểm:
 * - Đơn giản, dễ đoán, không có chiến lược tấn công gì cả.
 * - Người chơi dễ thắng nếu chơi cẩn thận.
 *
 * @author Nguyen Viet Hoang
 * @version 1.0
 * @since 2025-04-28
 */
public class EasyBotAtkStrategy implements IBotAttackStrategy {
	// --- THUỘC TÍNH ---
	private Random random = new Random();

	// --- CÁC PHƯƠNG THỨC KHÁC ---
	/**
	 * Hàm chọn nước đi tiếp theo dựa trên chiến lược hiện tại.
	 *
	 * @param opponentBoard Bàn cờ của đối thủ (người chơi)
	 * @return int[] {x, y} là tọa độ bot sẽ bắn
	 */
	@Override
	public int[] chooseAttack(Board opponentBoard) {
		List<int[]> available = new ArrayList<>(); // Danh sách các ô có thể bắn
		int size = opponentBoard.getBoardSize(); // Kích thước bàn cờ
		Node[][] nodes = opponentBoard.getBoard(); // Mảng ô trên bàn cờ

		// Lặp qua từng ô trên bàn cờ
		for (int x = 0; x < size; x++) {
			for (int y = 0; y < size; y++) {
				if (!nodes[x][y].isHit()) { // Nếu ô chưa bị bắn
					available.add(new int[] { x, y });
				}
			}
		}
		if (available.isEmpty()) { // Nếu không có ô nào có thể bắn
			return new int[] { -1, -1 }; // Trả về tọa độ -1, -1
		}
		return available.get(random.nextInt(available.size())); // Trả về tọa độ ngẫu nhiên
	}

}
