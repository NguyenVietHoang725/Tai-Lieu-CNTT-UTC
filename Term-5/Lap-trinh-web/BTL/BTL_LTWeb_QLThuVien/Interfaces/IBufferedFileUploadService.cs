namespace Library_Manager.Interfaces
{
    public interface IBufferedFileUploadService
    {
        // Thay đổi kiểu trả về từ Task<bool> thành Task<string> (đường dẫn tương đối)
        // Thêm tham số MaTl để sử dụng trong việc đặt tên file duy nhất
        Task<string> UploadFile(IFormFile file, string maTl);
    }
}