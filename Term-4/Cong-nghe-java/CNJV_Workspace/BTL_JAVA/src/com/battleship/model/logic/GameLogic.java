package com.battleship.model.logic;

import com.battleship.model.player.Player;

/**
 * Lớp "GameLogic" biểu diễn logic của trò chơi
 *
 * @author Nguyen Viet Hoang
 * @version 1.0
 * @since 2025-04-28
 */
public abstract class GameLogic {
    // --- THUỘC TÍNH ---
    protected Player player;

    // --- HÀM KHỞI TẠO ---
    public GameLogic(Player player) {
        this.player = player;
    }

    // --- GETTER & SETTER ---
    public Player getPlayer() {
        return player;
    }

    // --- CÁC PHƯƠNG THỨC KHÁC ---
    /**
     * Bắt đầu game
     */
    public abstract void startGame();

    /**
     * Kiểm tra game đã kết thúc chưa
     *
     * @return true nếu game đã kết thúc, false nếu ngược lại
     */
    public abstract boolean isGameOver();

    /**
     * Kiểm tra người chơi đã thắng chưa
     *
     * @return true nếu người chơi đã thắng, false nếu ngược lại
     */
    public abstract boolean isPlayerWin();

    /**
     * Có thể thêm các phương thức chung khác nếu cần
     */
}