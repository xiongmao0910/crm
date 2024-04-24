using System;
using System.Collections.Generic;

namespace locy_api.Models.Domains;

public partial class TblDmphanLoaiKhachHang
{
    public long Id { get; set; }

    public string? Code { get; set; }

    public string? NameVi { get; set; }

    public string? NameEn { get; set; }

    public virtual ICollection<TblDmcustomerPhanLoaiKh> TblDmcustomerPhanLoaiKhs { get; set; } = new List<TblDmcustomerPhanLoaiKh>();
}
