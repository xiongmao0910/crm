using locy_api.Models.Domains;
using locy_api.Models.DTOs;
using locy_api.Models.Requests;

namespace locy_api.Interfaces
{
    public interface IPortService
    {
        Task<List<PortDto>?> GetPorts(int Start = 0, int Size = 0, string Search = "");
        Task<int> GetTotalPorts(string Search = "");
        Task<List<PortDto>?> GetPortsByIdCountry(long idCountry);
        Task<TblDmport?> GetPortById(long id);
        Task CreatePort(CreatePortRequest req);
        Task UpdatePort(TblDmport data, UpdatePortRequest req);
        Task DeletePort(TblDmport data);
    }
}
