#include <iostream>
#include <string>
#include <iomanip>
using namespace std;

class Thisinh {
protected:
    string hoten;
    float diem;

public:
    Thisinh() {
        hoten = "";
        diem = 0;
    }

    Thisinh(string ten, float d) {
        hoten = ten;
        diem = d;
    }

    virtual void nhap() {
        cin.ignore();
        cout << "Nhap ho ten thi sinh: ";
        getline(cin, hoten);
        cout << "Nhap diem: ";
        cin >> diem;
    }

    virtual void xuat() const {
        cout << "Ho ten: " << setw(20) << left << hoten
             << " | Diem: " << diem;
    }

    float getDiem() const { return diem; }
    string getTen() const { return hoten; }
};

class Olympic : public Thisinh {
private:
    string truong; // A, B, C

public:
    Olympic() : Thisinh() {
        truong = "";
    }

    Olympic(string ten, float d, string t) : Thisinh(ten, d) {
        truong = t;
    }

    void nhap() override {
        Thisinh::nhap();
        cin.ignore();
        cout << "Nhap truong (A, B, C): ";
        getline(cin, truong);
    }

    void xuat() const override {
        Thisinh::xuat();
        cout << " | Truong: " << truong << endl;
    }

    string getTruong() const { return truong; }
};

int main() {
    int n;
    cout << "Nhap so luong thi sinh: ";
    cin >> n;

    Olympic *ds = new Olympic[n];

    cout << "\n=== NHAP THONG TIN THI SINH ===\n";
    for (int i = 0; i < n; i++) {
        cout << "\nThi sinh thu " << i + 1 << ":\n";
        ds[i].nhap();
    }

    float tongA = 0, tongB = 0, tongC = 0;
    int demA = 0, demB = 0, demC = 0;

    for (int i = 0; i < n; i++) {
        if (ds[i].getTruong() == "A" || ds[i].getTruong() == "a") {
            tongA += ds[i].getDiem();
            demA++;
        } else if (ds[i].getTruong() == "B" || ds[i].getTruong() == "b") {
            tongB += ds[i].getDiem();
            demB++;
        } else if (ds[i].getTruong() == "C" || ds[i].getTruong() == "c") {
            tongC += ds[i].getDiem();
            demC++;
        }
    }

    cout << "\n=== DANH SACH THI SINH ===\n";
    for (int i = 0; i < n; i++)
        ds[i].xuat();

    cout << "\n\n=== THONG KE DIEM ===\n";
    cout << "Tong diem truong A: " << tongA << " (" << demA << " thi sinh)\n";
    cout << "Tong diem truong B: " << tongB << " (" << demB << " thi sinh)\n";
    cout << "Tong diem truong C: " << tongC << " (" << demC << " thi sinh)\n";

    float maxTong = tongA;
    string truongMax = "A";
    if (tongB > maxTong) { maxTong = tongB; truongMax = "B"; }
    if (tongC > maxTong) { maxTong = tongC; truongMax = "C"; }

    cout << "\n=> Truong co tong diem cao nhat: " << truongMax << " (" << maxTong << ")\n";

    delete[] ds;
    return 0;
}
