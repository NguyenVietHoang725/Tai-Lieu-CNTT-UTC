import vn.pipeline.Annotation;
import vn.pipeline.VnCoreNLP;
import vn.pipeline.Sentence;
import vn.pipeline.Word;
import java.util.ArrayList;
import java.util.List;

/**
 * Class để xử lý tách từ từ văn bản tiếng Việt sử dụng VnCoreNLP.
 */
public class TextProcessor {
    /**
     * Tách văn bản thành danh sách các cụm từ có nghĩa.
     * @param inputText Văn bản đầu vào.
     * @return Danh sách các cụm từ (ví dụ: "Việt_Nam").
     * @throws Exception Nếu có lỗi trong quá trình xử lý.
     */
    public static List<String> segmentText(String inputText) throws Exception {
        String[] annotators = {"wseg"}; // Chỉ sử dụng word segmentation
        VnCoreNLP pipeline = new VnCoreNLP(annotators);
        Annotation annotation = new Annotation(inputText);
        pipeline.annotate(annotation);
        
        List<String> words = new ArrayList<>();
        for (Sentence sentence : annotation.getSentences()) {
            for (Word word : sentence.getWords()) {
                words.add(word.getForm()); // Lấy từ/cụm đã tách (ví dụ: "cộng_hòa")
            }
        }
        return words;
    }
}