// ** library **
// ** architecture **
using locy_api.Models.Domains;
using locy_api.Models.DTOs;
using locy_api.Models.Requests;

namespace locy_api.Interfaces
{
    public interface IEmployeeService
    {
        Task<TblSysUser?> GetAccountById(long id);
        Task<TblNhanSu?> GetInfoById(long id);
        Task<List<ProfileDto>?> GetEmployees(int Start = 0, int Size = 10, string Search = "", bool isDelete = false);
        Task<int> GetTotalEmployees(string Search = "", bool isDelete = false);
        Task CreateEmployee(CreateEmployeeRequest req);
        Task UpdateEmployee(TblSysUser account, TblNhanSu info, UpdateEmployeeRequest req);
        Task DeleteEmployee(TblNhanSu info, DeleteEmployeeRequest req);
        Task<bool> IsUsernameExist(string username);
        Task<bool> IsPersonnelCodeExist(string code);
    }
}
