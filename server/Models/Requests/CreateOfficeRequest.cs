﻿namespace locy_api.Models.Requests
{
    public class CreateOfficeRequest
    {
        public required string Code { get; set; }
        public required string NameVi {  get; set; }
        public string? NameEn { get; set; }
        public string? AddressVi { get; set; }
        public string? AddressEn { get; set; }
        public long? IdCountry { get; set; }
        public long? IdCity { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public string? Note { get; set; }
        public string? TaxCode { get; set; }
    }
}
