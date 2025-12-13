package com.battleship.model.save;

public class SaveInfo {
	private String name;
    private String difficulty;
    private String description;
    private String author;
    private String created;

    public SaveInfo(String name, String difficulty, String description, String author, String created) {
        this.name = name;
        this.difficulty = difficulty;
        this.description = description;
        this.author = author;
        this.created = created;
    }

    // Getter
    public String getName() { return name; }
    public String getDifficulty() { return difficulty; }
    public String getDescription() { return description; }
    public String getAuthor() { return author; }
    public String getCreated() { return created; }
}
