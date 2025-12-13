package com.battleship.model.board;

/**
 * Lớp "Node" biểu diễn một ô trên ma trận bàn chơi.
 *
 * @author Nguyen Pham Hoang Mai
 * @version 1.0
 * @since 2025-04-27
 */

public class Node {

	// --- THUỘC TÍNH ---
	private int x, y; // Tọa độ của một ô
	private boolean hasShip; // Biến logic thể hiện có tàu hay không
	private boolean isHit; // Biến logic thể hiện bị bắn hay chưa

	// --- HÀM KHỞI TẠO ---
	/**
	 * Hàm khởi tạo với 2 tham số:
	 *
	 * @param x Tọa độ x của ô
	 * @param y Tọa độ y của ô
	 */
	public Node(int x, int y) {
		this.x = x;
		this.y = y;
		this.hasShip = false;
		this.isHit = false;
	}

	/**
	 * Hàm khởi tạo với 4 tham số:
	 *
	 * @param x Tọa độ x của ô
	 * @param y Tọa độ y của ô
	 * @param hasShip Có tàu hay không
	 * @param isHit Bị bắn hay chưa
	 */
	public Node(int x, int y, boolean hasShip, boolean isHit) {
		this.x = x;
		this.y = y;
		this.hasShip = hasShip;
		this.isHit = isHit;
	}

	// --- GETTER & SETTER ---
	/**
	 * Hàm lấy tọa độ x của ô
	 *
	 * @return Tọa độ x của ô
	 */
	public int getX() {
		return x;
	}

	/**
	 * Hàm thiết lập tọa độ x của ô
	 *
	 * @param x Tọa độ x của ô
	 */
	public void setX(int x) {
		this.x = x;
	}

	/**
	 * Hàm lấy tọa độ y của ô
	 *
	 * @return Tọa độ y của ô
	 */
	public int getY() {
		return y;
	}

	/**
	 * Hàm thiết lập tọa độ y của ô
	 *
	 * @param y Tọa độ y của ô
	 */
	public void setY(int y) {
		this.y = y;
	}

	/**
	 * Hàm lấy có tàu hay không
	 *
	 * @return Có tàu hay không
	 */
	public boolean isHasShip() {
		return hasShip;
	}

	/**
	 * Hàm thiết lập có tàu hay không
	 *
	 * @param hasShip Có tàu hay không
	 */
	public void setHasShip(boolean hasShip) {
		this.hasShip = hasShip;
	}

	/**
	 * Hàm lấy có bị bắn hay chưa
	 *
	 * @return Có bị bắn hay chưa
	 */
	public boolean isHit() {
		return isHit;
	}

	/**
	 * Hàm thiết lập có bị bắn hay chưa
	 *
	 * @param isHit Có bị bắn hay chưa
	 */
	public void setHit(boolean isHit) {
		this.isHit = isHit;
	}

}
