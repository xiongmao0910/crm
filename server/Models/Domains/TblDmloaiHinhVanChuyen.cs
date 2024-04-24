using System;
using System.Collections.Generic;

namespace locy_api.Models.Domains;

public partial class TblDmloaiHinhVanChuyen
{
    public long Id { get; set; }

    public string? NameVi { get; set; }

    public string? NameEn { get; set; }

    public string? Code { get; set; }

    public bool? FlagFavorite { get; set; }

    public virtual ICollection<TblDmcustomerTuyenHang> TblDmcustomerTuyenHangs { get; set; } = new List<TblDmcustomerTuyenHang>();
}
