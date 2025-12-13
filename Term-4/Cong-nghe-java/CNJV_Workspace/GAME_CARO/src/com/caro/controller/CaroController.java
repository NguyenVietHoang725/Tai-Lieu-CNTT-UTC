package com.caro.controller;

import com.caro.interfaces.Playable;
import com.caro.model.Game;
import com.caro.utils.InputHandler;
import com.caro.view.CaroView;

public class CaroController implements Playable {
    // Attributes
    private Game game;
    private CaroView view;
    private InputHandler input;
    private char currentPlayer;

    // Constructor
    public CaroController(int size) {
        game = new Game(size);
        view = new CaroView();
        input = new InputHandler();
        currentPlayer = 'X';
    }

    @Override
    public void init() {
        view.displayMessage("Welcome to Caro!");
    }

    @Override
    public void launch() {
        try {
            while (true) {
                view.displayBoard(game.getBoard());

                int x = -1, y = -1;
                boolean validMove = false;

                while (!validMove) {
                    try {
                        view.displayMessage("Enter your move (x, y) : ");
                        int[] move = input.getInput();
                        x = move[0];
                        y = move[1];

                        // Check for out-of-bound coordinates
                        if (x < 0 || x >= game.getBoard().length || y < 0 || y >= game.getBoard()[0].length) {
                            throw new IllegalArgumentException("Move out of bounds. Please enter valid coordinates.");
                        }

                        validMove = true;
                    } catch (IllegalArgumentException e) {
                        view.displayMessage("Error: " + e.getMessage());
                    } catch (Exception e) {
                        view.displayMessage("Invalid input. Please enter two integers separated by space.");
                        input.clear(); // Clear invalid input
                    }
                }

                if (game.makeMove(x, y, currentPlayer)) {
                    char winner = game.checkWin();
                    if (winner != ' ') {
                        view.displayBoard(game.getBoard());
                        view.displayMessage("Player " + winner + " wins!");
                        break;
                    }
                    currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
                } else {
                    view.displayMessage("Position already occupied. Try again.");
                }
            }
        } finally {
            close();
        }
    }

    @Override
    public void close() {
        input.close();
        view.displayMessage("Game Over!");
    }
}
