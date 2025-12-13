#include <iostream>
#include <cmath>
using namespace std;

class Diem {
private:
	float x;
	float y;
public:
	Diem() {
		x = 0;
		y = 0;
	}
	
	Diem(float dx, float dy) {
		x = dx;
		y = dy;
	}
	
	void setHD(float hD) {
        x = hD;
    } 
    void setTD (float tD) {
        y = tD;
    }
    float getHD () {
        return x;
    }
    float getTD () {
        return y;
    }
	
	void nhap() {
		cin >> x >> y;
	}
	
	void xuat() {
		cout << "(" << x << ", " << y << ")\n";
	}
	
	float kc(){
        return  sqrt( pow(x,2) + pow(y,2) );
    }
    float kc2( Diem d){
        return sqrt( pow(x-d.x,2) + pow(y-d.y,2) );
    }
};

int main() {
	Diem a;
	Diem b(2, 3);
	cout << "Nhap a: ";
	a.nhap();
	cout << "Hai diem vua nhap la: ";
	a.xuat();
	b.xuat();
	
	cout << "\nDiem a(" << a.getHD() << ", " << a.getTD() << ")";
}