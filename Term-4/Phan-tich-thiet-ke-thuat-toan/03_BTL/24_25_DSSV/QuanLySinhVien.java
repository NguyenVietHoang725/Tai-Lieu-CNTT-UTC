import java.util.ArrayList;
import java.util.List;

public class QuanLySinhVien {

    private BST<String, SV> stMaSV;
    private BST<String, SV> stTen;
    private BST<String, List<SV>> dsLop;

    public QuanLySinhVien() {
        stMaSV = new BST<>();
        stTen = new BST<>();
        dsLop = new BST<>();
    }

    public void docTuCSV(String filename) {
        In in = new In(filename);
        while (in.hasNextLine()) {
            String line = in.readLine();
            String[] tokens = line.split(",");

            if (tokens.length < 7 || tokens[0].trim().equalsIgnoreCase("maSV")) {
                continue;
            }

            String maSV = tokens[0].trim();
            String hoDem = tokens[1].trim();
            String ten = tokens[2].trim();
            String ngaySinh = tokens[3].trim();
            String queQuan = tokens[4].trim();
            double diemTB = Double.parseDouble(tokens[5].trim());
            String lop = tokens[6].trim();

            SV sv = new SV(maSV, hoDem, ten, ngaySinh, queQuan, lop);
            stMaSV.put(maSV, sv);
            stTen.put(ten + "-" + maSV, sv);

            // Thêm SV vào danh sách lớp
            List<SV> danhSach = dsLop.get(lop);
            if (danhSach == null) {
                danhSach = new ArrayList<>();
                dsLop.put(lop, danhSach);
            }
            danhSach.add(sv);
        }
    }

    public void inTheoMaSV() {
        System.out.println("=== Danh sách sinh viên theo MaSV ===");
        for (String ma : stMaSV.keys()) {
            System.out.println(stMaSV.get(ma));
        }
    }

    public void inTheoTen() {
        System.out.println("=== Danh sách sinh viên theo Tên ===");
        for (String ten : stTen.keys()) {
            System.out.println(stTen.get(ten));
        }
    }

    public boolean putIfAbsent(String maSV, SV sv) {
        if (!stMaSV.contains(maSV)) {
            stMaSV.put(maSV, sv);
            stTen.put(sv.getTen() + "-" + maSV, sv);

            // Đồng thời cập nhật vào dsLop
            String lop = sv.getLop();
            List<SV> danhSach = dsLop.get(lop);
            if (danhSach == null) {
                danhSach = new ArrayList<>();
                dsLop.put(lop, danhSach);
            }
            danhSach.add(sv);

            return true;
        }
        return false;
    }

    public SV timSVTheoMa(String maSV) {
        return stMaSV.get(maSV);
    }

    public void themDiemChoSV(String maSV, MonHoc mh, double diem) {
        SV sv = stMaSV.get(maSV);
        if (sv != null) {
            sv.themDiem(mh, diem);
        } else {
            System.out.println("Không tìm thấy sinh viên với mã: " + maSV);
        }
    }

    public void inBangDiem(String maSV) {
        SV sv = stMaSV.get(maSV);
        if (sv == null) {
            System.out.println("Không tìm thấy sinh viên.");
            return;
        }
        System.out.println("=== Bảng điểm của " + sv.getHoTen() + " ===");
        for (MonHoc mh : sv.getBangDiem().keys()) {
            double diem = sv.getBangDiem().get(mh);
            System.out.println(mh + ": " + diem);
        }
        System.out.printf("Điểm TB: %.2f\n", sv.tinhDiemTrungBinh());
    }

    public void docDiemTuFile(String filename) {
        String name = filename.substring(0, filename.lastIndexOf('.')); // Bỏ .csv
        String[] parts = name.split("_");

        if (parts.length < 2) {
            System.out.println("Tên file không đúng định dạng (thiếu _): " + filename);
            return;
        }

        String tenVaSoTC = parts[0];        // Ví dụ: Toan3
        String kyHoc = parts[1];            // Ví dụ: HK1-2024

        // Tách tên môn và số tín chỉ
        String tenMH = tenVaSoTC.replaceAll("\\d+$", "");
        String soTCStr = tenVaSoTC.replaceAll("\\D+", "");

        if (tenMH.isEmpty() || soTCStr.isEmpty()) {
            System.out.println("Lỗi tên file (tên môn hoặc số TC rỗng): " + filename);
            return;
        }

        int soTC = Integer.parseInt(soTCStr);
        MonHoc mh = new MonHoc(tenMH, soTC, kyHoc);

        In in = new In(filename);
        while (in.hasNextLine()) {
            String line = in.readLine();
            String[] tokens = line.split(",");
            if (tokens.length < 2 || tokens[0].trim().equalsIgnoreCase("maSV")) continue;

            String maSV = tokens[0].trim();
            double diem = Double.parseDouble(tokens[1].trim());
            themDiemChoSV(maSV, mh, diem);
        }
    }

    public double tinhDiemTrungBinhMonHocTheoKy(String tenMonHoc, String kyHoc) {
        double tongDiem = 0;
        int dem = 0;

        for (String ma : stMaSV.keys()) {
            SV sv = stMaSV.get(ma);
            BST<MonHoc, Double> bd = sv.getBangDiem();
            for (MonHoc mh : bd.keys()) {
                if (mh.getTenMH().equalsIgnoreCase(tenMonHoc) && mh.getKyHoc().equalsIgnoreCase(kyHoc)) {
                    tongDiem += bd.get(mh);
                    dem++;
                }
            }
        }

        if (dem == 0) {
            System.out.println("Không có sinh viên nào học " + tenMonHoc + " trong kỳ " + kyHoc);
            return 0;
        }

        return tongDiem / dem;
    }

    public void tongKetHocKyTheoLop(String ky, String lop) {
        if (!dsLop.contains(lop)) {
            System.out.println("Không có lớp " + lop);
            return;
        }

        List<SV> danhSach = dsLop.get(lop);
        System.out.println("=== Tổng kết học kỳ " + ky + " - Lớp " + lop + " ===");

        for (SV sv : danhSach) {
            double tongDiem = 0;
            int tongTC = 0;

            for (MonHoc mh : sv.getBangDiem().keys()) {
                if (mh.getKyHoc().equalsIgnoreCase(ky)) {
                    double diem = sv.getBangDiem().get(mh);
                    tongDiem += diem * mh.getSoTC();
                    tongTC += mh.getSoTC();
                }
            }

            double dtb = tongTC > 0 ? tongDiem / tongTC : 0.0;
            System.out.printf("%s (%s): %.2f\n", sv.getHoTen(), sv.getMaSV(), dtb);
        }
    }

    public static void main(String[] args) {
        if (args.length < 2) {
            System.out.println("Cách dùng: java QuanLySinhVien <fileSinhVien.csv> <fileDiem1.csv> <fileDiem2.csv> ...");
            return;
        }
    
        QuanLySinhVien ql = new QuanLySinhVien();
        ql.docTuCSV(args[0]);
    
        for (int i = 1; i < args.length; i++) {
            ql.docDiemTuFile(args[i]);
        }
    
        ql.inTheoMaSV();
        System.out.println();
    
        ql.inTheoTen();
        System.out.println();
    
        String hocKy = "HK1-2024";
        String lop = "IT3";
    
        System.out.printf("Tổng kết học kỳ %s lớp %s:\n", hocKy, lop);
        ql.tongKetHocKyTheoLop(hocKy, lop);
    
        // Ví dụ tìm SV theo mã và in bảng điểm
        String maTim = "SV999";
        SV svTimDuoc = ql.timSVTheoMa(maTim);
        if (svTimDuoc != null) {
            System.out.println("Tìm thấy: " + svTimDuoc);
            ql.inBangDiem(maTim);
        } else {
            System.out.println("Không tìm thấy mã: " + maTim);
        }
    
        double tb = ql.tinhDiemTrungBinhMonHocTheoKy("Toan", hocKy);
        System.out.printf("Điểm trung bình môn Toan trong học kỳ %s: %.2f\n", hocKy, tb);
    }

}
