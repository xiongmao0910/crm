// ** library **
using Microsoft.EntityFrameworkCore;
// ** architecture **
using locy_api.Data;
using locy_api.Models.Domains;
using locy_api.Interfaces;
using locy_api.Models.DTOs;

namespace locy_api.Helpers
{

    public class EmployeeHelpers: IEmployeeHelper
    {
        private readonly BestCareDbContext _db;

        public EmployeeHelpers(BestCareDbContext dbContext)
        {
            _db = dbContext;
        }

        // Hàm lấy danh sách nhân viên quản lý
        public async Task<List<long>?> GetListEmployee(long idNhanVien = 0)
        {
            List<long>? employees = new List<long>();

            var ListEmployeeTreeList = await _db.TblNhanSuTreelists.Where(x => x.IdnhanVien != null).ToListAsync();

            if (ListEmployeeTreeList.Count > 0)
            {
                var EmployeeTreeList = ListEmployeeTreeList.FirstOrDefault(x => x.IdnhanVien == idNhanVien);

                if (EmployeeTreeList != null)
                {
                    var ListEmployeeParent = ListEmployeeTreeList.Where(x => x.ParentId == EmployeeTreeList.Id).ToList();

                    if (ListEmployeeParent.Count > 0) // Trưởng nhóm
                    {
                        GetEmployeeView(EmployeeTreeList, ListEmployeeTreeList, employees);
                    }
                }
            }

            return employees;
        }

        private void GetEmployeeView(TblNhanSuTreelist c, List<TblNhanSuTreelist> ListEmployeeTreeList, List<long> listEmployee)
        {
            if (c == null) return;

            var ListEmployeeParent = ListEmployeeTreeList.Where(x => x.ParentId == c.Id).ToList();

            if (ListEmployeeParent.Count > 0) // Trưởng nhóm
            {
                foreach (var employee in ListEmployeeParent)
                {
                    if (employee.IdnhanVien != null)
                    {
                        listEmployee.Add(employee.IdnhanVien.Value);
                    }

                    var _listEmployeeParent = ListEmployeeTreeList.Where(x => x.ParentId == employee.Id).ToList();
                    if (_listEmployeeParent.Count > 0)
                    {
                        GetEmployeeView(employee, ListEmployeeTreeList, listEmployee);
                    }
                }
            }
        }

        public async Task<List<EmployeeDto>?> GetAllEmployees()
        {
            List<EmployeeDto>? data = await _db.TblNhanSus.Where(x => x.FlagDelete != true).Select(x => new EmployeeDto()
                                               {
                                                   Id = x.Id, NameVI = x.HoTenVi ?? "", NameEN = x.HoTenEn ?? "",
                                               }).ToListAsync();
            return data;
        }

        public async Task<List<EmployeeDto>?> GetAllEmployeesGroup(List<long>? ids)
        {
            List<EmployeeDto>? dataList = await _db.TblNhanSus.Where(x => x.FlagDelete != true)
                                               .Select(x => new EmployeeDto()
                                               {
                                                    Id = x.Id, NameVI = x.HoTenVi ?? "", NameEN = x.HoTenEn ?? "",
                                               })
                                               .ToListAsync();
            var data = dataList.Where(x => ids != null && ids.Contains(x.Id)).ToList();
            return data;
        }
    }
}
