package com.battleship.view.utils;

import java.awt.Color;
import java.awt.Font;

import javax.swing.BorderFactory;
import javax.swing.border.Border;

public class ViewConstants {
	
	// Menu
    // Đường dẫn resource
    public static final String MAINMENU_BG_GIF_PATH = "/images/backgrounds/menu_background.gif";
    public static final String LOGO_PATH = "/images/logo/logo.png";
    public static final String CHALLENGE_BG_IMG_PATH = "/images/backgrounds/challenge_background.png";

    // Tên panel cho CardPanel
    public static final String MAIN_MENU = "mainMenu";
    public static final String CHALLENGE_PLAY = "challengePlay";
    public static final String VSBOT_MANAGE = "vsbotManage";
    public static final String VSBOT_PLAY = "vsbotPlay";
    public static final String RULE = "rule";
    
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
    public static final String FONT_PATH = "/fonts/groundhog.ttf";

    public static final String[] CHALLENGE_ATK_ON_BUTTON_IMAGES = {
        	"/images/buttons/challenge/challenge_single_atk_btn.png",
            "/images/buttons/challenge/challenge_cross_atk_btn.png",
            "/images/buttons/challenge/challenge_random_atk_btn.png",
            "/images/buttons/challenge/challenge_diamond_atk_btn.png"
        };
    
    public static final String[] CHALLENGE_ATK_HOVER_BUTTON_IMAGES = {
        	"/images/buttons/challenge/challenge_single_atk_hover_btn.png",
            "/images/buttons/challenge/challenge_cross_atk_hover_btn.png",
            "/images/buttons/challenge/challenge_random_atk_hover_btn.png",
            "/images/buttons/challenge/challenge_diamond_atk_hover_btn.png"
        };
    
    public static final String[] CHALLENGE_ATK_PRESSED_BUTTON_IMAGES = {
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
