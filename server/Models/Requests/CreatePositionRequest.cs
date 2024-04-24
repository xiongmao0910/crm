namespace locy_api.Models.Requests
{
    public class CreatePositionRequest
    {
        public required string Code { get; set; }
        public required string NameVi { get; set; }
        public string? NameEn { get; set; }
    }
}
