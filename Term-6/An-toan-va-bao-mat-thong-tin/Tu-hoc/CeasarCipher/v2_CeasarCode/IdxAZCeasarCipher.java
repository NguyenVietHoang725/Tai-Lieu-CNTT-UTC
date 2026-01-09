public class IdxAZCeasarCipher
{
    private IndexAlphabet al;
    
    public IdxAZCeasarCipher(IndexAlphabet al) {
        this.al = al;
    }
    
    private char shiftChar(char c, int k) {
        int size = al.getSize();
        int curIdx = al.getIndexOf(c);
        
        if (curIdx == -1) {
            return c; 
        }
    
        int newIdx = ((curIdx + k) % size + size) % size;
        
        return al.getCharAt(newIdx);
    }
    
    public String encrypt(String plainText, int k) {
        StringBuilder sb = new StringBuilder();
        
        String upPlainText = plainText.toUpperCase();
        
        for (int i = 0; i < upPlainText.length(); i++) {
            char c = upPlainText.charAt(i);
            sb.append(shiftChar(c, k));
        }
        
        return sb.toString();
    }
    
    public String decrypt(String cipherText, int k) {
        return encrypt(cipherText, -k);
    }
    
    public void bruteForceDecrypt(String cipherText) {
        System.out.println("Giai ma xau " + cipherText + ":");
        for (int i = 1; i < al.getSize(); i++) {
            System.out.println("k = " + i + " :" + decrypt(cipherText, i));
        }
    }
    
    public static void main(String[] args) {
        IndexAlphabet al = new IndexAlphabet();
        IdxAZCeasarCipher cc = new IdxAZCeasarCipher(al);
        
        String plainText = "Viet Nam Trong Toi";
        int k = 5;
        
        String encrypted = cc.encrypt(plainText, k);
        System.out.println("Ban ro: " + plainText);
        System.out.println("Ban ma (k=" + k + "): " + encrypted);
        
        cc.bruteForceDecrypt(encrypted);
    }
}
