﻿using System;
using System.Collections.Generic;

namespace locy_api.Models.Domains;

public partial class TblSysCodeKyHieu
{
    public long Id { get; set; }

    public long? IdsysCode { get; set; }

    public int? EnumLoaiKh { get; set; }

    public string? Kh { get; set; }

    public virtual TblSysCode? IdsysCodeNavigation { get; set; }
}
