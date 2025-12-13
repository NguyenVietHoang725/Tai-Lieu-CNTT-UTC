// File: LibraryManagerApp.Helpers/ExcelHelper.cs

using OfficeOpenXml; // Cần using namespace này (từ EPPlus)
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing; // Để dùng Color
using System.Windows.Forms; // Để hiện MessageBox

namespace LibraryManagerApp.Helpers
{
    public static class ExcelHelper
    {
        // Cấu hình License Context
        static ExcelHelper()
        {
            try
            {
                // Đặt License Context trước khi sử dụng EPPlus
                OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                System.Diagnostics.Debug.WriteLine($"Error setting EPPlus license: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Hàm xuất danh sách dữ liệu ra file Excel.
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu của danh sách (VD: TheBanDocDTO)</typeparam>
        /// <param name="dataList">Danh sách dữ liệu cần xuất</param>
        /// <param name="columnMapping">Dictionary ánh xạ: Key = Tên thuộc tính trong DTO, Value = Tên cột hiển thị trên Excel</param>
        /// <param name="filePath">Đường dẫn lưu file</param>
        /// <returns>True nếu thành công, False nếu lỗi</returns>
        public static bool ExportToExcel<T>(List<T> dataList, Dictionary<string, string> columnMapping, string filePath)
        {
            try
            {
                if (dataList == null || dataList.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                // Tạo file Excel mới
                using (var package = new ExcelPackage())
                {
                    // Tạo một Sheet tên là "Data"
                    var worksheet = package.Workbook.Worksheets.Add("Data");

                    // 1. TẠO HEADER (DÒNG 1)
                    int colIndex = 1;
                    foreach (var map in columnMapping)
                    {
                        var cell = worksheet.Cells[1, colIndex];
                        cell.Value = map.Value; // Tên hiển thị (Value của Dictionary)

                        // Format Header cho đẹp
                        cell.Style.Font.Bold = true;
                        cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        cell.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        cell.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                        colIndex++;
                    }

                    // 2. ĐỔ DỮ LIỆU (TỪ DÒNG 2)
                    int rowIndex = 2;
                    foreach (var item in dataList)
                    {
                        colIndex = 1;
                        foreach (var map in columnMapping)
                        {
                            string propertyName = map.Key; // Tên thuộc tính trong Code (Key của Dictionary)

                            // Dùng Reflection để lấy giá trị của thuộc tính từ object item
                            var propertyInfo = typeof(T).GetProperty(propertyName);
                            if (propertyInfo != null)
                            {
                                var value = propertyInfo.GetValue(item, null);

                                // Xử lý định dạng đặc biệt cho Ngày tháng
                                if (value is DateTime dateValue)
                                {
                                    worksheet.Cells[rowIndex, colIndex].Value = dateValue.ToString("dd/MM/yyyy");
                                }
                                else
                                {
                                    worksheet.Cells[rowIndex, colIndex].Value = value;
                                }
                            }
                            colIndex++;
                        }
                        rowIndex++;
                    }

                    // 3. TỰ ĐỘNG CHỈNH ĐỘ RỘNG CỘT
                    try
                    {
                        // Duyệt qua từng cột để tính độ rộng phù hợp
                        for (int col = 1; col <= columnMapping.Count; col++)
                        {
                            double maxLength = 0;

                            // Duyệt qua tất cả các dòng trong cột (bao gồm cả header)
                            for (int row = 1; row <= rowIndex - 1; row++)
                            {
                                var cellValue = worksheet.Cells[row, col].Value;
                                if (cellValue != null)
                                {
                                    double length = cellValue.ToString().Length;
                                    if (length > maxLength)
                                    {
                                        maxLength = length;
                                    }
                                }
                            }

                            // Tính độ rộng: độ dài * hệ số (1.2) + padding (2)
                            // Giới hạn min = 10, max = 50
                            double columnWidth = Math.Max(10, Math.Min(maxLength * 1.2 + 2, 50));
                            worksheet.Column(col).Width = columnWidth;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Fallback: Nếu lỗi thì set độ rộng mặc định
                        System.Diagnostics.Debug.WriteLine($"Error auto-sizing columns: {ex.Message}");
                        for (int col = 1; col <= columnMapping.Count; col++)
                        {
                            worksheet.Column(col).Width = 20;
                        }
                    }

                    // 4. LƯU FILE
                    FileInfo fi = new FileInfo(filePath);
                    package.SaveAs(fi);
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}