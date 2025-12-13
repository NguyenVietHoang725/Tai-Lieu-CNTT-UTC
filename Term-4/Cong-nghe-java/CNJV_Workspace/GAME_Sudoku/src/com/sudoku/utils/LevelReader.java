package com.sudoku.utils;

import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;

public class LevelReader {
    private String filePath;
    private static final int SIZE = 9;

    public LevelReader(String filePath) {
        this.filePath = filePath;
    }

    public int[][] readBoard() {
        int[][] board = new int[SIZE][SIZE];
        int row = 0;
        

        try {
            BufferedReader reader = new BufferedReader(new FileReader(filePath));
            String line;

            while ((line = reader.readLine()) != null && row < SIZE) {
                String[] tokens = line.trim().split("\\s+");

                for (int col = 0; col < SIZE; col++) {
                    board[row][col] = Integer.parseInt(tokens[col]);
                }

                row++;
            }

            reader.close();
        } catch (IOException e) {
            System.out.println("Read file error: " + e.getMessage());
        }

        return board;
    }
}
