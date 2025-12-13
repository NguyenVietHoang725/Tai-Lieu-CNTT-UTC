import java.io.*;

/**
 * Công cụ hiển thị nội dung file dưới dạng nhị phân (0/1).
 * Dùng để kiểm tra, so sánh dữ liệu giữa các file.
 */
public class BinaryDump {

    private BinaryDump() { }

    /**
     * Hàm main thực hiện đọc file và in bit.
     * Tham số: [số_bit_trên_dòng] [tên_file]
     */
    public static void main(String[] args) throws IOException {
        int bitsPerLine = 16; // Mặc định 16 bit/dòng nếu không chỉ định
        String filename;

        if (args.length >= 2) {
            bitsPerLine = Integer.parseInt(args[0]);
            filename = args[1];
        } else if (args.length == 1) {
            filename = args[0];
        } else {
            System.out.println("Usage: java BinaryDump <bitsPerLine> <filename>");
            return;
        }

        // Đọc file từ đường dẫn
        File file = new File(filename);
        if (!file.exists()) {
            System.out.println("File không tồn tại: " + filename);
            return;
        }
        System.setIn(new FileInputStream(file));

        int count;
        // Đọc từng bit và in ra
        for (count = 0; !BinaryStdIn.isEmpty(); count++) {
            if (bitsPerLine != 0) {
                if (count != 0 && count % bitsPerLine == 0) StdOut.println();
            }
            if (BinaryStdIn.readBoolean()) StdOut.print(1);
            else                           StdOut.print(0);
        }
        
        if (bitsPerLine != 0) StdOut.println();
        System.out.println("\nTổng số bit: " + count + " bits");
    }
}