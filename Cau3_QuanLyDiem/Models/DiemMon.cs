using System;
using System.Collections.Generic;

namespace Cau3_QuanLyDiem.Models;

public partial class DiemMon
{
    public int MaDiem { get; set; }

    public string MaSv { get; set; } = null!;

    public string TenMon { get; set; } = null!;

    public int? SoTinChi { get; set; }

    public decimal? Diem { get; set; }

    public virtual SinhVien MaSvNavigation { get; set; } = null!;
}
