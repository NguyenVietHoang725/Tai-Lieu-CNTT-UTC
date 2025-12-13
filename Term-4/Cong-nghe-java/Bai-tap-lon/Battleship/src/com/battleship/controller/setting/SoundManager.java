package com.battleship.controller.setting;

import com.battleship.model.setting.SoundSettings;
import com.battleship.utils.AppConstants;
import com.battleship.utils.ResourceLoader;
import javax.sound.sampled.*;
import java.util.HashMap;
import java.util.Map;

public class SoundManager {
	private static Clip bgmClip;
	private static Clip sfxClip;
	private static final SoundSettings settings = new SoundSettings();
	private static final Map<String, Clip> bgmCache = new HashMap<>();
	private static final Map<String, Clip> sfxCache = new HashMap<>();
	private static String currentBgmPath;
	private static String currentSfxPath;

	public static void preloadSounds() {
		try {
			// Preload BGM
			loadBGM(AppConstants.BGM_MAIN_MENU);
			loadBGM(AppConstants.BGM_CHALLENGE);
			loadBGM(AppConstants.BGM_SHIP_PLACEMENT);
			loadBGM(AppConstants.BGM_EASY_BOT);
			loadBGM(AppConstants.BGM_MEDIUM_BOT);
			loadBGM(AppConstants.BGM_HARD_BOT);

			// Preload SFX
			loadSFX(AppConstants.SFX_SINGLE_ATK);
			loadSFX(AppConstants.SFX_CROSS_ATK);
			loadSFX(AppConstants.SFX_DIAMOND_ATK);
			loadSFX(AppConstants.SFX_RANDOM_ATK);
			loadSFX(AppConstants.SFX_VICTORY);
			loadSFX(AppConstants.SFX_GAMEOVER);
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	private static void loadBGM(String path) {
		try {
			Clip clip = ResourceLoader.loadAudioClip(path);
			bgmCache.put(path, clip);
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	private static void loadSFX(String path) {
		try {
			Clip clip = ResourceLoader.loadAudioClip(path);
			sfxCache.put(path, clip);
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public static void playBGM(String path) {
		stopBGM();
		if (!settings.isMusicEnabled())
			return;

		try {
			if (bgmCache.containsKey(path)) {
				bgmClip = bgmCache.get(path);
			} else {
				bgmClip = ResourceLoader.loadAudioClip(path);
				bgmCache.put(path, bgmClip);
			}

			bgmClip.setFramePosition(0);
			currentBgmPath = path;
			setVolume(bgmClip, settings.getMusicVolume());
			bgmClip.loop(Clip.LOOP_CONTINUOUSLY);
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public static void playSFX(String path) {
        // Dừng SFX hiện tại nếu đang phát
        stopSFX();
        if (!settings.isSfxEnabled()) return;


        try {
            if (sfxCache.containsKey(path)) {
                sfxClip = sfxCache.get(path);
            } else {
                sfxClip = ResourceLoader.loadAudioClip(path);
                sfxCache.put(path, sfxClip);
            }
           
            // Reset clip về đầu và phát lại
            sfxClip.setFramePosition(0);
            currentSfxPath = path;
            setVolume(sfxClip, settings.getSfxVolume());
            sfxClip.start();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

	public static void stopBGM() {
		if (bgmClip != null) {
			if (bgmClip.isRunning()) {
				bgmClip.stop();
			}
		}
	}

	public static void stopSFX() {
		if (sfxClip != null) {
			if (sfxClip.isRunning()) {
				sfxClip.stop();
			}
		}
	}

	public static void setVolume(Clip clip, float volume) {
		if (clip != null) {
			FloatControl gainControl = (FloatControl) clip.getControl(FloatControl.Type.MASTER_GAIN);
			float dB = (float) (Math.log(volume) / Math.log(10.0) * 20.0);
			gainControl.setValue(dB);
		}
	}

	public static void toggleMusic() {
		settings.toggleMusic();
		if (settings.isMusicEnabled()) {
			if (currentBgmPath != null) {
				playBGM(currentBgmPath);
			}
		} else {
			stopBGM();
		}
	}

	public static void setMusicVolume(float volume) {
		settings.setMusicVolume(volume);
		if (bgmClip != null) {
			setVolume(bgmClip, volume);
		}
	}

	public static float getMusicVolume() {
		return settings.getMusicVolume();
	}

	public static boolean isMusicEnabled() {
		return settings.isMusicEnabled();
	}

	public static SoundSettings getSettings() {
		return settings;
	}

	public static String getCurrentBgmPath() {
		return currentBgmPath;
	}

	public static boolean isBgmPlaying() {
		return bgmClip != null && bgmClip.isRunning();
	}

	public static void updateBgmVolume() {
		if (bgmClip != null) {
			setVolume(bgmClip, settings.getMusicVolume());
		}
	}

	public static void updateSfxVolume() {
		if (sfxClip != null) {
			setVolume(sfxClip, settings.getSfxVolume());
		}
	}
}
