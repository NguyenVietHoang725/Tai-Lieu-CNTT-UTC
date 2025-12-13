package com.battleship.model.save;

import java.io.File;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import com.battleship.model.loader.ChallengeBoardLoader;

public class ChallengTemplateManager {
	private static final String DEFAULT_DIR = "resources/challenge_templates/default/";
    private static final String CUSTOM_DIR = "resources/challenge_templates/custom/";

    // Liệt kê danh sách template mặc định
    public List<String> listDefaultTemplates() {
        return listTemplatesInDir(DEFAULT_DIR);
    }

    // Liệt kê danh sách template custom
    public List<String> listCustomTemplates() {
        return listTemplatesInDir(CUSTOM_DIR);
    }

    // Tạo template mới (chỉ cho custom)
    public boolean createTemplate(String fileName, ChallengeBoardLoader loader, String content) throws IOException {
        File dir = new File(CUSTOM_DIR);
        if (!dir.exists()) {
			dir.mkdirs();
		}
        File file = new File(CUSTOM_DIR + fileName);
        if (file.exists())
		 {
			return false; // Không ghi đè
		}
        return writeContentToFile(file, content);
    }

    // Sửa template custom (ghi đè)
    public boolean updateTemplate(String fileName, String content) throws IOException {
        File file = new File(CUSTOM_DIR + fileName);
        if (!file.exists()) {
			return false;
		}
        return writeContentToFile(file, content);
    }

    // Xóa template custom
    public boolean deleteTemplate(String fileName) {
        File file = new File(CUSTOM_DIR + fileName);
        if (file.exists()) {
			return file.delete();
		}
        return false;
    }

    // Load template (dùng ChallengeBoardLoader)
    public ChallengeBoardLoader loadTemplate(String filePath) throws IOException {
        ChallengeBoardLoader loader = new ChallengeBoardLoader();
        loader.loadBoard(filePath);
        return loader;
    }

    // --- Hỗ trợ ---
    private List<String> listTemplatesInDir(String dirPath) {
        List<String> result = new ArrayList<>();
        File dir = new File(dirPath);
        if (dir.exists() && dir.isDirectory()) {
            for (File file : dir.listFiles()) {
                if (file.isFile() && file.getName().endsWith(".txt")) {
                    result.add(file.getName());
                }
            }
        }
        return result;
    }

    private boolean writeContentToFile(File file, String content) throws IOException {
        try (java.io.FileWriter writer = new java.io.FileWriter(file)) {
            writer.write(content);
        }
        return true;
    }
}
