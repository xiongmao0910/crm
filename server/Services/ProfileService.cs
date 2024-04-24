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
    public class ProfileService: IProfileService
    {
        private readonly BestCareDbContext _db;

        public ProfileService(BestCareDbContext db)
        {
            _db = db;
        }

        public async Task<ProfileDto?> GetProfileByIdAccount(long idAccount)
        {
            ProfileDto? data;
            if(idAccount == 1)
            {
                data = await _db.TblSysUsers.Select(a => new ProfileDto()
                {
                    Id = a.Id, username = a.UserName ?? "admin", active = a.Active, permission = a.Permission, IDNhanVien = 0,
                    idChucVu = 0, chucvu = "", idPhongBan = 0, phongban = "", idVanPhong = 0, vanphong = "", manhanvien = "", hoten = "",
                    hotenen = "", namsinh = "", gioitinh = 3, quequan = "", diachi = "", soCMT = "", noiCapCMT = "", ngayCapCMT = "",
                    photoURL = "", didong = "", email = "", ghichu = "", createDate = "", editDate = "", soLuongKH = 0, flagDelete = false,
                    idUserDelete = null, dateDelete = ""
                }).Where(x => x.Id == idAccount).FirstOrDefaultAsync();

            } else
            {
                data = await _db.TblSysUsers.Join(_db.TblNhanSus, a => a.IdnhanVien, e => e.Id, (a, e) => new ProfileDto()
                {
                    Id = a.Id, username = a.UserName ?? "", active = a.Active, permission = a.Permission, IDNhanVien = a.IdnhanVien,
                    idChucVu = e.IdchucVu, idPhongBan = e.IdphongBan, idVanPhong = e.IdvanPhong, manhanvien = e.Manhansu, 
                    chucvu = e.IdchucVu != null ? _db.TblDmchucVus.Where(c => c.Id == e.IdchucVu).Select(c => c.NameVi).FirstOrDefault() : "",
                    phongban = e.IdphongBan != null ? _db.TblDmphongBans.Where(c => c.Id == e.IdphongBan).Select(c => c.NameVi).FirstOrDefault() : "",
                    vanphong = e.IdvanPhong != null ? _db.TblDmvanPhongs.Where(c => c.Id == e.IdvanPhong).Select(c => c.NameVi).FirstOrDefault() : "", hoten = e.HoTenVi ?? "",
                    hotenen = e.HoTenEn ?? "", namsinh = string.Format("{0:yyyy-MM-dd}", e.NamSinh), gioitinh = e.GioiTinh ?? 3, quequan = e.QueQuan ?? "",
                    diachi = e.DiaChiHienTai ?? "", soCMT = e.SoCmt ?? "", noiCapCMT = e.NoiCapCmt ?? "", photoURL = e.PhotoUrl ?? "", didong = e.DiDong ?? "",
                    ngayCapCMT = e.NgayCapCmt != null ? string.Format("{0:yyyy-MM-dd}", e.NgayCapCmt) : "", ghichu = e.GhiChu ?? "", soLuongKH = e.SoLuongKh ?? 0,
                    createDate = e.CreateDate != null ? string.Format("{0:yyyy-MM-dd}", e.CreateDate) : "", idUserDelete = e.IduserDelete,
                    editDate = e.EditDate != null ? string.Format("{0:yyyy-MM-dd}", e.EditDate) : "", flagDelete = e.FlagDelete ?? false,email = e.Email ?? "",
                    dateDelete = e.DateDelete != null ? string.Format("{0:yyyy-MM-dd}", e.DateDelete) : ""
                }).Where(x => x.Id == idAccount).FirstOrDefaultAsync();
            }

            return data;
        }

        public async Task UpdateProfile(TblNhanSu info, UpdateProfileRequest req)
        {
            info.HoTenVi = req.hoten?.Trim()?.Length > 0 ? req.hoten : info.HoTenVi;
            info.NamSinh = req.namsinh?.Trim()?.Length > 0 ? DateOnly.Parse(req.namsinh) : info.NamSinh;
            info.GioiTinh = req.gioitinh ?? info.GioiTinh;
            info.QueQuan = req.quequan?.Trim()?.Length > 0 ? req.quequan : info.QueQuan;
            info.DiaChiHienTai = req.diachi?.Trim()?.Length > 0 ? req.diachi : info.DiaChiHienTai;
            info.SoCmt = req.soCMT?.Trim()?.Length > 0 ? req.soCMT : info.SoCmt;
            info.NoiCapCmt = req.noiCapCMT?.Trim()?.Length > 0 ? req.noiCapCMT : info.NoiCapCmt;
            info.NgayCapCmt = req.ngayCapCMT?.Trim()?.Length > 0 ? DateOnly.Parse(req.ngayCapCMT) : info.NgayCapCmt;
            info.DiDong = req.didong?.Trim()?.Length > 0 ? req.didong : info.DiDong;
            info.Email = req.email?.Trim()?.Length > 0 ? req.email : info.Email;
            info.PhotoUrl = req.PhotoURL?.Trim().Length > 0 ? req.PhotoURL : info.PhotoUrl;

            await _db.SaveChangesAsync();
        }
    }
}
