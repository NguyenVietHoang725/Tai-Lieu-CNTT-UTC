# Cấu hình Switch cơ bản
---

## 1. Các chế độ thực hiện lệnh
Switch C2960-24TT có 3 chế độ thực hiện lệnh chính, mỗi chế độ cho phép bạn thực hiện các tác vụ với quyền hạn khác nhau:

| Chế độ | Mô tả | Dấu hiệu nhận biết | Chức năng | 
|--------|-------|--------------------|-----------|
| Chế độ Người dùng <br/> (User EXEC Mode) | Đây là chế độ đầu tiên bạn truy cập vào khi đăng nhập | Dấu nhắc lệnh kết thúc bằng ký tự `>` (ví dụ `Switch>`) | Chỉ cho phép sử dụng một số lệnh giám sát cơ bản, không thể thay đổi cấu hình |
| Chế độ Đặc quyền <br/> (Privileged EXEC Mode) | Được truy cập từ chế độ người dùng bằng lệnh `enable` | Dấu nhắc lệnh kết thúc bằng ký tự `#` (ví dụ `Switch#`) | Cho phép truy cập vào tất cả các lệnh để giám sát, kiểm tra và sao lưu cấu hình. Đây là cửa ngõ để vào chế độ cấu hình | 
| Chế độ Cấu hình Toàn cục <br/> (Global Configuration Mode) | Được truy cập từ chế độ Đặc quyền bằng lệnh `configure terminal` | Dấu nhắc lệnh có dạng `Switch(config)#` | Đây là chế độ dùng để thay đổi cấu hình cho toàn bộ thiết bị | 

## 2. Hiển thị thông tin trên Switch

### 2.1. Các câu lệnh phổ thông

#### Di chuyển giữa các chế độ

Bạn có thể hình dung việc di chuyển giữa các chế độ giống như một cái thang, và các lệnh sau là cách để bạn leo lên hoặc tụt xuống.

1. Lệnh để đi lên (tăng quyền hạn)

| Lệnh | Công dụng | Cách dùng |
|-|-|-|
| `enable` | Chuyển từ Chế độ Người dùng (`Switch>`) sang Chế độ Đặc quyền (`Switch#`) | Tại dấu nhắc `Switch>`, bạn gõ `enable` và nhấn Enter |
| `configure terminal` (hoặc viết tắt `conf t`) | Chuyển từ Chế độ Đặc quyền (`Switch>`) sang Chế độ Cấu hình Toàn cục (`Switch(config)#`) | Tại dấu nhắc `Switch#`, bạn gõ `conf t` và nhấn Enter |

2. Lệnh để đi xuống (giảm quyền hạn hoặc thoát)

| Lệnh | Công dụng | Cách dùng |
|-|-|-|
| `disable` | Chuyển từ Chế độ Đặc quyền (`Switch#`) sang Chế độ Người dùng (`Switch>`) | Tại dấu nhắc `Switch#`, bạn gõ `disable` và nhấn Enter |
| `exit` | Thoát khỏi chế độ hiện tại để trở về chế độ trước đó. |Tại dấu `Switch(config)#`, gõ `exit` sẽ trở về `Switch#` <br/> Tại dấu nhắc `Switch#`, bạn gõ `exit` (hoặc `logout`) sẽ thoát hoàn toàn khỏi thiết bị |
| `end` hoặc tổ hợp phím `Ctrl + Z` |  Thoát khỏi bất kỳ chế độ cấu hình nào và trở về Chế độ Đặc quyền (`Switch#`), là cách nhanh nhất để kết thúc việc cấu hình. |  | 

#### Trợ giúp

Đây là lệnh mạnh mẽ và hữu ích nhất, đặc biệt khi bạn không nhớ rõ lệnh.

- Công dụng: Hiển thị danh sách tất cả các lệnh khả dụng trong chế độ hiện tại hoặc gợi ý cách hoàn thành một lệnh.

- Cách dùng:
  - Gõ ? để xem tất cả lệnh có thể dùng.
  - Gõ một vài ký tự rồi gõ ? (ví dụ: sh?) để xem các lệnh bắt đầu bằng những ký tự đó.
  - Gõ một lệnh rồi gõ ? (ví dụ: show ?) để xem các tham số hoặc từ khóa tiếp theo có thể điền vào.
- Chú ý khi dùng ?:
  - Màn hình có thể hiển thị nhiều thông tin. Nhấn Phím Cách (SPACE) để xem trang tiếp theo.
  - Nhấn Ctrl + Z để thoát khỏi danh sách trợ giúp và quay lại dòng lệnh.

### 2.2. Các lệnh `Show` 

#### Trong Chế độ Người dùng

Ở chế độ `Switch>`, bạn có thể sử dụng các lệnh `show` cơ bản sau để giám sát và xem trạng thái hoạt động của thiết bị. Các lệnh này không làm thay đổi cấu hình.

- `show arp`: Hiển thị bảng ARP của switch, cho biết mối quan hệ giữa địa chỉ IP và địa chỉ MAC trên các VLAN.

- `show mac address-table` (hoặc show mac-address-table): Hiển thị bảng chuyển tiếp MAC. Đây là bảng quyết định switch sẽ chuyển frame đến cổng (port) nào dựa trên địa chỉ MAC đích.

- `show interfaces` (viết tắt: show int): Hiển thị thông tin chi tiết về trạng thái và thống kê của tất cả các cổng trên switch.

- `show interfaces fastEthernet 0/1` (viết tắt: show int fa0/1): Hiển thị thông tin chi tiết của riêng cổng FastEthernet 0/1.

- `show interfaces fastEthernet 0/3 status` (viết tắt: show int fa0/3 stat): Hiển thị bảng tóm tắt trạng thái của cổng, bao gồm: Tên cổng, Trạng thái (up/down), VLAN, Tốc độ, Chế độ duplex. Rất hữu ích để kiểm tra nhanh.

- `show vlan`: Hiển thị danh sách đầy đủ các VLAN trên switch, bao gồm ID, Tên, Trạng thái và các cổng thành viên.

- `show vlan brief` (viết tắt: show vlan br): Hiển thị bảng tóm tắt các VLAN, ngắn gọn và dễ xem hơn.

- `show vlan id [số_vlan]` (ví dụ: show vlan id 1): Chỉ hiển thị thông tin chi tiết của một VLAN cụ thể mà bạn chỉ định.

#### Trong Chế độ Đặc quyền
Khi bạn đã vào chế độ Switch#, bạn có quyền truy cập vào tất cả các lệnh trên và lệnh quan trọng sau:

- `show running-config` (viết tắt: show run): Đây là lệnh quan trọng và được dùng nhiều nhất. Nó hiển thị toàn bộ cấu hình đang được thiết bị sử dụng trong bộ nhớ RAM. Bất kỳ thay đổi cấu hình mới nào bạn vừa làm sẽ hiển thị ở đây.

#### Tóm tắt cách dùng

Bước 1: Chuyển sang chế độ có đủ quyền để xem:
- `Switch> enable` (để vào chế độ `#` nếu muốn dùng `show run`)

Bước 2: Gõ lệnh show bạn cần.
- Ví dụ: Để xem cấu hình hiện tại, gõ: `Switch# show running-config`
- Ví dụ: Để kiểm tra nhanh trạng thái các cổng, gõ: `Switch> show int status`

---
## 3. Cấu hình Switch cơ bản

### Tên thiết bị

Tên thiết bị (hostname) giúp nhận diện switch trong mạng và xuất hiện ở đầu dấu nhắc lệnh.
- Tên mặc định: Khi mới mua, switch có tên mặc định là "`Switch`".
- Mục đích: Đặt tên riêng (ví dụ: `SW-TANG1`, `SW-PHONGKT`) để dễ quản lý và phân biệt nhiều thiết bị trong mạng.

Các bước thực hiện:

1. Vào Chế độ Cấu hình Toàn cục:
- `Switch> enable`
- `Switch# configure terminal` (hoặc `conf t`)

2. Đặt tên mới cho thiết bị:
- `Switch(config)# hostname [Tên_mới]`
- Ví dụ: `Switch(config)# hostname SW-PHONG1`
- Ngay lập tức, dấu nhắc lệnh sẽ đổi thành: `SW-PHONG1(config)#`

3. Khôi phục về tên mặc định:
- `SW-PHONG1(config)# no hostname`
- Dấu nhắc lệnh sẽ trở lại thành `Switch(config)#`

### Đặt mật khẩu truy cập Chế độ đặc quyền (`Enable Secret`)

Đây là bước bảo mật quan trọng, yêu cầu người dùng phải nhập mật khẩu để từ chế độ User (`>`) vào chế độ Đặc quyền (`#`).

Các bước thực hiện:

1. Vào Chế độ Cấu hình Toàn cục (nếu chưa ở đó):
- `Switch> enable`
- `Switch# configure terminal`

2. Đặt mật khẩu:
- Sử dụng lệnh `enable secret` (mật khẩu sẽ được mã hóa).
- `Switch(config)# enable secret [mật_khẩu_của_bạn]`
- Ví dụ: `Switch(config)# enable secret Cisc0@123`

3. Kiểm tra:
- Thoát về chế độ User bằng lệnh disable hoặc exit.
- Khi bạn gõ enable lần sau, hệ thống sẽ yêu cầu nhập mật khẩu bạn vừa đặt.

### Lưu cấu hình

**CẢNH BÁO QUAN TRỌNG:** Mọi thay đổi cấu hình (đặt tên, mật khẩu...) chỉ có tác dụng trong bộ nhớ RAM (running-config). Nếu bạn tắt nguồn switch, mọi cấu hình sẽ bị mất. Để lưu lại vĩnh viễn vào bộ nhớ Flash (startup-config), bạn phải thực hiện lệnh sau:

Từ Chế độ Đặc quyền (`#`), gõ:

- `Switch# write memory` (lệnh cổ điển) (viết tắt là `Switch# wr`)

- hoặc `Switch# copy running-config startup-config` (lệnh đầy đủ)

Sau khi lưu, cấu hình của bạn sẽ được giữ nguyên ngay cả khi khởi động lại thiết bị.

### Tóm tắt quy trình 

```cisco
Switch> enable
Switch# configure terminal
Switch(config)# hostname SW-TANG1
SW-TANG1(config)# enable secret MatKhauM@nh123
SW-TANG1(config)# end
SW-TANG1# write memory
```

---
## 4. Quản lý VLAN

### Tạo, Sửa, Xóa

VLAN (Virtual LAN) giúp chia mạng LAN vật lý thành các mạng logic độc lập. Dưới đây là các thao tác cơ bản nhất để quản lý VLAN.

#### Tạo VLAN mới

Ví dụ: Tạo VLAN 2 với tên là VLAN-Test.

Các bước lệnh:
- Vào Chế độ Cấu hình Toàn cục.
- Vào mode cấu hình cho VLAN muốn tạo.
- Đặt tên cho VLAN (tùy chọn, nhưng nên dùng để dễ quản lý).
- Thoát về chế độ đặc quyền và lưu cấu hình.

Cụ thể:
```cisco
Switch> enable
Switch# configure terminal
Switch(config)# vlan 2                         ! Tạo VLAN 2 và chuyển sang mode cấu hình VLAN
Switch(config-vlan)# name VLAN-Test           ! Đặt tên miêu tả cho VLAN
Switch(config-vlan)# end                      ! Thoát thẳng về Privileged EXEC mode
Switch# show vlan brief                       ! Kiểm tra xem VLAN 2 đã được tạo chưa
```

#### Sửa VLAN

Ví dụ: Đổi tên VLAN 2 từ "VLAN-Test" thành "VLAN-Test-02".

Các bước lệnh: Tương tự như tạo VLAN, bạn chỉ cần truy cập vào VLAN đã tồn tại và đặt tên mới. Tên mới sẽ ghi đè lên tên cũ.

Cụ thể:
```cisco
Switch> enable
Switch# configure terminal
Switch(config)# vlan 2                         ! Chọn VLAN 2 cần sửa
Switch(config-vlan)# name VLAN-Test-02        ! Đặt tên mới
Switch(config-vlan)# end                      ! Thoát
Switch# show vlan brief                       ! Kiểm tra kết quả
```

#### Xóa VLAN
Ví dụ: Xóa VLAN 2 ("VLAN-Test-02") khỏi cơ sở dữ liệu.

Cảnh báo: Khi xóa một VLAN, các cổng đã được gán vào VLAN đó sẽ trở thành "inactive" cho đến khi bạn gán chúng vào một VLAN khác. Cổng mặc định thuộc VLAN 1.

Các bước lệnh:
```cisco
Switch> enable
Switch# configure terminal
Switch(config)# no vlan 2          ! Lệnh xóa VLAN 2
Switch(config)# end                ! Thoát
Switch# show vlan brief            ! Kiểm tra xem VLAN 2 đã biến mất chưa
```

#### Lưu ý quan trọng

Sau khi thực hiện bất kỳ thay đổi cấu hình nào (tạo, sửa, xóa VLAN), bạn PHẢI lưu cấu hình vào bộ nhớ khởi động (startup-config) để không bị mất khi thiết bị tắt nguồn.

```cisco
Switch# write memory    ! Lệnh ngắn gọn
! Hoặc
Switch# copy running-config startup-config   ! Lệnh đầy đủ
```

---
## 5. Gán cổng Access vào một VLAN
Cổng Access được sử dụng để kết nối các thiết bị cuối (như máy tính, máy in) vào một VLAN cụ thể. Dưới đây là quy trình chung và các ví dụ minh họa.

Quy Trình 4 Bước Để Gán Một Cổng Access:
1. Vào chế độ cấu hình cho cổng: Chọn cổng vật lý cần cấu hình.
2. Đặt chế độ cho cổng: Khai báo đây là cổng access (không phải trunk).
3. Gán cổng vào VLAN: Chỉ định VLAN mà cổng này sẽ thuộc về.
4. Kiểm tra và lưu cấu hình: Xác minh kết quả và lưu lại.

### Ví dụ 1: Gán một cổng đơn vào VLAN
Nhiệm vụ: Gán cổng FastEthernet 0/20 vào VLAN 20.

```cisco
Switch> enable
Switch# configure terminal
Switch(config)# interface fastEthernet 0/20     ! Bước 1: Chọn cổng
Switch(config-if)# switchport mode access       ! Bước 2: Đặt chế độ Access
Switch(config-if)# switchport access vlan 20    ! Bước 3: Gán vào VLAN 20
Switch(config-if)# end                          ! Thoát
Switch# show vlan brief                         ! Kiểm tra xem cổng đã trong VLAN 20 chưa
```

### Ví dụ 2: Gán nhiều cổng rời rạc vào cùng một VLAN
Nhiệm vụ: Gán các cổng Fa0/11, Fa0/13, Fa0/15 vào VLAN 20.

Các lệnh thực hiện: Sử dụng lệnh interface range để cấu hình hàng loạt.
```cisco
Switch> enable
Switch# configure terminal
Switch(config)# interface range fa0/11, fa0/13, fa0/15  ! Chọn nhiều cổng
Switch(config-if-range)# switchport mode access
Switch(config-if-range)# switchport access vlan 20
Switch(config-if-range)# end
Switch# show vlan brief
```

### Ví dụ 3: Gán một dải cổng liên tục vào cùng một VLAN
Nhiệm vụ: Gán các cổng từ Fa0/6 đến Fa0/9 vào VLAN 20.

Các lệnh thực hiện: Sử dụng dấu gạch ngang - để chọn một khoảng cổng.
```cisco
Switch> enable
Switch# configure terminal
Switch(config)# interface range fa0/6 - 9       ! Chọn dải cổng từ 6 đến 9
Switch(config-if-range)# switchport mode access
Switch(config-if-range)# switchport access vlan 20
Switch(config-if-range)# end
Switch# show vlan brief
```

### Ví dụ 4: Chuyển cổng từ VLAN này sang VLAN khác
Nhiệm vụ: Tạo VLAN 5 (tên ABC) và chuyển cổng Fa0/20 từ VLAN 20 sang VLAN 5.

Các lệnh thực hiện: Việc chuyển VLAN rất đơn giản, chỉ cần gán cổng vào VLAN mới.
```cisco
Switch> enable
Switch# configure terminal

! Tạo VLAN mới nếu nó chưa tồn tại
Switch(config)# vlan 5
Switch(config-vlan)# name ABC
Switch(config-vlan)# exit

! Chuyển cổng sang VLAN mới
Switch(config)# interface fa0/20
Switch(config-if)# switchport access vlan 5     ! Bước quan trọng: gán sang VLAN mới
Switch(config-if)# end
Switch# show vlan brief                         ! Kiểm tra xem cổng giờ đã thuộc VLAN 5
```

### Lưu ý
Kiểm tra: Luôn sử dụng lệnh show vlan brief hoặc show interfaces [tên-cổng] switchport để xác minh cấu hình.

Lưu cấu hình: Đừng quên lưu cấu hình sau khi hoàn tất để tránh bị mất khi khởi động lại.
```cisco
Switch# write memory
```

---
## 6. Đưa cổng về VLAN mặc định (VLAN 1)

Khi muốn xóa cấu hình VLAN hiện tại trên một cổng và đưa nó trở về trạng thái mặc định (thuộc VLAN 1), bạn có 2 cách để thực hiện.

Lưu ý: VLAN mặc định trên switch Cisco là VLAN 1.

### Cách thực hiện
Cách 1: Sử Dụng Lệnh `default interface` (Nhanh và Toàn Diện)
- Công dụng: Lệnh này sẽ xóa toàn bộ cấu hình đặc biệt trên cổng (bao gồm cả cấu hình VLAN, tốc độ, duplex...) và đưa cổng trở về trạng thái mặc định ban đầu.
- Thực hiện trong: Chế độ Cấu hình Toàn cục (config).
- Cú pháp: `default interface [tên-cổng]`

Cách 2: Sử Dụng Lệnh `no switchport access vlan` (Chỉ Xóa Cấu hình VLAN)
- Công dụng: Lệnh này chỉ xóa cấu hình VLAN được gán trước đó, đưa cổng trở lại VLAN 1 mà không ảnh hưởng đến các cấu hình khác (như mô tả, tốc độ...).
- Thực hiện trong: Chế độ Cấu hình Interface (config-if).
- Cú pháp: `no switchport access vlan`

### Ví dụ
Nhiệm vụ: Đưa hai cổng Fa0/11 và Fa0/13 về VLAN mặc định (VLAN 1).

```cisco
Switch> enable
Switch# configure terminal

! Sử dụng Cách 1 (default interface) cho cổng Fa0/11
Switch(config)# default interface fastEthernet 0/11

! Sử dụng Cách 2 (no switchport access vlan) cho cổng Fa0/13
Switch(config)# interface fastEthernet 0/13
Switch(config-if)# no switchport access vlan
Switch(config-if)# end

! Kiểm tra kết quả bằng lệnh show vlan
Switch# show vlan brief
```

Sau khi thực hiện, bạn sẽ thấy các cổng Fa0/11 và Fa0/13 không còn trong VLAN cũ mà đã được chuyển về VLAN 1.

### Khi nào nên dùng cách nào ?

Dùng default interface khi bạn muốn thiết lập lại hoàn toàn một cổng về mặc định của nhà sản xuất.

Dùng no switchport access vlan khi bạn chỉ muốn thay đổi VLAN của cổng về mặc định và giữ nguyên các cấu hình khác.

---
## 7. Cấu hình cổng Trunk
Cổng Trunk được sử dụng để kết nối giữa các switch, cho phép nhiều VLAN truyền lưu lượng qua một liên kết vật lý duy nhất.

Các Bước Cơ Bản Để Tạo Cổng Trunk:
1. Vào chế độ cấu hình cho cổng: Chọn cổng cần cấu hình.
2. Đặt chế độ cho cổng: Khai báo đây là cổng trunk.

### Ví dụ 1: Cấu hình cổng Trunk cơ bản
Nhiệm vụ: Cấu hình cổng GigabitEthernet 0/2 thành cổng trunk (cho phép tất cả VLAN đi qua).

```cisco
Switch> enable
Switch# configure terminal
Switch(config)# interface GigabitEthernet 0/2     ! Bước 1: Chọn cổng
Switch(config-if)# switchport mode trunk          ! Bước 2: Đặt chế độ Trunk
Switch(config-if)# end
Switch# show interfaces Gi0/2 switchport         ! Kiểm tra trạng thái cổng
```

### Ví dụ 2: Cấu hình Trunk nâng cao (Cho phép VLAN cụ thể)
Nhiệm vụ: Cấu hình cổng Gi0/1:
- Là cổng trunk.
- Chỉ cho phép các VLAN 1, 3, 5 đi qua.
- Thêm mô tả "To-SW1-Gi0/1" để dễ quản lý.

```cisco
Switch> enable
Switch# configure terminal
Switch(config)# interface GigabitEthernet 0/1
Switch(config-if)# description To-SW1-Gi0/1       ! Tùy chọn: Thêm mô tả
Switch(config-if)# switchport mode trunk
Switch(config-if)# switchport trunk allowed vlan 1,3,5  ! Chỉ cho phép VLAN 1,3,5
Switch(config-if)# end
Switch# show interfaces trunk                    ! Kiểm tra các VLAN được phép trên trunk
```

### Ví dụ 3: Thay đổi Native VLAN
Nhiệm vụ: Cấu hình cổng Fa0/24:
- Là cổng trunk.
- Đặt Native VLAN thành VLAN 5.

Lưu ý quan trọng:
- Native VLAN là VLAN mà lưu lượng không được gắn thẻ (tag) khi đi qua trunk.
- Mặc định là VLAN 1. Luôn đặt Native VLAN giống nhau ở cả hai đầu trunk để tránh sự cố.

```cisco
Switch> enable
Switch# configure terminal
Switch(config)# interface FastEthernet 0/24
Switch(config-if)# switchport mode trunk
Switch(config-if)# switchport trunk native vlan 5  ! Đặt Native VLAN là 5
Switch(config-if)# end
Switch# show interfaces Fa0/24 switchport         ! Kiểm tra Native VLAN
```

### Lưu ý
Switch layer 2 như 2960 mặc định sử dụng đóng gói dot1q cho trunk, vì vậy không cần lệnh cấu hình đóng gói thủ công.

Luôn lưu cấu hình sau khi hoàn tất:
```cisco
Switch# write memory
```