package com.battleship.enums;

/**
 * Enum biểu diễn các kiểu tấn công trong trò chơi
 *
 * @author Nguyen Viet Hoang
 * @version 1.0
 * @since 2025-04-27
 */

public enum AttackType {
	SINGLE,	// Tấn công đơn, bắn 1 ô
	CROSS,	// Tấn công dẫu cộng, bắn 5 ô (trung tâm + 4 hướng)
	RANDOM,	// Tấn công ngẫu nhiên, bắn nhiều ô (trung tâm + n ô random)
	DIAMOND	// Tấn công hình thoi, bắn 9 ô theo hình thoi 3x3
}
