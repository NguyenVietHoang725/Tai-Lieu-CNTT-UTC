import java.util.Random;
 
public class MonoAlphabet extends IndexAlphabet
{    
    public MonoAlphabet() {
        super();
        alphabet = shuffle(alphabet);
    }
    
    private String shuffle(String input) {
        char[] c = input.toCharArray();
        Random rand = new Random();
        
        for (int i = c.length - 1; i > 0; i--) {
            int j = rand.nextInt(i + 1);
            
            char temp = c[i];
            c[i] = c[j];
            c[j] = temp;
        }
        
        return new String(c);
    }
    
    public static void main(String[] args) {
        IndexAlphabet normal = new IndexAlphabet();
        MonoAlphabet mono = new MonoAlphabet();

        System.out.print("Bang goc: ");
        normal.print();

        System.out.print("Bang xao tron: ");
        mono.print();
    }
}
