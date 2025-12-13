#include <iostream>
using namespace std;

void hoanvi(float &x, float &y) {
	float temp = x;
	x = y;
	y = temp;
}

void bubbleSort(float a[], int n) {
	for (int i = 0; i < n-1; i++) {
		for (int j = 0; j < n-i-1; j++) {
			if (a[i] > a[j+1]) {
				hoanvi(a[j], a[j+1]);
			}
		}
	}
}

void in(float a[], int n) {
	for (int i = 0; i < n; i++) {
		cout << a[i] << " ";
	}
	cout << endl;
}

int main() {
	int n;
	cout << "Nhap n: ";
	cin >> n;
	
	cout << "Nhap lan luot " << n << " so phan tu cua mang so thuc:\n";
	float *a = new float[n];
	for (int i = 0; i < n; i++) {
		cout << "a[" << i << "] = ";
		cin >> a[i];
	}
	
	cout << "Mang so thuc vua nhap la: ";
	in(a, n);
	
	bubbleSort(a, n);
	cout << "Mang so thuc sau khi sap xep la: ";
	in(a, n);
	
	delete[] a;
	
	return 0;
}