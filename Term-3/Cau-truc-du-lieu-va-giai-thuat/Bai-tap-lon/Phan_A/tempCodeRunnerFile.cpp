#include <bits/stdc++.h>
#include <list>

using namespace std;

class SinhVien {
private:
    string maSV;
    string hoTen;
    string ngaySinh;
    string gioiTinh;
    string lopHoc;
    double diemTongKet; 

public:
    // Constructor
    SinhVien(string ms, string ht, string ns, string gt, string lh, double dtk)
        : maSV(ms), hoTen(ht), ngaySinh(ns), gioiTinh(gt), lopHoc(lh), diemTongKet(dtk) {}

    // Getter cho các thuộc tính
    string getMaSV() const { return maSV; }
    string getHoTen() const { return hoTen; }
    string getNgaySinh() const { return ngaySinh; }
    string getGioiTinh() const { return gioiTinh; }
    string getLopHoc() const { return lopHoc; }
    double getDiemTongKet() const { return diemTongKet; }

    // Setter cho các thuộc tính
    void setHoTen(string ht) { hoTen = ht; }
    void setNgaySinh(string ns) { ngaySinh = ns; }
    void setGioiTinh(string gt) { gioiTinh = gt; }
    void setLopHoc(string lh) { lopHoc = lh; }
    void setDiemTongKet(double dtk) { diemTongKet = dtk; }

    friend istream& operator>>(istream& is, SinhVien& sv) {
        cout << "Nhap ma SV: "; is >> sv.maSV; is.ignore();
        cout << "Nhap ho va ten: "; getline(is, sv.hoTen);
        cout << "Nhap ngay sinh: "; getline(is, sv.ngaySinh);
        cout << "Nhap gioi tinh: "; getline(is, sv.gioiTinh);
        cout << "Nhap lop: "; getline(is, sv.lopHoc);
        cout << "Nhap diem tong ket: "; is >> sv.diemTongKet;
        return is;
    }

    // Toán tử << để hiển thị thông tin sinh viên
    friend ostream& operator<<(ostream& os, const SinhVien& sv) {
        os << "| " << setw(10) << sv.maSV 
           << " | " << setw(20) << sv.hoTen 
           << " | " << setw(12) << sv.ngaySinh 
           << " | " << setw(10) << sv.gioiTinh 
           << " | " << setw(10) << sv.lopHoc 
           << " | " << setw(10) << sv.diemTongKet 
           << " |" << endl;
        os << "+------------+----------------------+--------------+------------+------------+------------+" << endl;
        return os;
    }

    // Toán tử so sánh cho phép sắp xếp theo `diemTongKet` và `hoTen`
    bool operator<(const SinhVien& other) const {
        if (diemTongKet != other.diemTongKet) {
            return diemTongKet > other.diemTongKet; // Sắp xếp giảm dần theo điểm tổng kết
        }
        return hoTen < other.hoTen; // Nếu điểm tổng kết bằng nhau, sắp xếp tăng dần theo tên
    }
};

class QuanLySinhVien {
private:
    list<SinhVien> dsSinhVien;

public:
    // Đọc danh sách sinh viên từ file
    void docTuFile(const string& filename) {
        ifstream inFile(filename);
        if (!inFile) {
            cout << "Khong the mo file!\n";
            return;
        }
        string ma, ten, ns, gt, lop;
        double dtk;
        while (inFile >> ma >> ten >> ns >> gt >> lop >> dtk) {
            dsSinhVien.emplace_back(ma, ten, ns, gt, lop, dtk);
        }
        inFile.close();
        cout << "Doc file thanh cong.\n";
    }

    // Ghi danh sách sinh viên vào file
    void ghiVaoFile(const string& filename) const {
        ofstream outFile(filename);
        if (!outFile) {
            cout << "Khong the mo file de ghi!\n";
            return;
        }
        for (const auto& sv : dsSinhVien) {
            outFile << sv.getMaSV() << " " << sv.getHoTen() << " "
                    << sv.getNgaySinh() << " " << sv.getGioiTinh() << " "
                    << sv.getLopHoc() << " " << sv.getDiemTongKet() << endl;
        }
        outFile.close();
        cout << "Ghi file thanh cong.\n";
    }

    // Thêm sinh viên mới vào danh sách
    void themSinhVien(const SinhVien& sv) {
        dsSinhVien.push_back(sv);
    }

    // Xóa sinh viên theo mã
    void xoaSinhVien(const string& ma) {
        auto it = remove_if(dsSinhVien.begin(), dsSinhVien.end(), 
                            [&](const SinhVien& sv) { return sv.getMaSV() == ma; });
        if (it != dsSinhVien.end()) {
            dsSinhVien.erase(it, dsSinhVien.end());
            cout << "Xoa sinh vien thanh cong.\n";
        } else {
            cout << "Khong tim thay sinh vien voi ma " << ma << endl;
        }
    }

    // Sửa thông tin sinh viên theo mã
    void suaSinhVien(const string& ma) {
        for (auto& sv : dsSinhVien) {
            if (sv.getMaSV() == ma) {
                string ten, ns, gt, lop;
                double dtk;
                cout << "Nhap thong tin moi:\n";
                cin.ignore(); 
                cout << "Ho va Ten: "; getline(cin, ten);
                cout << "Ngay Sinh: "; getline(cin, ns);
                cout << "Gioi Tinh: "; getline(cin, gt);
                cout << "Lop: "; getline(cin, lop);
                cout << "Diem Tong Ket: "; cin >> dtk;
                sv.setHoTen(ten);
                sv.setNgaySinh(ns);
                sv.setGioiTinh(gt);
                sv.setLopHoc(lop);
                sv.setDiemTongKet(dtk);
                cout << "Sua thong tin thanh cong.\n";
                return;
            }
        }
        cout << "Khong tim thay sinh vien voi ma " << ma << endl;
    }

    // Hiển thị danh sách sinh viên
    void hienThiDanhSach() const {
        if (dsSinhVien.empty()) {
            cout << "Danh sach sinh vien trong.\n";
            return;
        }
        for (const auto& sv : dsSinhVien) {
            cout << sv;
        }
    }

    // Tìm kiếm sinh viên theo tên
    void timKiemTheoTen(const string& ten) const {
        bool found = false;
        for (const auto& sv : dsSinhVien) {
            if (sv.getHoTen() == ten) {
                cout << sv;
                found = true;
            }
        }
        if (!found) {
            cout << "Khong tim thay sinh vien voi ten " << ten << endl;
        }
    }

    // Thống kê số lượng sinh viên
    void thongKeSoLuong() const {
        cout << "Tong so sinh vien: " << dsSinhVien.size() << endl;
    }

    // Sắp xếp danh sách sinh viên
    void sapXepDanhSach() {
        dsSinhVien.sort();
        cout << "Da sap xep danh sach sinh vien.\n";
    }
};

// Lớp App để quản lý menu
class App {
private:
    QuanLySinhVien qlsv;

public:
    void menu() {
        int choice;
        do {
            cout << "\n==== QUAN LY SINH VIEN ====\n";
            cout << "1. Nhap danh sach sinh vien tu file\n";
            cout << "2. Ghi danh sach sinh vien vao file\n";
            cout << "3. Them sinh vien moi\n";
            cout << "4. Xoa sinh vien\n";
            cout << "5. Sua thong tin sinh vien\n";
            cout << "6. Hien thi danh sach sinh vien\n";
            cout << "7. Tim kiem sinh vien theo ten\n";
            cout << "8. Thong ke so luong sinh vien\n";
            cout << "9. Sap xep danh sach sinh vien\n";
            cout << "0. Thoat\n";
            cout << "Lua chon cua ban: ";
            cin >> choice;

            switch (choice) {
                case 1: {
                    string filename;
                    cout << "Nhap ten file: ";
                    cin >> filename;
                    qlsv.docTuFile(filename);
                    break;
                }
                case 2: {
                    string filename;
                    cout << "Nhap ten file: ";
                    cin >> filename;
                    qlsv.ghiVaoFile(filename);
                    break;
                }
                case 3: {
                    SinhVien sv("", "", "", "", "", 0.0); // Tạo một đối tượng SinhVien trống
                    cin >> sv; // Nhập thông tin sinh viên từ người dùng
                    qlsv.themSinhVien(sv); // Thêm sinh viên vào danh sách
                    break;
                }
                case 4: {
                    string ma;
                    cout << "Nhap ma sinh vien can xoa: ";
                    cin >> ma;
                    qlsv.xoaSinhVien(ma);
                    break;
                }
                case 5: {
                    string ma;
                    cout << "Nhap ma sinh vien can sua: ";
                    cin >> ma;
                    qlsv.suaSinhVien(ma);
                    break;
                }
                case 6:
                    qlsv.hienThiDanhSach();
                    break;
                case 7: {
                    string ten;
                    cout << "Nhap ten sinh vien can tim: ";
                    cin.ignore();
                    getline(cin, ten);
                    qlsv.timKiemTheoTen(ten);
                    break;
                }
                case 8:
                    qlsv.thongKeSoLuong();
                    break;
                case 9:
                    qlsv.sapXepDanhSach();
                    break;
                case 0:
                    cout << "Thoat chuong trinh.\n";
                    break;
                default:
                    cout << "Lua chon khong hop le.\n";
            }
        } while (choice != 0);
    }
};

int main() {
    App app;
    app.menu();
    return 0;
}
