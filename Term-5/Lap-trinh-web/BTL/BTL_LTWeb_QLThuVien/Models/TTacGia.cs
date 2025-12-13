using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation; // Dòng này là cần thiết

namespace Library_Manager.Models;

public partial class TTacGia
{
    [ValidateNever] // MaTg được sinh tự động, không cần kiểm tra validation ở tầng ứng dụng
    public string MaTg { get; set; } = null!;

    public string MaQg { get; set; } = null!;

    public string HoDem { get; set; } = null!;

    public string Ten { get; set; } = null!;

    [ValidateNever] // Loại bỏ validation cho Navigation Property
    public virtual TQuocGia MaQgNavigation { get; set; } = null!;

    public virtual ICollection<TTaiLieuTacGia> TTaiLieuTacGia { get; set; } = new List<TTaiLieuTacGia>();
}