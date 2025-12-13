// File: wwwroot/js/user-statistics.js
// Thống kê Thẻ Bạn đọc mới & Tài liệu mới

const UserStatistics = (function () {
    const chartColors = [
        '#696cff', '#71dd37', '#ff3e1d', '#03c3ec',
        '#ffab00', '#8592a3', '#ff6384', '#36a2eb',
        '#cc65fe', '#ffce56', '#4bc0c0', '#9966ff'
    ];
    const labelColor = '#6c757d';
    const borderColor = '#f1f1f1';

    let banDocChart = null;
    let taiLieuMoiChart = null;
    let vaiTroChart = null;

    let banDocChartElement = null;
    let taiLieuMoiChartElement = null;
    let vaiTroChartElement = null;

    let yearSelectorBanDoc = null;
    let yearSelectorTaiLieu = null;

    let banDocViewMode = 'year';
    let taiLieuViewMode = 'year';

    // ==================== HELPER FUNCTIONS ====================

    function showLoading(element) {
        element.innerHTML = `
            <div class="d-flex justify-content-center mt-3">
                <div class="spinner-border" role="status">
                    <span class="visually-hidden">Loading...</span>
                </div>
            </div>`;
    }

    function showError(element, message) {
        element.innerHTML = `
            <div class='alert alert-danger text-center mt-3'>
                ❌ ${message}
            </div>`;
    }

    function showNoData(element, year, title) {
        element.innerHTML = `
            <div class='alert alert-warning text-center mt-3'>
                ⚠️ Không có dữ liệu ${title} cho năm ${year}.
            </div>`;
    }

    function destroyChart(chart) {
        if (chart) {
            chart.destroy();
            return null;
        }
        return null;
    }

    // ==================== BIỂU ĐỒ THẺ BẠN ĐỌC ====================

    function createBanDocChartConfig(data, year, isCompare = false) {
        if (isCompare) {
            return createMultiYearCompareConfig(data, 'Thẻ Bạn đọc', 'thẻ', '#03c3ec');
        }

        const categories = data.map(x => 'Tháng ' + x.thang);
        const counts = data.map(x => x.soBanDocMoi);

        return {
            series: [{
                name: 'Số thẻ cấp mới',
                data: counts
            }],
            chart: {
                height: 450,
                type: 'line',
                toolbar: { show: true },
                zoom: { enabled: true }
            },
            title: {
                text: `Thống kê Thẻ Bạn đọc cấp mới năm ${year}`,
                align: 'center',
                style: {
                    fontSize: '18px',
                    color: '#566a7f',
                    fontWeight: '600'
                }
            },
            stroke: {
                curve: 'smooth',
                width: 3
            },
            markers: {
                size: 6,
                hover: {
                    size: 8
                }
            },
            xaxis: {
                categories: categories,
                axisBorder: { show: false },
                labels: {
                    style: {
                        colors: labelColor,
                        fontSize: '13px'
                    }
                }
            },
            yaxis: {
                title: {
                    text: 'Số Thẻ Cấp Mới',
                    style: {
                        color: labelColor,
                        fontSize: '14px',
                        fontWeight: 600
                    }
                },
                labels: {
                    style: {
                        colors: labelColor,
                        fontSize: '14px'
                    }
                }
            },
            grid: {
                borderColor: borderColor,
                padding: { top: -20, bottom: -12 }
            },
            colors: ['#03c3ec'],
            dataLabels: {
                enabled: true,
                style: {
                    fontSize: '12px',
                    colors: ['#03c3ec']
                },
                background: {
                    enabled: true,
                    foreColor: '#fff',
                    borderRadius: 2,
                    padding: 4,
                    opacity: 0.9,
                    borderWidth: 1,
                    borderColor: '#03c3ec'
                }
            },
            tooltip: {
                y: {
                    formatter: val => `${val} thẻ`
                }
            }
        };
    }

    // ==================== BIỂU ĐỒ TÀI LIỆU MỚI ====================

    function createTaiLieuMoiChartConfig(data, year, isCompare = false) {
        if (isCompare) {
            return createMultiYearCompareConfig(data, 'Tài liệu mới', 'tài liệu', '#ffab00');
        }

        const categories = data.map(x => 'Tháng ' + x.thang);
        const counts = data.map(x => x.soTaiLieuMoi);

        return {
            series: [{
                name: 'Số tài liệu mới',
                data: counts
            }],
            chart: {
                height: 450,
                type: 'line',
                toolbar: { show: true },
                zoom: { enabled: true }
            },
            title: {
                text: `Thống kê Tài liệu mới năm ${year}`,
                align: 'center',
                style: {
                    fontSize: '18px',
                    color: '#566a7f',
                    fontWeight: '600'
                }
            },
            stroke: {
                curve: 'smooth',
                width: 3
            },
            markers: {
                size: 6,
                hover: {
                    size: 8
                }
            },
            xaxis: {
                categories: categories,
                axisBorder: { show: false },
                labels: {
                    style: {
                        colors: labelColor,
                        fontSize: '13px'
                    }
                }
            },
            yaxis: {
                title: {
                    text: 'Số Tài Liệu Mới',
                    style: {
                        color: labelColor,
                        fontSize: '14px',
                        fontWeight: 600
                    }
                },
                labels: {
                    style: {
                        colors: labelColor,
                        fontSize: '14px'
                    }
                }
            },
            grid: {
                borderColor: borderColor,
                padding: { top: -20, bottom: -12 }
            },
            colors: ['#ffab00'],
            dataLabels: {
                enabled: true,
                style: {
                    fontSize: '12px',
                    colors: ['#ffab00']
                },
                background: {
                    enabled: true,
                    foreColor: '#fff',
                    borderRadius: 2,
                    padding: 4,
                    opacity: 0.9,
                    borderWidth: 1,
                    borderColor: '#ffab00'
                }
            },
            tooltip: {
                y: {
                    formatter: val => `${val} tài liệu`
                }
            }
        };
    }

    // ==================== SO SÁNH NHIỀU NĂM ====================

    function createMultiYearCompareConfig(data, title, unit, color) {
        return {
            series: data.series,
            chart: {
                height: 450,
                type: 'line',
                toolbar: { show: true },
                zoom: { enabled: true }
            },
            title: {
                text: `So sánh ${title} (${data.years.join(', ')})`,
                align: 'center',
                style: {
                    fontSize: '18px',
                    color: '#566a7f',
                    fontWeight: '600'
                }
            },
            stroke: {
                curve: 'smooth',
                width: 3
            },
            markers: {
                size: 5,
                hover: {
                    size: 7
                }
            },
            xaxis: {
                categories: data.categories,
                axisBorder: { show: false },
                labels: {
                    style: {
                        colors: labelColor,
                        fontSize: '13px'
                    }
                }
            },
            yaxis: {
                labels: {
                    style: {
                        colors: labelColor,
                        fontSize: '14px'
                    }
                }
            },
            grid: {
                borderColor: borderColor,
                padding: { top: -20, bottom: -12 }
            },
            colors: chartColors,
            legend: {
                show: true,
                position: 'top',
                horizontalAlign: 'right',
                fontSize: '14px',
                labels: {
                    colors: labelColor
                }
            },
            tooltip: {
                y: {
                    formatter: val => `${val} ${unit}`
                }
            }
        };
    }

    // ==================== DATA FETCHING ====================

    async function fetchBanDocData(year) {
        const response = await fetch(`/ThongKe/GetBanDocMoiTheoThang?year=${year}`);
        if (!response.ok) {
            throw new Error(`Lỗi HTTP ${response.status}`);
        }
        return response.json();
    }

    async function fetchTaiLieuMoiData(year) {
        const response = await fetch(`/ThongKe/GetTaiLieuMoiTheoThang?year=${year}`);
        if (!response.ok) {
            throw new Error(`Lỗi HTTP ${response.status}`);
        }
        return response.json();
    }

    async function fetchMultiYearData(years, apiEndpoint, dataKey) {
        const promises = years.map(year =>
            fetch(`${apiEndpoint}?year=${year}`).then(r => r.json())
        );
        const results = await Promise.all(promises);

        const categories = results[0].map(x => 'Tháng ' + x.thang);
        const series = results.map((data, index) => ({
            name: `Năm ${years[index]}`,
            data: data.map(x => x[dataKey] || 0)
        }));

        return {
            categories: categories,
            series: series,
            years: years
        };
    }

    // ==================== RENDER CHARTS ====================

    function renderBanDocChart(year, mode = 'year', compareYears = null) {
        banDocChart = destroyChart(banDocChart);
        showLoading(banDocChartElement);

        if (mode === 'compare' && compareYears && compareYears.length > 0) {
            fetchMultiYearData(compareYears, '/ThongKe/GetBanDocMoiTheoThang', 'soBanDocMoi')
                .then(data => {
                    banDocChartElement.innerHTML = '';
                    const chartConfig = createBanDocChartConfig(data, null, true);
                    banDocChart = new ApexCharts(banDocChartElement, chartConfig);
                    banDocChart.render();
                })
                .catch(err => showError(banDocChartElement, err.message));
        } else {
            fetchBanDocData(year)
                .then(data => {
                    if (!data || data.length === 0) {
                        showNoData(banDocChartElement, year, 'thẻ bạn đọc');
                        return;
                    }
                    banDocChartElement.innerHTML = '';
                    const chartConfig = createBanDocChartConfig(data, year);
                    banDocChart = new ApexCharts(banDocChartElement, chartConfig);
                    banDocChart.render();
                })
                .catch(err => showError(banDocChartElement, err.message));
        }
    }

    function renderTaiLieuMoiChart(year, mode = 'year', compareYears = null) {
        taiLieuMoiChart = destroyChart(taiLieuMoiChart);
        showLoading(taiLieuMoiChartElement);

        if (mode === 'compare' && compareYears && compareYears.length > 0) {
            fetchMultiYearData(compareYears, '/ThongKe/GetTaiLieuMoiTheoThang', 'soTaiLieuMoi')
                .then(data => {
                    taiLieuMoiChartElement.innerHTML = '';
                    const chartConfig = createTaiLieuMoiChartConfig(data, null, true);
                    taiLieuMoiChart = new ApexCharts(taiLieuMoiChartElement, chartConfig);
                    taiLieuMoiChart.render();
                })
                .catch(err => showError(taiLieuMoiChartElement, err.message));
        } else {
            fetchTaiLieuMoiData(year)
                .then(data => {
                    if (!data || data.length === 0) {
                        showNoData(taiLieuMoiChartElement, year, 'tài liệu mới');
                        return;
                    }
                    taiLieuMoiChartElement.innerHTML = '';
                    const chartConfig = createTaiLieuMoiChartConfig(data, year);
                    taiLieuMoiChart = new ApexCharts(taiLieuMoiChartElement, chartConfig);
                    taiLieuMoiChart.render();
                })
                .catch(err => showError(taiLieuMoiChartElement, err.message));
        }
    }

    // ==================== BIỂU ĐỒ VAI TRÒ (DONUT) ====================

    function createVaiTroChartConfig(data) {
        const labels = data.map(x => x.vaiTro);
        const counts = data.map(x => x.soLuong);

        return {
            series: counts,
            chart: {
                height: 350,
                type: 'donut'
            },
            labels: labels,
            colors: chartColors,
            dataLabels: {
                enabled: true,
                formatter: function (val, opts) {
                    return opts.w.config.series[opts.seriesIndex];
                }
            },
            legend: {
                show: true,
                position: 'bottom',
                horizontalAlign: 'center',
                fontSize: '14px',
                labels: {
                    colors: labelColor
                }
            },
            plotOptions: {
                pie: {
                    donut: {
                        size: '65%',
                        labels: {
                            show: true,
                            name: {
                                show: true,
                                fontSize: '16px',
                                fontWeight: 600,
                                color: labelColor
                            },
                            value: {
                                show: true,
                                fontSize: '24px',
                                fontWeight: 700,
                                color: '#566a7f',
                                formatter: function (val) {
                                    return val;
                                }
                            },
                            total: {
                                show: true,
                                label: 'Tổng',
                                fontSize: '16px',
                                color: labelColor,
                                formatter: function (w) {
                                    return w.globals.seriesTotals.reduce((a, b) => a + b, 0);
                                }
                            }
                        }
                    }
                }
            },
            tooltip: {
                y: {
                    formatter: val => `${val} tài khoản`
                }
            }
        };
    }

    async function fetchVaiTroData() {
        const response = await fetch('/ThongKe/GetTaiKhoanTheoVaiTro');
        if (!response.ok) {
            throw new Error(`Lỗi HTTP ${response.status}`);
        }
        return response.json();
    }

    function renderVaiTroChart() {
        vaiTroChart = destroyChart(vaiTroChart);
        showLoading(vaiTroChartElement);

        fetchVaiTroData()
            .then(data => {
                if (!data || data.length === 0) {
                    vaiTroChartElement.innerHTML = `
                        <div class='alert alert-warning text-center mt-3'>
                            ⚠️ Không có dữ liệu tài khoản.
                        </div>`;
                    return;
                }

                vaiTroChartElement.innerHTML = '';
                const chartConfig = createVaiTroChartConfig(data);
                vaiTroChart = new ApexCharts(vaiTroChartElement, chartConfig);
                vaiTroChart.render();
            })
            .catch(err => showError(vaiTroChartElement, err.message));
    }

    // ==================== YEAR MANAGEMENT ====================

    function getYearsFromInputs(containerId) {
        const container = document.getElementById(containerId);
        if (!container) return [];

        const wrappers = container.querySelectorAll('.year-input-wrapper');
        const years = [];
        wrappers.forEach(wrapper => {
            const input = wrapper.querySelector('input[type="number"]');
            const year = parseInt(input.value);
            if (year && !years.includes(year)) {
                years.push(year);
            }
        });
        return years.sort();
    }

    function addYearInput(containerId) {
        const container = document.getElementById(containerId);
        const currentWrappers = container.querySelectorAll('.year-input-wrapper');
        if (currentWrappers.length >= 5) {
            alert('Tối đa 5 năm để so sánh');
            return;
        }

        const wrapper = document.createElement('div');
        wrapper.className = 'year-input-wrapper';
        wrapper.style.display = 'inline-block';
        wrapper.style.marginRight = '8px';

        const newInput = document.createElement('input');
        newInput.type = 'number';
        newInput.className = 'form-control form-control-sm year-compare-input';
        newInput.style.width = '80px';
        newInput.style.display = 'inline-block';
        newInput.min = '2000';
        newInput.max = '2100';
        newInput.value = new Date().getFullYear();

        const removeBtn = document.createElement('button');
        removeBtn.type = 'button';
        removeBtn.className = 'btn btn-sm btn-outline-danger remove-year-btn ms-1';
        removeBtn.innerHTML = '<i class="bx bx-x"></i>';
        removeBtn.addEventListener('click', function () {
            removeYearInput(wrapper, containerId);
        });

        wrapper.appendChild(newInput);
        wrapper.appendChild(removeBtn);
        container.appendChild(wrapper);

        updateRemoveButtons(containerId);
    }

    function removeYearInput(wrapper, containerId) {
        const container = document.getElementById(containerId);
        const wrappers = container.querySelectorAll('.year-input-wrapper');

        if (wrappers.length <= 2) {
            alert('Cần ít nhất 2 năm để so sánh');
            return;
        }

        wrapper.remove();
        updateRemoveButtons(containerId);
    }

    function updateRemoveButtons(containerId) {
        const container = document.getElementById(containerId);
        if (!container) return;

        const wrappers = container.querySelectorAll('.year-input-wrapper');

        wrappers.forEach((wrapper, index) => {
            const removeBtn = wrapper.querySelector('.remove-year-btn');
            if (removeBtn) {
                // Luôn hiển thị nút xóa nếu có nhiều hơn 2 năm
                if (wrappers.length > 2) {
                    removeBtn.style.display = 'inline-block';
                } else {
                    removeBtn.style.display = 'none';
                }
            }
        });
    }

    function initRemoveButtonListeners(containerId) {
        const container = document.getElementById(containerId);
        if (!container) return;

        const removeBtns = container.querySelectorAll('.remove-year-btn');
        removeBtns.forEach(btn => {
            btn.addEventListener('click', function () {
                const wrapper = this.closest('.year-input-wrapper');
                removeYearInput(wrapper, containerId);
            });
        });
    }

    // ==================== EVENT LISTENERS ====================

    function initEventListeners() {
        // ========== BẠN ĐỌC ==========
        const banDocYearMode = document.getElementById('banDocYearMode');
        const banDocCompareMode = document.getElementById('banDocCompareMode');
        const banDocYearSelector = document.getElementById('banDocYearSelector');
        const banDocYearCompare = document.getElementById('banDocYearCompare');
        const addYearBanDoc = document.getElementById('addYearBanDoc');
        const updateChartBanDoc = document.getElementById('updateChartBanDoc');

        if (banDocYearMode) {
            banDocYearMode.addEventListener('change', function () {
                if (this.checked) {
                    banDocViewMode = 'year';
                    banDocYearSelector.style.display = 'block';
                    banDocYearCompare.style.display = 'none';
                    renderBanDocChart(yearSelectorBanDoc.value);
                }
            });
        }

        if (banDocCompareMode) {
            banDocCompareMode.addEventListener('change', function () {
                if (this.checked) {
                    banDocViewMode = 'compare';
                    banDocYearSelector.style.display = 'none';
                    banDocYearCompare.style.display = 'flex';
                    const years = getYearsFromInputs('banDocYearInputs');
                    renderBanDocChart(null, 'compare', years);
                }
            });
        }

        if (yearSelectorBanDoc) {
            yearSelectorBanDoc.addEventListener('change', function () {
                if (banDocViewMode === 'year') {
                    renderBanDocChart(this.value);
                    if (window.updateExportBanDocLink) {
                        window.updateExportBanDocLink(this.value);
                    }
                }
            });
        }

        if (addYearBanDoc) {
            addYearBanDoc.addEventListener('click', function () {
                addYearInput('banDocYearInputs');
            });
        }

        if (updateChartBanDoc) {
            updateChartBanDoc.addEventListener('click', function () {
                const years = getYearsFromInputs('banDocYearInputs');
                if (years.length < 2) {
                    alert('Vui lòng chọn ít nhất 2 năm để so sánh');
                    return;
                }
                renderBanDocChart(null, 'compare', years);
            });
        }

        // ========== TÀI LIỆU MỚI ==========
        const taiLieuYearMode = document.getElementById('taiLieuYearMode');
        const taiLieuCompareMode = document.getElementById('taiLieuCompareMode');
        const taiLieuYearSelector = document.getElementById('taiLieuYearSelector');
        const taiLieuYearCompare = document.getElementById('taiLieuYearCompare');
        const addYearTaiLieu = document.getElementById('addYearTaiLieu');
        const updateChartTaiLieu = document.getElementById('updateChartTaiLieu');

        if (taiLieuYearMode) {
            taiLieuYearMode.addEventListener('change', function () {
                if (this.checked) {
                    taiLieuViewMode = 'year';
                    taiLieuYearSelector.style.display = 'block';
                    taiLieuYearCompare.style.display = 'none';
                    renderTaiLieuMoiChart(yearSelectorTaiLieu.value);
                }
            });
        }

        if (taiLieuCompareMode) {
            taiLieuCompareMode.addEventListener('change', function () {
                if (this.checked) {
                    taiLieuViewMode = 'compare';
                    taiLieuYearSelector.style.display = 'none';
                    taiLieuYearCompare.style.display = 'flex';
                    const years = getYearsFromInputs('taiLieuYearInputs');
                    renderTaiLieuMoiChart(null, 'compare', years);
                }
            });
        }

        if (yearSelectorTaiLieu) {
            yearSelectorTaiLieu.addEventListener('change', function () {
                if (taiLieuViewMode === 'year') {
                    renderTaiLieuMoiChart(this.value);
                    if (window.updateExportTaiLieuLink) {
                        window.updateExportTaiLieuLink(this.value);
                    }
                }
            });
        }

        if (addYearTaiLieu) {
            addYearTaiLieu.addEventListener('click', function () {
                addYearInput('taiLieuYearInputs');
            });
        }

        if (updateChartTaiLieu) {
            updateChartTaiLieu.addEventListener('click', function () {
                const years = getYearsFromInputs('taiLieuYearInputs');
                if (years.length < 2) {
                    alert('Vui lòng chọn ít nhất 2 năm để so sánh');
                    return;
                }
                renderTaiLieuMoiChart(null, 'compare', years);
            });
        }
    }

    // ==================== PUBLIC API ====================

    return {
        init: function () {
            banDocChartElement = document.querySelector('#banDocBarChart');
            taiLieuMoiChartElement = document.querySelector('#taiLieuMoiBarChart');
            vaiTroChartElement = document.querySelector('#vaiTroDonutChart');
            yearSelectorBanDoc = document.querySelector('#yearSelectorBanDoc');
            yearSelectorTaiLieu = document.querySelector('#yearSelectorTaiLieu');

            if (!banDocChartElement || !taiLieuMoiChartElement || !vaiTroChartElement) {
                console.error('Chart elements not found!');
                return;
            }

            if (yearSelectorBanDoc) {
                renderBanDocChart(yearSelectorBanDoc.value);
            }

            if (yearSelectorTaiLieu) {
                renderTaiLieuMoiChart(yearSelectorTaiLieu.value);
            }

            renderVaiTroChart();
            initEventListeners();

            // Khởi tạo remove button listeners
            initRemoveButtonListeners('banDocYearInputs');
            initRemoveButtonListeners('taiLieuYearInputs');

            // Cập nhật trạng thái ban đầu
            updateRemoveButtons('banDocYearInputs');
            updateRemoveButtons('taiLieuYearInputs');
        },

        destroy: function () {
            banDocChart = destroyChart(banDocChart);
            taiLieuMoiChart = destroyChart(taiLieuMoiChart);
            vaiTroChart = destroyChart(vaiTroChart);
        },

        refreshBanDocChart: function (year) {
            renderBanDocChart(year || yearSelectorBanDoc.value);
        },

        refreshTaiLieuMoiChart: function (year) {
            renderTaiLieuMoiChart(year || yearSelectorTaiLieu.value);
        },

        refreshVaiTroChart: function () {
            renderVaiTroChart();
        }
    };
})();

// Auto-init khi DOM ready
document.addEventListener('DOMContentLoaded', function () {
    UserStatistics.init();
});