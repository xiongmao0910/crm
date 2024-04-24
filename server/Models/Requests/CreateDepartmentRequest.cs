namespace locy_api.Models.Requests
{
    public class CreateDepartmentRequest
    {
        public required string NameVi { get; set; }
        public string? NameEn { get; set; }
        public required long IdVanPhong {  get; set; }
    }
}
