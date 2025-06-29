using System;
using System.Collections.Generic;

namespace OnTapCuoiKy_De1.Models;

public partial class Thuoc
{
    public string MaThuoc { get; set; } = null!;

    public string? TenThuoc { get; set; }

    public double? GiaBan { get; set; }

    public int? SoLuong { get; set; }

    public string? MaDm { get; set; }

    public virtual DanhMucThuoc? MaDmNavigation { get; set; }
}
