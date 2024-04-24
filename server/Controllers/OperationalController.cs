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
    * URL: https://localhost:portnumber/api/v1/operational
    */
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class OperationalController : Controller
    {
        private readonly ICategoryService _categoryService;

        public OperationalController(ICategoryService CategoryService)
        {
            _categoryService = CategoryService;
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/operational/all
        * Description: Lấy dữ liệu về tất cả loại tác nghiệp trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllOperationals()
        {
            var response = new Response();

            try
            {
                var data = await _categoryService.GetAllOperationals();

                response.Status = true;
                response.Message = "Lấy dữ liệu về tất cả loại tác nghiệp thành công";
                response.Data = data;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi lấy dữ liệu về tất cả loại tác nghiệp";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/operational?start=0&size=100&search=
        * Description: Lấy dữ liệu về loại tác nghiệp trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpGet]
        public async Task<IActionResult> GetOperationals([FromQuery(Name = "start")] int Start = 0, [FromQuery(Name = "size")] int Size = 10, [FromQuery(Name = "search")] string Search = "")
        {
            var response = new ResponsePage();

            try
            {
                var search = Search.ToLower().Trim();
                var data = await _categoryService.GetOperationals(Start, Size, search);
                var totalRow = await _categoryService.GetTotalOperationals(search);

                response.Status = true;
                response.Message = "Lấy dữ liệu về loại tác nghiệp thành công";
                response.Data = data;
                response.TotalRowCount = totalRow;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi lấy dữ liệu về loại tác nghiệp";
                response.Data = null;
                response.TotalRowCount = 0;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [POST] -> https://localhost:portnumber/api/v1/operational
        * Description: Thêm mới dữ liệu về loại tác nghiệp trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpPost]
        public async Task<IActionResult> CreateOperational([FromBody] CreateOperationalRequest req)
        {
            var response = new Response();

            try
            {
                await _categoryService.CreateOperational(req);

                response.Status = true;
                response.Message = "Tạo loại tác nghiệp thành công trên hệ thống";
                response.Data = null;

                return StatusCode(201, response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi tạo loại tác nghiệp trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [PUT] -> https://localhost:portnumber/api/v1/operational/{id}
        * Description: Cập nhật dữ liệu về loại tác nghiệp trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateOperational([FromRoute] long id, [FromBody] UpdateOperationalRequest req)
        {
            var response = new Response();

            try
            {
                if (id != req.Id)
                {
                    response.Status = false;
                    response.Message = "Lỗi cập nhật loại tác nghiệp trên hệ thống";
                    response.Data = null;
                    return BadRequest(response);
                }

                var data = await _categoryService.GetOperationalById(id);

                if (data == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi cập nhật loại tác nghiệp trên hệ thống vì dữ liệu không tồn tại";
                    response.Data = null;

                    return NotFound(response);
                }

                if (data != null)
                {
                    await _categoryService.UpdateOperational(data, req);
                }

                response.Status = true;
                response.Message = "Bạn đã cập nhật loại tác nghiệp thành công trên hệ thống";
                response.Data = null;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi cập nhật loại tác nghiệp trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [DELETE] -> https://localhost:portnumber/api/v1/operational/{id}
        * Description: Xoá dữ liệu về loại tác nghiệp trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteOperational([FromRoute] long id)
        {
            var response = new Response();

            try
            {
                var data = await _categoryService.GetOperationalById(id);

                if (data == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi xoá loại tác nghiệp trên hệ thống vì dữ liệu không tồn tại";
                    response.Data = null;

                    return NotFound(response);
                }

                await _categoryService.DeleteOperational(data);

                response.Status = true;
                response.Message = "Xoá loại tác nghiệp thành công";
                response.Data = null;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi xoá loại tác nghiệp trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }
    }
}
