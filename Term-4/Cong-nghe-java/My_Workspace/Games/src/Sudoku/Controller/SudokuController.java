package Sudoku.Controller;

import Sudoku.Model.Game;
import Sudoku.View.SudokuView;

public class SudokuController {
    private Game model;
    private SudokuView view;

    public SudokuController(Game model, SudokuView view) {
        this.model = model;
        this.view = view;
    }

    public void play() {
        while (!model.isSolved()) {
            view.displayBoard(model.getBoard());
            int[] move = view.getMove();

            int row = move[0];
            int col = move[1];
            int num = move[2];

            if (model.isValidMove(row, col, num)) {
                model.makeMove(row, col, num);
            } else {
                view.showMessage("Invalid move!. Enter again.");
            }
        }

        view.displayBoard(model.getBoard());
        view.showMessage("Ping pong, YOU WIN!!!");
    }
}
