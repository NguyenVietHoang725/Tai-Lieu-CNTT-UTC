public class MonoAZSubCipher
{
    private MonoAlphabet mal;
    
    public MonoAZSubCipher(MonoAlphabet mal) {
        this.mal = mal;
    }
    
    private char shiftChar(char c, int k) {
        int size = mal.getSize();
        int curIdx = mal.getIndexOf(c);
        
        if (curIdx == -1) {
            return c;
        }
        
        int newIdx = ((curIdx + k) % size + size) % size;
        
        return mal.getCharAt(newIdx);
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
    
    public static void main(String[] args) {
        MonoAlphabet mal = new MonoAlphabet();
        MonoAZSubCipher ms = new MonoAZSubCipher(mal);
        
        String plainText = "Viet Nam Trong Toi";
        int k = 5;
        
        String encrypted = ms.encrypt(plainText, k);
        System.out.println("Ban ro: " + plainText);
        System.out.println("Ban ma (k=" + k + "): " + encrypted);
    }
}
