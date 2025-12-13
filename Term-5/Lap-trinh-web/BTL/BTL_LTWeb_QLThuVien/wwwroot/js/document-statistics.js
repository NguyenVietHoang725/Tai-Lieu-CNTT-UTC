// File: wwwroot/js/document-statistics.js

const DocumentStatistics = (function () {
    let totalBanSaoEl = null;
    let totalCoSanEl = null;
    let totalDangMuonEl = null;
    let tyLeCoSanEl = null;
    let categoryTableBody = null;
    let toggleDetailBtn = null;
    let categoryDetail = null;
    let summaryCards = null;
    let toggleIcon = null;
    let toggleText = null;
    let isDetailVisible = false; // Mặc định ẨN chi tiết

    let categoryDetailModalEl = null;
    let categoryDetailModal = null;
    let modalCategoryTitleEl = null;
    let modalCategoryBodyEl = null;

    function showLoadingCard(element) {
        element.innerHTML = `
            <div class="spinner-border spinner-border-sm" role="status">
                <span class="visually-hidden">Loading...</span>
            </div>`;
    }

    function showError(element, message) {
        element.innerHTML = `<small class="text-danger">${message}</small>`;
    }

    function showLoadingTable() {
        if (!categoryTableBody) return;
        categoryTableBody.innerHTML = `
            <tr>
                <td colspan="5" class="text-center">
                    <div class="spinner-border" role="status">
                        <span class="visually-hidden">Loading...</span>
                    </div>
                </td>
            </tr>`;
    }

    function showTableError(message) {
        if (!categoryTableBody) return;
        categoryTableBody.innerHTML = `
            <tr>
                <td colspan="5" class="text-center text-danger">
                    <i class="bx bx-error-circle me-1"></i>
                    ${message}
                </td>
            </tr>`;
    }

    function showNoDataTable() {
        if (!categoryTableBody) return;
        categoryTableBody.innerHTML = `
            <tr>
                <td colspan="5" class="text-center text-muted">
                    <i class="bx bx-info-circle me-1"></i>
                    Không có dữ liệu
                </td>
            </tr>`;
    }

    function animateNumber(element, targetNumber, duration = 1000, suffix = '') {
        if (!element) return;
        const start = 0;
        const increment = targetNumber / (duration / 16);
        let current = start;

        const timer = setInterval(() => {
            current += increment;
            if (current >= targetNumber) {
                element.textContent = targetNumber.toLocaleString('vi-VN') + suffix;
                clearInterval(timer);
            } else {
                element.textContent = Math.floor(current).toLocaleString('vi-VN') + suffix;
            }
        }, 16);
    }

    function calculatePercentage(available, total) {
        if (total === 0) return 0;
        return ((available / total) * 100).toFixed(1);
    }

    function createProgressBar(percentage) {
        let colorClass = 'bg-success';
        if (percentage < 30) colorClass = 'bg-danger';
        else if (percentage < 60) colorClass = 'bg-warning';

        return `
            <div class="d-flex align-items-center gap-2">
                <div class="progress flex-grow-1" style="height: 8px;">
                    <div class="progress-bar ${colorClass}" 
                         role="progressbar" 
                         style="width: ${percentage}%"
                         aria-valuenow="${percentage}" 
                         aria-valuemin="0" 
                         aria-valuemax="100">
                    </div>
                </div>
                <small class="text-muted">${percentage}%</small>
            </div>`;
    }

    function renderSummaryCards(data) {
        const tongGiaoTrinh = data.tongGiaoTrinh || 0;
        const tongTaiLieu = data.tongTaiLieu || 0;
        const tongBanSao = tongGiaoTrinh + tongTaiLieu;

        const giaoTrinhCoSan = data.giaoTrinhCoSan || 0;
        const taiLieuCoSan = data.taiLieuCoSan || 0;
        const tongCoSan = giaoTrinhCoSan + taiLieuCoSan;

        const tongDangMuon = tongBanSao - tongCoSan;
        const tyLe = calculatePercentage(tongCoSan, tongBanSao);

        if (totalBanSaoEl) {
            animateNumber(totalBanSaoEl, tongBanSao);
        }
        if (totalCoSanEl) {
            animateNumber(totalCoSanEl, tongCoSan);
        }
        if (totalDangMuonEl) {
            animateNumber(totalDangMuonEl, tongDangMuon);
        }
        if (tyLeCoSanEl) {
            animateNumber(tyLeCoSanEl, parseFloat(tyLe), 1000, '%');
        }
    }

    function renderCategoryTable(categories) {
        if (!categoryTableBody) return;

        if (!categories || categories.length === 0) {
            showNoDataTable();
            return;
        }

        const allCategoriesDefinitions = [
            { code: 'GT', name: 'Sách giáo trình' },
            { code: 'TK', name: 'Sách tham khảo' },
            { code: 'LV', name: 'Luận văn' },
            { code: 'LA', name: 'Luận án' },
            { code: 'BA', name: 'Báo' },
            { code: 'TC', name: 'Tạp chí' },
            { code: 'DT', name: 'Đề tài nghiên cứu khoa học' },
            { code: 'KY', name: 'Kỷ yếu hội thảo' },
            { code: 'DTU', name: 'Tài liệu điện tử' },
            { code: 'NB', name: 'Tài liệu nội bộ' }
        ];

        const combinedCategories = allCategoriesDefinitions.map(catDef => {
            const cat = categories.find(c => c.tenDanhMuc === catDef.name);
            return {
                ...catDef,
                tongSoLuong: cat ? cat.tongSoLuong : 0,
                soLuongCoSan: cat ? cat.soLuongCoSan : 0,
                apiData: cat
            };
        });

        combinedCategories.sort((a, b) => b.tongSoLuong - a.tongSoLuong);

        const rows = combinedCategories.map(item => {
            const cat = item.apiData;
            const categoryCode = item.code;
            const categoryName = item.name;

            const rowClass = "class='category-row-clickable' style='cursor: pointer;'";
            const rowData = `data-category-code="${categoryCode}" data-category-name="${categoryName}"`;

            if (cat) {
                const percentage = calculatePercentage(cat.soLuongCoSan, cat.tongSoLuong);
                const dangMuon = cat.tongSoLuong - cat.soLuongCoSan;

                return `
                    <tr ${rowClass} ${rowData}>
                        <td>
                            <div class="d-flex align-items-center">
                                <i class="bx bx-folder me-2 text-primary"></i>
                                <strong>${cat.tenDanhMuc}</strong>
                            </div>
                        </td>
                        <td class="text-center">
                            <span class="badge bg-label-primary">${cat.tongSoLuong}</span>
                        </td>
                        <td class="text-center">
                            <span class="badge bg-label-success">${cat.soLuongCoSan}</span>
                        </td>
                        <td class="text-center">
                            <span class="badge bg-label-warning">${dangMuon}</span>
                        </td>
                        <td>${createProgressBar(percentage)}</td>
                    </tr>`;
            } else {
                return `
                    <tr ${rowClass} ${rowData}>
                        <td>
                            <div class="d-flex align-items-center">
                                <i class="bx bx-folder me-2 text-muted"></i>
                                <span class="text-muted">${categoryName}</span>
                            </div>
                        </td>
                        <td class="text-center">
                            <span class="badge bg-label-secondary">0</span>
                        </td>
                        <td class="text-center">
                            <span class="badge bg-label-secondary">0</span>
                        </td>
                        <td class="text-center">
                            <span class="badge bg-label-secondary">0</span>
                        </td>
                        <td>${createProgressBar(0)}</td>
                    </tr>`;
            }
        }).join('');

        categoryTableBody.innerHTML = rows;
        addTableClickListeners();
    }

    function addTableClickListeners() {
        if (!categoryTableBody) return;
        const clickableRows = categoryTableBody.querySelectorAll('.category-row-clickable');
        clickableRows.forEach(row => {
            row.addEventListener('click', handleCategoryRowClick);
        });
    }

    function handleCategoryRowClick(event) {
        const row = event.currentTarget;
        const code = row.dataset.categoryCode;
        const name = row.dataset.categoryName;

        if (!code || !categoryDetailModal) return;

        modalCategoryTitleEl.textContent = `Chi tiết: ${name}`;
        modalCategoryBodyEl.innerHTML = `
            <div class="d-flex justify-content-center p-3">
                <div class="spinner-border" role="status">
                    <span class="visually-hidden">Loading...</span>
                </div>
            </div>`;
        categoryDetailModal.show();

        fetchCategoryDetails(code);
    }

    async function fetchCategoryDetails(code) {
        try {
            const response = await fetch(`/ThongKe/GetChiTietTheLoai?maTheLoai=${code}`);
            if (!response.ok) {
                throw new Error(`Lỗi HTTP ${response.status}`);
            }
            const data = await response.json();
            renderCategoryDetailsTable(data);
        } catch (error) {
            console.error('Error fetching category details:', error);
            modalCategoryBodyEl.innerHTML = `<div class="alert alert-danger">Không thể tải chi tiết: ${error.message}</div>`;
        }
    }

    function renderCategoryDetailsTable(details) {
        if (!details || details.length === 0) {
            modalCategoryBodyEl.innerHTML = `<div class="alert alert-info">Không có dữ liệu chi tiết cho thể loại này.</div>`;
            return;
        }

        const tableRows = details.map(item => {
            const statusBadge = item.trangThai === "Đang mượn"
                ? `<span class="badge bg-label-warning">Đang mượn</span>`
                : `<span class="badge bg-label-success">Có sẵn</span>`;

            return `
                <tr>
                    <td>${item.maBanSao}</td>
                    <td>${item.tenTaiLieu}</td>
                    <td class="text-center">${statusBadge}</td>
                </tr>`;
        }).join('');

        modalCategoryBodyEl.innerHTML = `
            <div class="table-responsive" style="max-height: 400px; overflow-y: auto;">
                <table class="table table-hover">
                    <thead class="table-light" style="position: sticky; top: 0;">
                        <tr>
                            <th>Mã Bản Sao</th>
                            <th>Tên Tài Liệu</th>
                            <th class="text-center">Trạng Thái</th>
                        </tr>
                    </thead>
                    <tbody>
                        ${tableRows}
                    </tbody>
                </table>
            </div>
        `;
    }

    function toggleCategoryDetail() {
        if (!categoryDetail || !toggleIcon || !toggleText) return;

        isDetailVisible = !isDetailVisible;

        if (isDetailVisible) {
            categoryDetail.style.display = 'block';
            toggleIcon.className = 'bx bx-chevron-up';
            toggleText.textContent = 'Ẩn chi tiết';
        } else {
            categoryDetail.style.display = 'none';
            toggleIcon.className = 'bx bx-chevron-down';
            toggleText.textContent = 'Xem chi tiết';
        }
    }

    async function fetchStatistics() {
        try {
            const response = await fetch('/ThongKe/GetThongKeTaiLieu');
            if (!response.ok) {
                throw new Error(`Lỗi HTTP ${response.status}`);
            }
            return await response.json();
        } catch (error) {
            throw error;
        }
    }

    function loadStatistics() {
        showLoadingCard(totalBanSaoEl);
        showLoadingCard(totalCoSanEl);
        showLoadingCard(totalDangMuonEl);
        showLoadingCard(tyLeCoSanEl);
        showLoadingTable();

        fetchStatistics()
            .then(data => {
                console.log('Statistics data:', data);
                renderSummaryCards(data);
                renderCategoryTable(data.chiTietTheoDanhMuc);
            })
            .catch(error => {
                console.error('Error loading statistics:', error);
                showError(totalBanSaoEl, 'Lỗi');
                showError(totalCoSanEl, 'Lỗi');
                showError(totalDangMuonEl, 'Lỗi');
                showError(tyLeCoSanEl, 'Lỗi');
                showTableError(error.message);
            });
    }

    return {
        init: function () {
            totalBanSaoEl = document.getElementById('totalBanSao');
            totalCoSanEl = document.getElementById('totalCoSan');
            totalDangMuonEl = document.getElementById('totalDangMuon');
            tyLeCoSanEl = document.getElementById('tyLeCoSan');
            categoryTableBody = document.querySelector('#categoryTable tbody');
            toggleDetailBtn = document.getElementById('toggleDetailBtn');
            categoryDetail = document.getElementById('categoryDetail');
            summaryCards = document.getElementById('summaryCards');
            toggleIcon = document.getElementById('toggleIcon');
            toggleText = document.getElementById('toggleText');

            categoryDetailModalEl = document.getElementById('categoryDetailModal');
            modalCategoryTitleEl = document.getElementById('modalCategoryTitle');
            modalCategoryBodyEl = document.getElementById('modalCategoryBody');

            if (!totalBanSaoEl || !totalCoSanEl || !categoryTableBody) {
                console.error('Required DOM elements not found!');
                return;
            }

            if (categoryDetailModalEl && typeof bootstrap !== 'undefined') {
                categoryDetailModal = new bootstrap.Modal(categoryDetailModalEl);
            } else {
                console.warn('Bootstrap Modal not available or modal element not found');
            }

            if (toggleDetailBtn) {
                toggleDetailBtn.addEventListener('click', toggleCategoryDetail);
            } else {
                console.warn('Toggle detail button not found');
            }

            // Đảm bảo chi tiết bắt đầu ở trạng thái ẩn
            if (categoryDetail) {
                categoryDetail.style.display = 'none';
            }

            loadStatistics();
        },

        refresh: function () {
            loadStatistics();
        }
    };
})();

document.addEventListener('DOMContentLoaded', function () {
    DocumentStatistics.init();
});