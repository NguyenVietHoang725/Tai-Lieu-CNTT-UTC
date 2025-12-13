using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models
{
    public class Student
    {
        public int Id { get; set; } // Mã sinh viên

        [Display(Name = "Họ và tên")]
        [Required(ErrorMessage = "Vui lòng nhập họ và tên")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Họ và tên phải có từ 4 đến 100 ký tự")]
        public string? Name { get; set; } // Tên sinh viên

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Vui lòng nhập email")]
        [RegularExpression(@"^[A-Za-z0-9._%+-]+@gmail+\.com$", ErrorMessage = "Email phải có đuôi @gmail.com")]
        public string? Email { get; set; } // Email sinh viên

        [Display(Name = "Mật khẩu")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Mật khẩu phải có ít nhất {2} ký tự")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", 
            ErrorMessage = "Mật khẩu phải có ít nhất 8 ký tự, bao gồm chữ thường, chữ hoa, số và ký tự đặc biệt")]
        public string? Password { get; set; } // Mật khẩu sinh viên

        [Display(Name = "Ngành học")]
        [Required(ErrorMessage = "Vui lòng chọn ngành học")]
        public Branch? Branch { get; set; } // Ngành học

        [Display(Name = "Giới tính")]
        [Required(ErrorMessage = "Giới tính bắt buộc phải được chọn")]
        public Gender? Gender { get; set; } // Giới tính

        [Display(Name = "Chính quy")]
        public bool IsRegular { get; set; } // Học chính quy hay không: true - chính quy, false - không chính quy

        [Display(Name = "Địa chỉ")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Địa chỉ bắt buộc phải được nhập")]
        public string? Address { get; set; } // Địa chỉ sinh viên

        [Display(Name = "Ngày sinh")]
        [Range(typeof(DateTime), "1/1/1963", "31/12/2005", ErrorMessage = "Ngày sinh phải từ 1/1/1963 đến 31/12/2005")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Vui lòng nhập ngày sinh")]
        public DateTime DateOfBirth { get; set; } // Ngày sinh

        [Display(Name = "Ảnh đại diện")]
        [BindNever]
        public string? ImagePath { get; set; } // Đường dẫn ảnh đại diện

        [Display(Name = "Điểm")]
        [Required(ErrorMessage = "Vui lòng nhập điểm")]
        [Range(0.0, 10.0, ErrorMessage = "Điểm phải nằm trong khoảng từ 0.0 đến 10.0")]
        public double Score { get; set; } // Điểm sinh viên
    }
}
