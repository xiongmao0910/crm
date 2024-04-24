using System;
using System.Collections.Generic;

namespace locy_api.Models.Domains;

public partial class TblJobnotification
{
    public long Id { get; set; }

    public long? Idjob { get; set; }

    public long? Idobject { get; set; }

    public DateTime? NgayThongBao { get; set; }

    public bool? FlagTraTheoKy { get; set; }
}
