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
 * Lớp "HardBotAtkStrategy" biểu diễn chiến lược tấn công của bot khó
 *
 * Cách hoạt động:
 * - Bot sẽ duyệt toàn bộ bàn cờ, đánh giá các ô chưa bị bắn, chọn ô nào có nhiều ô chưa bị bắn xung quanh nhất (tăng xác suất trúng tàu lớn).
 * - Có thể mở rộng thêm các thuật toán xác suất, hoặc "gian lận" (nếu muốn).
 *
 * Đặc điểm:
 * - Khó đoán, có thể gây khó khăn cho người chơi.
 * - Tối ưu hóa khả năng bắn trúng tàu.
 *
 * @author Nguyen Viet Hoang
 * @version 1.0
 * @since 2025-04-28
 */
public class HardBotAtkStrategy implements IBotAttackStrategy, INotifiableBotStrategy {
	// --- THUỘC TÍNH ---
	private Random random = new Random();
	private Queue<int[]> hitQueue = new LinkedList<>();

	// --- CÁC PHƯƠNG THỨC KHÁC ---
	@Override
    public void notifyHit(int x, int y) {
        hitQueue.offer(new int[] { x, y });
    }

	@Override
    public int[] chooseAttack(Board opponentBoard) {
        int size = opponentBoard.getBoardSize();
        Node[][] nodes = opponentBoard.getBoard();

        // Ưu tiên truy vết như MediumBot
        while (!hitQueue.isEmpty()) {
            int[] lastHit = hitQueue.peek();
            List<int[]> neighbors = getUnhitNeighbors(lastHit[0], lastHit[1], nodes, size);
            if (!neighbors.isEmpty()) {
                // Ưu tiên các ô có nhiều ô chưa bắn xung quanh nhất
                int maxScore = -1;
                List<int[]> best = new ArrayList<>();
                for (int[] n : neighbors) {
                    int score = countUnhitNeighbors(n[0], n[1], nodes, size);
                    if (score > maxScore) {
                        maxScore = score;
                        best.clear();
                        best.add(n);
                    } else if (score == maxScore) {
                        best.add(n);
                    }
                }
                return best.get(random.nextInt(best.size()));
            } else {
                hitQueue.poll();
            }
        }

        // Nếu không truy vết, dùng xác suất như HardBot cũ
        int maxScore = -1;
        List<int[]> bestChoices = new ArrayList<>();
        for (int x = 0; x < size; x++) {
            for (int y = 0; y < size; y++) {
                if (!nodes[x][y].isHit()) {
                    int score = countUnhitNeighbors(x, y, nodes, size);
                    if (score > maxScore) {
                        maxScore = score;
                        bestChoices.clear();
                        bestChoices.add(new int[] { x, y });
                    } else if (score == maxScore) {
                        bestChoices.add(new int[] { x, y });
                    }
                }
            }
        }
        if (bestChoices.isEmpty()) {
            return new int[] { -1, -1 };
        }
        return bestChoices.get(random.nextInt(bestChoices.size()));
    }

    private List<int[]> getUnhitNeighbors(int x, int y, Node[][] nodes, int size) {
        int[] dx = { 0, 0, 1, -1 };
        int[] dy = { 1, -1, 0, 0 };
        List<int[]> neighbors = new ArrayList<>();
        for (int d = 0; d < 4; d++) {
            int nx = x + dx[d];
            int ny = y + dy[d];
            if (nx >= 0 && nx < size && ny >= 0 && ny < size && !nodes[nx][ny].isHit()) {
                neighbors.add(new int[] { nx, ny });
            }
        }
        return neighbors;
    }

    private int countUnhitNeighbors(int x, int y, Node[][] nodes, int size) {
        int[] dx = { 0, 0, 1, -1 };
        int[] dy = { 1, -1, 0, 0 };
        int count = 0;
        for (int d = 0; d < 4; d++) {
            int nx = x + dx[d];
            int ny = y + dy[d];
            if (nx >= 0 && nx < size && ny >= 0 && ny < size && !nodes[nx][ny].isHit()) {
                count++;
            }
        }
        return count;
    }
}
