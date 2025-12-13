package com.battleship.model.logic;

import java.util.ArrayList;
import java.util.List;

import com.battleship.enums.AttackType;
import com.battleship.model.attack.AttackInventory;
import com.battleship.model.attack.AttackLogic;
import com.battleship.model.board.Node;
import com.battleship.model.player.Player;

/**
 * Lớp "ChallengeModeLogic" biểu diễn logic của chế độ chơi chống lại
 *
 * @author Nguyen Viet Hoang
 * @version 1.0
 * @since 2025-04-28
 */
public class ChallengeModeLogic extends GameLogic {
    // --- THUỘC TÍNH ---
    private final int maxShots; // Số lượt bắn tối đa
    private final int maxTime; // Thời gian tối đa
    private int shotsUsed; // Số lượt bắn đã dùng
    private int timeUsed; // Thời gian đã dùng
    private final AttackLogic attackLogic; // Logic tấn công

    // --- HÀM KHỞI TẠO ---
    public ChallengeModeLogic(Player player, int maxShots, int maxTime, AttackInventory attackInventory) {
        super(player);
        this.maxShots = maxShots;
        this.maxTime = maxTime;
        this.shotsUsed = 0;
        this.timeUsed = 0;
        this.attackLogic = new AttackLogic(player.getBoard(), attackInventory);
    }

    // --- CÁC PHƯƠNG THỨC KHÁC ---
    /**
     * Bắt đầu game
     */
    @Override
    public void startGame() {
        shotsUsed = 0;
        timeUsed = 0;
        // Có thể khởi tạo lại AttackInventory nếu muốn reset
    }

    /**
     * Thực hiện tấn công, trả về danh sách các node bị bắn trúng.
     * Tăng số lượt bắn đã dùng.
     *
     * @param type Kiểu tấn công
     * @param x Tọa độ x
     * @param y Tọa độ y
     * @return Danh sách các node bị bắn trúng
     */
    public List<Node> playerAttack(AttackType type, int x, int y) {
        if (isGameOver() || !player.canAttack(x, y, type)) {
			return new ArrayList<>();
		}
        shotsUsed++;
        return attackLogic.attack(type, x, y);
    }

    /**
     * Cập nhật thời gian đã dùng (tính bằng giây).
     * Gọi phương thức này từ controller/timer mỗi giây hoặc mỗi lần cập nhật.
     *
     * @param seconds Thời gian đã dùng
     */
    public void updateTimeUsed(int seconds) {
        this.timeUsed = seconds;
    }

    /**
     * Kiểm tra game đã kết thúc chưa
     *
     * @return true nếu game đã kết thúc, false nếu ngược lại
     */
    @Override
    public boolean isGameOver() {
        // Hết lượt bắn, hết thời gian, hoặc đã thắng
        return shotsUsed >= maxShots || timeUsed >= maxTime || isPlayerWin();
    }

    /**
     * Kiểm tra người chơi đã thắng chưa
     *
     * @return true nếu người chơi đã thắng, false nếu ngược lại
     */
    @Override
    public boolean isPlayerWin() {
        // Kiểm tra tất cả tàu trên board đã bị bắn chìm chưa
        return player.getBoard().allShipsSunk();
    }

    /**
     * Lấy số lượt bắn đã dùng
     *
     * @return Số lượt bắn đã dùng
     */
    public int getShotsUsed() {
        return shotsUsed;
    }

    /**
     * Lấy số lượt bắn còn lại
     *
     * @return Số lượt bắn còn lại
     */
    public int getShotsLeft() {
        return Math.max(0, maxShots - shotsUsed);
    }

    /**
     * Lấy thời gian đã dùng
     *
     * @return Thời gian đã dùng
     */
    public int getTimeUsed() {
        return timeUsed;
    }

    /**
     * Lấy thời gian còn lại
     *
     * @return Thời gian còn lại
     */
    public int getTimeLeft() {
        return Math.max(0, maxTime - timeUsed);
    }

    /**
     * Lấy số lượt bắn tối đa
     *
     * @return Số lượt bắn tối đa
     */
    public int getMaxShots() {
        return maxShots;
    }

    /**
     * Lấy thời gian tối đa
     *
     * @return Thời gian tối đa
     */
    public int getMaxTime() {
        return maxTime;
    }
}