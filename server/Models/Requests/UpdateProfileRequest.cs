namespace locy_api.Models.Requests
{
    public class UpdateProfileRequest
    {
        public long Id { get; set; }
        public string? hoten {  get; set; }
        public string? namsinh { get; set; }
        public int? gioitinh { get; set; }
        public string? quequan { get; set; }
        public string? diachi { get; set; }
        public string? soCMT { get; set; }
        public string? noiCapCMT { get; set; }
        public string? ngayCapCMT { get; set; }
        public string? didong { get; set; }
        public string? email { get; set; }
        public string? PhotoURL { get; set; }
    }
}
