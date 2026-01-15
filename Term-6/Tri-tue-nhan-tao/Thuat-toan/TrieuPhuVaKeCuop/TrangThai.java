import java.util.ArrayList;
import java.util.List;
import java.util.Objects;

public class TrangThai {
    public int a; 
    public int b;
    public boolean k; 
    
    // Biến lưu vết (Cha)
    public TrangThai father;

    public TrangThai(int a, int b, boolean k) {
        this.a = a;
        this.b = b;
        this.k = k;
        this.father = null;
    }

    // Kiểm tra tính hợp lệ
    public boolean validate() {
        if (a < 0 || a > 3 || b < 0 || b > 3) return false;
        
        // Bờ A
        if (a > 0 && a < b) return false;

        // Bờ B
        int aKhac = 3 - a;
        int bKhac = 3 - b;
        if (aKhac > 0 && aKhac < bKhac) return false;

        return true;
    }

    // Logic di chuyển
    private TrangThai diChuyen(int da, int db) {
        int dau = k ? -1 : 1;
        return new TrangThai(this.a + (da * dau), this.b + (db * dau), !this.k);
    }

    // Lấy danh sách các nước đi tiếp theo
    public List<TrangThai> layBuocDi() {
        List<TrangThai> kq = new ArrayList<>();
        TrangThai[] trangThais = new TrangThai[] {
            diChuyen(0, 1), diChuyen(1, 0), diChuyen(1, 1), diChuyen(0, 2), diChuyen(2, 0)
        };

        for (TrangThai tt : trangThais) {
            if (tt.validate()) {
                kq.add(tt);
            }
        }
        return kq;
    }

    // Ghi đè để so sánh (Bắt buộc cho việc tránh trùng lặp)
    @Override
    public boolean equals(Object obj) {
        if (this == obj) return true;
        if (obj == null || getClass() != obj.getClass()) return false;
        TrangThai other = (TrangThai) obj;
        return a == other.a && b == other.b && k == other.k;
    }

    @Override
    public int hashCode() {
        return Objects.hash(a, b, k);
    }

    @Override
    public String toString() {
        String bo = k ? "Bờ A" : "Bờ B";
        return String.format("[TP: %d, KC: %d, Thuyền: %s]", a, b, bo);
    }
}