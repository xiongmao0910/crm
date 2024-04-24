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
    * URL: https://localhost:portnumber/api/v1/country
    */
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class CountryController : Controller
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/country/all
        * Description: Lấy dữ liệu về tất cả quốc gia trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllCountries()
        {
            var response = new Response();

            try
            {
                var data = await _countryService.GetAllCountries();

                response.Status = true;
                response.Message = "Lấy dữ liệu về tất cả quốc gia thành công";
                response.Data = data;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi lấy dữ liệu về tất cả quốc gia";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/country/{id}
        * Description: Lấy dữ liệu về quốc gia dựa theo trường id trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCountryById([FromRoute] long id)
        {
            var response = new Response();

            try
            {
                var data = await _countryService.GetCountryById(id);

                response.Status = true;
                response.Message = "Lấy dữ liệu về quốc gia thành công";
                response.Data = data;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi lấy dữ liệu về quốc gia";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/country
        * Description: Lấy dữ liệu về quốc gia trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpGet]
        public async Task<IActionResult> GetCountries([FromQuery(Name = "start")] int Start = 0, [FromQuery(Name = "size")] int Size = 10, [FromQuery(Name = "search")] string Search = "")
        {
            var response = new ResponsePage();

            try
            {
                var search = Search.ToLower().Trim();
                var data = await _countryService.GetCountries(Start, Size, search);
                var total = await _countryService.GetTotalCountries(search);

                response.Status = true;
                response.Message = "Lấy dữ liệu quốc gia thành công";
                response.Data = data;
                response.TotalRowCount = total;

                return Ok(response);
            } catch
            {
                response.Status = false;
                response.Message = "Lỗi lấy dữ liệu quốc gia";
                response.Data = null;
                response.TotalRowCount = 0;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [POST] -> https://localhost:portnumber/api/v1/country
        * Description: Tạo mới dữ liệu quốc gia trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpPost]
        public async Task<IActionResult> CreateCountry([FromBody] CreateCountryRequest req)
        {
            var response = new Response();

            try
            {
                await _countryService.CreateCountry(req);

                response.Status = true;
                response.Message = "Tạo dữ liệu quốc gia thành công";
                response.Data = null;

                return StatusCode(201, response);
            } catch
            {
                response.Status = false;
                response.Message = "Lỗi tạo dữ liệu quốc gia trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [PUT] -> https://localhost:portnumber/api/v1/country/{id}
        * Description: Cập nhật dữ liệu quốc gia trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCountry([FromRoute] long id, [FromBody] UpdateCountryRequest req)
        {
            var response = new Response();

            try
            {
                if(id != req.Id)
                {
                    response.Status = false;
                    response.Message = "Lỗi cập nhật dữ liệu quốc gia trên hệ thống";
                    response.Data = null;

                    return BadRequest(response);
                }

                var data = await _countryService.GetCountryById(id);

                if (data == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi cập nhật dữ liệu quốc gia trên hệ thống vì dữ liệu không tồn tại";
                    response.Data = null;

                    return NotFound(response);
                }

                if (data != null)
                {
                    await _countryService.UpdateCountry(data, req);
                }

                response.Status = true;
                response.Message = "Bạn đã cập nhật dữ liệu quốc gia thành công trên hệ thống";
                response.Data = null;

                return Ok(response);
            } catch
            {
                response.Status = false;
                response.Message = "Lỗi cập nhật dữ liệu quốc gia trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [DELETE] -> https://localhost:portnumber/api/v1/country/{id}
        * Description: Xoá dữ liệu quốc gia trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCountry([FromRoute] long id)
        {
            var response = new Response();

            try
            {
                var data = await _countryService.GetCountryById(id);

                if(data == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi xoá quốc gia trên hệ thống vì dữ liệu không tồn tại";
                    response.Data = null;

                    return NotFound(response);
                }

                await _countryService.DeleteCountry(data);

                response.Status = true;
                response.Message = "Xoá dữ liệu quốc gia thành công";
                response.Data = null;

                return Ok(response);
            } catch
            {
                response.Status = false;
                response.Message = "Lỗi xoá dữ liệu quốc gia trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }
    }
}
