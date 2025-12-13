package com.battleship.model.ship;

import com.battleship.model.board.Board;
import com.battleship.model.board.Node;
import java.util.*;

public class ShipPlacement {
    private Board board;
    private Stack<Ship> undoStack = new Stack<>();
    private Stack<Ship> redoStack = new Stack<>();
    public static final int[] SHIP_LENGTHS = {2, 3, 3, 4, 5};

    public ShipPlacement(Board board) {
        if (board == null) throw new IllegalArgumentException("Board không thể là null");
        this.board = board;
    }

    public boolean canPlaceShip(int length, int x, int y, boolean isHorizontal) {
        Ship ship = new Ship(length, isHorizontal);
        return isValidPlacement(ship, x, y);
    }

    public boolean placeShip(int length, int x, int y, boolean isHorizontal) {
        Ship ship = new Ship(length, isHorizontal);
        if (!isValidPlacement(ship, x, y)) return false;

        Node[][] nodes = board.getBoard();
        List<Node> shipNodes = new ArrayList<>();
        if (isHorizontal) {
            for (int i = y; i < y + length; i++) {
                shipNodes.add(nodes[x][i]);
            }
        } else {
            for (int i = x; i < x + length; i++) {
                shipNodes.add(nodes[i][y]);
            }
        }
        for (Node node : shipNodes) node.setHasShip(true);
        ship = new Ship(length, shipNodes, isHorizontal);
        board.addShip(ship);
        undoStack.push(ship);
        redoStack.clear();
        return true;
    }

    public boolean undo() {
        if (undoStack.isEmpty()) return false;
        Ship ship = undoStack.pop();
        removeShip(ship);
        redoStack.push(ship);
        return true;
    }

    public boolean redo() {
        if (redoStack.isEmpty()) return false;
        Ship ship = redoStack.pop();
        // Đặt lại tàu
        for (Node node : ship.getNodes()) node.setHasShip(true);
        board.addShip(ship);
        undoStack.push(ship);
        return true;
    }

    public void removeShip(Ship ship) {
        for (Node node : ship.getNodes()) node.setHasShip(false);
        board.removeShip(ship);
    }

    private boolean isValidPlacement(Ship ship, int x, int y) {
        return board.isValidCoordinate(x, y)
                && isInBoard(ship, x, y)
                && !isOverlap(ship, x, y);
    }

    private boolean isInBoard(Ship ship, int x, int y) {
        if (ship.isHorizontal()) {
            return y + ship.getLength() - 1 < board.getBoardSize();
        } else {
            return x + ship.getLength() - 1 < board.getBoardSize();
        }
    }

    public boolean isOverlap(Ship ship, int x, int y) {
        Node[][] nodes = board.getBoard();
        if (ship.isHorizontal()) {
            for (int i = y; i < y + ship.getLength(); i++) {
                if (nodes[x][i].isHasShip()) return true;
            }
        } else {
            for (int i = x; i < x + ship.getLength(); i++) {
                if (nodes[i][y].isHasShip()) return true;
            }
        }
        return false;
    }

    public Board getBoard() {
        return board;
    }

    public List<Ship> getShips() {
        return board.getShips();
    }

    // Trả về mảng bool: loại tàu nào đã đặt
    public boolean[] getPlacedShipLengths() {
        boolean[] placed = new boolean[SHIP_LENGTHS.length];
        List<Ship> ships = getShips();
        for (int i = 0; i < SHIP_LENGTHS.length; i++) {
            for (Ship s : ships) {
                if (s.getLength() == SHIP_LENGTHS[i]) {
                    placed[i] = true;
                    break;
                }
            }
        }
        return placed;
    }

    public void reset() {
        // Xóa toàn bộ tàu, stack
        for (Ship ship : new ArrayList<>(getShips())) {
            removeShip(ship);
        }
        undoStack.clear();
        redoStack.clear();
    }
}