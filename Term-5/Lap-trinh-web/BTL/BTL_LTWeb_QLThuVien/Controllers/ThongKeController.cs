using Microsoft.AspNetCore.Mvc;
using Library_Manager.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System;
using ClosedXML.Excel;
using System.IO;
using System.Collections.Generic;

namespace Library_Manager.Controllers
{
    public class ThongKeController : Controller
    {
        private readonly QlthuVienContext _context;

        public ThongKeController(QlthuVienContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            int nam = DateTime.Now.Year;
            ViewBag.Nam = nam;
            return View();
        }

        // ==========================================================
        // CHỨC NĂNG 1: THỐNG KÊ LƯỢT MƯỢN
        // ==========================================================

        [HttpGet]
        public async Task<IActionResult> GetLuotMuonTheoThang(int year)
        {
            var thongKe = await _context.TGiaoDichMuonTra
                .Where(g => g.NgayMuon.Year == year)
                .GroupBy(g => g.NgayMuon.Month)
                .Select(g => new
                {
                    Thang = g.Key,
                    SoLuotMuon = g.Count()
                })
                .OrderBy(x => x.Thang)
                .ToListAsync();

            var duLieuDayDu = Enumerable.Range(1, 12).Select(thang =>
            {
                var data = thongKe.FirstOrDefault(x => x.Thang == thang);
                return new
                {
                    Thang = thang,
                    SoLuotMuon = data?.SoLuotMuon ?? 0
                };
            }).ToList();

            return Json(duLieuDayDu);
        }

        [HttpGet]
        public async Task<IActionResult> ExportLuotMuonToExcel(int year)
        {
            var thongKe = await _context.TGiaoDichMuonTra
                .Where(g => g.NgayMuon.Year == year)
                .GroupBy(g => g.NgayMuon.Month)
                .Select(g => new
                {
                    Thang = g.Key,
                    SoLuotMuon = g.Count()
                })
                .OrderBy(x => x.Thang)
                .ToListAsync();

            var duLieuDayDu = Enumerable.Range(1, 12).Select(thang =>
            {
                var data = thongKe.FirstOrDefault(x => x.Thang == thang);
                return new
                {
                    Thang = thang,
                    SoLuotMuon = data?.SoLuotMuon ?? 0
                };
            }).ToList();

            var tongLuotMuon = duLieuDayDu.Sum(x => x.SoLuotMuon);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add($"LuotMuon_Nam{year}");

                worksheet.Cell("A1").Value = $"BÁO CÁO THỐNG KÊ LƯỢT MƯỢN THEO THÁNG NĂM {year}";
                worksheet.Range("A1:D1").Merge().Style.Font.Bold = true;
                worksheet.Range("A1:D1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Range("A1:D1").Style.Font.FontSize = 14;

                worksheet.Cell("A3").Value = "Tổng Lượt Mượn Cả Năm:";
                worksheet.Cell("B3").Value = tongLuotMuon;
                worksheet.Range("A3:B3").Style.Font.Bold = true;

                worksheet.Cell("A5").Value = "STT";
                worksheet.Cell("B5").Value = "Tháng";
                worksheet.Cell("C5").Value = "Số Lượt Mượn";
                worksheet.Cell("D5").Value = "Tỷ Lệ (%)";

                var headerRange = worksheet.Range("A5:D5");
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
                headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                var currentRow = 6;
                foreach (var item in duLieuDayDu)
                {
                    worksheet.Cell(currentRow, 1).Value = currentRow - 5;
                    worksheet.Cell(currentRow, 2).Value = item.Thang;
                    worksheet.Cell(currentRow, 3).Value = item.SoLuotMuon;
                    worksheet.Cell(currentRow, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                    double tyLe = tongLuotMuon > 0 ? (double)item.SoLuotMuon / tongLuotMuon : 0;
                    worksheet.Cell(currentRow, 4).Value = tyLe;
                    worksheet.Cell(currentRow, 4).Style.NumberFormat.Format = "0.00%";
                    worksheet.Cell(currentRow, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                    currentRow++;
                }

                worksheet.Columns(1, 4).AdjustToContents();
                worksheet.Column(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        $"ThongKeLuotMuon_Nam{year}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
                    );
                }
            }
        }

        // ==========================================================
        // CHỨC NĂNG 2: THỐNG KÊ TÀI LIỆU VÀ TỒN KHO
        // ==========================================================

        [HttpGet]
        public IActionResult GetThongKeTaiLieu()
        {
            try
            {
                var banSaoDangMuon = _context.TGiaoDichMuonTra
                    .Where(g => g.NgayTra == null)
                    .Join(_context.TGiaoDichBanSao,
                        gd => gd.MaGd,
                        gdbs => gdbs.MaGd,
                        (gd, gdbs) => gdbs.MaBs)
                    .Distinct()
                    .ToList();

                var tongGiaoTrinh = _context.TBanSao
                    .Join(_context.TTaiLieu,
                        bs => bs.MaTl,
                        tl => tl.MaTl,
                        (bs, tl) => tl)
                    .Count(tl => tl.MaThL == "GT");

                var tongTaiLieu = _context.TBanSao
                    .Join(_context.TTaiLieu,
                        bs => bs.MaTl,
                        tl => tl.MaTl,
                        (bs, tl) => tl)
                    .Count(tl => tl.MaThL != "GT" && tl.MaThL != null);

                var giaoTrinhCoSan = _context.TBanSao
                    .Join(_context.TTaiLieu,
                        bs => bs.MaTl,
                        tl => tl.MaTl,
                        (bs, tl) => new { bs.MaBs, tl.MaThL })
                    .Count(x => x.MaThL == "GT" && !banSaoDangMuon.Contains(x.MaBs));

                var taiLieuCoSan = _context.TBanSao
                    .Join(_context.TTaiLieu,
                        bs => bs.MaTl,
                        tl => tl.MaTl,
                        (bs, tl) => new { bs.MaBs, tl.MaThL })
                    .Count(x => x.MaThL != "GT" && x.MaThL != null && !banSaoDangMuon.Contains(x.MaBs));

                var chiTietTheoDanhMuc = _context.TBanSao
                    .Join(_context.TTaiLieu,
                        bs => bs.MaTl,
                        tl => tl.MaTl,
                        (bs, tl) => new { bs.MaBs, tl.MaThL })
                    .Join(_context.TTheLoai,
                        x => x.MaThL,
                        thl => thl.MaThL,
                        (x, thl) => new { x.MaBs, x.MaThL, thl.TenThL })
                    .GroupBy(x => new { x.MaThL, x.TenThL })
                    .Select(g => new
                    {
                        MaThL = g.Key.MaThL,
                        TenDanhMuc = g.Key.TenThL,
                        TongSoLuong = g.Count(),
                        SoLuongCoSan = g.Count(x => !banSaoDangMuon.Contains(x.MaBs))
                    })
                    .Where(x => x.TongSoLuong > 0)
                    .OrderByDescending(x => x.TongSoLuong)
                    .ToList();

                var result = new
                {
                    tongGiaoTrinh = tongGiaoTrinh,
                    tongTaiLieu = tongTaiLieu,
                    giaoTrinhCoSan = giaoTrinhCoSan,
                    taiLieuCoSan = taiLieuCoSan,
                    chiTietTheoDanhMuc = chiTietTheoDanhMuc
                };

                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    error = ex.Message,
                    tongGiaoTrinh = 0,
                    tongTaiLieu = 0,
                    giaoTrinhCoSan = 0,
                    taiLieuCoSan = 0,
                    chiTietTheoDanhMuc = new object[] { }
                });
            }
        }

        [HttpGet]
        public IActionResult GetChiTietTheLoai(string maTheLoai)
        {
            if (string.IsNullOrEmpty(maTheLoai))
            {
                return BadRequest("Vui lòng cung cấp mã thể loại.");
            }

            try
            {
                var banSaoDangMuon = _context.TGiaoDichMuonTra
                    .Where(g => g.NgayTra == null)
                    .Join(_context.TGiaoDichBanSao,
                        gd => gd.MaGd,
                        gdbs => gdbs.MaGd,
                        (gd, gdbs) => gdbs.MaBs)
                    .Distinct()
                    .ToList();

                var chiTiet = _context.TBanSao
                    .Join(_context.TTaiLieu,
                        bs => bs.MaTl,
                        tl => tl.MaTl,
                        (bs, tl) => new { bs, tl })
                    .Where(x => x.tl.MaThL == maTheLoai)
                    .Select(x => new
                    {
                        MaBanSao = x.bs.MaBs,
                        TenTaiLieu = x.tl.TenTl,
                        TrangThai = banSaoDangMuon.Contains(x.bs.MaBs) ? "Đang mượn" : "Có sẵn"
                    })
                    .OrderBy(x => x.TenTaiLieu)
                    .ToList();

                return Json(chiTiet);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // ==========================================================
        // CHỨC NĂNG 3: THỐNG KÊ THẺ BẠN ĐỌC MỚI
        // ==========================================================

        [HttpGet]
        public async Task<IActionResult> GetBanDocMoiTheoThang(int year)
        {
            var thongKe = await _context.TTheBanDoc
                .Where(tbd => tbd.NgayCap.Year == year)
                .GroupBy(tbd => tbd.NgayCap.Month)
                .Select(g => new
                {
                    Thang = g.Key,
                    SoBanDocMoi = g.Count()
                })
                .OrderBy(x => x.Thang)
                .ToListAsync();

            var duLieuDayDu = Enumerable.Range(1, 12).Select(thang =>
            {
                var data = thongKe.FirstOrDefault(x => x.Thang == thang);
                return new
                {
                    Thang = thang,
                    SoBanDocMoi = data?.SoBanDocMoi ?? 0
                };
            }).ToList();

            return Json(duLieuDayDu);
        }

        [HttpGet]
        public async Task<IActionResult> ExportBanDocMoiToExcel(int year)
        {
            var thongKe = await _context.TTheBanDoc
                .Where(tbd => tbd.NgayCap.Year == year)
                .GroupBy(tbd => tbd.NgayCap.Month)
                .Select(g => new
                {
                    Thang = g.Key,
                    SoBanDocMoi = g.Count()
                })
                .OrderBy(x => x.Thang)
                .ToListAsync();

            var duLieuDayDu = Enumerable.Range(1, 12).Select(thang =>
            {
                var data = thongKe.FirstOrDefault(x => x.Thang == thang);
                return new
                {
                    Thang = thang,
                    SoBanDocMoi = data?.SoBanDocMoi ?? 0
                };
            }).ToList();

            var tongBanDocMoi = duLieuDayDu.Sum(x => x.SoBanDocMoi);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add($"BanDocMoi_Nam{year}");

                worksheet.Cell("A1").Value = $"BÁO CÁO THỐNG KÊ THẺ BẠN ĐỌC ĐƯỢC CẤP MỚI THEO THÁNG NĂM {year}";
                worksheet.Range("A1:D1").Merge().Style.Font.Bold = true;
                worksheet.Range("A1:D1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Range("A1:D1").Style.Font.FontSize = 14;

                worksheet.Cell("A3").Value = "Tổng Thẻ Bạn Đọc Cấp Mới Cả Năm:";
                worksheet.Cell("B3").Value = tongBanDocMoi;
                worksheet.Range("A3:B3").Style.Font.Bold = true;

                worksheet.Cell("A5").Value = "STT";
                worksheet.Cell("B5").Value = "Tháng";
                worksheet.Cell("C5").Value = "Số Thẻ Cấp Mới";
                worksheet.Cell("D5").Value = "Tỷ Lệ (%)";

                var headerRange = worksheet.Range("A5:D5");
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
                headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                var currentRow = 6;
                foreach (var item in duLieuDayDu)
                {
                    worksheet.Cell(currentRow, 1).Value = currentRow - 5;
                    worksheet.Cell(currentRow, 2).Value = item.Thang;
                    worksheet.Cell(currentRow, 3).Value = item.SoBanDocMoi;
                    worksheet.Cell(currentRow, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                    double tyLe = tongBanDocMoi > 0 ? (double)item.SoBanDocMoi / tongBanDocMoi : 0;
                    worksheet.Cell(currentRow, 4).Value = tyLe;
                    worksheet.Cell(currentRow, 4).Style.NumberFormat.Format = "0.00%";
                    worksheet.Cell(currentRow, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                    currentRow++;
                }

                worksheet.Columns(1, 4).AdjustToContents();
                worksheet.Column(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        $"ThongKeTheBanDocMoi_Nam{year}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
                    );
                }
            }
        }

        // ==========================================================
        // CHỨC NĂNG 4: THỐNG KÊ TÀI LIỆU MỚI THEO THÁNG
        // ==========================================================

        [HttpGet]
        public async Task<IActionResult> GetTaiLieuMoiTheoThang(int year)
        {
            var thongKe = await _context.TTaiLieu
                .Where(tl => tl.MaTkNavigation.NgayTao.Year == year)
                .GroupBy(tl => tl.MaTkNavigation.NgayTao.Month)
                .Select(g => new
                {
                    Thang = g.Key,
                    SoTaiLieuMoi = g.Count()
                })
                .OrderBy(x => x.Thang)
                .ToListAsync();

            var duLieuDayDu = Enumerable.Range(1, 12).Select(thang =>
            {
                var data = thongKe.FirstOrDefault(x => x.Thang == thang);
                return new
                {
                    Thang = thang,
                    SoTaiLieuMoi = data?.SoTaiLieuMoi ?? 0
                };
            }).ToList();

            return Json(duLieuDayDu);
        }

        [HttpGet]
        public async Task<IActionResult> ExportTaiLieuMoiToExcel(int year)
        {
            var thongKe = await _context.TTaiLieu
                .Where(tl => tl.MaTkNavigation.NgayTao.Year == year)
                .GroupBy(tl => tl.MaTkNavigation.NgayTao.Month)
                .Select(g => new
                {
                    Thang = g.Key,
                    SoTaiLieuMoi = g.Count()
                })
                .OrderBy(x => x.Thang)
                .ToListAsync();

            var duLieuDayDu = Enumerable.Range(1, 12).Select(thang =>
            {
                var data = thongKe.FirstOrDefault(x => x.Thang == thang);
                return new
                {
                    Thang = thang,
                    SoTaiLieuMoi = data?.SoTaiLieuMoi ?? 0
                };
            }).ToList();

            var tongTaiLieuMoi = duLieuDayDu.Sum(x => x.SoTaiLieuMoi);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add($"TaiLieuMoi_Nam{year}");

                worksheet.Cell("A1").Value = $"BÁO CÁO THỐNG KÊ TÀI LIỆU MỚI THEO THÁNG NĂM {year}";
                worksheet.Range("A1:D1").Merge().Style.Font.Bold = true;
                worksheet.Range("A1:D1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Range("A1:D1").Style.Font.FontSize = 14;

                worksheet.Cell("A3").Value = "Tổng Tài Liệu Mới Cả Năm:";
                worksheet.Cell("B3").Value = tongTaiLieuMoi;
                worksheet.Range("A3:B3").Style.Font.Bold = true;

                worksheet.Cell("A5").Value = "STT";
                worksheet.Cell("B5").Value = "Tháng";
                worksheet.Cell("C5").Value = "Số Tài Liệu Mới";
                worksheet.Cell("D5").Value = "Tỷ Lệ (%)";

                var headerRange = worksheet.Range("A5:D5");
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
                headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                var currentRow = 6;
                foreach (var item in duLieuDayDu)
                {
                    worksheet.Cell(currentRow, 1).Value = currentRow - 5;
                    worksheet.Cell(currentRow, 2).Value = item.Thang;
                    worksheet.Cell(currentRow, 3).Value = item.SoTaiLieuMoi;
                    worksheet.Cell(currentRow, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                    double tyLe = tongTaiLieuMoi > 0 ? (double)item.SoTaiLieuMoi / tongTaiLieuMoi : 0;
                    worksheet.Cell(currentRow, 4).Value = tyLe;
                    worksheet.Cell(currentRow, 4).Style.NumberFormat.Format = "0.00%";
                    worksheet.Cell(currentRow, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                    currentRow++;
                }

                worksheet.Columns(1, 4).AdjustToContents();
                worksheet.Column(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        $"ThongKeTaiLieuMoi_Nam{year}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
                    );
                }
            }
        }

        // ==========================================================
        // CHỨC NĂNG 5: THỐNG KÊ TÀI KHOẢN THEO VAI TRÒ
        // ==========================================================

        [HttpGet]
        public async Task<IActionResult> GetTaiKhoanTheoVaiTro()
        {
            var thongKe = await _context.TTaiKhoan
                .GroupBy(tk => tk.MaVtNavigation.TenVt)
                .Select(g => new
                {
                    VaiTro = g.Key,
                    SoLuong = g.Count()
                })
                .OrderByDescending(x => x.SoLuong)
                .ToListAsync();

            return Json(thongKe);
        }
    }
}