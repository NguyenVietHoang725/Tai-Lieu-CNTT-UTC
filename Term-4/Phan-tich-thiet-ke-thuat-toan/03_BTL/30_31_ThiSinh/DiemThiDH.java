import edu.princeton.cs.algs4.SeparateChainingHashST;
import edu.princeton.cs.algs4.ST;

import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;

public class DiemThiDH {
    private SeparateChainingHashST<ThiSinh, ST<String, Double>> bangDiem = new SeparateChainingHashST<>();

    public void docFileCSV(String tenFile) {
        try (BufferedReader br = new BufferedReader(new FileReader(tenFile))) {
            String dong;
            while ((dong = br.readLine()) != null) {
                if (dong.trim().isEmpty()) continue;
                String[] tach = dong.split(",");

                String hoDem = tach[0].trim();
                String ten = tach[1].trim();
                String ngaySinh = tach[2].trim();
                double dtbPT = Double.parseDouble(tach[3].trim());

                ThiSinh ts = new ThiSinh(hoDem, ten, ngaySinh, dtbPT);

                ST<String, Double> diem = new ST<>();
                diem.put("Toán", Double.parseDouble(tach[4].trim()));
                diem.put("Lý", Double.parseDouble(tach[5].trim()));
                diem.put("Hóa", Double.parseDouble(tach[6].trim()));

                bangDiem.put(ts, diem);
            }
        } catch (IOException e) {
            System.out.println("Lỗi đọc file: " + e.getMessage());
        }
    }

    public void inBangDiem() {
        for (ThiSinh ts : bangDiem.keys()) {
            System.out.println("Thí sinh: " + ts);
            ST<String, Double> dsDiem = bangDiem.get(ts);
            for (String mon : dsDiem.keys()) {
                System.out.printf("   %s: %.2f\n", mon, dsDiem.get(mon));
            }
        }
    }

    public SeparateChainingHashST<ThiSinh, ST<String, Double>> getBangDiem() {
        return bangDiem;
    }
}
