namespace locy_api.Models.Requests
{
    public class CreateTransportationRequest
    {
        public required string Code { get; set; }
        public required string NameVI { get; set; }
        public string? NameEN { get; set; }
    }
}
