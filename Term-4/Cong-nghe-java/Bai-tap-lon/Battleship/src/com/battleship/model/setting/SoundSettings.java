package com.battleship.model.setting;

public class SoundSettings {
    private boolean musicEnabled = true;
    private boolean sfxEnabled = true;
    private float musicVolume = 1.0f;
    private float sfxVolume = 1.0f;

    public boolean isMusicEnabled() {
        return musicEnabled;
    }

    public void setMusicEnabled(boolean musicEnabled) {
        this.musicEnabled = musicEnabled;
    }

    public boolean isSfxEnabled() {
        return sfxEnabled;
    }

    public void setSfxEnabled(boolean sfxEnabled) {
        this.sfxEnabled = sfxEnabled;
    }

    public float getMusicVolume() {
        return musicVolume;
    }

    public void setMusicVolume(float musicVolume) {
        this.musicVolume = clamp(musicVolume);
    }

    public float getSfxVolume() {
        return sfxVolume;
    }

    public void setSfxVolume(float sfxVolume) {
        this.sfxVolume = clamp(sfxVolume);
    }

    private float clamp(float value) {
        return Math.max(0f, Math.min(1f, value));
    }

	public void toggleMusic() {
		this.musicEnabled = !this.musicEnabled;
	}
}
