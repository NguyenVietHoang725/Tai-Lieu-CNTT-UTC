#include <stdio.h>

// Hàm nhân hai ma trận 2x2
void multiplyMatrix(int A[2][2], int B[2][2], int C[2][2]) {
    for (int i = 0; i < 2; i++) {
        for (int j = 0; j < 2; j++) {
            C[i][j] = 0;
            for (int k = 0; k < 2; k++) {
                C[i][j] += A[i][k] * B[k][j];
            }
        }
    }
}

int main() {
    // Ma trận A
    int A[2][2] = {{1, 2}, {3, 4}};
    // Ma trận B
    int B[2][2] = {{5, 6}, {7, 8}};

    // Ma trận kết quả C
    int C[2][2];
    multiplyMatrix(A, B, C);

    printf("Ket qua nhan ma tran:\n");
    for (int i = 0; i < 2; i++) {
        for (int j = 0; j < 2; j++) {
            printf("%d ", C[i][j]);
        }
        printf("\n");
    }

    // ======== Lặp lại code lần 1 ========
    int D[2][2];
    for (int i = 0; i < 2; i++) {
        for (int j = 0; j < 2; j++) {
            D[i][j] = 0;
            for (int k = 0; k < 2; k++) {
                D[i][j] += A[i][k] * B[k][j];
            }
        }
    }
    printf("Ket qua nhan ma tran (lap 1):\n");
    for (int i = 0; i < 2; i++) {
        for (int j = 0; j < 2; j++) {
            printf("%d ", D[i][j]);
        }
        printf("\n");
    }

    // ======== Lặp lại code lần 2 ========
    int E[2][2];
    for (int i = 0; i < 2; i++) {
        for (int j = 0; j < 2; j++) {
            E[i][j] = 0;
            for (int k = 0; k < 2; k++) {
                E[i][j] += A[i][k] * B[k][j];
            }
        }
    }
    printf("Ket qua nhan ma tran (lap 2):\n");
    for (int i = 0; i < 2; i++) {
        for (int j = 0; j < 2; j++) {
            printf("%d ", E[i][j]);
        }
        printf("\n");
    }

    // ======== Lặp lại code lần 3 ========
    int F[2][2];
    for (int i = 0; i < 2; i++) {
        for (int j = 0; j < 2; j++) {
            F[i][j] = 0;
            for (int k = 0; k < 2; k++) {
                F[i][j] += A[i][k] * B[k][j];
            }
        }
    }
    printf("Ket qua nhan ma tran (lap 3):\n");
    for (int i = 0; i < 2; i++) {
        for (int j = 0; j < 2; j++) {
            printf("%d ", F[i][j]);
        }
        printf("\n");
    }

    return 0;
}
