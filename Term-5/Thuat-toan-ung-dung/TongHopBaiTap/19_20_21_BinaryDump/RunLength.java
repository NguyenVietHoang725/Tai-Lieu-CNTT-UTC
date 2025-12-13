import java.io.*;

/**
 * Lớp RunLength thực hiện nén và giải nén dữ liệu nhị phân sử dụng thuật toán RLE.
 * Phiên bản này sử dụng BinaryIn và BinaryOut để đọc/ghi file trực tiếp, 
 * ổn định hơn việc điều hướng System.in.
 */
public class RunLength {
    private static final int R    = 256; // Số lượng giá trị đếm tối đa (2^8)
    private static final int LG_R = 8;   // Số bit dùng để mã hóa độ dài chạy

    private RunLength() { }

    /**
     * Hàm nén (Compress)
     * @param inputFilename Tên file đầu vào (ví dụ: q32x48.bin)
     * @param outputFilename Tên file đầu ra (ví dụ: compressed.txt)
     */
    public static void compress(String inputFilename, String outputFilename) {
        // Khởi tạo luồng đọc từ file và luồng ghi ra file
        BinaryIn in = new BinaryIn(inputFilename);
        BinaryOut out = new BinaryOut(outputFilename);
        
        char run = 0; 
        boolean old = false; 
        int totalBits = 0;

        System.out.println("   [LOG] Đang nén...");

        // Đọc cho đến khi hết file
        while (!in.isEmpty()) {
            boolean b = in.readBoolean();
            totalBits++;
            
            if (b != old) {
                out.write(run, LG_R);
                run = 1;
                old = !old;
            } else {
                if (run == R - 1) { 
                    out.write(run, LG_R);
                    run = 0;
                    out.write(run, LG_R);
                }
                run++;
            }
        }
        // Ghi chuỗi cuối cùng
        out.write(run, LG_R);
        
        // Đóng luồng (QUAN TRỌNG: Không có dòng này file sẽ rỗng)
        out.close(); 
        
        System.out.println("   [LOG] Đã đọc " + totalBits + " bits.");
        System.out.println("   [LOG] Đã ghi file: " + outputFilename);
    }

    /**
     * Hàm giải nén (Expand)
     */
    public static void expand(String inputFilename, String outputFilename) {
        BinaryIn in = new BinaryIn(inputFilename);
        BinaryOut out = new BinaryOut(outputFilename);
        
        boolean b = false; 
        System.out.println("   [LOG] Đang giải nén...");

        while (!in.isEmpty()) {
            // Đọc 8 bit độ dài
            int run = in.readInt(LG_R); 
            
            // Ghi bit b lặp lại run lần
            for (int i = 0; i < run; i++) {
                out.write(b); 
            }
            b = !b; // Đảo bit
        }
        out.close();
        System.out.println("   [LOG] Giải nén hoàn tất: " + outputFilename);
    }

    /**
     * Hàm main
     */
    public static void main(String[] args) {
        if (args.length != 3) {
            System.out.println("Usage: java RunLength [-/+] inputFile outputFile");
            return;
        }

        String mode = args[0];       
        String inputFile = args[1];  
        String outputFile = args[2]; 

        // Kiểm tra file đầu vào có tồn tại không
        File file = new File(inputFile);
        if (!file.exists()) {
            System.out.println("Lỗi: Không tìm thấy file " + inputFile);
            return;
        }

        try {
            if (mode.equals("-")) {
                compress(inputFile, outputFile);
            } else if (mode.equals("+")) {
                expand(inputFile, outputFile);
            } else {
                System.out.println("Lỗi: Chế độ phải là '-' (nén) hoặc '+' (giải nén)");
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}