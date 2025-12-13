package com.battleshipv1.model;

import java.util.ArrayList;
import java.util.List;
import java.util.Stack;

import com.battleshipv1.enums.NodeStatus;

public class GameLogic {
	
	// Attributes
	private Node[][] board;
	private List<Ship> ships;
	private final int BOARD_SIZE = 10;
	private final int[] SHIP_SIZES = {2, 3, 3, 4, 5};
	private Stack<ShipPlacement> placementUndoStack = new Stack<>();
	private Stack<ShipPlacement> placementRedoStack = new Stack<>();
	private List<Move> attackHistory = new ArrayList<>();

	
	// Constructor
	public GameLogic() {
		board = new Node[BOARD_SIZE][BOARD_SIZE];
		ships = new ArrayList<>();
		initBoard();
	}
	
	private void initBoard() {
		for (int i = 0; i < BOARD_SIZE; i++) {
			for (int j = 0; j < BOARD_SIZE; j++) {
				board[i][j] = new Node(i, j);
			}
		}
	}
	
	public boolean placeShip(int x, int y, int size, boolean isHorizontal) {
		List<Node> shipNodes = new ArrayList<>();
		
		for (int i = 0; i < size; i++) {
			int xi = isHorizontal ? x + i : x;
			int yi = isHorizontal ? y : y + i;
			
			if (!isValidCoordinate(xi, yi) || board[xi][yi].isOccupied()) {
				return false;
			}
			shipNodes.add(board[xi][yi]);
		}
		
		for (Node node : shipNodes) {
			node.setStatus(NodeStatus.SHIP);
		}
		
		Node[] placedNodes = shipNodes.toArray(new Node[0]);
		ships.add(new Ship(placedNodes));
		placementUndoStack.push(new ShipPlacement(placedNodes));
	    placementRedoStack.clear(); // clear redo sau khi đặt mới)
		
		return true;
	}
	
	public String attack(int x, int y) {
        if (!isValidCoordinate(x, y)) return "Invalid";

        Node targetNode = board[x][y];

        String result = updateNodeStatus(targetNode);

        if (result.equals("Hit")) {
            for (Ship ship : ships) {
            	boolean wasSunkBefore = ship.isSunk();
                ship.updateStatus();
                
                if (!wasSunkBefore && ship.isSunk()) {
                	result += " & Sunk";
                }
            }
        }

        return result;
    }
	
	private void recordMove(int x, int y, NodeStatus prev, NodeStatus next) {
	    attackHistory.add(new Move(x, y, prev, next));
	}
	
	private String updateNodeStatus(Node node) {
		NodeStatus prev = node.getStatus();
		NodeStatus newStatus;
		
        switch (prev) {
            case SHIP:
                newStatus = NodeStatus.HIT;
                break;

            case EMPTY:
                newStatus = NodeStatus.MISS;
                break;

            case HIT:
            case MISS:
                return "Already Attacked";

            default:
                return "Unknown";
        }
        
        node.setStatus(newStatus);
        
        recordMove(node.getX(), node.getY(), prev, newStatus);

        return newStatus == NodeStatus.HIT ? "Hit" : "Miss";
    }
	
	public boolean undoShipPlacement() {
	    if (placementUndoStack.isEmpty()) return false;

	    ShipPlacement lastPlacement = placementUndoStack.pop();
	    for (Node node : lastPlacement.getNodes()) {
	        node.setStatus(NodeStatus.EMPTY);
	    }

	    // Xóa khỏi danh sách tàu
	    ships.removeIf(ship -> {
	        Node[] shipNodes = ship.getNodes();
	        return shipNodes.length == lastPlacement.getNodes().length &&
	               java.util.Arrays.equals(shipNodes, lastPlacement.getNodes());
	    });

	    placementRedoStack.push(lastPlacement);
	    return true;
	}

	public boolean redoShipPlacement() {
	    if (placementRedoStack.isEmpty()) return false;

	    ShipPlacement redoPlacement = placementRedoStack.pop();
	    for (Node node : redoPlacement.getNodes()) {
	        node.setStatus(NodeStatus.SHIP);
	    }

	    ships.add(new Ship(redoPlacement.getNodes()));
	    placementUndoStack.push(redoPlacement);
	    return true;
	}


	
	public boolean canUndoPlacement() {
	    return !placementUndoStack.isEmpty();
	}

	public boolean canRedoPlacement() {
	    return !placementRedoStack.isEmpty();
	}
	
	public boolean isGameOver() {
		for (Ship ship : ships) {
	        if (!ship.isSunk()) {
	            return false;
	        }
	    }

	    return true;
    }
	
	private boolean isValidCoordinate(int x, int y) {
        return x >= 0 && x < BOARD_SIZE && y >= 0 && y < BOARD_SIZE;
    }
	
	public Node[][] getBoard() {
        return board;
    }
	
	public Node getNode(int x, int y) {
		return board[x][y];
	}

    public List<Ship> getShips() {
        return ships;
    }
    
    public int[] getShipSizes() {
        return SHIP_SIZES;
    }

}
