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
    * URL: https://localhost:portnumber/api/v1/typeofcustomer
    */
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class TypeOfCustomerController : Controller
    {
        private readonly ICategoryService _categoryService;

        public TypeOfCustomerController(ICategoryService CategoryService)
        {
            _categoryService = CategoryService;
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/typeofcustomer/all
        * Description: Lấy dữ liệu về tất cả phân loại khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetTypeOfCustomers()
        {
            var response = new Response();

            try
            {
                var data = await _categoryService.GetTypeOfCustomers();

                response.Status = true;
                response.Message = "Lấy dữ liệu về tất cả phân loại khách hàng thành công";
                response.Data = data;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi lấy dữ liệu về tất cả phân loại khách hàng";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/typeofcustomer?start=0&size=100&search=
        * Description: Lấy dữ liệu về phân loại khách hàng trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpGet]
        public async Task<IActionResult> GetTypeOfCustomers([FromQuery(Name = "start")] int Start = 0, [FromQuery(Name = "size")] int Size = 10, [FromQuery(Name = "search")] string Search = "")
        {
            var response = new ResponsePage();

            try
            {
                var search = Search.ToLower().Trim();
                var data = await _categoryService.GetTypeOfCustomers(Start, Size, search);
                var totalRow = await _categoryService.GetTotalTypeOfCustomers(search);

                response.Status = true;
                response.Message = "Lấy dữ liệu về phân loại khách hàng thành công";
                response.Data = data;
                response.TotalRowCount = totalRow;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi lấy dữ liệu về phân loại khách hàng";
                response.Data = null;
                response.TotalRowCount = 0;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [POST] -> https://localhost:portnumber/api/v1/typeofcustomer
        * Description: Thêm mới dữ liệu về phân loại khách hàng trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpPost]
        public async Task<IActionResult> CreateTypeOfCustomer([FromBody] CreateTypeOfCustomerRequest req)
        {
            var response = new Response();

            try
            {
                await _categoryService.CreateTypeOfCustomer(req);

                response.Status = true;
                response.Message = "Tạo phân loại khách hàng thành công trên hệ thống";
                response.Data = null;

                return StatusCode(201, response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi tạo phân loại khách hàng trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [PUT] -> https://localhost:portnumber/api/v1/typeofcustomer/{id}
        * Description: Cập nhật dữ liệu về phân loại khách hàng trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateTypeOfCustomer([FromRoute] long id, [FromBody] UpdateTypeOfCustomerRequest req)
        {
            var response = new Response();

            try
            {
                if (id != req.Id)
                {
                    response.Status = false;
                    response.Message = "Lỗi cập nhật phân loại khách hàng trên hệ thống";
                    response.Data = null;
                    return BadRequest(response);
                }

                var data = await _categoryService.GetTypeOfCustomerById(id);

                if (data == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi cập nhật phân loại khách hàng trên hệ thống vì dữ liệu không tồn tại";
                    response.Data = null;

                    return NotFound(response);
                }

                if (data != null)
                {
                    await _categoryService.UpdateTypeOfCustomer(data, req);
                }

                response.Status = true;
                response.Message = "Bạn đã cập nhật phân loại khách hàng thành công trên hệ thống";
                response.Data = null;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi cập nhật phân loại khách hàng trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [DELETE] -> https://localhost:portnumber/api/v1/typeofcustomer/{id}
        * Description: Xoá dữ liệu về phân loại khách hàng trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteTypeOfCustomer([FromRoute] long id)
        {
            var response = new Response();

            try
            {
                var data = await _categoryService.GetTypeOfCustomerById(id);

                if (data == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi xoá phân loại khách hàng trên hệ thống vì dữ liệu không tồn tại";
                    response.Data = null;

                    return NotFound(response);
                }

                await _categoryService.DeleteTypeOfCustomer(data);

                response.Status = true;
                response.Message = "Xoá phân loại khách hàng thành công";
                response.Data = null;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi xoá phân loại khách hàng trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }
    }
}
