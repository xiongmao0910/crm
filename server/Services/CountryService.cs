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
    public class CountryService: ICountryService
    {
        private readonly BestCareDbContext _db;

        public CountryService(BestCareDbContext db)
        {
            _db = db;
        }

        public async Task<List<CountryDto>?> GetAllCountries()
        {
            List<CountryDto>? data = await _db.TblDmcountries.Select(c => new CountryDto()
                                              {
                                                  Id = c.Id, Code = c.Code ?? "", NameVI = c.NameVi ?? "", NameEN = c.NameEn ?? "",
                                                  Note = c.Note ?? "", FlagFavorite = c.FlagFavorite ?? false
                                              }).ToListAsync();

            return data;
        }
        public async Task<TblDmcountry?> GetCountryById(long id)
        {
            TblDmcountry? data = await _db.TblDmcountries.Where(x => x.Id == id).FirstOrDefaultAsync();

            return data;
        }

        public async Task<List<CountryDto>?> GetCountries(int Start = 0, int Size = 0, string Search = "")
        {
            List<CountryDto>? data;

            if(Search == "")
            {
                data = await _db.TblDmcountries.Select(c => new CountryDto()
                                               {
                                                   Id = c.Id, Code = c.Code ?? "", NameVI = c.NameVi ?? "", NameEN = c.NameEn ?? "",
                                                   Note = c.Note ?? "", FlagFavorite = c.FlagFavorite ?? false
                                               }).OrderByDescending(x => x.Id).Skip(Start).Take(Size).ToListAsync();
            } else
            {
                data = await _db.TblDmcountries.Select(c => new CountryDto()
                                               {
                                                   Id = c.Id, Code = c.Code ?? "", NameVI = c.NameVi ?? "", NameEN = c.NameEn ?? "",
                                                   Note = c.Note ?? "", FlagFavorite = c.FlagFavorite ?? false
                                               })
                                               .Where(x => (x.Code != null && x.Code.Contains(Search)) || (x.NameVI != null && x.NameVI.Contains(Search)) ||
                                                           (x.NameEN != null && x.NameEN.Contains(Search)) || (x.Note != null && x.Note.Contains(Search))
                                               )
                                               .OrderByDescending(x => x.Id).Skip(Start).Take(Size).ToListAsync();
            }

            return data;
        }

        public async Task<int> GetTotalCountries(string Search = "")
        {
            var total = 0;

            if(Search == "")
            {
                total = await _db.TblDmcountries.Select(c => new CountryDto()
                                                {
                                                    Id = c.Id, Code = c.Code ?? "", NameVI = c.NameVi ?? "", NameEN = c.NameEn ?? "",
                                                    Note = c.Note ?? "", FlagFavorite = c.FlagFavorite ?? false
                                                }).OrderByDescending(x => x.Id).CountAsync();
            } else
            {
                total = await _db.TblDmcountries.Select(c => new CountryDto()
                                                {
                                                    Id = c.Id, Code = c.Code ?? "", NameVI = c.NameVi ?? "", NameEN = c.NameEn ?? "",
                                                    Note = c.Note ?? "", FlagFavorite = c.FlagFavorite ?? false
                                                })
                                                .Where(x => (x.Code != null && x.Code.Contains(Search)) || (x.NameVI != null && x.NameVI.Contains(Search)) ||
                                                            (x.NameEN != null && x.NameEN.Contains(Search)) || (x.Note != null && x.Note.Contains(Search))
                                                ).CountAsync();
            }

            return total;
        }

        public async Task CreateCountry(CreateCountryRequest req)
        {
            var data = new TblDmcountry()
            {
                Code = req.Code ?? "", NameVi = req.NameVi ?? "", NameEn = req.NameEn ?? ""
            };

            await _db.TblDmcountries.AddAsync(data);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateCountry(TblDmcountry data, UpdateCountryRequest req)
        {
            data.Code = req.Code ?? data.Code;
            data.NameVi = req.NameVi ?? data.NameVi;
            data.NameEn = req.NameEn ?? data.NameEn;
            
            await _db.SaveChangesAsync();
        }

        public async Task DeleteCountry(TblDmcountry data)
        {
            _db.TblDmcountries.Remove(data);
            await _db.SaveChangesAsync();
        }
    }
}
