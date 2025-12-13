#include <iostream>
#include <iomanip>
using namespace std;

class HCN {
private:
    float dai, rong;

public:
    HCN() {
        dai = rong = 0;
    }

    HCN(float d, float r) {
        dai = d;
        rong = r;
    }

    void nhap() {
        cout << "Nhap chieu dai: ";
        cin >> dai;
        cout << "Nhap chieu rong: ";
        cin >> rong;
    }

    void xuat() const {
        cout << "Hinh chu nhat (dai = " << dai << ", rong = " << rong << ")";
    }

    float dienTich() const {
        return dai * rong;
    }
};

int main() {
    int n;
    cout << "Nhap so luong hinh chu nhat: ";
    cin >> n;

    HCN *ds = new HCN[n];
    float tongDT = 0;

    cout << "\n=== NHAP THONG TIN CAC HINH CHU NHAT ===\n";
    for (int i = 0; i < n; i++) {
        cout << "\nHinh thu " << i + 1 << ":\n";
        ds[i].nhap();
        tongDT += ds[i].dienTich();
    }

    float tb = tongDT / n;
    cout << "\nDien tich trung binh cua cac hinh: " << fixed << setprecision(2) << tb << endl;

    float minDT = ds[0].dienTich();
    for (int i = 1; i < n; i++)
        if (ds[i].dienTich() < minDT)
            minDT = ds[i].dienTich();

    cout << "\nNhung hinh co dien tich nho nhat (" << minDT << "):\n";
    for (int i = 0; i < n; i++) {
        if (ds[i].dienTich() == minDT) {
            ds[i].xuat();
            cout << endl;
        }
    }

    delete[] ds;
    return 0;
}
