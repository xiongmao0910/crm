﻿namespace locy_api.Models.Requests
{
    public class UpdateCountryRequest
    {
        public long Id { get; set; }
        public string? Code { get; set; }
        public string? NameVi { get; set; }
        public string? NameEn { get; set; }
    }
}
