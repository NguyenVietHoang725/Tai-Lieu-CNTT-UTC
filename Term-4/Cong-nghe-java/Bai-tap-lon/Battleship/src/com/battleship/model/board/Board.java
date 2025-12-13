package com.battleship.model.board;

import java.util.ArrayList;
import java.util.List;

import com.battleship.model.ship.Ship;

/**
 * Lớp {@code Board} đại diện cho bàn chơi trong trò chơi Battleship.
 * Nó chứa các ô (nodes) và các tàu (ships) trên bàn cờ, đồng thời cung cấp các phương thức để
 * quản lý và tương tác với các tàu và ô trên bảng.
 * 
 */
public class Board {
    private static final int BOARD_SIZE = 10; // Kích thước của bảng
    private Node[][] board; // Mảng 2 chiều lưu trữ các ô trên bảng
    private List<Ship> ships; // Danh sách các tàu trên bảng

    /**
     * Khởi tạo bảng với kích thước mặc định và tạo các ô cho bảng.
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

    /**
     * Lấy ô tại tọa độ (x, y) trên bảng.
     *
     * @param x Tọa độ x của ô.
     * @param y Tọa độ y của ô.
     * @return ô tại tọa độ (x, y).
     */
    public Node getNode(int x, int y) {
        return board[x][y];
    }

    /**
     * Lấy mảng 2 chiều chứa tất cả các ô trên bảng.
     *
     * @return mảng 2 chiều chứa các ô trên bảng.
     */
    public Node[][] getBoard() {
        return board;
    }

    /**
     * Lấy kích thước của bảng.
     *
     * @return kích thước của bảng.
     */
    public int getBoardSize() {
        return BOARD_SIZE;
    }

    /**
     * Thêm một tàu vào danh sách các tàu trên bảng.
     *
     * @param ship Tàu cần thêm vào bảng.
     */
    public void addShip(Ship ship) {
        ships.add(ship);
    }

    /**
     * Thêm tàu vào bảng tại vị trí (x, y) với chiều dài và hướng xác định.
     *
     * @param x Tọa độ x của tàu.
     * @param y Tọa độ y của tàu.
     * @param length Chiều dài của tàu.
     * @param isHorizontal Nếu {@code true}, tàu được đặt theo chiều ngang, nếu {@code false}, tàu được đặt theo chiều dọc.
     * @return tàu vừa được thêm vào bảng.
     */
    public Ship addShip(int x, int y, int length, boolean isHorizontal) {
        List<Node> shipNodes = new ArrayList<>();
        if (isHorizontal) {
            for (int i = 0; i < length; i++) {
                shipNodes.add(getNode(x, y + i));
            }
        } else {
            for (int i = 0; i < length; i++) {
                shipNodes.add(getNode(x + i, y));
            }
        }
        for (Node node : shipNodes) node.setHasShip(true);
        Ship ship = new Ship(length, shipNodes, isHorizontal);
        ships.add(ship);
        return ship;
    }

    /**
     * Lấy danh sách các tàu trên bảng.
     *
     * @return danh sách các tàu.
     */
    public List<Ship> getShips() {
        return ships;
    }

    /**
     * Kiểm tra xem tất cả các tàu trên bảng đã chìm chưa.
     *
     * @return {@code true} nếu tất cả tàu đã chìm, {@code false} nếu không.
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
     * Xóa tàu khỏi bảng và cập nhật trạng thái các ô bị ảnh hưởng.
     *
     * @param ship Tàu cần xóa khỏi bảng.
     */
    public void removeShip(Ship ship) {
        if (ship == null) return;
        // Xóa trạng thái có tàu trên các node
        for (Node node : ship.getNodes()) {
            node.setHasShip(false);
        }
        // Xóa tàu khỏi danh sách
        ships.remove(ship);
    }

    /**
     * Kiểm tra xem tàu có bị chìm tại tọa độ (x, y) hay không.
     *
     * @param x Tọa độ x của ô cần kiểm tra.
     * @param y Tọa độ y của ô cần kiểm tra.
     * @return {@code true} nếu tàu đã chìm tại tọa độ, {@code false} nếu chưa chìm.
     */
    public boolean isShipSunkAt(int x, int y) {
        for (Ship ship : ships) {
            for (Node node : ship.getNodes()) {
                if (node.getX() == x && node.getY() == y) {
                    return ship.isSunk();
                }
            }
        }
        return false;
    }

    /**
     * Kiểm tra tính hợp lệ của tọa độ (x, y) trên bảng.
     *
     * @param x Tọa độ x của ô.
     * @param y Tọa độ y của ô.
     * @return {@code true} nếu tọa độ hợp lệ, {@code false} nếu không hợp lệ.
     */
    public boolean isValidCoordinate(int x, int y) {
        return x >= 0 && x < BOARD_SIZE && y >= 0 && y < BOARD_SIZE;
    }

    public int getRemainingShips() {
        int remainingShips = 0;
        
        // Duyệt qua tất cả các tàu trên bảng
        for (Ship ship : ships) {
            // Kiểm tra xem tàu có bị đánh chìm không
            if (!isShipSunkAt(ship.getNodes().get(0).getX(), ship.getNodes().get(0).getY())) {
                remainingShips++;
            }
        }
        
        return remainingShips;
    }

}
