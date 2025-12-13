#include <iostream>
using namespace std;

class QLH {
private:
    string maHang;
    string tenHang;
    string xuatXu;
    int loaiHang;
    int soLuong;
public:
    QLH() {
        maHang = tenHang = xuatXu = "Empty";
        loaiHang = 0;
        soLuong = 0;
    }
    
    QLH(string mh, string th, string xx, int lh, int sl) {
        maHang = mh;
        tenHang = th;
        xuatXu = xx;
        loaiHang = lh;
        soLuong = sl;
    }
    
    void nhap() {
        cin.ignore();
        cout << "Nhap ma hang: ";
        getline(cin, maHang);
        cout << "Nhap ten hang: ";
        getline(cin, tenHang);
        cout << "Nhap xuat xu: ";
        getline(cin, xuatXu);
        cout << "Nhap loai hang (1/2/3): ";
        cin >> loaiHang;
        cout << "Nhap so luong: ";
        cin >> soLuong;
    }
    
    void xuat() {
        cout << "Ma hang: " << maHang << "\n";
        cout << "Ten hang: " << tenHang << "\n";
        cout << "Xuat xu: " << xuatXu << "\n";
        cout << "Loai hang: " << loaiHang << "\n";
        cout << "So luong: " << soLuong << "\n";
    }

    int getLoaiHang() const { return loaiHang; }
    int getSoLuong() const { return soLuong; }
    string getTenHang() const { return tenHang; }
};

class DIENTU : public QLH {
private:
    int tgbh;
public:
    DIENTU() : QLH() {
        tgbh = 0;
    }
    
    DIENTU(string mh, string th, string xx, int lh, int sl, int tg) 
        : QLH(mh, th, xx, lh, sl) {
        tgbh = tg;
    }
    
    void nhap() {
        QLH::nhap(); 
        cout << "Nhap thoi gian bao hanh (thang): ";
        cin >> tgbh;
    }

    void xuat() {
        QLH::xuat(); 
        cout << "Thoi gian bao hanh: " << tgbh << " thang\n";
    }

    int getTgbh() const { return tgbh; }
};

int main() {
    int n;
    cout << "Nhap so luong mat hang dien tu: ";
    cin >> n;

    DIENTU* ds = new DIENTU[n];
    for (int i = 0; i < n; i++) {
        cout << "\nNhap thong tin cho mat hang dien tu thu " << i + 1 << ":\n";
        ds[i].nhap();
    }

    int tongLoai1 = 0, tongLoai2 = 0, tongLoai3 = 0;
    for (int i = 0; i < n; i++) {
        if (ds[i].getLoaiHang() == 1) tongLoai1 += ds[i].getSoLuong();
        else if (ds[i].getLoaiHang() == 2) tongLoai2 += ds[i].getSoLuong();
        else if (ds[i].getLoaiHang() == 3) tongLoai3 += ds[i].getSoLuong();
    }

    cout << "\nThong ke so luong theo loai\n";
    cout << "Loai 1: " << tongLoai1 << "\n";
    cout << "Loai 2: " << tongLoai2 << "\n";
    cout << "Loai 3: " << tongLoai3 << "\n";

    int maxBH = -1;
    int idx = -1;
    for (int i = 0; i < n; i++) {
        if (ds[i].getLoaiHang() == 1 && ds[i].getTgbh() > maxBH) {
            maxBH = ds[i].getTgbh();
            idx = i;
        }
    }

    if (idx != -1) {
        cout << "\nHang dien tu loai 1 co thoi gian bao hanh lau nhat la:\n";
        ds[idx].xuat();
    } else {
        cout << "\nKhong co mat hang loai 1 nao.\n";
    }

    delete[] ds;
    return 0;
}
