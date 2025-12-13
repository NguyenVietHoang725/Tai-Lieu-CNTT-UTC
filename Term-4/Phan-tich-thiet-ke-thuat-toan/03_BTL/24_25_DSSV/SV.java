public class SV {
    private String maSV;
    private String hoDem;
    private String ten;
    private String ngaySinh;
    private String queQuan;
    private String lop;
    private BST<MonHoc, Double> bangDiem;

    // Constructor chính
    public SV(String maSV, String hoDem, String ten, String ngaySinh, String queQuan, String lop) {
        this.maSV = maSV;
        this.hoDem = hoDem;
        this.ten = ten;
        this.ngaySinh = ngaySinh;
        this.queQuan = queQuan;
        this.lop = lop;
        this.bangDiem = new BST<>();
    }

    public String getMaSV() {
        return maSV;
    }

    public String getTen() {
        return ten;
    }

    public String getHoTen() {
        return hoDem + " " + ten;
    }

    public String getLop() {
        return lop;
    }

    public void themDiem(MonHoc mh, double diem) {
        bangDiem.put(mh, diem);
    }

    public BST<MonHoc, Double> getBangDiem() {
        return bangDiem;
    }

    public double tinhDiemTrungBinhHocKy(String kyHoc) {
        double tongDiem = 0;
        int tongTC = 0;

        for (MonHoc mh : bangDiem.keys()) {
            if (mh.getKyHoc().equalsIgnoreCase(kyHoc)) {
                tongDiem += bangDiem.get(mh) * mh.getSoTC();
                tongTC += mh.getSoTC();
            }
        }

        return tongTC == 0 ? 0 : tongDiem / tongTC;
    }

    public double tinhDiemTrungBinh() {
        double tongDiem = 0;
        int tongTC = 0;

        for (MonHoc mh : bangDiem.keys()) {
            tongDiem += bangDiem.get(mh) * mh.getSoTC();
            tongTC += mh.getSoTC();
        }

        return tongTC == 0 ? 0 : tongDiem / tongTC;
    }

    @Override
    public String toString() {
        return String.format("%s - %s %s - %s - %s - Lớp: %s", maSV, hoDem, ten, ngaySinh, queQuan, lop);
    }
}
