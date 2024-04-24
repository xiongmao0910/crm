namespace locy_api.Models.Requests
{
    public class CreateCustomerRequest
    {
        public required long IdQuocGia { get; set; }
        public required long IdCity { get; set; }
        public string? Code { get; set; }
        public string? NameVI { get; set; }
        public string? NameEN { get; set; }
        public string? AddressVI { get; set; }
        public string? AddressEN { get; set; }
        public string? TaxCode { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public string? Note { get; set; }
        public required long IdUserCreate { get; set; }
    }
}
