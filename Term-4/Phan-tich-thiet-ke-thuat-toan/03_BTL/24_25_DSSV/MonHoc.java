public class MonHoc implements Comparable<MonHoc> {
    private String tenMH;
    private int soTC;
    private String kyHoc; 

    public MonHoc(String tenMH, int soTC, String kyHoc) {
        this.tenMH = tenMH;
        this.soTC = soTC;
        this.kyHoc = kyHoc;
    }

    public String getTenMH() {
        return tenMH;
    }

    public int getSoTC() {
        return soTC;
    }

    public String getKyHoc() {
        return kyHoc;
    }

    @Override
    public String toString() {
        return tenMH + " (" + soTC + " TC, Kỳ " + kyHoc + ")";
    }

    @Override
    public int compareTo(MonHoc o) {
        int cmp = this.kyHoc.compareTo(o.kyHoc);
        if (cmp != 0) return cmp;
        cmp = this.tenMH.compareTo(o.tenMH);
        if (cmp != 0) return cmp;
        return Integer.compare(this.soTC, o.soTC);
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (!(o instanceof MonHoc)) return false;
        MonHoc mh = (MonHoc) o;
        return soTC == mh.soTC && tenMH.equals(mh.tenMH) && kyHoc.equals(mh.kyHoc);
    }

    @Override
    public int hashCode() {
        return tenMH.hashCode() * 31 + soTC * 17 + kyHoc.hashCode();
    }
}
