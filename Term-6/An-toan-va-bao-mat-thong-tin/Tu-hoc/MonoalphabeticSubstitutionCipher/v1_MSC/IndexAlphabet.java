public class IndexAlphabet
{
    protected String alphabet;
    
    public IndexAlphabet() {
        alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";   
    }
    
    public int getSize() {
        return alphabet.length();
    }
    
    public int getIndexOf(char c) {
        return alphabet.indexOf(c);
    }
    
    public char getCharAt(int idx) {
        return alphabet.charAt(idx);
    }
    
    public void print() {
        System.out.println(alphabet);
    }
}
