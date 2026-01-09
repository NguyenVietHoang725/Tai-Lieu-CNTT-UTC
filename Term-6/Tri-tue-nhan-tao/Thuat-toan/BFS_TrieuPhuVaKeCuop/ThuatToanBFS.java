import java.util.ArrayList;
import java.util.LinkedList;
import java.util.List;
import java.util.Queue;
import java.util.Stack;

public class ThuatToanBFS {
    
    // Hàm thực hiện tìm kiếm, trả về trạng thái đích nếu tìm thấy, null nếu thất bại
    public TrangThai timKiem(TrangThai trangThaiDau) {
        Queue<TrangThai> L = new LinkedList<>();
        L.add(trangThaiDau);

        List<TrangThai> daDuyet = new ArrayList<>();
        daDuyet.add(trangThaiDau);

        while (!L.isEmpty()) {
            TrangThai u = L.poll();

            // Kiểm tra đích (0, 0, false)
            if (u.a == 0 && u.b == 0 && u.k == false) {
                return u; // Trả về trạng thái đích (đã chứa link father)
            }

            // Duyệt các trạng thái con
            for (TrangThai v : u.layBuocDi()) {
                if (!daDuyet.contains(v)) {
                    v.father = u; // Lưu vết
                    L.add(v);
                    daDuyet.add(v);
                }
            }
        }
        return null; // Không tìm thấy đường đi
    }

    // Hàm hỗ trợ in đường đi
    public void inKetQua(TrangThai ketThuc) {
        if (ketThuc == null) {
            System.out.println("Không tìm thấy giải pháp!");
            return;
        }

        System.out.println("\n--> TÌM THẤY KẾT QUẢ!");
        Stack<TrangThai> duongDi = new Stack<>();
        TrangThai curr = ketThuc;

        while (curr != null) {
            duongDi.push(curr);
            curr = curr.father;
        }

        int buoc = 0;
        System.out.println("Tổng số bước: " + (duongDi.size() - 1));
        while (!duongDi.isEmpty()) {
            TrangThai t = duongDi.pop();
            System.out.println("Bước " + (buoc++) + ": " + t.toString());
        }
    }
}