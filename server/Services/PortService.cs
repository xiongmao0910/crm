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
    public class PortService: IPortService
    {
        private readonly BestCareDbContext _db;

        public PortService(BestCareDbContext db)
        {
            _db = db;
        }

        public async Task<List<PortDto>?> GetPorts(int Start = 0, int Size = 0, string Search = "")
        {
            List<PortDto>? data;

            if(Search == "")
            {
                data = await _db.TblDmports.Select(x => new PortDto()
                                {
                                    Id = x.Id, IdQuocGia = x.IdquocGia, IdCity = x.Idcity,
                                    QuocGia = _db.TblDmcountries.Where(c => x.IdquocGia != null && c.Id == x.IdquocGia).Select(c => c.NameVi).FirstOrDefault() ?? "",
                                    ThanhPho = _db.TblDmcities.Where(c => x.Idcity != null && c.Id == x.Idcity).Select(c => c.NameVi).FirstOrDefault() ?? "",
                                    Code = x.Code ?? "", TaxCode = x.TaxCode ?? "",
                                    NameVI = x.NameVi ?? "", NameEN = x.NameEn ?? "",
                                    AddressVI = x.AddressVi ?? "", AddressEN = x.AddressEn ?? "",
                                    Phone = x.Phone ?? "", Fax = x.Fax ?? "", Email = x.Email ?? "",
                                    Website = x.Website ?? "", Note = x.Note ?? ""
                                }).OrderByDescending(x => x.Id)
                                .Skip(Start).Take(Size).ToListAsync();
            } else
            {
                data = await _db.TblDmports.Select(x => new PortDto()
                                {
                                    Id = x.Id, IdQuocGia = x.IdquocGia, IdCity = x.Idcity,
                                    QuocGia = _db.TblDmcountries.Where(c => x.IdquocGia != null && c.Id == x.IdquocGia).Select(c => c.NameVi).FirstOrDefault() ?? "",
                                    ThanhPho = _db.TblDmcities.Where(c => x.Idcity != null && c.Id == x.Idcity).Select(c => c.NameVi).FirstOrDefault() ?? "",
                                    Code = x.Code ?? "", TaxCode = x.TaxCode ?? "", NameVI = x.NameVi ?? "", NameEN = x.NameEn ?? "",
                                    AddressVI = x.AddressVi ?? "", AddressEN = x.AddressEn ?? "", Phone = x.Phone ?? "", Fax = x.Fax ?? "", Email = x.Email ?? "",
                                    Website = x.Website ?? "", Note = x.Note ?? ""
                                }).OrderByDescending(x => x.Id)
                                .Where(x => 
                                            (x.QuocGia != null && x.QuocGia.Contains(Search)) || (x.ThanhPho != null && x.ThanhPho.Contains(Search)) ||
                                            (x.Code != null && x.Code.Contains(Search)) || (x.TaxCode != null && x.TaxCode.Contains(Search)) ||
                                            (x.NameVI != null && x.NameVI.Contains(Search)) || (x.NameEN != null && x.NameEN.Contains(Search)) ||
                                            (x.AddressVI != null && x.AddressVI.Contains(Search)) || (x.AddressEN != null && x.AddressEN.Contains(Search)) ||
                                            (x.Email != null && x.Email.Contains(Search)) || (x.Website != null && x.Website.Contains(Search))
                                )
                                .Skip(Start).Take(Size).ToListAsync();
            }

            return data;
        }

        public async Task<int> GetTotalPorts(string Search = "")
        {
            var total = 0;

            if(Search == "")
            {
                total = await _db.TblDmports.Select(x => new PortDto()
                                 {
                                     Id = x.Id, IdQuocGia = x.IdquocGia, IdCity = x.Idcity,
                                     QuocGia = _db.TblDmcountries.Where(c => x.IdquocGia != null && c.Id == x.IdquocGia).Select(c => c.NameVi).FirstOrDefault() ?? "",
                                     ThanhPho = _db.TblDmcities.Where(c => x.Idcity != null && c.Id == x.Idcity).Select(c => c.NameVi).FirstOrDefault() ?? "",
                                     Code = x.Code ?? "", TaxCode = x.TaxCode ?? "",
                                     NameVI = x.NameVi ?? "", NameEN = x.NameEn ?? "",
                                     AddressVI = x.AddressVi ?? "", AddressEN = x.AddressEn ?? "",
                                     Phone = x.Phone ?? "", Fax = x.Fax ?? "", Email = x.Email ?? "",
                                     Website = x.Website ?? "", Note = x.Note ?? ""
                                 }).CountAsync();
            } else
            {
                total = await _db.TblDmports.Select(x => new PortDto()
                                 {
                                     Id = x.Id, IdQuocGia = x.IdquocGia, IdCity = x.Idcity,
                                     QuocGia = _db.TblDmcountries.Where(c => x.IdquocGia != null && c.Id == x.IdquocGia).Select(c => c.NameVi).FirstOrDefault() ?? "",
                                     ThanhPho = _db.TblDmcities.Where(c => x.Idcity != null && c.Id == x.Idcity).Select(c => c.NameVi).FirstOrDefault() ?? "",
                                     Code = x.Code ?? "", TaxCode = x.TaxCode ?? "", NameVI = x.NameVi ?? "", NameEN = x.NameEn ?? "",
                                     AddressVI = x.AddressVi ?? "", AddressEN = x.AddressEn ?? "", Phone = x.Phone ?? "", Fax = x.Fax ?? "", Email = x.Email ?? "",
                                     Website = x.Website ?? "", Note = x.Note ?? ""
                                 }).OrderByDescending(x => x.Id)
                                 .Where(x => 
                                             (x.QuocGia != null && x.QuocGia.Contains(Search)) || (x.ThanhPho != null && x.ThanhPho.Contains(Search)) ||
                                             (x.Code != null && x.Code.Contains(Search)) || (x.TaxCode != null && x.TaxCode.Contains(Search)) ||
                                             (x.NameVI != null && x.NameVI.Contains(Search)) || (x.NameEN != null && x.NameEN.Contains(Search)) ||
                                             (x.AddressVI != null && x.AddressVI.Contains(Search)) || (x.AddressEN != null && x.AddressEN.Contains(Search)) ||
                                             (x.Email != null && x.Email.Contains(Search)) || (x.Website != null && x.Website.Contains(Search))
                                 )
                                 .CountAsync();
            }

            return total;
        }

        public async Task<List<PortDto>?> GetPortsByIdCountry(long idCountry)
        {
            List<PortDto>? data = await _db.TblDmports.Select(x => new PortDto()
                                                 {
                                                     Id = x.Id, IdQuocGia = x.IdquocGia, IdCity = x.Idcity,
                                                     QuocGia = _db.TblDmcountries.Where(c => x.IdquocGia != null && c.Id == x.IdquocGia).Select(c => c.NameVi).FirstOrDefault() ?? "",
                                                     ThanhPho = _db.TblDmcities.Where(c => x.Idcity != null && c.Id == x.Idcity).Select(c => c.NameVi).FirstOrDefault() ?? "",
                                                     Code = x.Code ?? "", TaxCode = x.TaxCode ?? "",
                                                     NameVI = x.NameVi ?? "", NameEN = x.NameEn ?? "",
                                                     AddressVI = x.AddressVi ?? "", AddressEN = x.AddressEn ?? "",
                                                     Phone = x.Phone ?? "", Fax = x.Fax ?? "", Email = x.Email ?? "",
                                                     Website = x.Website ?? "", Note = x.Note ?? ""
                                                 }).Where(x => x.IdQuocGia == idCountry).ToListAsync();

            return data;
        }

        public async Task<TblDmport?> GetPortById(long id)
        {
            TblDmport? data = await _db.TblDmports.Where(x => x.Id == id).FirstOrDefaultAsync();

            return data;
        }
        public async Task CreatePort(CreatePortRequest req)
        {
            var data = new TblDmport()
            {
                IdquocGia = req.IdQuocGia != -1 ? req.IdQuocGia : null, Idcity = req.IdCity != -1 ? req.IdCity : null,
                Code = req.Code ?? "", TaxCode = req.TaxCode ?? "", NameVi = req.NameVi ?? "", NameEn = req.NameEn ?? "",
                AddressVi = req.AddressVi ?? "", Note = req.Note ?? "", Phone = req.Phone ?? "", Fax = req.Fax ?? "",
                Email = req.Email ?? "", Website = req.Website ?? "",
            };

            await _db.TblDmports.AddAsync(data);
            await _db.SaveChangesAsync();
        }
        
        public async Task UpdatePort(TblDmport data, UpdatePortRequest req)
        {
            data.IdquocGia = req.IdQuocGia ?? data.IdquocGia;
            data.Idcity = req.IdCity ?? data.Idcity;
            data.Code = req.Code ?? data.Code;
            data.TaxCode = req.TaxCode ?? data.TaxCode;
            data.NameVi = req.NameVi ?? data.NameVi;
            data.NameEn = req.NameEn ?? data.NameEn;
            data.AddressVi = req.AddressVi ?? data.AddressVi;
            data.AddressEn = req.AddressEn ?? data.AddressEn;
            data.Phone = req.Phone ?? data.Phone;
            data.Fax = req.Fax ?? data.Fax;
            data.Email = req.Email ?? data.Email;
            data.Website = req.Website ?? data.Website;
            data.Note = req.Note ?? data.Note;

            await _db.SaveChangesAsync();
        }
        
        public async Task DeletePort(TblDmport data)
        {
            _db.TblDmports.Remove(data);
            await _db.SaveChangesAsync();
        }
    }
}
