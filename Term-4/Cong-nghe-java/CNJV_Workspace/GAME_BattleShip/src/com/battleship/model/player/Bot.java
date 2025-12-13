package com.battleship.model.player;

import com.battleship.enums.AttackType;
import com.battleship.interfaces.IBotAttackStrategy;
import com.battleship.model.attack.AttackInventory;
import com.battleship.model.board.Board;

/**
 * Lớp "Bot" biểu diễn bot trong trò chơi
 *
 * @author Nguyen Viet Hoang
 * @version 1.0
 * @since 2025-04-28
 */
public class Bot extends Player {
    // --- THUỘC TÍNH ---
    private IBotAttackStrategy attackStrategy;

    // --- HÀM KHỞI TẠO ---
    public Bot(String name, Board board, AttackInventory atkInv, IBotAttackStrategy attackStrategy) {
        super(name, board, atkInv);
        this.attackStrategy = attackStrategy;
    }

    // --- GETTER & SETTER ---
    public void setAttackStrategy(IBotAttackStrategy attackStrategy) {
        this.attackStrategy = attackStrategy;
    }

    public IBotAttackStrategy getAttackStrategy() {
        return attackStrategy;
    }

    // --- CÁC PHƯƠNG THỨC KHÁC ---
    /**
     * Hàm chọn nước đi tiếp theo dựa trên chiến lược hiện tại.
     *
     * @param opponentBoard Bàn cờ của đối thủ (người chơi)
     * @return int[] {x, y} là tọa độ bot sẽ bắn
     */
    public int[] chooseAttack(Board opponentBoard) {
        if (attackStrategy == null) {
            throw new IllegalStateException("Bot attack strategy is not set!");
        }
        return attackStrategy.chooseAttack(opponentBoard);
    }

    /**
     * Hàm kiểm tra xem bot có thể sử dụng kiểu tấn công hay không.
     *
     * @param type Kiểu tấn công
     * @return true nếu bot có thể sử dụng kiểu tấn công, false nếu không
     */
    @Override
    public boolean canUseAttackType(AttackType type) {
        return atkInv.hasAttack(type);
    }

    /**
     * Hàm kiểm tra xem bot có thể tấn công hay không.
     *
     * @param x Tọa độ x của ô
     * @param y Tọa độ y của ô
     * @param type Kiểu tấn công
     * @return true nếu bot có thể tấn công, false nếu không
     */
    @Override
    public boolean canAttack(int x, int y, AttackType type) {
        // Nên kiểm tra ở logic tổng thể, ở đây chỉ kiểm tra lượt tấn công
        return canUseAttackType(type);
    }
}