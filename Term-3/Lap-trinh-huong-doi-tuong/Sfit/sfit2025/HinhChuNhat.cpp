#include <iostream>
using namespace std;

class HCN {
	private:
		int chieuDai;
		int chieuRong; 
	
	public:
		// Ham tao mac dinh
		HCN() {
			chieuDai = 0;
			chieuRong = 0;
		}
		
		// Ham tao co tham so
		HCN(int cd, int cr) {
			chieuDai = cd;
			chieuRong = cr;
		}
		
		void nhap() {
			cout << "Chieu dai: ";
			cin >> chieuDai;
			cout << "Chieu rong: ";
			cin >> chieuRong;
		}
		
		void in() {
			cout << "(" << chieuDai << ", " << chieuRong << ") ";
		}
		
		int dienTich() {
			return chieuDai * chieuRong;
		}
		
		bool isSquare() {
			return (chieuDai == chieuRong);
		}
};

int main() {
	int n;
	cout << "Nhap n: ";
	cin >> n;
	
	
	HCN *hcn = new HCN[n];
	for (int i = 0; i < n; i++) {
		cout << "Nhap chieu dai va chieu rong cho hcn thu " << i + 1 << ":\n";
		hcn[i].nhap();
	}
	
	cout << "Cac hinh chu nhat vua nhap la:\n";
	for (int i = 0; i < n; i++) {
		hcn[i].in();
	}
	
	bool check = false;
	int maxIndex = 0;
	int maxDienTich = 0;
	for (int i = 0; i < n; i++) {
		if ((hcn[i].dienTich() > maxDienTich) && hcn[i].isSquare()) {
			maxIndex = i;
			maxDienTich = hcn[i].dienTich();
			check = true;
		}
	}
	
	if (check) {
		cout << "\nHinh vuong co dien tich lon nhat la: " << hcn[maxIndex].dienTich();
	} else {
		cout << "\nKhong hinh vuong nao.";
	}
	
	delete[] hcn;
	
	return 0;
}