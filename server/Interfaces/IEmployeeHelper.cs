using locy_api.Models.DTOs;

namespace locy_api.Interfaces
{
    public interface IEmployeeHelper
    {
        Task<List<long>?> GetListEmployee(long idNhanVien = 0);
        Task<List<EmployeeDto>?> GetAllEmployees();
        Task<List<EmployeeDto>?> GetAllEmployeesGroup(List<long>? ids);
    }
}
