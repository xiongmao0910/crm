﻿using System;
using System.Collections.Generic;

namespace locy_api.Models.Domains;

public partial class TblSysCodeKey
{
    public long Id { get; set; }

    public long? IdsysCode { get; set; }

    public int? EnumLoaiKey { get; set; }

    public string? KeyCode { get; set; }

    public string? Format { get; set; }

    public virtual TblSysCode? IdsysCodeNavigation { get; set; }
}
