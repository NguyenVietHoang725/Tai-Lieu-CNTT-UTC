package com.battleship.model.attack;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.battleship.enums.AttackType;
import com.battleship.interfaces.IAttackStrategy;
import com.battleship.model.board.Board;
import com.battleship.model.board.Node;

/**
 * Lớp "AttackLogic" biểu diễn logic tấn công trong trò chơi, sử dụng các chiến lược tấn công khác nhau
 *
 * @author Nguyen Viet Hoang
 * @version 1.0
 * @since 2025-04-27
 */

public class AttackLogic {
    // --- THUỘC TÍNH ---
	private final Board board; // Bảng trò chơi
	private final AttackInventory atkInv; // Kho tấn công
	private final Map<AttackType, IAttackStrategy> strategies; // Các chiến lược tấn công

	// --- HÀM KHỞI TẠO ---
	/**
	 * Hàm khởi tạo với 2 tham số:
	 *
	 * @param board Bảng trò chơi
	 * @param atkInv Kho tấn công
	 */
	public AttackLogic(Board board, AttackInventory atkInv) {
		this.board = board;
		this.atkInv = atkInv;
		this.strategies = new HashMap<>();
		// Khởi tạo các chiến lược
        strategies.put(AttackType.SINGLE, new SingleAttackStrategy());
        strategies.put(AttackType.CROSS, new CrossAttackStrategy());
        strategies.put(AttackType.RANDOM, new RandomAttackStrategy());
        strategies.put(AttackType.DIAMOND, new DiamondAttackStrategy());
	}

	/**
     * Kiểm tra hợp lệ trước khi tấn công.
     * @param type Loại đạn
     * @param x Tọa độ x
     * @param y Tọa độ y
     * @return true nếu hợp lệ, false nếu không
     */
    public boolean canAttack(AttackType type, int x, int y) {
        Node center = board.getNode(x, y);
        if (center.isHit() || !atkInv.hasAttack(type) || !strategies.containsKey(type)) {
			return false;
		}
        return true;
    }

    /**
     * Thực hiện tấn công (giả định đã kiểm tra hợp lệ trước đó).
     * @param type Loại đạn
     * @param x Tọa độ x
     * @param y Tọa độ y
     * @return Danh sách các điểm đã bắn trúng tàu
     */
    public List<Node> attack(AttackType type, int x, int y) {
        // Có thể assert hoặc throw nếu muốn chắc chắn chỉ gọi khi hợp lệ
        if (!canAttack(type, x, y)) {
			return new ArrayList<>();
		}

        IAttackStrategy strategy = strategies.get(type);
        List<Node> targets = strategy.getAttackPoints(x, y, board);
        List<Node> attacked = new ArrayList<>();

        for (Node node : targets) {
            node.setHit(true);
            attacked.add(node);
        }

        atkInv.useAttack(type);
        return attacked;
    }
}
