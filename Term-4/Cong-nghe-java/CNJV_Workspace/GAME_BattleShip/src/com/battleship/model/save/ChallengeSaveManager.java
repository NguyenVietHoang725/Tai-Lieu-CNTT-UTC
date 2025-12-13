package com.battleship.model.save;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;

import com.battleship.model.board.Board;
import com.battleship.model.loader.ChallengeBoardLoader;
import com.battleship.model.ship.Ship;

public class ChallengeSaveManager {
    private static final int MAX_SLOTS = 3;
    private static final String SAVE_DIR = "resources/challenge_saves/";

    // Lưu trạng thái game vào slot
    public boolean saveToSlot(int slot, ChallengeBoardLoader loader, Board board) throws IOException {
        if (slot < 1 || slot > MAX_SLOTS) {
			return false;
		}
        File dir = new File(SAVE_DIR);
        if (!dir.exists()) {
			dir.mkdirs();
		}
        String filePath = SAVE_DIR + "slot" + slot + ".txt";
        try (FileWriter writer = new FileWriter(filePath)) {
            // Ghi metadata
            writer.write("@NAME\n" + loader.getName() + "\n");
            writer.write("@DIFFICULTY\n" + loader.getDifficulty() + "\n");
            writer.write("@DESCRIPTION\n" + loader.getDescription() + "\n");
            writer.write("@AUTHOR\n" + loader.getAuthor() + "\n");
            writer.write("@CREATED\n" + loader.getCreated().toLocalDate().toString() + "\n");

            // Ghi thông tin tàu
            writer.write("@SHIPS\n");
            for (Ship ship : board.getShips()) {
                writer.write(ship.toSaveString() + "\n");
            }

            // Ghi thiết lập game
            writer.write("@SETTINGS\n");
            writer.write("MAX_SHOTS=" + loader.getMaxShots() + "\n");
            int minutes = loader.getMaxTime() / 60;
            int seconds = loader.getMaxTime() % 60;
            writer.write("TIME_LIMIT=" + String.format("%d:%02d", minutes, seconds) + "\n");

            // Ghi kho đạn
            writer.write("@ATTACKS\n");
            writer.write("CROSS=" + loader.getCrossCount() + "\n");
            writer.write("RANDOM=" + loader.getRandomCount() + "\n");
            writer.write("DIAMOND=" + loader.getDiamondCount() + "\n");
        }
        return true;
    }

    // Trả về loader đã nạp metadata và gameplay
    public ChallengeBoardLoader loadFromSlot(int slot) throws IOException {
        if (slot < 1 || slot > MAX_SLOTS) {
            throw new IllegalArgumentException("Slot phải từ 1 đến " + MAX_SLOTS);
        }
        String filePath = SAVE_DIR + "slot" + slot + ".txt";
        File file = new File(filePath);
        if (!file.exists()) {
            throw new IOException("Không tìm thấy file save cho slot " + slot);
        }
        ChallengeBoardLoader loader = new ChallengeBoardLoader();
        loader.loadBoard(filePath);
        return loader;
    }

    // Trả về trực tiếp đối tượng Board đã nạp từ file save
    public Board loadBoardFromSlot(int slot) throws IOException {
        if (slot < 1 || slot > MAX_SLOTS) {
            throw new IllegalArgumentException("Slot phải từ 1 đến " + MAX_SLOTS);
        }
        String filePath = SAVE_DIR + "slot" + slot + ".txt";
        File file = new File(filePath);
        if (!file.exists()) {
            throw new IOException("Không tìm thấy file save cho slot " + slot);
        }
        ChallengeBoardLoader loader = new ChallengeBoardLoader();
        return loader.loadBoard(filePath);
    }

    public SaveInfo getSlotInfo(int slot) throws IOException {
        if (slot < 1 || slot > MAX_SLOTS) {
            throw new IllegalArgumentException("Slot phải từ 1 đến " + MAX_SLOTS);
        }
        String filePath = SAVE_DIR + "slot" + slot + ".txt";
        File file = new File(filePath);
        if (!file.exists()) {
            return null; // Slot trống
        }

        String name = "";
        String difficulty = "";
        String description = "";
        String author = "";
        String created = "";

        try (BufferedReader br = new BufferedReader(new FileReader(file))) {
            String line;
            String currentSection = "";
            while ((line = br.readLine()) != null) {
                line = line.trim();
                if (line.isEmpty() || line.startsWith("#")) {
					continue;
				}
                if (line.startsWith("@")) {
                    currentSection = line;
                    continue;
                }
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
                        created = line;
                        break;
                }
                // Đã đủ thông tin metadata thì có thể break sớm nếu muốn tối ưu
                if (!name.isEmpty() && !difficulty.isEmpty() && !description.isEmpty() && !author.isEmpty() && !created.isEmpty()) {
                    break;
                }
            }
        }
        // Nếu file không hợp lệ, trả về null hoặc SaveInfo rỗng
        if (name.isEmpty()) {
			return null;
		}
        return new SaveInfo(name, difficulty, description, author, created);
    }

    public boolean deleteSlot(int slot) {
        if (slot < 1 || slot > MAX_SLOTS) {
            throw new IllegalArgumentException("Slot phải từ 1 đến " + MAX_SLOTS);
        }
        String filePath = SAVE_DIR + "slot" + slot + ".txt";
        File file = new File(filePath);
        if (file.exists()) {
            return file.delete();
        }
        return false; // File không tồn tại
    }
}