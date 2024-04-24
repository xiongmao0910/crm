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
    * URL: https://localhost:portnumber/api/v1/customertypes
    */
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class CustomerTypesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CustomerTypesController(ICategoryService CategoryService)
        {
            _categoryService = CategoryService;
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/customertypes?start=0&size=100&search=
        * Description: Lấy dữ liệu về loại khách hàng trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpGet]
        public async Task<IActionResult> GetCustomerTypes([FromQuery(Name = "start")] int Start = 0, [FromQuery(Name = "size")] int Size = 10, [FromQuery(Name = "search")] string Search = "")
        {
            var response = new ResponsePage();

            try
            {
                var search = Search.ToLower().Trim();
                var data = await _categoryService.GetCustomerTypes(Start, Size, search);
                var totalRow = await _categoryService.GetTotalCustomerTypes(search);

                response.Status = true;
                response.Message = "Lấy dữ liệu về loại khách hàng thành công";
                response.Data = data;
                response.TotalRowCount = totalRow;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi lấy dữ liệu về loại khách hàng";
                response.Data = null;
                response.TotalRowCount = 0;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [POST] -> https://localhost:portnumber/api/v1/customertypes
        * Description: Thêm mới dữ liệu về loại khách hàng trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpPost]
        public async Task<IActionResult> CreateCustomerType([FromBody] CreateCustomerTypeRequest req)
        {
            var response = new Response();

            try
            {
                await _categoryService.CreateCustomerType(req);

                response.Status = true;
                response.Message = "Tạo loại khách hàng thành công trên hệ thống";
                response.Data = null;

                return StatusCode(201, response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi tạo loại khách hàng trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [PUT] -> https://localhost:portnumber/api/v1/customertypes/{id}
        * Description: Cập nhật dữ liệu về loại khách hàng trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCustomerType([FromRoute] long id, [FromBody] UpdateCustomerTypeRequest req)
        {
            var response = new Response();

            try
            {
                if (id != req.Id)
                {
                    response.Status = false;
                    response.Message = "Lỗi cập nhật loại khách hàng trên hệ thống";
                    response.Data = null;
                    return BadRequest(response);
                }

                var data = await _categoryService.GetCustomerTypeById(id);

                if (data == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi cập nhật loại khách hàng trên hệ thống vì dữ liệu không tồn tại";
                    response.Data = null;

                    return NotFound(response);
                }

                if (data != null)
                {
                    await _categoryService.UpdateCustomerType(data, req);
                }

                response.Status = true;
                response.Message = "Bạn đã cập nhật loại khách hàng thành công trên hệ thống";
                response.Data = null;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi cập nhật loại khách hàng trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [DELETE] -> https://localhost:portnumber/api/v1/customertypes/{id}
        * Description: Xoá dữ liệu về loại khách hàng trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCustomerType([FromRoute] long id)
        {
            var response = new Response();

            try
            {
                var data = await _categoryService.GetCustomerTypeById(id);

                if (data == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi xoá loại khách hàng trên hệ thống vì dữ liệu không tồn tại";
                    response.Data = null;

                    return NotFound(response);
                }

                await _categoryService.DeleteCustomerType(data);

                response.Status = true;
                response.Message = "Xoá loại khách hàng thành công";
                response.Data = null;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi xoá loại khách hàng trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }
    }
}
