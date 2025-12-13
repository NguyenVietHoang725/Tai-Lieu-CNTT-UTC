package com.battleshipv1.controller;

import com.battleshipv1.interfaces.IController;
import com.battleshipv1.model.GameLogic;
import com.battleshipv1.model.Move;
import com.battleshipv1.utils.InputHandler;
import com.battleshipv1.view.GameView;

public class GameController implements IController {

    private GameLogic game;
    private GameView view;
    private InputHandler input;
    private boolean isPaused;

    @Override
    public void init() {
        this.game = new GameLogic();
        this.view = new GameView();
        this.input = new InputHandler(game, view);
        this.isPaused = false;

        view.showMessage("🎮 Game Battleship đã khởi tạo!");
    }

    @Override
    public void start() {
        view.showMessage("🚀 BẮT ĐẦU GAME BATTLESHIP");

        runShipSetupPhase();
        runBattlePhase();

        end();
    }

    @Override
    public void pause() {
        isPaused = true;
        view.showMessage("⏸ Game đã tạm dừng. Nhấn Enter để tiếp tục...");
        input.waitForEnter();
        isPaused = false;
    }

    @Override
    public void end() {
        view.printBoard(game.getBoard(), true);
        view.showMessage("🏁 Game kết thúc! Bạn đã tiêu diệt toàn bộ tàu.");
    }

    private void runShipSetupPhase() {
        view.showMessage("⚙️ GIAI ĐOẠN ĐẶT TÀU: [2, 3, 3, 4, 5]");
        int currentIndex = 0;
        int[] sizes = game.getShipSizes();

        while (currentIndex < sizes.length) {
            int size = sizes[currentIndex];
            view.showMessage("🚢 Tàu kích thước " + size + ": Nhập x y chiều (0-ngang, 1-dọc)");

            view.showShipPlacementMenu();
            int choice = input.getSetupAction();

            switch (choice) {
                case 1 -> {
                    int[] info = input.getPlaceShipWithSize(size);
                    boolean placed = game.placeShip(info[0], info[1], size, info[2] == 0);

                    if (placed) {
                        view.showMessage("✅ Đặt thành công tàu " + size);
                        view.printBoard(game.getBoard(), true);
                        currentIndex++;
                    } else {
                        view.showMessage("❌ Không thể đặt tàu tại vị trí này.");
                    }
                }
                case 2 -> {
                    if (game.undoShipPlacement()) {
                        view.showMessage("↩️ Đã hoàn tác đặt tàu.");
                        currentIndex = Math.max(currentIndex - 1, 0);
                        view.printBoard(game.getBoard(), true);
                    } else {
                        view.showMessage("❌ Không còn hành động để hoàn tác.");
                    }
                }
                case 3 -> {
                    if (game.redoShipPlacement()) {
                        view.showMessage("↪️ Đã làm lại đặt tàu.");
                        currentIndex++;
                        view.printBoard(game.getBoard(), true);
                    } else {
                        view.showMessage("❌ Không còn hành động để làm lại.");
                    }
                }
                default -> view.showMessage("⚠️ Lựa chọn không hợp lệ!");
            }
        }
    }

    private void runBattlePhase() {
        while (!game.isGameOver()) {
            if (isPaused) continue;

            view.printBoard(game.getBoard(), false);
            view.showBattleMenu();
            int choice = input.getBattleAction();

            switch (choice) {
                case 1 -> handleAttack();
                case 2 -> pause();
                case 3 -> {
                    view.showMessage("❌ Thoát game...");
                    return;
                }
                default -> view.showMessage("⚠️ Lựa chọn không hợp lệ!");
            }
        }
    }

    private void handleAttack() {
        view.showMessage("🎯 Nhập tọa độ tấn công (x y):");
        Move move = input.getMove();
        String result = game.attack(move.getRow(), move.getCol());
        move.setNewValue(game.getNode(move.getRow(), move.getCol()).getStatus());

        if (result.contains("Sunk")) {
            view.showMessage("💥 Một tàu đã bị đánh chìm!");
        }

        view.showMessage("🎯 Kết quả: " + result.replace(" & Sunk", ""));
    }
}
