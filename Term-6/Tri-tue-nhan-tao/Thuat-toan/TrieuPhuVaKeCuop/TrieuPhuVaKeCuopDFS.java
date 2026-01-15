public class TrieuPhuVaKeCuopDFS {
    public static void main(String[] args) {
        TrangThai batDau = new TrangThai(3, 3, true);
        
        ThuatToanDFS solver = new ThuatToanDFS();
        
        TrangThai ketQua = solver.timKiem(batDau);

        solver.inKetQua(ketQua);
    }
}