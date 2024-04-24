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
    public class CategoryService : ICategoryService
    {
        private readonly BestCareDbContext _db;

        public CategoryService(BestCareDbContext db)
        {
            _db = db;
        }

        public async Task<List<PositionDto>?> GetAllPositions()
        {
            List<PositionDto>? data = await _db.TblDmchucVus.Select(x => new PositionDto()
            {
                Id = x.Id, NameVI = x.NameVi ?? "", NameEN = x.NameEn ?? "",
                Code = x.Code ?? "", FlagFavorite = x.FlagFavorite ?? false,
            }).ToListAsync();

            return data;
        }

        public async Task<List<PositionDto>?> GetPositions(int Start = 0, int Size = 10, string Search = "")
        {
            List<PositionDto>? data;

            if (Search == "")
            {
                data = await _db.TblDmchucVus.Select(x => new PositionDto()
                {
                    Id = x.Id, NameVI = x.NameVi ?? "", NameEN = x.NameEn ?? "",
                    Code = x.Code ?? "", FlagFavorite = x.FlagFavorite ?? false,
                }).OrderByDescending(x => x.Id).Skip(Start).Take(Size).ToListAsync();
            } else
            {
                data = await _db.TblDmchucVus.Select(x => new PositionDto()
                {
                    Id = x.Id, NameVI = x.NameVi ?? "", NameEN = x.NameEn ?? "",
                    Code = x.Code ?? "", FlagFavorite = x.FlagFavorite ?? false,
                }).OrderByDescending(x => x.Id).Skip(Start).Take(Size)
                                                   .Where(
                                                       x => (x.Code != null && x.Code.Contains(Search)) ||
                                                            (x.NameVI != null && x.NameVI.Contains(Search)) ||
                                                            (x.NameEN != null && x.NameEN.Contains(Search))
                                                   ).ToListAsync();
            }

            return data;
        }

        public async Task<int> GetTotalPositions(string Search)
        {
            var total = 0;

            if (Search == "")
            {
                total = await _db.TblDmchucVus.Select(x => new PositionDto()
                {
                    Id = x.Id, NameVI = x.NameVi ?? "", NameEN = x.NameEn ?? "",
                    Code = x.Code ?? "", FlagFavorite = x.FlagFavorite ?? false,
                }).CountAsync();
            } else
            {
                total = await _db.TblDmchucVus.Select(x => new PositionDto()
                {
                    Id = x.Id, NameVI = x.NameVi ?? "", NameEN = x.NameEn ?? "",
                    Code = x.Code ?? "", FlagFavorite = x.FlagFavorite ?? false,
                })
                                                     .Where(
                                                         x => (x.Code != null && x.Code.Contains(Search)) ||
                                                              (x.NameVI != null && x.NameVI.Contains(Search)) ||
                                                              (x.NameEN != null && x.NameEN.Contains(Search))
                                                     ).CountAsync();
            }

            return total;
        }

        public async Task<TblDmchucVu?> GetPositionById(long id)
        {
            TblDmchucVu? data = await _db.TblDmchucVus.Where(x => x.Id == id).FirstOrDefaultAsync();

            return data;
        }

        public async Task CreatePosition(CreatePositionRequest req)
        {
            var position = new TblDmchucVu()
            {
                Code = req.Code, NameVi = req.NameVi ?? "", NameEn = req.NameEn ?? "",
            };

            await _db.TblDmchucVus.AddAsync(position);
            await _db.SaveChangesAsync();
        }

        public async Task UpdatePosition(TblDmchucVu data, UpdatePositionRequest req)
        {
            data.Code = req.Code ?? data.Code;
            data.NameVi = req.NameVi ?? data.NameVi;
            data.NameEn = req.NameEn ?? data.NameEn;

            await _db.SaveChangesAsync();
        }

        public async Task DeletePosition(TblDmchucVu data)
        {
            _db.TblDmchucVus.Remove(data);
            await _db.SaveChangesAsync();
        }

        public async Task<List<DepartmentDto>?> GetAllDepartments()
        {
            List<DepartmentDto>? data = await _db.TblDmphongBans.Select(x => new DepartmentDto()
            {
                Id = x.Id, NameVI = x.NameVi ?? "", NameEN = x.NameEn ?? "", IdVanPhong = x.IddmvanPhong ?? -1,
                GhiChu = x.GhiChu ?? "", FlagFavorite = x.FlagFavorite ?? false,
            }).ToListAsync();

            return data;
        }

        public async Task<List<DepartmentDto>?> GetDepartments(int Start = 0, int Size = 10, string Search = "")
        {
            List<DepartmentDto>? data;

            if (Search == "")
            {
                data = await _db.TblDmphongBans.Select(x => new DepartmentDto()
                {
                    Id = x.Id, NameVI = x.NameVi ?? "", NameEN = x.NameEn ?? "", IdVanPhong = x.IddmvanPhong ?? -1,
                    GhiChu = x.GhiChu ?? "", FlagFavorite = x.FlagFavorite ?? false,
                }).OrderByDescending(x => x.Id).Skip(Start).Take(Size).ToListAsync();
            } else
            {
                data = await _db.TblDmphongBans.Select(x => new DepartmentDto()
                {
                    Id = x.Id, NameVI = x.NameVi ?? "", NameEN = x.NameEn ?? "", IdVanPhong = x.IddmvanPhong ?? -1,
                    GhiChu = x.GhiChu ?? "", FlagFavorite = x.FlagFavorite ?? false,
                }).OrderByDescending(x => x.Id).Skip(Start).Take(Size)
                                               .Where(
                                                   x => (x.NameVI != null && x.NameVI.Contains(Search)) ||
                                                        (x.NameEN != null && x.NameEN.Contains(Search)) ||
                                                        (x.GhiChu != null && x.GhiChu.Contains(Search))
                                               ).ToListAsync();
            }

            return data;
        }

        public async Task<int> GetTotalDepartments(string Search = "")
        {
            var total = 0;

            if (Search == "")
            {
                total = await _db.TblDmphongBans.Select(x => new DepartmentDto()
                {
                    Id = x.Id, NameVI = x.NameVi ?? "", NameEN = x.NameEn ?? "", IdVanPhong = x.IddmvanPhong ?? -1,
                    GhiChu = x.GhiChu ?? "", FlagFavorite = x.FlagFavorite ?? false,
                }).CountAsync();
            } else
            {
                total = await _db.TblDmphongBans.Select(x => new DepartmentDto()
                {
                    Id = x.Id, NameVI = x.NameVi ?? "", NameEN = x.NameEn ?? "", IdVanPhong = x.IddmvanPhong ?? -1,
                    GhiChu = x.GhiChu ?? "", FlagFavorite = x.FlagFavorite ?? false,
                })
                                                .Where(
                                                    x => (x.NameVI != null && x.NameVI.Contains(Search)) ||
                                                         (x.NameEN != null && x.NameEN.Contains(Search)) ||
                                                         (x.GhiChu != null && x.GhiChu.Contains(Search))
                                                ).CountAsync();
            }

            return total;
        }

        public async Task<TblDmphongBan?> GetDepartmentById(long id)
        {
            TblDmphongBan? data = await _db.TblDmphongBans.Where(x => x.Id == id).FirstOrDefaultAsync();
            return data;
        }

        public async Task CreateDepartment(CreateDepartmentRequest req)
        {
            var department = new TblDmphongBan()
            {
                NameVi = req.NameVi ?? "", NameEn = req.NameEn ?? "", IddmvanPhong = req.IdVanPhong,
            };

            await _db.TblDmphongBans.AddAsync(department);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateDepartment(TblDmphongBan data, UpdateDepartmentRequest req)
        {
            data.NameVi = req.NameVi ?? data.NameVi;
            data.NameEn = req.NameEn ?? data.NameEn;
            data.IddmvanPhong = req.IdVanPhong ?? data.IddmvanPhong;

            await _db.SaveChangesAsync();
        }
        public async Task DeleteDepartment(TblDmphongBan data)
        {
            _db.TblDmphongBans.Remove(data);
            await _db.SaveChangesAsync();
        }

        public async Task<List<OfficeDto>?> GetAllOffices()
        {
            List<OfficeDto>? data = await _db.TblDmvanPhongs.Select(x => new OfficeDto()
            {
                Id = x.Id, NameVI = x.NameVi ?? "", NameEN = x.NameEn ?? "", Code = x.Code ?? "",
                IdCountry = x.Idcountry, IdCity = x.Idcity, Fax = x.Fax ?? "", AddressVI = x.AddressVi ?? "",
                QuocGia = x.Idcountry != null ? _db.TblDmcountries.Where(c => c.Id == x.Idcountry).Select(x => x.NameVi).FirstOrDefault() : "",
                ThanhPho = x.Idcity != null ? _db.TblDmcities.Where(c => c.Id == x.Idcity).Select(x => x.NameVi).FirstOrDefault() : "",
                AddressEN = x.AddressEn ?? "", Email = x.Email ?? "", Note = x.Note ?? "",
                Phone = x.Phone ?? "", TaxCode = x.TaxCode ?? "", Website = x.Website ?? "",
                FlagFavorite = x.FlagFavorite ?? false,
            }).ToListAsync();

            return data;
        }

        public async Task<List<OfficeDto>?> GetOffices(int Start = 0, int Size = 10, string Search = "")
        {
            List<OfficeDto>? data;

            if (Search == "")
            {
                data = await _db.TblDmvanPhongs.Select(x => new OfficeDto()
                {
                    Id = x.Id, NameVI = x.NameVi ?? "", NameEN = x.NameEn ?? "", Code = x.Code ?? "",
                    IdCountry = x.Idcountry, IdCity = x.Idcity, Fax = x.Fax ?? "", AddressVI = x.AddressVi ?? "",
                    AddressEN = x.AddressEn ?? "", Email = x.Email ?? "", Note = x.Note ?? "",
                    QuocGia = x.Idcountry != null ? _db.TblDmcountries.Where(c => c.Id == x.Idcountry).Select(x => x.NameVi).FirstOrDefault() : "",
                    ThanhPho = x.Idcity != null ? _db.TblDmcities.Where(c => c.Id == x.Idcity).Select(x => x.NameVi).FirstOrDefault() : "",
                    Phone = x.Phone ?? "", TaxCode = x.TaxCode ?? "", Website = x.Website ?? "",
                    FlagFavorite = x.FlagFavorite ?? false,
                }).OrderByDescending(x => x.Id).Skip(Start).Take(Size).ToListAsync();
            } else
            {
                data = await _db.TblDmvanPhongs.Select(x => new OfficeDto()
                {
                    Id = x.Id, NameVI = x.NameVi ?? "", NameEN = x.NameEn ?? "", Code = x.Code ?? "",
                    IdCountry = x.Idcountry, IdCity = x.Idcity, Fax = x.Fax ?? "", AddressVI = x.AddressVi ?? "",
                    AddressEN = x.AddressEn ?? "", Email = x.Email ?? "", Note = x.Note ?? "",
                    QuocGia = x.Idcountry != null ? _db.TblDmcountries.Where(c => c.Id == x.Idcountry).Select(x => x.NameVi).FirstOrDefault() : "",
                    ThanhPho = x.Idcity != null ? _db.TblDmcities.Where(c => c.Id == x.Idcity).Select(x => x.NameVi).FirstOrDefault() : "",
                    Phone = x.Phone ?? "", TaxCode = x.TaxCode ?? "", Website = x.Website ?? "",
                    FlagFavorite = x.FlagFavorite ?? false,
                }).OrderByDescending(x => x.Id).Skip(Start).Take(Size)
                                               .Where(
                                                   x => (x.NameVI != null && x.NameVI.Contains(Search)) || (x.NameEN != null && x.NameEN.Contains(Search)) ||
                                                        (x.Code != null && x.Code.Contains(Search)) || (x.AddressVI != null && x.AddressVI.Contains(Search)) ||
                                                        (x.AddressEN != null && x.AddressEN.Contains(Search)) || (x.Email != null && x.Email.Contains(Search)) ||
                                                        (x.Note != null && x.Note.Contains(Search)) || (x.Phone != null && x.Phone.Contains(Search)) ||
                                                        (x.TaxCode != null && x.TaxCode.Contains(Search)) || (x.Website != null && x.Website.Contains(Search))
                                               ).ToListAsync();
            }

            return data;
        }

        public async Task<int> GetTotalOffices(string Search = "")
        {
            var total = 0;

            if (Search == "")
            {
                total = await _db.TblDmvanPhongs.Select(x => new OfficeDto()
                {
                    Id = x.Id, NameVI = x.NameVi ?? "", NameEN = x.NameEn ?? "", Code = x.Code ?? "",
                    IdCountry = x.Idcountry, IdCity = x.Idcity, Fax = x.Fax ?? "", AddressVI = x.AddressVi ?? "",
                    QuocGia = x.Idcountry != null ? _db.TblDmcountries.Where(c => c.Id == x.Idcountry).Select(x => x.NameVi).FirstOrDefault() : "",
                    ThanhPho = x.Idcity != null ? _db.TblDmcities.Where(c => c.Id == x.Idcity).Select(x => x.NameVi).FirstOrDefault() : "",
                    AddressEN = x.AddressEn ?? "", Email = x.Email ?? "", Note = x.Note ?? "",
                    Phone = x.Phone ?? "", TaxCode = x.TaxCode ?? "", Website = x.Website ?? "",
                    FlagFavorite = x.FlagFavorite ?? false,
                }).CountAsync();
            } else
            {
                total = await _db.TblDmvanPhongs.Select(x => new OfficeDto()
                {
                    Id = x.Id, NameVI = x.NameVi ?? "", NameEN = x.NameEn ?? "", Code = x.Code ?? "",
                    IdCountry = x.Idcountry, IdCity = x.Idcity, Fax = x.Fax ?? "", AddressVI = x.AddressVi ?? "",
                    QuocGia = x.Idcountry != null ? _db.TblDmcountries.Where(c => c.Id == x.Idcountry).Select(x => x.NameVi).FirstOrDefault() : "",
                    ThanhPho = x.Idcity != null ? _db.TblDmcities.Where(c => c.Id == x.Idcity).Select(x => x.NameVi).FirstOrDefault() : "",
                    AddressEN = x.AddressEn ?? "", Email = x.Email ?? "", Note = x.Note ?? "",
                    Phone = x.Phone ?? "", TaxCode = x.TaxCode ?? "", Website = x.Website ?? "",
                    FlagFavorite = x.FlagFavorite ?? false,
                })
                                                .Where(
                                                    x => (x.NameVI != null && x.NameVI.Contains(Search)) || (x.NameEN != null && x.NameEN.Contains(Search)) ||
                                                         (x.Code != null && x.Code.Contains(Search)) || (x.AddressVI != null && x.AddressVI.Contains(Search)) ||
                                                         (x.AddressEN != null && x.AddressEN.Contains(Search)) || (x.Email != null && x.Email.Contains(Search)) ||
                                                         (x.Note != null && x.Note.Contains(Search)) || (x.Phone != null && x.Phone.Contains(Search)) ||
                                                         (x.TaxCode != null && x.TaxCode.Contains(Search)) || (x.Website != null && x.Website.Contains(Search))
                                                ).CountAsync();
            }

            return total;
        }

        public async Task<TblDmvanPhong?> GetOfficeById(long id)
        {
            TblDmvanPhong? data = await _db.TblDmvanPhongs.Where(x => x.Id == id).FirstOrDefaultAsync();
            return data;
        }

        public async Task CreateOffice(CreateOfficeRequest req)
        {
            var data = new TblDmvanPhong()
            {
                Code = req.Code, NameVi = req.NameVi, NameEn = req.NameEn ?? "",
                Idcity = (req.IdCity != null && req.IdCity != -1) ? req.IdCity : null,
                Idcountry = (req.IdCountry != null && req.IdCountry != -1) ? req.IdCountry : null,
                AddressVi = req.AddressVi ?? "", AddressEn = req.AddressEn ?? "",
                Phone = req.Phone ?? "", Fax = req.Fax ?? "", Email = req.Email ?? "",
                Website = req.Website ?? "", Note = req.Note ?? "", TaxCode = req.TaxCode ?? "",
            };

            await _db.TblDmvanPhongs.AddAsync(data);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateOffice(TblDmvanPhong data, UpdateOfficeRequest req)
        {
            data.Code = req.Code ?? data.Code;
            data.NameVi = req.NameVi ?? data.NameVi;
            data.NameEn = req.NameEn ?? data.NameEn;
            data.AddressVi = req.AddressVi ?? data.AddressVi;
            data.AddressEn = req.AddressEn ?? data.AddressEn;
            data.Phone = req.Phone ?? data.Phone;
            data.Fax = req.Fax ?? data.Fax;
            data.Email = req.Email ?? data.Email;
            data.Website = req.Website ?? data.Website;
            data.Note = req.Note ?? data.Note;
            data.TaxCode = req.TaxCode ?? data.TaxCode;
            data.Idcountry = (req.IdCountry != null && req.IdCountry != -1) ? req.IdCountry : data.Idcountry;
            data.Idcity = (req.IdCity != null && req.IdCity != -1) ? req.IdCity : data.Idcity;

            await _db.SaveChangesAsync();
        }

        public async Task DeleteOffice(TblDmvanPhong data)
        {
            _db.TblDmvanPhongs.Remove(data);
            await _db.SaveChangesAsync();
        }

        public async Task<List<BusinessDto>?> GetAllBusinesses()
        {
            List<BusinessDto>? data = await _db.TblDmloaiDoanhNghieps.Select(x => new BusinessDto()
            {
                Id = x.Id, Code = x.Code ?? "", NameVI = x.NameVi ?? "",
                NameEN = x.NameEn ?? "",
            }).ToListAsync();

            return data;
        }

        public async Task<List<BusinessDto>?> GetBusinesses(int Start = 0, int Size = 10, string Search = "")
        {
            List<BusinessDto>? data;

            if (Search == "")
            {
                data = await _db.TblDmloaiDoanhNghieps.Select(x => new BusinessDto()
                {
                    Id = x.Id, Code = x.Code ?? "", NameVI = x.NameVi ?? "",
                    NameEN = x.NameEn ?? "",
                }).OrderByDescending(x => x.Id).Skip(Start).Take(Size)
                                                      .ToListAsync();
            } else
            {
                data = await _db.TblDmloaiDoanhNghieps.Select(x => new BusinessDto()
                {
                    Id = x.Id, Code = x.Code ?? "", NameVI = x.NameVi ?? "",
                    NameEN = x.NameEn ?? "",
                }).OrderByDescending(x => x.Id).Skip(Start).Take(Size)
                                                      .Where(x => (x.Code != null && x.Code.Contains(Search)) ||
                                                                  (x.NameVI != null && x.NameVI.Contains(Search)) ||
                                                                  (x.NameEN != null && x.NameEN.Contains(Search))
                                                      )
                                                      .ToListAsync();
            }

            return data;
        }
        public async Task<int> GetTotalBusinesses(string Search)
        {
            var total = 0;

            if (Search == "")
            {
                total = await _db.TblDmloaiDoanhNghieps.Select(x => new BusinessDto()
                {
                    Id = x.Id, Code = x.Code ?? "", NameVI = x.NameVi ?? "",
                    NameEN = x.NameEn ?? "",
                }).CountAsync();
            } else
            {
                total = await _db.TblDmloaiDoanhNghieps.Select(x => new BusinessDto()
                {
                    Id = x.Id, Code = x.Code ?? "", NameVI = x.NameVi ?? "",
                    NameEN = x.NameEn ?? "",
                })
                                                       .Where(x => (x.Code != null && x.Code.Contains(Search)) ||
                                                                   (x.NameVI != null && x.NameVI.Contains(Search)) ||
                                                                   (x.NameEN != null && x.NameEN.Contains(Search))
                                                       ).CountAsync();
            }

            return total;
        }

        public async Task<TblDmloaiDoanhNghiep?> GetBusinessById(long id)
        {
            TblDmloaiDoanhNghiep? data = await _db.TblDmloaiDoanhNghieps.Where(x => x.Id == id).FirstOrDefaultAsync();

            return data;
        }

        public async Task CreateBusiness(CreateBusinessRequest req)
        {
            var data = new TblDmloaiDoanhNghiep()
            {
                Code = req.Code ?? "",
                NameVi = req.NameVI ?? "",
                NameEn = req.NameEN ?? "",
            };

            await _db.TblDmloaiDoanhNghieps.AddAsync(data);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateBusiness(TblDmloaiDoanhNghiep data, UpdateBusinessRequest req)
        {
            data.Code = req.Code ?? data.Code;
            data.NameVi = req.NameVI ?? data.NameVi;
            data.NameEn = req.NameEN ?? data.NameEn;

            await _db.SaveChangesAsync();
        }

        public async Task DeleteBusiness(TblDmloaiDoanhNghiep data)
        {
            _db.TblDmloaiDoanhNghieps.Remove(data);
            await _db.SaveChangesAsync();
        }

        public async Task<List<TransportationDto>?> GetAllTransportations()
        {
            List<TransportationDto>? data =  await _db.TblDmloaiHinhVanChuyens.Select(x => new TransportationDto()
                                                      {
                                                          Id = x.Id, Code = x.Code ?? "", NameVI = x.NameVi ?? "", NameEN = x.NameEn ?? ""
                                                      }).ToListAsync();;

            return data;
        }

        public async Task<List<TransportationDto>?> GetTransportations(int Start = 0, int Size = 10, string Search = "")
        {
            List<TransportationDto>? data;

            if (Search == "") {
                data = await _db.TblDmloaiHinhVanChuyens.Select(x => new TransportationDto()
                {
                    Id = x.Id, Code = x.Code ?? "", NameVI = x.NameVi ?? "", NameEN = x.NameEn ?? ""
                }).OrderByDescending(x => x.Id).Skip(Start).Take(Size).ToListAsync();
            } else
            {
                data = await _db.TblDmloaiHinhVanChuyens.Select(x => new TransportationDto()
                {
                    Id = x.Id, Code = x.Code ?? "", NameVI = x.NameVi ?? "", NameEN = x.NameEn ?? ""
                })
                                                        .Where(x =>
                                                            (x.Code != null && x.Code.Contains(Search)) || (x.NameVI != null && x.NameVI.Contains(Search)) ||
                                                            (x.NameEN != null && x.NameEN.Contains(Search))
                                                        )
                                                        .OrderByDescending(x => x.Id).Skip(Start).Take(Size).ToListAsync();
            }

            return data;
        }

        public async Task<int> GetTotalTransportations(string Search)
        {
            var total = 0;

            if (Search == "") {
                total = await _db.TblDmloaiHinhVanChuyens.Select(x => new TransportationDto()
                {
                    Id = x.Id, Code = x.Code ?? "", NameVI = x.NameVi ?? "", NameEN = x.NameEn ?? ""
                }).CountAsync();
            } else
            {
                total = await _db.TblDmloaiHinhVanChuyens.Select(x => new TransportationDto()
                {
                    Id = x.Id, Code = x.Code ?? "", NameVI = x.NameVi ?? "", NameEN = x.NameEn ?? ""
                })
                                                         .Where(x =>
                                                             (x.Code != null && x.Code.Contains(Search)) || (x.NameVI != null && x.NameVI.Contains(Search)) ||
                                                             (x.NameEN != null && x.NameEN.Contains(Search))
                                                         )
                                                         .CountAsync();
            }

            return total;
        }

        public async Task<TblDmloaiHinhVanChuyen?> GetTransportationById(long id)
        {
            TblDmloaiHinhVanChuyen? data = await _db.TblDmloaiHinhVanChuyens.Where(x => x.Id == id).FirstOrDefaultAsync();
            return data;
        }

        public async Task CreateTransportation(CreateTransportationRequest req)
        {
            var data = new TblDmloaiHinhVanChuyen()
            {
                Code = req.Code, NameVi = req.NameVI, NameEn = req.NameEN ?? ""
            };

            await _db.TblDmloaiHinhVanChuyens.AddAsync(data);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateTransportation(TblDmloaiHinhVanChuyen data, UpdateTransportationRequest req)
        {
            data.Code = req.Code ?? data.Code;
            data.NameVi = req.NameVI ?? data.NameVi;
            data.NameEn = req.NameEN ?? data.NameEn;

            await _db.SaveChangesAsync();
        }

        public async Task DeleteTransportation(TblDmloaiHinhVanChuyen data)
        {
            _db.TblDmloaiHinhVanChuyens.Remove(data);
            await _db.SaveChangesAsync();
        }

        public async Task<List<OperationalDto>?> GetAllOperationals()
        {
            List<OperationalDto>? data =  await _db.TblDmloaiTacNghieps.Select(x => new OperationalDto()
                {
                    Id = x.Id, Name = x.Name ?? "", R = x.R, G = x.G, B = x.B, NgayTuTraKhach = x.NgayTuTraKhac
                }).ToListAsync();
            return data;
        }

        public async Task<List<OperationalDto>?> GetOperationals(int Start = 0, int Size = 10, string Search = "")
        {
            List<OperationalDto>? data;

            if (Search == "")
            {
                data = await _db.TblDmloaiTacNghieps.Select(x => new OperationalDto()
                {
                    Id = x.Id, Name = x.Name ?? "", R = x.R, G = x.G, B = x.B, NgayTuTraKhach = x.NgayTuTraKhac
                }).OrderByDescending(x => x.Id).Skip(Start).Take(Size).ToListAsync();
            } else
            {
                data = await _db.TblDmloaiTacNghieps.Select(x => new OperationalDto()
                {
                    Id = x.Id, Name = x.Name ?? "", R = x.R, G = x.G, B = x.B, NgayTuTraKhach = x.NgayTuTraKhac
                }).OrderByDescending(x => x.Id).Skip(Start).Take(Size)
                                                    .Where(x => x.Name != null && x.Name.Contains(Search)).ToListAsync();
            }

            return data;
        }

        public async Task<int> GetTotalOperationals(string Search)
        {
            var total = 0;

            if (Search == "")
            {
                total = await _db.TblDmloaiTacNghieps.Select(x => new OperationalDto()
                {
                    Id = x.Id, Name = x.Name ?? "", R = x.R, G = x.G, B = x.B, NgayTuTraKhach = x.NgayTuTraKhac
                }).OrderByDescending(x => x.Id).CountAsync();
            } else
            {
                total = await _db.TblDmloaiTacNghieps.Select(x => new OperationalDto()
                {
                    Id = x.Id, Name = x.Name ?? "", R = x.R, G = x.G, B = x.B, NgayTuTraKhach = x.NgayTuTraKhac
                }).Where(x => x.Name != null && x.Name.Contains(Search)).CountAsync();
            }

            return total;
        }

        public async Task<TblDmloaiTacNghiep?> GetOperationalById(long id)
        {
            TblDmloaiTacNghiep? data = await _db.TblDmloaiTacNghieps.Where(x => x.Id == id).FirstOrDefaultAsync();
            return data;
        }
        public async Task CreateOperational(CreateOperationalRequest req)
        {
            var data = new TblDmloaiTacNghiep()
            {
                Name = req.Name, R = req.R, G = req.G, B = req.B, NgayTuTraKhac = req.NgayTuTraKhach
            };

            await _db.TblDmloaiTacNghieps.AddAsync(data);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateOperational(TblDmloaiTacNghiep data, UpdateOperationalRequest req)
        {
            data.Name = req.Name ?? data.Name;
            data.R = req.R ?? data.R;
            data.G = req.G ?? data.G;
            data.B = req.B ?? data.B;
            data.NgayTuTraKhac = req.NgayTuTraKhach ?? data.NgayTuTraKhac;

            await _db.SaveChangesAsync();
        }
        public async Task DeleteOperational(TblDmloaiTacNghiep data)
        {
            _db.TblDmloaiTacNghieps.Remove(data);
            await _db.SaveChangesAsync();
        }

        public async Task<List<MajorDto>?> GetAllMajors()
        {
            List<MajorDto>? data = await _db.TblDmnghiepVus.Select(x => new MajorDto()
                                            {
                                                Id = x.Id, Code = x.Code ?? "", NameVI = x.NameVi ?? "", NameEN = x.NameEn ?? ""
                                            }).ToListAsync();

            return data;
        }

        public async Task<List<MajorDto>?> GetMajors(int Start = 0, int Size = 10, string Search = "")
        {
            List<MajorDto>? data;

            if(Search == "")
            {
                data = await _db.TblDmnghiepVus.Select(x => new MajorDto()
                                               {
                                                   Id = x.Id, Code = x.Code ?? "", NameVI = x.NameVi ?? "", NameEN = x.NameEn ?? ""
                                               }).OrderByDescending(x => x.Id).Skip(Start).Take(Size).ToListAsync();
            } else
            {
                data = await _db.TblDmnghiepVus.Select(x => new MajorDto()
                                               {
                                                   Id = x.Id, Code = x.Code ?? "", NameVI = x.NameVi ?? "", NameEN = x.NameEn ?? ""
                                               }).OrderByDescending(x => x.Id).Skip(Start).Take(Size)
                                               .Where(
                                                   x => (x.Code != null && x.Code.Contains(Search)) ||
                                                   (x.NameVI != null && x.NameVI.Contains(Search)) || (x.NameEN != null && x.NameEN.Contains(Search))
                                               ).ToListAsync();
            }

            return data;
        }

        public async Task<int> GetTotalMajors(string Search)
        {
            var total = 0;

            if(Search == "")
            {
                total = await _db.TblDmnghiepVus.Select(x => new MajorDto()
                                                {
                                                    Id = x.Id, Code = x.Code ?? "", NameVI = x.NameVi ?? "", NameEN = x.NameEn ?? ""
                                                }).CountAsync();
            } else
            {
                total = await _db.TblDmnghiepVus.Select(x => new MajorDto()
                                                {
                                                    Id = x.Id, Code = x.Code ?? "", NameVI = x.NameVi ?? "", NameEN = x.NameEn ?? ""
                                                })
                                                .Where(
                                                    x => (x.Code != null && x.Code.Contains(Search)) ||
                                                    (x.NameVI != null && x.NameVI.Contains(Search)) || (x.NameEN != null && x.NameEN.Contains(Search))
                                                ).CountAsync();
            }

            return total;
        }

        public async Task<TblDmnghiepVu?> GetMajorById(long id)
        {
            TblDmnghiepVu? data = await _db.TblDmnghiepVus.Where(x => x.Id == id).FirstOrDefaultAsync();
            return data;
        }

        public async Task CreateMajor(CreateMajorRequest req)
        {
            var data = new TblDmnghiepVu()
            {
                Code = req.Code, NameVi = req.NameVI, NameEn = req.NameEN ?? ""
            };

            await _db.TblDmnghiepVus.AddAsync(data);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateMajor(TblDmnghiepVu data, UpdateMajorRequest req)
        {
            data.Code = req.Code ?? data.Code;
            data.NameVi = req.NameVI ?? data.NameVi;
            data.NameEn = req.NameEN ?? data.NameEn;

            await _db.SaveChangesAsync();
        }

        public async Task DeleteMajor(TblDmnghiepVu data)
        {
            _db.TblDmnghiepVus.Remove(data);
            await _db.SaveChangesAsync();
        }

        public async Task<List<TypeOfCustomerDto>?> GetAllTypeOfCustomers()
        {
            List<TypeOfCustomerDto>? data = await _db.TblDmphanLoaiKhachHangs.Select(x => new TypeOfCustomerDto()
                                                     {
                                                         Id = x.Id, Code = x.Code ?? "", NameVI = x.NameVi ?? "", NameEN = x.NameEn ?? ""
                                                     }).ToListAsync();;
            return data;
        }

        public async Task<List<TypeOfCustomerDto>?> GetTypeOfCustomers(int Start = 0, int Size = 10, string Search = "")
        {
            List<TypeOfCustomerDto>? data;

            if(Search == "")
            {
                data = await _db.TblDmphanLoaiKhachHangs.Select(x => new TypeOfCustomerDto()
                                               {
                                                   Id = x.Id, Code = x.Code ?? "", NameVI = x.NameVi ?? "", NameEN = x.NameEn ?? ""
                                               }).OrderByDescending(x => x.Id).Skip(Start).Take(Size).ToListAsync();
            } else
            {
                data = await _db.TblDmphanLoaiKhachHangs.Select(x => new TypeOfCustomerDto()
                                               {
                                                   Id = x.Id, Code = x.Code ?? "", NameVI = x.NameVi ?? "", NameEN = x.NameEn ?? ""
                                               }).OrderByDescending(x => x.Id).Skip(Start).Take(Size)
                                               .Where(
                                                   x => (x.Code != null && x.Code.Contains(Search)) ||
                                                   (x.NameVI != null && x.NameVI.Contains(Search)) || (x.NameEN != null && x.NameEN.Contains(Search))
                                               ).ToListAsync();
            }

            return data;
        }

        public async Task<int> GetTotalTypeOfCustomers(string Search)
        {
            var total = 0;

            if(Search == "")
            {
                total = await _db.TblDmphanLoaiKhachHangs.Select(x => new TypeOfCustomerDto()
                                                {
                                                    Id = x.Id, Code = x.Code ?? "", NameVI = x.NameVi ?? "", NameEN = x.NameEn ?? ""
                                                }).CountAsync();
            } else
            {
                total = await _db.TblDmphanLoaiKhachHangs.Select(x => new TypeOfCustomerDto()
                                                {
                                                    Id = x.Id, Code = x.Code ?? "", NameVI = x.NameVi ?? "", NameEN = x.NameEn ?? ""
                                                })
                                                .Where(
                                                    x => (x.Code != null && x.Code.Contains(Search)) ||
                                                    (x.NameVI != null && x.NameVI.Contains(Search)) || (x.NameEN != null && x.NameEN.Contains(Search))
                                                ).CountAsync();
            }

            return total;
        }

        public async Task<TblDmphanLoaiKhachHang?> GetTypeOfCustomerById(long id)
        {
            TblDmphanLoaiKhachHang? data = await _db.TblDmphanLoaiKhachHangs.Where(x => x.Id == id).FirstOrDefaultAsync();
            return data;
        }

        public async Task CreateTypeOfCustomer(CreateTypeOfCustomerRequest req)
        {
            var data = new TblDmphanLoaiKhachHang()
            {
                Code = req.Code, NameVi = req.NameVI, NameEn = req.NameEN ?? ""
            };

            await _db.TblDmphanLoaiKhachHangs.AddAsync(data);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateTypeOfCustomer(TblDmphanLoaiKhachHang data, UpdateTypeOfCustomerRequest req)
        {
            data.Code = req.Code ?? data.Code;
            data.NameVi = req.NameVI ?? data.NameVi;
            data.NameEn = req.NameEN ?? data.NameEn;

            await _db.SaveChangesAsync();
        }

        public async Task DeleteTypeOfCustomer(TblDmphanLoaiKhachHang data)
        {
            _db.TblDmphanLoaiKhachHangs.Remove(data);
            await _db.SaveChangesAsync();
        }

        public async Task<List<CustomerTypeDto>?> GetCustomerTypes(int Start = 0, int Size = 10, string Search = "")
        {
            List<CustomerTypeDto>? data;

            if(Search == "")
            {
                data = await _db.TblDmcustomerTypes.Select(x => new CustomerTypeDto()
                                               {
                                                   Id = x.Id, Code = x.Code ?? "", NameVI = x.NameVi ?? "", NameEN = x.NameEn ?? ""
                                               }).OrderByDescending(x => x.Id).Skip(Start).Take(Size).ToListAsync();
            } else
            {
                data = await _db.TblDmcustomerTypes.Select(x => new CustomerTypeDto()
                                               {
                                                   Id = x.Id, Code = x.Code ?? "", NameVI = x.NameVi ?? "", NameEN = x.NameEn ?? ""
                                               }).OrderByDescending(x => x.Id).Skip(Start).Take(Size)
                                               .Where(
                                                   x => (x.Code != null && x.Code.Contains(Search)) ||
                                                   (x.NameVI != null && x.NameVI.Contains(Search)) || (x.NameEN != null && x.NameEN.Contains(Search))
                                               ).ToListAsync();
            }

            return data;
        }

        public async Task<int> GetTotalCustomerTypes(string Search)
        {
            var total = 0;

            if(Search == "")
            {
                total = await _db.TblDmcustomerTypes.Select(x => new CustomerTypeDto()
                                                {
                                                    Id = x.Id, Code = x.Code ?? "", NameVI = x.NameVi ?? "", NameEN = x.NameEn ?? ""
                                                }).CountAsync();
            } else
            {
                total = await _db.TblDmphanLoaiKhachHangs.Select(x => new CustomerTypeDto()
                                                {
                                                    Id = x.Id, Code = x.Code ?? "", NameVI = x.NameVi ?? "", NameEN = x.NameEn ?? ""
                                                })
                                                .Where(
                                                    x => (x.Code != null && x.Code.Contains(Search)) ||
                                                    (x.NameVI != null && x.NameVI.Contains(Search)) || (x.NameEN != null && x.NameEN.Contains(Search))
                                                ).CountAsync();
            }

            return total;
        }
        
        public async Task<TblDmcustomerType?> GetCustomerTypeById(long id)
        {
            TblDmcustomerType? data = await _db.TblDmcustomerTypes.Where(x => x.Id == id).FirstOrDefaultAsync();
            return data;
        }
        
        public async Task CreateCustomerType(CreateCustomerTypeRequest req)
        {
            var data = new TblDmcustomerType()
            {
                Code = req.Code,
                NameVi = req.NameVI,
                NameEn = req.NameEN ?? ""
            };

            await _db.TblDmcustomerTypes.AddAsync(data);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateCustomerType(TblDmcustomerType data, UpdateCustomerTypeRequest req)
        {
            data.Code = req.Code ?? data.Code;
            data.NameVi = req.NameVI ?? data.NameVi;
            data.NameEn = req.NameEN ?? data.NameEn;

            await _db.SaveChangesAsync();
        }

        public async Task DeleteCustomerType(TblDmcustomerType data)
        {
            _db.TblDmcustomerTypes.Remove(data);
            await _db.SaveChangesAsync();
        }
    }
}
