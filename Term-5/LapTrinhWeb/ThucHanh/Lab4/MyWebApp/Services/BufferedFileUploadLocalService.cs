using MyWebApp.Interfaces;

namespace MyWebApp.Services
{
    public class BufferedFileUploadLocalService : IBufferedFileUploadService
    {
        public async Task<string> UploadFile(IFormFile file, int studentId)
        {
            try
            {
                if (file.Length > 0)
                {
                    // Tạo thư mục riêng cho từng sinh viên
                    string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedFiles", "Students", studentId.ToString());
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    // Đặt lại tên file để tránh trùng (dùng Guid + đuôi gốc)
                    string fileExtension = Path.GetExtension(file.FileName);
                    string uniqueFileName = $"avatar{fileExtension}";
                    string filePath = Path.Combine(folderPath, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    // Trả về đường dẫn tương đối để lưu trong DB
                    return $"/UploadedFiles/Students/{studentId}/{uniqueFileName}";
                }
                return null!;
            }
            catch (Exception ex)
            {
                throw new Exception("File Copy Failed", ex);
            }
        }

    }
}
