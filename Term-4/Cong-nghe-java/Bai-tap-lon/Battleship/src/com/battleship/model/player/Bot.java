package com.battleship.model.player;

import com.battleship.enums.AttackType;
import com.battleship.interfaces.IBotAttackStrategy;
import com.battleship.model.attack.AttackInventory;
import com.battleship.model.board.Board;
import com.battleship.model.botstrategy.EasyBotAtkStrategy;
import com.battleship.model.botstrategy.HardBotAtkStrategy;
import com.battleship.model.botstrategy.MediumBotAtkStrategy;

public class Bot extends Player {
    private IBotAttackStrategy attackStrategy;
    private String difficulty; // "Easy", "Medium", "Hard"

    // Constructor đầy đủ
    public Bot(String name, Board board, AttackInventory atkInv, IBotAttackStrategy attackStrategy, String difficulty) {
        super(name, board, atkInv);
        this.attackStrategy = attackStrategy;
        this.difficulty = difficulty;
    }

    // Constructor đơn giản hóa cho việc tạo bot với độ khó
    public Bot(String difficulty) {
        super("Bot", new Board(), new AttackInventory());
        this.difficulty = difficulty;
        // Khởi tạo attack strategy dựa vào độ khó
        this.attackStrategy = createAttackStrategy(difficulty);
    }

    // Phương thức tạo attack strategy dựa vào độ khó
    private IBotAttackStrategy createAttackStrategy(String difficulty) {
        // TODO: Implement logic tạo attack strategy dựa vào độ khó
        // Ví dụ:
        switch (difficulty.toLowerCase()) {
            case "easy":
                return new EasyBotAtkStrategy();
            case "medium":
                return new MediumBotAtkStrategy();
            case "hard":
                return new HardBotAtkStrategy();
            default:
                return new EasyBotAtkStrategy();
        }
    }

    // Getter & Setter
    public void setAttackStrategy(IBotAttackStrategy attackStrategy) {
        this.attackStrategy = attackStrategy;
    }

    public IBotAttackStrategy getAttackStrategy() {
        return attackStrategy;
    }

    public String getDifficulty() {
        return difficulty;
    }

    public void setDifficulty(String difficulty) {
        this.difficulty = difficulty;
        // Cập nhật attack strategy khi thay đổi độ khó
        this.attackStrategy = createAttackStrategy(difficulty);
    }
    
    // Các phương thức khác giữ nguyên
    public int[] chooseAttack(Board opponentBoard) {
        if (attackStrategy == null) {
            throw new IllegalStateException("Bot attack strategy is not set!");
        }
        return attackStrategy.chooseAttack(opponentBoard);
    }

    @Override
    public boolean canUseAttackType(AttackType type) {
        return atkInv.hasAttack(type);
    }

    @Override
    public boolean canAttack(int x, int y, AttackType type) {
        return canUseAttackType(type);
    }
}