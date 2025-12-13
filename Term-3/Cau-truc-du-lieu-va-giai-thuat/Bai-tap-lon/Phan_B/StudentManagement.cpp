#include "BinarySearchTree.cpp"
#include <iostream>
#include <iomanip>
#include <fstream>
#include <string>
#include <sstream>
using namespace std;

struct SinhVien {
    string msv;            
    string ten;       
    int tuoi;            

    bool operator<(const SinhVien& other) const { return msv < other.msv; }
    bool operator>(const SinhVien& other) const { return msv > other.msv; }
    bool operator==(const SinhVien& other) const { return msv == other.msv; }

    friend ostream& operator<<(ostream& os, const SinhVien& sv) {
        os << "| " << setw(10) << sv.msv
           << " | " << setw(20) << sv.ten 
           << " | " << setw(10) << sv.tuoi
           << " |" << endl; 
        os << "+------------+----------------------+------------+" << endl;
        return os;
    }
};

class QuanLySinhVien {
private:
    BinarySearchTree<SinhVien> bst;

public:
    // a. Đọc danh sách sinh viên từ tệp vào cây
    void docTuFile(const string& filename) {
        ifstream inFile(filename);
        if (!inFile) {
            cout << "Khong the mo file!\n";
            return;
        }
        string line;
        while (getline(inFile, line)) {
            stringstream ss(line);
            string msv, ten, tuoi_str;
            int tuoi;

            getline(ss, msv, ',');   
            getline(ss, ten, ',');   
            getline(ss, tuoi_str, ','); 
            stringstream(tuoi_str) >> tuoi;

            // Thêm sinh viên vào cây
            bst.them(SinhVien{msv, ten, tuoi});
        }
        inFile.close();
        cout << "Doc file thanh cong.\n";
    }

    // b. Thêm 1 sinh viên vào cây
    void themSinhVien(const SinhVien& sv) {
        if (bst.timKiem(sv)) {
            cout << "Loi: Sinh vien voi MSV " << sv.msv << " da ton tai." << endl;
        } else {
            bst.them(sv);
            cout << "Them sinh vien thanh cong.\n";
        }
    }

    // c. Xóa 1 sinh viên theo MSV
    void xoaSinhVien(const string& msv) {
        SinhVien sv{msv, "", 0};
        if (!bst.timKiem(sv)) {
            cout << "Loi: Sinh vien voi MSV " << msv << " khong ton tai." << endl;
        } else {
            bst.xoa(sv);
            cout << "Xoa sinh vien thanh cong.\n";
        }
    }

    // d. Cập nhật thông tin 1 sinh viên khi biết MSV
    void capNhatSinhVien(const string& msv, const string& tenMoi, int tuoiMoi) {
        xoaSinhVien(msv);  
        themSinhVien(SinhVien{msv, tenMoi, tuoiMoi}); 
        cout << "Cap nhat thong tin sinh vien thanh cong.\n";
    }

    // e. Tìm kiếm sinh viên theo MSV
    void timSinhVien(const string& msv) {
        SinhVien sv{msv, "", 0};  
        Node<SinhVien>* node = bst.timKiem(sv);  

        if (node != nullptr) {  
            cout << "Da tim thay sinh vien: " << endl;
            cout << "+------------+----------------------+------------+" << endl;
            cout << node->data; 
        } else {
            cout << "Sinh vien voi MSV " << msv << " khong ton tai." << endl;
        }
    }

    // f. Hiển thị danh sách sinh viên lên màn hình
    void hienThiSinhVien() {
        cout << "Danh sach sinh vien:\n";
        cout << "+------------+----------------------+------------+" << endl;
        bst.duyetGiua();
    }

    // g. Ghi toàn bộ sinh viên của cây vào file
    void luuVaoFile(const string& filename) {
        ofstream file(filename);
        if (file.is_open()) {
            ghiVaoFile(bst.root, file);
            file.close();
            cout << "Du lieu da duoc luu vao tep.\n";
        } else {
            cerr << "Khong the mo tep: " << filename << endl;
        }
    }

private:
    void ghiVaoFile(Node<SinhVien>* node, ofstream& file) {
        if (node != nullptr) {
            ghiVaoFile(node->left, file);
            file << node->data.msv << "," << node->data.ten << "," << node->data.tuoi << endl;
            ghiVaoFile(node->right, file);
        }
    }
};

class App {
private:
    QuanLySinhVien qlsv;

public:
    void hienThiMenu() {
        cout << "\n==== QUAN LY SINH VIEN ====\n";
        cout << "1. Tai danh sach sinh vien tu tep\n";
        cout << "2. Them sinh vien\n";
        cout << "3. Xoa sinh vien\n";
        cout << "4. Cap nhat thong tin sinh vien\n";
        cout << "5. Tim kiem sinh vien\n";
        cout << "6. Hien thi danh sach sinh vien\n";
        cout << "7. Luu danh sach sinh vien vao tep\n";
        cout << "0. Thoat\n";
        cout << "Chon chuc nang: ";
    }

    void menu() {
        int luaChon;

        do {
            hienThiMenu();
            cin >> luaChon;

            switch (luaChon) {
                case 1: {
                    string filename;
                    cout << "Nhap ten tep de tai danh sach sinh vien: ";
                    cin >> filename;
                    qlsv.docTuFile(filename);
                    break;
                }
                case 2: {
                    string msv, ten;
                    int tuoi;
                    cout << "Nhap MSV sinh vien: ";
                    cin >> msv;
                    cout << "Nhap ten sinh vien: ";
                    cin.ignore();
                    getline(cin, ten);
                    cout << "Nhap tuoi sinh vien: ";
                    cin >> tuoi;
                    qlsv.themSinhVien({msv, ten, tuoi});
                    break;
                }
                case 3: {
                    string msv;
                    cout << "Nhap MSV cua sinh vien can xoa: ";
                    cin >> msv;
                    qlsv.xoaSinhVien(msv);
                    break;
                }
                case 4: {
                    string msv, ten;
                    int tuoi;
                    cout << "Nhap MSV cua sinh vien can cap nhat: ";
                    cin >> msv;
                    cout << "Nhap ten moi: ";
                    cin.ignore();
                    getline(cin, ten);
                    cout << "Nhap tuoi moi: ";
                    cin >> tuoi;
                    qlsv.capNhatSinhVien(msv, ten, tuoi);
                    break;
                }
                case 5: {
                    string msv;
                    cout << "Nhap MSV can tim: ";
                    cin >> msv;
                    qlsv.timSinhVien(msv);
                    break;
                }
                case 6:
                    qlsv.hienThiSinhVien();
                    break;
                case 7: {
                    string filename;
                    cout << "Nhap ten tep de luu danh sach sinh vien: ";
                    cin >> filename;
                    qlsv.luuVaoFile(filename);
                    break;
                }
                case 0:
                    cout << "Thoat chuong trinh.\n";
                    break;
                default:
                    cout << "Lua chon khong hop le. Vui long chon lai.\n";
                    break;
            }
        } while (luaChon != 0);
    }
};

int main() {
    App app;
    app.menu();
    return 0;
}
