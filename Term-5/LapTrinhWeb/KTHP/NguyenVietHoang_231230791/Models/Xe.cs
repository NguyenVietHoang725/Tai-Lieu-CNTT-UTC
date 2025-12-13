using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NguyenVietHoang_231230791.Models;

public partial class Xe
{
    [Required(ErrorMessage = "Biển số xe không được để trống")]
    [RegularExpression(@"^\d{2}[A-Za-z]\d{3}\.\d{2}$", ErrorMessage = "Biển số phải có dạng ##X###.## (VD: 29T123.45)")]
    public string SoXe { get; set; } = null!;

    public string? MauXe { get; set; }

    public int? SoChoNgoi { get; set; }

    public int? MaLoaiXe { get; set; }

    public string? MaCongTy { get; set; }

    public string? Anh { get; set; }

    public virtual ICollection<Chuyen> Chuyens { get; set; } = new List<Chuyen>();

    [ValidateNever]
    public virtual CongTy? MaCongTyNavigation { get; set; }
    [ValidateNever]
    public virtual LoaiXe? MaLoaiXeNavigation { get; set; }
}
