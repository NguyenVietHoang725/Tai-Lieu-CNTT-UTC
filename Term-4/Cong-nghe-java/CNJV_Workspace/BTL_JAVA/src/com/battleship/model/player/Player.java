package com.battleship.model.player;

import com.battleship.enums.AttackType;
import com.battleship.model.attack.AttackInventory;
import com.battleship.model.board.Board;
import com.battleship.model.ship.ShipPlacement;

/**
 * Lớp "Player" biểu diễn người chơi trong trò chơi
 *
 * @author Nguyen Viet Hoang
 * @version 1.0
 * @since 2025-04-28
 */
public class Player {
	// --- THUỘC TÍNH ---
	protected String name;
	protected Board board;
	protected AttackInventory atkInv;
	protected ShipPlacement shipPlacement;

	// --- HÀM KHỞI TẠO ---
	public Player(String name, Board board, AttackInventory atkInv) {
		this.name = name;
		this.board = board;
		this.atkInv = atkInv;
		this.shipPlacement = new ShipPlacement(board);
	}

	// --- GETTER & SETTER ---
	public String getName() {
		return name;
	}

	public Board getBoard() {
		return board;
	}

	public AttackInventory getAttackInventory() {
		return atkInv;
	}

	public void setBoard(Board board) {
		this.board = board;
		this.shipPlacement = new ShipPlacement(board);
	}

	public void setAttackInventory(AttackInventory atkInv) {
		this.atkInv = atkInv;
	}

	// --- CÁC PHƯƠNG THỨC KHÁC ---
	/**
	 * Hàm kiểm tra xem có thể đặt tàu hay không
	 *
	 * @param x Tọa độ x của tàu
	 * @param y Tọa độ y của tàu
	 * @param length Độ dài của tàu
	 * @param isHorizontal Xác định tàu nằm ngang hay dọc
	 * @return true nếu có thể đặt tàu, false nếu không
	 */
	public boolean canPlaceShip(int x, int y, int length, boolean isHorizontal) {
		return shipPlacement.canPlaceShip(length, x, y, isHorizontal);
	}

	/**
	 * Hàm đặt tàu
	 *
	 * @param length Độ dài của tàu
	 * @param x Tọa độ x của tàu
	 * @param y Tọa độ y của tàu
	 * @param isHorizontal Xác định tàu nằm ngang hay dọc
	 * @return true nếu đặt tàu thành công, false nếu không
	 */
	public boolean placeShip(int length, int x, int y, boolean isHorizontal) {
		return shipPlacement.placeShip(length, x, y, isHorizontal);
	}

	/**
	 * Hàm kiểm tra xem có thể sử dụng kiểu tấn công hay không
	 *
	 * @param type Kiểu tấn công
	 * @return true nếu có thể sử dụng kiểu tấn công, false nếu không
	 */
	public boolean canUseAttackType(AttackType type) {
		return atkInv.hasAttack(type);
	}

	/**
	 * Hàm kiểm tra xem có thể tấn công hay không
	 *
	 * @param x Tọa độ x của ô
	 * @param y Tọa độ y của ô
	 * @param type Kiểu tấn công
	 * @return true nếu có thể tấn công, false nếu không
	 */
	public boolean canAttack(int x, int y, AttackType type) {
		return board.isValidCoordinate(x, y) && canUseAttackType(type);
	}
}
