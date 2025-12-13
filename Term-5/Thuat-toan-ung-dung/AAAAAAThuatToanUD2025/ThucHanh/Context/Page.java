import java.util.ArrayList;
import java.util.List;

/**
 * Represents a generic page of data, identified by a unique key.
 *
 * @param <Key> The type of the key used to identify elements within the page.
 */
public class Page<Key> {

    private final Key pageKey; // The unique key identifying this page
    private final List<String> content; // Example content of the page

    /**
     * Constructs a new Page with a given key.
     *
     * @param pageKey The unique key for this page.
     */
    public Page(Key pageKey) {
        this.pageKey = pageKey;
        this.content = new ArrayList<>();
    }

    /**
     * Returns the unique key of this page.
     *
     * @return The page's key.
     */
    public Key getPageKey() {
        return pageKey;
    }

    /**
     * Adds a string element to the page's content.
     *
     * @param item The string item to add.
     */
    public void addContent(String item) {
        this.content.add(item);
    }

    /**
     * Returns the list of content items in this page.
     *
     * @return A List of strings representing the page's content.
     */
    public List<String> getContent() {
        return new ArrayList<>(content); // Return a copy to prevent external modification
    }

    @Override
    public String toString() {
        return "Page{" +
               "pageKey=" + pageKey +
               ", content=" + content +
               '}';
    }

    // Example usage in a main method (optional, for demonstration)
    public static void main(String[] args) {
        // Create a Page with an Integer key
        Page<Integer> intPage = new Page<>(1);
        intPage.addContent("First line of content.");
        intPage.addContent("Second line of content.");
        System.out.println(intPage);

        // Create a Page with a String key
        Page<String> stringPage = new Page<>("Introduction");
        stringPage.addContent("This is the introduction page.");
        System.out.println(stringPage);
    }
}