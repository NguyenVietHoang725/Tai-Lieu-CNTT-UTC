# Thuật toán Huffman
---
# 1. Mã có độ dài thay đổi (Variable-length codes)

## 1.1. Khái niệm
Đúng như tên gọi, đây là phương pháp **sử dụng một số bit khác nhau** để **biểu diễn các ký tự khác nhau**.

| Loại mã | Giải thích | Ví dụ |
|---------|------------|-------|
| Mã cố định độ dài (Fixed-length code) | Quen thuộc nhất nhưng lãng phí không gian vì bỏ qua tần suất xuất hiện của ký tự mà ký tự nào cũng đều được biểu diễn một số bit như nhau. | Ví dụ trong bảng mã ASCII sử dụng 8 bit cho mọi ký tự: <br/> - Chữ 'A' (65) là `01000001` </br> - Chữ 'B' (66) là `01000010` |
| Mã thay đổi độ dài (Variable-length code) | Ý tưởng là gán những mã ngắn cho các ký tự xuất hiện nhiều và mã dài cho các ký tự xuất hiện ít. Điều này giúp giảm đáng kể tổng số bit cần dùng để biểu diễn toàn bộ thông điệp. | Ví dụ đoạn mã: AABAC chúng ta giả sử biểu diễn A là '1', B là '0', C là '10' thì khi nén chỉ là `110110` | 

## 1.2. Vấn đề khi sử dụng Mã có độ dài thay đổi
Nhưng việc sử dụng mã thay đổi độ dài có tiềm ẩn một vấn đề - **Tính mơ hồ**: 

Ví dụ: Mã Morse là minh họa hoàn hảo cho vấn đề này:

- Giả sử bạn có chuỗi tín hiệu: `• • • − − − • • •` (3 chấm, 3 gạch, 3 chấm)

- Nếu giải mã theo từng ký tự, nó là S O S (S = `• • •`, O = `− − −`, S = `• • •`) - một tín hiệu cầu cứu.

- Nhưng nếu không có khoảng cách ngăn cách, làm sao ta biết nó không phải là V 7? (V = `• • • −`, 7 = `− − • • •`)?

Hoặc I A M I E (I = `• •`, A = `• −`, M = `− −`, I = `• •`, E = `•`)?

Hoặc E E W N I... và vô số cách kết hợp khác.

**Vấn đề**: Khi các mã có độ dài khác nhau được nối với nhau mà **không có ranh giới rõ ràng**, máy tính (hoặc người giải mã) sẽ **không thể biết được nên ngắt chuỗi bit ở đâu** để tạo thành một ký tự hợp lệ, dẫn đến **nhiều cách hiểu khác nhau**.

# 1.3. Giải pháp: Mã tiền tố (Prefix-free code) 

Đây là trái tim của thuật toán Huffman. Giải pháp để tránh sự mơ hồ mà không cần thêm bất kỳ ký tự đặc biệt nào để phân tách các từ mã.

**Định nghĩa:** Một bộ mã được gọi là Prefix-free (hoặc "prefix code") nếu không có từ mã nào trong bộ mã là tiền tố (phần đầu) của một từ mã khác.

Xét các ví dụ:

| | A | B | C | D |
|-|---|---|---|---|
| Bộ mã 1 | 0 | 1 | 01 | 10 |
| Bộ mã 2 | 0 | 10 | 110 | 111 |

Xét giải mã đoạn mã sau: `0101110110`
- Với bộ mã 1: 
  - TH1: ABABBBABBA
  - TH2: ACAADAAB
  - /.../ (còn nhiều)
- Với bộ mã 2: ABDAC (chỉ có một cách giải mã duy nhất)

**Tại sao nó hoạt động?** Tính chất "**không mã nào là tiền tố của mã khác**" đảm bảo rằng ngay khi bạn đọc đủ một chuỗi bit khớp với một từ mã, bạn có thể ngay lập tức xuất ký tự đó ra mà không cần phải "nhìn trước" các bit tiếp theo để xem liệu nó có phải là phần đầu của một mã dài hơn không. Việc giải mã có thể được thực hiện một cách tham lam (greedy) từ trái sang phải.

---
# 2. Biểu diễn Mã tiền tố bằng Cây Trie nhị phân

## 2.1. Ý tưởng
Câu hỏi: Làm thế nào để biểu diễn một bộ mã prefix-free?
Câu trả lời: Dùng một cây Trie nhị phân (binary trie)!

Đây là một ý tưởng cực kỳ thông minh và quan trọng. Một cây Trie cho mã tiền tố có các đặc điểm sau:
- **Lá (Leaves)**: Mỗi lá của cây đại diện cho một ký tự trong bảng chữ cái cần mã hóa. Lá cũng thường chứa tần suất (trọng số) của ký tự đó.
- **Nút trong (Internal nodes)**: Các nút không phải là lá. Chúng không chứa ký tự nào, mà chỉ dùng để xây dựng đường đi.
- **Đường đi (Path)**: Mã của một ký tự chính là chuỗi các bước đi từ gốc (root) đến lá chứa ký tự đó.
- **Quy ước**: Thông thường, quy ước đi về nhánh trái là bit 0, và đi về nhánh phải là bit 1.

Ví dụ: ABRACADABRA!
![Hình minh họa](/rsc/trie.png)

Tính chất prefix-free được đảm bảo tuyệt đối vì để đến được một ký tự, bạn bắt buộc phải đi đến tận lá của cây.

## 2.2. Nén (Compression) - Từ ký tự sang chuỗi bits

**Mục tiêu:** Cho một ký tự, làm sao để tìm được chuỗi bit mã hóa của nó?

Có 2 phương pháp:

**1. Phương pháp đi từ lá lên gốc (và đảo ngược):**

- Bước 1: Tìm nút lá chứa ký tự cần mã hóa.

- Bước 2: Đi ngược từ lá này lên nút gốc. Ghi lại từng bước đi:
  - Nếu nút hiện tại là con phải của nút cha, ghi lại 1.
  - Nếu nút hiện tại là con trái của nút cha, ghi lại 0.

- Bước 3: Chuỗi bit thu được sau khi đi từ lá lên gốc sẽ là chuỗi ngược so với mã thực sự. Vì vậy, ta cần in chuỗi bit này theo thứ tự đảo ngược.

=> Ưu điểm: Không cần lưu trữ trước cả cây, có thể làm trong lúc xây dựng cây.
=> Nhược điểm: Chậm hơn vì phải đảo ngược chuỗi.

**2. Phương pháp dùng Bảng ký hiệu (Symbol Table - ST):**

- Bước 1: Duyệt qua toàn bộ cây Trie một lần và xây dựng một bảng ánh xạ (giống như từ điển).

- Bước 2: Với mỗi ký tự, mã của nó được lưu trực tiếp trong bảng này. Ví dụ: `ST['A'] = "0"`, `ST['B'] = "00"`, `ST['C'] = "01"`, v.v.

- Bước 3: Khi nén, với mỗi ký tự trong thông điệp, ta chỉ việc tra bảng và lấy ra chuỗi bit tương ứng.

=> Ưu điểm: Tốc độ nén cực nhanh, chỉ là thao tác tra bảng.
=> Nhược điểm: Cần thêm bộ nhớ để lưu trữ bảng.

**Trong thực tế, Phương pháp 2 (dùng bảng) được sử dụng phổ biến hơn vì tốc độ.**

## 2.3. Giải nén (Expression) - Từ chuỗi bits sang ký tự

**Mục tiêu:** Làm thế nào để giải mã một chuỗi bit (ví dụ: 0100011) mà không bị nhầm lẫn?

Giải nén chính là quá trình mô phỏng lại việc đi từ gốc đến lá trên cây Trie.

- Bước 1: Bắt đầu từ nút gốc (root) của cây Trie.

- Bước 2: Với mỗi bit trong luồng bit nén:
  - Nếu bit là 0, bạn đi sang nhánh trái của nút hiện tại.
  - Nếu bit là 1, bạn đi sang nhánh phải của nút hiện tại.

- Bước 3: Kiểm tra xem nút hiện tại có phải là một lá không?
  - Nếu là lá: Bạn đã tìm thấy một ký tự! Ghi ký tự đó ra kết quả giải nén, và quay trở lại nút gốc để bắt đầu giải mã ký tự tiếp theo.
  - Nếu không phải lá: Tiếp tục đọc bit tiếp theo và lặp lại Bước 2.

---
# 3. Thuật toán Huffman

## 3.1. Tổng quan
Thuật toán Huffman sử dụng một mô hình động (dynamic model): nó tạo ra một bộ mã prefix-free tối ưu riêng cho từng thông điệp cần nén.

**Quy trình nén (Compression):**

1. Đọc thông điệp: Đọc toàn bộ dữ liệu đầu vào.

2. Xây dựng bộ mã tối ưu: Tạo cây Huffman tối ưu dựa trên tần suất của các ký tự trong thông điệp. (Đây là phần then chốt).

3. Ghi cây mã hóa: Ghi cây Trie (biểu diễn bộ mã prefix-free) ra file nén. Điều này là bắt buộc để bên giải nén có thể giải mã.

4. Nén dữ liệu: Sử dụng bộ mã vừa tạo để mã hóa từng ký tự trong thông điệp thành chuỗi bit và ghi ra file.

**Quy trình giải nén (Expansion):**
1. Đọc cây mã hóa: Đọc cây Trie từ file nén. (Bước này khôi phục lại bộ mã prefix-free).

2. Giải nén thông điệp: Đọc luồng bit nén và sử dụng cây Trie để dịch chúng trở lại thành các ký tự gốc.

## 3.2. Xây dựng thuật toán

### 3.2.1. Kiểu dữ liệu Node của Cây Huffman

```java
private static class Node implements Comparable<Node>
{
    private final char ch;   // Chỉ dùng cho nút LÁ: lưu ký tự
    private final int freq;  // Chỉ dùng cho quá trình NÉN: lưu tần suất
    private final Node left, right; // Con trỏ trái và phải

    // Hàm khởi tạo
    public Node(char ch, int freq, Node left, Node right) {
        this.ch = ch;
        this.freq = freq;
        this.left = left;
        this.right = right;
    }
    // Kiểm tra xem nút có phải là lá không (không có con)
    public boolean isLeaf() {
        return left == null && right == null;
    }
    // So sánh hai nút DỰA VÀO TẦN SUẤT.
    // Điều này rất quan trọng để xây dựng cây tối ưu.
    public int compareTo(Node that) {
        return this.freq - that.freq;
    }
}
```

### 3.2.2. Giải nén

```java
public void expand() {
    Node root = readTrie(); // Bước 1: Đọc và khôi phục cây Trie từ file
    int N = BinaryStdIn.readInt(); // Bước 2: Đọc số lượng ký tự gốc cần giải nén

    for (int i = 0; i < N; i++) { // Lặp lại cho mỗi ký tự
        Node x = root; // Bắt đầu từ gốc cho mỗi ký tự
        while (!x.isLeaf()) { // Đi cho đến khi gặp một nút lá
            // Đọc 1 bit: false (0) đi sang trái, true (1) đi sang phải
            if (!BinaryStdIn.readBoolean())
                x = x.left;
            else
                x = x.right;
        }
        // Khi gặp lá, ghi ký tự trong nút lá đó ra output
        BinaryStdOut.write(x.ch, 8);
    }
    BinaryStdOut.close();
}
```
Độ phức tạp: Tuyến tính (O(N)) so với độ dài input N.

### 3.2.3. Truyền tải Cây Trie: Cách ghi và đọc

Vấn đề: Làm thế nào để chuyển cấu trúc cây Trie phức tạp thành một chuỗi bit để ghi vào file?
Giải pháp: Dùng duyệt cây thứ tự trước (Preorder Traversal) và đánh dấu các loại nút.

**Cách ghi cây** (`writeTrie`)
```java
private static void writeTrie(Node x) {
    if (x.isLeaf()) {       // Nếu là nút LÁ
        BinaryStdOut.write(true);  // Ghi bit đánh dấu '1' (true)
        BinaryStdOut.write(x.ch, 8); // Ghi ký tự của lá đó (8 bit)
        return;
    }
    BinaryStdOut.write(false); // Nếu là nút TRONG, ghi bit đánh dấu '0' (false)
    writeTrie(x.left);  // Duyệt sang cây con trái
    writeTrie(x.right); // Duyệt sang cây con phải
}
```

**Cách đọc cây** (`readTrie`)
```java
private static Node readTrie() {
    if (BinaryStdIn.readBoolean()) { // Đọc 1 bit: Nếu là '1' (true)
        char c = BinaryStdIn.readChar(8); // Đọc 8 bit tiếp theo để lấy ký tự
        return new Node(c, 0, null, null); // Trả về một nút LÁ
    }
    // Nếu đọc được '0' (false), đây là nút TRONG
    Node left = readTrie();  // Đệ quy đọc cây con trái
    Node right = readTrie(); // Đệ quy đọc cây con phải
    return new Node('\0', 0, left, right); // Trả về một nút trong với 2 con vừa đọc
}
```
Lưu ý: Nếu thông điệp đủ dài, chi phí truyền tải cây Trie này là rất nhỏ so với dữ liệu được nén.

### 3.2.4. Xây dựng Cây Huffman

```java
private static Node buildTrie(int[] freq) {
    MinPQ<Node> pq = new MinPQ<Node>(); // Tạo hàng đợi ưu tiên Min (ưu tiên nút có tần suất nhỏ nhất)
    for (char c = 0; c < R; c++)        // Với mỗi ký tự trong bảng chữ cái (R là kích thước bảng chữ)
        if (freq[c] > 0)                // Nếu ký tự đó xuất hiện ít nhất 1 lần
            pq.insert(new Node(c, freq[c], null, null)); // Thêm một nút LÁ vào hàng đợi

    while (pq.size() > 1) {             // Lặp cho đến khi chỉ còn 1 nút (cây duy nhất)
        Node x = pq.delMin();           // Lấy ra 2 nút có tần suất NHỎ NHẤT
        Node y = pq.delMin();
        Node parent = new Node('\0', x.freq + y.freq, x, y); // Tạo nút CHA mới cho 2 nút trên
        pq.insert(parent);              // Chèn nút cha mới này lại vào hàng đợi
    }
    return pq.delMin(); // Nút cuối cùng còn lại chính là gốc của cây Huffman hoàn chỉnh
}
```

