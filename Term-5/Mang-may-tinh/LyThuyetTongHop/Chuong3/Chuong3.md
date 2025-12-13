# Chương 3: Định tuyến

---

## Phần 1: Tầng mạng

### 1. Vai trò và Chức năng trọng tâm

Tầng mạng (Layer 3 trong mô hình OSI) đóng vai trò then chốt trong việc giúp dữ liệu có thể **đi từ máy tính này ở mạng này đến máy tính khác ở một mạng hoàn toàn khác** trên toàn cầu. Nó thường được ví như "**hệ thống định vị và vận chuyển**" của Internet.

Các chức năng cốt lõi của tầng mạng bao gồm:

1. **Đánh địa chỉ (Addressing)**:

- Mỗi thiết bị mạng (máy tính, router, server...) khi tham gia vào Internet đều được gán một địa chỉ logic duy nhất, gọi là địa chỉ IP. Địa chỉ này giống như địa chỉ nhà của bạn, giúp xác định chính xác thiết bị trên toàn cầu.

2. **Đóng gói (Encapsulation) & Mở gói (Decapsulation)**:

- Đóng gói: Tầng mạng nhận dữ liệu từ tầng vận chuyển (Transport Layer), thêm vào một Header chứa thông tin quan trọng như địa chỉ IP nguồn, địa chỉ IP đích. Khi đó, dữ liệu trở thành một Gói tin (Packet).

- Mở gói: Khi gói tin đến đích, tầng mạng tại đích sẽ tháo bỏ header này và chuyển dữ liệu lên cho tầng vận chuyển xử lý.

3. **Định tuyến (Routing)**:

- Đây là quá trình xác định con đường tối ưu nhất để gói tin đi từ nguồn đến đích thông qua một loạt các mạng trung gian. Công việc này do các thiết bị đặc biệt gọi là Router (bộ định tuyến) đảm nhận.

4. **Chuyển tiếp (Forwarding)**:

- Sau khi đường đi đã được xác định (bởi quá trình Định tuyến), việc chuyển tiếp là hành động thực tế của router: nhận một gói tin từ một cổng (interface) và gửi nó ra một cổng khác dẫn về phía đích.

Tóm tắt mối quan hệ: Định tuyến là lập bản đồ, Chuyển tiếp là lái xe theo bản đồ đó.

### 2. Phân loại giao thức tầng mạng 

Các giao thức ở tầng này có thể được phân loại rõ ràng dựa trên chức năng của chúng.

1. **Giao thức được định tuyến (Routed Protocols)**
- Vai trò: Đây là các giao thức mang dữ liệu thực tế của người dùng (như email, web, file...). Chúng định nghĩa cấu trúc địa chỉ và định dạng gói tin để dữ liệu có thể được chuyển tiếp qua mạng.

- Ví dụ điển hình:
  - IPv4 / IPv6 (Internet Protocol): Là giao thức nền tảng của Internet ngày nay.
  - Các giao thức cũ, ít dùng: IPX (Novell), AppleTalk (Apple). Các giao thức này giúp ta hiểu rằng IP không phải là giao thức duy nhất, nhưng hiện tại nó là chuẩn toàn cầu.

2. **Giao thức định tuyến (Routing Protocols)**
- Vai trò: Các giao thức này KHÔNG mang dữ liệu người dùng. Chúng hoạt động "hậu trường" giữa các router, dùng để trao đổi thông tin về đường đi (gọi là bảng định tuyến - routing table). Nhờ đó, các router mới "biết đường" để chuyển tiếp các gói tin của các Routed Protocols.

- Phân loại theo thuật toán & phạm vi:
  - RIP (Routing Information Protocol): Đơn giản, dễ cấu hình, phù hợp mạng nhỏ.
  - OSPF (Open Shortest Path First): Phổ biến trong các mạng doanh nghiệp lớn, sử dụng thuật toán link-state.
  - BGP (Border Gateway Protocol): Là "giao thức của Internet", dùng để trao đổi đường đi giữa các hệ thống tự trị (AS) khác nhau.
  - EIGRP (Enhanced Interior Gateway Routing Protocol): Giao thức độc quyền của Cisco, rất hiệu quả.

3. **Các giao thức hỗ trợ khác**
Đây là những giao thức "trợ lý" không thể thiếu, hỗ trợ cho hoạt động chính của IP.
- ICMP (Internet Control Message Protocol): Dùng để gửi các thông báo lỗi và kiểm tra trạng thái mạng (ví dụ: lệnh ping và tracert sử dụng ICMP).
- ARP (Address Resolution Protocol): Giúp tìm ra địa chỉ vật lý (MAC Address) của một thiết bị khi đã biết địa chỉ IP của nó trong cùng mạng nội bộ (LAN).
- IGMP (Internet Group Management Protocol): Quản lý việc tham gia và rời khỏi các nhóm multicast (phát đa hướng).

### 3. So Sánh Trực Quan: Giao thức được định tuyến vs. Giao thức định tuyến

Đây là sự phân biệt quan trọng nhất trong chương này. Bạn có thể hình dung qua bảng so sánh dưới đây:

| Tiêu chí so sánh | Giao thức được định tuyến (Routed Protocols) | Giao thức định tuyến (Routing Protocols) |
|-|-|-|
| Vai trò chính | Vận chuyển dữ liệu người dùng (data traffic). Là "hành khách" trên mạng. | Trao đổi thông tin đường đi (route information) giữa các router. Là "hệ thống tín hiệu và bản đồ" cho router. |
| Đối tượng sử dụng | Được sử dụng giữa các máy tính đầu cuối (end devices). | Chỉ được sử dụng giữa các router (các thiết bị Layer 3). |
| Ví dụ | IP (IPv4, IPv6) - Giống như chiếc xe tải chở hàng. | RIP, OSPF, BGP, EIGRP - Giống như bản đồ GPS và hệ thống liên lạc giữa các trạm trung chuyển. |
| Hoạt động | Định nghĩa cấu trúc địa chỉ và định dạng gói tin. | Định nghĩa các quy tắc để router học, cập nhật và quảng bá các đường đi. |

Kết luận: Giao thức định tuyến (Routing) điều hướng cho Giao thức được định tuyến (Routed).

---

## Phần 2: Giao thức Internet (IPv4 / IPv6)

### 1. Bản chất của Giao thức Internet (IP)

IP là giao thức Routed Protocol quan trọng nhất tại tầng mạng, đóng vai trò là "người vận chuyển" cho mọi dữ liệu trên Internet. Ba đặc tính cốt lõi của nó là:

- Không kết nối (Connectionless):
  - Ý nghĩa: Mỗi gói tin IP được gửi đi một cách độc lập, không cần thiết lập một kết nối riêng với máy đích trước khi truyền dữ liệu. Giống như gửi thư bưu điện: bạn bỏ thư vào hộp mà không cần báo trước cho người nhận, mỗi bức thư là độc lập.
  - Ưu điểm: Tốc độ xử lý nhanh, đơn giản.

- Nỗ lực tối đa (Best-Effort / Unreliable):
  - Ý nghĩa: IP không đảm bảo rằng gói tin sẽ đến đích. Nó sẽ cố gắng hết sức để chuyển gói tin đi, nhưng nếu xảy ra lỗi (như mất gói tin, hết thời gian TTL), IP sẽ không thông báo cho người gửi và cũng không gửi lại gói tin.
  - Lưu ý: Tính "không tin cậy" này không có nghĩa là IP kém hiệu quả. Thay vào đó, nhiệm vụ đảm bảo độ tin cậy (như phát hiện mất gói và gửi lại) được giao cho tầng giao vận (Transport Layer - ví dụ TCP) hoặc cho ứng dụng tự xử lý (ví dụ UDP).

- Độc lập với môi trường truyền dẫn (Media Independent):
  - Ý nghĩa: IP hoạt động mà không quan tâm đến môi trường vật lý bên dưới (cáp đồng, cáp quang, sóng wifi, vệ tinh...). Nó chỉ quan tâm đến địa chỉ IP logic. Việc chuyển đổi giữa các môi trường vật lý khác nhau là nhiệm vụ của các tầng thấp hơn (như Data Link Layer).

### 2. Quá trình Đóng gói IP (IP Encapsulation)

- Hành động: Khi nhận được một Segment từ tầng giao vận (ví dụ từ TCP hoặc UDP), IP sẽ thực hiện đóng gói bằng cách thêm một Header (phần tiêu đề) vào phía trước dữ liệu đó. Dữ liệu sau khi đóng gói được gọi là một Packet (gói tin) hoặc Datagram.

- Địa chỉ IP là cố định: Một điểm quan trọng là địa chỉ IP nguồn và đích trong header không thay đổi trong suốt hành trình từ nguồn đến đích (trừ khi có NAT - như slide đã nói, ta tạm bỏ qua ở đây).

- Kiểm tra bởi các thiết bị Layer 3: Mỗi khi gói tin đi qua một router (thiết bị lớp 3), router đó sẽ kiểm tra header IP (chủ yếu là địa chỉ đích) để quyết định đường đi tiếp theo.

### 3. Chi tiết Định dạng Tiêu đề Gói tin IPv4

Tiêu đề IPv4 có độ dài thay đổi từ 20 đến 60 byte, trong đó 20 byte là phần cố định bắt buộc.

| Nhóm chức năng | Tên Trường | Độ dài | Mô tả và Ý nghĩa Chi tiết |
|-|-|-|-|
| Thông tin cơ bản | Version | 4 bits | Phiên bản IP. Với IPv4, giá trị này luôn là 4. |
| | IHL (Header Length) | 4 bits | Chỉ ra tổng độ dài của header, tính theo đơn vị từ 32-bit (4 bytes). Giá trị tối thiểu là 5 (5 * 4 = 20 byte). Giá trị tối đa là 15 (15 * 4 = 60 byte). Giá trị này thay đổi nếu có trường Options. |
| | Type of Service (ToS) | 8 bits | Dùng để đánh dấu mức độ ưu tiên và loại dịch vụ cho gói tin (Ví dụ: ưu tiên cho voice, video). Hiện ít được sử dụng theo cách cũ. |
| | Total Length | 16 bits | Tổng độ dài của cả gói tin (bao gồm cả header và dữ liệu). Giá trị tối đa là 65,535 byte. |
| Điều khiển phân mảnh | Identification | 16 bits | Số nhận dạng duy nhất cho một gói tin. Nếu gói tin bị phân mảnh, tất cả các mảnh sẽ có cùng Identification này để máy đích có thể ghép lại chính xác. |
| | Flags | 3 bits | Cờ điều khiển phân mảnh: </br>- Bit 0: Dự trữ (phải là 0). </br>- Bit 1 (DF - Don't Fragment): 0 = Cho phép phân mảnh, 1 = Không cho phép phân mảnh. Nếu router cần phân mảnh nhưng cờ này được bật (1), gói tin sẽ bị hủy và gửi thông báo lỗi ICMP về máy nguồn. </br>- Bit 2 (MF - More Fragments): 0 = Đây là mảnh cuối cùng, 1 = Vẫn còn mảnh khác phía sau. |
| | Fragment Offset | 13 bits | Chỉ ra vị trí của mảnh hiện tại so với đầu dữ liệu (payload) của gói tin gốc. Giúp máy đích sắp xếp lại các mảnh đúng thứ tự. |
| Điều khiển vòng đời & Định tuyến | Time to Live (TTL) | 8 bits | Bộ đếm "số bước nhảy" tối đa. Mỗi khi gói tin đi qua một router, giá trị này giảm đi 1. Khi TTL = 0, gói tin bị hủy và router gửi thông báo lỗi (ICMP Time Exceeded) về máy nguồn. Ngăn chặn gói tin đi lòng vòng vô hạn trên mạng. |
| | Protocol | 8 bits | Chỉ ra giao thức ở tầng trên kế tiếp (tầng giao vận) đang được đóng gói bên trong. Ví dụ: 6 = TCP, 17 = UDP, 1 = ICMP. Router không cần quan tâm đến trường này, nó dành cho máy đích. |
| |Header Checksum | 16 bits | Giá trị kiểm tra lỗi cho phần header. Lưu ý quan trọng: Nó chỉ kiểm tra header, không kiểm tra dữ liệu. Mỗi router trên đường đi phải tính lại checksum này vì trường TTL đã thay đổi. Nếu checksum không khớp, gói tin bị hủy. | 
| Địa chỉ | Source IP Address | 32 bits | Địa chỉ IPv4 của thiết bị gửi. |
| | Destination IP Address | 32 bits | Địa chỉ IPv4 của thiết bị nhận. |
| Tùy chọn | Options + Padding | Độ dài thay đổi | Các tùy chọn đặc biệt (ít dùng). Trường Padding (đệm) được thêm các bit 0 để đảm bảo phần header luôn kết thúc ở biên 32-bit (vì trường IHL tính theo đơn vị 32-bit). |

### 4. Chi tiết Định dạng Tiêu đề Gói Tin IPv6

IPv6 được thiết kế để khắc phục những hạn chế của IPv4, đặc biệt là tình trạng thiếu địa chỉ. Một trong những thay đổi lớn nhất là cấu trúc header được đơn giản hóa và mô-đun hóa.

Cấu trúc chính: Tiêu đề IPv6 được chia thành hai phần:
- Tiêu đề cơ bản (Basic Header): Cố định 40 byte, chứa các thông tin thiết yếu.
- Tiêu đề mở rộng (Extension Headers): Tùy chọn, được nối tiếp theo tiêu đề cơ bản, mỗi tiêu đề phục vụ một mục đích cụ thể.

#### 4.1. Tiêu đề cơ bản IPv6 (Basic Headers)
Tiêu đề cơ bản của IPv6 đơn giản hơn rất nhiều so với IPv4, giúp router xử lý nhanh hơn.

| Nhóm chức năng | Tên Trường | Độ dài | Mô tả và Ý nghĩa Chi tiết |
|-|-|-|-|
| Thông tin cơ bản | Version | 4 bits | Phiên bản IP. Với IPv6, giá trị này luôn là 6. |
| | Traffic Class | 8 bits | Tương tự trường ToS trong IPv4, dùng để đánh dấu mức độ ưu tiên cho gói tin (Ví dụ: ưu tiên cho voice, video). |
| | Flow Label | 20 bits | Một tính năng mới của IPv6. Dùng để đánh dấu một chuỗi các gói tin (luồng - flow) yêu cầu cùng một cách thức xử lý đặc biệt (ví dụ: cho các kết nối real-time). Giúp router xử lý nhanh chóng mà không cần kiểm tra sâu bên trong gói tin. |
| Kích thước & Định tuyến | Payload Length | 16 bits | Chỉ ra độ dài của phần dữ liệu (payload) theo ngay sau tiêu đề cơ bản. Payload này có thể bao gồm cả các Tiêu đề mở rộng và dữ liệu thực tế của tầng trên. So sánh: IPv4's "Total Length" bao gồm luôn cả header của nó. |
| | Next Header | 8 bits | Đây là trường then chốt. Nó xác định loại header ngay lập tức theo sau tiêu đề cơ bản. Nó có thể là: </br>- Giao thức tầng trên (ví dụ: 6=TCP, 17=UDP, 58=ICMPv6) - giống trường Protocol trong IPv4. </br>- Một Tiêu đề mở rộng (ví dụ: 0=Hop-by-Hop, 43=Routing, 44=Fragment). |
| | Hop Limit | 8 bits | Hoàn toàn tương tự trường TTL trong IPv4. Giá trị giảm 1 tại mỗi router. Khi bằng 0, gói tin bị hủy. |
| Địa chỉ | Source Address | 128 bits | Địa chỉ IPv6 của thiết bị gửi. |
| | | Destination Address | 128 bits | Địa chỉ IPv6 của thiết bị nhận. Lưu ý: Nếu có tiêu đề "Routing", đây có thể là địa chỉ của router kế tiếp, không phải đích cuối. |

#### 4.2. Tiêu đề Mở rộng IPv6 (Extension Headers)

Đây là điểm đặc trưng của IPv6. Thay vì nhồi nhét mọi tùy chọn vào một header duy nhất như IPv4, IPv6 sử dụng một chuỗi các tiêu đề mở rộng được liên kết với nhau. Điều này làm cho việc xử lý hiệu quả hơn vì router chỉ cần xem xét các tiêu đề thực sự cần thiết.

| Tên Tiêu đề Mở rộng | Giá trị Next Header | Độ dài | Mô tả và Ý nghĩa Chi tiết |
|-|-|-|-|
| Hop-by-Hop Options | 0 | 8 bits | Chứa thông tin mà mọi router trên đường đi phải xem xét. Ví dụ: hỗ trợ cho các gói tin Jumbogram (rất lớn). |
| Routing Header | 43 | 8 bits | Cho phép nguồn xác định một phần hoặc toàn bộ đường đi mà gói tin phải đi qua (tương tự Source Routing trong IPv4). |
| Fragment Header | 44 | 8 bits | Trong IPv6, chỉ máy nguồn mới được phép phân mảnh gói tin. Router trên đường đi không được phân mảnh. Nếu gói tin quá lớn, router sẽ hủy và gửi thông báo lỗi yêu cầu nguồn phân mảnh. Tiêu đề này chứa thông tin phân mảnh nếu cần. |
| Destination Options | 60 | 8 bits	| Chứa thông tin chỉ cần xử lý bởi máy đích. |
| Authentication Header (AH) | 51 | 8 bits | Cung cấp tính toàn vẹn và xác thực cho gói tin. |
| Encapsulating Security Payload (ESP) | 50 | 8 bits |Cung cấp tính bảo mật (mã hóa) cho dữ liệu. |

Cách thức hoạt động của chuỗi tiêu đề: Các tiêu đề được xếp thành một chuỗi. Trường Next Header của tiêu đề trước sẽ chỉ ra loại tiêu đề ngay sau nó. Chuỗi kết thúc khi Next Header chỉ đến một giao thức tầng trên (như TCP/UDP).

