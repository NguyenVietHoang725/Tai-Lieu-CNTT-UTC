using Library_Manager.Interfaces;
using Microsoft.AspNetCore.Hosting; // Cần thiết để truy cập wwwroot
using System;
using System.IO;
using System.Threading.Tasks;

namespace Library_Manager.Services
{
    public class BufferedFileUploadLocalService : IBufferedFileUploadService
    {
        // Khai báo để truy cập thông tin môi trường hosting
        private readonly IWebHostEnvironment _hostingEnvironment;

        // Constructor để Inject IWebHostEnvironment
        public BufferedFileUploadLocalService(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        // Cập nhật phương thức: trả về string (đường dẫn tương đối)
        public async Task<string> UploadFile(IFormFile file, string maTl)
        {
            // Kiểm tra file có hợp lệ không
            if (file == null || file.Length == 0)
            {
                return null; // Trả về null nếu không có file
            }

            try
            {
                // 1. Định nghĩa thư mục lưu trong wwwroot
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Assets", "img", "TaiLieu");

                // 2. Tạo thư mục nếu chưa tồn tại
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // 3. Tạo tên file duy nhất: [MaTL]_[GUID].ext
                string fileExtension = Path.GetExtension(file.FileName);
                // Sử dụng MaTl và một GUID để đảm bảo tính duy nhất
                string uniqueFileName = $"{maTl}_{Guid.NewGuid().ToString().Substring(0, 8)}{fileExtension}";
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // 4. Lưu file vào thư mục đích
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                // 5. Trả về đường dẫn tương đối để lưu vào DB 
                // (Bắt đầu từ thư mục Assets trong wwwroot, và thay thế ký tự '\\' thành '/' cho URL)
                string relativePath = Path.Combine("/Assets/img/TaiLieu", uniqueFileName).Replace('\\', '/');

                return relativePath; // Trả về đường dẫn tương đối thành công

            }
            catch (Exception ex)
            {
                // Thay vì chỉ throw, nên log lỗi và throw Exception rõ ràng
                throw new Exception($"Lỗi khi sao chép tệp tin cho Tài liệu {maTl}: {ex.Message}", ex);
            }
        }
    }
}