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
    * URL: https://localhost:portnumber/api/v1/city
    */
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class CityController : Controller
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/city/all
        * Description: Lấy dữ liệu về tất cả thành phố trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllCities()
        {
            var response = new Response();

            try
            {
                var data = await _cityService.GetAllCities();

                response.Status = true;
                response.Message = "Lấy dữ liệu về tất cả thành phố thành công";
                response.Data = data;

                return Ok(response);
            } catch
            {
                response.Status = false;
                response.Message = "Lỗi lấy dữ liệu về tất cả thành phố";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/city/{idQuocGia}/country
        * Description: Lấy dữ liệu về tất cả thành phố dựa theo trường idQuocgia trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpGet]
        [Route("{idQuocGia}/country")]
        public async Task<IActionResult> GetCitiesByIdCountry([FromRoute] long idQuocGia)
        {
            var response = new Response();

            try
            {
                var data = await _cityService.GetCitiesByIdCountry(idQuocGia);

                response.Status = true;
                response.Message = "Lấy dữ liệu về tất cả thành phố theo quốc gia thành công";
                response.Data = data;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi lấy dữ liệu về tất cả thành phố theo quốc gia";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/city/{id}
        * Description: Lấy dữ liệu về thành phố dựa theo trường id trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCityById([FromRoute] long id)
        {
            var response = new Response();

            try
            {
                var data = await _cityService.GetCityById(id);

                response.Status = true;
                response.Message = "Lấy dữ liệu về thành phố thành công";
                response.Data = data;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi lấy dữ liệu về thành phố";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/city?start=0&size=100&search=
        * Description: Lấy dữ liệu về thành phố trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpGet]
        public async Task<IActionResult> GetCities([FromQuery(Name = "start")] int Start = 0, [FromQuery(Name = "size")] int Size = 10, [FromQuery(Name = "search")] string Search = "")
        {
            var response = new ResponsePage();

            try
            {
                var search = Search.ToLower().Trim();
                var data = await _cityService.GetCities(Start, Size, search);
                var totalRow = await _cityService.GetTotalCities(search);

                response.Status = true;
                response.Message = "Lấy dữ liệu về thành phố thành công";
                response.Data = data;
                response.TotalRowCount = totalRow;

                return Ok(response);
            } catch
            {
                response.Status = false;
                response.Message = "Lỗi lấy dữ liệu về thành phố";
                response.Data = null;
                response.TotalRowCount = 0;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [POST] -> https://localhost:portnumber/api/v1/city
        * Description: Thêm mới dữ liệu về thành phố trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpPost]
        public async Task<IActionResult> CreateCity([FromBody] CreateCityRequest req)
        {
            var response = new Response();

            try
            {
                await _cityService.CreateCity(req);

                response.Status = true;
                response.Message = "Tạo thành phố thành công trên hệ thống";
                response.Data = null;

                return StatusCode(201, response);
            } catch
            {
                response.Status = false;
                response.Message = "Lỗi tạo thành phố trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [PUT] -> https://localhost:portnumber/api/v1/city/{id}
        * Description: Cập nhật dữ liệu về thành phố trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCity([FromRoute] long id, [FromBody] UpdateCityRequest req)
        {
            var response = new Response();

            try
            {
                if(id != req.Id)
                {
                    response.Status = false;
                    response.Message = "Lỗi cập nhật thành phố trên hệ thống";
                    response.Data = null;

                    return BadRequest(response);
                }

                var data = await _cityService.GetCityById(id);

                if(data == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi cập nhật thành phố trên hệ thống vì dữ liệu không tồn tại";
                    response.Data = null;

                    return NotFound(response);
                }

                if(data != null)
                {
                    await _cityService.UpdateCity(data, req);
                }

                response.Status = true;
                response.Message = "Bạn đã cập nhật thành phố thành công trên hệ thống";
                response.Data = null;

                return Ok(response);
            } catch
            {
                response.Status = false;
                response.Message = "Lỗi cập nhật thành phố trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [DELETE] -> https://localhost:portnumber/api/v1/city/{id}
        * Description: Xoá dữ liệu về thành phố trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCity([FromRoute] long id)
        {
            var response = new Response();

            try
            {
                var data = await _cityService.GetCityById(id);

                if(data == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi xoá thành phố trên hệ thống vì dữ liệu không tồn tại";
                    response.Data = null;

                    return NotFound(response);
                }

                await _cityService.DeleteCity(data);

                response.Status = true;
                response.Message = "Xoá thành phố thành công";
                response.Data = null;

                return Ok(response);
            } catch
            {
                response.Status = false;
                response.Message = "Lỗi xoá thành phố trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }
    }
}
