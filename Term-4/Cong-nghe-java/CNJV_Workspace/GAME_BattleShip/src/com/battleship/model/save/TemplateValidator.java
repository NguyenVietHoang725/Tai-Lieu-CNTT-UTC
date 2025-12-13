package com.battleship.model.save;

import java.util.HashSet;
import java.util.List;
import java.util.Set;

import com.battleship.model.ship.Ship;

public class TemplateValidator {
	public static boolean validateMetadata(String name, String difficulty, String author, String created) {
        return name != null && !name.isEmpty()
            && difficulty != null && !difficulty.isEmpty()
            && author != null && !author.isEmpty()
            && created != null && !created.isEmpty();
    }

    public static boolean validateGameSettings(int maxShots, int maxTime, int cross, int random, int diamond) {
        return maxShots > 0 && maxTime > 0 && cross >= 0 && random >= 0 && diamond >= 0;
    }

    public static boolean validateShips(List<Ship> ships, int boardSize) {
        Set<String> occupied = new HashSet<>();
        for (Ship ship : ships) {
            for (int i = 0; i < ship.getLength(); i++) {
                int x = ship.isHorizontal() ? ship.getNodes().get(0).getX() : ship.getNodes().get(0).getX() + i;
                int y = ship.isHorizontal() ? ship.getNodes().get(0).getY() + i : ship.getNodes().get(0).getY();
                if (x < 0 || x >= boardSize || y < 0 || y >= boardSize) {
					return false;
				}
                String pos = x + "," + y;
                if (occupied.contains(pos))
				 {
					return false; // Trùng vị trí
				}
                occupied.add(pos);
            }
        }
        return true;
    }
}
