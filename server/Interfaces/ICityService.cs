// ** library **
// ** architecture **
using locy_api.Models.Domains;
using locy_api.Models.DTOs;
using locy_api.Models.Requests;

namespace locy_api.Interfaces
{
    public interface ICityService
    {
        Task<List<CityDto>?> GetAllCities();
        Task<List<CityDto>?> GetCitiesByIdCountry(long idQuocGia);
        Task<TblDmcity?> GetCityById(long id);
        Task<List<CityDto>?> GetCities(int Start = 0, int Size = 10, string Search = "");
        Task<int> GetTotalCities(string Search);
        Task CreateCity(CreateCityRequest req);
        Task UpdateCity(TblDmcity data, UpdateCityRequest req);
        Task DeleteCity(TblDmcity data);
    }
}
