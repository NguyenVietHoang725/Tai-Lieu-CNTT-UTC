public class SuffixArrayXVN extends SuffixArrayX {
    // Chỉ cần gọi đúng Constructor của cha truyền vào bảng chữ cái VN
    public SuffixArrayXVN(String text) {
        super(text, VietnameseAlphabet.VIETNAMESE_ALPHABET); 
    }
}