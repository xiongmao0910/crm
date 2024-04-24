using System;
using System.Collections.Generic;

namespace locy_api.Models.Domains;

public partial class TblDmloaiDoanhNghiep
{
    public long Id { get; set; }

    public string? Code { get; set; }

    public string? NameVi { get; set; }

    public string? NameEn { get; set; }

    public virtual ICollection<TblDmcustomer> TblDmcustomers { get; set; } = new List<TblDmcustomer>();
}
