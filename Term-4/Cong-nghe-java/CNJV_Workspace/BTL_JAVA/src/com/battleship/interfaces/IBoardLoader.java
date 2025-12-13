package com.battleship.interfaces;

import java.io.IOException;

import com.battleship.model.board.Board;

/**
 * Interface biểu diễn tải bảng trò chơi từ file
 *
 * @author Nguyen Viet Hoang
 * @version 1.0
 * @since 2025-04-27
 */

public interface IBoardLoader {
	/**
	 * Tải bảng trò chơi từ file
	 *
	 * @param filePath Đường dẫn tới file chứa bảng trò chơi
	 * @return Bảng trò chơi đã tải
	 * @throws IOException Nếu xảy ra lỗi khi đọc file
	 */
	Board loadBoard(String filePath) throws IOException;
}
