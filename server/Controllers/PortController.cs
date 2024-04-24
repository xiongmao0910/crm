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
    * URL: https://localhost:portnumber/api/v1/port
    */
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class PortController : Controller
    {
        private readonly IPortService _portService;

        public PortController(IPortService portService)
        {
            _portService = portService;
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/port/{idCountry}/all
        * Description: Lấy dữ liệu về cảng ứng với quốc gia trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpGet]
        [Route("{idCountry}/all")]
        public async Task<IActionResult> GetPortsByIdCountry([FromRoute] long idCountry)
        {
            var response = new Response();

            try
            {
                var data = await _portService.GetPortsByIdCountry(idCountry);

                response.Status = true;
                response.Message = "Lấy dữ liệu về cảng ứng với quốc gia thành công";
                response.Data = data;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi lấy dữ liệu về cảng ứng với quốc gia";
                response.Data = null;

                return StatusCode(500, response);
            }
        }


        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/port?start=0&size=100&search=
        * Description: Lấy dữ liệu về cảng trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpGet]
        public async Task<IActionResult> GetPorts([FromQuery(Name = "start")] int Start = 0, [FromQuery(Name = "size")] int Size = 10, [FromQuery(Name = "search")] string Search = "")
        {
            var response = new ResponsePage();

            try
            {
                var search = Search.ToLower().Trim();
                var data = await _portService.GetPorts(Start, Size, search);
                var totalRow = await _portService.GetTotalPorts(search);

                response.Status = true;
                response.Message = "Lấy dữ liệu về cảng thành công";
                response.Data = data;
                response.TotalRowCount = totalRow;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi lấy dữ liệu về cảng";
                response.Data = null;
                response.TotalRowCount = 0;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [POST] -> https://localhost:portnumber/api/v1/port
        * Description: Thêm mới dữ liệu về cảng trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpPost]
        public async Task<IActionResult> CreatePort([FromBody] CreatePortRequest req)
        {
            var response = new Response();

            try
            {
                await _portService.CreatePort(req);

                response.Status = true;
                response.Message = "Tạo cảng thành công trên hệ thống";
                response.Data = null;

                return StatusCode(201, response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi tạo cảng trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [PUT] -> https://localhost:portnumber/api/v1/port/{id}
        * Description: Cập nhật dữ liệu về cảng trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdatePort([FromRoute] long id, [FromBody] UpdatePortRequest req)
        {
            var response = new Response();

            try
            {
                if (id != req.Id)
                {
                    response.Status = false;
                    response.Message = "Lỗi cập nhật cảng trên hệ thống";
                    response.Data = null;

                    return BadRequest(response);
                }

                var data = await _portService.GetPortById(id);

                if (data == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi cập nhật cảng trên hệ thống vì dữ liệu không tồn tại";
                    response.Data = null;

                    return NotFound(response);
                }

                if (data != null)
                {
                    await _portService.UpdatePort(data, req);
                }

                response.Status = true;
                response.Message = "Bạn đã cập nhật cảng thành công trên hệ thống";
                response.Data = null;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi cập nhật cảng trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [DELETE] -> https://localhost:portnumber/api/v1/port/{id}
        * Description: Xoá dữ liệu về cảng trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeletePort([FromRoute] long id)
        {
            var response = new Response();

            try
            {
                var data = await _portService.GetPortById(id);

                if (data == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi xoá cảng trên hệ thống vì dữ liệu không tồn tại";
                    response.Data = null;

                    return NotFound(response);
                }

                await _portService.DeletePort(data);

                response.Status = true;
                response.Message = "Xoá cảng thành công";
                response.Data = null;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi xoá cảng trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }
    }
}
