namespace locy_api.Models.Requests
{
    public class CreateEmployeeRequest
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public string? Permission {  get; set; }
        public required long IdChucVu { get; set; }
        public required long IdPhongBan { get; set; }
        public required long IdVanPhong { get; set; }
        public required string manhanvien { get; set; }
        public required string HoTen { get; set; }
        public required string NamSinh { get; set; }
        public int? GioiTinh { get; set; }
        public string? QueQuan { get; set; }
        public string? DiaChi { get; set; }
        public string? SoCMT { get; set; }
        public string? NoiCapCMT { get; set; }
        public string? NgayCapCMT { get; set; }
        public string? PhotoURL { get; set; }
        public string? GhiChu { get; set; }
        public int? SoLuongKH { get; set; }
    }
}
