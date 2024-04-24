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
    * URL: https://localhost:portnumber/api/v1/customerinfo
    */
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class CustomerInfoController : Controller
    {
        private readonly ICustomerInfoService _customerInfoService;

        public CustomerInfoController(ICustomerInfoService customerInfoService)
        {
            _customerInfoService = customerInfoService;
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/customerinfo/imex/{idCustomer}?start=0&size=100&search=
        * Description: Lấy dữ liệu về danh sách xuất nhập khẩu của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpGet]
        [Route("imex/{idCustomer}")]
        public async Task<IActionResult> GetCustomerListImEx([FromQuery(Name = "start")] int Start = 0, [FromQuery(Name = "size")] int Size = 10, [FromQuery(Name = "search")] string Search = "", [FromRoute] long idCustomer = 0)
        {
            var response = new ResponsePage();

            try
            {
                var search = Search.ToLower().Trim();
                var data = await _customerInfoService.GetCustomerListImEx(Start, Size, search, idCustomer);
                var totalRow = await _customerInfoService.GetTotalCustomerListImEx(search, idCustomer);

                response.Status = true;
                response.Message = "Lấy dữ liệu về danh sách xuất nhập khẩu thành công";
                response.Data = data;
                response.TotalRowCount = totalRow;

                return Ok(response);
            } catch
            {
                response.Status = false;
                response.Message = "Lỗi lấy dữ liệu về danh sách xuất nhập khẩu";
                response.Data = null;
                response.TotalRowCount = 0;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [POST] -> https://localhost:portnumber/api/v1/customerinfo/imex
        * Description: Thêm mới dữ liệu về dữ liệu xuất nhập khẩu của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpPost]
        [Route("imex")]
        public async Task<IActionResult> CreateImEx([FromBody] CreateCustomerImExRequest req)
        {
            var response = new Response();

            try
            {
                await _customerInfoService.CreateCustomerImEx(req);

                response.Status = true;
                response.Message = "Tạo dữ liệu xuất nhập khẩu thành công trên hệ thống";
                response.Data = null;

                return StatusCode(201, response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi tạo dữ liệu xuất nhập khẩu trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [PUT] -> https://localhost:portnumber/api/v1/customerinfo/imex/{id}
        * Description: Cập nhật về dữ liệu xuất nhập khẩu của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpPut]
        [Route("imex/{id}")]
        public async Task<IActionResult> UpdateImEx([FromBody] UpdateCustomerImExRequest req, [FromRoute] long id = 0)
        {
            var response = new Response();

            try
            {
                if (id != req.Id)
                {
                    response.Status = false;
                    response.Message = "Lỗi cập nhật dữ liệu xuất nhập khẩu trên hệ thống";
                    response.Data = null;

                    return BadRequest(response);
                }

                var data = await _customerInfoService.GetCustomerImExById(id);

                if (data == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi cập nhật dữ liệu xuất nhập khẩu trên hệ thống vì dữ liệu không tồn tại";
                    response.Data = null;

                    return NotFound(response);
                }

                if (data != null)
                {
                    await _customerInfoService.UpdateCustomerImEx(data, req);
                }

                response.Status = true;
                response.Message = "Bạn đã cập nhật dữ liệu xuất nhập khẩu thành công trên hệ thống";
                response.Data = null;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi cập nhật dữ liệu xuất nhập khẩu trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [DELETE] -> https://localhost:portnumber/api/v1/customerinfo/imex/{id}
        * Description: Xoá dữ liệu xuất nhập khẩu của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpDelete]
        [Route("imex/{id}")]
        public async Task<IActionResult> DeleteImEx([FromRoute] long id)
        {
            var response = new Response();

            try
            {
                var data = await _customerInfoService.GetCustomerImExById(id);

                if (data == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi xoá dữ liệu xuất nhập khẩu trên hệ thống vì dữ liệu không tồn tại";
                    response.Data = null;

                    return NotFound(response);
                }

                await _customerInfoService.DeleteCustomerImEx(data);

                response.Status = true;
                response.Message = "Xoá dữ liệu xuất nhập khẩu thành công";
                response.Data = null;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi xoá dữ liệu xuất nhập khẩu trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/customerinfo/operational/{idCustomer}?start=0&size=100&search=
        * Description: Lấy dữ liệu về danh sách tác nghiệp của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpGet]
        [Route("operational/{idCustomer}")]
        public async Task<IActionResult> GetCustomerOperationals([FromQuery(Name = "start")] int Start = 0, [FromQuery(Name = "size")] int Size = 10, [FromQuery(Name = "search")] string Search = "", [FromRoute] long idCustomer = 0)
        {
            var response = new ResponsePage();

            try
            {
                var search = Search.ToLower().Trim();
                var data = await _customerInfoService.GetCustomerOperationals(Start, Size, search, idCustomer);
                var totalRow = await _customerInfoService.GetTotalCustomerOperationals(search, idCustomer);

                response.Status = true;
                response.Message = "Lấy dữ liệu về danh sách tác nghiệp thành công";
                response.Data = data;
                response.TotalRowCount = totalRow;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi lấy dữ liệu về danh sách tác nghiệp";
                response.Data = null;
                response.TotalRowCount = 0;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [POST] -> https://localhost:portnumber/api/v1/customerinfo/operational
        * Description: Thêm mới dữ liệu về dữ liệu tác nghiệp của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpPost]
        [Route("operational")]
        public async Task<IActionResult> CreateOperational([FromBody] CreateCustomerOperationalRequest req)
        {
            var response = new Response();

            try
            {
                await _customerInfoService.CreateCustomerOperational(req);

                response.Status = true;
                response.Message = "Tạo dữ liệu tác nghiệp thành công trên hệ thống";
                response.Data = null;

                return StatusCode(201, response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi tạo dữ liệu tác nghiệp trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [PUT] -> https://localhost:portnumber/api/v1/customerinfo/operational/{id}
        * Description: Cập nhật về dữ liệu tác nghiệp của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpPut]
        [Route("operational/{id}")]
        public async Task<IActionResult> UpdateOperational([FromBody] UpdateCustomerOperationalRequest req, [FromRoute] long id = 0)
        {
            var response = new Response();

            try
            {
                if (id != req.Id)
                {
                    response.Status = false;
                    response.Message = "Lỗi cập nhật dữ liệu tác nghiệp trên hệ thống";
                    response.Data = null;

                    return BadRequest(response);
                }

                var data = await _customerInfoService.GetCustomerOperationalById(id);

                if (data == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi cập nhật dữ liệu tác nghiệp trên hệ thống vì dữ liệu không tồn tại";
                    response.Data = null;

                    return NotFound(response);
                }

                if (data != null)
                {
                    await _customerInfoService.UpdateCustomerOperational(data, req);
                }

                response.Status = true;
                response.Message = "Bạn đã cập nhật dữ liệu tác nghiệp thành công trên hệ thống";
                response.Data = null;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi cập nhật dữ liệu tác nghiệp trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [DELETE] -> https://localhost:portnumber/api/v1/customerinfo/operational/{id}
        * Description: Xoá dữ liệu tác nghiệp của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpDelete]
        [Route("operational/{id}")]
        public async Task<IActionResult> DeleteOperational([FromRoute] long id)
        {
            var response = new Response();

            try
            {
                var data = await _customerInfoService.GetCustomerOperationalById(id);

                if (data == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi xoá dữ liệu tác nghiệp trên hệ thống vì dữ liệu không tồn tại";
                    response.Data = null;

                    return NotFound(response);
                }

                await _customerInfoService.DeleteCustomerOperational(data);

                response.Status = true;
                response.Message = "Xoá dữ liệu tác nghiệp thành công";
                response.Data = null;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi xoá dữ liệu tác nghiệp trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/customerinfo/contact/{idCustomer}/all
        * Description: Lấy dữ liệu về danh sách tất cả người liên hệ của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpGet]
        [Route("contact/{idCustomer}/all")]
        public async Task<IActionResult> GetAllCustomerContacts([FromRoute] long idCustomer = 0)
        {
            var response = new Response();

            try
            {
                var data = await _customerInfoService.GetAllCustomerContacts(idCustomer);

                response.Status = true;
                response.Message = "Lấy dữ liệu về danh sách tất cả người liên hệ của một khách hàng thành công";
                response.Data = data;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi lấy dữ liệu về danh sách tất cả người liên hệ của một khách hàng";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/customerinfo/contact/{idCustomer}?start=0&size=100&search=
        * Description: Lấy dữ liệu về danh sách người liên hệ của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpGet]
        [Route("contact/{idCustomer}")]
        public async Task<IActionResult> GetCustomerContacts([FromQuery(Name = "start")] int Start = 0, [FromQuery(Name = "size")] int Size = 10, [FromQuery(Name = "search")] string Search = "", [FromRoute] long idCustomer = 0)
        {
            var response = new ResponsePage();

            try
            {
                var search = Search.ToLower().Trim();
                var data = await _customerInfoService.GetCustomerContacts(Start, Size, search, idCustomer);
                var totalRow = await _customerInfoService.GetTotalCustomerContacts(search, idCustomer);

                response.Status = true;
                response.Message = "Lấy dữ liệu về danh sách người liên hệ thành công";
                response.Data = data;
                response.TotalRowCount = totalRow;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi lấy dữ liệu về danh sách người liên hệ";
                response.Data = null;
                response.TotalRowCount = 0;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [POST] -> https://localhost:portnumber/api/v1/customerinfo/contact
        * Description: Thêm mới dữ liệu về dữ liệu người liên hệ của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpPost]
        [Route("contact")]
        public async Task<IActionResult> CreateContact([FromBody] CreateCustomerContactRequest req)
        {
            var response = new Response();

            try
            {
                await _customerInfoService.CreateCustomerContact(req);

                response.Status = true;
                response.Message = "Tạo dữ liệu người liên hệ thành công trên hệ thống";
                response.Data = null;

                return StatusCode(201, response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi tạo dữ liệu người liên hệ trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [PUT] -> https://localhost:portnumber/api/v1/customerinfo/contact/{id}
        * Description: Cập nhật về dữ liệu người liên hệ của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpPut]
        [Route("contact/{id}")]
        public async Task<IActionResult> UpdateContact([FromBody] UpdateCustomerContactRequest req, [FromRoute] long id = 0)
        {
            var response = new Response();

            try
            {
                if (id != req.Id)
                {
                    response.Status = false;
                    response.Message = "Lỗi cập nhật dữ liệu người liên hệ trên hệ thống";
                    response.Data = null;

                    return BadRequest(response);
                }

                var data = await _customerInfoService.GetCustomerContactById(id);

                if (data == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi cập nhật dữ liệu người liên hệ trên hệ thống vì dữ liệu không tồn tại";
                    response.Data = null;

                    return NotFound(response);
                }

                if (data != null)
                {
                    await _customerInfoService.UpdateCustomerContact(data, req);
                }

                response.Status = true;
                response.Message = "Bạn đã cập nhật dữ liệu người liên hệ thành công trên hệ thống";
                response.Data = null;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi cập nhật dữ liệu người liên hệ trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [DELETE] -> https://localhost:portnumber/api/v1/customerinfo/contact/{id}
        * Description: Xoá dữ liệu người liên hệ của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpDelete]
        [Route("contact/{id}")]
        public async Task<IActionResult> DeleteContact([FromRoute] long id)
        {
            var response = new Response();

            try
            {
                var data = await _customerInfoService.GetCustomerContactById(id);

                if (data == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi xoá dữ liệu người liên hệ trên hệ thống vì dữ liệu không tồn tại";
                    response.Data = null;

                    return NotFound(response);
                }

                await _customerInfoService.DeleteCustomerContact(data);

                response.Status = true;
                response.Message = "Xoá dữ liệu người liên hệ thành công";
                response.Data = null;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi xoá dữ liệu người liên hệ trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/customerinfo/evaluate/{idCustomer}?start=0&size=100&search=
        * Description: Lấy dữ liệu về danh sách đánh giá của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpGet]
        [Route("evaluate/{idCustomer}")]
        public async Task<IActionResult> GetCustomerEvaluates([FromQuery(Name = "start")] int Start = 0, [FromQuery(Name = "size")] int Size = 10, [FromQuery(Name = "search")] string Search = "", [FromRoute] long idCustomer = 0)
        {
            var response = new ResponsePage();

            try
            {
                var search = Search.ToLower().Trim();
                var data = await _customerInfoService.GetCustomerEvaluates(Start, Size, search, idCustomer);
                var totalRow = await _customerInfoService.GetTotalCustomerEvaluates(search, idCustomer);

                response.Status = true;
                response.Message = "Lấy dữ liệu về danh sách đánh giá thành công";
                response.Data = data;
                response.TotalRowCount = totalRow;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi lấy dữ liệu về danh sách đánh giá";
                response.Data = null;
                response.TotalRowCount = 0;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [POST] -> https://localhost:portnumber/api/v1/customerinfo/evaluate
        * Description: Thêm mới dữ liệu về dữ liệu đánh giá của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpPost]
        [Route("evaluate")]
        public async Task<IActionResult> CreateEvaluate([FromBody] CreateCustomerEvaluateRequest req)
        {
            var response = new Response();

            try
            {
                await _customerInfoService.CreateCustomerEvaluate(req);

                response.Status = true;
                response.Message = "Tạo dữ liệu đánh giá thành công trên hệ thống";
                response.Data = null;

                return StatusCode(201, response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi tạo dữ liệu đánh giá trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [PUT] -> https://localhost:portnumber/api/v1/customerinfo/evaluate/{id}
        * Description: Cập nhật về dữ liệu đánh giá của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpPut]
        [Route("evaluate/{id}")]
        public async Task<IActionResult> UpdateEvaluate([FromBody] UpdateCustomerEvaluateRequest req, [FromRoute] long id = 0)
        {
            var response = new Response();

            try
            {
                if (id != req.Id)
                {
                    response.Status = false;
                    response.Message = "Lỗi cập nhật dữ liệu đánh giá trên hệ thống";
                    response.Data = null;

                    return BadRequest(response);
                }

                var data = await _customerInfoService.GetCustomerEvaluateById(id);

                if (data == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi cập nhật dữ liệu đánh giá trên hệ thống vì dữ liệu không tồn tại";
                    response.Data = null;

                    return NotFound(response);
                }

                if (data != null)
                {
                    await _customerInfoService.UpdateCustomerEvaluate(data, req);
                }

                response.Status = true;
                response.Message = "Bạn đã cập nhật dữ liệu đánh giá thành công trên hệ thống";
                response.Data = null;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi cập nhật dữ liệu đánh giá trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [DELETE] -> https://localhost:portnumber/api/v1/customerinfo/evaluate/{id}
        * Description: Xoá dữ liệu đánh giá của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpDelete]
        [Route("evaluate/{id}")]
        public async Task<IActionResult> DeleteEvaluate([FromRoute] long id)
        {
            var response = new Response();

            try
            {
                var data = await _customerInfoService.GetCustomerEvaluateById(id);

                if (data == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi xoá dữ liệu đánh giá trên hệ thống vì dữ liệu không tồn tại";
                    response.Data = null;

                    return NotFound(response);
                }

                await _customerInfoService.DeleteCustomerEvaluate(data);

                response.Status = true;
                response.Message = "Xoá dữ liệu đánh giá thành công";
                response.Data = null;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi xoá dữ liệu đánh giá trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/customerinfo/classify/{idCustomer}?start=0&size=100&search=
        * Description: Lấy dữ liệu về danh sách phân loại khách hàng của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpGet]
        [Route("classify/{idCustomer}")]
        public async Task<IActionResult> GetCustomerClassifies([FromQuery(Name = "start")] int Start = 0, [FromQuery(Name = "size")] int Size = 10, [FromQuery(Name = "search")] string Search = "", [FromRoute] long idCustomer = 0)
        {
            var response = new ResponsePage();

            try
            {
                var search = Search.ToLower().Trim();
                var data = await _customerInfoService.GetCustomerClassifies(Start, Size, search, idCustomer);
                var totalRow = await _customerInfoService.GetTotalCustomerClassifies(search, idCustomer);

                response.Status = true;
                response.Message = "Lấy dữ liệu về danh sách phân loại khách hàng thành công";
                response.Data = data;
                response.TotalRowCount = totalRow;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi lấy dữ liệu về danh sách phân loại khách hàng";
                response.Data = null;
                response.TotalRowCount = 0;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [POST] -> https://localhost:portnumber/api/v1/customerinfo/classify
        * Description: Thêm mới dữ liệu về dữ liệu phân loại khách hàng của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpPost]
        [Route("classify")]
        public async Task<IActionResult> CreateClassify([FromBody] CreateCustomerClassifyRequest req)
        {
            var response = new Response();

            try
            {
                await _customerInfoService.CreateCustomerClassify(req);

                response.Status = true;
                response.Message = "Tạo dữ liệu phân loại khách hàng thành công trên hệ thống";
                response.Data = null;

                return StatusCode(201, response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi tạo dữ liệu phân loại khách hàng trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [PUT] -> https://localhost:portnumber/api/v1/customerinfo/classify/{id}
        * Description: Cập nhật về dữ liệu phân loại khách hàng của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpPut]
        [Route("classify/{id}")]
        public async Task<IActionResult> UpdateClassify([FromBody] UpdateCustomerClassifyRequest req, [FromRoute] long id = 0)
        {
            var response = new Response();

            try
            {
                if (id != req.Id)
                {
                    response.Status = false;
                    response.Message = "Lỗi cập nhật dữ liệu phân loại khách hàng trên hệ thống";
                    response.Data = null;

                    return BadRequest(response);
                }

                var data = await _customerInfoService.GetCustomerClassifyById(id);

                if (data == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi cập nhật dữ liệu phân loại khách hàng trên hệ thống vì dữ liệu không tồn tại";
                    response.Data = null;

                    return NotFound(response);
                }

                if (data != null)
                {
                    await _customerInfoService.UpdateCustomerClassify(data, req);
                }

                response.Status = true;
                response.Message = "Bạn đã cập nhật dữ liệu phân loại khách hàng thành công trên hệ thống";
                response.Data = null;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi cập nhật dữ liệu phân loại khách hàng trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [DELETE] -> https://localhost:portnumber/api/v1/customerinfo/classify/{id}
        * Description: Xoá dữ liệu phân loại khách hàng của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpDelete]
        [Route("classify/{id}")]
        public async Task<IActionResult> DeleteClassify([FromRoute] long id)
        {
            var response = new Response();

            try
            {
                var data = await _customerInfoService.GetCustomerClassifyById(id);

                if (data == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi xoá dữ liệu phân loại khách hàng trên hệ thống vì dữ liệu không tồn tại";
                    response.Data = null;

                    return NotFound(response);
                }

                await _customerInfoService.DeleteCustomerClassify(data);

                response.Status = true;
                response.Message = "Xoá dữ liệu phân loại khách hàng thành công";
                response.Data = null;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi xoá dữ liệu phân loại khách hàng trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/customerinfo/major/{idCustomer}?start=0&size=100&search=
        * Description: Lấy dữ liệu về danh sách nghiệp vụ của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpGet]
        [Route("major/{idCustomer}")]
        public async Task<IActionResult> GetCustomerMajors([FromQuery(Name = "start")] int Start = 0, [FromQuery(Name = "size")] int Size = 10, [FromQuery(Name = "search")] string Search = "", [FromRoute] long idCustomer = 0)
        {
            var response = new ResponsePage();

            try
            {
                var search = Search.ToLower().Trim();
                var data = await _customerInfoService.GetCustomerMajors(Start, Size, search, idCustomer);
                var totalRow = await _customerInfoService.GetTotalCustomerMajors(search, idCustomer);

                response.Status = true;
                response.Message = "Lấy dữ liệu về danh sách nghiệp vụ thành công";
                response.Data = data;
                response.TotalRowCount = totalRow;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi lấy dữ liệu về danh sách nghiệp vụ";
                response.Data = null;
                response.TotalRowCount = 0;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [POST] -> https://localhost:portnumber/api/v1/customerinfo/major
        * Description: Thêm mới dữ liệu về dữ liệu nghiệp vụ của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpPost]
        [Route("major")]
        public async Task<IActionResult> CreateMajor([FromBody] CreateCustomerMajorRequest req)
        {
            var response = new Response();

            try
            {
                await _customerInfoService.CreateCustomerMajor(req);

                response.Status = true;
                response.Message = "Tạo dữ liệu nghiệp vụ thành công trên hệ thống";
                response.Data = null;

                return StatusCode(201, response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi tạo dữ liệu nghiệp vụ trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [PUT] -> https://localhost:portnumber/api/v1/customerinfo/major/{id}
        * Description: Cập nhật về dữ liệu nghiệp vụ của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpPut]
        [Route("major/{id}")]
        public async Task<IActionResult> UpdateMajor([FromBody] UpdateCustomerMajorRequest req, [FromRoute] long id = 0)
        {
            var response = new Response();

            try
            {
                if (id != req.Id)
                {
                    response.Status = false;
                    response.Message = "Lỗi cập nhật dữ liệu nghiệp vụ trên hệ thống";
                    response.Data = null;

                    return BadRequest(response);
                }

                var data = await _customerInfoService.GetCustomerMajorById(id);

                if (data == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi cập nhật dữ liệu nghiệp vụ trên hệ thống vì dữ liệu không tồn tại";
                    response.Data = null;

                    return NotFound(response);
                }

                if (data != null)
                {
                    await _customerInfoService.UpdateCustomerMajor(data, req);
                }

                response.Status = true;
                response.Message = "Bạn đã cập nhật dữ liệu nghiệp vụ thành công trên hệ thống";
                response.Data = null;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi cập nhật dữ liệu nghiệp vụ trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [DELETE] -> https://localhost:portnumber/api/v1/customerinfo/major/{id}
        * Description: Xoá dữ liệu nghiệp vụ của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpDelete]
        [Route("major/{id}")]
        public async Task<IActionResult> DeleteMajor([FromRoute] long id)
        {
            var response = new Response();

            try
            {
                var data = await _customerInfoService.GetCustomerMajorById(id);

                if (data == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi xoá dữ liệu nghiệp vụ trên hệ thống vì dữ liệu không tồn tại";
                    response.Data = null;

                    return NotFound(response);
                }

                await _customerInfoService.DeleteCustomerMajor(data);

                response.Status = true;
                response.Message = "Xoá dữ liệu nghiệp vụ thành công";
                response.Data = null;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi xoá dữ liệu nghiệp vụ trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/customerinfo/route/{idCustomer}?start=0&size=100&search=
        * Description: Lấy dữ liệu về danh sách tuyến hàng của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpGet]
        [Route("route/{idCustomer}")]
        public async Task<IActionResult> GetCustomerRoutes([FromQuery(Name = "start")] int Start = 0, [FromQuery(Name = "size")] int Size = 10, [FromQuery(Name = "search")] string Search = "", [FromRoute] long idCustomer = 0)
        {
            var response = new ResponsePage();

            try
            {
                var search = Search.ToLower().Trim();
                var data = await _customerInfoService.GetCustomerRoutes(Start, Size, search, idCustomer);
                var totalRow = await _customerInfoService.GetTotalCustomerRoutes(search, idCustomer);

                response.Status = true;
                response.Message = "Lấy dữ liệu về danh sách tuyến hàng thành công";
                response.Data = data;
                response.TotalRowCount = totalRow;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi lấy dữ liệu về danh sách tuyến hàng";
                response.Data = null;
                response.TotalRowCount = 0;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [POST] -> https://localhost:portnumber/api/v1/customerinfo/route
        * Description: Thêm mới dữ liệu về dữ liệu tuyến hàng của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpPost]
        [Route("route")]
        public async Task<IActionResult> CreateRoute([FromBody] CreateCustomerRouteRequest req)
        {
            var response = new Response();

            try
            {
                await _customerInfoService.CreateCustomerRoute(req);

                response.Status = true;
                response.Message = "Tạo dữ liệu tuyến hàng thành công trên hệ thống";
                response.Data = null;

                return StatusCode(201, response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi tạo dữ liệu tuyến hàng trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [PUT] -> https://localhost:portnumber/api/v1/customerinfo/route/{id}
        * Description: Cập nhật về dữ liệu tuyến hàng của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpPut]
        [Route("route/{id}")]
        public async Task<IActionResult> UpdateRoute([FromBody] UpdateCustomerRouteRequest req, [FromRoute] long id = 0)
        {
            var response = new Response();

            try
            {
                if (id != req.Id)
                {
                    response.Status = false;
                    response.Message = "Lỗi cập nhật dữ liệu tuyến hàng trên hệ thống";
                    response.Data = null;

                    return BadRequest(response);
                }

                var data = await _customerInfoService.GetCustomerRouteById(id);

                if (data == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi cập nhật dữ liệu tuyến hàng trên hệ thống vì dữ liệu không tồn tại";
                    response.Data = null;

                    return NotFound(response);
                }

                if (data != null)
                {
                    await _customerInfoService.UpdateCustomerRoute(data, req);
                }

                response.Status = true;
                response.Message = "Bạn đã cập nhật dữ liệu tuyến hàng thành công trên hệ thống";
                response.Data = null;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi cập nhật dữ liệu tuyến hàng trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [DELETE] -> https://localhost:portnumber/api/v1/customerinfo/route/{id}
        * Description: Xoá dữ liệu tuyến hàng của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpDelete]
        [Route("route/{id}")]
        public async Task<IActionResult> DeleteRoute([FromRoute] long id)
        {
            var response = new Response();

            try
            {
                var data = await _customerInfoService.GetCustomerRouteById(id);

                if (data == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi xoá dữ liệu tuyến hàng trên hệ thống vì dữ liệu không tồn tại";
                    response.Data = null;

                    return NotFound(response);
                }

                await _customerInfoService.DeleteCustomerRoute(data);

                response.Status = true;
                response.Message = "Xoá dữ liệu tuyến hàng thành công";
                response.Data = null;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi xoá dữ liệu tuyến hàng trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }
    }
}
