namespace locy_api.Models.Requests
{
    public class UpdatePortRequest
    {
        public long Id { get; set; }
        public long? IdQuocGia { get; set; }
        public long? IdCity { get; set; }
        public string? Code { get; set; }
        public string? TaxCode { get; set; }
        public string? NameVi { get; set; }
        public string? NameEn { get; set; }
        public string? AddressVi { get; set; }
        public string? AddressEn { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public string? Note { get; set; }
    }
}
