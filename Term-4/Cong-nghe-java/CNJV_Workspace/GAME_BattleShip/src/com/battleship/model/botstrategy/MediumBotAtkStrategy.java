package com.battleship.model.botstrategy;

import java.util.ArrayList;
import java.util.LinkedList;
import java.util.List;
import java.util.Queue;
import java.util.Random;

import com.battleship.interfaces.IBotAttackStrategy;
import com.battleship.interfaces.INotifiableBotStrategy;
import com.battleship.model.board.Board;
import com.battleship.model.board.Node;

/**
 * Lớp "MediumBotAtkStrategy" biểu diễn chiến lược tấn công của bot trung bình
 *
 * Cách hoạt động:
 * - Nếu bot vừa bắn trúng một ô có tàu, nó sẽ ưu tiên bắn tiếp các ô xung quanh ô đó (trái, phải, trên, dưới) trong các lượt tiếp theo.
 * - Nếu không có ô trúng nào để truy vết, bot sẽ bắn ngẫu nhiên như EasyBot.
 *
 * Đặc điểm:
 * - Có "trí tuệ" hơn: biết truy vết tàu khi đã bắn trúng.
 * - Khi bắn trúng, khả năng đánh chìm tàu nhanh hơn.
 * - Khi không bắn trúng, vẫn bắn ngẫu nhiên.
 *
 * @author Nguyen Viet Hoang
 * @version 1.0
 * @since 2025-04-28
 */
public class MediumBotAtkStrategy implements IBotAttackStrategy, INotifiableBotStrategy {
	// --- THUỘC TÍNH ---
	private Random random = new Random();
    private Queue<int[]> hitQueue = new LinkedList<>();

    // --- CÁC PHƯƠNG THỨC KHÁC ---
    /**
     * Gọi phương thức này từ GameLogic khi bot bắn trúng tàu tại (x, y)
     *
     * @param x Tọa độ x của ô
     * @param y Tọa độ y của ô
     */
    @Override
    public void notifyHit(int x, int y) {
        hitQueue.offer(new int[] { x, y });
    }

	/**
	 * Hàm chọn nước đi tiếp theo dựa trên chiến lược hiện tại.
	 *
	 * @param opponentBoard Bàn cờ của đối thủ (người chơi)
	 * @return int[] {x, y} là tọa độ bot sẽ bắn
	 */
    @Override
    public int[] chooseAttack(Board opponentBoard) {
        int size = opponentBoard.getBoardSize(); // Kích thước bàn cờ
        Node[][] nodes = opponentBoard.getBoard(); // Mảng ô trên bàn cờ

        // Nếu có ô trúng gần nhất, ưu tiên bắn các ô xung quanh
        while (!hitQueue.isEmpty()) {
            int[] lastHit = hitQueue.peek(); // Lấy ô trúng gần nhất
            List<int[]> neighbors = getUnhitNeighbors(lastHit[0], lastHit[1], nodes, size); // Lấy các ô xung quanh
            if (!neighbors.isEmpty()) { // Nếu có ô xung quanh
                return neighbors.get(random.nextInt(neighbors.size())); // Trả về ô xung quanh ngẫu nhiên
            } else {
                hitQueue.poll(); // Không còn ô xung quanh, bỏ qua
            }
        }

        // Nếu không có ô trúng gần nhất, bắn ngẫu nhiên
        List<int[]> available = new ArrayList<>(); // Danh sách các ô có thể bắn
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

	/**
	 * Hàm lấy các ô xung quanh ô trúng gần nhất
	 *
	 * @param x Tọa độ x của ô
	 * @param y Tọa độ y của ô
	 * @param nodes Mảng ô trên bàn cờ
	 * @param size Kích thước bàn cờ
	 * @return Danh sách các ô xung quanh
	 */
    private List<int[]> getUnhitNeighbors(int x, int y, Node[][] nodes, int size) {
        int[] dx = { 0, 0, 1, -1 }; // Các ô xung quanh
        int[] dy = { 1, -1, 0, 0 }; // Các ô xung quanh
        List<int[]> neighbors = new ArrayList<>(); // Danh sách các ô xung quanh
        for (int d = 0; d < 4; d++) { // Lặp qua các ô xung quanh
            int nx = x + dx[d]; // Tọa độ x của ô xung quanh
            int ny = y + dy[d]; // Tọa độ y của ô xung quanh
            if (nx >= 0 && nx < size && ny >= 0 && ny < size && !nodes[nx][ny].isHit()) { // Nếu ô xung quanh hợp lệ và chưa bị bắn
                neighbors.add(new int[] { nx, ny }); // Thêm ô xung quanh vào danh sách
            }
        }
        return neighbors;
    }
}