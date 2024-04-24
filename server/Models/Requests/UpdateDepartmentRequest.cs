namespace locy_api.Models.Requests
{
    public class UpdateDepartmentRequest
    {
        public long Id { get; set; }
        public string? NameVi { get; set; }
        public string? NameEn { get; set; }
        public long? IdVanPhong { get; set; }
    }
}
