
public class TrieuPhuVaKeCuopBFS
{
    public static void main(String[] args) {
        TrangThai batDau = new TrangThai(3, 3, true);
        
        ThuatToanBFS solver = new ThuatToanBFS();

        TrangThai ketQua = solver.timKiem(batDau);

        solver.inKetQua(ketQua);
    }
}
