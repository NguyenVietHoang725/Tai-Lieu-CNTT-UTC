import java.util.Objects;

public class ThiSinh {
    private String hoDem;
    private String ten;
    private String ngaySinh;
    private double dtbChungPT;

    public ThiSinh(String hoDem, String ten, String ngaySinh, double dtbChungPT) {
        this.hoDem = hoDem;
        this.ten = ten;
        this.ngaySinh = ngaySinh;
        this.dtbChungPT = dtbChungPT;
    }

    public String getHoTen() {
        return hoDem + " " + ten;
    }

    public String getNgaySinh() {
        return ngaySinh;
    }

    public double getDtbChungPT() {
        return dtbChungPT;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (!(o instanceof ThiSinh)) return false;
        ThiSinh that = (ThiSinh) o;
        return Double.compare(that.dtbChungPT, dtbChungPT) == 0 &&
               hoDem.equals(that.hoDem) &&
               ten.equals(that.ten) &&
               ngaySinh.equals(that.ngaySinh);
    }

    @Override
    public int hashCode() {
        return Objects.hash(hoDem, ten, ngaySinh, dtbChungPT);
    }

    @Override
    public String toString() {
        return String.format("%s %s | %s | %.2f", hoDem, ten, ngaySinh, dtbChungPT);
    }
}
