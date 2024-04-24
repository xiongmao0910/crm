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
    * URL: https://localhost:portnumber/api/v1/position
    */
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class PositionController : Controller
    {
        private readonly ICategoryService _categoryService;

        public PositionController(ICategoryService CategoryService)
        {
            _categoryService = CategoryService;
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/position/all
        * Description: Lấy dữ liệu về tất cả chức vụ trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllPositions()
        {
            var response = new Response();

            try
            {
                var data = await _categoryService.GetAllPositions();

                response.Status = true;
                response.Message = "Lấy dữ liệu về tất cả chức vụ thành công";
                response.Data = data;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi lấy dữ liệu về tất cả chức vụ";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/position?start=0&size=100&search=
        * Description: Lấy dữ liệu về chức vụ trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpGet]
        public async Task<IActionResult> GetPositions([FromQuery(Name = "start")] int Start = 0, [FromQuery(Name = "size")] int Size = 10, [FromQuery(Name = "search")] string Search = "")
        {
            var response = new ResponsePage();

            try
            {
                var search = Search.ToLower().Trim();
                var data = await _categoryService.GetPositions(Start, Size, search);
                var totalRow = await _categoryService.GetTotalPositions(search);

                response.Status = true;
                response.Message = "Lấy dữ liệu về chức vụ thành công";
                response.Data = data;
                response.TotalRowCount = totalRow;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi lấy dữ liệu về chức vụ";
                response.Data = null;
                response.TotalRowCount = 0;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [POST] -> https://localhost:portnumber/api/v1/position
        * Description: Thêm mới dữ liệu về chức vụ trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpPost]
        public async Task<IActionResult> CreatePosition([FromBody] CreatePositionRequest req)
        {
            var response = new Response();

            try
            {
                await _categoryService.CreatePosition(req);

                response.Status = true;
                response.Message = "Tạo chức vụ thành công trên hệ thống";
                response.Data = null;

                return StatusCode(201, response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi tạo chức vụ trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [PUT] -> https://localhost:portnumber/api/v1/position/{id}
        * Description: Cập nhật dữ liệu về chức vụ trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdatePosition([FromRoute] long id, [FromBody] UpdatePositionRequest req)
        {
            var response = new Response();

            try
            {
                if (id != req.Id)
                {
                    response.Status = false;
                    response.Message = "Lỗi cập nhật chức vụ trên hệ thống";
                    response.Data = null;
                    return BadRequest(response);
                }

                var data = await _categoryService.GetPositionById(id);

                if (data == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi cập nhật chức vụ trên hệ thống vì dữ liệu không tồn tại";
                    response.Data = null;

                    return NotFound(response);
                }

                if (data != null)
                {
                    await _categoryService.UpdatePosition(data, req);
                }

                response.Status = true;
                response.Message = "Bạn đã cập nhật chức vụ thành công trên hệ thống";
                response.Data = null;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi cập nhật chức vụ trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [DELETE] -> https://localhost:portnumber/api/v1/position/{id}
        * Description: Xoá dữ liệu về chức vụ trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeletePosition([FromRoute] long id)
        {
            var response = new Response();

            try
            {
                var data = await _categoryService.GetPositionById(id);

                if (data == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi xoá chức vụ trên hệ thống vì dữ liệu không tồn tại";
                    response.Data = null;

                    return NotFound(response);
                }

                await _categoryService.DeletePosition(data);

                response.Status = true;
                response.Message = "Xoá chức vụ thành công";
                response.Data = null;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi xoá chức vụ trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }
    }
}
