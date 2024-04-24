// ** library **
// ** architecture **
using locy_api.Models.Domains;
using locy_api.Models.DTOs;
using locy_api.Models.Requests;

namespace locy_api.Interfaces
{
    public interface ICountryService
    {
        Task<List<CountryDto>?> GetAllCountries();
        Task<TblDmcountry?> GetCountryById(long id);
        Task<List<CountryDto>?> GetCountries(int Start = 0, int Size = 0, string Search = "");
        Task<int> GetTotalCountries(string Search = "");
        Task CreateCountry(CreateCountryRequest req);
        Task UpdateCountry(TblDmcountry data, UpdateCountryRequest req);
        Task DeleteCountry(TblDmcountry data);
    }
}
