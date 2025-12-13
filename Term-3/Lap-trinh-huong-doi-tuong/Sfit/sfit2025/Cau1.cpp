#include <iostream>
using namespace std;

int main() {
	int n;
	cout << "Nhap n: ";
	cin >> n;
	
	float ar[n];
	float *a = new float[n];
	for (int i = 0; i < n; i++) {
		cout << "a[" << i << "] = ";
		cin >> a[i];
	}
	
	cout << "Cac so thuc duong la: ";
	for (int i = 0; i < n; i++) {
		if (a[i] > 0) {
			cout << a[i] << " ";
		}
	}
	
	cout << "\nCac so thuc am la: ";
	for (int i = 0; i < n; i++) {
		if (a[i] < 0) {
			cout << a[i] << " ";
		}
	}
	
	return 0;
}
