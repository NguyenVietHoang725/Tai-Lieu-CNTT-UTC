package com.battleship.model.ship;

import java.util.ArrayList;
import java.util.List;

import com.battleship.model.board.Node;

/**
 * Lớp "Ship" biểu diễn tàu trong trò chơi
 *
 * @author Nguyen Pham Hoang Mai
 * @version 1.0
 * @since 2025-04-27
 */

public class Ship {

	// --- THUỘC TÍNH ---
	private int length; // Độ dài của tàu
	private List<Node> nodes; // Danh sách node của tàu
	private boolean isHorizontal; // Xác định tàu nằm ngang hay dọc (true = ngang; false = dọc)

	// --- HÀM KHỞI TẠO ---
	/**
	 * Hàm khởi tạo với 2 tham số:
	 *
	 * @param length Độ dài của tàu
	 * @param isHorizontal Xác định tàu nằm ngang hay dọc (true = ngang; false = dọc)
	 */
	public Ship(int length, boolean isHorizontal) {
		this.length = length;
		this.isHorizontal = isHorizontal;
		this.nodes = new ArrayList<>();
	}

	/**
	 * Hàm khởi tạo với 3 tham số:
	 *
	 * @param length Độ dài của tàu
	 * @param lnode Danh sách node của tàu
	 * @param isHorizontal Xác định tàu nằm ngang hay dọc (true = ngang; false = dọc)
	 */
	public Ship(int length, List<Node> lnode, boolean isHorizontal) {
		this.length = length;
		nodes = lnode;
		this.isHorizontal = isHorizontal;
	}

	// --- GETTER & SETTER ---
	/**
	 * Hàm lấy độ dài của tàu
	 *
	 * @return Độ dài của tàu
	 */
	public int getLength() {
		return length;
	}

	/**
	 * Hàm thiết lập độ dài của tàu
	 *
	 * @param length Độ dài của tàu
	 */
	public void setLength(int length) {
		this.length = length;
	}

	public List<Node> getNodes() {
	    return nodes;
	}

	/**
	 * Hàm lấy xác định tàu nằm ngang hay dọc
	 *
	 * @return Xác định tàu nằm ngang hay dọc
	 */
	public boolean isHorizontal() {
		return isHorizontal;
	}

	/**
	 * Hàm thiết lập xác định tàu nằm ngang hay dọc
	 *
	 * @param isHorizontal Xác định tàu nằm ngang hay dọc
	 */
	public void setHorizontal(boolean isHorizontal) {
		this.isHorizontal = isHorizontal;
	}

	// --- CÁC PHƯƠNG THỨC KHÁC ---
	/**
	 * Hàm thêm một node vào tàu
	 *
	 * @param node Node cần thêm
	 */
	public void addNode(Node node) {
		nodes.add(node);
		node.setHasShip(true);
	}

	public String toSaveString() {
	    if (nodes.isEmpty()) {
			return "";
		}
	    Node first = nodes.get(0);
	    int x = first.getX();
	    int y = first.getY();
	    // Độ dài và hướng đã có sẵn
	    return x + " " + y + " " + length + " " + isHorizontal;
	}

	/**
	 * Hàm kiểm tra tàu chìm hay chưa
	 *
	 * @return true nếu tàu chìm, false nếu không
	 */
	public boolean isSunk() {
		for (Node node : nodes) {
			if (!node.isHit()) {
				return false;
			}
		}

		return true;
	}
}
