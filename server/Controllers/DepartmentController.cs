// ** library **
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
// ** architecture **
using locy_api.Models.Response;
using locy_api.Interfaces;
using locy_api.Models.Requests;

namespace locy_api.Controllers
{
    /*
    * URL: https://localhost:portnumber/api/v1/department
    */
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly ICategoryService _categoryService;

        public DepartmentController(ICategoryService CategoryService)
        {
            _categoryService = CategoryService;
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/department/all
        * Description: Lấy dữ liệu về tất cả phòng ban trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllDepartments()
        {
            var response = new Response();

            try
            {
                var data = await _categoryService.GetAllDepartments();

                response.Status = true;
                response.Message = "Lấy dữ liệu về tất cả phòng ban thành công";
                response.Data = data;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi lấy dữ liệu về tất cả phòng ban";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/department?start=0&size=100&search=
        * Description: Lấy dữ liệu về phòng ban trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpGet]
        public async Task<IActionResult> GetDepartments([FromQuery(Name = "start")] int Start = 0, [FromQuery(Name = "size")] int Size = 10, [FromQuery(Name = "search")] string Search = "")
        {
            var response = new ResponsePage();

            try
            {
                var search = Search.ToLower().Trim();
                var data = await _categoryService.GetDepartments(Start, Size, search);
                var totalRow = await _categoryService.GetTotalDepartments(search);

                response.Status = true;
                response.Message = "Lấy dữ liệu về phòng ban thành công";
                response.Data = data;
                response.TotalRowCount = totalRow;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi lấy dữ liệu về phòng ban";
                response.Data = null;
                response.TotalRowCount = 0;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [POST] -> https://localhost:portnumber/api/v1/department
        * Description: Thêm mới dữ liệu về phòng ban trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentRequest req)
        {
            var response = new Response();

            try
            {
                await _categoryService.CreateDepartment(req);

                response.Status = true;
                response.Message = "Tạo phòng ban thành công trên hệ thống";
                response.Data = null;

                return StatusCode(201, response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi tạo phòng ban trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [PUT] -> https://localhost:portnumber/api/v1/department/{id}
        * Description: Cập nhật dữ liệu về phòng ban trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateDepartment([FromRoute] long id, [FromBody] UpdateDepartmentRequest req)
        {
            var response = new Response();

            try
            {
                if (id != req.Id)
                {
                    response.Status = false;
                    response.Message = "Lỗi cập nhật phòng ban trên hệ thống";
                    response.Data = null;
                    return BadRequest(response);
                }

                var data = await _categoryService.GetDepartmentById(id);

                if (data == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi cập nhật phòng ban trên hệ thống vì dữ liệu không tồn tại";
                    response.Data = null;

                    return NotFound(response);
                }

                if (data != null)
                {
                    await _categoryService.UpdateDepartment(data, req);
                }

                response.Status = true;
                response.Message = "Bạn đã cập nhật phòng ban thành công trên hệ thống";
                response.Data = null;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi cập nhật phòng ban trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [DELETE] -> https://localhost:portnumber/api/v1/department/{id}
        * Description: Xoá dữ liệu về phòng ban trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteDepartment([FromRoute] long id)
        {
            var response = new Response();

            try
            {
                var data = await _categoryService.GetDepartmentById(id);

                if (data == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi xoá phòng ban trên hệ thống vì dữ liệu không tồn tại";
                    response.Data = null;

                    return NotFound(response);
                }

                await _categoryService.DeleteDepartment(data);

                response.Status = true;
                response.Message = "Xoá phòng ban thành công";
                response.Data = null;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi xoá phòng ban trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }
    }
}
