﻿using System;
using System.Collections.Generic;

namespace locy_api.Models.Domains;

public partial class TblDmcurrency
{
    public long Id { get; set; }

    public string? Code { get; set; }

    public string? NameVi { get; set; }

    public string? NameEn { get; set; }

    public string? KyHieu { get; set; }

    public string? Note { get; set; }

    public bool? IsBasicCurrency { get; set; }

    public bool? FlagFavorite { get; set; }

    public virtual ICollection<TblDmbankAccount> TblDmbankAccounts { get; set; } = new List<TblDmbankAccount>();

    public virtual ICollection<TblDmvanPhong> TblDmvanPhongs { get; set; } = new List<TblDmvanPhong>();
}
