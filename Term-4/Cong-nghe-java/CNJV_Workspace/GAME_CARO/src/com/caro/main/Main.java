package com.caro.main;

import com.caro.controller.CaroController;

public class Main {
	public static void main(String[] args) {
		CaroController game = new CaroController(10); // 5x5 board
		game.init();
		game.launch();
		game.close();
	}
}
