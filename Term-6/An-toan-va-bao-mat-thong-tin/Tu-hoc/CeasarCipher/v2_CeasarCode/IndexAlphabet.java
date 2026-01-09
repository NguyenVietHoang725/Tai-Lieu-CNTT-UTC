public class IndexAlphabet
{
    private String alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    
    public int getSize() {
        return alphabet.length();
    }
    
    public int getIndexOf(char c) {
        return alphabet.indexOf(c);
    }
    
    public char getCharAt(int index) {
        return alphabet.charAt(index);
    }
}
