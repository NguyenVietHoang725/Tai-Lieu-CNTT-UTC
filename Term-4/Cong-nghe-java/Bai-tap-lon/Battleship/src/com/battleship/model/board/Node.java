package com.battleship.model.board;

/**
 * Lớp {@code Node} đại diện cho một ô (cell) trên bàn cờ của trò chơi Battleship.
 * Mỗi ô có tọa độ riêng (x, y), một trạng thái để xác định ô có chứa tàu hay không,
 * và một trạng thái để xác định ô đã bị bắn trúng hay chưa.
 * 
 * Lớp này được sử dụng để quản lý các ô trên bàn cờ, giúp theo dõi việc đặt tàu và trạng thái 
 * của các cuộc tấn công vào mỗi ô.
 * 
 */
public class Node {
    private final int x; 
    private final int y; 
    private boolean hasShip;
    private boolean isHit;

    /**
     * Khởi tạo một đối tượng {@code Node} mới với tọa độ đã chỉ định.
     * Mặc định, ô này không chứa tàu và chưa bị bắn trúng.
     *
     * @param x tọa độ x (hàng) của ô.
     * @param y tọa độ y (cột) của ô.
     */
    public Node(int x, int y) {
        this.x = x;
        this.y = y;
        this.hasShip = false;
        this.isHit = false;
    }

    /**
     * Lấy tọa độ x (hàng) của ô.
     *
     * @return tọa độ x của ô.
     */
    public int getX() {
        return x;
    }

    /**
     * Lấy tọa độ y (cột) của ô.
     *
     * @return tọa độ y của ô.
     */
    public int getY() {
        return y;
    }

    /**
     * Kiểm tra xem ô này có chứa tàu hay không.
     *
     * @return {@code true} nếu ô chứa tàu, {@code false} nếu không chứa tàu.
     */
    public boolean isHasShip() {
        return hasShip;
    }

    /**
     * Đặt trạng thái cho ô này để xác định có chứa tàu hay không.
     *
     * @param hasShip {@code true} nếu ô chứa tàu, {@code false} nếu không chứa tàu.
     */
    public void setHasShip(boolean hasShip) {
        this.hasShip = hasShip;
    }

    /**
     * Kiểm tra xem ô này đã bị bắn trúng hay chưa.
     *
     * @return {@code true} nếu ô đã bị bắn trúng, {@code false} nếu chưa bị bắn trúng.
     */
    public boolean isHit() {
        return isHit;
    }

    /**
     * Đặt trạng thái cho ô này để xác định ô đã bị bắn trúng hay chưa.
     *
     * @param hit {@code true} nếu ô đã bị bắn trúng, {@code false} nếu chưa bị bắn trúng.
     */
    public void setHit(boolean hit) {
        isHit = hit;
    }
}
