using System;
using System.Collections.Generic;

namespace locy_api.Models.Domains;

public partial class TblDmchucVu
{
    public long Id { get; set; }

    public string? Code { get; set; }

    public string? NameVi { get; set; }

    public string? NameEn { get; set; }

    public bool? FlagFavorite { get; set; }

    public int? ShowOrder { get; set; }

    public virtual ICollection<TblNhanSu> TblNhanSus { get; set; } = new List<TblNhanSu>();
}
