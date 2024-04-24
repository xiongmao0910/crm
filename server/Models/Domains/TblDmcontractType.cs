using System;
using System.Collections.Generic;

namespace locy_api.Models.Domains;

public partial class TblDmcontractType
{
    public long Id { get; set; }

    public string? Code { get; set; }

    public string? NameVi { get; set; }

    public string? NameEn { get; set; }

    public string? Note { get; set; }

    public bool? FlagFavorite { get; set; }
}
