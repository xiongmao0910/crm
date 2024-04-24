namespace locy_api.Models.DTOs
{
    public class ProfileDto
    {
        public long Id { get; set; }
        public required string username { get; set; }
        public bool? active { get; set; }
        public string? permission { get; set; }
        public long? IDNhanVien { get; set; }
        public long? idChucVu { get; set; }
        public string? chucvu { get; set; }
        public long? idPhongBan { get; set; }
        public string? phongban { get; set; }
        public long? idVanPhong { get; set; }
        public string? vanphong { get; set; }
        public string? manhanvien { get; set; }
        public string? hoten { get; set; }
        public string? hotenen { get; set; }
        public string? namsinh { get; set; }
        public int? gioitinh { get; set; }
        public string? quequan { get; set; }
        public string? diachi { get; set; }
        public string? soCMT { get; set; }
        public string? noiCapCMT { get; set; }
        public string? ngayCapCMT { get; set; }
        public string? photoURL { get; set; }
        public string? didong { get; set; }
        public string? email { get; set; }
        public string? ghichu { get; set; }
        public string? createDate { get; set; }
        public string? editDate { get; set; }
        public int? soLuongKH { get; set; }
        public bool? flagDelete { get; set; }
        public long? idUserDelete { get; set; }
        public string? dateDelete { get; set; }
    }
}
