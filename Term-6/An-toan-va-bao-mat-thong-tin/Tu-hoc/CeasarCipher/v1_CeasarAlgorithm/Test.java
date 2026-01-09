public class Test
{
    public static void main(String[] args) {
        IndexAlphabet al = new IndexAlphabet();
        CeasarAlgorithm cal = new CeasarAlgorithm(al);
        String e = "HaNoi";
        System.out.println("Xau ma hoa: " + cal.encrypt(e, 5));
        
        String d = "MFSTN";
        System.out.println("Xau tham ma: " + cal.decrypt(d, 5));
    }
}
