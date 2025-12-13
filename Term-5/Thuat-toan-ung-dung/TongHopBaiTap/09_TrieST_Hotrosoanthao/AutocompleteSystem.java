import vn.pipeline.VnCoreNLP;
import java.util.List;
import java.util.Scanner;
import java.io.File;
import java.io.FileInputStream;
import org.apache.poi.xwpf.usermodel.XWPFDocument;
import org.apache.poi.xwpf.usermodel.XWPFParagraph;

/**
 * Bài 9: Hệ thống Autocomplete Tiếng Việt sử dụng TrieST.
 * * Chức năng:
 * 1. Đọc văn bản từ file .docx.
 * 2. Tách từ ngữ tiếng Việt (sử dụng VnCoreNLP).
 * 3. Xây dựng cây TrieST với bảng chữ cái Tiếng Việt.
 * 4. Hỗ trợ tra cứu gợi ý (Autocomplete) và thêm từ mới.
 */
public class AutocompleteSystem {

    public static void main(String[] args) throws Exception {
        // --- BƯỚC 1: Cấu hình bộ tách từ Tiếng Việt ---
        // Sử dụng VnCoreNLP để nhận diện từ ghép (vd: "Việt Nam" là 1 token, không phải 2)
        String[] annotators = {"wseg"}; 
        VnCoreNLP pipeline = new VnCoreNLP(annotators);

        // --- BƯỚC 2: Đọc dữ liệu từ file văn bản mẫu ---
        String filePath = "vanban.docx"; // File nguồn dữ liệu
        System.out.println("Đang đọc dữ liệu từ: " + filePath + "...");
        
        StringBuilder textBuilder = new StringBuilder();
        try (FileInputStream fis = new FileInputStream(new File(filePath));
             XWPFDocument document = new XWPFDocument(fis)) {
            for (XWPFParagraph para : document.getParagraphs()) {
                String text = para.getText().trim();
                if (!text.isEmpty()) {
                    textBuilder.append(text).append(" ");
                }
            }
        } catch (Exception e) {
            System.err.println("Lỗi đọc file: " + e.getMessage());
            return;
        }

        // Chuẩn hóa văn bản
        String inputText = textBuilder.toString().trim().replaceAll("\\s+", " ");
        
        // --- BƯỚC 3: Tách từ và Xây dựng Trie ---
        System.out.println("Đang tách từ và xây dựng cây Trie...");
        
        // Gọi TextProcessor để tách văn bản thành danh sách từ/cụm từ
        List<String> words = TextProcessor.segmentText(inputText);

        // Khởi tạo TrieST với bảng chữ cái Tiếng Việt (VietnameseAlphabet)
        // Value của Trie là Integer (lưu vị trí/thứ tự xuất hiện của từ)
        TrieST<Integer> trie = new TrieST<>(VietnameseAlphabet.VIETNAMESE_ALPHABET);
        
        // Nạp từ vào Trie
        for (int i = 0; i < words.size(); i++) {
            String word = words.get(i);
            // trie.put(key, value): key là từ, value là thứ tự i
            trie.put(word, i);
        }
        
        System.out.println("Đã nạp " + words.size() + " cụm từ vào hệ thống.");
        System.out.println("--------------------------------------------------");

        // --- BƯỚC 4: Vòng lặp tương tác (Autocomplete) ---
        Scanner scanner = new Scanner(System.in, "UTF-8");
        int nextPosition = words.size(); // Dùng để đánh số cho từ mới thêm vào

        while (true) {
            System.out.println("\n[HƯỚNG DẪN]:");
            System.out.println(" - Nhập prefix + ' ' (dấu cách) để xem gợi ý (Vd: 'cộng ')");
            System.out.println(" - Nhập từ mới + '/' để thêm vào từ điển (Vd: 'AI/')");
            System.out.println(" - Nhập rỗng (Enter) để thoát.");
            System.out.print(">> Nhập: ");
            
            String input = scanner.nextLine();
            if (input.isEmpty()) break;

            if (input.endsWith(" ")) {
                // === CHỨC NĂNG GỢI Ý (AUTOCOMPLETE) ===
                String prefix = input.trim();
                System.out.println("--> Các từ gợi ý cho '" + prefix + "':");
                
                // Sử dụng hàm keysWithPrefix của TrieST để tìm tất cả các từ bắt đầu bằng prefix
                Iterable<String> suggestions = trie.keysWithPrefix(prefix);
                
                int count = 0;
                for (String s : suggestions) {
                    // Lấy vị trí xuất hiện (Value)
                    Integer pos = trie.get(s); 
                    System.out.println("   " + (++count) + ". " + s + " (Index: " + pos + ")");
                }
                
                if (count == 0) {
                    System.out.println("   (Không tìm thấy từ nào)");
                }

            } else if (input.endsWith("/")) {
                // === CHỨC NĂNG THÊM TỪ MỚI ===
                String newWord = input.substring(0, input.length() - 1).trim();
                
                if (!trie.contains(newWord)) {
                    trie.put(newWord, nextPosition++);
                    System.out.println("--> Đã thêm từ mới: [" + newWord + "]");
                } else {
                    System.out.println("--> Từ [" + newWord + "] đã tồn tại trong hệ thống.");
                }
            } else {
                System.out.println("(!) Vui lòng kết thúc bằng dấu cách (để tìm) hoặc dấu / (để thêm).");
            }
        }
        scanner.close();
    }
}