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
    * URL: https://localhost:portnumber/api/v1/office
    */
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class OfficeController : Controller
    {
        private readonly ICategoryService _categoryService;

        public OfficeController(ICategoryService CategoryService)
        {
            _categoryService = CategoryService;
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/office/all
        * Description: Lấy dữ liệu về tất cả văn phòng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllOffices()
        {
            var response = new Response();

            try
            {
                var data = await _categoryService.GetAllOffices();

                response.Status = true;
                response.Message = "Lấy dữ liệu về tất cả văn phòng thành công";
                response.Data = data;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi lấy dữ liệu về tất cả văn phòng";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/office?start=0&size=100&search=
        * Description: Lấy dữ liệu về văn phòng trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpGet]
        public async Task<IActionResult> GetOffices([FromQuery(Name = "start")] int Start = 0, [FromQuery(Name = "size")] int Size = 10, [FromQuery(Name = "search")] string Search = "")
        {
            var response = new ResponsePage();

            try
            {
                var search = Search.ToLower().Trim();
                var data = await _categoryService.GetOffices(Start, Size, search);
                var totalRow = await _categoryService.GetTotalOffices(search);

                response.Status = true;
                response.Message = "Lấy dữ liệu về văn phòng thành công";
                response.Data = data;
                response.TotalRowCount = totalRow;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi lấy dữ liệu về văn phòng";
                response.Data = null;
                response.TotalRowCount = 0;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [POST] -> https://localhost:portnumber/api/v1/office
        * Description: Thêm mới dữ liệu về văn phòng trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpPost]
        public async Task<IActionResult> CreateOffice([FromBody] CreateOfficeRequest req)
        {
            var response = new Response();

            try
            {
                await _categoryService.CreateOffice(req);

                response.Status = true;
                response.Message = "Tạo văn phòng thành công trên hệ thống";
                response.Data = null;

                return StatusCode(201, response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi tạo văn phòng trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [PUT] -> https://localhost:portnumber/api/v1/office/{id}
        * Description: Cập nhật dữ liệu về văn phòng trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateOffice([FromRoute] long id, [FromBody] UpdateOfficeRequest req)
        {
            var response = new Response();

            try
            {
                if (id != req.Id)
                {
                    response.Status = false;
                    response.Message = "Lỗi cập nhật văn phòng trên hệ thống";
                    response.Data = null;
                    return BadRequest(response);
                }

                var data = await _categoryService.GetOfficeById(id);

                if (data == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi cập nhật văn phòng trên hệ thống vì dữ liệu không tồn tại";
                    response.Data = null;

                    return NotFound(response);
                }

                if (data != null)
                {
                    await _categoryService.UpdateOffice(data, req);
                }

                response.Status = true;
                response.Message = "Bạn đã cập nhật văn phòng thành công trên hệ thống";
                response.Data = null;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi cập nhật văn phòng trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [DELETE] -> https://localhost:portnumber/api/v1/office/{id}
        * Description: Xoá dữ liệu về văn phòng trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteOffice([FromRoute] long id)
        {
            var response = new Response();

            try
            {
                var data = await _categoryService.GetOfficeById(id);

                if (data == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi xoá văn phòng trên hệ thống vì dữ liệu không tồn tại";
                    response.Data = null;

                    return NotFound(response);
                }

                await _categoryService.DeleteOffice(data);

                response.Status = true;
                response.Message = "Xoá văn phòng thành công";
                response.Data = null;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi xoá văn phòng trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }
    }
}
