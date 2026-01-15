import java.util.ArrayList;
import java.util.LinkedList;
import java.util.List;
import java.util.Queue;
import java.util.Stack;

public class ThuatToanBFS {
    
    public TrangThai timKiem(TrangThai trangThaiDau) {
        Queue<TrangThai> L = new LinkedList<>();
        L.add(trangThaiDau);

        List<TrangThai> Q = new ArrayList<>();
        Q.add(trangThaiDau);

        while (!L.isEmpty()) {
            TrangThai u = L.poll();

            // Kiểm tra đích (0, 0, false)
            if (u.a == 0 && u.b == 0 && u.k == false) {
                return u; 
            }

            // Duyệt các trạng thái con
            for (TrangThai v : u.layBuocDi()) {
                if (!Q.contains(v)) {
                    v.father = u; // Lưu vết
                    L.add(v);
                    Q.add(v);
                }
            }
        }
        return null; 
    }

    public void inKetQua(TrangThai ketThuc) {
        if (ketThuc == null) {
            System.out.println("Không tìm thấy giải pháp!");
            return;
        }

        System.out.println("\nTìm thấy kết quả:");
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