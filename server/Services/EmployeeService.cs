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
    public class EmployeeService: IEmployeeService
    {
        private readonly BestCareDbContext _db;
        private readonly IAuthHelper _authHelpers;

        public EmployeeService(BestCareDbContext db, IAuthHelper authHelper)
        {
            _db = db;
            _authHelpers = authHelper;
        }

        public async Task<TblSysUser?> GetAccountById(long id)
        {
            TblSysUser? data = await _db.TblSysUsers.Where(a => a.Id == id).FirstOrDefaultAsync();

            return data;
        }

        public async Task<TblNhanSu?> GetInfoById(long id)
        {
            TblNhanSu? data = await _db.TblNhanSus.Where(e => e.Id == id).FirstOrDefaultAsync();

            return data;
        }

        public async Task<List<ProfileDto>?> GetEmployees(int Start = 0, int Size = 10, string Search = "", bool isDelete = false)
        {
            List<ProfileDto>? data;

            if(Search == "")
            {
                data = await _db.TblSysUsers.Join(_db.TblNhanSus, a => a.IdnhanVien, e => e.Id, (a, e) => new ProfileDto()
                                {
                                    Id = a.Id, username = a.UserName ?? "", active = a.Active, permission = a.Permission, IDNhanVien = a.IdnhanVien,
                                    idChucVu = e.IdchucVu, idPhongBan = e.IdphongBan, idVanPhong = e.IdvanPhong, manhanvien = e.Manhansu, 
                                    chucvu = e.IdchucVu != null ? _db.TblDmchucVus.Where(c => c.Id == e.IdchucVu).Select(c => c.NameVi).FirstOrDefault() : "",
                                    phongban = e.IdphongBan != null ? _db.TblDmphongBans.Where(c => c.Id == e.IdphongBan).Select(c => c.NameVi).FirstOrDefault() : "",
                                    vanphong = e.IdvanPhong != null ? _db.TblDmvanPhongs.Where(c => c.Id == e.IdvanPhong).Select(c => c.NameVi).FirstOrDefault() : "",
                                    hoten = e.HoTenVi ?? "", hotenen = e.HoTenEn ?? "", namsinh = string.Format("{0:yyyy-MM-dd}", e.NamSinh), gioitinh = e.GioiTinh ?? 3, quequan = e.QueQuan ?? "",
                                    diachi = e.DiaChiHienTai ?? "", soCMT = e.SoCmt ?? "", noiCapCMT = e.NoiCapCmt ?? "", photoURL = e.PhotoUrl ?? "", didong = e.DiDong ?? "",
                                    ngayCapCMT = e.NgayCapCmt != null ? string.Format("{0:yyyy-MM-dd}", e.NgayCapCmt) : "", ghichu = e.GhiChu ?? "", soLuongKH = e.SoLuongKh ?? 0,
                                    createDate = e.CreateDate != null ? string.Format("{0:yyyy-MM-dd}", e.CreateDate) : "", idUserDelete = e.IduserDelete,
                                    editDate = e.EditDate != null ? string.Format("{0:yyyy-MM-dd}", e.EditDate) : "", email = e.Email ?? "", flagDelete = e.FlagDelete ?? false,
                                    dateDelete = e.DateDelete != null ? string.Format("{0:yyyy-MM-dd}", e.DateDelete) : ""
                                }).Where(x => (isDelete == false && (x.flagDelete == null || x.flagDelete == isDelete)) || (isDelete == true && x.flagDelete == isDelete))
                                .OrderByDescending(x => x.Id).Skip(Start).Take(Size).ToListAsync();
            } else
            {
                data = await _db.TblSysUsers.Join(_db.TblNhanSus, a => a.IdnhanVien, e => e.Id, (a, e) => new ProfileDto()
                                {
                                    Id = a.Id, username = a.UserName ?? "", active = a.Active, permission = a.Permission, IDNhanVien = a.IdnhanVien,
                                    idChucVu = e.IdchucVu, idPhongBan = e.IdphongBan, idVanPhong = e.IdvanPhong, manhanvien = e.Manhansu, 
                                    chucvu = e.IdchucVu != null ? _db.TblDmchucVus.Where(c => c.Id == e.IdchucVu).Select(c => c.NameVi).FirstOrDefault() : "",
                                    phongban = e.IdphongBan != null ? _db.TblDmphongBans.Where(c => c.Id == e.IdphongBan).Select(c => c.NameVi).FirstOrDefault() : "",
                                    vanphong = e.IdvanPhong != null ? _db.TblDmvanPhongs.Where(c => c.Id == e.IdvanPhong).Select(c => c.NameVi).FirstOrDefault() : "",
                                    hoten = e.HoTenVi ?? "", hotenen = e.HoTenEn ?? "", namsinh = string.Format("{0:yyyy-MM-dd}", e.NamSinh), gioitinh = e.GioiTinh ?? 3, quequan = e.QueQuan ?? "",
                                    diachi = e.DiaChiHienTai ?? "", soCMT = e.SoCmt ?? "", noiCapCMT = e.NoiCapCmt ?? "", photoURL = e.PhotoUrl ?? "", didong = e.DiDong ?? "",
                                    ngayCapCMT = e.NgayCapCmt != null ? string.Format("{0:yyyy-MM-dd}", e.NgayCapCmt) : "", ghichu = e.GhiChu ?? "", soLuongKH = e.SoLuongKh ?? 0,
                                    createDate = e.CreateDate != null ? string.Format("{0:yyyy-MM-dd}", e.CreateDate) : "", idUserDelete = e.IduserDelete,
                                    editDate = e.EditDate != null ? string.Format("{0:yyyy-MM-dd}", e.EditDate) : "", email = e.Email ?? "", flagDelete = e.FlagDelete ?? false,
                                    dateDelete = e.DateDelete != null ? string.Format("{0:yyyy-MM-dd}", e.DateDelete) : ""
                                }).Where(x => (isDelete == false && (x.flagDelete == null || x.flagDelete == isDelete)) || (isDelete == true && x.flagDelete == isDelete))
                                .Where(x => (x.username != null && x.username.Contains(Search)) || (x.manhanvien != null && x.manhanvien.Contains(Search)) ||
                                            (x.chucvu != null && x.chucvu.Contains(Search)) || (x.phongban != null && x.phongban.Contains(Search)) ||
                                            (x.vanphong != null && x.vanphong.Contains(Search)) || (x.hoten != null && x.hoten.Contains(Search)) ||
                                            (x.quequan != null && x.quequan.Contains(Search)) || (x.diachi != null && x.diachi.Contains(Search)) ||
                                            (x.ghichu != null && x.ghichu.Contains(Search))
                                )
                                .OrderByDescending(x => x.Id).Skip(Start).Take(Size).ToListAsync();
            }

            return data;
        }

        public async Task<int> GetTotalEmployees(string Search = "", bool isDelete = false)
        {
            var total = 0;

            if(Search == "")
            {
                total = await _db.TblSysUsers.Join(_db.TblNhanSus, a => a.IdnhanVien, e => e.Id, (a, e) => new ProfileDto()
                                 {
                                     Id = a.Id, username = a.UserName ?? "", active = a.Active, permission = a.Permission, IDNhanVien = a.IdnhanVien,
                                     idChucVu = e.IdchucVu, idPhongBan = e.IdphongBan, idVanPhong = e.IdvanPhong, manhanvien = e.Manhansu, 
                                     chucvu = e.IdchucVu != null ? _db.TblDmchucVus.Where(c => c.Id == e.IdchucVu).Select(c => c.NameVi).FirstOrDefault() : "",
                                     phongban = e.IdphongBan != null ? _db.TblDmphongBans.Where(c => c.Id == e.IdphongBan).Select(c => c.NameVi).FirstOrDefault() : "",
                                     vanphong = e.IdvanPhong != null ? _db.TblDmvanPhongs.Where(c => c.Id == e.IdvanPhong).Select(c => c.NameVi).FirstOrDefault() : "",
                                     hoten = e.HoTenVi ?? "", hotenen = e.HoTenEn ?? "", namsinh = string.Format("{0:yyyy-MM-dd}", e.NamSinh), gioitinh = e.GioiTinh ?? 3, quequan = e.QueQuan ?? "",
                                     diachi = e.DiaChiHienTai ?? "", soCMT = e.SoCmt ?? "", noiCapCMT = e.NoiCapCmt ?? "", photoURL = e.PhotoUrl ?? "", didong = e.DiDong ?? "",
                                     ngayCapCMT = e.NgayCapCmt != null ? string.Format("{0:yyyy-MM-dd}", e.NgayCapCmt) : "", ghichu = e.GhiChu ?? "", soLuongKH = e.SoLuongKh ?? 0,
                                     createDate = e.CreateDate != null ? string.Format("{0:yyyy-MM-dd}", e.CreateDate) : "", idUserDelete = e.IduserDelete,
                                     editDate = e.EditDate != null ? string.Format("{0:yyyy-MM-dd}", e.EditDate) : "", email = e.Email ?? "", flagDelete = e.FlagDelete ?? false,
                                     dateDelete = e.DateDelete != null ? string.Format("{0:yyyy-MM-dd}", e.DateDelete) : ""
                                 }).Where(x => (isDelete == false && (x.flagDelete == null || x.flagDelete == isDelete)) || (isDelete == true && x.flagDelete == isDelete))
                                 .CountAsync();
            } else
            {
                total = await _db.TblSysUsers.Join(_db.TblNhanSus, a => a.IdnhanVien, e => e.Id, (a, e) => new ProfileDto()
                                 {
                                     Id = a.Id, username = a.UserName ?? "", active = a.Active, permission = a.Permission, IDNhanVien = a.IdnhanVien,
                                     idChucVu = e.IdchucVu, idPhongBan = e.IdphongBan, idVanPhong = e.IdvanPhong, manhanvien = e.Manhansu, 
                                     chucvu = e.IdchucVu != null ? _db.TblDmchucVus.Where(c => c.Id == e.IdchucVu).Select(c => c.NameVi).FirstOrDefault() : "",
                                     phongban = e.IdphongBan != null ? _db.TblDmphongBans.Where(c => c.Id == e.IdphongBan).Select(c => c.NameVi).FirstOrDefault() : "",
                                     vanphong = e.IdvanPhong != null ? _db.TblDmvanPhongs.Where(c => c.Id == e.IdvanPhong).Select(c => c.NameVi).FirstOrDefault() : "",
                                     hoten = e.HoTenVi ?? "", hotenen = e.HoTenEn ?? "", namsinh = string.Format("{0:yyyy-MM-dd}", e.NamSinh), gioitinh = e.GioiTinh ?? 3, quequan = e.QueQuan ?? "",
                                     diachi = e.DiaChiHienTai ?? "", soCMT = e.SoCmt ?? "", noiCapCMT = e.NoiCapCmt ?? "", photoURL = e.PhotoUrl ?? "", didong = e.DiDong ?? "",
                                     ngayCapCMT = e.NgayCapCmt != null ? string.Format("{0:yyyy-MM-dd}", e.NgayCapCmt) : "", ghichu = e.GhiChu ?? "", soLuongKH = e.SoLuongKh ?? 0,
                                     createDate = e.CreateDate != null ? string.Format("{0:yyyy-MM-dd}", e.CreateDate) : "", idUserDelete = e.IduserDelete,
                                     editDate = e.EditDate != null ? string.Format("{0:yyyy-MM-dd}", e.EditDate) : "", email = e.Email ?? "", flagDelete = e.FlagDelete ?? false,
                                     dateDelete = e.DateDelete != null ? string.Format("{0:yyyy-MM-dd}", e.DateDelete) : ""
                                 }).Where(x => (isDelete == false && (x.flagDelete == null || x.flagDelete == isDelete)) || (isDelete == true && x.flagDelete == isDelete))
                                 .Where(x => (x.username != null && x.username.Contains(Search)) || (x.manhanvien != null && x.manhanvien.Contains(Search)) ||
                                            (x.chucvu != null && x.chucvu.Contains(Search)) || (x.phongban != null && x.phongban.Contains(Search)) ||
                                            (x.vanphong != null && x.vanphong.Contains(Search)) || (x.hoten != null && x.hoten.Contains(Search)) ||
                                            (x.quequan != null && x.quequan.Contains(Search)) || (x.diachi != null && x.diachi.Contains(Search)) ||
                                            (x.ghichu != null && x.ghichu.Contains(Search))
                                 )
                                 .CountAsync();
            }

            return total;
        }

        public async Task CreateEmployee(CreateEmployeeRequest req)
        {
            var employee = new TblNhanSu()
            {
                Manhansu = req.manhanvien, HoTenVi = req.HoTen, IdchucVu = req.IdChucVu, IdphongBan = req.IdPhongBan, IdvanPhong = req.IdVanPhong,
                NamSinh = DateOnly.Parse(req.NamSinh), GioiTinh = req.GioiTinh ?? null, QueQuan = req.QueQuan ?? "", DiaChiHienTai = req.DiaChi ?? "",
                SoCmt = req.SoCMT ?? "", NoiCapCmt = req.NoiCapCMT ?? "", PhotoUrl = req.PhotoURL ?? "", GhiChu = req.GhiChu ?? "", FlagDelete = false,
                NgayCapCmt = (req.NgayCapCMT != null && req.NgayCapCMT != "") ? DateOnly.Parse(req.NgayCapCMT) : null, SoLuongKh = req.SoLuongKH ?? 0,
                CreateDate = DateTime.Now
            };

            await _db.TblNhanSus.AddAsync(employee);
            await _db.SaveChangesAsync();

            var hashPassword = _authHelpers.Encrypt(req.Password);

            var account = new TblSysUser()
            {
                UserName = req.Username, Password = hashPassword, Permission = req.Permission, Active = true, IdnhanVien = employee.Id
            };

            await _db.TblSysUsers.AddAsync(account);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateEmployee(TblSysUser account, TblNhanSu info, UpdateEmployeeRequest req)
        {
            account.UserName = req.Username ?? account.UserName;
            if(req.Password != "" && req.Password != null)
            {
                var hashPassword = _authHelpers.Encrypt(req.Password);
                account.Password = hashPassword;
            }
            account.Permission = req.Permission ?? account.Permission;
            account.Active = req.Active ?? account.Active;

            info.IdchucVu = req.IdChucVu ?? info.IdchucVu;
            info.IdphongBan = req.IdPhongBan ?? info.IdphongBan;
            info.IdvanPhong = req.IdVanPhong ?? info.IdvanPhong;
            info.Manhansu = req.manhanvien ?? info.Manhansu;
            info.HoTenVi = req.HoTen ?? info.HoTenVi;
            if(req.NamSinh != null && req.NamSinh != "")
            {
                info.NamSinh = DateOnly.Parse(req.NamSinh);
            }
            info.GioiTinh = req.GioiTinh ?? info.GioiTinh;
            info.QueQuan = req.QueQuan ?? info.QueQuan;
            info.DiaChiHienTai = req.DiaChi ?? info.DiaChiHienTai;
            info.SoCmt = req.SoCMT ?? info.SoCmt;
            info.NoiCapCmt = req.NoiCapCMT ?? info.NoiCapCmt;
            if (req.NgayCapCMT != null && req.NgayCapCMT != "")
            {
                info.NgayCapCmt = DateOnly.Parse(req.NgayCapCMT);
            }
            info.PhotoUrl = req.PhotoURL ?? info.PhotoUrl;
            info.GhiChu = req.GhiChu ?? info.GhiChu;
            info.SoLuongKh = req.SoLuongKH ?? info.SoLuongKh;
            info.EditDate = DateTime.Now;

            await _db.SaveChangesAsync();
        }

        public async Task DeleteEmployee(TblNhanSu info, DeleteEmployeeRequest req)
        {
            info.FlagDelete = req.FlagDelete;
            info.IduserDelete = req.IdUserDelete;
            info.DateDelete = req.FlagDelete == true ? DateTime.Now : null;

            await _db.SaveChangesAsync();
        }

        public async Task<bool> IsUsernameExist(string username)
        {
            var employee = await _db.TblSysUsers.Where(x => x.UserName != null && x.UserName.Trim().ToLower() == username.Trim().ToLower()).FirstOrDefaultAsync();
            if (employee == null) return false;
            return true;
        }

        public async Task<bool> IsPersonnelCodeExist(string code)
        {
            var employee = await _db.TblNhanSus.Where(x => x.Manhansu.Trim().ToLower() == code.Trim().ToLower()).FirstOrDefaultAsync();
            if (employee == null) return false;
            return true;
        }
    }
}
