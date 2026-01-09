public class CeasarAlgorithm
{
    private IndexAlphabet al;
    
    CeasarAlgorithm(IndexAlphabet al) {
        this.al = al;
    }
    
    public String encrypt(String plainText, int key) {
        String enString = "";
        String upPlainText = plainText.toUpperCase();
        for (int i = 0; i < plainText.length(); i++) {
            char c = upPlainText.charAt(i);
            c = shiftChar(c, key);
            enString += c;
        }
        return enString;
    }
    
    public String decrypt(String cipherText, int key) {
        String deString = "";
        for (int i = 0; i < cipherText.length(); i++) {
            char c = cipherText.charAt(i);
            c = shiftChar(c, (-1 * key));
            deString += c;
        }
        return deString;
    }
    
    private char shiftChar(char c, int k) {
        int curIdx = al.getIndexOf(c);
        int newIdx = (curIdx + k) % al.getSize();
        return al.getCharAt(newIdx);
    }
}
