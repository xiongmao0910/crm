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
    * URL: https://localhost:portnumber/api/v1/major
    */
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class MajorController : Controller
    {
        private readonly ICategoryService _categoryService;

        public MajorController(ICategoryService CategoryService)
        {
            _categoryService = CategoryService;
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/major/all
        * Description: Lấy dữ liệu về tất cả nghiệp vụ trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllMajors()
        {
            var response = new Response();

            try
            {
                var data = await _categoryService.GetAllMajors();

                response.Status = true;
                response.Message = "Lấy dữ liệu về tất cả nghiệp vụ thành công";
                response.Data = data;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi lấy dữ liệu về tất cả nghiệp vụ";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/major?start=0&size=100&search=
        * Description: Lấy dữ liệu về nghiệp vụ trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpGet]
        public async Task<IActionResult> GetMajors([FromQuery(Name = "start")] int Start = 0, [FromQuery(Name = "size")] int Size = 10, [FromQuery(Name = "search")] string Search = "")
        {
            var response = new ResponsePage();

            try
            {
                var search = Search.ToLower().Trim();
                var data = await _categoryService.GetMajors(Start, Size, search);
                var totalRow = await _categoryService.GetTotalMajors(search);

                response.Status = true;
                response.Message = "Lấy dữ liệu về nghiệp vụ thành công";
                response.Data = data;
                response.TotalRowCount = totalRow;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi lấy dữ liệu về nghiệp vụ";
                response.Data = null;
                response.TotalRowCount = 0;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [POST] -> https://localhost:portnumber/api/v1/major
        * Description: Thêm mới dữ liệu về nghiệp vụ trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpPost]
        public async Task<IActionResult> CreateMajor([FromBody] CreateMajorRequest req)
        {
            var response = new Response();

            try
            {
                await _categoryService.CreateMajor(req);

                response.Status = true;
                response.Message = "Tạo nghiệp vụ thành công trên hệ thống";
                response.Data = null;

                return StatusCode(201, response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi tạo nghiệp vụ trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [PUT] -> https://localhost:portnumber/api/v1/major/{id}
        * Description: Cập nhật dữ liệu về nghiệp vụ trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateMajor([FromRoute] long id, [FromBody] UpdateMajorRequest req)
        {
            var response = new Response();

            try
            {
                if (id != req.Id)
                {
                    response.Status = false;
                    response.Message = "Lỗi cập nhật nghiệp vụ trên hệ thống";
                    response.Data = null;
                    return BadRequest(response);
                }

                var data = await _categoryService.GetMajorById(id);

                if (data == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi cập nhật nghiệp vụ trên hệ thống vì dữ liệu không tồn tại";
                    response.Data = null;

                    return NotFound(response);
                }

                if (data != null)
                {
                    await _categoryService.UpdateMajor(data, req);
                }

                response.Status = true;
                response.Message = "Bạn đã cập nhật nghiệp vụ thành công trên hệ thống";
                response.Data = null;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi cập nhật nghiệp vụ trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [DELETE] -> https://localhost:portnumber/api/v1/major/{id}
        * Description: Xoá dữ liệu về nghiệp vụ trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteMajor([FromRoute] long id)
        {
            var response = new Response();

            try
            {
                var data = await _categoryService.GetMajorById(id);

                if (data == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi xoá nghiệp vụ trên hệ thống vì dữ liệu không tồn tại";
                    response.Data = null;

                    return NotFound(response);
                }

                await _categoryService.DeleteMajor(data);

                response.Status = true;
                response.Message = "Xoá nghiệp vụ thành công";
                response.Data = null;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi xoá nghiệp vụ trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }
    }
}
