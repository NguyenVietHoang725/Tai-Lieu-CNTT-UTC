package com.battleship.model.loader;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.List;
import com.battleship.interfaces.IBoardLoader;
import com.battleship.model.board.Board;
import com.battleship.model.ship.Ship;

public class ChallengeBoardLoader implements IBoardLoader {
    private int maxShots;
    private int crossCount;
    private int randomCount;
    private int diamondCount;
    private int maxTimeSeconds;

    public int getMaxShots() { return maxShots; }
    public int getCrossCount() { return crossCount; }
    public int getRandomCount() { return randomCount; }
    public int getDiamondCount() { return diamondCount; }
    public int getMaxTimeSeconds() { return maxTimeSeconds; }

    @Override
    public Board loadBoard(String filePath) throws IOException {
        Board board = new Board();
        try (InputStream is = getClass().getResourceAsStream(filePath);
             BufferedReader br = new BufferedReader(new InputStreamReader(is))) {
            if (is == null) throw new IOException("Resource not found: " + filePath);
            String line;
            int lineNum = 0;
            List<String> shipLines = new ArrayList<>();
            while ((line = br.readLine()) != null) {
                line = line.trim();
                if (line.isEmpty() || line.startsWith("#")) continue;
                lineNum++;
                if (lineNum == 1) continue; // Challenge mode (bỏ qua)
                if (lineNum >= 2 && lineNum <= 6) {
                    shipLines.add(line);
                } else if (lineNum == 7) {
                    maxShots = Integer.parseInt(line);
                } else if (lineNum == 8) {
                    String[] parts = line.split("\\s+");
                    crossCount = Integer.parseInt(parts[1]);
                } else if (lineNum == 9) {
                    String[] parts = line.split("\\s+");
                    randomCount = Integer.parseInt(parts[1]);
                } else if (lineNum == 10) {
                    String[] parts = line.split("\\s+");
                    diamondCount = Integer.parseInt(parts[1]);
                } else if (lineNum == 11) {
                    String[] timeParts = line.split(":");
                    int minutes = Integer.parseInt(timeParts[0]);
                    int seconds = Integer.parseInt(timeParts[1]);
                    maxTimeSeconds = minutes * 60 + seconds;
                }
            }
            
            for (String shipLine : shipLines) {
                String[] parts = shipLine.split("\\s+");
                if (parts.length == 4) {
                    int x = Integer.parseInt(parts[0]);
                    int y = Integer.parseInt(parts[1]);
                    int length = Integer.parseInt(parts[2]);
                    boolean isHorizontal = Boolean.parseBoolean(parts[3]);
                    board.addShip(x, y, length, isHorizontal);
                }
            }
        }
        
        System.out.println("Số tàu trên board: " + board.getShips().size());
        for (com.battleship.model.ship.Ship ship : board.getShips()) {
            System.out.println("Ship: " + ship.toSaveString());
            for (com.battleship.model.board.Node node : ship.getNodes()) {
                System.out.println("Node hit? " + node.isHit());
            }
        }
        
        return board;
    }
}