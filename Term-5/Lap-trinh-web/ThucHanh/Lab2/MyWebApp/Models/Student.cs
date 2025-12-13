using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models
{
    public class Student
    {
        public int Id { get; set; } // Mã sinh viên
        public string? Name { get; set; } // Tên sinh viên
        public string? Email { get; set; } // Email sinh viên
        public string? Password { get; set; } // Mật khẩu sinh viên
        public Branch? Branch { get; set; } // Ngành học
        public Gender? Gender { get; set; } // Giới tính
        public bool IsRegular { get; set; } // Học chính quy hay không: true - chính quy, false - không chính quy
        public string? Address { get; set; } // Địa chỉ sinh viên
        public DateTime DateOfBirth { get; set; } // Ngày sinh
        public string? ImagePath { get; set; } // Đường dẫn ảnh đại diện
    }
}
