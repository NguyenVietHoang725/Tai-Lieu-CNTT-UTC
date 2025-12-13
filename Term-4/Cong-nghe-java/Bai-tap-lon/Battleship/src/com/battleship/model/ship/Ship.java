package com.battleship.model.ship;

import java.util.ArrayList;
import java.util.List;
import com.battleship.model.board.Node;

/**
 * Lớp "Ship" biểu diễn tàu trong trò chơi.
 *
 */
public class Ship {
	private int length; // Độ dài của tàu
	private List<Node> nodes; // Danh sách các node của tàu
	private boolean isHorizontal; // Xác định tàu nằm ngang hay dọc (true = ngang, false = dọc)

	/**
	 * Hàm khởi tạo tàu với độ dài và hướng.
	 *
	 * @param length       Độ dài của tàu
	 * @param isHorizontal Xác định tàu nằm ngang hay dọc (true = ngang, false =
	 *                     dọc)
	 */
	public Ship(int length, boolean isHorizontal) {
		this.length = length;
		this.isHorizontal = isHorizontal;
		this.nodes = new ArrayList<>();
	}

	/**
	 * Hàm khởi tạo tàu với độ dài, danh sách node và hướng.
	 *
	 * @param length       Độ dài của tàu
	 * @param nodes        Danh sách node của tàu
	 * @param isHorizontal Xác định tàu nằm ngang hay dọc
	 */
	public Ship(int length, List<Node> nodes, boolean isHorizontal) {
		this.length = length;
		this.nodes = nodes;
		this.isHorizontal = isHorizontal;
	}

	public int getLength() {
		return length;
	}

	public void setLength(int length) {
		this.length = length;
	}

	public List<Node> getNodes() {
		return nodes;
	}

	public boolean isHorizontal() {
		return isHorizontal;
	}

	public void setHorizontal(boolean isHorizontal) {
		this.isHorizontal = isHorizontal;
	}

	/**
	 * Hàm thêm một node vào tàu.
	 *
	 * @param node Node cần thêm vào tàu
	 */
	public void addNode(Node node) {
		nodes.add(node);
		node.setHasShip(true);
	}

	/**
	 * Hàm kiểm tra tàu đã chìm hay chưa.
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

	/**
	 * Hàm trả về thông tin tàu dưới dạng chuỗi để lưu trữ.
	 *
	 * @return Chuỗi đại diện tàu
	 */
	public String toSaveString() {
		if (nodes.isEmpty()) {
			return "";
		}
		Node first = nodes.get(0);
		int x = first.getX();
		int y = first.getY();
		return x + " " + y + " " + length + " " + isHorizontal;
	}

	/**
	 * Lấy tọa độ x của điểm bắt đầu của tàu
	 * 
	 * @return tọa độ x của điểm bắt đầu
	 */
	public int getStartX() {
		if (nodes != null && !nodes.isEmpty()) {
			return nodes.get(0).getX();
		}
		return 0;
	}

	/**
	 * Lấy tọa độ y của điểm bắt đầu của tàu
	 * 
	 * @return tọa độ y của điểm bắt đầu
	 */
	public int getStartY() {
		if (nodes != null && !nodes.isEmpty()) {
			return nodes.get(0).getY();
		}
		return 0;
	}

}