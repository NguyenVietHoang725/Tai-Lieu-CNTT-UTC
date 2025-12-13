#include <iostream>
#include <string>
using namespace std;

class NgayThang {
private:
    int ngay, thang, nam;
public:
    NgayThang() { ngay = thang = nam = 0; }

    NgayThang(int d, int m, int y) {
        ngay = d; thang = m; nam = y;
    }

    void nhap() {
        cout << "Nhap ngay, thang, nam: ";
        cin >> ngay >> thang >> nam;
    }

    void xuat() const {
        cout << ngay << "/" << thang << "/" << nam;
    }
};

class Nguoi {
protected:
    string ten;
    NgayThang ngaySinh;
    string diaChi;
public:
    Nguoi() { ten = ""; diaChi = ""; }

    Nguoi(string ten, NgayThang ns, string dc) {
        this->ten = ten;
        this->ngaySinh = ns;
        this->diaChi = dc;
    }

    virtual void nhap() {
        cin.ignore(); 
        cout << "Nhap ho ten: ";
        getline(cin, ten);

        cout << "Nhap ngay sinh:\n";
        ngaySinh.nhap();

        cin.ignore(); 
        cout << "Nhap dia chi: ";
        getline(cin, diaChi);
    }

    virtual void xuat() const {
        cout << "Ho ten: " << ten << endl;
        cout << "Ngay sinh: ";
        ngaySinh.xuat();
        cout << "\nDia chi: " << diaChi << endl;
    }

    string getTen() const { return ten; }
};

class KhachHangTieuThuDien : public Nguoi {
private:
    string maKH;
    int chiSoTruoc, chiSoSau;

public:
    KhachHangTieuThuDien() : Nguoi() {
        maKH = "";
        chiSoTruoc = chiSoSau = 0;
    }

    void nhap() override {
        Nguoi::nhap();
        cout << "Nhap ma khach hang: ";
        getline(cin, maKH);
        cout << "Nhap chi so truoc: ";
        cin >> chiSoTruoc;
        cout << "Nhap chi so sau: ";
        cin >> chiSoSau;
    }

    void xuat() const override {
        cout << "\n=== Thong tin khach hang ===\n";
        Nguoi::xuat();
        cout << "Ma khach hang: " << maKH << endl;
        cout << "Chi so truoc: " << chiSoTruoc << endl;
        cout << "Chi so sau: " << chiSoSau << endl;
        cout << "So dien tieu thu: " << (chiSoSau - chiSoTruoc) << " kWh" << endl;
        cout << "Tien dien phai tra: " << tinhTienDien() << " dong\n";
    }

    float tinhTienDien() const {
        int soDien = chiSoSau - chiSoTruoc;
        float tien = 0;
        if (soDien <= 0) return 0;

        if (soDien <= 50)
            tien = soDien * 1.678;
        else if (soDien <= 100)
            tien = 50 * 1.678 + (soDien - 50) * 1.734;
        else if (soDien <= 200)
            tien = 50 * 1.678 + 50 * 1.734 + (soDien - 100) * 2.014;
        else if (soDien <= 300)
            tien = 50 * 1.678 + 50 * 1.734 + 100 * 2.014 + (soDien - 200) * 2.536;
        else
            tien = 50 * 1.678 + 50 * 1.734 + 100 * 2.014 + 100 * 2.536 + (soDien - 300) * 2.834;

        return tien;
    }

    string getMaKH() const { return maKH; }
    string getTen() const { return ten; }

    int getBacTieuThu() const {
        int soDien = chiSoSau - chiSoTruoc;
        if (soDien <= 50) return 1;
        else if (soDien <= 100) return 2;
        else if (soDien <= 200) return 3;
        else if (soDien <= 300) return 4;
        else return 5;
    }
};

int main() {
    int n;
    cout << "Nhap so luong khach hang: ";
    cin >> n;

    KhachHangTieuThuDien *ds = new KhachHangTieuThuDien[n];

    for (int i = 0; i < n; i++) {
        cout << "\nNhap thong tin khach hang thu " << i + 1 << ":\n";
        ds[i].nhap();
    }

    cin.ignore();
    string tenTim;
    cout << "\nNhap ten khach hang can tim: ";
    getline(cin, tenTim);

    bool timThay = false;
    for (int i = 0; i < n; i++) {
        if (ds[i].getTen() == tenTim) {
            cout << "\n==> Da tim thay khach hang:\n";
            ds[i].xuat();
            timThay = true;
        }
    }
    if (!timThay)
        cout << "Khong tim thay khach hang ten \"" << tenTim << "\"\n";

    int demBac[6] = {0};
    for (int i = 0; i < n; i++) {
        int bac = ds[i].getBacTieuThu();
        demBac[bac]++;
    }

    cout << "\n=== Thong ke khach hang theo bac tieu thu ===\n";
    for (int i = 1; i <= 5; i++) {
        cout << "Bac " << i << ": " << demBac[i] << " khach hang\n";
    }

    delete[] ds;
    return 0;
}
