import java.util.List;

public class TestTextProcessor {
    public static void main(String[] args) throws Exception {
        // Văn bản thử nghiệm
        String inputText = "Ông Nguyễn Khắc Chúc đang làm việc tại Đại học Quốc gia Hà Nội.";
        
        // Gọi TextProcessor để tách từ
        List<String> words = TextProcessor.segmentText(inputText);
        
        // In kết quả tách từ
        System.out.println("Kết quả tách từ:");
        for (int i = 0; i < words.size(); i++) {
            System.out.println((i + 1) + ". " + words.get(i));
        }
    }
}