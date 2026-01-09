
public class IndexAlphabet
{
    private String al = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    
    public int getSize() {
        return al.length();
    }
    
    public int getIndexOf(char c) {
        return al.indexOf(c);
    }
    
    public char getCharAt(int index) {
        return al.charAt(index);
    }
}
