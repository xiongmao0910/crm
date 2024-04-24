using System;
using System.Collections.Generic;

namespace locy_api.Models.Domains;

public partial class TblDmnghiepVu
{
    public long Id { get; set; }

    public string? Code { get; set; }

    public string? NameVi { get; set; }

    public string? NameEn { get; set; }

    public virtual ICollection<TblDmcustomerNghiepVu> TblDmcustomerNghiepVus { get; set; } = new List<TblDmcustomerNghiepVu>();
}
