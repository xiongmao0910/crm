// ** library **
using Microsoft.EntityFrameworkCore;
// ** architecture **
using locy_api.Data;
using locy_api.Interfaces;
using locy_api.Models.DTOs;
using locy_api.Models.Requests;
using locy_api.Models.Domains;

namespace locy_api.Services
{
    public class CityService: ICityService
    {
        private readonly BestCareDbContext _db;

        public CityService(BestCareDbContext db)
        {
            _db = db;
        }

        public async Task<List<CityDto>?> GetAllCities()
        {
            List<CityDto>? data = await _db.TblDmcities.Select(c => new CityDto()
                                           {
                                              Id = c.Id,Code = c.Code ?? "", IdQuocGia= c.IdquocGia, NameVI = c.NameVi ?? "",
                                              NameEN = c.NameEn ?? "",FlagFavorite = c.FlagFavorite, Note = c.Note
                                           }).ToListAsync();

            return data;
        }

        public async Task<List<CityDto>?> GetCitiesByIdCountry(long idQuocGia)
        {
            List<CityDto>? data = await _db.TblDmcities.Select(c => new CityDto()
                                           {
                                              Id = c.Id,Code = c.Code ?? "", IdQuocGia= c.IdquocGia, NameVI = c.NameVi ?? "",
                                              NameEN = c.NameEn ?? "", FlagFavorite = c.FlagFavorite, Note = c.Note
                                           }).Where(c => c.IdQuocGia == idQuocGia).ToListAsync();

            return data;
        }
        public async Task<TblDmcity?> GetCityById(long id)
        {
            TblDmcity? data = await _db.TblDmcities.Where(c => c.Id == id).FirstOrDefaultAsync();

            return data;
        }

        public async Task<List<CityDto>?> GetCities(int Start = 0, int Size = 10, string Search = "")
        {
            List<CityDto>? data;

            if(Search == "")
            {
                data = await _db.TblDmcities.Select(c => new CityDto()
                                {
                                    Id = c.Id,Code = c.Code ?? "", IdQuocGia= c.IdquocGia, NameVI = c.NameVi ?? "",
                                    NameEN = c.NameEn ?? "", FlagFavorite = c.FlagFavorite, Note = c.Note
                                }).OrderByDescending(x => x.Id).Skip(Start).Take(Size).ToListAsync();
            } else
            {
                data = await _db.TblDmcities.Select(c => new CityDto()
                                {
                                    Id = c.Id,Code = c.Code ?? "", IdQuocGia= c.IdquocGia, NameVI = c.NameVi ?? "",
                                    NameEN = c.NameEn ?? "", FlagFavorite = c.FlagFavorite, Note = c.Note
                                }).OrderByDescending(x => x.Id).Skip(Start).Take(Size)
                                .Where(x => (x.Code != null && x.Code.Contains(Search)) || (x.NameVI != null && x.NameVI.Contains(Search)) ||
                                            (x.NameEN != null && x.NameEN.Contains(Search)) || (x.Note != null && x.Note.Contains(Search))
                                )
                                .ToListAsync();                
            }

            return data;
        }

        public async Task<int> GetTotalCities(string Search)
        {
            var total = 0;

            if(Search == "")
            {
                total = await _db.TblDmcities.Select(c => new CityDto()
                                 {
                                     Id = c.Id,Code = c.Code ?? "", IdQuocGia= c.IdquocGia, NameVI = c.NameVi ?? "",
                                     NameEN = c.NameEn ?? "", FlagFavorite = c.FlagFavorite, Note = c.Note
                                 }).CountAsync();
            } else
            {
                total = await _db.TblDmcities.Select(c => new CityDto()
                                 {
                                     Id = c.Id,Code = c.Code ?? "", IdQuocGia= c.IdquocGia, NameVI = c.NameVi ?? "",
                                     NameEN = c.NameEn ?? "", FlagFavorite = c.FlagFavorite, Note = c.Note
                                 })
                                 .Where(x => (x.Code != null && x.Code.Contains(Search)) || (x.NameVI != null && x.NameVI.Contains(Search)) ||
                                             (x.NameEN != null && x.NameEN.Contains(Search)) || (x.Note != null && x.Note.Contains(Search))
                                 )
                                 .CountAsync();
            }

            return total;
        }

        public async Task CreateCity(CreateCityRequest req)
        {
            var data = new TblDmcity()
            {
                Code = req.Code ?? "", IdquocGia = req.IdQuocGia,
                NameVi = req.NameVI ?? "", NameEn = req.NameEN ?? "",
            };

            await _db.TblDmcities.AddAsync(data);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateCity(TblDmcity data, UpdateCityRequest req)
        {
            data.Code = req.Code ?? data.Code;
            data.IdquocGia = req.IdQuocGia ?? data.IdquocGia;
            data.NameVi = req.NameVI ?? data.NameVi;
            data.NameEn = req.NameEN ?? data.NameEn;

            await _db.SaveChangesAsync();
        }

        public async Task DeleteCity(TblDmcity data)
        {
            _db.TblDmcities.Remove(data);
            await _db.SaveChangesAsync();
        }
    }
}
