
public class TrieuPhuVaKeCuop
{
    public static void main(String[] args) {
        // 1. Cấu hình bài toán
        TrangThai batDau = new TrangThai(3, 3, true);
        
        // 2. Khởi tạo bộ giải (Thuật toán)
        ThuatToanBFS solver = new ThuatToanBFS();
        
        System.out.println("Đang bắt đầu tìm kiếm...");
        
        // 3. Thực thi tìm kiếm
        TrangThai ketQua = solver.timKiem(batDau);
        
        // 4. In kết quả
        solver.inKetQua(ketQua);
    }
}
