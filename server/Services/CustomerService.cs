// ** library **
using Microsoft.EntityFrameworkCore;
// ** architecture **
using locy_api.Data;
using locy_api.Interfaces;
using locy_api.Models.DTOs;
using locy_api.Models.Domains;
using locy_api.Models.Requests;

namespace locy_api.Services
{
    public class CustomerService: ICustomerService
    {
        private readonly BestCareDbContext _db;
        private readonly IEmployeeHelper _employeeHelper;

        public CustomerService(BestCareDbContext db, IEmployeeHelper employeeHelper)
        {
            _db = db;
            _employeeHelper = employeeHelper;
        }

        public async Task<List<CustomerDto>?> GetCustomers(int Start = 0, int Size = 10, string Search = "", bool isDelete = false)
        {
            List<CustomerDto>? data;

            if(Search == "")
            {
                data = await _db.TblDmcustomers.Select(c => new CustomerDto()
                                {
                                    Id = c.Id, IdQuocGia = c.IdquocGia, IdCity = c.Idcity, NameVI = c.NameVi ?? "", NameEN = c.NameEn ?? "", Code = c.Code ?? "", AddressVI = c.AddressVi ?? "", AddressEN = c.AddressEn ?? "", TaxCode = c.TaxCode ?? "",
                                    QuocGia = _db.TblDmcountries.Where(x => x.Id == c.IdquocGia).Select(x => x.NameVi).FirstOrDefault() ?? "", ThanhPho = _db.TblDmcities.Where(x => x.Id == c.Idcity).Select(x => x.NameVi).FirstOrDefault() ?? "",
                                    Phone = c.Phone ?? "", Fax = c.Fax ?? "", Email = c.Email ?? "", Website = c.Website ?? "", Note = c.Note ?? "", IdNhanVienSale = c.IdnhanVienSale, IdUserCreate = c.IduserCreate, FlagActive = c.FlagActive, FlagDel = c.FlagDel,
                                    NguoiTao = c.IduserCreate == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserCreate).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    EnumGiaoNhan = c.EnumGiaoNhan != null ? c.EnumGiaoNhan : 0, NhanVien = _db.TblNhanSus.Where(x => x.Id == c.IdnhanVienSale).Select(x => x.HoTenVi).FirstOrDefault() ?? "",
                                    DateCreate = c.DateCreate != null ? string.Format("{0:yyyy-MM-dd}", c.DateCreate) : "", EnumLoaiKhachHang = c.EnumLoaiKhachHang, IdLoaiDoanhNghiep = c.IdloaiDoanhNghiep,
                                    LoaiDoanhNghiep = _db.TblDmloaiDoanhNghieps.Where(x => x.Id == c.IdloaiDoanhNghiep).Select(x => x.NameVi).FirstOrDefault() ?? "", IdUserDelete = c.IduserDelete,
                                    NguoiXoa = c.IduserDelete == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserDelete).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    DateDelete = c.DateDelete != null ? string.Format("{0:yyyy-MM-dd}", c.DateDelete) : "", LyDoXoa = c.LyDoXoa ?? "", NgayTuongTac = c.NgayTuongTac != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuongTac) : "",
                                    NgayChonKhach = c.NgayChonKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChonKhach) : "", NgayTraVe = c.NgayTraVe != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTraVe) : "",
                                    NgayChotKhach = c.NgayChotKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChotKhach) : "", NgayTacNghiep = c.NgayTacNghiep != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTacNghiep) : "",
                                    SttMaxTacNghiep = c.SttmaxTacNghiep, NgayGiao = c.NgayGiao != null ? string.Format("{0:yyyy-MM-dd}", c.NgayGiao) : "", NgayNhan = c.NgayNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayNhan) : "",
                                    IdUserGiaoViec = c.IduserGiaoViec, IdUserTraKhach = c.IduserTraKhach, NgayKetThucNhan = c.NgayKetThucNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayKetThucNhan) : "", ThongTinGiaoViec = c.ThongTinGiaoViec ?? "",
                                    NguoiGiaoViec = c.IduserGiaoViec == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserGiaoViec).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    NguoiTraKhach = c.IduserTraKhach == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserTraKhach).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    ListTacNghiepText = c.ListTacNghiepText ?? "", ListTuyenHangText = c.ListTuyenHangText ?? "", ListPhanHoiText = c.ListPhanHoiText ?? "", NgayTuTraKhach = c.NgayTuTraKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuTraKhach) : "",
                                    LyDoTuChoi = c.LyDoTuChoi ?? "", IdTacNghiepCuoi = c.IdtacNghiepCuoi
                                }).Where(c => (isDelete == false && (c.FlagDel == null || c.FlagDel == isDelete)) || (isDelete == true && c.FlagDel == isDelete))
                                .OrderByDescending(c => c.Id).Skip(Start).Take(Size).ToListAsync();
            } else
            {
                data = await _db.TblDmcustomers.Select(c => new CustomerDto()
                                {
                                    Id = c.Id, IdQuocGia = c.IdquocGia, IdCity = c.Idcity, NameVI = c.NameVi ?? "", NameEN = c.NameEn ?? "", Code = c.Code ?? "", AddressVI = c.AddressVi ?? "", AddressEN = c.AddressEn ?? "", TaxCode = c.TaxCode ?? "",
                                    QuocGia = _db.TblDmcountries.Where(x => x.Id == c.IdquocGia).Select(x => x.NameVi).FirstOrDefault() ?? "", ThanhPho = _db.TblDmcities.Where(x => x.Id == c.Idcity).Select(x => x.NameVi).FirstOrDefault() ?? "",
                                    Phone = c.Phone ?? "", Fax = c.Fax ?? "", Email = c.Email ?? "", Website = c.Website ?? "", Note = c.Note ?? "", IdNhanVienSale = c.IdnhanVienSale, IdUserCreate = c.IduserCreate, FlagActive = c.FlagActive, FlagDel = c.FlagDel,
                                    NguoiTao = c.IduserCreate == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserCreate).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    EnumGiaoNhan = c.EnumGiaoNhan != null ? c.EnumGiaoNhan : 0, NhanVien = _db.TblNhanSus.Where(x => x.Id == c.IdnhanVienSale).Select(x => x.HoTenVi).FirstOrDefault() ?? "",
                                    DateCreate = c.DateCreate != null ? string.Format("{0:yyyy-MM-dd}", c.DateCreate) : "", EnumLoaiKhachHang = c.EnumLoaiKhachHang, IdLoaiDoanhNghiep = c.IdloaiDoanhNghiep,
                                    LoaiDoanhNghiep = _db.TblDmloaiDoanhNghieps.Where(x => x.Id == c.IdloaiDoanhNghiep).Select(x => x.NameVi).FirstOrDefault() ?? "", IdUserDelete = c.IduserDelete,
                                    NguoiXoa = c.IduserDelete == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserDelete).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    DateDelete = c.DateDelete != null ? string.Format("{0:yyyy-MM-dd}", c.DateDelete) : "", LyDoXoa = c.LyDoXoa ?? "", NgayTuongTac = c.NgayTuongTac != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuongTac) : "",
                                    NgayChonKhach = c.NgayChonKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChonKhach) : "", NgayTraVe = c.NgayTraVe != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTraVe) : "",
                                    NgayChotKhach = c.NgayChotKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChotKhach) : "", NgayTacNghiep = c.NgayTacNghiep != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTacNghiep) : "",
                                    SttMaxTacNghiep = c.SttmaxTacNghiep, NgayGiao = c.NgayGiao != null ? string.Format("{0:yyyy-MM-dd}", c.NgayGiao) : "", NgayNhan = c.NgayNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayNhan) : "",
                                    IdUserGiaoViec = c.IduserGiaoViec, IdUserTraKhach = c.IduserTraKhach, NgayKetThucNhan = c.NgayKetThucNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayKetThucNhan) : "", ThongTinGiaoViec = c.ThongTinGiaoViec ?? "",
                                    NguoiGiaoViec = c.IduserGiaoViec == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserGiaoViec).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    NguoiTraKhach = c.IduserTraKhach == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserTraKhach).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    ListTacNghiepText = c.ListTacNghiepText ?? "", ListTuyenHangText = c.ListTuyenHangText ?? "", ListPhanHoiText = c.ListPhanHoiText ?? "", NgayTuTraKhach = c.NgayTuTraKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuTraKhach) : "",
                                    LyDoTuChoi = c.LyDoTuChoi ?? "", IdTacNghiepCuoi = c.IdtacNghiepCuoi
                                }).Where(c => (isDelete == false && (c.FlagDel == null || c.FlagDel == isDelete)) || (isDelete == true && c.FlagDel == isDelete))
                                .OrderByDescending(c => c.Id).Skip(Start).Take(Size).Where(c => 
                                    (c.QuocGia != null && c.QuocGia.Contains(Search)) || (c.ThanhPho != null && c.ThanhPho.Contains(Search)) || (c.NameVI != null && c.NameVI.Contains(Search)) || (c.NameEN != null && c.NameEN.Contains(Search)) ||
                                    (c.Code != null && c.Code.Contains(Search)) || (c.TaxCode != null && c.TaxCode.Contains(Search)) || (c.Email != null && c.Email.Contains(Search)) || (c.Website != null && c.Website.Contains(Search)) ||
                                    (c.Note != null && c.Note.Contains(Search)) || (c.NguoiTao != null && c.NguoiTao.Contains(Search)) || (c.LoaiDoanhNghiep != null && c.LoaiDoanhNghiep.Contains(Search)) || (c.NguoiXoa != null && c.NguoiXoa.Contains(Search)) ||
                                    (c.LyDoXoa != null && c.LyDoXoa.Contains(Search)) || (c.NguoiGiaoViec != null && c.NguoiGiaoViec.Contains(Search)) || (c.NguoiTraKhach != null && c.NguoiTraKhach.Contains(Search)) || (c.ListTacNghiepText != null && c.ListTacNghiepText.Contains(Search)) ||
                                    (c.ListTuyenHangText != null && c.ListTuyenHangText.Contains(Search)) || (c.ListPhanHoiText != null && c.ListPhanHoiText.Contains(Search)) || (c.ThongTinGiaoViec != null && c.ThongTinGiaoViec.Contains(Search)) || (c.LyDoTuChoi != null && c.LyDoTuChoi.Contains(Search))
                                ).ToListAsync();
            }

            return data;
        }

        public async Task<int> GetTotalCustomers(string Search = "", bool isDelete = false)
        {
            var total = 0;

            if(Search == "")
            {
                total = await _db.TblDmcustomers.Select(c => new CustomerDto()
                                 {
                                     Id = c.Id, IdQuocGia = c.IdquocGia, IdCity = c.Idcity, NameVI = c.NameVi ?? "", NameEN = c.NameEn ?? "", Code = c.Code ?? "", AddressVI = c.AddressVi ?? "", AddressEN = c.AddressEn ?? "", TaxCode = c.TaxCode ?? "",
                                     QuocGia = _db.TblDmcountries.Where(x => x.Id == c.IdquocGia).Select(x => x.NameVi).FirstOrDefault() ?? "", ThanhPho = _db.TblDmcities.Where(x => x.Id == c.Idcity).Select(x => x.NameVi).FirstOrDefault() ?? "",
                                     Phone = c.Phone ?? "", Fax = c.Fax ?? "", Email = c.Email ?? "", Website = c.Website ?? "", Note = c.Note ?? "", IdNhanVienSale = c.IdnhanVienSale, IdUserCreate = c.IduserCreate, FlagActive = c.FlagActive, FlagDel = c.FlagDel,
                                     NguoiTao = c.IduserCreate == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserCreate).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     EnumGiaoNhan = c.EnumGiaoNhan != null ? c.EnumGiaoNhan : 0, NhanVien = _db.TblNhanSus.Where(x => x.Id == c.IdnhanVienSale).Select(x => x.HoTenVi).FirstOrDefault() ?? "",
                                     DateCreate = c.DateCreate != null ? string.Format("{0:yyyy-MM-dd}", c.DateCreate) : "", EnumLoaiKhachHang = c.EnumLoaiKhachHang, IdLoaiDoanhNghiep = c.IdloaiDoanhNghiep,
                                     LoaiDoanhNghiep = _db.TblDmloaiDoanhNghieps.Where(x => x.Id == c.IdloaiDoanhNghiep).Select(x => x.NameVi).FirstOrDefault() ?? "", IdUserDelete = c.IduserDelete,
                                     NguoiXoa = c.IduserDelete == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserDelete).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     DateDelete = c.DateDelete != null ? string.Format("{0:yyyy-MM-dd}", c.DateDelete) : "", LyDoXoa = c.LyDoXoa ?? "", NgayTuongTac = c.NgayTuongTac != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuongTac) : "",
                                     NgayChonKhach = c.NgayChonKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChonKhach) : "", NgayTraVe = c.NgayTraVe != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTraVe) : "",
                                     NgayChotKhach = c.NgayChotKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChotKhach) : "", NgayTacNghiep = c.NgayTacNghiep != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTacNghiep) : "",
                                     SttMaxTacNghiep = c.SttmaxTacNghiep, NgayGiao = c.NgayGiao != null ? string.Format("{0:yyyy-MM-dd}", c.NgayGiao) : "", NgayNhan = c.NgayNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayNhan) : "",
                                     IdUserGiaoViec = c.IduserGiaoViec, IdUserTraKhach = c.IduserTraKhach, NgayKetThucNhan = c.NgayKetThucNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayKetThucNhan) : "", ThongTinGiaoViec = c.ThongTinGiaoViec ?? "",
                                     NguoiGiaoViec = c.IduserGiaoViec == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserGiaoViec).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     NguoiTraKhach = c.IduserTraKhach == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserTraKhach).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     ListTacNghiepText = c.ListTacNghiepText ?? "", ListTuyenHangText = c.ListTuyenHangText ?? "", ListPhanHoiText = c.ListPhanHoiText ?? "", NgayTuTraKhach = c.NgayTuTraKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuTraKhach) : "",
                                     LyDoTuChoi = c.LyDoTuChoi ?? "", IdTacNghiepCuoi = c.IdtacNghiepCuoi
                                 }).Where(c => (isDelete == false && (c.FlagDel == null || c.FlagDel == isDelete)) || (isDelete == true && c.FlagDel == isDelete))
                                 .CountAsync();
            } else
            {
                total = await _db.TblDmcustomers.Select(c => new CustomerDto()
                                 {
                                     Id = c.Id, IdQuocGia = c.IdquocGia, IdCity = c.Idcity, NameVI = c.NameVi ?? "", NameEN = c.NameEn ?? "", Code = c.Code ?? "", AddressVI = c.AddressVi ?? "", AddressEN = c.AddressEn ?? "", TaxCode = c.TaxCode ?? "",
                                     QuocGia = _db.TblDmcountries.Where(x => x.Id == c.IdquocGia).Select(x => x.NameVi).FirstOrDefault() ?? "", ThanhPho = _db.TblDmcities.Where(x => x.Id == c.Idcity).Select(x => x.NameVi).FirstOrDefault() ?? "",
                                     Phone = c.Phone ?? "", Fax = c.Fax ?? "", Email = c.Email ?? "", Website = c.Website ?? "", Note = c.Note ?? "", IdNhanVienSale = c.IdnhanVienSale, IdUserCreate = c.IduserCreate, FlagActive = c.FlagActive, FlagDel = c.FlagDel,
                                     NguoiTao = c.IduserCreate == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserCreate).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     EnumGiaoNhan = c.EnumGiaoNhan != null ? c.EnumGiaoNhan : 0, NhanVien = _db.TblNhanSus.Where(x => x.Id == c.IdnhanVienSale).Select(x => x.HoTenVi).FirstOrDefault() ?? "",
                                     DateCreate = c.DateCreate != null ? string.Format("{0:yyyy-MM-dd}", c.DateCreate) : "", EnumLoaiKhachHang = c.EnumLoaiKhachHang, IdLoaiDoanhNghiep = c.IdloaiDoanhNghiep,
                                     LoaiDoanhNghiep = _db.TblDmloaiDoanhNghieps.Where(x => x.Id == c.IdloaiDoanhNghiep).Select(x => x.NameVi).FirstOrDefault() ?? "", IdUserDelete = c.IduserDelete,
                                     NguoiXoa = c.IduserDelete == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserDelete).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     DateDelete = c.DateDelete != null ? string.Format("{0:yyyy-MM-dd}", c.DateDelete) : "", LyDoXoa = c.LyDoXoa ?? "", NgayTuongTac = c.NgayTuongTac != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuongTac) : "",
                                     NgayChonKhach = c.NgayChonKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChonKhach) : "", NgayTraVe = c.NgayTraVe != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTraVe) : "",
                                     NgayChotKhach = c.NgayChotKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChotKhach) : "", NgayTacNghiep = c.NgayTacNghiep != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTacNghiep) : "",
                                     SttMaxTacNghiep = c.SttmaxTacNghiep, NgayGiao = c.NgayGiao != null ? string.Format("{0:yyyy-MM-dd}", c.NgayGiao) : "", NgayNhan = c.NgayNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayNhan) : "",
                                     IdUserGiaoViec = c.IduserGiaoViec, IdUserTraKhach = c.IduserTraKhach, NgayKetThucNhan = c.NgayKetThucNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayKetThucNhan) : "", ThongTinGiaoViec = c.ThongTinGiaoViec ?? "",
                                     NguoiGiaoViec = c.IduserGiaoViec == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserGiaoViec).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     NguoiTraKhach = c.IduserTraKhach == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserTraKhach).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     ListTacNghiepText = c.ListTacNghiepText ?? "", ListTuyenHangText = c.ListTuyenHangText ?? "", ListPhanHoiText = c.ListPhanHoiText ?? "", NgayTuTraKhach = c.NgayTuTraKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuTraKhach) : "",
                                     LyDoTuChoi = c.LyDoTuChoi ?? "", IdTacNghiepCuoi = c.IdtacNghiepCuoi
                                 }).Where(c => (isDelete == false && (c.FlagDel == null || c.FlagDel == isDelete)) || (isDelete == true && c.FlagDel == isDelete))
                                 .Where(c => 
                                     (c.QuocGia != null && c.QuocGia.Contains(Search)) || (c.ThanhPho != null && c.ThanhPho.Contains(Search)) || (c.NameVI != null && c.NameVI.Contains(Search)) || (c.NameEN != null && c.NameEN.Contains(Search)) ||
                                     (c.Code != null && c.Code.Contains(Search)) || (c.TaxCode != null && c.TaxCode.Contains(Search)) || (c.Email != null && c.Email.Contains(Search)) || (c.Website != null && c.Website.Contains(Search)) ||
                                     (c.Note != null && c.Note.Contains(Search)) || (c.NguoiTao != null && c.NguoiTao.Contains(Search)) || (c.LoaiDoanhNghiep != null && c.LoaiDoanhNghiep.Contains(Search)) || (c.NguoiXoa != null && c.NguoiXoa.Contains(Search)) ||
                                     (c.LyDoXoa != null && c.LyDoXoa.Contains(Search)) || (c.NguoiGiaoViec != null && c.NguoiGiaoViec.Contains(Search)) || (c.NguoiTraKhach != null && c.NguoiTraKhach.Contains(Search)) || (c.ListTacNghiepText != null && c.ListTacNghiepText.Contains(Search)) ||
                                     (c.ListTuyenHangText != null && c.ListTuyenHangText.Contains(Search)) || (c.ListPhanHoiText != null && c.ListPhanHoiText.Contains(Search)) || (c.ThongTinGiaoViec != null && c.ThongTinGiaoViec.Contains(Search)) || (c.LyDoTuChoi != null && c.LyDoTuChoi.Contains(Search))
                                 ).CountAsync();
            }

            return total;
        }

        // Danh sách khách hàng dùng cho việc export dữ liệu
        public async Task<List<TblDmcustomer>?> GetCustomersData(int pageNumber = 0, int pageSize = 10)
        {
            List<TblDmcustomer>? data = await _db.TblDmcustomers.OrderBy(x => x.Id).Skip(pageNumber * pageSize).Take(pageSize).ToListAsync();
            return data;
        }

        // Danh sách đã giao bởi admin hoặc người có quyền giao
        public async Task<List<CustomerDto>?> GetCustomersAssigned(int Start = 0, int Size = 10, string Search = "", string permission = "", long idUser = 0)
        {
            var isAdmin = permission.Contains("1048576") || permission.Contains("7000");
            List<CustomerDto>? data;
            
            if(isAdmin && Search == "")
            {
                data = await _db.TblDmcustomers.Select(c => new CustomerDto()
                                {
                                    Id = c.Id, IdQuocGia = c.IdquocGia, IdCity = c.Idcity, NameVI = c.NameVi ?? "", NameEN = c.NameEn ?? "", Code = c.Code ?? "", AddressVI = c.AddressVi ?? "", AddressEN = c.AddressEn ?? "", TaxCode = c.TaxCode ?? "",
                                    QuocGia = _db.TblDmcountries.Where(x => x.Id == c.IdquocGia).Select(x => x.NameVi).FirstOrDefault() ?? "", ThanhPho = _db.TblDmcities.Where(x => x.Id == c.Idcity).Select(x => x.NameVi).FirstOrDefault() ?? "",
                                    Phone = c.Phone ?? "", Fax = c.Fax ?? "", Email = c.Email ?? "", Website = c.Website ?? "", Note = c.Note ?? "", IdNhanVienSale = c.IdnhanVienSale, IdUserCreate = c.IduserCreate, FlagActive = c.FlagActive, FlagDel = c.FlagDel,
                                    NguoiTao = c.IduserCreate == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserCreate).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    EnumGiaoNhan = c.EnumGiaoNhan != null ? c.EnumGiaoNhan : 0, NhanVien = _db.TblNhanSus.Where(x => x.Id == c.IdnhanVienSale).Select(x => x.HoTenVi).FirstOrDefault() ?? "",
                                    DateCreate = c.DateCreate != null ? string.Format("{0:yyyy-MM-dd}", c.DateCreate) : "", EnumLoaiKhachHang = c.EnumLoaiKhachHang, IdLoaiDoanhNghiep = c.IdloaiDoanhNghiep,
                                    LoaiDoanhNghiep = _db.TblDmloaiDoanhNghieps.Where(x => x.Id == c.IdloaiDoanhNghiep).Select(x => x.NameVi).FirstOrDefault() ?? "", IdUserDelete = c.IduserDelete,
                                    NguoiXoa = c.IduserDelete == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserDelete).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    DateDelete = c.DateDelete != null ? string.Format("{0:yyyy-MM-dd}", c.DateDelete) : "", LyDoXoa = c.LyDoXoa ?? "", NgayTuongTac = c.NgayTuongTac != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuongTac) : "",
                                    NgayChonKhach = c.NgayChonKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChonKhach) : "", NgayTraVe = c.NgayTraVe != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTraVe) : "",
                                    NgayChotKhach = c.NgayChotKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChotKhach) : "", NgayTacNghiep = c.NgayTacNghiep != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTacNghiep) : "",
                                    SttMaxTacNghiep = c.SttmaxTacNghiep, NgayGiao = c.NgayGiao != null ? string.Format("{0:yyyy-MM-dd}", c.NgayGiao) : "", NgayNhan = c.NgayNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayNhan) : "",
                                    IdUserGiaoViec = c.IduserGiaoViec, IdUserTraKhach = c.IduserTraKhach, NgayKetThucNhan = c.NgayKetThucNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayKetThucNhan) : "", ThongTinGiaoViec = c.ThongTinGiaoViec ?? "",
                                    NguoiGiaoViec = c.IduserGiaoViec == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserGiaoViec).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    NguoiTraKhach = c.IduserTraKhach == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserTraKhach).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    ListTacNghiepText = c.ListTacNghiepText ?? "", ListTuyenHangText = c.ListTuyenHangText ?? "", ListPhanHoiText = c.ListPhanHoiText ?? "", NgayTuTraKhach = c.NgayTuTraKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuTraKhach) : "",
                                    LyDoTuChoi = c.LyDoTuChoi ?? "", IdTacNghiepCuoi = c.IdtacNghiepCuoi
                                }).Where(c => c.FlagActive == true)
                                .Where(c => c.EnumGiaoNhan != null && c.EnumGiaoNhan == 1)
                                .OrderByDescending(c => c.Id).Skip(Start).Take(Size).ToListAsync();
            } else if(isAdmin && Search != "")
            {
                data = await _db.TblDmcustomers.Select(c => new CustomerDto()
                                {
                                    Id = c.Id, IdQuocGia = c.IdquocGia, IdCity = c.Idcity, NameVI = c.NameVi ?? "", NameEN = c.NameEn ?? "", Code = c.Code ?? "", AddressVI = c.AddressVi ?? "", AddressEN = c.AddressEn ?? "", TaxCode = c.TaxCode ?? "",
                                    QuocGia = _db.TblDmcountries.Where(x => x.Id == c.IdquocGia).Select(x => x.NameVi).FirstOrDefault() ?? "", ThanhPho = _db.TblDmcities.Where(x => x.Id == c.Idcity).Select(x => x.NameVi).FirstOrDefault() ?? "",
                                    Phone = c.Phone ?? "", Fax = c.Fax ?? "", Email = c.Email ?? "", Website = c.Website ?? "", Note = c.Note ?? "", IdNhanVienSale = c.IdnhanVienSale, IdUserCreate = c.IduserCreate, FlagActive = c.FlagActive, FlagDel = c.FlagDel,
                                    NguoiTao = c.IduserCreate == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserCreate).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    EnumGiaoNhan = c.EnumGiaoNhan != null ? c.EnumGiaoNhan : 0, NhanVien = _db.TblNhanSus.Where(x => x.Id == c.IdnhanVienSale).Select(x => x.HoTenVi).FirstOrDefault() ?? "",
                                    DateCreate = c.DateCreate != null ? string.Format("{0:yyyy-MM-dd}", c.DateCreate) : "", EnumLoaiKhachHang = c.EnumLoaiKhachHang, IdLoaiDoanhNghiep = c.IdloaiDoanhNghiep,
                                    LoaiDoanhNghiep = _db.TblDmloaiDoanhNghieps.Where(x => x.Id == c.IdloaiDoanhNghiep).Select(x => x.NameVi).FirstOrDefault() ?? "", IdUserDelete = c.IduserDelete,
                                    NguoiXoa = c.IduserDelete == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserDelete).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    DateDelete = c.DateDelete != null ? string.Format("{0:yyyy-MM-dd}", c.DateDelete) : "", LyDoXoa = c.LyDoXoa ?? "", NgayTuongTac = c.NgayTuongTac != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuongTac) : "",
                                    NgayChonKhach = c.NgayChonKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChonKhach) : "", NgayTraVe = c.NgayTraVe != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTraVe) : "",
                                    NgayChotKhach = c.NgayChotKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChotKhach) : "", NgayTacNghiep = c.NgayTacNghiep != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTacNghiep) : "",
                                    SttMaxTacNghiep = c.SttmaxTacNghiep, NgayGiao = c.NgayGiao != null ? string.Format("{0:yyyy-MM-dd}", c.NgayGiao) : "", NgayNhan = c.NgayNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayNhan) : "",
                                    IdUserGiaoViec = c.IduserGiaoViec, IdUserTraKhach = c.IduserTraKhach, NgayKetThucNhan = c.NgayKetThucNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayKetThucNhan) : "", ThongTinGiaoViec = c.ThongTinGiaoViec ?? "",
                                    NguoiGiaoViec = c.IduserGiaoViec == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserGiaoViec).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    NguoiTraKhach = c.IduserTraKhach == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserTraKhach).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    ListTacNghiepText = c.ListTacNghiepText ?? "", ListTuyenHangText = c.ListTuyenHangText ?? "", ListPhanHoiText = c.ListPhanHoiText ?? "", NgayTuTraKhach = c.NgayTuTraKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuTraKhach) : "",
                                    LyDoTuChoi = c.LyDoTuChoi ?? "", IdTacNghiepCuoi = c.IdtacNghiepCuoi
                                }).Where(c => c.FlagActive == true)
                                .Where(c => c.EnumGiaoNhan != null && c.EnumGiaoNhan == 1)
                                .Where(c => 
                                    (c.QuocGia != null && c.QuocGia.Contains(Search)) || (c.ThanhPho != null && c.ThanhPho.Contains(Search)) || (c.NameVI != null && c.NameVI.Contains(Search)) || (c.NameEN != null && c.NameEN.Contains(Search)) ||
                                    (c.Code != null && c.Code.Contains(Search)) || (c.TaxCode != null && c.TaxCode.Contains(Search)) || (c.Email != null && c.Email.Contains(Search)) || (c.Website != null && c.Website.Contains(Search)) ||
                                    (c.Note != null && c.Note.Contains(Search)) || (c.NguoiTao != null && c.NguoiTao.Contains(Search)) || (c.LoaiDoanhNghiep != null && c.LoaiDoanhNghiep.Contains(Search)) || (c.NguoiXoa != null && c.NguoiXoa.Contains(Search)) ||
                                    (c.LyDoXoa != null && c.LyDoXoa.Contains(Search)) || (c.NguoiGiaoViec != null && c.NguoiGiaoViec.Contains(Search)) || (c.NguoiTraKhach != null && c.NguoiTraKhach.Contains(Search)) || (c.ListTacNghiepText != null && c.ListTacNghiepText.Contains(Search)) ||
                                    (c.ListTuyenHangText != null && c.ListTuyenHangText.Contains(Search)) || (c.ListPhanHoiText != null && c.ListPhanHoiText.Contains(Search)) || (c.ThongTinGiaoViec != null && c.ThongTinGiaoViec.Contains(Search)) || (c.LyDoTuChoi != null && c.LyDoTuChoi.Contains(Search))
                                )
                                .OrderByDescending(c => c.Id).Skip(Start).Take(Size).ToListAsync();
            } else if (!isAdmin && Search == "")
            {
                data = await _db.TblDmcustomers.Select(c => new CustomerDto()
                                {
                                    Id = c.Id, IdQuocGia = c.IdquocGia, IdCity = c.Idcity, NameVI = c.NameVi ?? "", NameEN = c.NameEn ?? "", Code = c.Code ?? "", AddressVI = c.AddressVi ?? "", AddressEN = c.AddressEn ?? "", TaxCode = c.TaxCode ?? "",
                                    QuocGia = _db.TblDmcountries.Where(x => x.Id == c.IdquocGia).Select(x => x.NameVi).FirstOrDefault() ?? "", ThanhPho = _db.TblDmcities.Where(x => x.Id == c.Idcity).Select(x => x.NameVi).FirstOrDefault() ?? "",
                                    Phone = c.Phone ?? "", Fax = c.Fax ?? "", Email = c.Email ?? "", Website = c.Website ?? "", Note = c.Note ?? "", IdNhanVienSale = c.IdnhanVienSale, IdUserCreate = c.IduserCreate, FlagActive = c.FlagActive, FlagDel = c.FlagDel,
                                    NguoiTao = c.IduserCreate == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserCreate).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    EnumGiaoNhan = c.EnumGiaoNhan != null ? c.EnumGiaoNhan : 0, NhanVien = _db.TblNhanSus.Where(x => x.Id == c.IdnhanVienSale).Select(x => x.HoTenVi).FirstOrDefault() ?? "",
                                    DateCreate = c.DateCreate != null ? string.Format("{0:yyyy-MM-dd}", c.DateCreate) : "", EnumLoaiKhachHang = c.EnumLoaiKhachHang, IdLoaiDoanhNghiep = c.IdloaiDoanhNghiep,
                                    LoaiDoanhNghiep = _db.TblDmloaiDoanhNghieps.Where(x => x.Id == c.IdloaiDoanhNghiep).Select(x => x.NameVi).FirstOrDefault() ?? "", IdUserDelete = c.IduserDelete,
                                    NguoiXoa = c.IduserDelete == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserDelete).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    DateDelete = c.DateDelete != null ? string.Format("{0:yyyy-MM-dd}", c.DateDelete) : "", LyDoXoa = c.LyDoXoa ?? "", NgayTuongTac = c.NgayTuongTac != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuongTac) : "",
                                    NgayChonKhach = c.NgayChonKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChonKhach) : "", NgayTraVe = c.NgayTraVe != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTraVe) : "",
                                    NgayChotKhach = c.NgayChotKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChotKhach) : "", NgayTacNghiep = c.NgayTacNghiep != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTacNghiep) : "",
                                    SttMaxTacNghiep = c.SttmaxTacNghiep, NgayGiao = c.NgayGiao != null ? string.Format("{0:yyyy-MM-dd}", c.NgayGiao) : "", NgayNhan = c.NgayNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayNhan) : "",
                                    IdUserGiaoViec = c.IduserGiaoViec, IdUserTraKhach = c.IduserTraKhach, NgayKetThucNhan = c.NgayKetThucNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayKetThucNhan) : "", ThongTinGiaoViec = c.ThongTinGiaoViec ?? "",
                                    NguoiGiaoViec = c.IduserGiaoViec == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserGiaoViec).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    NguoiTraKhach = c.IduserTraKhach == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserTraKhach).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    ListTacNghiepText = c.ListTacNghiepText ?? "", ListTuyenHangText = c.ListTuyenHangText ?? "", ListPhanHoiText = c.ListPhanHoiText ?? "", NgayTuTraKhach = c.NgayTuTraKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuTraKhach) : "",
                                    LyDoTuChoi = c.LyDoTuChoi ?? "", IdTacNghiepCuoi = c.IdtacNghiepCuoi
                                }).Where(c => c.FlagActive == true)
                                .Where(c => c.EnumGiaoNhan != null && c.EnumGiaoNhan == 1 && c.IdUserGiaoViec == idUser)
                                .OrderByDescending(c => c.Id).Skip(Start).Take(Size).ToListAsync();
            } else
            {
                data = await _db.TblDmcustomers.Select(c => new CustomerDto()
                                {
                                    Id = c.Id, IdQuocGia = c.IdquocGia, IdCity = c.Idcity, NameVI = c.NameVi ?? "", NameEN = c.NameEn ?? "", Code = c.Code ?? "", AddressVI = c.AddressVi ?? "", AddressEN = c.AddressEn ?? "", TaxCode = c.TaxCode ?? "",
                                    QuocGia = _db.TblDmcountries.Where(x => x.Id == c.IdquocGia).Select(x => x.NameVi).FirstOrDefault() ?? "", ThanhPho = _db.TblDmcities.Where(x => x.Id == c.Idcity).Select(x => x.NameVi).FirstOrDefault() ?? "",
                                    Phone = c.Phone ?? "", Fax = c.Fax ?? "", Email = c.Email ?? "", Website = c.Website ?? "", Note = c.Note ?? "", IdNhanVienSale = c.IdnhanVienSale, IdUserCreate = c.IduserCreate, FlagActive = c.FlagActive, FlagDel = c.FlagDel,
                                    NguoiTao = c.IduserCreate == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserCreate).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    EnumGiaoNhan = c.EnumGiaoNhan != null ? c.EnumGiaoNhan : 0, NhanVien = _db.TblNhanSus.Where(x => x.Id == c.IdnhanVienSale).Select(x => x.HoTenVi).FirstOrDefault() ?? "",
                                    DateCreate = c.DateCreate != null ? string.Format("{0:yyyy-MM-dd}", c.DateCreate) : "", EnumLoaiKhachHang = c.EnumLoaiKhachHang, IdLoaiDoanhNghiep = c.IdloaiDoanhNghiep,
                                    LoaiDoanhNghiep = _db.TblDmloaiDoanhNghieps.Where(x => x.Id == c.IdloaiDoanhNghiep).Select(x => x.NameVi).FirstOrDefault() ?? "", IdUserDelete = c.IduserDelete,
                                    NguoiXoa = c.IduserDelete == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserDelete).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    DateDelete = c.DateDelete != null ? string.Format("{0:yyyy-MM-dd}", c.DateDelete) : "", LyDoXoa = c.LyDoXoa ?? "", NgayTuongTac = c.NgayTuongTac != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuongTac) : "",
                                    NgayChonKhach = c.NgayChonKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChonKhach) : "", NgayTraVe = c.NgayTraVe != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTraVe) : "",
                                    NgayChotKhach = c.NgayChotKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChotKhach) : "", NgayTacNghiep = c.NgayTacNghiep != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTacNghiep) : "",
                                    SttMaxTacNghiep = c.SttmaxTacNghiep, NgayGiao = c.NgayGiao != null ? string.Format("{0:yyyy-MM-dd}", c.NgayGiao) : "", NgayNhan = c.NgayNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayNhan) : "",
                                    IdUserGiaoViec = c.IduserGiaoViec, IdUserTraKhach = c.IduserTraKhach, NgayKetThucNhan = c.NgayKetThucNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayKetThucNhan) : "", ThongTinGiaoViec = c.ThongTinGiaoViec ?? "",
                                    NguoiGiaoViec = c.IduserGiaoViec == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserGiaoViec).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    NguoiTraKhach = c.IduserTraKhach == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserTraKhach).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    ListTacNghiepText = c.ListTacNghiepText ?? "", ListTuyenHangText = c.ListTuyenHangText ?? "", ListPhanHoiText = c.ListPhanHoiText ?? "", NgayTuTraKhach = c.NgayTuTraKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuTraKhach) : "",
                                    LyDoTuChoi = c.LyDoTuChoi ?? "", IdTacNghiepCuoi = c.IdtacNghiepCuoi
                                }).Where(c => c.FlagActive == true)
                                .Where(c => c.EnumGiaoNhan != null && c.EnumGiaoNhan == 1 && c.IdUserGiaoViec == idUser)
                                .Where(c => 
                                    (c.QuocGia != null && c.QuocGia.Contains(Search)) || (c.ThanhPho != null && c.ThanhPho.Contains(Search)) || (c.NameVI != null && c.NameVI.Contains(Search)) || (c.NameEN != null && c.NameEN.Contains(Search)) ||
                                    (c.Code != null && c.Code.Contains(Search)) || (c.TaxCode != null && c.TaxCode.Contains(Search)) || (c.Email != null && c.Email.Contains(Search)) || (c.Website != null && c.Website.Contains(Search)) ||
                                    (c.Note != null && c.Note.Contains(Search)) || (c.NguoiTao != null && c.NguoiTao.Contains(Search)) || (c.LoaiDoanhNghiep != null && c.LoaiDoanhNghiep.Contains(Search)) || (c.NguoiXoa != null && c.NguoiXoa.Contains(Search)) ||
                                    (c.LyDoXoa != null && c.LyDoXoa.Contains(Search)) || (c.NguoiGiaoViec != null && c.NguoiGiaoViec.Contains(Search)) || (c.NguoiTraKhach != null && c.NguoiTraKhach.Contains(Search)) || (c.ListTacNghiepText != null && c.ListTacNghiepText.Contains(Search)) ||
                                    (c.ListTuyenHangText != null && c.ListTuyenHangText.Contains(Search)) || (c.ListPhanHoiText != null && c.ListPhanHoiText.Contains(Search)) || (c.ThongTinGiaoViec != null && c.ThongTinGiaoViec.Contains(Search)) || (c.LyDoTuChoi != null && c.LyDoTuChoi.Contains(Search))
                                )
                                .OrderByDescending(c => c.Id).Skip(Start).Take(Size).ToListAsync();
            }

            return data;
        }

        public async Task<int> GetTotalCustomersAssigned(string Search = "", string permission = "", long idUser = 0)
        {
            var isAdmin = permission.Contains("1048576") || permission.Contains("7000");
            var total = 0;

            if(isAdmin && Search == "")
            {
                total = await _db.TblDmcustomers.Select(c => new CustomerDto()
                                 {
                                     Id = c.Id, IdQuocGia = c.IdquocGia, IdCity = c.Idcity, NameVI = c.NameVi ?? "", NameEN = c.NameEn ?? "", Code = c.Code ?? "", AddressVI = c.AddressVi ?? "", AddressEN = c.AddressEn ?? "", TaxCode = c.TaxCode ?? "",
                                     QuocGia = _db.TblDmcountries.Where(x => x.Id == c.IdquocGia).Select(x => x.NameVi).FirstOrDefault() ?? "", ThanhPho = _db.TblDmcities.Where(x => x.Id == c.Idcity).Select(x => x.NameVi).FirstOrDefault() ?? "",
                                     Phone = c.Phone ?? "", Fax = c.Fax ?? "", Email = c.Email ?? "", Website = c.Website ?? "", Note = c.Note ?? "", IdNhanVienSale = c.IdnhanVienSale, IdUserCreate = c.IduserCreate, FlagActive = c.FlagActive, FlagDel = c.FlagDel,
                                     NguoiTao = c.IduserCreate == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserCreate).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     EnumGiaoNhan = c.EnumGiaoNhan != null ? c.EnumGiaoNhan : 0, NhanVien = _db.TblNhanSus.Where(x => x.Id == c.IdnhanVienSale).Select(x => x.HoTenVi).FirstOrDefault() ?? "",
                                     DateCreate = c.DateCreate != null ? string.Format("{0:yyyy-MM-dd}", c.DateCreate) : "", EnumLoaiKhachHang = c.EnumLoaiKhachHang, IdLoaiDoanhNghiep = c.IdloaiDoanhNghiep,
                                     LoaiDoanhNghiep = _db.TblDmloaiDoanhNghieps.Where(x => x.Id == c.IdloaiDoanhNghiep).Select(x => x.NameVi).FirstOrDefault() ?? "", IdUserDelete = c.IduserDelete,
                                     NguoiXoa = c.IduserDelete == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserDelete).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     DateDelete = c.DateDelete != null ? string.Format("{0:yyyy-MM-dd}", c.DateDelete) : "", LyDoXoa = c.LyDoXoa ?? "", NgayTuongTac = c.NgayTuongTac != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuongTac) : "",
                                     NgayChonKhach = c.NgayChonKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChonKhach) : "", NgayTraVe = c.NgayTraVe != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTraVe) : "",
                                     NgayChotKhach = c.NgayChotKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChotKhach) : "", NgayTacNghiep = c.NgayTacNghiep != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTacNghiep) : "",
                                     SttMaxTacNghiep = c.SttmaxTacNghiep, NgayGiao = c.NgayGiao != null ? string.Format("{0:yyyy-MM-dd}", c.NgayGiao) : "", NgayNhan = c.NgayNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayNhan) : "",
                                     IdUserGiaoViec = c.IduserGiaoViec, IdUserTraKhach = c.IduserTraKhach, NgayKetThucNhan = c.NgayKetThucNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayKetThucNhan) : "", ThongTinGiaoViec = c.ThongTinGiaoViec ?? "",
                                     NguoiGiaoViec = c.IduserGiaoViec == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserGiaoViec).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     NguoiTraKhach = c.IduserTraKhach == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserTraKhach).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     ListTacNghiepText = c.ListTacNghiepText ?? "", ListTuyenHangText = c.ListTuyenHangText ?? "", ListPhanHoiText = c.ListPhanHoiText ?? "", NgayTuTraKhach = c.NgayTuTraKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuTraKhach) : "",
                                     LyDoTuChoi = c.LyDoTuChoi ?? "", IdTacNghiepCuoi = c.IdtacNghiepCuoi
                                 }).Where(c => c.FlagActive == true)
                                 .Where(c => c.EnumGiaoNhan != null && c.EnumGiaoNhan == 1)
                                 .CountAsync();
            } else if(isAdmin && Search != "")
            {
                total = await _db.TblDmcustomers.Select(c => new CustomerDto()
                                 {
                                     Id = c.Id, IdQuocGia = c.IdquocGia, IdCity = c.Idcity, NameVI = c.NameVi ?? "", NameEN = c.NameEn ?? "", Code = c.Code ?? "", AddressVI = c.AddressVi ?? "", AddressEN = c.AddressEn ?? "", TaxCode = c.TaxCode ?? "",
                                     QuocGia = _db.TblDmcountries.Where(x => x.Id == c.IdquocGia).Select(x => x.NameVi).FirstOrDefault() ?? "", ThanhPho = _db.TblDmcities.Where(x => x.Id == c.Idcity).Select(x => x.NameVi).FirstOrDefault() ?? "",
                                     Phone = c.Phone ?? "", Fax = c.Fax ?? "", Email = c.Email ?? "", Website = c.Website ?? "", Note = c.Note ?? "", IdNhanVienSale = c.IdnhanVienSale, IdUserCreate = c.IduserCreate, FlagActive = c.FlagActive, FlagDel = c.FlagDel,
                                     NguoiTao = c.IduserCreate == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserCreate).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     EnumGiaoNhan = c.EnumGiaoNhan != null ? c.EnumGiaoNhan : 0, NhanVien = _db.TblNhanSus.Where(x => x.Id == c.IdnhanVienSale).Select(x => x.HoTenVi).FirstOrDefault() ?? "",
                                     DateCreate = c.DateCreate != null ? string.Format("{0:yyyy-MM-dd}", c.DateCreate) : "", EnumLoaiKhachHang = c.EnumLoaiKhachHang, IdLoaiDoanhNghiep = c.IdloaiDoanhNghiep,
                                     LoaiDoanhNghiep = _db.TblDmloaiDoanhNghieps.Where(x => x.Id == c.IdloaiDoanhNghiep).Select(x => x.NameVi).FirstOrDefault() ?? "", IdUserDelete = c.IduserDelete,
                                     NguoiXoa = c.IduserDelete == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserDelete).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     DateDelete = c.DateDelete != null ? string.Format("{0:yyyy-MM-dd}", c.DateDelete) : "", LyDoXoa = c.LyDoXoa ?? "", NgayTuongTac = c.NgayTuongTac != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuongTac) : "",
                                     NgayChonKhach = c.NgayChonKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChonKhach) : "", NgayTraVe = c.NgayTraVe != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTraVe) : "",
                                     NgayChotKhach = c.NgayChotKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChotKhach) : "", NgayTacNghiep = c.NgayTacNghiep != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTacNghiep) : "",
                                     SttMaxTacNghiep = c.SttmaxTacNghiep, NgayGiao = c.NgayGiao != null ? string.Format("{0:yyyy-MM-dd}", c.NgayGiao) : "", NgayNhan = c.NgayNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayNhan) : "",
                                     IdUserGiaoViec = c.IduserGiaoViec, IdUserTraKhach = c.IduserTraKhach, NgayKetThucNhan = c.NgayKetThucNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayKetThucNhan) : "", ThongTinGiaoViec = c.ThongTinGiaoViec ?? "",
                                     NguoiGiaoViec = c.IduserGiaoViec == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserGiaoViec).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     NguoiTraKhach = c.IduserTraKhach == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserTraKhach).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     ListTacNghiepText = c.ListTacNghiepText ?? "", ListTuyenHangText = c.ListTuyenHangText ?? "", ListPhanHoiText = c.ListPhanHoiText ?? "", NgayTuTraKhach = c.NgayTuTraKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuTraKhach) : "",
                                     LyDoTuChoi = c.LyDoTuChoi ?? "", IdTacNghiepCuoi = c.IdtacNghiepCuoi
                                 }).Where(c => c.FlagActive == true)
                                 .Where(c => c.EnumGiaoNhan != null && c.EnumGiaoNhan == 1)
                                 .Where(c => 
                                     (c.QuocGia != null && c.QuocGia.Contains(Search)) || (c.ThanhPho != null && c.ThanhPho.Contains(Search)) || (c.NameVI != null && c.NameVI.Contains(Search)) || (c.NameEN != null && c.NameEN.Contains(Search)) ||
                                     (c.Code != null && c.Code.Contains(Search)) || (c.TaxCode != null && c.TaxCode.Contains(Search)) || (c.Email != null && c.Email.Contains(Search)) || (c.Website != null && c.Website.Contains(Search)) ||
                                     (c.Note != null && c.Note.Contains(Search)) || (c.NguoiTao != null && c.NguoiTao.Contains(Search)) || (c.LoaiDoanhNghiep != null && c.LoaiDoanhNghiep.Contains(Search)) || (c.NguoiXoa != null && c.NguoiXoa.Contains(Search)) ||
                                     (c.LyDoXoa != null && c.LyDoXoa.Contains(Search)) || (c.NguoiGiaoViec != null && c.NguoiGiaoViec.Contains(Search)) || (c.NguoiTraKhach != null && c.NguoiTraKhach.Contains(Search)) || (c.ListTacNghiepText != null && c.ListTacNghiepText.Contains(Search)) ||
                                     (c.ListTuyenHangText != null && c.ListTuyenHangText.Contains(Search)) || (c.ListPhanHoiText != null && c.ListPhanHoiText.Contains(Search)) || (c.ThongTinGiaoViec != null && c.ThongTinGiaoViec.Contains(Search)) || (c.LyDoTuChoi != null && c.LyDoTuChoi.Contains(Search))
                                 )
                                 .CountAsync();
            } else if (!isAdmin && Search == "")
            {
                total = await _db.TblDmcustomers.Select(c => new CustomerDto()
                                 {
                                     Id = c.Id, IdQuocGia = c.IdquocGia, IdCity = c.Idcity, NameVI = c.NameVi ?? "", NameEN = c.NameEn ?? "", Code = c.Code ?? "", AddressVI = c.AddressVi ?? "", AddressEN = c.AddressEn ?? "", TaxCode = c.TaxCode ?? "",
                                     QuocGia = _db.TblDmcountries.Where(x => x.Id == c.IdquocGia).Select(x => x.NameVi).FirstOrDefault() ?? "", ThanhPho = _db.TblDmcities.Where(x => x.Id == c.Idcity).Select(x => x.NameVi).FirstOrDefault() ?? "",
                                     Phone = c.Phone ?? "", Fax = c.Fax ?? "", Email = c.Email ?? "", Website = c.Website ?? "", Note = c.Note ?? "", IdNhanVienSale = c.IdnhanVienSale, IdUserCreate = c.IduserCreate, FlagActive = c.FlagActive, FlagDel = c.FlagDel,
                                     NguoiTao = c.IduserCreate == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserCreate).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     EnumGiaoNhan = c.EnumGiaoNhan != null ? c.EnumGiaoNhan : 0, NhanVien = _db.TblNhanSus.Where(x => x.Id == c.IdnhanVienSale).Select(x => x.HoTenVi).FirstOrDefault() ?? "",
                                     DateCreate = c.DateCreate != null ? string.Format("{0:yyyy-MM-dd}", c.DateCreate) : "", EnumLoaiKhachHang = c.EnumLoaiKhachHang, IdLoaiDoanhNghiep = c.IdloaiDoanhNghiep,
                                     LoaiDoanhNghiep = _db.TblDmloaiDoanhNghieps.Where(x => x.Id == c.IdloaiDoanhNghiep).Select(x => x.NameVi).FirstOrDefault() ?? "", IdUserDelete = c.IduserDelete,
                                     NguoiXoa = c.IduserDelete == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserDelete).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     DateDelete = c.DateDelete != null ? string.Format("{0:yyyy-MM-dd}", c.DateDelete) : "", LyDoXoa = c.LyDoXoa ?? "", NgayTuongTac = c.NgayTuongTac != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuongTac) : "",
                                     NgayChonKhach = c.NgayChonKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChonKhach) : "", NgayTraVe = c.NgayTraVe != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTraVe) : "",
                                     NgayChotKhach = c.NgayChotKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChotKhach) : "", NgayTacNghiep = c.NgayTacNghiep != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTacNghiep) : "",
                                     SttMaxTacNghiep = c.SttmaxTacNghiep, NgayGiao = c.NgayGiao != null ? string.Format("{0:yyyy-MM-dd}", c.NgayGiao) : "", NgayNhan = c.NgayNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayNhan) : "",
                                     IdUserGiaoViec = c.IduserGiaoViec, IdUserTraKhach = c.IduserTraKhach, NgayKetThucNhan = c.NgayKetThucNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayKetThucNhan) : "", ThongTinGiaoViec = c.ThongTinGiaoViec ?? "",
                                     NguoiGiaoViec = c.IduserGiaoViec == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserGiaoViec).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     NguoiTraKhach = c.IduserTraKhach == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserTraKhach).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     ListTacNghiepText = c.ListTacNghiepText ?? "", ListTuyenHangText = c.ListTuyenHangText ?? "", ListPhanHoiText = c.ListPhanHoiText ?? "", NgayTuTraKhach = c.NgayTuTraKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuTraKhach) : "",
                                     LyDoTuChoi = c.LyDoTuChoi ?? "", IdTacNghiepCuoi = c.IdtacNghiepCuoi
                                 }).Where(c => c.FlagActive == true)
                                 .Where(c => c.EnumGiaoNhan != null && c.EnumGiaoNhan == 1 && c.IdUserGiaoViec == idUser)
                                 .CountAsync();
            } else
            {
                total = await _db.TblDmcustomers.Select(c => new CustomerDto()
                                 {
                                     Id = c.Id, IdQuocGia = c.IdquocGia, IdCity = c.Idcity, NameVI = c.NameVi ?? "", NameEN = c.NameEn ?? "", Code = c.Code ?? "", AddressVI = c.AddressVi ?? "", AddressEN = c.AddressEn ?? "", TaxCode = c.TaxCode ?? "",
                                     QuocGia = _db.TblDmcountries.Where(x => x.Id == c.IdquocGia).Select(x => x.NameVi).FirstOrDefault() ?? "", ThanhPho = _db.TblDmcities.Where(x => x.Id == c.Idcity).Select(x => x.NameVi).FirstOrDefault() ?? "",
                                     Phone = c.Phone ?? "", Fax = c.Fax ?? "", Email = c.Email ?? "", Website = c.Website ?? "", Note = c.Note ?? "", IdNhanVienSale = c.IdnhanVienSale, IdUserCreate = c.IduserCreate, FlagActive = c.FlagActive, FlagDel = c.FlagDel,
                                     NguoiTao = c.IduserCreate == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserCreate).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     EnumGiaoNhan = c.EnumGiaoNhan != null ? c.EnumGiaoNhan : 0, NhanVien = _db.TblNhanSus.Where(x => x.Id == c.IdnhanVienSale).Select(x => x.HoTenVi).FirstOrDefault() ?? "",
                                     DateCreate = c.DateCreate != null ? string.Format("{0:yyyy-MM-dd}", c.DateCreate) : "", EnumLoaiKhachHang = c.EnumLoaiKhachHang, IdLoaiDoanhNghiep = c.IdloaiDoanhNghiep,
                                     LoaiDoanhNghiep = _db.TblDmloaiDoanhNghieps.Where(x => x.Id == c.IdloaiDoanhNghiep).Select(x => x.NameVi).FirstOrDefault() ?? "", IdUserDelete = c.IduserDelete,
                                     NguoiXoa = c.IduserDelete == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserDelete).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     DateDelete = c.DateDelete != null ? string.Format("{0:yyyy-MM-dd}", c.DateDelete) : "", LyDoXoa = c.LyDoXoa ?? "", NgayTuongTac = c.NgayTuongTac != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuongTac) : "",
                                     NgayChonKhach = c.NgayChonKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChonKhach) : "", NgayTraVe = c.NgayTraVe != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTraVe) : "",
                                     NgayChotKhach = c.NgayChotKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChotKhach) : "", NgayTacNghiep = c.NgayTacNghiep != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTacNghiep) : "",
                                     SttMaxTacNghiep = c.SttmaxTacNghiep, NgayGiao = c.NgayGiao != null ? string.Format("{0:yyyy-MM-dd}", c.NgayGiao) : "", NgayNhan = c.NgayNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayNhan) : "",
                                     IdUserGiaoViec = c.IduserGiaoViec, IdUserTraKhach = c.IduserTraKhach, NgayKetThucNhan = c.NgayKetThucNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayKetThucNhan) : "", ThongTinGiaoViec = c.ThongTinGiaoViec ?? "",
                                     NguoiGiaoViec = c.IduserGiaoViec == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserGiaoViec).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     NguoiTraKhach = c.IduserTraKhach == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserTraKhach).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     ListTacNghiepText = c.ListTacNghiepText ?? "", ListTuyenHangText = c.ListTuyenHangText ?? "", ListPhanHoiText = c.ListPhanHoiText ?? "", NgayTuTraKhach = c.NgayTuTraKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuTraKhach) : "",
                                     LyDoTuChoi = c.LyDoTuChoi ?? "", IdTacNghiepCuoi = c.IdtacNghiepCuoi
                                 }).Where(c => c.FlagActive == true)
                                 .Where(c => c.EnumGiaoNhan != null && c.EnumGiaoNhan == 1 && c.IdUserGiaoViec == idUser)
                                 .Where(c => 
                                     (c.QuocGia != null && c.QuocGia.Contains(Search)) || (c.ThanhPho != null && c.ThanhPho.Contains(Search)) || (c.NameVI != null && c.NameVI.Contains(Search)) || (c.NameEN != null && c.NameEN.Contains(Search)) ||
                                     (c.Code != null && c.Code.Contains(Search)) || (c.TaxCode != null && c.TaxCode.Contains(Search)) || (c.Email != null && c.Email.Contains(Search)) || (c.Website != null && c.Website.Contains(Search)) ||
                                     (c.Note != null && c.Note.Contains(Search)) || (c.NguoiTao != null && c.NguoiTao.Contains(Search)) || (c.LoaiDoanhNghiep != null && c.LoaiDoanhNghiep.Contains(Search)) || (c.NguoiXoa != null && c.NguoiXoa.Contains(Search)) ||
                                     (c.LyDoXoa != null && c.LyDoXoa.Contains(Search)) || (c.NguoiGiaoViec != null && c.NguoiGiaoViec.Contains(Search)) || (c.NguoiTraKhach != null && c.NguoiTraKhach.Contains(Search)) || (c.ListTacNghiepText != null && c.ListTacNghiepText.Contains(Search)) ||
                                     (c.ListTuyenHangText != null && c.ListTuyenHangText.Contains(Search)) || (c.ListPhanHoiText != null && c.ListPhanHoiText.Contains(Search)) || (c.ThongTinGiaoViec != null && c.ThongTinGiaoViec.Contains(Search)) || (c.LyDoTuChoi != null && c.LyDoTuChoi.Contains(Search))
                                 )
                                 .CountAsync();
            }

            return total;
        }

        // Danh sách chưa giao
        public async Task<List<CustomerDto>?> GetCustomersUndelivered(int Start = 0, int Size = 10, string Search = "")
        {
            List<CustomerDto>? data;

            if(Search == "")
            {
                data = await _db.TblDmcustomers.Select(c => new CustomerDto()
                                {
                                    Id = c.Id, IdQuocGia = c.IdquocGia, IdCity = c.Idcity, NameVI = c.NameVi ?? "", NameEN = c.NameEn ?? "", Code = c.Code ?? "", AddressVI = c.AddressVi ?? "", AddressEN = c.AddressEn ?? "", TaxCode = c.TaxCode ?? "",
                                    QuocGia = _db.TblDmcountries.Where(x => x.Id == c.IdquocGia).Select(x => x.NameVi).FirstOrDefault() ?? "", ThanhPho = _db.TblDmcities.Where(x => x.Id == c.Idcity).Select(x => x.NameVi).FirstOrDefault() ?? "",
                                    Phone = c.Phone ?? "", Fax = c.Fax ?? "", Email = c.Email ?? "", Website = c.Website ?? "", Note = c.Note ?? "", IdNhanVienSale = c.IdnhanVienSale, IdUserCreate = c.IduserCreate, FlagActive = c.FlagActive, FlagDel = c.FlagDel,
                                    NguoiTao = c.IduserCreate == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserCreate).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    EnumGiaoNhan = c.EnumGiaoNhan != null ? c.EnumGiaoNhan : 0, NhanVien = _db.TblNhanSus.Where(x => x.Id == c.IdnhanVienSale).Select(x => x.HoTenVi).FirstOrDefault() ?? "",
                                    DateCreate = c.DateCreate != null ? string.Format("{0:yyyy-MM-dd}", c.DateCreate) : "", EnumLoaiKhachHang = c.EnumLoaiKhachHang, IdLoaiDoanhNghiep = c.IdloaiDoanhNghiep,
                                    LoaiDoanhNghiep = _db.TblDmloaiDoanhNghieps.Where(x => x.Id == c.IdloaiDoanhNghiep).Select(x => x.NameVi).FirstOrDefault() ?? "", IdUserDelete = c.IduserDelete,
                                    NguoiXoa = c.IduserDelete == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserDelete).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    DateDelete = c.DateDelete != null ? string.Format("{0:yyyy-MM-dd}", c.DateDelete) : "", LyDoXoa = c.LyDoXoa ?? "", NgayTuongTac = c.NgayTuongTac != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuongTac) : "",
                                    NgayChonKhach = c.NgayChonKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChonKhach) : "", NgayTraVe = c.NgayTraVe != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTraVe) : "",
                                    NgayChotKhach = c.NgayChotKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChotKhach) : "", NgayTacNghiep = c.NgayTacNghiep != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTacNghiep) : "",
                                    SttMaxTacNghiep = c.SttmaxTacNghiep, NgayGiao = c.NgayGiao != null ? string.Format("{0:yyyy-MM-dd}", c.NgayGiao) : "", NgayNhan = c.NgayNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayNhan) : "",
                                    IdUserGiaoViec = c.IduserGiaoViec, IdUserTraKhach = c.IduserTraKhach, NgayKetThucNhan = c.NgayKetThucNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayKetThucNhan) : "", ThongTinGiaoViec = c.ThongTinGiaoViec ?? "",
                                    NguoiGiaoViec = c.IduserGiaoViec == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserGiaoViec).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    NguoiTraKhach = c.IduserTraKhach == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserTraKhach).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    ListTacNghiepText = c.ListTacNghiepText ?? "", ListTuyenHangText = c.ListTuyenHangText ?? "", ListPhanHoiText = c.ListPhanHoiText ?? "", NgayTuTraKhach = c.NgayTuTraKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuTraKhach) : "",
                                    LyDoTuChoi = c.LyDoTuChoi ?? "", IdTacNghiepCuoi = c.IdtacNghiepCuoi
                                }).Where(c => c.FlagActive == true)
                                .Where(c => (c.EnumGiaoNhan != null && (c.EnumGiaoNhan == 0 || c.EnumGiaoNhan == 3)) || (c.EnumGiaoNhan == null && c.IdNhanVienSale == null))
                                .OrderByDescending(c => c.Id).Skip(Start).Take(Size).ToListAsync();
            } else
            {
                data = await _db.TblDmcustomers.Select(c => new CustomerDto()
                                {
                                    Id = c.Id, IdQuocGia = c.IdquocGia, IdCity = c.Idcity, NameVI = c.NameVi ?? "", NameEN = c.NameEn ?? "", Code = c.Code ?? "", AddressVI = c.AddressVi ?? "", AddressEN = c.AddressEn ?? "", TaxCode = c.TaxCode ?? "",
                                    QuocGia = _db.TblDmcountries.Where(x => x.Id == c.IdquocGia).Select(x => x.NameVi).FirstOrDefault() ?? "", ThanhPho = _db.TblDmcities.Where(x => x.Id == c.Idcity).Select(x => x.NameVi).FirstOrDefault() ?? "",
                                    Phone = c.Phone ?? "", Fax = c.Fax ?? "", Email = c.Email ?? "", Website = c.Website ?? "", Note = c.Note ?? "", IdNhanVienSale = c.IdnhanVienSale, IdUserCreate = c.IduserCreate, FlagActive = c.FlagActive, FlagDel = c.FlagDel,
                                    NguoiTao = c.IduserCreate == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserCreate).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    EnumGiaoNhan = c.EnumGiaoNhan != null ? c.EnumGiaoNhan : 0, NhanVien = _db.TblNhanSus.Where(x => x.Id == c.IdnhanVienSale).Select(x => x.HoTenVi).FirstOrDefault() ?? "",
                                    DateCreate = c.DateCreate != null ? string.Format("{0:yyyy-MM-dd}", c.DateCreate) : "", EnumLoaiKhachHang = c.EnumLoaiKhachHang, IdLoaiDoanhNghiep = c.IdloaiDoanhNghiep,
                                    LoaiDoanhNghiep = _db.TblDmloaiDoanhNghieps.Where(x => x.Id == c.IdloaiDoanhNghiep).Select(x => x.NameVi).FirstOrDefault() ?? "", IdUserDelete = c.IduserDelete,
                                    NguoiXoa = c.IduserDelete == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserDelete).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    DateDelete = c.DateDelete != null ? string.Format("{0:yyyy-MM-dd}", c.DateDelete) : "", LyDoXoa = c.LyDoXoa ?? "", NgayTuongTac = c.NgayTuongTac != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuongTac) : "",
                                    NgayChonKhach = c.NgayChonKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChonKhach) : "", NgayTraVe = c.NgayTraVe != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTraVe) : "",
                                    NgayChotKhach = c.NgayChotKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChotKhach) : "", NgayTacNghiep = c.NgayTacNghiep != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTacNghiep) : "",
                                    SttMaxTacNghiep = c.SttmaxTacNghiep, NgayGiao = c.NgayGiao != null ? string.Format("{0:yyyy-MM-dd}", c.NgayGiao) : "", NgayNhan = c.NgayNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayNhan) : "",
                                    IdUserGiaoViec = c.IduserGiaoViec, IdUserTraKhach = c.IduserTraKhach, NgayKetThucNhan = c.NgayKetThucNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayKetThucNhan) : "", ThongTinGiaoViec = c.ThongTinGiaoViec ?? "",
                                    NguoiGiaoViec = c.IduserGiaoViec == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserGiaoViec).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    NguoiTraKhach = c.IduserTraKhach == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserTraKhach).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    ListTacNghiepText = c.ListTacNghiepText ?? "", ListTuyenHangText = c.ListTuyenHangText ?? "", ListPhanHoiText = c.ListPhanHoiText ?? "", NgayTuTraKhach = c.NgayTuTraKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuTraKhach) : "",
                                    LyDoTuChoi = c.LyDoTuChoi ?? "", IdTacNghiepCuoi = c.IdtacNghiepCuoi
                                }).Where(c => c.FlagActive == true)
                                .Where(c => (c.EnumGiaoNhan != null && (c.EnumGiaoNhan == 0 || c.EnumGiaoNhan == 3)) || (c.EnumGiaoNhan == null && c.IdNhanVienSale == null))
                                .Where(c => 
                                    (c.QuocGia != null && c.QuocGia.Contains(Search)) || (c.ThanhPho != null && c.ThanhPho.Contains(Search)) || (c.NameVI != null && c.NameVI.Contains(Search)) || (c.NameEN != null && c.NameEN.Contains(Search)) ||
                                    (c.Code != null && c.Code.Contains(Search)) || (c.TaxCode != null && c.TaxCode.Contains(Search)) || (c.Email != null && c.Email.Contains(Search)) || (c.Website != null && c.Website.Contains(Search)) ||
                                    (c.Note != null && c.Note.Contains(Search)) || (c.NguoiTao != null && c.NguoiTao.Contains(Search)) || (c.LoaiDoanhNghiep != null && c.LoaiDoanhNghiep.Contains(Search)) || (c.NguoiXoa != null && c.NguoiXoa.Contains(Search)) ||
                                    (c.LyDoXoa != null && c.LyDoXoa.Contains(Search)) || (c.NguoiGiaoViec != null && c.NguoiGiaoViec.Contains(Search)) || (c.NguoiTraKhach != null && c.NguoiTraKhach.Contains(Search)) || (c.ListTacNghiepText != null && c.ListTacNghiepText.Contains(Search)) ||
                                    (c.ListTuyenHangText != null && c.ListTuyenHangText.Contains(Search)) || (c.ListPhanHoiText != null && c.ListPhanHoiText.Contains(Search)) || (c.ThongTinGiaoViec != null && c.ThongTinGiaoViec.Contains(Search)) || (c.LyDoTuChoi != null && c.LyDoTuChoi.Contains(Search))
                                )
                                .OrderByDescending(c => c.Id).Skip(Start).Take(Size).ToListAsync();
            }

            return data;
        }

        public async Task<int> GetTotalCustomersUndelivered(string Search = "")
        {
            var total = 0;

            if(Search == "")
            {
                total = await _db.TblDmcustomers.Select(c => new CustomerDto()
                                 {
                                     Id = c.Id, IdQuocGia = c.IdquocGia, IdCity = c.Idcity, NameVI = c.NameVi ?? "", NameEN = c.NameEn ?? "", Code = c.Code ?? "", AddressVI = c.AddressVi ?? "", AddressEN = c.AddressEn ?? "", TaxCode = c.TaxCode ?? "",
                                     QuocGia = _db.TblDmcountries.Where(x => x.Id == c.IdquocGia).Select(x => x.NameVi).FirstOrDefault() ?? "", ThanhPho = _db.TblDmcities.Where(x => x.Id == c.Idcity).Select(x => x.NameVi).FirstOrDefault() ?? "",
                                     Phone = c.Phone ?? "", Fax = c.Fax ?? "", Email = c.Email ?? "", Website = c.Website ?? "", Note = c.Note ?? "", IdNhanVienSale = c.IdnhanVienSale, IdUserCreate = c.IduserCreate, FlagActive = c.FlagActive, FlagDel = c.FlagDel,
                                     NguoiTao = c.IduserCreate == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserCreate).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     EnumGiaoNhan = c.EnumGiaoNhan != null ? c.EnumGiaoNhan : 0, NhanVien = _db.TblNhanSus.Where(x => x.Id == c.IdnhanVienSale).Select(x => x.HoTenVi).FirstOrDefault() ?? "",
                                     DateCreate = c.DateCreate != null ? string.Format("{0:yyyy-MM-dd}", c.DateCreate) : "", EnumLoaiKhachHang = c.EnumLoaiKhachHang, IdLoaiDoanhNghiep = c.IdloaiDoanhNghiep,
                                     LoaiDoanhNghiep = _db.TblDmloaiDoanhNghieps.Where(x => x.Id == c.IdloaiDoanhNghiep).Select(x => x.NameVi).FirstOrDefault() ?? "", IdUserDelete = c.IduserDelete,
                                     NguoiXoa = c.IduserDelete == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserDelete).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     DateDelete = c.DateDelete != null ? string.Format("{0:yyyy-MM-dd}", c.DateDelete) : "", LyDoXoa = c.LyDoXoa ?? "", NgayTuongTac = c.NgayTuongTac != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuongTac) : "",
                                     NgayChonKhach = c.NgayChonKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChonKhach) : "", NgayTraVe = c.NgayTraVe != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTraVe) : "",
                                     NgayChotKhach = c.NgayChotKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChotKhach) : "", NgayTacNghiep = c.NgayTacNghiep != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTacNghiep) : "",
                                     SttMaxTacNghiep = c.SttmaxTacNghiep, NgayGiao = c.NgayGiao != null ? string.Format("{0:yyyy-MM-dd}", c.NgayGiao) : "", NgayNhan = c.NgayNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayNhan) : "",
                                     IdUserGiaoViec = c.IduserGiaoViec, IdUserTraKhach = c.IduserTraKhach, NgayKetThucNhan = c.NgayKetThucNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayKetThucNhan) : "", ThongTinGiaoViec = c.ThongTinGiaoViec ?? "",
                                     NguoiGiaoViec = c.IduserGiaoViec == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserGiaoViec).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     NguoiTraKhach = c.IduserTraKhach == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserTraKhach).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     ListTacNghiepText = c.ListTacNghiepText ?? "", ListTuyenHangText = c.ListTuyenHangText ?? "", ListPhanHoiText = c.ListPhanHoiText ?? "", NgayTuTraKhach = c.NgayTuTraKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuTraKhach) : "",
                                     LyDoTuChoi = c.LyDoTuChoi ?? "", IdTacNghiepCuoi = c.IdtacNghiepCuoi
                                 }).Where(c => c.FlagActive == true)
                                 .Where(c => (c.EnumGiaoNhan != null && (c.EnumGiaoNhan == 0 || c.EnumGiaoNhan == 3)) || (c.EnumGiaoNhan == null && c.IdNhanVienSale == null))
                                 .CountAsync();
            } else
            {
                total = await _db.TblDmcustomers.Select(c => new CustomerDto()
                                 {
                                     Id = c.Id, IdQuocGia = c.IdquocGia, IdCity = c.Idcity, NameVI = c.NameVi ?? "", NameEN = c.NameEn ?? "", Code = c.Code ?? "", AddressVI = c.AddressVi ?? "", AddressEN = c.AddressEn ?? "", TaxCode = c.TaxCode ?? "",
                                     QuocGia = _db.TblDmcountries.Where(x => x.Id == c.IdquocGia).Select(x => x.NameVi).FirstOrDefault() ?? "", ThanhPho = _db.TblDmcities.Where(x => x.Id == c.Idcity).Select(x => x.NameVi).FirstOrDefault() ?? "",
                                     Phone = c.Phone ?? "", Fax = c.Fax ?? "", Email = c.Email ?? "", Website = c.Website ?? "", Note = c.Note ?? "", IdNhanVienSale = c.IdnhanVienSale, IdUserCreate = c.IduserCreate, FlagActive = c.FlagActive, FlagDel = c.FlagDel,
                                     NguoiTao = c.IduserCreate == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserCreate).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     EnumGiaoNhan = c.EnumGiaoNhan != null ? c.EnumGiaoNhan : 0, NhanVien = _db.TblNhanSus.Where(x => x.Id == c.IdnhanVienSale).Select(x => x.HoTenVi).FirstOrDefault() ?? "",
                                     DateCreate = c.DateCreate != null ? string.Format("{0:yyyy-MM-dd}", c.DateCreate) : "", EnumLoaiKhachHang = c.EnumLoaiKhachHang, IdLoaiDoanhNghiep = c.IdloaiDoanhNghiep,
                                     LoaiDoanhNghiep = _db.TblDmloaiDoanhNghieps.Where(x => x.Id == c.IdloaiDoanhNghiep).Select(x => x.NameVi).FirstOrDefault() ?? "", IdUserDelete = c.IduserDelete,
                                     NguoiXoa = c.IduserDelete == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserDelete).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     DateDelete = c.DateDelete != null ? string.Format("{0:yyyy-MM-dd}", c.DateDelete) : "", LyDoXoa = c.LyDoXoa ?? "", NgayTuongTac = c.NgayTuongTac != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuongTac) : "",
                                     NgayChonKhach = c.NgayChonKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChonKhach) : "", NgayTraVe = c.NgayTraVe != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTraVe) : "",
                                     NgayChotKhach = c.NgayChotKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChotKhach) : "", NgayTacNghiep = c.NgayTacNghiep != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTacNghiep) : "",
                                     SttMaxTacNghiep = c.SttmaxTacNghiep, NgayGiao = c.NgayGiao != null ? string.Format("{0:yyyy-MM-dd}", c.NgayGiao) : "", NgayNhan = c.NgayNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayNhan) : "",
                                     IdUserGiaoViec = c.IduserGiaoViec, IdUserTraKhach = c.IduserTraKhach, NgayKetThucNhan = c.NgayKetThucNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayKetThucNhan) : "", ThongTinGiaoViec = c.ThongTinGiaoViec ?? "",
                                     NguoiGiaoViec = c.IduserGiaoViec == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserGiaoViec).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     NguoiTraKhach = c.IduserTraKhach == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserTraKhach).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     ListTacNghiepText = c.ListTacNghiepText ?? "", ListTuyenHangText = c.ListTuyenHangText ?? "", ListPhanHoiText = c.ListPhanHoiText ?? "", NgayTuTraKhach = c.NgayTuTraKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuTraKhach) : "",
                                     LyDoTuChoi = c.LyDoTuChoi ?? "", IdTacNghiepCuoi = c.IdtacNghiepCuoi
                                 }).Where(c => c.FlagActive == true)
                                 .Where(c => (c.EnumGiaoNhan != null && (c.EnumGiaoNhan == 0 || c.EnumGiaoNhan == 3)) || (c.EnumGiaoNhan == null && c.IdNhanVienSale == null))
                                 .Where(c => 
                                     (c.QuocGia != null && c.QuocGia.Contains(Search)) || (c.ThanhPho != null && c.ThanhPho.Contains(Search)) || (c.NameVI != null && c.NameVI.Contains(Search)) || (c.NameEN != null && c.NameEN.Contains(Search)) ||
                                     (c.Code != null && c.Code.Contains(Search)) || (c.TaxCode != null && c.TaxCode.Contains(Search)) || (c.Email != null && c.Email.Contains(Search)) || (c.Website != null && c.Website.Contains(Search)) ||
                                     (c.Note != null && c.Note.Contains(Search)) || (c.NguoiTao != null && c.NguoiTao.Contains(Search)) || (c.LoaiDoanhNghiep != null && c.LoaiDoanhNghiep.Contains(Search)) || (c.NguoiXoa != null && c.NguoiXoa.Contains(Search)) ||
                                     (c.LyDoXoa != null && c.LyDoXoa.Contains(Search)) || (c.NguoiGiaoViec != null && c.NguoiGiaoViec.Contains(Search)) || (c.NguoiTraKhach != null && c.NguoiTraKhach.Contains(Search)) || (c.ListTacNghiepText != null && c.ListTacNghiepText.Contains(Search)) ||
                                     (c.ListTuyenHangText != null && c.ListTuyenHangText.Contains(Search)) || (c.ListPhanHoiText != null && c.ListPhanHoiText.Contains(Search)) || (c.ThongTinGiaoViec != null && c.ThongTinGiaoViec.Contains(Search)) || (c.LyDoTuChoi != null && c.LyDoTuChoi.Contains(Search))
                                 )
                                 .CountAsync();
            }

            return total;
        }

        // Danh sách đã nhận
        public async Task<List<CustomerDto>?> GetCustomersReceived(int Start = 0, int Size = 10, string Search = "", string permission = "", long idNhanVien = 0)
        {
            var isAdmin = permission.Contains("1048576") || permission.Contains("7000");
            List<CustomerDto>? data;

            if (isAdmin && Search == "")
            {
                data = await _db.TblDmcustomers.Select(c => new CustomerDto()
                                {
                                    Id = c.Id, IdQuocGia = c.IdquocGia, IdCity = c.Idcity, NameVI = c.NameVi ?? "", NameEN = c.NameEn ?? "", Code = c.Code ?? "", AddressVI = c.AddressVi ?? "", AddressEN = c.AddressEn ?? "", TaxCode = c.TaxCode ?? "",
                                    QuocGia = _db.TblDmcountries.Where(x => x.Id == c.IdquocGia).Select(x => x.NameVi).FirstOrDefault() ?? "", ThanhPho = _db.TblDmcities.Where(x => x.Id == c.Idcity).Select(x => x.NameVi).FirstOrDefault() ?? "",
                                    Phone = c.Phone ?? "", Fax = c.Fax ?? "", Email = c.Email ?? "", Website = c.Website ?? "", Note = c.Note ?? "", IdNhanVienSale = c.IdnhanVienSale, IdUserCreate = c.IduserCreate, FlagActive = c.FlagActive, FlagDel = c.FlagDel,
                                    NguoiTao = c.IduserCreate == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserCreate).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    EnumGiaoNhan = c.EnumGiaoNhan != null ? c.EnumGiaoNhan : 0, NhanVien = _db.TblNhanSus.Where(x => x.Id == c.IdnhanVienSale).Select(x => x.HoTenVi).FirstOrDefault() ?? "",
                                    DateCreate = c.DateCreate != null ? string.Format("{0:yyyy-MM-dd}", c.DateCreate) : "", EnumLoaiKhachHang = c.EnumLoaiKhachHang, IdLoaiDoanhNghiep = c.IdloaiDoanhNghiep,
                                    LoaiDoanhNghiep = _db.TblDmloaiDoanhNghieps.Where(x => x.Id == c.IdloaiDoanhNghiep).Select(x => x.NameVi).FirstOrDefault() ?? "", IdUserDelete = c.IduserDelete,
                                    NguoiXoa = c.IduserDelete == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserDelete).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    DateDelete = c.DateDelete != null ? string.Format("{0:yyyy-MM-dd}", c.DateDelete) : "", LyDoXoa = c.LyDoXoa ?? "", NgayTuongTac = c.NgayTuongTac != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuongTac) : "",
                                    NgayChonKhach = c.NgayChonKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChonKhach) : "", NgayTraVe = c.NgayTraVe != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTraVe) : "",
                                    NgayChotKhach = c.NgayChotKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChotKhach) : "", NgayTacNghiep = c.NgayTacNghiep != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTacNghiep) : "",
                                    SttMaxTacNghiep = c.SttmaxTacNghiep, NgayGiao = c.NgayGiao != null ? string.Format("{0:yyyy-MM-dd}", c.NgayGiao) : "", NgayNhan = c.NgayNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayNhan) : "",
                                    IdUserGiaoViec = c.IduserGiaoViec, IdUserTraKhach = c.IduserTraKhach, NgayKetThucNhan = c.NgayKetThucNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayKetThucNhan) : "", ThongTinGiaoViec = c.ThongTinGiaoViec ?? "",
                                    NguoiGiaoViec = c.IduserGiaoViec == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserGiaoViec).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    NguoiTraKhach = c.IduserTraKhach == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserTraKhach).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    ListTacNghiepText = c.ListTacNghiepText ?? "", ListTuyenHangText = c.ListTuyenHangText ?? "", ListPhanHoiText = c.ListPhanHoiText ?? "", NgayTuTraKhach = c.NgayTuTraKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuTraKhach) : "",
                                    LyDoTuChoi = c.LyDoTuChoi ?? "", IdTacNghiepCuoi = c.IdtacNghiepCuoi
                                }).Where(c => c.FlagActive == true)
                                .Where(c => c.EnumGiaoNhan != null && c.EnumGiaoNhan == 2)
                                .OrderByDescending(c => c.Id).Skip(Start).Take(Size).ToListAsync();
            } else if (isAdmin && Search != "")
            {
                data = await _db.TblDmcustomers.Select(c => new CustomerDto()
                                {
                                    Id = c.Id, IdQuocGia = c.IdquocGia, IdCity = c.Idcity, NameVI = c.NameVi ?? "", NameEN = c.NameEn ?? "", Code = c.Code ?? "", AddressVI = c.AddressVi ?? "", AddressEN = c.AddressEn ?? "", TaxCode = c.TaxCode ?? "",
                                    QuocGia = _db.TblDmcountries.Where(x => x.Id == c.IdquocGia).Select(x => x.NameVi).FirstOrDefault() ?? "", ThanhPho = _db.TblDmcities.Where(x => x.Id == c.Idcity).Select(x => x.NameVi).FirstOrDefault() ?? "",
                                    Phone = c.Phone ?? "", Fax = c.Fax ?? "", Email = c.Email ?? "", Website = c.Website ?? "", Note = c.Note ?? "", IdNhanVienSale = c.IdnhanVienSale, IdUserCreate = c.IduserCreate, FlagActive = c.FlagActive, FlagDel = c.FlagDel,
                                    NguoiTao = c.IduserCreate == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserCreate).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    EnumGiaoNhan = c.EnumGiaoNhan != null ? c.EnumGiaoNhan : 0, NhanVien = _db.TblNhanSus.Where(x => x.Id == c.IdnhanVienSale).Select(x => x.HoTenVi).FirstOrDefault() ?? "",
                                    DateCreate = c.DateCreate != null ? string.Format("{0:yyyy-MM-dd}", c.DateCreate) : "", EnumLoaiKhachHang = c.EnumLoaiKhachHang, IdLoaiDoanhNghiep = c.IdloaiDoanhNghiep,
                                    LoaiDoanhNghiep = _db.TblDmloaiDoanhNghieps.Where(x => x.Id == c.IdloaiDoanhNghiep).Select(x => x.NameVi).FirstOrDefault() ?? "", IdUserDelete = c.IduserDelete,
                                    NguoiXoa = c.IduserDelete == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserDelete).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    DateDelete = c.DateDelete != null ? string.Format("{0:yyyy-MM-dd}", c.DateDelete) : "", LyDoXoa = c.LyDoXoa ?? "", NgayTuongTac = c.NgayTuongTac != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuongTac) : "",
                                    NgayChonKhach = c.NgayChonKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChonKhach) : "", NgayTraVe = c.NgayTraVe != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTraVe) : "",
                                    NgayChotKhach = c.NgayChotKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChotKhach) : "", NgayTacNghiep = c.NgayTacNghiep != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTacNghiep) : "",
                                    SttMaxTacNghiep = c.SttmaxTacNghiep, NgayGiao = c.NgayGiao != null ? string.Format("{0:yyyy-MM-dd}", c.NgayGiao) : "", NgayNhan = c.NgayNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayNhan) : "",
                                    IdUserGiaoViec = c.IduserGiaoViec, IdUserTraKhach = c.IduserTraKhach, NgayKetThucNhan = c.NgayKetThucNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayKetThucNhan) : "", ThongTinGiaoViec = c.ThongTinGiaoViec ?? "",
                                    NguoiGiaoViec = c.IduserGiaoViec == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserGiaoViec).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    NguoiTraKhach = c.IduserTraKhach == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserTraKhach).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    ListTacNghiepText = c.ListTacNghiepText ?? "", ListTuyenHangText = c.ListTuyenHangText ?? "", ListPhanHoiText = c.ListPhanHoiText ?? "", NgayTuTraKhach = c.NgayTuTraKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuTraKhach) : "",
                                    LyDoTuChoi = c.LyDoTuChoi ?? "", IdTacNghiepCuoi = c.IdtacNghiepCuoi
                                }).Where(c => c.FlagActive == true)
                                .Where(c => c.EnumGiaoNhan != null && c.EnumGiaoNhan == 2)
                                .Where(c =>
                                    (c.QuocGia != null && c.QuocGia.Contains(Search)) || (c.ThanhPho != null && c.ThanhPho.Contains(Search)) || (c.NameVI != null && c.NameVI.Contains(Search)) || (c.NameEN != null && c.NameEN.Contains(Search)) ||
                                    (c.Code != null && c.Code.Contains(Search)) || (c.TaxCode != null && c.TaxCode.Contains(Search)) || (c.Email != null && c.Email.Contains(Search)) || (c.Website != null && c.Website.Contains(Search)) ||
                                    (c.Note != null && c.Note.Contains(Search)) || (c.NguoiTao != null && c.NguoiTao.Contains(Search)) || (c.LoaiDoanhNghiep != null && c.LoaiDoanhNghiep.Contains(Search)) || (c.NguoiXoa != null && c.NguoiXoa.Contains(Search)) ||
                                    (c.LyDoXoa != null && c.LyDoXoa.Contains(Search)) || (c.NguoiGiaoViec != null && c.NguoiGiaoViec.Contains(Search)) || (c.NguoiTraKhach != null && c.NguoiTraKhach.Contains(Search)) || (c.ListTacNghiepText != null && c.ListTacNghiepText.Contains(Search)) ||
                                    (c.ListTuyenHangText != null && c.ListTuyenHangText.Contains(Search)) || (c.ListPhanHoiText != null && c.ListPhanHoiText.Contains(Search)) || (c.ThongTinGiaoViec != null && c.ThongTinGiaoViec.Contains(Search)) || (c.LyDoTuChoi != null && c.LyDoTuChoi.Contains(Search))
                                )
                                .OrderByDescending(c => c.Id).Skip(Start).Take(Size).ToListAsync();
            } else if (!isAdmin && permission.Contains("7080") && Search == "") {
                var ids = await _employeeHelper.GetListEmployee(idNhanVien);
                var dataCustomers = await _db.TblDmcustomers.Select(c => new CustomerDto()
                                             {
                                                 Id = c.Id, IdQuocGia = c.IdquocGia, IdCity = c.Idcity, NameVI = c.NameVi ?? "", NameEN = c.NameEn ?? "", Code = c.Code ?? "", AddressVI = c.AddressVi ?? "", AddressEN = c.AddressEn ?? "", TaxCode = c.TaxCode ?? "",
                                                 QuocGia = _db.TblDmcountries.Where(x => x.Id == c.IdquocGia).Select(x => x.NameVi).FirstOrDefault() ?? "", ThanhPho = _db.TblDmcities.Where(x => x.Id == c.Idcity).Select(x => x.NameVi).FirstOrDefault() ?? "",
                                                 Phone = c.Phone ?? "", Fax = c.Fax ?? "", Email = c.Email ?? "", Website = c.Website ?? "", Note = c.Note ?? "", IdNhanVienSale = c.IdnhanVienSale, IdUserCreate = c.IduserCreate, FlagActive = c.FlagActive, FlagDel = c.FlagDel,
                                                 NguoiTao = c.IduserCreate == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserCreate).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                                 EnumGiaoNhan = c.EnumGiaoNhan != null ? c.EnumGiaoNhan : 0, NhanVien = _db.TblNhanSus.Where(x => x.Id == c.IdnhanVienSale).Select(x => x.HoTenVi).FirstOrDefault() ?? "",
                                                 DateCreate = c.DateCreate != null ? string.Format("{0:yyyy-MM-dd}", c.DateCreate) : "", EnumLoaiKhachHang = c.EnumLoaiKhachHang, IdLoaiDoanhNghiep = c.IdloaiDoanhNghiep,
                                                 LoaiDoanhNghiep = _db.TblDmloaiDoanhNghieps.Where(x => x.Id == c.IdloaiDoanhNghiep).Select(x => x.NameVi).FirstOrDefault() ?? "", IdUserDelete = c.IduserDelete,
                                                 NguoiXoa = c.IduserDelete == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserDelete).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                                 DateDelete = c.DateDelete != null ? string.Format("{0:yyyy-MM-dd}", c.DateDelete) : "", LyDoXoa = c.LyDoXoa ?? "", NgayTuongTac = c.NgayTuongTac != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuongTac) : "",
                                                 NgayChonKhach = c.NgayChonKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChonKhach) : "", NgayTraVe = c.NgayTraVe != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTraVe) : "",
                                                 NgayChotKhach = c.NgayChotKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChotKhach) : "", NgayTacNghiep = c.NgayTacNghiep != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTacNghiep) : "",
                                                 SttMaxTacNghiep = c.SttmaxTacNghiep, NgayGiao = c.NgayGiao != null ? string.Format("{0:yyyy-MM-dd}", c.NgayGiao) : "", NgayNhan = c.NgayNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayNhan) : "",
                                                 IdUserGiaoViec = c.IduserGiaoViec, IdUserTraKhach = c.IduserTraKhach, NgayKetThucNhan = c.NgayKetThucNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayKetThucNhan) : "", ThongTinGiaoViec = c.ThongTinGiaoViec ?? "",
                                                 NguoiGiaoViec = c.IduserGiaoViec == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserGiaoViec).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                                 NguoiTraKhach = c.IduserTraKhach == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserTraKhach).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                                 ListTacNghiepText = c.ListTacNghiepText ?? "", ListTuyenHangText = c.ListTuyenHangText ?? "", ListPhanHoiText = c.ListPhanHoiText ?? "", NgayTuTraKhach = c.NgayTuTraKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuTraKhach) : "",
                                                 LyDoTuChoi = c.LyDoTuChoi ?? "", IdTacNghiepCuoi = c.IdtacNghiepCuoi
                                             }).Where(c => c.FlagActive == true)
                                             .Where(c => c.EnumGiaoNhan != null && c.EnumGiaoNhan == 2)
                                             .OrderByDescending(c => c.Id).Skip(Start).Take(Size).ToListAsync();
                data = dataCustomers.Where(c => c.IdNhanVienSale == idNhanVien || (ids != null && c.IdNhanVienSale != null && ids.Contains(c.IdNhanVienSale.Value))).ToList();
            } else if (!isAdmin && permission.Contains("7080") && Search != "") {
                var ids = await _employeeHelper.GetListEmployee(idNhanVien);
                var dataCustomers = await _db.TblDmcustomers.Select(c => new CustomerDto()
                                             {
                                                 Id = c.Id, IdQuocGia = c.IdquocGia, IdCity = c.Idcity, NameVI = c.NameVi ?? "", NameEN = c.NameEn ?? "", Code = c.Code ?? "", AddressVI = c.AddressVi ?? "", AddressEN = c.AddressEn ?? "", TaxCode = c.TaxCode ?? "",
                                                 QuocGia = _db.TblDmcountries.Where(x => x.Id == c.IdquocGia).Select(x => x.NameVi).FirstOrDefault() ?? "", ThanhPho = _db.TblDmcities.Where(x => x.Id == c.Idcity).Select(x => x.NameVi).FirstOrDefault() ?? "",
                                                 Phone = c.Phone ?? "", Fax = c.Fax ?? "", Email = c.Email ?? "", Website = c.Website ?? "", Note = c.Note ?? "", IdNhanVienSale = c.IdnhanVienSale, IdUserCreate = c.IduserCreate, FlagActive = c.FlagActive, FlagDel = c.FlagDel,
                                                 NguoiTao = c.IduserCreate == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserCreate).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                                 EnumGiaoNhan = c.EnumGiaoNhan != null ? c.EnumGiaoNhan : 0, NhanVien = _db.TblNhanSus.Where(x => x.Id == c.IdnhanVienSale).Select(x => x.HoTenVi).FirstOrDefault() ?? "",
                                                 DateCreate = c.DateCreate != null ? string.Format("{0:yyyy-MM-dd}", c.DateCreate) : "", EnumLoaiKhachHang = c.EnumLoaiKhachHang, IdLoaiDoanhNghiep = c.IdloaiDoanhNghiep,
                                                 LoaiDoanhNghiep = _db.TblDmloaiDoanhNghieps.Where(x => x.Id == c.IdloaiDoanhNghiep).Select(x => x.NameVi).FirstOrDefault() ?? "", IdUserDelete = c.IduserDelete,
                                                 NguoiXoa = c.IduserDelete == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserDelete).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                                 DateDelete = c.DateDelete != null ? string.Format("{0:yyyy-MM-dd}", c.DateDelete) : "", LyDoXoa = c.LyDoXoa ?? "", NgayTuongTac = c.NgayTuongTac != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuongTac) : "",
                                                 NgayChonKhach = c.NgayChonKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChonKhach) : "", NgayTraVe = c.NgayTraVe != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTraVe) : "",
                                                 NgayChotKhach = c.NgayChotKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChotKhach) : "", NgayTacNghiep = c.NgayTacNghiep != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTacNghiep) : "",
                                                 SttMaxTacNghiep = c.SttmaxTacNghiep, NgayGiao = c.NgayGiao != null ? string.Format("{0:yyyy-MM-dd}", c.NgayGiao) : "", NgayNhan = c.NgayNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayNhan) : "",
                                                 IdUserGiaoViec = c.IduserGiaoViec, IdUserTraKhach = c.IduserTraKhach, NgayKetThucNhan = c.NgayKetThucNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayKetThucNhan) : "", ThongTinGiaoViec = c.ThongTinGiaoViec ?? "",
                                                 NguoiGiaoViec = c.IduserGiaoViec == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserGiaoViec).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                                 NguoiTraKhach = c.IduserTraKhach == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserTraKhach).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                                 ListTacNghiepText = c.ListTacNghiepText ?? "", ListTuyenHangText = c.ListTuyenHangText ?? "", ListPhanHoiText = c.ListPhanHoiText ?? "", NgayTuTraKhach = c.NgayTuTraKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuTraKhach) : "",
                                                 LyDoTuChoi = c.LyDoTuChoi ?? "", IdTacNghiepCuoi = c.IdtacNghiepCuoi
                                             }).Where(c => c.FlagActive == true)
                                             .Where(c => c.EnumGiaoNhan != null && c.EnumGiaoNhan == 2)
                                             .Where(c =>
                                                 (c.QuocGia != null && c.QuocGia.Contains(Search)) || (c.ThanhPho != null && c.ThanhPho.Contains(Search)) || (c.NameVI != null && c.NameVI.Contains(Search)) || (c.NameEN != null && c.NameEN.Contains(Search)) ||
                                                 (c.Code != null && c.Code.Contains(Search)) || (c.TaxCode != null && c.TaxCode.Contains(Search)) || (c.Email != null && c.Email.Contains(Search)) || (c.Website != null && c.Website.Contains(Search)) ||
                                                 (c.Note != null && c.Note.Contains(Search)) || (c.NguoiTao != null && c.NguoiTao.Contains(Search)) || (c.LoaiDoanhNghiep != null && c.LoaiDoanhNghiep.Contains(Search)) || (c.NguoiXoa != null && c.NguoiXoa.Contains(Search)) ||
                                                 (c.LyDoXoa != null && c.LyDoXoa.Contains(Search)) || (c.NguoiGiaoViec != null && c.NguoiGiaoViec.Contains(Search)) || (c.NguoiTraKhach != null && c.NguoiTraKhach.Contains(Search)) || (c.ListTacNghiepText != null && c.ListTacNghiepText.Contains(Search)) ||
                                                 (c.ListTuyenHangText != null && c.ListTuyenHangText.Contains(Search)) || (c.ListPhanHoiText != null && c.ListPhanHoiText.Contains(Search)) || (c.ThongTinGiaoViec != null && c.ThongTinGiaoViec.Contains(Search)) || (c.LyDoTuChoi != null && c.LyDoTuChoi.Contains(Search))
                                             )
                                             .OrderByDescending(c => c.Id).Skip(Start).Take(Size).ToListAsync();
                data = dataCustomers.Where(c => c.IdNhanVienSale == idNhanVien || (ids != null && c.IdNhanVienSale != null && ids.Contains(c.IdNhanVienSale.Value))).ToList();
            } else if (!isAdmin && !permission.Contains("7080") && Search == "")
            {
                data = await _db.TblDmcustomers.Select(c => new CustomerDto()
                                {
                                    Id = c.Id, IdQuocGia = c.IdquocGia, IdCity = c.Idcity, NameVI = c.NameVi ?? "", NameEN = c.NameEn ?? "", Code = c.Code ?? "", AddressVI = c.AddressVi ?? "", AddressEN = c.AddressEn ?? "", TaxCode = c.TaxCode ?? "",
                                    QuocGia = _db.TblDmcountries.Where(x => x.Id == c.IdquocGia).Select(x => x.NameVi).FirstOrDefault() ?? "", ThanhPho = _db.TblDmcities.Where(x => x.Id == c.Idcity).Select(x => x.NameVi).FirstOrDefault() ?? "",
                                    Phone = c.Phone ?? "", Fax = c.Fax ?? "", Email = c.Email ?? "", Website = c.Website ?? "", Note = c.Note ?? "", IdNhanVienSale = c.IdnhanVienSale, IdUserCreate = c.IduserCreate, FlagActive = c.FlagActive, FlagDel = c.FlagDel,
                                    NguoiTao = c.IduserCreate == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserCreate).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    EnumGiaoNhan = c.EnumGiaoNhan != null ? c.EnumGiaoNhan : 0, NhanVien = _db.TblNhanSus.Where(x => x.Id == c.IdnhanVienSale).Select(x => x.HoTenVi).FirstOrDefault() ?? "",
                                    DateCreate = c.DateCreate != null ? string.Format("{0:yyyy-MM-dd}", c.DateCreate) : "", EnumLoaiKhachHang = c.EnumLoaiKhachHang, IdLoaiDoanhNghiep = c.IdloaiDoanhNghiep,
                                    LoaiDoanhNghiep = _db.TblDmloaiDoanhNghieps.Where(x => x.Id == c.IdloaiDoanhNghiep).Select(x => x.NameVi).FirstOrDefault() ?? "", IdUserDelete = c.IduserDelete,
                                    NguoiXoa = c.IduserDelete == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserDelete).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    DateDelete = c.DateDelete != null ? string.Format("{0:yyyy-MM-dd}", c.DateDelete) : "", LyDoXoa = c.LyDoXoa ?? "", NgayTuongTac = c.NgayTuongTac != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuongTac) : "",
                                    NgayChonKhach = c.NgayChonKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChonKhach) : "", NgayTraVe = c.NgayTraVe != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTraVe) : "",
                                    NgayChotKhach = c.NgayChotKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChotKhach) : "", NgayTacNghiep = c.NgayTacNghiep != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTacNghiep) : "",
                                    SttMaxTacNghiep = c.SttmaxTacNghiep, NgayGiao = c.NgayGiao != null ? string.Format("{0:yyyy-MM-dd}", c.NgayGiao) : "", NgayNhan = c.NgayNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayNhan) : "",
                                    IdUserGiaoViec = c.IduserGiaoViec, IdUserTraKhach = c.IduserTraKhach, NgayKetThucNhan = c.NgayKetThucNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayKetThucNhan) : "", ThongTinGiaoViec = c.ThongTinGiaoViec ?? "",
                                    NguoiGiaoViec = c.IduserGiaoViec == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserGiaoViec).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    NguoiTraKhach = c.IduserTraKhach == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserTraKhach).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    ListTacNghiepText = c.ListTacNghiepText ?? "", ListTuyenHangText = c.ListTuyenHangText ?? "", ListPhanHoiText = c.ListPhanHoiText ?? "", NgayTuTraKhach = c.NgayTuTraKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuTraKhach) : "",
                                    LyDoTuChoi = c.LyDoTuChoi ?? "", IdTacNghiepCuoi = c.IdtacNghiepCuoi
                                }).Where(c => c.FlagActive == true)
                                .Where(c => c.EnumGiaoNhan != null && c.EnumGiaoNhan == 2 && c.IdNhanVienSale == idNhanVien)
                                .OrderByDescending(c => c.Id).Skip(Start).Take(Size).ToListAsync();
            } else
            {
                data = await _db.TblDmcustomers.Select(c => new CustomerDto()
                                {
                                    Id = c.Id, IdQuocGia = c.IdquocGia, IdCity = c.Idcity, NameVI = c.NameVi ?? "", NameEN = c.NameEn ?? "", Code = c.Code ?? "", AddressVI = c.AddressVi ?? "", AddressEN = c.AddressEn ?? "", TaxCode = c.TaxCode ?? "",
                                    QuocGia = _db.TblDmcountries.Where(x => x.Id == c.IdquocGia).Select(x => x.NameVi).FirstOrDefault() ?? "", ThanhPho = _db.TblDmcities.Where(x => x.Id == c.Idcity).Select(x => x.NameVi).FirstOrDefault() ?? "",
                                    Phone = c.Phone ?? "", Fax = c.Fax ?? "", Email = c.Email ?? "", Website = c.Website ?? "", Note = c.Note ?? "", IdNhanVienSale = c.IdnhanVienSale, IdUserCreate = c.IduserCreate, FlagActive = c.FlagActive, FlagDel = c.FlagDel,
                                    NguoiTao = c.IduserCreate == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserCreate).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    EnumGiaoNhan = c.EnumGiaoNhan != null ? c.EnumGiaoNhan : 0, NhanVien = _db.TblNhanSus.Where(x => x.Id == c.IdnhanVienSale).Select(x => x.HoTenVi).FirstOrDefault() ?? "",
                                    DateCreate = c.DateCreate != null ? string.Format("{0:yyyy-MM-dd}", c.DateCreate) : "", EnumLoaiKhachHang = c.EnumLoaiKhachHang, IdLoaiDoanhNghiep = c.IdloaiDoanhNghiep,
                                    LoaiDoanhNghiep = _db.TblDmloaiDoanhNghieps.Where(x => x.Id == c.IdloaiDoanhNghiep).Select(x => x.NameVi).FirstOrDefault() ?? "", IdUserDelete = c.IduserDelete,
                                    NguoiXoa = c.IduserDelete == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserDelete).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    DateDelete = c.DateDelete != null ? string.Format("{0:yyyy-MM-dd}", c.DateDelete) : "", LyDoXoa = c.LyDoXoa ?? "", NgayTuongTac = c.NgayTuongTac != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuongTac) : "",
                                    NgayChonKhach = c.NgayChonKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChonKhach) : "", NgayTraVe = c.NgayTraVe != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTraVe) : "",
                                    NgayChotKhach = c.NgayChotKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChotKhach) : "", NgayTacNghiep = c.NgayTacNghiep != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTacNghiep) : "",
                                    SttMaxTacNghiep = c.SttmaxTacNghiep, NgayGiao = c.NgayGiao != null ? string.Format("{0:yyyy-MM-dd}", c.NgayGiao) : "", NgayNhan = c.NgayNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayNhan) : "",
                                    IdUserGiaoViec = c.IduserGiaoViec, IdUserTraKhach = c.IduserTraKhach, NgayKetThucNhan = c.NgayKetThucNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayKetThucNhan) : "", ThongTinGiaoViec = c.ThongTinGiaoViec ?? "",
                                    NguoiGiaoViec = c.IduserGiaoViec == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserGiaoViec).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    NguoiTraKhach = c.IduserTraKhach == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserTraKhach).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    ListTacNghiepText = c.ListTacNghiepText ?? "", ListTuyenHangText = c.ListTuyenHangText ?? "", ListPhanHoiText = c.ListPhanHoiText ?? "", NgayTuTraKhach = c.NgayTuTraKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuTraKhach) : "",
                                    LyDoTuChoi = c.LyDoTuChoi ?? "", IdTacNghiepCuoi = c.IdtacNghiepCuoi
                                }).Where(c => c.FlagActive == true)
                                .Where(c => c.EnumGiaoNhan != null && c.EnumGiaoNhan == 2 && c.IdNhanVienSale == idNhanVien)
                                .Where(c =>
                                    (c.QuocGia != null && c.QuocGia.Contains(Search)) || (c.ThanhPho != null && c.ThanhPho.Contains(Search)) || (c.NameVI != null && c.NameVI.Contains(Search)) || (c.NameEN != null && c.NameEN.Contains(Search)) ||
                                    (c.Code != null && c.Code.Contains(Search)) || (c.TaxCode != null && c.TaxCode.Contains(Search)) || (c.Email != null && c.Email.Contains(Search)) || (c.Website != null && c.Website.Contains(Search)) ||
                                    (c.Note != null && c.Note.Contains(Search)) || (c.NguoiTao != null && c.NguoiTao.Contains(Search)) || (c.LoaiDoanhNghiep != null && c.LoaiDoanhNghiep.Contains(Search)) || (c.NguoiXoa != null && c.NguoiXoa.Contains(Search)) ||
                                    (c.LyDoXoa != null && c.LyDoXoa.Contains(Search)) || (c.NguoiGiaoViec != null && c.NguoiGiaoViec.Contains(Search)) || (c.NguoiTraKhach != null && c.NguoiTraKhach.Contains(Search)) || (c.ListTacNghiepText != null && c.ListTacNghiepText.Contains(Search)) ||
                                    (c.ListTuyenHangText != null && c.ListTuyenHangText.Contains(Search)) || (c.ListPhanHoiText != null && c.ListPhanHoiText.Contains(Search)) || (c.ThongTinGiaoViec != null && c.ThongTinGiaoViec.Contains(Search)) || (c.LyDoTuChoi != null && c.LyDoTuChoi.Contains(Search))
                                )
                                .OrderByDescending(c => c.Id).Skip(Start).Take(Size).ToListAsync();
            }

            return data;
        }

        public async Task<int> GetTotalCustomersReceived(string Search = "", string permission = "", long idNhanVien = 0)
        {
            var isAdmin = permission.Contains("1048576") || permission.Contains("7000");
            var total = 0;

            if(isAdmin && Search == "")
            {
                total = await _db.TblDmcustomers.Select(c => new CustomerDto()
                                 {
                                     Id = c.Id, IdQuocGia = c.IdquocGia, IdCity = c.Idcity, NameVI = c.NameVi ?? "", NameEN = c.NameEn ?? "", Code = c.Code ?? "", AddressVI = c.AddressVi ?? "", AddressEN = c.AddressEn ?? "", TaxCode = c.TaxCode ?? "",
                                     QuocGia = _db.TblDmcountries.Where(x => x.Id == c.IdquocGia).Select(x => x.NameVi).FirstOrDefault() ?? "", ThanhPho = _db.TblDmcities.Where(x => x.Id == c.Idcity).Select(x => x.NameVi).FirstOrDefault() ?? "",
                                     Phone = c.Phone ?? "", Fax = c.Fax ?? "", Email = c.Email ?? "", Website = c.Website ?? "", Note = c.Note ?? "", IdNhanVienSale = c.IdnhanVienSale, IdUserCreate = c.IduserCreate, FlagActive = c.FlagActive, FlagDel = c.FlagDel,
                                     NguoiTao = c.IduserCreate == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserCreate).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     EnumGiaoNhan = c.EnumGiaoNhan != null ? c.EnumGiaoNhan : 0, NhanVien = _db.TblNhanSus.Where(x => x.Id == c.IdnhanVienSale).Select(x => x.HoTenVi).FirstOrDefault() ?? "",
                                     DateCreate = c.DateCreate != null ? string.Format("{0:yyyy-MM-dd}", c.DateCreate) : "", EnumLoaiKhachHang = c.EnumLoaiKhachHang, IdLoaiDoanhNghiep = c.IdloaiDoanhNghiep,
                                     LoaiDoanhNghiep = _db.TblDmloaiDoanhNghieps.Where(x => x.Id == c.IdloaiDoanhNghiep).Select(x => x.NameVi).FirstOrDefault() ?? "", IdUserDelete = c.IduserDelete,
                                     NguoiXoa = c.IduserDelete == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserDelete).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     DateDelete = c.DateDelete != null ? string.Format("{0:yyyy-MM-dd}", c.DateDelete) : "", LyDoXoa = c.LyDoXoa ?? "", NgayTuongTac = c.NgayTuongTac != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuongTac) : "",
                                     NgayChonKhach = c.NgayChonKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChonKhach) : "", NgayTraVe = c.NgayTraVe != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTraVe) : "",
                                     NgayChotKhach = c.NgayChotKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChotKhach) : "", NgayTacNghiep = c.NgayTacNghiep != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTacNghiep) : "",
                                     SttMaxTacNghiep = c.SttmaxTacNghiep, NgayGiao = c.NgayGiao != null ? string.Format("{0:yyyy-MM-dd}", c.NgayGiao) : "", NgayNhan = c.NgayNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayNhan) : "",
                                     IdUserGiaoViec = c.IduserGiaoViec, IdUserTraKhach = c.IduserTraKhach, NgayKetThucNhan = c.NgayKetThucNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayKetThucNhan) : "", ThongTinGiaoViec = c.ThongTinGiaoViec ?? "",
                                     NguoiGiaoViec = c.IduserGiaoViec == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserGiaoViec).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     NguoiTraKhach = c.IduserTraKhach == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserTraKhach).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     ListTacNghiepText = c.ListTacNghiepText ?? "", ListTuyenHangText = c.ListTuyenHangText ?? "", ListPhanHoiText = c.ListPhanHoiText ?? "", NgayTuTraKhach = c.NgayTuTraKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuTraKhach) : "",
                                     LyDoTuChoi = c.LyDoTuChoi ?? "", IdTacNghiepCuoi = c.IdtacNghiepCuoi
                                 }).Where(c => c.FlagActive == true)
                                 .Where(c => c.EnumGiaoNhan != null && c.EnumGiaoNhan == 2)
                                 .CountAsync();
            } else if(isAdmin && Search != "")
            {
                total = await _db.TblDmcustomers.Select(c => new CustomerDto()
                                 {
                                     Id = c.Id, IdQuocGia = c.IdquocGia, IdCity = c.Idcity, NameVI = c.NameVi ?? "", NameEN = c.NameEn ?? "", Code = c.Code ?? "", AddressVI = c.AddressVi ?? "", AddressEN = c.AddressEn ?? "", TaxCode = c.TaxCode ?? "",
                                     QuocGia = _db.TblDmcountries.Where(x => x.Id == c.IdquocGia).Select(x => x.NameVi).FirstOrDefault() ?? "", ThanhPho = _db.TblDmcities.Where(x => x.Id == c.Idcity).Select(x => x.NameVi).FirstOrDefault() ?? "",
                                     Phone = c.Phone ?? "", Fax = c.Fax ?? "", Email = c.Email ?? "", Website = c.Website ?? "", Note = c.Note ?? "", IdNhanVienSale = c.IdnhanVienSale, IdUserCreate = c.IduserCreate, FlagActive = c.FlagActive, FlagDel = c.FlagDel,
                                     NguoiTao = c.IduserCreate == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserCreate).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     EnumGiaoNhan = c.EnumGiaoNhan != null ? c.EnumGiaoNhan : 0, NhanVien = _db.TblNhanSus.Where(x => x.Id == c.IdnhanVienSale).Select(x => x.HoTenVi).FirstOrDefault() ?? "",
                                     DateCreate = c.DateCreate != null ? string.Format("{0:yyyy-MM-dd}", c.DateCreate) : "", EnumLoaiKhachHang = c.EnumLoaiKhachHang, IdLoaiDoanhNghiep = c.IdloaiDoanhNghiep,
                                     LoaiDoanhNghiep = _db.TblDmloaiDoanhNghieps.Where(x => x.Id == c.IdloaiDoanhNghiep).Select(x => x.NameVi).FirstOrDefault() ?? "", IdUserDelete = c.IduserDelete,
                                     NguoiXoa = c.IduserDelete == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserDelete).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     DateDelete = c.DateDelete != null ? string.Format("{0:yyyy-MM-dd}", c.DateDelete) : "", LyDoXoa = c.LyDoXoa ?? "", NgayTuongTac = c.NgayTuongTac != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuongTac) : "",
                                     NgayChonKhach = c.NgayChonKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChonKhach) : "", NgayTraVe = c.NgayTraVe != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTraVe) : "",
                                     NgayChotKhach = c.NgayChotKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChotKhach) : "", NgayTacNghiep = c.NgayTacNghiep != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTacNghiep) : "",
                                     SttMaxTacNghiep = c.SttmaxTacNghiep, NgayGiao = c.NgayGiao != null ? string.Format("{0:yyyy-MM-dd}", c.NgayGiao) : "", NgayNhan = c.NgayNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayNhan) : "",
                                     IdUserGiaoViec = c.IduserGiaoViec, IdUserTraKhach = c.IduserTraKhach, NgayKetThucNhan = c.NgayKetThucNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayKetThucNhan) : "", ThongTinGiaoViec = c.ThongTinGiaoViec ?? "",
                                     NguoiGiaoViec = c.IduserGiaoViec == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserGiaoViec).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     NguoiTraKhach = c.IduserTraKhach == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserTraKhach).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     ListTacNghiepText = c.ListTacNghiepText ?? "", ListTuyenHangText = c.ListTuyenHangText ?? "", ListPhanHoiText = c.ListPhanHoiText ?? "", NgayTuTraKhach = c.NgayTuTraKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuTraKhach) : "",
                                     LyDoTuChoi = c.LyDoTuChoi ?? "", IdTacNghiepCuoi = c.IdtacNghiepCuoi
                                 }).Where(c => c.FlagActive == true)
                                 .Where(c => c.EnumGiaoNhan != null && c.EnumGiaoNhan == 2)
                                 .Where(c => 
                                     (c.QuocGia != null && c.QuocGia.Contains(Search)) || (c.ThanhPho != null && c.ThanhPho.Contains(Search)) || (c.NameVI != null && c.NameVI.Contains(Search)) || (c.NameEN != null && c.NameEN.Contains(Search)) ||
                                     (c.Code != null && c.Code.Contains(Search)) || (c.TaxCode != null && c.TaxCode.Contains(Search)) || (c.Email != null && c.Email.Contains(Search)) || (c.Website != null && c.Website.Contains(Search)) ||
                                     (c.Note != null && c.Note.Contains(Search)) || (c.NguoiTao != null && c.NguoiTao.Contains(Search)) || (c.LoaiDoanhNghiep != null && c.LoaiDoanhNghiep.Contains(Search)) || (c.NguoiXoa != null && c.NguoiXoa.Contains(Search)) ||
                                     (c.LyDoXoa != null && c.LyDoXoa.Contains(Search)) || (c.NguoiGiaoViec != null && c.NguoiGiaoViec.Contains(Search)) || (c.NguoiTraKhach != null && c.NguoiTraKhach.Contains(Search)) || (c.ListTacNghiepText != null && c.ListTacNghiepText.Contains(Search)) ||
                                     (c.ListTuyenHangText != null && c.ListTuyenHangText.Contains(Search)) || (c.ListPhanHoiText != null && c.ListPhanHoiText.Contains(Search)) || (c.ThongTinGiaoViec != null && c.ThongTinGiaoViec.Contains(Search)) || (c.LyDoTuChoi != null && c.LyDoTuChoi.Contains(Search))
                                 )
                                 .CountAsync();
            } else if (!isAdmin && permission.Contains("7080") && Search == "") { 
                var ids = await _employeeHelper.GetListEmployee(idNhanVien);
                var dataCustomers = await _db.TblDmcustomers.Select(c => new CustomerDto()
                                             {
                                                 Id = c.Id, IdQuocGia = c.IdquocGia, IdCity = c.Idcity, NameVI = c.NameVi ?? "", NameEN = c.NameEn ?? "", Code = c.Code ?? "", AddressVI = c.AddressVi ?? "", AddressEN = c.AddressEn ?? "", TaxCode = c.TaxCode ?? "",
                                                 QuocGia = _db.TblDmcountries.Where(x => x.Id == c.IdquocGia).Select(x => x.NameVi).FirstOrDefault() ?? "", ThanhPho = _db.TblDmcities.Where(x => x.Id == c.Idcity).Select(x => x.NameVi).FirstOrDefault() ?? "",
                                                 Phone = c.Phone ?? "", Fax = c.Fax ?? "", Email = c.Email ?? "", Website = c.Website ?? "", Note = c.Note ?? "", IdNhanVienSale = c.IdnhanVienSale, IdUserCreate = c.IduserCreate, FlagActive = c.FlagActive, FlagDel = c.FlagDel,
                                                 NguoiTao = c.IduserCreate == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserCreate).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                                 EnumGiaoNhan = c.EnumGiaoNhan != null ? c.EnumGiaoNhan : 0, NhanVien = _db.TblNhanSus.Where(x => x.Id == c.IdnhanVienSale).Select(x => x.HoTenVi).FirstOrDefault() ?? "",
                                                 DateCreate = c.DateCreate != null ? string.Format("{0:yyyy-MM-dd}", c.DateCreate) : "", EnumLoaiKhachHang = c.EnumLoaiKhachHang, IdLoaiDoanhNghiep = c.IdloaiDoanhNghiep,
                                                 LoaiDoanhNghiep = _db.TblDmloaiDoanhNghieps.Where(x => x.Id == c.IdloaiDoanhNghiep).Select(x => x.NameVi).FirstOrDefault() ?? "", IdUserDelete = c.IduserDelete,
                                                 NguoiXoa = c.IduserDelete == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserDelete).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                                 DateDelete = c.DateDelete != null ? string.Format("{0:yyyy-MM-dd}", c.DateDelete) : "", LyDoXoa = c.LyDoXoa ?? "", NgayTuongTac = c.NgayTuongTac != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuongTac) : "",
                                                 NgayChonKhach = c.NgayChonKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChonKhach) : "", NgayTraVe = c.NgayTraVe != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTraVe) : "",
                                                 NgayChotKhach = c.NgayChotKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChotKhach) : "", NgayTacNghiep = c.NgayTacNghiep != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTacNghiep) : "",
                                                 SttMaxTacNghiep = c.SttmaxTacNghiep, NgayGiao = c.NgayGiao != null ? string.Format("{0:yyyy-MM-dd}", c.NgayGiao) : "", NgayNhan = c.NgayNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayNhan) : "",
                                                 IdUserGiaoViec = c.IduserGiaoViec, IdUserTraKhach = c.IduserTraKhach, NgayKetThucNhan = c.NgayKetThucNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayKetThucNhan) : "", ThongTinGiaoViec = c.ThongTinGiaoViec ?? "",
                                                 NguoiGiaoViec = c.IduserGiaoViec == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserGiaoViec).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                                 NguoiTraKhach = c.IduserTraKhach == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserTraKhach).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                                 ListTacNghiepText = c.ListTacNghiepText ?? "", ListTuyenHangText = c.ListTuyenHangText ?? "", ListPhanHoiText = c.ListPhanHoiText ?? "", NgayTuTraKhach = c.NgayTuTraKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuTraKhach) : "",
                                                 LyDoTuChoi = c.LyDoTuChoi ?? "", IdTacNghiepCuoi = c.IdtacNghiepCuoi
                                             }).Where(c => c.FlagActive == true)
                                             .Where(c => c.EnumGiaoNhan != null && c.EnumGiaoNhan == 2).ToListAsync();
                total = dataCustomers.Where(c => c.IdNhanVienSale == idNhanVien || (ids != null && c.IdNhanVienSale != null && ids.Contains(c.IdNhanVienSale.Value))).Count();
            } else if (!isAdmin && permission.Contains("7080") && Search != "") {
                var ids = await _employeeHelper.GetListEmployee(idNhanVien);
                var dataCustomers = await _db.TblDmcustomers.Select(c => new CustomerDto()
                                             {
                                                 Id = c.Id, IdQuocGia = c.IdquocGia, IdCity = c.Idcity, NameVI = c.NameVi ?? "", NameEN = c.NameEn ?? "", Code = c.Code ?? "", AddressVI = c.AddressVi ?? "", AddressEN = c.AddressEn ?? "", TaxCode = c.TaxCode ?? "",
                                                 QuocGia = _db.TblDmcountries.Where(x => x.Id == c.IdquocGia).Select(x => x.NameVi).FirstOrDefault() ?? "", ThanhPho = _db.TblDmcities.Where(x => x.Id == c.Idcity).Select(x => x.NameVi).FirstOrDefault() ?? "",
                                                 Phone = c.Phone ?? "", Fax = c.Fax ?? "", Email = c.Email ?? "", Website = c.Website ?? "", Note = c.Note ?? "", IdNhanVienSale = c.IdnhanVienSale, IdUserCreate = c.IduserCreate, FlagActive = c.FlagActive, FlagDel = c.FlagDel,
                                                 NguoiTao = c.IduserCreate == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserCreate).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                                 EnumGiaoNhan = c.EnumGiaoNhan != null ? c.EnumGiaoNhan : 0, NhanVien = _db.TblNhanSus.Where(x => x.Id == c.IdnhanVienSale).Select(x => x.HoTenVi).FirstOrDefault() ?? "",
                                                 DateCreate = c.DateCreate != null ? string.Format("{0:yyyy-MM-dd}", c.DateCreate) : "", EnumLoaiKhachHang = c.EnumLoaiKhachHang, IdLoaiDoanhNghiep = c.IdloaiDoanhNghiep,
                                                 LoaiDoanhNghiep = _db.TblDmloaiDoanhNghieps.Where(x => x.Id == c.IdloaiDoanhNghiep).Select(x => x.NameVi).FirstOrDefault() ?? "", IdUserDelete = c.IduserDelete,
                                                 NguoiXoa = c.IduserDelete == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserDelete).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                                 DateDelete = c.DateDelete != null ? string.Format("{0:yyyy-MM-dd}", c.DateDelete) : "", LyDoXoa = c.LyDoXoa ?? "", NgayTuongTac = c.NgayTuongTac != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuongTac) : "",
                                                 NgayChonKhach = c.NgayChonKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChonKhach) : "", NgayTraVe = c.NgayTraVe != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTraVe) : "",
                                                 NgayChotKhach = c.NgayChotKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChotKhach) : "", NgayTacNghiep = c.NgayTacNghiep != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTacNghiep) : "",
                                                 SttMaxTacNghiep = c.SttmaxTacNghiep, NgayGiao = c.NgayGiao != null ? string.Format("{0:yyyy-MM-dd}", c.NgayGiao) : "", NgayNhan = c.NgayNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayNhan) : "",
                                                 IdUserGiaoViec = c.IduserGiaoViec, IdUserTraKhach = c.IduserTraKhach, NgayKetThucNhan = c.NgayKetThucNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayKetThucNhan) : "", ThongTinGiaoViec = c.ThongTinGiaoViec ?? "",
                                                 NguoiGiaoViec = c.IduserGiaoViec == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserGiaoViec).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                                 NguoiTraKhach = c.IduserTraKhach == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserTraKhach).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                                 ListTacNghiepText = c.ListTacNghiepText ?? "", ListTuyenHangText = c.ListTuyenHangText ?? "", ListPhanHoiText = c.ListPhanHoiText ?? "", NgayTuTraKhach = c.NgayTuTraKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuTraKhach) : "",
                                                 LyDoTuChoi = c.LyDoTuChoi ?? "", IdTacNghiepCuoi = c.IdtacNghiepCuoi
                                             }).Where(c => c.FlagActive == true)
                                             .Where(c => c.EnumGiaoNhan != null && c.EnumGiaoNhan == 2)
                                             .Where(c =>
                                                 (c.QuocGia != null && c.QuocGia.Contains(Search)) || (c.ThanhPho != null && c.ThanhPho.Contains(Search)) || (c.NameVI != null && c.NameVI.Contains(Search)) || (c.NameEN != null && c.NameEN.Contains(Search)) ||
                                                 (c.Code != null && c.Code.Contains(Search)) || (c.TaxCode != null && c.TaxCode.Contains(Search)) || (c.Email != null && c.Email.Contains(Search)) || (c.Website != null && c.Website.Contains(Search)) ||
                                                 (c.Note != null && c.Note.Contains(Search)) || (c.NguoiTao != null && c.NguoiTao.Contains(Search)) || (c.LoaiDoanhNghiep != null && c.LoaiDoanhNghiep.Contains(Search)) || (c.NguoiXoa != null && c.NguoiXoa.Contains(Search)) ||
                                                 (c.LyDoXoa != null && c.LyDoXoa.Contains(Search)) || (c.NguoiGiaoViec != null && c.NguoiGiaoViec.Contains(Search)) || (c.NguoiTraKhach != null && c.NguoiTraKhach.Contains(Search)) || (c.ListTacNghiepText != null && c.ListTacNghiepText.Contains(Search)) ||
                                                 (c.ListTuyenHangText != null && c.ListTuyenHangText.Contains(Search)) || (c.ListPhanHoiText != null && c.ListPhanHoiText.Contains(Search)) || (c.ThongTinGiaoViec != null && c.ThongTinGiaoViec.Contains(Search)) || (c.LyDoTuChoi != null && c.LyDoTuChoi.Contains(Search))
                                             ).ToListAsync();
                total = dataCustomers.Where(c => c.IdNhanVienSale == idNhanVien || (ids != null && c.IdNhanVienSale != null && ids.Contains(c.IdNhanVienSale.Value))).Count();
            } else if (!isAdmin && !permission.Contains("7080") && Search == "")
            {
                total = await _db.TblDmcustomers.Select(c => new CustomerDto()
                                 {
                                     Id = c.Id, IdQuocGia = c.IdquocGia, IdCity = c.Idcity, NameVI = c.NameVi ?? "", NameEN = c.NameEn ?? "", Code = c.Code ?? "", AddressVI = c.AddressVi ?? "", AddressEN = c.AddressEn ?? "", TaxCode = c.TaxCode ?? "",
                                     QuocGia = _db.TblDmcountries.Where(x => x.Id == c.IdquocGia).Select(x => x.NameVi).FirstOrDefault() ?? "", ThanhPho = _db.TblDmcities.Where(x => x.Id == c.Idcity).Select(x => x.NameVi).FirstOrDefault() ?? "",
                                     Phone = c.Phone ?? "", Fax = c.Fax ?? "", Email = c.Email ?? "", Website = c.Website ?? "", Note = c.Note ?? "", IdNhanVienSale = c.IdnhanVienSale, IdUserCreate = c.IduserCreate, FlagActive = c.FlagActive, FlagDel = c.FlagDel,
                                     NguoiTao = c.IduserCreate == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserCreate).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     EnumGiaoNhan = c.EnumGiaoNhan != null ? c.EnumGiaoNhan : 0, NhanVien = _db.TblNhanSus.Where(x => x.Id == c.IdnhanVienSale).Select(x => x.HoTenVi).FirstOrDefault() ?? "",
                                     DateCreate = c.DateCreate != null ? string.Format("{0:yyyy-MM-dd}", c.DateCreate) : "", EnumLoaiKhachHang = c.EnumLoaiKhachHang, IdLoaiDoanhNghiep = c.IdloaiDoanhNghiep,
                                     LoaiDoanhNghiep = _db.TblDmloaiDoanhNghieps.Where(x => x.Id == c.IdloaiDoanhNghiep).Select(x => x.NameVi).FirstOrDefault() ?? "", IdUserDelete = c.IduserDelete,
                                     NguoiXoa = c.IduserDelete == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserDelete).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     DateDelete = c.DateDelete != null ? string.Format("{0:yyyy-MM-dd}", c.DateDelete) : "", LyDoXoa = c.LyDoXoa ?? "", NgayTuongTac = c.NgayTuongTac != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuongTac) : "",
                                     NgayChonKhach = c.NgayChonKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChonKhach) : "", NgayTraVe = c.NgayTraVe != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTraVe) : "",
                                     NgayChotKhach = c.NgayChotKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChotKhach) : "", NgayTacNghiep = c.NgayTacNghiep != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTacNghiep) : "",
                                     SttMaxTacNghiep = c.SttmaxTacNghiep, NgayGiao = c.NgayGiao != null ? string.Format("{0:yyyy-MM-dd}", c.NgayGiao) : "", NgayNhan = c.NgayNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayNhan) : "",
                                     IdUserGiaoViec = c.IduserGiaoViec, IdUserTraKhach = c.IduserTraKhach, NgayKetThucNhan = c.NgayKetThucNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayKetThucNhan) : "", ThongTinGiaoViec = c.ThongTinGiaoViec ?? "",
                                     NguoiGiaoViec = c.IduserGiaoViec == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserGiaoViec).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     NguoiTraKhach = c.IduserTraKhach == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserTraKhach).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     ListTacNghiepText = c.ListTacNghiepText ?? "", ListTuyenHangText = c.ListTuyenHangText ?? "", ListPhanHoiText = c.ListPhanHoiText ?? "", NgayTuTraKhach = c.NgayTuTraKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuTraKhach) : "",
                                     LyDoTuChoi = c.LyDoTuChoi ?? "", IdTacNghiepCuoi = c.IdtacNghiepCuoi
                                 }).Where(c => c.FlagActive == true)
                                 .Where(c => c.EnumGiaoNhan != null && c.EnumGiaoNhan == 2 && c.IdNhanVienSale == idNhanVien)
                                 .CountAsync();
            } else
            {
                total = await _db.TblDmcustomers.Select(c => new CustomerDto()
                                 {
                                     Id = c.Id, IdQuocGia = c.IdquocGia, IdCity = c.Idcity, NameVI = c.NameVi ?? "", NameEN = c.NameEn ?? "", Code = c.Code ?? "", AddressVI = c.AddressVi ?? "", AddressEN = c.AddressEn ?? "", TaxCode = c.TaxCode ?? "",
                                     QuocGia = _db.TblDmcountries.Where(x => x.Id == c.IdquocGia).Select(x => x.NameVi).FirstOrDefault() ?? "", ThanhPho = _db.TblDmcities.Where(x => x.Id == c.Idcity).Select(x => x.NameVi).FirstOrDefault() ?? "",
                                     Phone = c.Phone ?? "", Fax = c.Fax ?? "", Email = c.Email ?? "", Website = c.Website ?? "", Note = c.Note ?? "", IdNhanVienSale = c.IdnhanVienSale, IdUserCreate = c.IduserCreate, FlagActive = c.FlagActive, FlagDel = c.FlagDel,
                                     NguoiTao = c.IduserCreate == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserCreate).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     EnumGiaoNhan = c.EnumGiaoNhan != null ? c.EnumGiaoNhan : 0, NhanVien = _db.TblNhanSus.Where(x => x.Id == c.IdnhanVienSale).Select(x => x.HoTenVi).FirstOrDefault() ?? "",
                                     DateCreate = c.DateCreate != null ? string.Format("{0:yyyy-MM-dd}", c.DateCreate) : "", EnumLoaiKhachHang = c.EnumLoaiKhachHang, IdLoaiDoanhNghiep = c.IdloaiDoanhNghiep,
                                     LoaiDoanhNghiep = _db.TblDmloaiDoanhNghieps.Where(x => x.Id == c.IdloaiDoanhNghiep).Select(x => x.NameVi).FirstOrDefault() ?? "", IdUserDelete = c.IduserDelete,
                                     NguoiXoa = c.IduserDelete == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserDelete).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     DateDelete = c.DateDelete != null ? string.Format("{0:yyyy-MM-dd}", c.DateDelete) : "", LyDoXoa = c.LyDoXoa ?? "", NgayTuongTac = c.NgayTuongTac != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuongTac) : "",
                                     NgayChonKhach = c.NgayChonKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChonKhach) : "", NgayTraVe = c.NgayTraVe != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTraVe) : "",
                                     NgayChotKhach = c.NgayChotKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChotKhach) : "", NgayTacNghiep = c.NgayTacNghiep != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTacNghiep) : "",
                                     SttMaxTacNghiep = c.SttmaxTacNghiep, NgayGiao = c.NgayGiao != null ? string.Format("{0:yyyy-MM-dd}", c.NgayGiao) : "", NgayNhan = c.NgayNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayNhan) : "",
                                     IdUserGiaoViec = c.IduserGiaoViec, IdUserTraKhach = c.IduserTraKhach, NgayKetThucNhan = c.NgayKetThucNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayKetThucNhan) : "", ThongTinGiaoViec = c.ThongTinGiaoViec ?? "",
                                     NguoiGiaoViec = c.IduserGiaoViec == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserGiaoViec).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     NguoiTraKhach = c.IduserTraKhach == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserTraKhach).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     ListTacNghiepText = c.ListTacNghiepText ?? "", ListTuyenHangText = c.ListTuyenHangText ?? "", ListPhanHoiText = c.ListPhanHoiText ?? "", NgayTuTraKhach = c.NgayTuTraKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuTraKhach) : "",
                                     LyDoTuChoi = c.LyDoTuChoi ?? "", IdTacNghiepCuoi = c.IdtacNghiepCuoi
                                 }).Where(c => c.FlagActive == true)
                                 .Where(c => c.EnumGiaoNhan != null && c.EnumGiaoNhan == 2 && c.IdNhanVienSale == idNhanVien)
                                 .Where(c => 
                                     (c.QuocGia != null && c.QuocGia.Contains(Search)) || (c.ThanhPho != null && c.ThanhPho.Contains(Search)) || (c.NameVI != null && c.NameVI.Contains(Search)) || (c.NameEN != null && c.NameEN.Contains(Search)) ||
                                     (c.Code != null && c.Code.Contains(Search)) || (c.TaxCode != null && c.TaxCode.Contains(Search)) || (c.Email != null && c.Email.Contains(Search)) || (c.Website != null && c.Website.Contains(Search)) ||
                                     (c.Note != null && c.Note.Contains(Search)) || (c.NguoiTao != null && c.NguoiTao.Contains(Search)) || (c.LoaiDoanhNghiep != null && c.LoaiDoanhNghiep.Contains(Search)) || (c.NguoiXoa != null && c.NguoiXoa.Contains(Search)) ||
                                     (c.LyDoXoa != null && c.LyDoXoa.Contains(Search)) || (c.NguoiGiaoViec != null && c.NguoiGiaoViec.Contains(Search)) || (c.NguoiTraKhach != null && c.NguoiTraKhach.Contains(Search)) || (c.ListTacNghiepText != null && c.ListTacNghiepText.Contains(Search)) ||
                                     (c.ListTuyenHangText != null && c.ListTuyenHangText.Contains(Search)) || (c.ListPhanHoiText != null && c.ListPhanHoiText.Contains(Search)) || (c.ThongTinGiaoViec != null && c.ThongTinGiaoViec.Contains(Search)) || (c.LyDoTuChoi != null && c.LyDoTuChoi.Contains(Search))
                                 )
                                 .CountAsync();
            }

            return total;
        }


        // Danh sách được giao dành cho nhân viên
        public async Task<List<CustomerDto>?> GetCustomersDelivered(int Start = 0, int Size = 10, string Search = "", long idNhanVien = 0)
        {
            List<CustomerDto>? data;

            if (Search == "")
            {
                data = await _db.TblDmcustomers.Select(c => new CustomerDto()
                                {
                                    Id = c.Id, IdQuocGia = c.IdquocGia, IdCity = c.Idcity, NameVI = c.NameVi ?? "", NameEN = c.NameEn ?? "", Code = c.Code ?? "", AddressVI = c.AddressVi ?? "", AddressEN = c.AddressEn ?? "", TaxCode = c.TaxCode ?? "",
                                    QuocGia = _db.TblDmcountries.Where(x => x.Id == c.IdquocGia).Select(x => x.NameVi).FirstOrDefault() ?? "", ThanhPho = _db.TblDmcities.Where(x => x.Id == c.Idcity).Select(x => x.NameVi).FirstOrDefault() ?? "",
                                    Phone = c.Phone ?? "", Fax = c.Fax ?? "", Email = c.Email ?? "", Website = c.Website ?? "", Note = c.Note ?? "", IdNhanVienSale = c.IdnhanVienSale, IdUserCreate = c.IduserCreate, FlagActive = c.FlagActive, FlagDel = c.FlagDel,
                                    NguoiTao = c.IduserCreate == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserCreate).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    EnumGiaoNhan = c.EnumGiaoNhan != null ? c.EnumGiaoNhan : 0, NhanVien = _db.TblNhanSus.Where(x => x.Id == c.IdnhanVienSale).Select(x => x.HoTenVi).FirstOrDefault() ?? "",
                                    DateCreate = c.DateCreate != null ? string.Format("{0:yyyy-MM-dd}", c.DateCreate) : "", EnumLoaiKhachHang = c.EnumLoaiKhachHang, IdLoaiDoanhNghiep = c.IdloaiDoanhNghiep,
                                    LoaiDoanhNghiep = _db.TblDmloaiDoanhNghieps.Where(x => x.Id == c.IdloaiDoanhNghiep).Select(x => x.NameVi).FirstOrDefault() ?? "", IdUserDelete = c.IduserDelete,
                                    NguoiXoa = c.IduserDelete == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserDelete).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    DateDelete = c.DateDelete != null ? string.Format("{0:yyyy-MM-dd}", c.DateDelete) : "", LyDoXoa = c.LyDoXoa ?? "", NgayTuongTac = c.NgayTuongTac != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuongTac) : "",
                                    NgayChonKhach = c.NgayChonKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChonKhach) : "", NgayTraVe = c.NgayTraVe != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTraVe) : "",
                                    NgayChotKhach = c.NgayChotKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChotKhach) : "", NgayTacNghiep = c.NgayTacNghiep != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTacNghiep) : "",
                                    SttMaxTacNghiep = c.SttmaxTacNghiep, NgayGiao = c.NgayGiao != null ? string.Format("{0:yyyy-MM-dd}", c.NgayGiao) : "", NgayNhan = c.NgayNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayNhan) : "",
                                    IdUserGiaoViec = c.IduserGiaoViec, IdUserTraKhach = c.IduserTraKhach, NgayKetThucNhan = c.NgayKetThucNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayKetThucNhan) : "", ThongTinGiaoViec = c.ThongTinGiaoViec ?? "",
                                    NguoiGiaoViec = c.IduserGiaoViec == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserGiaoViec).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    NguoiTraKhach = c.IduserTraKhach == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserTraKhach).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    ListTacNghiepText = c.ListTacNghiepText ?? "", ListTuyenHangText = c.ListTuyenHangText ?? "", ListPhanHoiText = c.ListPhanHoiText ?? "", NgayTuTraKhach = c.NgayTuTraKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuTraKhach) : "",
                                    LyDoTuChoi = c.LyDoTuChoi ?? "", IdTacNghiepCuoi = c.IdtacNghiepCuoi
                                }).Where(c => c.FlagActive == true)
                                .Where(c => c.EnumGiaoNhan != null && c.EnumGiaoNhan == 1 && c.IdNhanVienSale == idNhanVien)
                                .OrderByDescending(c => c.Id).Skip(Start).Take(Size).ToListAsync();
            } else
            {
                data = await _db.TblDmcustomers.Select(c => new CustomerDto()
                                {
                                    Id = c.Id, IdQuocGia = c.IdquocGia, IdCity = c.Idcity, NameVI = c.NameVi ?? "", NameEN = c.NameEn ?? "", Code = c.Code ?? "", AddressVI = c.AddressVi ?? "", AddressEN = c.AddressEn ?? "", TaxCode = c.TaxCode ?? "",
                                    QuocGia = _db.TblDmcountries.Where(x => x.Id == c.IdquocGia).Select(x => x.NameVi).FirstOrDefault() ?? "", ThanhPho = _db.TblDmcities.Where(x => x.Id == c.Idcity).Select(x => x.NameVi).FirstOrDefault() ?? "",
                                    Phone = c.Phone ?? "", Fax = c.Fax ?? "", Email = c.Email ?? "", Website = c.Website ?? "", Note = c.Note ?? "", IdNhanVienSale = c.IdnhanVienSale, IdUserCreate = c.IduserCreate, FlagActive = c.FlagActive, FlagDel = c.FlagDel,
                                    NguoiTao = c.IduserCreate == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserCreate).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    EnumGiaoNhan = c.EnumGiaoNhan != null ? c.EnumGiaoNhan : 0, NhanVien = _db.TblNhanSus.Where(x => x.Id == c.IdnhanVienSale).Select(x => x.HoTenVi).FirstOrDefault() ?? "",
                                    DateCreate = c.DateCreate != null ? string.Format("{0:yyyy-MM-dd}", c.DateCreate) : "", EnumLoaiKhachHang = c.EnumLoaiKhachHang, IdLoaiDoanhNghiep = c.IdloaiDoanhNghiep,
                                    LoaiDoanhNghiep = _db.TblDmloaiDoanhNghieps.Where(x => x.Id == c.IdloaiDoanhNghiep).Select(x => x.NameVi).FirstOrDefault() ?? "", IdUserDelete = c.IduserDelete,
                                    NguoiXoa = c.IduserDelete == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserDelete).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    DateDelete = c.DateDelete != null ? string.Format("{0:yyyy-MM-dd}", c.DateDelete) : "", LyDoXoa = c.LyDoXoa ?? "", NgayTuongTac = c.NgayTuongTac != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuongTac) : "",
                                    NgayChonKhach = c.NgayChonKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChonKhach) : "", NgayTraVe = c.NgayTraVe != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTraVe) : "",
                                    NgayChotKhach = c.NgayChotKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChotKhach) : "", NgayTacNghiep = c.NgayTacNghiep != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTacNghiep) : "",
                                    SttMaxTacNghiep = c.SttmaxTacNghiep, NgayGiao = c.NgayGiao != null ? string.Format("{0:yyyy-MM-dd}", c.NgayGiao) : "", NgayNhan = c.NgayNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayNhan) : "",
                                    IdUserGiaoViec = c.IduserGiaoViec, IdUserTraKhach = c.IduserTraKhach, NgayKetThucNhan = c.NgayKetThucNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayKetThucNhan) : "", ThongTinGiaoViec = c.ThongTinGiaoViec ?? "",
                                    NguoiGiaoViec = c.IduserGiaoViec == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserGiaoViec).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    NguoiTraKhach = c.IduserTraKhach == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserTraKhach).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                    ListTacNghiepText = c.ListTacNghiepText ?? "", ListTuyenHangText = c.ListTuyenHangText ?? "", ListPhanHoiText = c.ListPhanHoiText ?? "", NgayTuTraKhach = c.NgayTuTraKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuTraKhach) : "",
                                    LyDoTuChoi = c.LyDoTuChoi ?? "", IdTacNghiepCuoi = c.IdtacNghiepCuoi
                                }).Where(c => c.FlagActive == true)
                                .Where(c => c.EnumGiaoNhan != null && c.EnumGiaoNhan == 1 && c.IdNhanVienSale == idNhanVien)
                                .Where(c => 
                                    (c.QuocGia != null && c.QuocGia.Contains(Search)) || (c.ThanhPho != null && c.ThanhPho.Contains(Search)) || (c.NameVI != null && c.NameVI.Contains(Search)) || (c.NameEN != null && c.NameEN.Contains(Search)) ||
                                    (c.Code != null && c.Code.Contains(Search)) || (c.TaxCode != null && c.TaxCode.Contains(Search)) || (c.Email != null && c.Email.Contains(Search)) || (c.Website != null && c.Website.Contains(Search)) ||
                                    (c.Note != null && c.Note.Contains(Search)) || (c.NguoiTao != null && c.NguoiTao.Contains(Search)) || (c.LoaiDoanhNghiep != null && c.LoaiDoanhNghiep.Contains(Search)) || (c.NguoiXoa != null && c.NguoiXoa.Contains(Search)) ||
                                    (c.LyDoXoa != null && c.LyDoXoa.Contains(Search)) || (c.NguoiGiaoViec != null && c.NguoiGiaoViec.Contains(Search)) || (c.NguoiTraKhach != null && c.NguoiTraKhach.Contains(Search)) || (c.ListTacNghiepText != null && c.ListTacNghiepText.Contains(Search)) ||
                                    (c.ListTuyenHangText != null && c.ListTuyenHangText.Contains(Search)) || (c.ListPhanHoiText != null && c.ListPhanHoiText.Contains(Search)) || (c.ThongTinGiaoViec != null && c.ThongTinGiaoViec.Contains(Search)) || (c.LyDoTuChoi != null && c.LyDoTuChoi.Contains(Search))
                                )
                                .OrderByDescending(c => c.Id).Skip(Start).Take(Size).ToListAsync();
            }

            return data;
        }

        public async Task<int> GetTotalCustomersDelivered(string Search = "", long idNhanVien = 0)
        {
            var total = 0;

            if(Search == "")
            {
                total = await _db.TblDmcustomers.Select(c => new CustomerDto()
                                 {
                                     Id = c.Id, IdQuocGia = c.IdquocGia, IdCity = c.Idcity, NameVI = c.NameVi ?? "", NameEN = c.NameEn ?? "", Code = c.Code ?? "", AddressVI = c.AddressVi ?? "", AddressEN = c.AddressEn ?? "", TaxCode = c.TaxCode ?? "",
                                     QuocGia = _db.TblDmcountries.Where(x => x.Id == c.IdquocGia).Select(x => x.NameVi).FirstOrDefault() ?? "", ThanhPho = _db.TblDmcities.Where(x => x.Id == c.Idcity).Select(x => x.NameVi).FirstOrDefault() ?? "",
                                     Phone = c.Phone ?? "", Fax = c.Fax ?? "", Email = c.Email ?? "", Website = c.Website ?? "", Note = c.Note ?? "", IdNhanVienSale = c.IdnhanVienSale, IdUserCreate = c.IduserCreate, FlagActive = c.FlagActive, FlagDel = c.FlagDel,
                                     NguoiTao = c.IduserCreate == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserCreate).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     EnumGiaoNhan = c.EnumGiaoNhan != null ? c.EnumGiaoNhan : 0, NhanVien = _db.TblNhanSus.Where(x => x.Id == c.IdnhanVienSale).Select(x => x.HoTenVi).FirstOrDefault() ?? "",
                                     DateCreate = c.DateCreate != null ? string.Format("{0:yyyy-MM-dd}", c.DateCreate) : "", EnumLoaiKhachHang = c.EnumLoaiKhachHang, IdLoaiDoanhNghiep = c.IdloaiDoanhNghiep,
                                     LoaiDoanhNghiep = _db.TblDmloaiDoanhNghieps.Where(x => x.Id == c.IdloaiDoanhNghiep).Select(x => x.NameVi).FirstOrDefault() ?? "", IdUserDelete = c.IduserDelete,
                                     NguoiXoa = c.IduserDelete == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserDelete).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     DateDelete = c.DateDelete != null ? string.Format("{0:yyyy-MM-dd}", c.DateDelete) : "", LyDoXoa = c.LyDoXoa ?? "", NgayTuongTac = c.NgayTuongTac != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuongTac) : "",
                                     NgayChonKhach = c.NgayChonKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChonKhach) : "", NgayTraVe = c.NgayTraVe != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTraVe) : "",
                                     NgayChotKhach = c.NgayChotKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChotKhach) : "", NgayTacNghiep = c.NgayTacNghiep != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTacNghiep) : "",
                                     SttMaxTacNghiep = c.SttmaxTacNghiep, NgayGiao = c.NgayGiao != null ? string.Format("{0:yyyy-MM-dd}", c.NgayGiao) : "", NgayNhan = c.NgayNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayNhan) : "",
                                     IdUserGiaoViec = c.IduserGiaoViec, IdUserTraKhach = c.IduserTraKhach, NgayKetThucNhan = c.NgayKetThucNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayKetThucNhan) : "", ThongTinGiaoViec = c.ThongTinGiaoViec ?? "",
                                     NguoiGiaoViec = c.IduserGiaoViec == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserGiaoViec).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     NguoiTraKhach = c.IduserTraKhach == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserTraKhach).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     ListTacNghiepText = c.ListTacNghiepText ?? "", ListTuyenHangText = c.ListTuyenHangText ?? "", ListPhanHoiText = c.ListPhanHoiText ?? "", NgayTuTraKhach = c.NgayTuTraKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuTraKhach) : "",
                                     LyDoTuChoi = c.LyDoTuChoi ?? "", IdTacNghiepCuoi = c.IdtacNghiepCuoi
                                 }).Where(c => c.FlagActive == true)
                                 .Where(c => c.EnumGiaoNhan != null && c.EnumGiaoNhan == 1 && c.IdNhanVienSale == idNhanVien)
                                 .CountAsync();
            } else
            {
                total = await _db.TblDmcustomers.Select(c => new CustomerDto()
                                 {
                                     Id = c.Id, IdQuocGia = c.IdquocGia, IdCity = c.Idcity, NameVI = c.NameVi ?? "", NameEN = c.NameEn ?? "", Code = c.Code ?? "", AddressVI = c.AddressVi ?? "", AddressEN = c.AddressEn ?? "", TaxCode = c.TaxCode ?? "",
                                     QuocGia = _db.TblDmcountries.Where(x => x.Id == c.IdquocGia).Select(x => x.NameVi).FirstOrDefault() ?? "", ThanhPho = _db.TblDmcities.Where(x => x.Id == c.Idcity).Select(x => x.NameVi).FirstOrDefault() ?? "",
                                     Phone = c.Phone ?? "", Fax = c.Fax ?? "", Email = c.Email ?? "", Website = c.Website ?? "", Note = c.Note ?? "", IdNhanVienSale = c.IdnhanVienSale, IdUserCreate = c.IduserCreate, FlagActive = c.FlagActive, FlagDel = c.FlagDel,
                                     NguoiTao = c.IduserCreate == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserCreate).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     EnumGiaoNhan = c.EnumGiaoNhan != null ? c.EnumGiaoNhan : 0, NhanVien = _db.TblNhanSus.Where(x => x.Id == c.IdnhanVienSale).Select(x => x.HoTenVi).FirstOrDefault() ?? "",
                                     DateCreate = c.DateCreate != null ? string.Format("{0:yyyy-MM-dd}", c.DateCreate) : "", EnumLoaiKhachHang = c.EnumLoaiKhachHang, IdLoaiDoanhNghiep = c.IdloaiDoanhNghiep,
                                     LoaiDoanhNghiep = _db.TblDmloaiDoanhNghieps.Where(x => x.Id == c.IdloaiDoanhNghiep).Select(x => x.NameVi).FirstOrDefault() ?? "", IdUserDelete = c.IduserDelete,
                                     NguoiXoa = c.IduserDelete == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserDelete).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     DateDelete = c.DateDelete != null ? string.Format("{0:yyyy-MM-dd}", c.DateDelete) : "", LyDoXoa = c.LyDoXoa ?? "", NgayTuongTac = c.NgayTuongTac != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuongTac) : "",
                                     NgayChonKhach = c.NgayChonKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChonKhach) : "", NgayTraVe = c.NgayTraVe != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTraVe) : "",
                                     NgayChotKhach = c.NgayChotKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChotKhach) : "", NgayTacNghiep = c.NgayTacNghiep != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTacNghiep) : "",
                                     SttMaxTacNghiep = c.SttmaxTacNghiep, NgayGiao = c.NgayGiao != null ? string.Format("{0:yyyy-MM-dd}", c.NgayGiao) : "", NgayNhan = c.NgayNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayNhan) : "",
                                     IdUserGiaoViec = c.IduserGiaoViec, IdUserTraKhach = c.IduserTraKhach, NgayKetThucNhan = c.NgayKetThucNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayKetThucNhan) : "", ThongTinGiaoViec = c.ThongTinGiaoViec ?? "",
                                     NguoiGiaoViec = c.IduserGiaoViec == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserGiaoViec).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     NguoiTraKhach = c.IduserTraKhach == 1 ? "admin" : _db.TblSysUsers.Where(x => x.Id == c.IduserTraKhach).Join(_db.TblNhanSus, x => x.IdnhanVien, y => y.Id, (x, y) => new { HoTen = y.HoTenVi ?? "" }).Select(x => x.HoTen).FirstOrDefault() ?? "",
                                     ListTacNghiepText = c.ListTacNghiepText ?? "", ListTuyenHangText = c.ListTuyenHangText ?? "", ListPhanHoiText = c.ListPhanHoiText ?? "", NgayTuTraKhach = c.NgayTuTraKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuTraKhach) : "",
                                     LyDoTuChoi = c.LyDoTuChoi ?? "", IdTacNghiepCuoi = c.IdtacNghiepCuoi
                                 }).Where(c => c.FlagActive == true)
                                 .Where(c => c.EnumGiaoNhan != null && c.EnumGiaoNhan == 1 && c.IdNhanVienSale == idNhanVien)
                                 .Where(c => 
                                     (c.QuocGia != null && c.QuocGia.Contains(Search)) || (c.ThanhPho != null && c.ThanhPho.Contains(Search)) || (c.NameVI != null && c.NameVI.Contains(Search)) || (c.NameEN != null && c.NameEN.Contains(Search)) ||
                                     (c.Code != null && c.Code.Contains(Search)) || (c.TaxCode != null && c.TaxCode.Contains(Search)) || (c.Email != null && c.Email.Contains(Search)) || (c.Website != null && c.Website.Contains(Search)) ||
                                     (c.Note != null && c.Note.Contains(Search)) || (c.NguoiTao != null && c.NguoiTao.Contains(Search)) || (c.LoaiDoanhNghiep != null && c.LoaiDoanhNghiep.Contains(Search)) || (c.NguoiXoa != null && c.NguoiXoa.Contains(Search)) ||
                                     (c.LyDoXoa != null && c.LyDoXoa.Contains(Search)) || (c.NguoiGiaoViec != null && c.NguoiGiaoViec.Contains(Search)) || (c.NguoiTraKhach != null && c.NguoiTraKhach.Contains(Search)) || (c.ListTacNghiepText != null && c.ListTacNghiepText.Contains(Search)) ||
                                     (c.ListTuyenHangText != null && c.ListTuyenHangText.Contains(Search)) || (c.ListPhanHoiText != null && c.ListPhanHoiText.Contains(Search)) || (c.ThongTinGiaoViec != null && c.ThongTinGiaoViec.Contains(Search)) || (c.LyDoTuChoi != null && c.LyDoTuChoi.Contains(Search))
                                 )
                                 .CountAsync();
            }

            return total;
        }

        public async Task<TblDmcustomer?> GetCustomerById(long id)
        {
            TblDmcustomer? data = await _db.TblDmcustomers.Where(c => c.Id == id).FirstOrDefaultAsync();
            return data;
        }

        public async Task CreateCustomer(CreateCustomerRequest req)
        {
            var data = new TblDmcustomer()
            {
                IdquocGia = req.IdQuocGia != -1 ? req.IdQuocGia : null, Idcity = req.IdCity != -1 ? req.IdCity : null,
                Code = req.Code ?? "", NameVi = req.NameVI ?? "",  NameEn = req.NameEN ?? "", AddressVi = req.AddressVI,
                AddressEn = req.AddressEN ?? "", TaxCode = req.TaxCode ?? "", Phone = req.Phone ?? "", Fax = req.Fax ?? "",
                Email = req.Email ?? "", Website = req.Website ?? "", Note = req.Note ?? "", IduserCreate = req.IdUserCreate,
                DateCreate = DateTime.Now, FlagActive = true, FlagDel = false, EnumGiaoNhan = 0,
            };

            await _db.TblDmcustomers.AddAsync(data);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateCustomer(TblDmcustomer data, UpdateCustomerRequest req)
        {
            data.IdquocGia = req.IdQuocGia ?? data.IdquocGia;
            data.Idcity = req.IdCity ?? data.Idcity;
            data.Code = req.Code ?? data.Code;
            data.NameVi = req.NameVI ?? data.NameVi;
            data.NameEn = req.NameEN ?? data.NameEn;
            data.AddressVi = req.AddressVI ?? data.AddressVi;
            data.AddressEn = req.AddressEN ?? data.AddressEn;
            data.TaxCode = req.TaxCode ?? data.TaxCode;
            data.Phone = req.Phone ?? data.Phone;
            data.Fax = req.Fax ?? data.Fax;
            data.Email = req.Email ?? data.Email;
            data.Website = req.Website ?? data.Website;
            data.Note = req.Note ?? data.Note;

            await _db.SaveChangesAsync();
        }

        public async Task DeleteCustomer(TblDmcustomer data, DeleteCustomerRequest req)
        {
            data.FlagActive = !req.FlagDel;
            data.FlagDel = req.FlagDel;
            data.LyDoXoa = req.LyDoXoa ?? "";
            data.DateDelete = req.FlagDel ? DateTime.Now : null;

            await _db.SaveChangesAsync();
        }

        public async Task<bool> IsExistCustomer(string code, string taxCode)
        {
            var data = await _db.TblDmcustomers.Where(x =>
                                    code != "" && taxCode != "" && ((x.Code == code && x.TaxCode == taxCode) || x.Code == code || x.TaxCode == taxCode)
                                 )
                                .FirstOrDefaultAsync();
            if (data == null) return false;
            return true;
        }

        public async Task<bool> IsExistCodeCustomer(string code)
        {
            if (code == "") return false;

            var data = await _db.TblDmcustomers.Where(x => x.Code == code).FirstOrDefaultAsync();

            if (data == null) return false;

            return true;
        }

        public async Task<bool> IsExistTaxCodeCustomer(string code)
        {
            if (code == "") return false;

            var data = await _db.TblDmcustomers.Where(x => x.TaxCode == code).FirstOrDefaultAsync();

            if (data == null) return false;

            return true;
        }

        public async Task<List<TblDmcustomer>?> GetCustomersByIdArray(long[] ids, long? IdNhanVien = null)
        {
            List<TblDmcustomer>? data;
            if (IdNhanVien != null)
            {
                data = await Task.FromResult(_db.TblDmcustomers.ToList().Where(x => ids.Contains(x.Id) && x.IdnhanVienSale == IdNhanVien).ToList());
            } else
            {
                data = await Task.FromResult(_db.TblDmcustomers.ToList().Where(x => ids.Contains(x.Id)).ToList());
            }

            return data;
        }

        public async Task ChooseCustomers(List<TblDmcustomer> data, ChooseCustomerRequest req)
        {
            foreach (var item in data)
            {
                item.IdnhanVienSale = req.IdNhanVienSale;
                item.EnumGiaoNhan = 2;
                item.NgayNhan = DateTime.Now;
                item.NgayKetThucNhan = null;
                item.ThongTinGiaoViec = "";
                item.IduserGiaoViec = null;
            }

            await _db.SaveChangesAsync();
        }

        public async Task DeliveryCustomers(List<TblDmcustomer> data, DeliveryCustomerRequest req)
        {
            var systemOps = await _db.TblSysOptions.FindAsync((long)1);

            foreach (var item in data)
            {
                item.IdnhanVienSale = req.IDNhanVienSale;
                item.IduserGiaoViec = req.IDUserGiaoViec;
                item.ThongTinGiaoViec = req.ThongTinGiaoViec;
                item.EnumGiaoNhan = 1;
                item.NgayGiao = DateTime.Now;
                item.NgayKetThucNhan = systemOps?.NgayNhanKhach != null ? DateTime.Now.AddDays(Convert.ToDouble(systemOps.NgayNhanKhach)) : DateTime.Now.AddDays(3);
            }

            await _db.SaveChangesAsync();
        }

        public async Task UndeliveryCustomers(List<TblDmcustomer> data, UndeliveryCustomerRequest req)
        {
            foreach (var item in data)
            {
                item.IdnhanVienSale = null;
                item.IduserGiaoViec = null;
                item.ThongTinGiaoViec = "";
                item.EnumGiaoNhan = 0;
                item.NgayGiao = null;
                item.NgayNhan = null;
                item.NgayKetThucNhan = null;
                item.LyDoTuChoi = null;
            }

            await _db.SaveChangesAsync();
        }

        public async Task AcceptCustomers(List<TblDmcustomer> data, AcceptCustomerRequest req)
        {
            foreach (var item in data)
            {
                item.EnumGiaoNhan = 2;
                item.NgayNhan = DateTime.Now;
                item.NgayKetThucNhan = null;
            }

            await _db.SaveChangesAsync();
        }

        public async Task DenyCustomers(List<TblDmcustomer> data, DenyCustomerRequest req)
        {
            foreach (var item in data)
            {
                item.IdnhanVienSale = null;
                item.EnumGiaoNhan = 3;
                item.LyDoTuChoi = req.LyDoTuChoi;
                item.NgayKetThucNhan = null;
            }

            await _db.SaveChangesAsync();
        }
    }
}
