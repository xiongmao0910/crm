using System;
using System.Collections.Generic;

namespace locy_api.Models.Domains;

public partial class TblDmaction
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public bool? FlagFavorite { get; set; }
}
