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
    * URL: https://localhost:portnumber/api/v1/business
    */
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class BusinessController : Controller
    {
        private readonly ICategoryService _categoryService;

        public BusinessController(ICategoryService CategoryService)
        {
            _categoryService = CategoryService;
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/business/all
        * Description: Lấy dữ liệu về tất cả loại doanh nghiệp trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllBusinesses()
        {
            var response = new Response();

            try
            {
                var data = await _categoryService.GetAllBusinesses();

                response.Status = true;
                response.Message = "Lấy dữ liệu về tất cả loại doanh nghiệp thành công";
                response.Data = data;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi lấy dữ liệu về tất cả loại doanh nghiệp";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/business
        * Description: Lấy dữ liệu về loại doanh nghiệp trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpGet]
        public async Task<IActionResult> GetBusinesses([FromQuery(Name = "start")] int Start = 0, [FromQuery(Name = "size")] int Size = 10, [FromQuery(Name = "search")] string Search = "")
        {
            var response = new ResponsePage();

            try
            {
                var search = Search.ToLower().Trim();
                var data = await _categoryService.GetBusinesses(Start, Size, search);
                var total = await _categoryService.GetTotalBusinesses(search);

                response.Status = true;
                response.Message = "Lấy dữ liệu loại doanh nghiệp thành công";
                response.Data = data;
                response.TotalRowCount = total;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi lấy dữ liệu loại doanh nghiệp";
                response.Data = null;
                response.TotalRowCount = 0;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [POST] -> https://localhost:portnumber/api/v1/business
        * Description: Tạo mới dữ liệu loại doanh nghiệp trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpPost]
        public async Task<IActionResult> CreateBusiness([FromBody] CreateBusinessRequest req)
        {
            var response = new Response();

            try
            {
                await _categoryService.CreateBusiness(req);

                response.Status = true;
                response.Message = "Tạo dữ liệu loại doanh nghiệp thành công";
                response.Data = null;

                return StatusCode(201, response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi tạo dữ liệu loại doanh nghiệp trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [PUT] -> https://localhost:portnumber/api/v1/business/{id}
        * Description: Cập nhật dữ liệu loại doanh nghiệp trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateBusiness([FromRoute] long id, [FromBody] UpdateBusinessRequest req)
        {
            var response = new Response();

            try
            {
                if (id != req.Id)
                {
                    response.Status = false;
                    response.Message = "Lỗi cập nhật dữ liệu loại doanh nghiệp trên hệ thống";
                    response.Data = null;
                    return BadRequest(response);
                }

                var data = await _categoryService.GetBusinessById(id);

                if (data == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi cập nhật dữ liệu loại doanh nghiệp trên hệ thống vì dữ liệu không tồn tại";
                    response.Data = null;

                    return NotFound(response);
                }

                if (data != null)
                {
                    await _categoryService.UpdateBusiness(data, req);
                }

                response.Status = true;
                response.Message = "Bạn đã cập nhật dữ liệu loại doanh nghiệp thành công trên hệ thống";
                response.Data = null;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi cập nhật dữ liệu loại doanh nghiệp trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [DELETE] -> https://localhost:portnumber/api/v1/business/{id}
        * Description: Xoá dữ liệu loại doanh nghiệp trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteBusiness([FromRoute] long id)
        {
            var response = new Response();

            try
            {
                var data = await _categoryService.GetBusinessById(id);

                if (data == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi xoá loại doanh nghiệp trên hệ thống vì dữ liệu không tồn tại";
                    response.Data = null;
                    return NotFound(response);
                }

                await _categoryService.DeleteBusiness(data);

                response.Status = true;
                response.Message = "Xoá dữ liệu loại doanh nghiệp thành công";
                response.Data = null;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi xoá dữ liệu loại doanh nghiệp trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }
    }
}
