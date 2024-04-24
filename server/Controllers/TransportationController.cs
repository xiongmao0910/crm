// ** library **
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
// ** architecture **
using locy_api.Models.Response;
using locy_api.Interfaces;
using locy_api.Models.Requests;
using locy_api.Services;

namespace locy_api.Controllers
{
    /*
    * URL: https://localhost:portnumber/api/v1/transportation
    */
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class TransportationController : Controller
    {
        private readonly ICategoryService _categoryService;

        public TransportationController(ICategoryService CategoryService)
        {
            _categoryService = CategoryService;
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/transportation/all
        * Description: Lấy dữ liệu về tất cả loại hình vận chuyển trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllTransportations()
        {
            var response = new Response();

            try
            {
                var data = await _categoryService.GetAllTransportations();

                response.Status = true;
                response.Message = "Lấy dữ liệu về tất cả loại hình vận chuyển thành công";
                response.Data = data;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi lấy dữ liệu về tất cả loại hình vận chuyển";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/transportation?start=0&size=100&search=
        * Description: Lấy dữ liệu về loại hình vận chuyển trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpGet]
        public async Task<IActionResult> GetTransportations([FromQuery(Name = "start")] int Start = 0, [FromQuery(Name = "size")] int Size = 10, [FromQuery(Name = "search")] string Search = "")
        {
            var response = new ResponsePage();

            try
            {
                var search = Search.ToLower().Trim();
                var data = await _categoryService.GetTransportations(Start, Size, search);
                var totalRow = await _categoryService.GetTotalTransportations(search);

                response.Status = true;
                response.Message = "Lấy dữ liệu về loại hình vận chuyển thành công";
                response.Data = data;
                response.TotalRowCount = totalRow;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi lấy dữ liệu về loại hình vận chuyển";
                response.Data = null;
                response.TotalRowCount = 0;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [POST] -> https://localhost:portnumber/api/v1/transportation
        * Description: Thêm mới dữ liệu về loại hình vận chuyển trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpPost]
        public async Task<IActionResult> CreateTransportation([FromBody] CreateTransportationRequest req)
        {
            var response = new Response();

            try
            {
                await _categoryService.CreateTransportation(req);

                response.Status = true;
                response.Message = "Tạo loại hình vận chuyển thành công trên hệ thống";
                response.Data = null;

                return StatusCode(201, response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi tạo loại hình vận chuyển trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [PUT] -> https://localhost:portnumber/api/v1/transportation/{id}
        * Description: Cập nhật dữ liệu về loại hình vận chuyển trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateTransportation([FromRoute] long id, [FromBody] UpdateTransportationRequest req)
        {
            var response = new Response();

            try
            {
                if (id != req.Id)
                {
                    response.Status = false;
                    response.Message = "Lỗi cập nhật loại hình vận chuyển trên hệ thống";
                    response.Data = null;
                    return BadRequest(response);
                }

                var data = await _categoryService.GetTransportationById(id);

                if (data == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi cập nhật loại hình vận chuyển trên hệ thống vì dữ liệu không tồn tại";
                    response.Data = null;

                    return NotFound(response);
                }

                if (data != null)
                {
                    await _categoryService.UpdateTransportation(data, req);
                }

                response.Status = true;
                response.Message = "Bạn đã cập nhật loại hình vận chuyển thành công trên hệ thống";
                response.Data = null;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi cập nhật loại hình vận chuyển trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [DELETE] -> https://localhost:portnumber/api/v1/transportation/{id}
        * Description: Xoá dữ liệu về loại hình vận chuyển trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteTransportation([FromRoute] long id)
        {
            var response = new Response();

            try
            {
                var data = await _categoryService.GetTransportationById(id);

                if (data == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi xoá loại hình vận chuyển trên hệ thống vì dữ liệu không tồn tại";
                    response.Data = null;

                    return NotFound(response);
                }

                await _categoryService.DeleteTransportation(data);

                response.Status = true;
                response.Message = "Xoá loại hình vận chuyển thành công";
                response.Data = null;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi xoá loại hình vận chuyển trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }
    }
}
