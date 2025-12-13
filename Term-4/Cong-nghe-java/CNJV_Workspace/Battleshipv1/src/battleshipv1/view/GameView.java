package com.battleshipv1.view;

import com.battleshipv1.model.Node;

public class GameView {

    public void printBoard(Node[][] board, boolean revealShips) {
        System.out.print("   ");
        for (int i = 0; i < board.length; i++) {
            System.out.printf("%2d ", i);
        }
        System.out.println();

        for (int i = 0; i < board.length; i++) {
            System.out.printf("%2d ", i);
            for (int j = 0; j < board[i].length; j++) {
                Node node = board[i][j];
                char symbol = getSymbol(node, revealShips);
                System.out.print(" " + symbol + " ");
            }
            System.out.println();
        }
    }

    private char getSymbol(Node node, boolean revealShips) {
        switch (node.getStatus()) {
            case HIT: return 'X';
            case MISS: return 'O';
            case SHIP: return revealShips ? 'S' : '~';
            default: return '~';
        }
    }

    // Menu hành động trong giai đoạn SETUP (đặt tàu)
    public void showShipPlacementMenu() {
        System.out.println("\n🔧 Hành động đặt tàu:");
        System.out.println("1. Đặt tàu");
        System.out.println("2. Undo");
        System.out.println("3. Redo");
        System.out.println("4. Bắt đầu chơi\n");
    }

    // Menu hành động trong giai đoạn PLAY (tấn công)
    public void showBattleMenu() {
        System.out.println("\n🎯 Hành động tấn công:");
        System.out.println("1. Bắn");
        System.out.println("2. Tạm dừng");
        System.out.println("3. Thoát\n");
    }

    public void showMessage(String message) {
        System.out.println(message);
    }
}
