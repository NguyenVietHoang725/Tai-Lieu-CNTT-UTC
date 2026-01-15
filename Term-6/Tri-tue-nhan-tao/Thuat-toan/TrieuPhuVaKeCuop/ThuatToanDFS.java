import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.Stack;

public class ThuatToanDFS {

    public TrangThai timKiem(TrangThai trangThaiDau) {
        Stack<TrangThai> L = new Stack<>();
        L.push(trangThaiDau);

        List<TrangThai> Q = new ArrayList<>();
        Q.add(trangThaiDau);

        while (!L.isEmpty()) {
            TrangThai u = L.pop();

            if (u.a == 0 && u.b == 0 && u.k == false) {
                return u; 
            }

            List<TrangThai> listCon = u.layBuocDi();
            
            Collections.reverse(listCon);

            for (TrangThai v : listCon) {
                if (!Q.contains(v)) {
                    v.father = u; 
                    L.push(v);   
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