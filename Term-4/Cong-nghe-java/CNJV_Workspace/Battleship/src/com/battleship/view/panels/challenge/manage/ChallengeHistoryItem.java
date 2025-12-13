package com.battleship.view.panels.challenge.manage;

// Sau này chuyển qua model

public class ChallengeHistoryItem {
	public String levelName;
	public int shotsUsed;
	public int shotsMax;
	public String timeUsed;
	public String timeMax;
	public boolean isWin;

	public ChallengeHistoryItem(String levelName, int shotsUsed, int shotsMax, String timeUsed, String timeMax,
			boolean isWin) {
		super();
		this.levelName = levelName;
		this.shotsUsed = shotsUsed;
		this.shotsMax = shotsMax;
		this.timeUsed = timeUsed;
		this.timeMax = timeMax;
		this.isWin = isWin;
	}

}
