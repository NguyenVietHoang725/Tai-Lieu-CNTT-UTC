namespace MyWebApp.Interfaces
{
    public interface IBufferedFileUploadService
    {
        Task<string> UploadFile(IFormFile file, int studentId);
    }
}
