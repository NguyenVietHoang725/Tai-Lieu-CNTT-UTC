// Các hàm trợ giúp (Helper functions) giữ nguyên
function positionDropdown(header, dropdown) {
    // Logic định vị giữ nguyên
    dropdown.style.visibility = "hidden";
    dropdown.style.opacity = "0";
    dropdown.style.pointerEvents = "none";
    dropdown.classList.add("measuring");

    const headerRect = header.getBoundingClientRect();
    const screenWidth = window.innerWidth;
    const screenHeight = window.innerHeight;

    dropdown.style.position = 'fixed';
    dropdown.style.top = `${headerRect.bottom}px`;
    dropdown.style.left = `${headerRect.left}px`;
    dropdown.style.minWidth = `${headerRect.width}px`;

    if (headerRect.left + dropdown.offsetWidth > screenWidth) {
        dropdown.style.left = 'auto';
        dropdown.style.right = `${screenWidth - headerRect.right}px`;
    }

    if (headerRect.bottom + dropdown.offsetHeight > screenHeight) {
        dropdown.style.top = `${headerRect.top - dropdown.offsetHeight}px`;
    }

    dropdown.classList.remove("measuring");
    dropdown.style.visibility = "";
    dropdown.style.opacity = "";
    dropdown.style.pointerEvents = "";
}

function closeAllFilters() {
    document.querySelectorAll(".filter-dropdown.show").forEach(d => d.classList.remove("show"));
}

function applyFilter(filterName, filterValue) {
    const url = new URL(window.location.href);
    if (filterValue) url.searchParams.set(filterName, filterValue);
    else url.searchParams.delete(filterName);
    url.searchParams.set("page", "1");
    window.location.href = url.toString();
}

// =========================================================================
// SỬ DỤNG EVENT DELEGATION
// =========================================================================

document.addEventListener("DOMContentLoaded", function () {
    // Logic đóng dropdown khi resize giữ nguyên
    window.addEventListener("resize", () => closeAllFilters());

    // Logic đóng khi click ra ngoài
    window.addEventListener("click", (e) => {
        // Kiểm tra nếu click không phải là bên trong bất kỳ dropdown nào
        if (!e.target.closest(".filter-dropdown") && !e.target.closest(".filterable-header")) {
            closeAllFilters();
        }
    });

    // Logic đóng khi scroll (chỉ đóng nếu scroll không nằm trong dropdown)
    window.addEventListener('scroll', function (e) {
        const dropdown = document.querySelector(".filter-dropdown.show");
        // Kiểm tra xem sự kiện scroll có phải là từ chính dropdown hay không
        if (dropdown && e.target.closest('.filter-dropdown') !== dropdown) {
            closeAllFilters();
        }
    }, true);
});


// Lắng nghe sự kiện CLICK trên toàn bộ tài liệu
document.addEventListener("click", function (event) {
    const headerClicked = event.target.closest(".filterable-header");
    const optionClicked = event.target.closest(".filter-option");

    // 1. Xử lý khi click vào tiêu đề cột (.filterable-header)
    if (headerClicked) {
        // Nếu click vào header, nhưng không phải click vào dropdown đang mở bên trong
        if (!event.target.closest(".filter-dropdown.show")) {
            event.stopPropagation();

            const dropdown = headerClicked.querySelector('.filter-dropdown');
            if (!dropdown) return;

            const isOpening = !dropdown.classList.contains("show");

            closeAllFilters(); // Đóng tất cả các bộ lọc khác

            if (isOpening) {
                positionDropdown(headerClicked, dropdown);
                dropdown.classList.add("show"); // Mở bộ lọc hiện tại
            }
        }
    }

    // 2. Xử lý khi click vào tùy chọn (.filter-option)
    if (optionClicked) {
        event.stopPropagation();

        const header = optionClicked.closest(".filterable-header");
        const filterName = header.dataset.filter;
        const filterValue = optionClicked.dataset.value;

        applyFilter(filterName, filterValue); // Áp dụng bộ lọc và chuyển hướng
        // closeAllFilters() sẽ được gọi gián tiếp khi trang tải lại
    }
});