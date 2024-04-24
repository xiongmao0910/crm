using System;
using System.Collections.Generic;

namespace locy_api.Models.Domains;

public partial class TblDmphongBan
{
    public long Id { get; set; }

    public long? ParentId { get; set; }

    public string? NameVi { get; set; }

    public string? NameEn { get; set; }

    public string? GhiChu { get; set; }

    public bool? FlagFavorite { get; set; }

    public long? IddmvanPhong { get; set; }

    public int? EnumNoiDiaOrXuatNhap { get; set; }

    public bool? FlagQuanLyJob { get; set; }

    public bool? FlagPhuTrachJob { get; set; }

    public bool? FlagSale { get; set; }

    public bool? FlagQuanLyKhachHang { get; set; }

    public virtual TblDmvanPhong? IddmvanPhongNavigation { get; set; }

    public virtual ICollection<TblNhanSu> TblNhanSus { get; set; } = new List<TblNhanSu>();
}
