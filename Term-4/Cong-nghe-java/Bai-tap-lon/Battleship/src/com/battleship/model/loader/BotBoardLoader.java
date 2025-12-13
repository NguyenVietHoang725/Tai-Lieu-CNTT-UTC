package com.battleship.model.loader;

import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;

import com.battleship.interfaces.IBoardLoader;
import com.battleship.model.board.Board;

/**
 * Lớp "BotBoardLoader" biểu diễn tải bảng trò chơi từ file
 *
 * @author Nguyen Viet Hoang
 * @version 1.0
 * @since 2025-04-27
 */

public class BotBoardLoader implements IBoardLoader {

    /**
     * Hàm tải bảng trò chơi từ file
     *
     * @param filePath Đường dẫn tới file chứa bảng trò chơi
     * @return Bảng trò chơi đã tải
     * @throws IOException Nếu xảy ra lỗi khi đọc file
     */
	@Override
	public Board loadBoard(String filePath) throws IOException {
	    Board board = new Board();
	    try (BufferedReader br = new BufferedReader(new FileReader(filePath))) {
	        String line;
	        while ((line = br.readLine()) != null && !line.trim().isEmpty()) {
	            String[] parts = line.trim().split("\\s+");
	            if (parts.length < 4) continue;
	            int x = Integer.parseInt(parts[0]);
	            int y = Integer.parseInt(parts[1]);
	            int length = Integer.parseInt(parts[2]);
	            boolean isHorizontal = Boolean.parseBoolean(parts[3]);
	            board.addShip(x, y, length, isHorizontal);
	        }
	    }
	    return board;
	}
	
	public static Board loadRandomBoardFromResources(String[] resourcePaths) {
        String file = resourcePaths[new java.util.Random().nextInt(resourcePaths.length)];
        try {
            String filePath = BotBoardLoader.class.getResource(file).getPath();
            return new BotBoardLoader().loadBoard(filePath);
        } catch (Exception e) {
            e.printStackTrace();
            return new Board();
        }
    }
}
