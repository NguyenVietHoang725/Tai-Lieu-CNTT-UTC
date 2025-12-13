package com.battleship.model.ship;

import com.battleship.model.board.Board;
import com.battleship.model.board.Node;

/**
 * Lớp "ShipPlacement" biểu diễn việc đặt tàu trong trò chơi
 *
 * @author Nguyen Viet Hoang, Nguyen Pham Hoang Mai
 * @version 1.0
 * @since 2025-04-27
 */

public class ShipPlacement {

    // --- THUỘC TÍNH ---
    private Board board; // Bảng trò chơi
    public static final int[] SHIP_LENGTHS = {2, 3, 3, 4, 5}; // Độ dài của các tàu

    // --- HÀM KHỞI TẠO ---
    /**
     * Hàm khởi tạo với 1 tham số:
     *
     * @param board Bảng trò chơi
     */
    public ShipPlacement(Board board) {
        if (board == null) {
            throw new IllegalArgumentException("Board không thể là null");
        }
        this.board = board;
    }

    // --- CÁC PHƯƠNG THỨC KHÁC ---
    /**
     * Hàm kiểm tra xem có thể đặt tàu hay không
     *
     * @param length Độ dài của tàu
     * @param x Tọa độ x của tàu
     * @param y Tọa độ y của tàu
     * @param isHorizontal Xác định tàu nằm ngang hay dọc
     * @return true nếu có thể đặt tàu, false nếu không thể đặt
     */
    public boolean canPlaceShip(int length, int x, int y, boolean isHorizontal) {
    	Ship ship = new Ship(length, isHorizontal);
    	return isValidPlacement(ship, x, y);
    }
    /**
     * Hàm kiểm tra và đặt tàu vào bảng
     *
     * @param length Độ dài của tàu
     * @param x Tọa độ x của tàu
     * @param y Tọa độ y của tàu
     * @param isHorizontal Xác định tàu nằm ngang hay dọc
     * @return true nếu đặt tàu thành công, false nếu không thể đặt
     */
    public boolean placeShip(int length, int x, int y, boolean isHorizontal) {
    	Ship ship = new Ship(length, isHorizontal);

        if (!isValidPlacement(ship, x, y)) {
            return false;
        }

        Node[][] nodes = board.getBoard();

        if (isHorizontal) {
            for (int i = y; i < y + length; i++) {
                ship.addNode(nodes[x][i]);
            }
        } else {
            for (int i = x; i < x + length; i++) {
                ship.addNode(nodes[i][y]);
            }
        }

        board.addShip(ship);

        return true;
    }

    /**
     * Hàm kiểm tra vị trí đặt tàu có hợp lệ không
     * Kiểm tra xem tàu có nằm trong bảng không, không chồng lên nhau và không ra ngoài bảng
     *
     * @param ship Tàu cần kiểm tra
     * @param x Tọa độ x của tàu
     * @param y Tọa độ y của tàu
     * @return true nếu vị trí đặt tàu hợp lệ, false nếu không hợp lệ
     */
    private boolean isValidPlacement(Ship ship, int x, int y) {
        return board.isValidCoordinate(x, y)
               && isInBoard(ship, x, y)
               && !isOverlap(ship, x, y);
    }

    /**
     * Hàm kiểm tra tàu có nằm trong bảng không
     * Kiểm tra theo chiều ngang hoặc chiều dọc
     * @return true nếu tàu nằm trong bảng, false nếu không nằm trong bảng
     */
    private boolean isInBoard(Ship ship, int x, int y) {
        // Sử dụng board.getBoardSize() để kiểm tra kích thước bảng
        if (ship.isHorizontal()) {
            return y + ship.getLength() - 1 < board.getBoardSize();
        } else {
            return x + ship.getLength() - 1 < board.getBoardSize();
        }
    }

    /**
     * Hàm kiểm tra tàu có chồng lên nhau không
     * Kiểm tra theo chiều ngang hoặc chiều dọc
     * @return true nếu tàu chồng lên nhau, false nếu không chồng lên nhau
     */
    public boolean isOverlap(Ship ship, int x, int y) {
        // Sử dụng board.getBoard() để lấy mảng Node
        Node[][] nodes = board.getBoard();
        if (ship.isHorizontal()) {
            for (int i = y; i < y + ship.getLength(); i++) {
                if (nodes[x][i].isHasShip()) {
                    return true;
                }
            }
        } else {
            for (int i = x; i < x + ship.getLength(); i++) {
                if (nodes[i][y].isHasShip()) {
                    return true;
                }
            }
        }
        return false;
    }
}
