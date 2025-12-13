package com.battleship.model.logic;

import java.util.ArrayList;
import java.util.List;

import com.battleship.enums.AttackType;
import com.battleship.interfaces.INotifiableBotStrategy;
import com.battleship.model.attack.AttackLogic;
import com.battleship.model.board.Node;
import com.battleship.model.player.Bot;
import com.battleship.model.player.Player;

/**
 * Lớp "VsBotModeLogic" biểu diễn logic của chế độ chơi người chơi vs bot
 *
 * @author Nguyen Viet Hoang
 * @version 1.0
 * @since 2025-04-28
 */
public class VsBotModeLogic extends GameLogic {
    // --- THUỘC TÍNH ---
    private final Bot bot; // Bot
    private final AttackLogic playerAttackLogic; // Logic tấn công của người chơi
    private final AttackLogic botAttackLogic; // Logic tấn công của bot
    private boolean playerTurn; // true: lượt người chơi, false: lượt bot

    // --- HÀM KHỞI TẠO ---
    public VsBotModeLogic(Player player, Bot bot) {
        super(player);
        this.bot = bot;
        this.playerAttackLogic = new AttackLogic(bot.getBoard(), player.getAttackInventory());
        this.botAttackLogic = new AttackLogic(player.getBoard(), bot.getAttackInventory());
        this.playerTurn = true; // Người chơi đi trước
    }

    // --- CÁC PHƯƠNG THỨC KHÁC ---
    /**
     * Bắt đầu game
     */
    @Override
    public void startGame() {
        playerTurn = true;
    }

    /**
     * Người chơi tấn công bot
     *
     * @param type Kiểu tấn công
     * @param x Tọa độ x
     * @param y Tọa độ y
     * @return Danh sách các node bị bắn trúng
     */
    public List<Node> playerAttack(AttackType type, int x, int y) {
        if (!playerTurn || isGameOver() || !player.canAttack(x, y, type)) {
			return new ArrayList<>();
		}
        List<Node> result = playerAttackLogic.attack(type, x, y);
        playerTurn = false;
        return result;
    }

    /**
     * Bot tấn công người chơi
     *
     * @return Danh sách các node bị bắn trúng
     */
    public List<Node> botAttack() {
        if (playerTurn || isGameOver()) {
			return new ArrayList<>();
		}
        int[] coords = bot.chooseAttack(player.getBoard());
        AttackType type = AttackType.SINGLE;
        if (!bot.canAttack(coords[0], coords[1], type)) {
            playerTurn = true;
            return new ArrayList<>();
        }
        List<Node> result = botAttackLogic.attack(type, coords[0], coords[1]);
        boolean hit = result.stream().anyMatch(Node::isHasShip);
        if (hit && bot.getAttackStrategy() instanceof INotifiableBotStrategy) {
            ((INotifiableBotStrategy) bot.getAttackStrategy()).notifyHit(coords[0], coords[1]);
        }
        playerTurn = true;
        return result;
    }


    /**
     * Kiểm tra game đã kết thúc chưa
     *
     * @return true nếu game đã kết thúc, false nếu ngược lại
     */
    @Override
    public boolean isGameOver() {
        // Một trong hai bên hết tàu
        return player.getBoard().allShipsSunk() || bot.getBoard().allShipsSunk();
    }

    /**
     * Kiểm tra người chơi đã thắng chưa
     *
     * @return true nếu người chơi đã thắng, false nếu ngược lại
     */
    @Override
    public boolean isPlayerWin() {
        return bot.getBoard().allShipsSunk();
    }

    /**
     * Kiểm tra lượt người chơi
     *
     * @return true nếu lượt người chơi, false nếu ngược lại
     */
    public boolean isPlayerTurn() {
        return playerTurn;
    }
}