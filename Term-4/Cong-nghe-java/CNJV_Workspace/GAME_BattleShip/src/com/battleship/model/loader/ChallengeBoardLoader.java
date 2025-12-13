package com.battleship.model.loader;

import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.List;

import com.battleship.interfaces.IBoardLoader;
import com.battleship.model.board.Board;

public class ChallengeBoardLoader implements IBoardLoader {
    // --- THUỘC TÍNH ---
    private static final DateTimeFormatter DATE_FORMATTER = DateTimeFormatter.ofPattern("yyyy-MM-dd");

    // Metadata
    private String name;
    private String difficulty;
    private String description;
    private String author;
    private LocalDateTime created;

    // Game Settings
    private int maxShots;
    private int maxTime;
    private int crossCount;
    private int randomCount;
    private int diamondCount;

    // --- GETTER & SETTER ---
    public String getName() { return name; }
    public String getDifficulty() { return difficulty; }
    public String getDescription() { return description; }
    public String getAuthor() { return author; }
    public LocalDateTime getCreated() { return created; }
    public int getMaxShots() { return maxShots; }
    public int getMaxTime() { return maxTime; }
    public int getCrossCount() { return crossCount; }
    public int getRandomCount() { return randomCount; }
    public int getDiamondCount() { return diamondCount; }

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
        List<String> lines = new ArrayList<>();

        try (BufferedReader br = new BufferedReader(new FileReader(filePath))) {
            String line;
            String currentSection = "";

            while ((line = br.readLine()) != null) {
                line = line.trim();
                if (line.isEmpty() || line.startsWith("#")) {
					continue;
				}

                // Xác định section đang đọc
                if (line.startsWith("@")) {
                    currentSection = line;
                    continue;
                }

                // Xử lý theo section
                switch (currentSection) {
                    case "@NAME":
                        name = line;
                        break;
                    case "@DIFFICULTY":
                        difficulty = line;
                        break;
                    case "@DESCRIPTION":
                        description = line;
                        break;
                    case "@AUTHOR":
                        author = line;
                        break;
                    case "@CREATED":
                        created = LocalDateTime.parse(line, DATE_FORMATTER);
                        break;
                    case "@SHIPS":
                        if (!line.startsWith("#")) {
                            lines.add(line);
                        }
                        break;
                    case "@SETTINGS":
                        if (line.startsWith("MAX_SHOTS=")) {
                            maxShots = Integer.parseInt(line.split("=")[1]);
                        } else if (line.startsWith("TIME_LIMIT=")) {
                            String[] timeParts = line.split("=")[1].split(":");
                            maxTime = Integer.parseInt(timeParts[0]) * 60 +
                                    Integer.parseInt(timeParts[1]);
                        }
                        break;
                    case "@ATTACKS":
                        if (line.startsWith("CROSS=")) {
                            crossCount = Integer.parseInt(line.split("=")[1]);
                        } else if (line.startsWith("RANDOM=")) {
                            randomCount = Integer.parseInt(line.split("=")[1]);
                        } else if (line.startsWith("DIAMOND=")) {
                            diamondCount = Integer.parseInt(line.split("=")[1]);
                        }
                        break;
                }
            }
        }

        // Xử lý thông tin tàu
        for (String shipLine : lines) {
            String[] parts = shipLine.split("\\s+");
            if (parts.length == 4) {
                int x = Integer.parseInt(parts[0]);
                int y = Integer.parseInt(parts[1]);
                int length = Integer.parseInt(parts[2]);
                boolean isHorizontal = Boolean.parseBoolean(parts[3]);
                board.addShip(x, y, length, isHorizontal);
            }
        }

        return board;
    }

    /**
     * Kiểm tra tính hợp lệ của dữ liệu đã load
     *
     * @return true nếu dữ liệu hợp lệ, false nếu không
     */
    public boolean isValid() {
        return name != null && !name.isEmpty() &&
               difficulty != null && !difficulty.isEmpty() &&
               maxShots > 0 &&
               maxTime > 0 &&
               crossCount >= 0 &&
               randomCount >= 0 &&
               diamondCount >= 0;
    }
}