// File: wwwroot/js/chart-statistics.js
// Thống kê lượt mượn sách

const ChartStatistics = (function () {
    const chartColors = [
        '#696cff', '#71dd37', '#ff3e1d', '#03c3ec',
        '#ffab00', '#8592a3', '#ff6384', '#36a2eb',
        '#cc65fe', '#ffce56', '#4bc0c0', '#9966ff'
    ];
    const labelColor = '#6c757d';
    const borderColor = '#f1f1f1';

    let chart = null;
    let chartElement = null;
    let yearSelector = null;
    let viewMode = 'year';

    // ==================== HELPER FUNCTIONS ====================

    function showLoading() {
        chartElement.innerHTML = `
            <div class="d-flex justify-content-center mt-3">
                <div class="spinner-border" role="status">
                    <span class="visually-hidden">Loading...</span>
                </div>
            </div>`;
    }

    function showError(message) {
        chartElement.innerHTML = `
            <div class='alert alert-danger text-center mt-3'>
                ❌ ${message}
            </div>`;
    }

    function showNoData(year) {
        chartElement.innerHTML = `
            <div class='alert alert-warning text-center mt-3'>
                ⚠️ Không có dữ liệu thống kê cho năm ${year}.
            </div>`;
    }

    function destroyChart() {
        if (chart) {
            chart.destroy();
            chart = null;
        }
    }

    // ==================== CHART CONFIG ====================

    function createChartConfig(data, year, isCompare = false) {
        if (isCompare) {
            return createMultiYearCompareConfig(data);
        }

        const categories = data.map(x => 'Tháng ' + x.thang);
        const counts = data.map(x => x.soLuotMuon);

        return {
            series: [{
                name: 'Số lượt mượn',
                data: counts
            }],
            chart: {
                height: 450,
                type: 'line',
                toolbar: { show: true },
                zoom: { enabled: true }
            },
            title: {
                text: `Thống kê lượt mượn sách năm ${year}`,
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
                    text: 'Số Lượt Mượn',
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
            colors: ['#696cff'],
            dataLabels: {
                enabled: true,
                style: {
                    fontSize: '12px',
                    colors: ['#696cff']
                },
                background: {
                    enabled: true,
                    foreColor: '#fff',
                    borderRadius: 2,
                    padding: 4,
                    opacity: 0.9,
                    borderWidth: 1,
                    borderColor: '#696cff'
                }
            },
            tooltip: {
                y: {
                    formatter: val => `${val} lượt`
                }
            }
        };
    }

    function createMultiYearCompareConfig(data) {
        return {
            series: data.series,
            chart: {
                height: 450,
                type: 'line',
                toolbar: { show: true },
                zoom: { enabled: true }
            },
            title: {
                text: `So sánh lượt mượn sách (${data.years.join(', ')})`,
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
                title: {
                    text: 'Số Lượt Mượn',
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
                    formatter: val => `${val} lượt`
                }
            }
        };
    }

    // ==================== DATA FETCHING ====================

    async function fetchData(year) {
        const response = await fetch(`/ThongKe/GetLuotMuonTheoThang?year=${year}`);
        if (!response.ok) {
            throw new Error(`Lỗi HTTP ${response.status}`);
        }
        return response.json();
    }

    async function fetchMultiYearData(years) {
        const promises = years.map(year => fetchData(year));
        const results = await Promise.all(promises);

        const categories = results[0].map(x => 'Tháng ' + x.thang);
        const series = results.map((data, index) => ({
            name: `Năm ${years[index]}`,
            data: data.map(x => x.soLuotMuon)
        }));

        return {
            categories: categories,
            series: series,
            years: years
        };
    }

    // ==================== RENDER CHART ====================

    function renderChart(year, mode = 'year', compareYears = null) {
        destroyChart();
        showLoading();

        if (mode === 'compare' && compareYears && compareYears.length > 0) {
            fetchMultiYearData(compareYears)
                .then(data => {
                    chartElement.innerHTML = '';
                    const chartConfig = createChartConfig(data, null, true);
                    chart = new ApexCharts(chartElement, chartConfig);
                    chart.render();
                })
                .catch(err => showError(err.message));
        } else {
            fetchData(year)
                .then(data => {
                    if (!data || data.length === 0) {
                        showNoData(year);
                        return;
                    }
                    chartElement.innerHTML = '';
                    const chartConfig = createChartConfig(data, year);
                    chart = new ApexCharts(chartElement, chartConfig);
                    chart.render();
                })
                .catch(err => showError(err.message));
        }
    }

    // ==================== YEAR MANAGEMENT ====================

    function getYearsFromInputs(containerId) {
        const container = document.getElementById(containerId);
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
        const muonYearMode = document.getElementById('muonYearMode');
        const muonCompareMode = document.getElementById('muonCompareMode');
        const muonYearSelector = document.getElementById('muonYearSelector');
        const muonYearCompare = document.getElementById('muonYearCompare');
        const addYearBtn = document.getElementById('addYearMuon');
        const updateChartBtn = document.getElementById('updateChartMuon');

        if (muonYearMode) {
            muonYearMode.addEventListener('change', function () {
                if (this.checked) {
                    viewMode = 'year';
                    muonYearSelector.style.display = 'block';
                    muonYearCompare.style.display = 'none';
                    renderChart(yearSelector.value);
                }
            });
        }

        if (muonCompareMode) {
            muonCompareMode.addEventListener('change', function () {
                if (this.checked) {
                    viewMode = 'compare';
                    muonYearSelector.style.display = 'none';
                    muonYearCompare.style.display = 'flex';
                    const years = getYearsFromInputs('muonYearInputs');
                    renderChart(null, 'compare', years);
                }
            });
        }

        if (yearSelector) {
            yearSelector.addEventListener('change', function () {
                if (viewMode === 'year') {
                    renderChart(this.value);
                    if (window.updateExportMuonLink) {
                        window.updateExportMuonLink(this.value);
                    }
                }
            });
        }

        if (addYearBtn) {
            addYearBtn.addEventListener('click', function () {
                addYearInput('muonYearInputs');
            });
        }

        if (updateChartBtn) {
            updateChartBtn.addEventListener('click', function () {
                const years = getYearsFromInputs('muonYearInputs');
                if (years.length < 2) {
                    alert('Vui lòng chọn ít nhất 2 năm để so sánh');
                    return;
                }
                renderChart(null, 'compare', years);
            });
        }
    }

    // ==================== PUBLIC API ====================

    return {
        init: function (chartElementId, yearSelectorId) {
            chartElement = document.querySelector(chartElementId);
            yearSelector = document.querySelector(yearSelectorId);

            if (!chartElement || !yearSelector) {
                console.error('Chart element or year selector not found!');
                return;
            }

            renderChart(yearSelector.value);
            initEventListeners();
            initRemoveButtonListeners('muonYearInputs');
            updateRemoveButtons('muonYearInputs');
        },

        destroy: function () {
            destroyChart();
        },

        refresh: function (year) {
            renderChart(year || yearSelector.value);
        }
    };
})();

// Auto-init khi DOM ready
document.addEventListener('DOMContentLoaded', function () {
    ChartStatistics.init('#horizontalBarChart', '#yearSelector');
});