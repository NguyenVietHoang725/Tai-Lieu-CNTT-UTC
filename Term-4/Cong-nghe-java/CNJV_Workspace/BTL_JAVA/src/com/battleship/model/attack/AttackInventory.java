package com.battleship.model.attack;

import java.util.HashMap;
import java.util.Map;

import com.battleship.enums.AttackType;

/**
 * Lớp "AttackInventory" biểu diễn kho tấn công trong trò chơi, lưu trữ số lượng tấn công của từng kiểu
 *
 * @author Nguyen Viet Hoang
 * @version 1.0
 * @since 2025-04-27
 */

public class AttackInventory {
    // --- THUỘC TÍNH ---
    private final Map<AttackType, Integer> attackCount = new HashMap<>(); // Số lượng tấn công của từng kiểu

    // --- HÀM KHỞI TẠO ---
    /**
     * Khởi tạo kho tấn công với số lượng tấn công của từng kiểu
     * Single Attack: vô hạn số lượng
     * Cross Attack: 2 lần
     * Random Attack: 0 lần
     * Diamond Attack: 0 lần
     */
    public AttackInventory() {
        attackCount.put(AttackType.SINGLE, Integer.MAX_VALUE);
        attackCount.put(AttackType.CROSS, 2);
        attackCount.put(AttackType.RANDOM, 0);
        attackCount.put(AttackType.DIAMOND, 0);
    }

    // --- CÁC PHƯƠNG THỨC KHÁC ---
    /**
     * Hàm kiểm tra xem còn số lượng tấn công kiểu này hay không
     *
     * @param type Kiểu tấn công
     * @return true nếu còn tấn công kiểu này, false nếu không
     */
    public boolean hasAttack(AttackType type) {
    	return attackCount.get(type) > 0;
    }

    /**
     * Hàm sử dụng tấn công kiểu này
     *
     * @param type Kiểu tấn công
     */
    public void useAttack(AttackType type) {
    	if (type != AttackType.SINGLE) {
    		attackCount.put(type,  attackCount.get(type) - 1);
    	}
    }

    /**
     * Hàm thêm tấn công kiểu này
     *
     * @param type Kiểu tấn công
     * @param amount Số lượng tấn công
     */
    public void addAttack(AttackType type, int amount) {
    	if (type != AttackType.SINGLE) {
    		attackCount.put(type, attackCount.get(type) + amount);
    	}
    }

    /**
     * Hàm lấy số lượng tấn công của kiểu này
     *
     * @param type Kiểu tấn công
     * @return Số lượng tấn công
     */
    public int getAttackCount(AttackType type) {
    	return attackCount.get(type);
    }
}
