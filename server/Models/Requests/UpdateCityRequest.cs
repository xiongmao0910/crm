﻿namespace locy_api.Models.Requests
{
    public class UpdateCityRequest
    {
        public long Id { get; set; }
        public string? Code { get; set; }
        public long? IdQuocGia { get; set; }
        public string? NameVI { get; set; }
        public string? NameEN { get; set; }
    }
}
