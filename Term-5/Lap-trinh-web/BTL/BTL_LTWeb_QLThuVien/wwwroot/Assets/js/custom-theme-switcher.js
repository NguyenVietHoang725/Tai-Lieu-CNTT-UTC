/**
 * Custom Theme Switcher Script for Sneat Template
 * ------------------------------------------------
 * FIX: Đảm bảo theme áp dụng cho MỌI trang, ngay cả khi không có bảng dữ liệu.
 */

'use strict';

document.addEventListener('DOMContentLoaded', function () {
    // 1. Khai báo các biến và tham chiếu DOM
    const themeToggle = document.getElementById('theme-toggle'); // Chỉ cần thiết nếu người dùng muốn chuyển đổi
    const htmlElement = document.documentElement; // Tham chiếu đến thẻ <html> (CẦN THIẾT)
    const localStorageKey = 'theme';
    const lightClass = 'light-style';
    const darkClass = 'dark-style';
    const lightTheme = 'theme-light-custom';
    const darkTheme = 'theme-dark-custom';

    // Tham chiếu DOM cho Bảng và Table Head (CÓ THỂ LÀ NULL)
    const tableElement = document.getElementById('myTable');
    const tableHeadElement = document.getElementById('myTableHead');

    // Các Class của Table
    const tableDarkClass = 'table-dark';
    const tableLightClass = 'table-light';

    // FIX: Không thoát script nếu các phần tử tùy chọn (toggle, table) không tồn tại.
    if (!htmlElement) {
        return;
    }


    // 2. Hàm áp dụng theme
    function applyTheme(theme) {
        // --- LOGIC CỐT LÕI (Áp dụng cho HTML) ---
        htmlElement.classList.remove(lightClass, darkClass);

        if (theme === 'dark') {
            htmlElement.classList.add(darkClass);
            htmlElement.setAttribute('data-theme', darkTheme);
        } else {
            htmlElement.classList.add(lightClass);
            htmlElement.setAttribute('data-theme', lightTheme);
        }

        // --- LOGIC BẢNG (Chỉ chạy nếu bảng tồn tại trên trang) ---
        if (tableElement && tableHeadElement) {
            tableElement.classList.remove(tableDarkClass, tableLightClass);
            tableHeadElement.classList.remove(tableDarkClass, tableLightClass);

            if (theme === 'dark') {
                // Áp dụng .table-dark cho toàn bộ bảng (kiểm soát tbody)
                tableElement.classList.add(tableDarkClass);
                // Áp dụng .table-light cho thead (tương phản: nền vàng, chữ xanh đậm)
                tableHeadElement.classList.add(tableLightClass);
            } else {
                // Áp dụng .table-light cho toàn bộ bảng (mặc định light)
                tableElement.classList.add(tableLightClass);
                // Áp dụng .table-dark cho thead (tương phản: nền xanh đậm, chữ vàng)
                tableHeadElement.classList.add(tableDarkClass);
            }
        }

        // --- LOGIC TOGGLE (Chỉ chạy nếu nút toggle tồn tại trên Navbar) ---
        if (themeToggle) {
            themeToggle.checked = (theme === 'dark');
        }

        // Lưu trạng thái mới vào Local Storage
        localStorage.setItem(localStorageKey, theme);
        window.dispatchEvent(new Event('resize'));
    }

    // 3. Tải theme đã lưu (khi trang tải)
    let savedTheme = localStorage.getItem(localStorageKey);
    let initialTheme;

    if (savedTheme) {
        initialTheme = savedTheme;
    } else {
        // Đọc trạng thái mặc định từ HTML nếu chưa có theme nào được lưu
        initialTheme = htmlElement.classList.contains(darkClass) ? 'dark' : 'light';
    }

    applyTheme(initialTheme);

    // 4. Xử lý sự kiện chuyển đổi (Chỉ gán listener nếu nút toggle tồn tại)
    if (themeToggle) {
        themeToggle.addEventListener('change', function () {
            const newTheme = themeToggle.checked ? 'dark' : 'light';
            applyTheme(newTheme);
        });
    }
});
