package Game;

import java.util.*;

public class Battle_Ship {
	public static int SIZE = 10;
	public static int[][] gameBoard = new int[SIZE][SIZE];

	public static int nB = 5;
	public static int nF = 20;

	static void createBoard(int gameBoard[][], int nB) {
		for (int i = 0; i < SIZE; i++) {
			for (int j = 0; j < SIZE; j++) {
				gameBoard[i][j] = -2;
			}
		}

		Random rand = new Random();
		int added = 0;

		while (added < nB) {
			int x = rand.nextInt(9);
			int y = rand.nextInt(9);

			if (gameBoard[x][y] != -1) {
				gameBoard[x][y] = -1;
				added++;
			}
		}

	}

	static void show(int gameBoard[][], boolean isCheat) {
		for (int i = 0; i < SIZE; i++) {
			for (int j = 0; j < SIZE; j++) {
				if (isCheat) {
					System.out.print(gameBoard[i][j] + " ");
				} else {
					if (gameBoard[i][j] == -1 || gameBoard[i][j] == -2) {
						System.out.print("* ");
					} else {
						System.out.print(gameBoard[i][j] + " ");
					}
				}
			}
			System.out.print("\n");
		}
	}

	static void fight(int gameBoard[][], int x, int y) {
		if (gameBoard[x][y] == -1) {
			gameBoard[x][y] = 10;
			nB--;
		} else if (gameBoard[x][y] == -2) {
			gameBoard[x][y] = count(gameBoard, x, y);
		}
		nF--;
	}

	public static int count(int gameBoard[][], int x, int y) {
		int count = 0;
		int xlow = x > 0 ? x - 1 : 0;
		int xup = x < SIZE - 1 ? x + 1 : SIZE - 1;
		int ylow = y > 0 ? y - 1 : 0;
		int yup = y < SIZE - 1 ? y + 1 : SIZE - 1;

		for (int i = xlow; i <= xup; i++) {
			for (int j = ylow; j <= yup; j++) {
				if (gameBoard[i][j] == -1) {
					count++;
				}
			}
		}
		return count;
	}

	public static int getStatus(int nB, int nF) {
		if (nB == 0)
			return 1;
		if (nF == 0)
			return -1;
		return 0;
	}

	public static void play(int gameBoard[][]) {
		Scanner scn = new Scanner(System.in);
		while (getStatus(nB, nF) == 0) {
			int x, y;
			System.out.println("Moi ban nhap toa do (x, y) : ");
			x = scn.nextInt();
			y = scn.nextInt();
			fight(gameBoard, x, y);
			show(gameBoard, true);
			if (getStatus(nB, nF) == 1) {
				System.out.println("Ban da thang!");
				break;
			} else if (getStatus(nB, nF) == -1) {
				System.out.println("Ban da thua!");
				break;
			}
		}
		scn.close();
	}

	public static void main(String[] args) {
		createBoard(gameBoard, nB);

		show(gameBoard, true);

		play(gameBoard);
	}

}
