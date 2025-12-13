#include <iostream>
using namespace std;

class Smartphone {
private:
	string ten;
	string hangSX;
	int luongPin;
	int dungLuong;
	int ram;
public:	
	Smartphone() {
	    this->ten = "";
	    this->hangSX = "";
	    this->luongPin = 0;
	    this->dungLuong = 0;
	    this->ram = 0;
	}
	
	Smartphone(string ten, string hangSX, int luongPin, int dungLuong, int ram) {
		this->ten = ten;
        this->hangSX = hangSX;
        this->luongPin = luongPin;
        this->dungLuong = dungLuong;
        this->ram = ram;
	}
	
	void nhap() {
        cout << "Nhap lan luot thong tin cua dien thoai:\n";
        cout << "Ten: "; getline(cin, ten);
        cout << "Hang san xuat: "; getline(cin, hangSX);
        cout << "Luong pin (mAh): "; cin >> luongPin;
        cout << "Dung luong bo nho (GB): "; cin >> dungLuong;
        cout << "RAM (GB): "; cin >> ram;
        cin.ignore(); 
    }
	
	void xuat() {
        cout << "\nThong tin dien thoai\n";
        cout << "Ten: " << ten << endl;
        cout << "Hang san xuat: " << hangSX << endl;
        cout << "Luong pin: " << luongPin << " mAh" << endl;
        cout << "Dung luong: " << dungLuong << " GB" << endl;
        cout << "RAM: " << ram << " GB" << endl;
    }
	
    string getTen() { return ten; }
    void setTen(string ten) { this->ten = ten; }

    string getHangSX() { return hangSX; }
    void setHangSX(string hangSX) { this->hangSX = hangSX; }

    int getLuongPin() { return luongPin; }
    void setLuongPin(int luongPin) { this->luongPin = luongPin; }

    int getDungLuong() { return dungLuong; }
    void setDungLuong(int dungLuong) { this->dungLuong = dungLuong; }

    int getRam() { return ram; }
    void setRam(int ram) { this->ram = ram; }
    
    void soSanhPin(const Smartphone &other) {
        if (luongPin > other.luongPin) {
            cout << ten << " co pin nhieu hon " << other.ten << endl;
        } else if (luongPin < other.luongPin) {
            cout << other.ten << " co pin nhieu hon " << ten << endl;
        } else {
            cout << ten << " va " << other.ten << " co luong pin bang nhau\n";
        }
    }
    
    void kiemTraHieuNang() {
        cout << "\nDanh gia hieu nang cua " << ten << ": ";
        if (ram >= 8 && dungLuong >= 128) {
            cout << "Hieu nang manh" << endl;
        } else {
            cout << "Hieu nang trung binh" << endl;
        }
    }
};

int main() {
	// "Galaxy A14", "Samsung", 5000, 64, 4
	Smartphone sp1;
    cout << "Nhap thong tin dien thoai:\n";
    sp1.nhap();
    Smartphone sp2("iPhone 15", "Apple", 3300, 256, 8);
    
    cout << "\n__Thong tin dien thoai__\n";
    sp1.xuat();
    sp2.xuat();
    
    cout << "\n__So sanh pin__\n";
    sp1.soSanhPin(sp2);
    
    cout << "\n__Kiem tra hieu nang__\n";
    sp1.kiemTraHieuNang();
    sp2.kiemTraHieuNang();
}