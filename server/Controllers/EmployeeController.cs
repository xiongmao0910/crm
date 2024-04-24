// ** library **
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
// ** architecture **
using locy_api.Models.Response;
using locy_api.Interfaces;
using locy_api.Models.Requests;
using locy_api.Models.DTOs;

namespace locy_api.Controllers
{
    /*
    * URL: https://localhost:portnumber/api/v1/employee
    */
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IEmployeeHelper _employeeHelper;

        public EmployeeController(IEmployeeService employeeService, IEmployeeHelper employeeHelper)
        {
            _employeeService = employeeService;
            _employeeHelper = employeeHelper;
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/employee
        * Description: Người dùng thực hiện lấy danh sách nhân viên trên hệ thống
        */
        [Authorize("ManageEmployee")]
        [HttpGet]
        public async Task<IActionResult> GetEmployees([FromQuery(Name = "start")] int Start = 0, [FromQuery(Name = "size")] int Size = 10, [FromQuery(Name = "search")] string Search = "")
        {
            var response = new ResponsePage();

            try
            {
                var search = Search.ToLower().Trim();
                var data = await _employeeService.GetEmployees(Start, Size, search);
                var total = await _employeeService.GetTotalEmployees(search);

                response.Status = true;
                response.Message = "Lấy dữ liệu về tất cả nhân viên thành công";
                response.Data = data;
                response.TotalRowCount = total;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi lấy dữ liệu về tất cả nhân viên";
                response.Data = null;
                response.TotalRowCount = 0;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/employee/delete
        * Description: Người dùng thực hiện lấy danh sách nhân viên đã bị xoá trên hệ thống
        */
        [Authorize("ManageEmployee")]
        [HttpGet]
        [Route("delete")]
        public async Task<IActionResult> GetEmployeesDelete([FromQuery(Name = "start")] int Start = 0, [FromQuery(Name = "size")] int Size = 10, [FromQuery(Name = "search")] string Search = "")
        {
            var response = new ResponsePage();

            try
            {
                var search = Search.ToLower().Trim();
                var data = await _employeeService.GetEmployees(Start, Size, search, true);
                var total = await _employeeService.GetTotalEmployees(search, true);

                response.Status = true;
                response.Message = "Lấy dữ liệu về tất cả nhân viên đã bị xoá thành công";
                response.Data = data;
                response.TotalRowCount = total;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi lấy dữ liệu về tất cả nhân viên đã bị xoá";
                response.Data = null;
                response.TotalRowCount = 0;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/employee/group
        * Description: Người dùng thực hiện lấy danh sách nhân viên trong nhóm
        */
        [Authorize("DeliveryCustomer")]
        [HttpGet]
        [Route("group")]
        public async Task<IActionResult> GetEmployeesGroup()
        {
            var response = new Response();

            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var idNhanVien = long.Parse(identity?.Claims.FirstOrDefault(o => o.Type == "IDEmployee")?.Value ?? "0");
                var permission = identity?.Claims.FirstOrDefault(o => o.Type == "Permission")?.Value ?? "";
                List<EmployeeDto>? data;

                if (permission.Contains("1048576") || permission.Contains("7000"))
                {
                    data = await _employeeHelper.GetAllEmployees();
                } else
                {
                    var ids = await _employeeHelper.GetListEmployee(idNhanVien);
                    data = (ids != null && ids.Count > 0) ? await _employeeHelper.GetAllEmployeesGroup(ids) : [];
                }

                response.Status = true;
                response.Message = "Lấy dữ liệu về tất cả nhân viên trong nhóm";
                response.Data = data;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi lấy dữ liệu về tất cả nhân viên trong nhóm";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [POST] -> https://localhost:portnumber/api/v1/employee
        * Description: Người dùng thực hiện tạo mới nhân viên trên hệ thống
        */
        [Authorize("ManageEmployee")]
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeRequest req)
        {
            var response = new Response();

            try
            {
                var isPersonnelCodeExist = await _employeeService.IsPersonnelCodeExist(req.manhanvien);
                var isUsernameExist = await _employeeService.IsUsernameExist(req.Username);

                if(isPersonnelCodeExist)
                {
                    response.Status = false;
                    response.Message = "Mã nhân sự đã tồn tại, vui lòng nhập lại mã nhân sự";
                    response.Data = null;

                    return StatusCode(409, response);
                }

                if(isUsernameExist)
                {
                    response.Status = false;
                    response.Message = "Tên đăng nhập đã tồn tại, vui lòng nhập lại tên đăng nhập";
                    response.Data = null;

                    return StatusCode(409, response);
                }

                await _employeeService.CreateEmployee(req);

                response.Status = true;
                response.Message = "Bạn đã tạo nhân viên thành công";
                response.Data = null;

                return StatusCode(201, response);
            } catch
            {
                response.Status = false;
                response.Message = "Lỗi tạo nhân viên trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [PUT] -> https://localhost:portnumber/api/v1/employee/{id}
        * Description: Người dùng thực hiện cập nhật thông tin nhân viên trên hệ thống
        */
        [Authorize("ManageEmployee")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] long id, [FromBody] UpdateEmployeeRequest req)
        {
            var response = new Response();

            try
            {
                if(id != req.IdNhanVien)
                {
                    response.Status = false;
                    response.Message = "Lỗi cập nhật nhân viên trên hệ thống";
                    response.Data = null;

                    return BadRequest(response);
                }

                var info = await _employeeService.GetInfoById(req.IdNhanVien);
                var account = await _employeeService.GetAccountById(req.Id);

                if (info == null || account == null) {
                    response.Status = false;
                    response.Message = "Lỗi cập nhật nhân viên vì nhân viên không tồn tại";
                    response.Data = null;

                    return NotFound(response);
                }

                if(req.manhanvien != null && req.manhanvien != "" && req.manhanvien != info?.Manhansu)
                {
                    var isPersonnelCodeExist = await _employeeService.IsPersonnelCodeExist(req.manhanvien);

                    if (isPersonnelCodeExist)
                    {
                        response.Status = false;
                        response.Message = "Mã nhân sự đã tồn tại, vui lòng nhập lại mã nhân sự";
                        response.Data = null;

                        return StatusCode(409, response);
                    }
                }

                if (req.Username != null && req.Username != "" && req.Username != account.UserName)
                {
                    var isUsernameExist = await _employeeService.IsUsernameExist(req.Username);

                    if (isUsernameExist)
                    {
                        response.Status = false;
                        response.Message = "Tên đăng nhập đã tồn tại, vui lòng nhập lại tên đăng nhập";
                        response.Data = null;

                        return StatusCode(409, response);
                    }
                }

                if(account != null && info != null)
                {
                    await _employeeService.UpdateEmployee(account, info, req);
                }

                response.Status = true;
                response.Message = "Bạn đã cập nhật thông tin nhân viên thành công";
                response.Data = null;

                return Ok(response);
            } catch
            {
                response.Status = false;
                response.Message = "Lỗi cập nhật nhân viên trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [PUT] -> https://localhost:portnumber/api/v1/employee/{id}/delete
        * Description: Người dùng thực hiện tạo mới nhân viên trên hệ thống
        */
        [Authorize("ManageEmployee")]
        [HttpPut]
        [Route("{id}/delete")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] long id, [FromBody] DeleteEmployeeRequest req)
        {
            var response = new Response();

            try
            {
                if (id != req.IdNhanVien)
                {
                    response.Status = false;
                    response.Message = !req.FlagDelete ? "Lỗi huỷ xoá nhân viên trên hệ thống" : "Lỗi yêu cầu xoá nhân viên trên hệ thống";
                    response.Data = null;

                    return BadRequest(response);
                }

                var data = await _employeeService.GetInfoById(req.IdNhanVien);

                if(data == null)
                {
                    response.Status = false;
                    response.Message = !req.FlagDelete ? "Lỗi huỷ xoá nhân viên vì nhân viên không tồn tại trên hệ thống" : "Lỗi yêu cầu xoá nhân viên vì nhân viên không tồn tại trên hệ thống";
                    response.Data = null;

                    return NotFound(response);
                }

                if(data != null)
                {
                    await _employeeService.DeleteEmployee(data, req);
                }

                response.Status = true;
                response.Message = !req.FlagDelete ? "Bạn đã huỷ xoá nhân viên thành công" : "Bạn đã yêu cầu xoá nhân viên thành công";
                response.Data = null;

                return Ok(response);
            } catch
            {
                response.Status = false;
                response.Message = !req.FlagDelete ? "Lỗi huỷ xoá nhân viên trên hệ thống" : "Lỗi yêu cầu xoá nhân viên trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }
    }
}
