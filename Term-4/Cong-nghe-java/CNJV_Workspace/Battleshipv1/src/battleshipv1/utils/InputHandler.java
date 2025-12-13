package com.battleshipv1.utils;

import java.util.Scanner;

import com.battleshipv1.model.GameLogic;
import com.battleshipv1.model.Move;
import com.battleshipv1.view.GameView;

public class InputHandler {

    private Scanner scn;
    private GameLogic game;
    private GameView view;

    public InputHandler(GameLogic game, GameView view) {
        this.scn = new Scanner(System.in);
        this.game = game;
        this.view = view;
    }

    // --- Dùng trong giai đoạn chơi ---
    public int getBattleAction() {
        view.showMessage("🔧 Chọn hành động (1: Bắn, 2: Tạm dừng, 3: Thoát): ");
        return readInt();
    }

    // --- Dùng trong giai đoạn đặt tàu ---
    public int getSetupAction() {
        view.showMessage("🔧 Chọn hành động (1: Đặt tàu, 2: Undo, 3: Redo): ");
        return readInt();
    }

    public Move getMove() {
        int row = readInt();
        int col = readInt();
        return new Move(row, col, game.getNode(row, col).getStatus(), null);
    }

    public void waitForEnter() {
        view.showMessage("Nhấn Enter để tiếp tục...");
        scn.nextLine(); // clear buffer
        scn.nextLine(); // đợi Enter
    }

    public int[] getPlaceShipWithSize(int size) {
        while (true) {
            try {
                int x = readInt();
                int y = readInt();
                int dir = readInt(); // 0-ngang, 1-dọc

                if (!validateShipPlacement(x, y, size, dir)) {
                    view.showMessage("❌ Vị trí không hợp lệ hoặc trùng tàu khác. Hãy thử lại.");
                    continue;
                }

                return new int[] { x, y, dir };
            } catch (Exception e) {
                view.showMessage("❌ Dữ liệu không hợp lệ. Nhập lại.");
                scn.nextLine(); // clear input
            }
        }
    }

    private boolean validateShipPlacement(int x, int y, int size, int dir) {
        if (size <= 0 || size > 5) return false;
        if (dir != 0 && dir != 1) return false;

        for (int i = 0; i < size; i++) {
            int xi = dir == 0 ? x + i : x;
            int yi = dir == 1 ? y + i : y;

            if (xi < 0 || xi >= 10 || yi < 0 || yi >= 10) return false;
            if (game.getNode(xi, yi).isOccupied()) return false;
        }

        return true;
    }

    private int readInt() {
        while (!scn.hasNextInt()) {
            view.showMessage("⚠️ Vui lòng nhập số hợp lệ!");
            scn.next(); // bỏ qua input không phải số
        }
        return scn.nextInt();
    }
}
