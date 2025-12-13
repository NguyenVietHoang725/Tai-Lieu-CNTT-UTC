# Thuật toán LZW Compression
---

## 1. Ba mô hình thống kế nén dữ liệu

| Mô hình | Khái niệm | Ưu điểm | Nhược điểm | Ví dụ |
|-|-|-|-|-|
| Static Mode <br/> (Mô hình tĩnh) | Sử dụng một bảng mã (model) cố định, được định nghĩa từ trước, áp dụng cho TẤT CẢ các văn bản hoặc tệp tin cần nén. | Rất nhanh. Vì bảng mã là cố định, không cần tốn thời gian để phân tích dữ liệu đầu vào hay xây dựng từ điển. | Không tối ưu. Nếu đặc điểm thống kê của dữ liệu bạn cần nén khác xa với mô hình được định nghĩa, tỷ lệ nén sẽ rất thấp, thậm chí có thể làm file phình to ra. | - ASCII: Mọi ký tự 'A' đều được biểu diễn bằng mã 65, bất kể nó xuất hiện ở đâu. <br/> - Morse code: Chữ 'E' (phổ biến nhất trong tiếng Anh) luôn là "." (1 ký tự), còn chữ 'Q' (ít phổ biến) luôn là "--.-" (4 ký tự), bất kể ngữ cảnh. |
| Dynamic Model <br/> (Mô hình động) | Thực hiện một lượt duyệt trước toàn bộ dữ liệu đầu vào để phân tích thống kê và tạo ra một mô hình tối ưu cho riêng tệp đó. Sau đó mới tiến hành nén bằng mô hình vừa tạo. | Tối ưu hơn Static. Mô hình được thiết kế riêng cho dữ liệu cần nén, nên tỷ lệ nén thường rất tốt. | - Chậm: Cần 2 lượt xử lý (lượt 1: phân tích, lượt 2: nén). <br/> - Phải truyền/tải kèm mô hình: Để giải nén, bên nhận cũng phải có cái mô hình đó. Điều này làm giảm phần nào hiệu quả nén vì kích thước của chính mô hình cũng phải được tính vào. | Huffman coding động. Thuật toán đọc toàn bộ file, đếm tần suất các ký tự, xây dựng cây Huffman, rồi dùng cây đó để mã hóa file. |
| Adaptive Model <br/> (Mô Hình Thích Nghi) | Mô hình bắt đầu từ một trạng thái đơn giản/mặc định và tự động học & cập nhật chính nó trong suốt quá trình nén. Nó thích nghi với nội dung của dữ liệu đầu vào. | - Mô hình hóa chính xác hơn: Nó bắt chước được các mẫu hình xuất hiện cục bộ trong dữ liệu. Ví dụ, một đoạn văn nói về "biology" sẽ có từ điển chứa "bio", "log",... còn đoạn khác nói về "company" sẽ có "com", "pan",... <br/> - Chỉ cần 1 lượt duyệt: Vừa đọc vừa nén vừa học, không cần lượt phân tích trước. <br/> - Không cần truyền mô hình: Đây là điểm mạnh nhất. Cả bên nén và giải nén đều xây dựng từ điển một cách đồng bộ dựa trên dữ liệu đã xử lý trước đó, nên không cần phải gửi kèm từ điển. | Giải mã phải bắt đầu từ đầu: Bạn không thể giải nén từ giữa một file. Bộ giải nén phải xây dựng lại từ điển từng bước một, y hệt như bộ nén đã làm. Nếu bị mất một phần dữ liệu, phần phía sau có thể không giải mã được. | Thuật toán LZW |

---

## 2. Minh họa quá trình nén LZW

### Tổng quan về Bảng từ điểm (Dictionary)

Đầu tiên, thuật toán khởi tạo một từ điển chứa tất cả các ký tự đơn cơ bản. Trong ví dụ này, các ký tự được quy ước mã hóa như sau:

- `A` = 41 (hexadecimal)
- `B` = 42
- `C` = 43
- `D` = 44
- `R` = 52

