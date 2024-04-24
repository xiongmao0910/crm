using System;
using System.Collections.Generic;

namespace locy_api.Models.Domains;

public partial class TblDmcustomerNghiepVu
{
    public int Id { get; set; }

    public long? Iddmcustomer { get; set; }

    public long? IddmnghiepVu { get; set; }

    public virtual TblDmcustomer? IddmcustomerNavigation { get; set; }

    public virtual TblDmnghiepVu? IddmnghiepVuNavigation { get; set; }
}
