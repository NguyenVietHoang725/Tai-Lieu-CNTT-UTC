package com.sudoku.controller;

import com.sudoku.interfaces.IController;
import com.sudoku.model.Game;
import com.sudoku.model.Move;
import com.sudoku.utils.InputHandler;
import com.sudoku.view.SudokuView;

import java.util.Set;

public class SudokuController implements IController {

    private Game model;
    private SudokuView view;
    private InputHandler input;

    public SudokuController(Game model, SudokuView view, InputHandler input) {
        this.model = model;
        this.view = view;
        this.input = input;
    }

    @Override
    public void init() {
        view.showMessage("Welcome to Sudoku!");
    }

    @Override
    public void launch() {
        while (!model.isSolved()) {
            view.displayBoard(model.getBoard());
            view.showSelectAction();

            int choice = input.getAction();

            switch (choice) {
                case 1 -> {
                    view.showMessage("Enter your move (x, y, val): ");
                    Move move = input.getMove();
                    if (input.validate(move) && model.validate(move)) {
                        model.updateValue(move);
                    } else {
                        view.showMessage("Invalid move. Try again!");
                    }
                }
                case 2 -> model.undo();
                case 3 -> model.redo();
                case 4 -> {
                    view.showMessage("Enter node (x, y) to get hint: ");
                    int[] coords = input.getHintInput();
                    Set<Integer> hint = model.hint(coords[0], coords[1]);
                    view.showMessage("Hint: " + hint);
                }
                case 5 -> {
                    view.showMessage("Exiting game...");
                    return; 
                }
                default -> view.showMessage("Invalid action. Try again!");
            }
        }

        view.displayBoard(model.getBoard());
        view.showMessage("Ping pong, YOU WIN!!!");
    }

    @Override
    public void pause() {
        view.showMessage("Game paused. Press any key to resume...");
    }

    @Override
    public void close() {
        view.showMessage("Closing resources...");
    }
}
