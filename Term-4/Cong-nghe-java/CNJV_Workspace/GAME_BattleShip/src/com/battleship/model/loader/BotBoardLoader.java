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
        try (BufferedReader br = new BufferedReader(new FileReader(filePath))) { // Tạo bộ đệm để đọc file
            String line;
            // Đọc thông tin tàu
            while ((line = br.readLine()) != null && !line.trim().isEmpty()) { // Đọc từng dòng
                String[] parts = line.trim().split("\\s+"); // Tách dòng thành các phần
                if (parts.length < 4)
				 {
					continue; // Nếu số phần không đủ, bỏ qua
				}
                int x = Integer.parseInt(parts[0]); // Tọa độ x
                int y = Integer.parseInt(parts[1]); // Tọa độ y
                int length = Integer.parseInt(parts[2]); // Độ dài tàu
                boolean isHorizontal = Boolean.parseBoolean(parts[3]); // Xác định tàu nằm ngang hay dọc
                board.addShip(x, y, length, isHorizontal); // Thêm tàu vào bảng
            }
        }
        return board; // Trả về bảng trò chơi đã tải
    }

}
