package com.battleship.controller;


import com.battleship.view.MainFrame;
import com.battleship.controller.menu.MenuController;
import com.battleship.controller.challenge.ChallengeController;
import com.battleship.controller.vsbot.VsBotController;
import com.battleship.controller.setting.SoundManager;
import com.battleship.interfaces.IBotAttackStrategy;
import com.battleship.model.logic.ChallengeModeLogic;
import com.battleship.model.attack.AttackInventory;
import com.battleship.model.board.Board;
import com.battleship.model.board.Node;
import com.battleship.model.botstrategy.EasyBotAtkStrategy;
import com.battleship.model.botstrategy.HardBotAtkStrategy;
import com.battleship.model.botstrategy.MediumBotAtkStrategy;
import com.battleship.model.loader.BotBoardLoader;
import com.battleship.model.loader.ChallengeBoardLoader;
import com.battleship.model.player.Bot;
import com.battleship.model.player.Player;
import com.battleship.model.ship.Ship;
import com.battleship.utils.AppConstants;
import com.battleship.utils.ResourceLoader;
import com.battleship.utils.ViewConstants;
import com.battleship.view.panels.challenge.ChallengePlayPanel;
import com.battleship.view.panels.vsbot.placement.VsBotShipPlacementPanel;
import com.battleship.view.panels.vsbot.play.VsBotPlayPanel;


import java.awt.Font;
import javax.swing.JOptionPane;


public class AppController {
    private MainFrame mainFrame;
    private MenuController menuController;
    private ChallengeController challengeController;
    private VsBotController vsBotController;
    private String currentBGM;


    public void start() {
        mainFrame = new MainFrame(this);
        SoundManager.preloadSounds();
        currentBGM = AppConstants.BGM_MAIN_MENU;
        SoundManager.playBGM(currentBGM);
        showMenu();
        mainFrame.setVisible(true);
    }


    public void showMenu() {
        if (menuController == null) {
            menuController = new MenuController(this, mainFrame);
            mainFrame.getCardPanel().setPanel(ViewConstants.MAIN_MENU, menuController.getMenuPanel());
        }
        // Luôn phát lại nhạc nền menu khi quay lại
        currentBGM = AppConstants.BGM_MAIN_MENU;
        SoundManager.playBGM(currentBGM);
        mainFrame.switchScreen(ViewConstants.MAIN_MENU);
    }


    public void startChallengeMode() {
        System.out.println("AppController: startChallengeMode");
        try {
            Font font = ResourceLoader.loadFont(ViewConstants.FONT_PATH, 16f);
            String[] challengeFiles = AppConstants.CHALLENGE_FILE_PATHS;
            String filePath = challengeFiles[new java.util.Random().nextInt(challengeFiles.length)];
            ChallengeBoardLoader loader = new ChallengeBoardLoader();
            Board board = loader.loadBoard(filePath);
            Player player = new Player("Player", board, null);
            ChallengeModeLogic gameLogic = new ChallengeModeLogic(player, loader);
            int leftPanelWidth = 500;
            int boardPanelSize = 780;
            int cellSize = 50;
            ChallengePlayPanel playPanel = new ChallengePlayPanel(font, leftPanelWidth, boardPanelSize, cellSize);
            challengeController = new ChallengeController(
                gameLogic,
                playPanel,
                playPanel.getInfoPanel(),
                playPanel.getBoardPanel(),
                this,
                cellSize
            );
            // Luôn phát lại nhạc nền challenge khi vào
            currentBGM = AppConstants.BGM_CHALLENGE;
            SoundManager.playBGM(currentBGM);
            mainFrame.getCardPanel().setPanel(ViewConstants.CHALLENGE_PLAY, playPanel);
            mainFrame.switchScreen(ViewConstants.CHALLENGE_PLAY);
        } catch (Exception e) {
            e.printStackTrace();
        }
    }


    public void startVsBotMode() {
        Font font = ResourceLoader.loadFont(ViewConstants.FONT_PATH, 16f);
        int cellSize1 = 42;
        int cellSize2 = 38;
        var placementPanel = new VsBotShipPlacementPanel(font, cellSize1, this);


        placementPanel.getInfoPanel().getConfirmButton().addActionListener(e -> {
            if (placementPanel.getBoardPanel().isAllShipsPlaced()) {
                try {
                    // Tạo bảng người chơi từ bảng đặt tàu
                    Board originalPlayerBoard = placementPanel.getBoardPanel().getBoard();
                    Board playerBoard = new Board();
                    for (int x = 0; x < 10; x++)
                        for (int y = 0; y < 10; y++)
                            playerBoard.getNode(x, y).setHasShip(originalPlayerBoard.getNode(x, y).isHasShip());
                    for (Ship ship : originalPlayerBoard.getShips()) {
                        int length = ship.getLength();
                        boolean isHorizontal = ship.isHorizontal();
                        Node firstNode = ship.getNodes().get(0);
                        int x = firstNode.getX();
                        int y = firstNode.getY();
                        playerBoard.addShip(x, y, length, isHorizontal);
                    }
                    // Lấy độ khó đã chọn
                    String diff = placementPanel.getInfoPanel().getSelectedDifficulty();
                    // Tạo chiến thuật tấn công cho bot dựa trên độ khó
                    IBotAttackStrategy botStrategy;
                    switch (diff) {
                        case "Easy":
                            botStrategy = new EasyBotAtkStrategy();
                            break;
                        case "Medium":
                            botStrategy = new MediumBotAtkStrategy();
                            break;
                        case "Hard":
                            botStrategy = new HardBotAtkStrategy();
                            break;
                        default:
                            botStrategy = new EasyBotAtkStrategy();
                    }
                    // Load bảng cho bot
                    Board botBoard = BotBoardLoader.loadRandomBoardFromResources(AppConstants.VSBOT_FILE_PATHS);
                    if (botBoard == null || botBoard.getShips().isEmpty()) {
                        throw new Exception("Failed to load bot board");
                    }
                    // Tạo inventory cho người chơi và bot
                    AttackInventory playerInventory = new AttackInventory();
                    AttackInventory botInventory = new AttackInventory();
                    // Tạo đối tượng người chơi và bot
                    var player = new Player("Player", playerBoard, playerInventory);
                    var bot = new Bot("Bot", botBoard, botInventory, botStrategy, diff);
                    bot.setDifficulty(diff);
                    // Tạo panel chơi game
                    var playPanel = new VsBotPlayPanel(font, cellSize2, diff);
                    // Cập nhật bảng người chơi và bot
                    playPanel.getPlayerBoardPanel().updatePlayerBoard(playerBoard);
                    playPanel.getBotBoardPanel().updateBotBoard(botBoard);
                    // Tạo controller cho chế độ chơi với bot
                    vsBotController = new VsBotController(player, bot, playPanel, this);
                    // Chuyển nhạc nền theo độ khó
                    startVsBotPlay(diff);
                    // Chuyển sang màn hình chơi game
                    mainFrame.getCardPanel().setPanel(ViewConstants.VSBOT_PLAY, playPanel);
                    mainFrame.switchScreen(ViewConstants.VSBOT_PLAY);
                } catch (Exception ex) {
                    ex.printStackTrace();
                    JOptionPane.showMessageDialog(
                        mainFrame,
                        "Error initializing game: " + ex.getMessage(),
                        "Error",
                        JOptionPane.ERROR_MESSAGE
                    );
                }
            } else {
                JOptionPane.showMessageDialog(
                    mainFrame,
                    "You haven't placed all ships!",
                    "Warning",
                    JOptionPane.WARNING_MESSAGE
                );
            }
        });


        // Luôn phát lại nhạc nền đặt tàu khi vào
        currentBGM = AppConstants.BGM_SHIP_PLACEMENT;
        SoundManager.playBGM(currentBGM);
        mainFrame.getCardPanel().setPanel(ViewConstants.VSBOT_SHIP_PLACEMENT, placementPanel);
        mainFrame.switchScreen(ViewConstants.VSBOT_SHIP_PLACEMENT);
    }


    private void startVsBotPlay(String difficulty) {
        String bgmPath = switch (difficulty.toLowerCase()) {
            case "medium" -> AppConstants.BGM_MEDIUM_BOT;
            case "hard" -> AppConstants.BGM_HARD_BOT;
            default -> AppConstants.BGM_EASY_BOT;
        };
        // Luôn phát lại nhạc nền khi chuyển độ khó
        currentBGM = bgmPath;
        SoundManager.playBGM(bgmPath);
    }


    public void replayVsBotMode() {
        startVsBotMode();
    }
}



