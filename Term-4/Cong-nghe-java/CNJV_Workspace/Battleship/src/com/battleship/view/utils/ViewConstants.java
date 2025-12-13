package com.battleship.view.utils;

import java.awt.Color;
import java.awt.Font;

import javax.swing.BorderFactory;
import javax.swing.border.Border;

public class ViewConstants {
	
	// Menu
    // Đường dẫn resource
    public static final String BG_GIF_PATH = "/images/backgrounds/menu_background.gif";
    public static final String LOGO_PATH = "/images/logo/logo.png";

    // Tên panel cho CardPanel
    public static final String MAIN_MENU = "mainMenu";
    public static final String CHALLENGE_MANAGE = "challengeManage";
    public static final String CHALLENGE_PLAY = "challengePlay";
    public static final String VSBOT_MANAGE = "vsbotManage";
    public static final String VSBOT_PLAY = "vsbotPlay";
    
    public static final String[] MENU_ON_BUTTON_IMAGES = {
    	"/images/buttons/menu/challenge_on_button.png",
        "/images/buttons/menu/vsbot_on_button.png",
        "/images/buttons/menu/rule_on_button.png",
        "/images/buttons/menu/setting_on_button.png",
        "/images/buttons/menu/quit_on_button.png"
    };
    
    public static final String[] MENU_HOVER_BUTTON_IMAGES = {
    	"/images/buttons/menu/challenge_hover_button.png",
        "/images/buttons/menu/vsbot_hover_button.png",
        "/images/buttons/menu/rule_hover_button.png",
        "/images/buttons/menu/setting_hover_button.png",
        "/images/buttons/menu/quit_hover_button.png"
    };
    
    public static final String[] MENU_OFF_BUTTON_IMAGES = {
        "/images/buttons/menu/challenge_off_button.png",
        "/images/buttons/menu/vsbot_off_button.png",
        "/images/buttons/menu/rule_off_button.png",
        "/images/buttons/menu/setting_off_button.png",
        "/images/buttons/menu/quit_off_button.png"
    };
    
    // Challenge Manage
    public static final String FONT_PATH = "/fonts/m6x11plus.ttf";
    public static final String[] CHALLENGE_HISTORY_BUTTON_IMAGES = {
    	"/images/buttons/challenge/history_container.png",
    	"/images/buttons/challenge/history_hover_container.png"
    };
    
    public static final String[] CHALLENGE_WIN_BUTTON_IMAGES = {
    	"/images/buttons/challenge/win_button.png",
    	"/images/buttons/challenge/win_hover_button.png"
    };
    public static final String[] CHALLENGE_LOSE_BUTTON_IMAGES = {
    	"/images/buttons/challenge/lose_button.png",
      	"/images/buttons/challenge/lose_hover_button.png"
    };
    
    //
    public static final String CHALLENGEBG_IMG_PATH = "/images/backgrounds/challenge-background.png";
    
    public static final String[] CHALLENGE_ON_BUTTON_IMAGES = {
        	"/images/buttons/challenge/challenge_single_atk_btn.png",
            "/images/buttons/challenge/challenge_cross_atk_btn.png",
            "/images/buttons/challenge/challenge_random_atk_btn.png",
            "/images/buttons/challenge/challenge_diamond_atk_btn.png"
        };
    
    public static final String[] CHALLENGE_HOVER_BUTTON_IMAGES = {
        	"/images/buttons/challenge/challenge_single_atk_hover_btn.png",
            "/images/buttons/challenge/challenge_cross_atk_hover_btn.png",
            "/images/buttons/challenge/challenge_random_atk_hover_btn.png",
            "/images/buttons/challenge/challenge_diamond_atk_hover_btn.png"
        };
    
    public static final String[] CHALLENGE_OFF_BUTTON_IMAGES = {
            "/images/buttons/challenge/challenge_single_atk_pressed_btn.png",
            "/images/buttons/challenge/challenge_cross_atk_pressed_btn.png",
            "/images/buttons/challenge/challenge_random_atk_pressed_btn.png",
            "/images/buttons/challenge/challenge_diamond_atk_pressed_btn.png"
        };
    
    public static final String CHALLENGE_CELL_NORMAL_IMG = "/images/buttons/challenge/challenge_cell.png";
    public static final String CHALLENGE_CELL_HOVER_IMG = "/images/buttons/challenge/challenge_cell_hover.png";
    public static final String CHALLENGE_CELL_HIT_IMG = "/images/buttons/challenge/challenge_cell_hit.png";
    public static final String CHALLENGE_CELL_MISS_IMG = "/images/buttons/challenge/challenge_cell_miss.png";
}
