package com.battleshipv1.main;

import com.battleshipv1.controller.GameController;
import com.battleshipv1.interfaces.IController;

public class Main {

    public static void main(String[] args) {
        IController controller = new GameController();

        controller.init();   // Khởi tạo game (tài nguyên, config,...)
        controller.start();  // Bắt đầu vòng lặp game
    }
}
