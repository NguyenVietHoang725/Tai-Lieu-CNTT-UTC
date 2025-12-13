#include <iostream>
using namespace std;

class Vector {
private:
    int n;      
    int *a;        

public:
    Vector() {
        n = 0;
        a = nullptr;
    }

    Vector(int k) {
        n = k;
        a = new int[n];
        for (int i = 0; i < n; i++)
            a[i] = 0;
    }

    friend istream& operator>>(istream &in, Vector &v) {
        cout << "Nhap so chieu cua vector: ";
        in >> v.n;
        if (v.a != nullptr) delete[] v.a;
        v.a = new int[v.n];
        for (int i = 0; i < v.n; i++) {
            cout << "Nhap phan tu thu " << i + 1 << ": ";
            in >> v.a[i];
        }
        return in;
    }

    friend ostream& operator<<(ostream &out, const Vector &v) {
        out << "(";
        for (int i = 0; i < v.n; i++) {
            out << v.a[i];
            if (i < v.n - 1) out << ", ";
        }
        out << ")";
        return out;
    }

    Vector operator+(const Vector &v) const {
        int k = (n < v.n ? n : v.n);
        Vector res(k);
        for (int i = 0; i < k; i++)
            res.a[i] = a[i] + v.a[i];
        return res;
    }

    int tichVoHuong(const Vector &v) const {
        int k = (n < v.n ? n : v.n);
        int sum = 0;
        for (int i = 0; i < k; i++)
            sum += a[i] * v.a[i];
        return sum;
    }

    Vector nhanVoiSo(int k) const {
        Vector res(n);
        for (int i = 0; i < n; i++)
            res.a[i] = a[i] * k;
        return res;
    }
};

int main() {
    Vector u, v;
    int m, n;

    cout << "=> Nhap vector u:\n";
    cin >> u;
    cout << "=> Nhap vector v:\n";
    cin >> v;

    cout << "Nhap so nguyen m: ";
    cin >> m;
    cout << "Nhap so nguyen n: ";
    cin >> n;

    int tich_uv = u.tichVoHuong(v);
    Vector nv = v.nhanVoiSo(n);

    cout << "\n=> Vector u = " << u;
    cout << "\n=> Vector v = " << v;
    cout << "\nTich vo huong u.v = " << tich_uv;

    cout << "\n=> Vector n*v = " << nv;
    cout << "\n";

    return 0;
}
