/**
 * Lớp LSD (Least Significant Digit Radix Sort)
 * Dùng để sắp xếp các chuỗi có độ dài cố định hoặc số nguyên 32-bit.
 */
public class LSD {
    private static final int BITS_PER_BYTE = 8; // Mỗi byte có 8 bit

    // Constructor private để ngăn không cho khởi tạo đối tượng
    private LSD() { }

   /**
     * Sắp xếp mảng số nguyên 32-bit (int).
     * Đây là phần trọng tâm của yêu cầu "tìm hiểu sort số nguyên 32 bit".
     * * Cơ chế: Số int 32-bit được chia làm 4 byte (mỗi byte 8 bit).
     * Thuật toán sẽ sort 4 lần, từ byte thấp nhất (cuối cùng) đến byte cao nhất (đầu tiên).
     *
     * @param a Mảng số nguyên cần sắp xếp
     */
    public static void sort(int[] a) {
        final int BITS = 32;                 // Mỗi số int có 32 bit
        final int R = 1 << BITS_PER_BYTE;    // R = 2^8 = 256 (kích thước bảng mã cho 1 byte)
        final int MASK = R - 1;              // MASK = 0xFF (dùng để lấy 8 bit cuối)
        final int w = BITS / BITS_PER_BYTE;  // w = 4 (số lượng byte trong 1 số int)

        int n = a.length;
        int[] aux = new int[n]; // Mảng phụ để hỗ trợ sắp xếp

        // Vòng lặp chạy 4 lần (d=0, 1, 2, 3) ứng với 4 byte của số nguyên
        for (int d = 0; d < w; d++) {         

            // BƯỚC 1: Đếm tần suất (Frequency counts)
            int[] count = new int[R+1];
            for (int i = 0; i < n; i++) {           
                // Kỹ thuật Bitwise:
                // 1. a[i] >> (8*d): Dịch phải để đưa byte thứ d về vị trí cuối
                // 2. & MASK: Dùng phép AND với 0xFF để lấy giá trị byte đó (0-255)
                int c = (a[i] >> BITS_PER_BYTE*d) & MASK;
                count[c + 1]++;
            }

            // BƯỚC 2: Tính vị trí bắt đầu (Compute cumulates)
            for (int r = 0; r < R; r++)
                count[r+1] += count[r];

            // BƯỚC 3: Xử lý byte dấu (Sign bit) - Chỉ chạy ở vòng lặp cuối cùng (d=3)
            // Lý do: Số int trong Java có dấu. Byte cao nhất chứa bit dấu.
            // Nếu không xử lý, số âm sẽ bị xếp sai vị trí so với số dương.
            if (d == w-1) {
                int shift1 = count[R] - count[R/2];
                int shift2 = count[R/2];
                for (int r = 0; r < R/2; r++)
                    count[r] += shift1;
                for (int r = R/2; r < R; r++)
                    count[r] -= shift2;
            }

            // BƯỚC 4: Di chuyển dữ liệu sang mảng phụ (Move data)
            for (int i = 0; i < n; i++) {
                // Lấy lại giá trị byte thứ d để biết vị trí cần đặt
                int c = (a[i] >> BITS_PER_BYTE*d) & MASK;
                aux[count[c]++] = a[i];
            }

            // BƯỚC 5: Sao chép ngược lại mảng chính (Copy back)
            for (int i = 0; i < n; i++)
                a[i] = aux[i];
        }
    }

    /**
     * Sắp xếp mảng String độ dài cố định W.
     * (Giữ lại phương thức này phòng khi bạn muốn sort String như cũ)
     */
    public static void sort(String[] a, int w) {
        int n = a.length;
        int R = 256;   // extended ASCII
        String[] aux = new String[n];

        for (int d = w-1; d >= 0; d--) {
            int[] count = new int[R+1];
            for (int i = 0; i < n; i++)
                count[a[i].charAt(d) + 1]++;

            for (int r = 0; r < R; r++)
                count[r+1] += count[r];

            for (int i = 0; i < n; i++)
                aux[count[a[i].charAt(d)]++] = a[i];

            for (int i = 0; i < n; i++)
                a[i] = aux[i];
        }
    }
}