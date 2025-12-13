package com.battleship.model.board;

import java.util.ArrayList;
import java.util.List;

import com.battleship.model.ship.Ship;

/**
 * Lớp "Board" biểu diễn một bàn chơi của trò chơi
 *
 * @author Nguyen Pham Hoang Mai
 * @version 1.0
 * @since 2025-04-27
 */

public class Board {

	// --- THUỘC TÍNH ---
	private static final int BOARD_SIZE = 10; // Kích thước của bảng
	private Node[][] board; // Mảng 2 chiều lưu trữ các ô trên bảng
	private List<Ship> ships; // Danh sách các tàu trên bảng

	// --- HÀM KHỞI TẠO ---
	/**
	 * Hàm khởi tạo với 2 tham số:
	 *
	 * @param board Mảng 2 chiều lưu trữ các ô trên bảng
	 * @param lship Danh sách các tàu trên bảng
	 */
	public Board() {
		this.board = new Node[BOARD_SIZE][BOARD_SIZE];
		this.ships = new ArrayList<>();
		for (int i = 0; i < BOARD_SIZE; i++) {
			for (int j = 0; j < BOARD_SIZE; j++) {
				board[i][j] = new Node(i, j);
			}
		}
	}

	// --- GETTER & SETTER ---
	/**
	 * Hàm lấy ô trên bảng
	 *
	 * @param x Tọa độ x của ô
	 * @param y Tọa độ y của ô
	 * @return ô trên bảng
	 */
	public Node getNode(int x, int y) {
		return board[x][y];
	}

	/**
	 * Hàm lấy mảng 2 chiều lưu trữ các ô trên bảng
	 *
	 * @return mảng 2 chiều lưu trữ các ô trên bảng
	 */
	public Node[][] getBoard() {
		return board;
	}

	/**
	 * Hàm lấy kích thước của bảng
	 *
	 * @return kích thước của bảng
	 */
	public int getBoardSize() {
		return BOARD_SIZE;
	}

	// --- CÁC PHƯƠNG THỨC KHÁC ---
	/**
	 * Hàm thêm tàu vào danh sách tàu
	 *
	 * @param ship Tàu cần thêm
	 */
	public void addShip(Ship ship) {
		ships.add(ship);
	}

	public void addShip(int x, int y, int length, boolean isHorizontal) {
	    Ship ship = new Ship(length, isHorizontal);
	    for (int i = 0; i < length; i++) {
	        int nx = isHorizontal ? x + i : x;
	        int ny = isHorizontal ? y : y + i;
	        Node node = getNode(nx, ny); // Lấy node từ board
	        ship.addNode(node);
	    }
	    ships.add(ship); // Thêm tàu vào danh sách tàu của board
	}

	public List<Ship> getShips() {
	    return ships;
	}

	/**
	 * Hàm kiểm tra tất cả tàu đã chìm hết chưa
	 *
	 * @return true nếu tất cả tàu đã chìm hết, false nếu không
	 */
	public boolean allShipsSunk() {
		for (Ship ship : ships) {
			if (!ship.isSunk()) {
				return false;
			}
		}

		return true;
	}

	/**
	 * Hàm kiểm tra tọa độ có hợp lệ hay không
	 *
	 * @param x Tọa độ x của ô
	 * @param y Tọa độ y của ô
	 * @return true nếu tọa độ có hợp lệ, false nếu không
	 */
	public boolean isValidCoordinate(int x, int y) {
		return x >= 0 && x < BOARD_SIZE && y >= 0 && y < BOARD_SIZE;
	}
}
