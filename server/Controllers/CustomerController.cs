// ** library **
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
// ** architecture **
using locy_api.Models.Response;
using locy_api.Interfaces;
using locy_api.Models.Requests;

namespace locy_api.Controllers
{
    /*
    * URL: https://localhost:portnumber/api/v1/customer
    */
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/customer
        * Description: Lấy dữ liệu về khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCustomers([FromQuery(Name = "start")] int Start = 0, [FromQuery(Name = "size")] int Size = 10, [FromQuery(Name = "search")] string Search = "")
        {
            var response = new ResponsePage();

            try
            {
                var search = Search.ToLower().Trim();
                var data = await _customerService.GetCustomers(Start, Size, search);
                var total = await _customerService.GetTotalCustomers(search);

                response.Status = true;
                response.Message = "Lấy dữ liệu loại khách hàng thành công";
                response.Data = data;
                response.TotalRowCount = total;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi lấy dữ liệu loại khách hàng";
                response.Data = null;
                response.TotalRowCount = 0;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/customer/delete
        * Description: Người dùng thực hiện lấy danh sách khách hàng đã bị xoá trên hệ thống
        */
        [Authorize("ManageCustomer")]
        [HttpGet]
        [Route("delete")]
        public async Task<IActionResult> GetCustomersDelete([FromQuery(Name = "start")] int Start = 0, [FromQuery(Name = "size")] int Size = 10, [FromQuery(Name = "search")] string Search = "")
        {
            var response = new ResponsePage();

            try
            {
                var search = Search.ToLower().Trim();
                var data = await _customerService.GetCustomers(Start, Size, search, true);
                var total = await _customerService.GetTotalCustomers(search, true);

                response.Status = true;
                response.Message = "Lấy dữ liệu về tất cả khách hàng đã bị xoá thành công";
                response.Data = data;
                response.TotalRowCount = total;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi lấy dữ liệu về tất cả khách hàng đã bị xoá";
                response.Data = null;
                response.TotalRowCount = 0;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/customer/assign
        * Description: Người dùng thực hiện lấy danh sách đã giao bởi admin hoặc người có quyền giao trên hệ thống
        */
        [Authorize("DeliveryCustomer")]
        [HttpGet]
        [Route("assign")]
        public async Task<IActionResult> GetCustomersAsign([FromQuery(Name = "start")] int Start = 0, [FromQuery(Name = "size")] int Size = 10, [FromQuery(Name = "search")] string Search = "")
        {
            var response = new ResponsePage();

            try
            {
                var search = Search.ToLower().Trim();

                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var idUser = long.Parse(identity?.Claims.FirstOrDefault(o => o.Type == "Id")?.Value ?? "0");
                var permission = identity?.Claims.FirstOrDefault(o => o.Type == "Permission")?.Value ?? "";
                var data = await _customerService.GetCustomersAssigned(Start, Size, search, permission, idUser);
                var total = await _customerService.GetTotalCustomersAssigned(search, permission, idUser);

                response.Status = true;
                response.Message = "Lấy dữ liệu về danh sách đã giao bởi admin hoặc người có quyền giao thành công";
                response.Data = data;
                response.TotalRowCount = total;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi lấy dữ liệu về danh sách đã giao bởi admin hoặc người có quyền giao";
                response.Data = null;
                response.TotalRowCount = 0;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/customer/delivered
        * Description: Người dùng thực hiện lấy danh sách được giao trên hệ thống
        */
        [Authorize]
        [HttpGet]
        [Route("delivered")]
        public async Task<IActionResult> GetCustomersDelivered([FromQuery(Name = "start")] int Start = 0, [FromQuery(Name = "size")] int Size = 10, [FromQuery(Name = "search")] string Search = "")
        {
            var response = new ResponsePage();

            try
            {
                var search = Search.ToLower().Trim();

                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var idNhanVien = long.Parse(identity?.Claims.FirstOrDefault(o => o.Type == "IDEmployee")?.Value ?? "0");
                var data = await _customerService.GetCustomersDelivered(Start, Size, search, idNhanVien);
                var total = await _customerService.GetTotalCustomersDelivered(search, idNhanVien);

                response.Status = true;
                response.Message = "Lấy dữ liệu về danh sách được giao thành công";
                response.Data = data;
                response.TotalRowCount = total;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi lấy dữ liệu về danh sách đã giao";
                response.Data = null;
                response.TotalRowCount = 0;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/customer/undelivered
        * Description: Người dùng thực hiện lấy danh sách chưa giao trên hệ thống
        */
        [Authorize("DeliveryCustomer")]
        [HttpGet]
        [Route("undelivered")]
        public async Task<IActionResult> GetCustomersUndelivered([FromQuery(Name = "start")] int Start = 0, [FromQuery(Name = "size")] int Size = 10, [FromQuery(Name = "search")] string Search = "")
        {
            var response = new ResponsePage();

            try
            {
                var search = Search.ToLower().Trim();
                var data = await _customerService.GetCustomersUndelivered(Start, Size, search);
                var total = await _customerService.GetTotalCustomersUndelivered(search);

                response.Status = true;
                response.Message = "Lấy dữ liệu về danh sách chưa giao thành công";
                response.Data = data;
                response.TotalRowCount = total;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi lấy dữ liệu về danh sách chưa giao";
                response.Data = null;
                response.TotalRowCount = 0;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/customer/received
        * Description: Người dùng thực hiện lấy danh sách đã nhận trên hệ thống
        */
        [Authorize]
        [HttpGet]
        [Route("received")]
        public async Task<IActionResult> GetCustomersReceived([FromQuery(Name = "start")] int Start = 0, [FromQuery(Name = "size")] int Size = 10, [FromQuery(Name = "search")] string Search = "")
        {
            var response = new ResponsePage();

            try
            {
                var search = Search.ToLower().Trim();

                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var permission = identity?.Claims.FirstOrDefault(o => o.Type == "Permission")?.Value ?? "";
                var idNhanVien = long.Parse(identity?.Claims.FirstOrDefault(o => o.Type == "IDEmployee")?.Value ?? "0");
                var data = await _customerService.GetCustomersReceived(Start, Size, search, permission, idNhanVien);
                var total = await _customerService.GetTotalCustomersReceived(search, permission, idNhanVien);

                response.Status = true;
                response.Message = "Lấy dữ liệu về danh sách đã nhận thành công";
                response.Data = data;
                response.TotalRowCount = total;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi lấy dữ liệu về danh sách đã nhận";
                response.Data = null;
                response.TotalRowCount = 0;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [POST] -> https://localhost:portnumber/api/v1/customer
        * Description: Người dùng thực hiện tạo mới khách hàng trên hệ thống
        */
        [Authorize("ManageCustomer")]
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest req)
        {
            var response = new Response();

            try
            {
                var isExistCode = await _customerService.IsExistCodeCustomer(req.Code ?? "");
                var isExistTaxCode = await _customerService.IsExistTaxCodeCustomer(req.TaxCode ?? "");

                if(isExistCode)
                {
                    response.Status = false;
                    response.Message = "Mã khách hàng đã tồn tại";
                    response.Data = null;

                    return StatusCode(409, response);
                }

                if (isExistTaxCode)
                {
                    response.Status = false;
                    response.Message = "Mã số thuế khách hàng đã tồn tại";
                    response.Data = null;

                    return StatusCode(409, response);
                }

                await _customerService.CreateCustomer(req);

                response.Status = true;
                response.Message = "Bạn đã tạo khách hàng thành công";
                response.Data = null;

                return StatusCode(201, response);
            } catch
            {
                response.Status = false;
                response.Message = "Lỗi tạo khách hàng trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [PUT] -> https://localhost:portnumber/api/v1/customer/{id}
        * Description: Người dùng thực hiện cập nhật thông tin khách hàng trên hệ thống
        */
        [Authorize("ManageCustomer")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCustomer([FromRoute] long id, [FromBody] UpdateCustomerRequest req)
        {
            var response = new Response();

            try
            {
                if(id != req.Id)
                {
                    response.Status = false;
                    response.Message = "Lỗi cập nhật khách hàng trên hệ thống";
                    response.Data = null;

                    return BadRequest(response);
                }

                var data = await _customerService.GetCustomerById(id);

                if(data == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi cập nhật khách hàng vì khách hàng không tồn tại";
                    response.Data = null;

                    return NotFound(response);
                }

                if (data.Code != req.Code)
                {
                    var isExistCode = await _customerService.IsExistCodeCustomer(req.Code ?? "");

                    if (isExistCode)
                    {
                        response.Status = false;
                        response.Message = "Mã khách hàng đã tồn tại";
                        response.Data = null;

                        return StatusCode(409, response);
                    }
                }

                if (data.TaxCode != req.TaxCode)
                {
                    var isExistTaxCode = await _customerService.IsExistTaxCodeCustomer(req.TaxCode ?? "");

                    if (isExistTaxCode)
                    {
                        response.Status = false;
                        response.Message = "Mã số thuế khách hàng đã tồn tại";
                        response.Data = null;

                        return StatusCode(409, response);
                    }
                }

                if (data != null)
                {
                    await _customerService.UpdateCustomer(data, req);
                }

                response.Status = true;
                response.Message = "Bạn đã cập nhật thông tin khách hàng thành công";
                response.Data = null;

                return Ok(response);
            } catch
            {
                response.Status = false;
                response.Message = "Lỗi cập nhật khách hàng trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [PUT] -> https://localhost:portnumber/api/v1/customer/{id}/delete
        * Description: Người dùng thực hiện cập nhật thông tin khách hàng trên hệ thống
        */
        [Authorize("ManageCustomer")]
        [HttpPut]
        [Route("{id}/delete")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] long id, [FromBody] DeleteCustomerRequest req)
        {
            var response = new Response();

            try
            {
                if (id != req.Id)
                {
                    response.Status = false;
                    response.Message = !req.FlagDel ? "Lỗi huỷ xoá nhân viên trên hệ thống" : "Lỗi yêu cầu xoá nhân viên trên hệ thống";
                    response.Data = null;

                    return BadRequest(response);
                }

                var data = await _customerService.GetCustomerById(req.Id);

                if(data == null)
                {
                    response.Status = false;
                    response.Message = !req.FlagDel ? "Lỗi huỷ xoá nhân viên vì nhân viên không tồn tại trên hệ thống" : "Lỗi yêu cầu xoá nhân viên vì nhân viên không tồn tại trên hệ thống";
                    response.Data = null;

                    return NotFound(response);
                }

                if(data != null)
                {
                    await _customerService.DeleteCustomer(data, req);
                }

                response.Status = true;
                response.Message = !req.FlagDel ? "Bạn đã huỷ xoá nhân viên thành công" : "Bạn đã yêu cầu xoá nhân viên thành công";
                response.Data = null;

                return Ok(response);
            } catch
            {
                response.Status = false;
                response.Message = !req.FlagDel ? "Lỗi huỷ xoá nhân viên trên hệ thống" : "Lỗi yêu cầu xoá nhân viên trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [PUT] -> https://localhost:portnumber/api/v1/customer/choose
        * Description: Người dùng thực hiện nhận khách hàng trên hệ thống
        */
        [Authorize]
        [HttpPut]
        [Route("choose")]
        public async Task<IActionResult> ChooseCustomers([FromBody] ChooseCustomerRequest req)
        {
            var response = new Response();

            try
            {
                var data = await _customerService.GetCustomersByIdArray(req.IdCustomers);

                if(data == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi nhận khách hàng trên hệ thống";
                    response.Data = null;

                    return BadRequest(response);
                }

                await _customerService.ChooseCustomers(data, req);

                response.Status = true;
                response.Message = "Bạn đã nhận khách hàng thành công";
                response.Data = null;

                return Ok(response);
            } catch
            {
                response.Status = false;
                response.Message = "Lỗi nhận khách hàng trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [PUT] -> https://localhost:portnumber/api/v1/customer/delivery
        * Description: Người dùng thực hiện giao khách hàng trên hệ thống
        */
        [Authorize("DeliveryCustomer")]
        [HttpPut]
        [Route("delivery")]
        public async Task<IActionResult> DeliveryCustomers([FromBody] DeliveryCustomerRequest req)
        {
            var response = new Response();

            try
            {
                var data = await _customerService.GetCustomersByIdArray(req.IdCustomers);

                if (data == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi giao khách hàng trên hệ thống";
                    response.Data = null;

                    return BadRequest(response);
                }

                await _customerService.DeliveryCustomers(data, req);

                response.Status = true;
                response.Message = "Bạn đã giao khách hàng thành công";
                response.Data = null;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi giao khách hàng trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [PUT] -> https://localhost:portnumber/api/v1/customer/undelivery
        * Description: Người dùng thực hiện huỷ giao khách hàng trên hệ thống
        */
        [Authorize("DeliveryCustomer")]
        [HttpPut]
        [Route("undelivery")]
        public async Task<IActionResult> UndeliveryCustomers([FromBody] UndeliveryCustomerRequest req)
        {
            var response = new Response();

            try
            {
                var data = await _customerService.GetCustomersByIdArray(req.IdCustomers);

                if (data == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi huỷ giao khách hàng trên hệ thống";
                    response.Data = null;

                    return BadRequest(response);
                }

                await _customerService.UndeliveryCustomers(data, req);

                response.Status = true;
                response.Message = "Bạn đã huỷ giao khách hàng thành công";
                response.Data = null;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi huỷ giao khách hàng trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [PUT] -> https://localhost:portnumber/api/v1/customer/accept
        * Description: Người dùng thực hiện nhận khách hàng trên hệ thống
        */
        [Authorize]
        [HttpPut]
        [Route("accept")]
        public async Task<IActionResult> AcceptCustomers([FromBody] AcceptCustomerRequest req)
        {
            var response = new Response();

            try
            {
                var data = await _customerService.GetCustomersByIdArray(req.IdCustomers);

                if (data == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi nhận khách hàng trên hệ thống";
                    response.Data = null;

                    return BadRequest(response);
                }

                await _customerService.AcceptCustomers(data, req);

                response.Status = true;
                response.Message = "Bạn đã nhận khách hàng thành công";
                response.Data = null;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi nhận khách hàng trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [PUT] -> https://localhost:portnumber/api/v1/customer/deny
        * Description: Người dùng thực hiện từ chối khách hàng trên hệ thống
        */
        [Authorize]
        [HttpPut]
        [Route("deny")]
        public async Task<IActionResult> DenyCustomers([FromBody] DenyCustomerRequest req)
        {
            var response = new Response();

            try
            {
                var data = await _customerService.GetCustomersByIdArray(req.IdCustomers);

                if (data == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi từ chối khách hàng trên hệ thống";
                    response.Data = null;

                    return BadRequest(response);
                }

                await _customerService.DenyCustomers(data, req);

                response.Status = true;
                response.Message = "Bạn đã từ chối khách hàng thành công";
                response.Data = null;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi từ chối khách hàng trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/customer/export
        * Description: Người dùng thực hiện export dữ liệu khách hàng trên hệ thống
        */
        [Authorize]
        [HttpGet]
        [Route("export")]
        public async Task<IActionResult> ExportData()
        {
            var response = new Response();

            try
            {
                var pageSize = 1000;
                var pageNumber = 0;
                var filePath = Path.Combine(Path.GetTempPath(), "customer_data.xlsx");

                using (var excelPackage = new ExcelPackage())
                {
                    // Create a worksheet in the package
                    var worksheet = excelPackage.Workbook.Worksheets.Add("Data Export");

                    // Write headers
                    worksheet.Cells[1, 1].Value = "Id";
                    worksheet.Cells[1, 2].Value = "Mã khách hàng";
                    worksheet.Cells[1, 3].Value = "Tên khách hàng (VI)";
                    worksheet.Cells[1, 4].Value = "Tên khách hàng (EN)";
                    worksheet.Cells[1, 5].Value = "Địa chỉ (VI)";
                    worksheet.Cells[1, 6].Value = "Địa chỉ (EN)";
                    worksheet.Cells[1, 7].Value = "Mã số thuế";
                    worksheet.Cells[1, 8].Value = "Số điện thoại";
                    worksheet.Cells[1, 9].Value = "Thư điện tử";
                    worksheet.Cells[1, 10].Value = "Trang web";
                    worksheet.Cells[1, 11].Value = "Ghi chú";
                    worksheet.Cells[1, 12].Value = "Rating";
                    worksheet.Cells[1, 13].Value = "Favorite";
                    worksheet.Cells[1, 14].Value = "Mã ngân hàng";
                    worksheet.Cells[1, 15].Value = "Số tài khoản";
                    worksheet.Cells[1, 16].Value = "Tên ngân hàng";
                    worksheet.Cells[1, 17].Value = "Địa chỉ ngân hàng";
                    worksheet.Cells[1, 18].Value = "FlagCrm";
                    worksheet.Cells[1, 19].Value = "Mã nhân viên sale";
                    worksheet.Cells[1, 20].Value = "Mã người tạo";
                    worksheet.Cells[1, 21].Value = "Ngày tạo";
                    worksheet.Cells[1, 22].Value = "Active";
                    worksheet.Cells[1, 23].Value = "Mã chi nhánh";
                    worksheet.Cells[1, 24].Value = "Mã loại khách hàng";
                    worksheet.Cells[1, 25].Value = "Delete";
                    worksheet.Cells[1, 26].Value = "Mã loại doanh nghiệp";
                    worksheet.Cells[1, 27].Value = "Mã người xoá";
                    worksheet.Cells[1, 28].Value = "Ngày xoá";
                    worksheet.Cells[1, 29].Value = "Lý do xoá";
                    worksheet.Cells[1, 30].Value = "Ngày tương tác";
                    worksheet.Cells[1, 31].Value = "Ngày chọn khách";
                    worksheet.Cells[1, 32].Value = "Ngày trả về";
                    worksheet.Cells[1, 33].Value = "Ngày chốt khách";
                    worksheet.Cells[1, 34].Value = "SttmaxTacNghiep";
                    worksheet.Cells[1, 35].Value = "Ngày tác nghiệp";
                    worksheet.Cells[1, 36].Value = "Mã trạng thái giao nhận";
                    worksheet.Cells[1, 37].Value = "Ngày giao";
                    worksheet.Cells[1, 38].Value = "Ngày nhận";
                    worksheet.Cells[1, 39].Value = "Mã người giao việc";
                    worksheet.Cells[1, 40].Value = "Mã người trả khách";
                    worksheet.Cells[1, 41].Value = "Ngày kết thúc nhận";
                    worksheet.Cells[1, 42].Value = "Danh sách tác nghiệp";
                    worksheet.Cells[1, 43].Value = "Danh sách tuyến hàng";
                    worksheet.Cells[1, 44].Value = "Danh sách phản hồi";
                    worksheet.Cells[1, 45].Value = "Ngày tự trả khách";
                    worksheet.Cells[1, 46].Value = "Thông tin giao việc";
                    worksheet.Cells[1, 47].Value = "Lý do từ chối";
                    worksheet.Cells[1, 48].Value = "Mã tác nghiệp cuối";

                    int rowIndex = 2; // Start writing data from row 2

                    while (true)
                    {
                        var data = await _customerService.GetCustomersData(pageNumber, pageSize);

                        if (data != null && !data.Any())
                        {
                            break;
                        }

                        if(data != null)
                        {
                            foreach (var item in data)
                            {
                                // Write data to the worksheet
                                worksheet.Cells[rowIndex, 1].Value = item.Id;
                                worksheet.Cells[rowIndex, 2].Value = item.Code ?? "";
                                worksheet.Cells[rowIndex, 3].Value = item.NameVi ?? "";
                                worksheet.Cells[rowIndex, 4].Value = item.NameEn ?? "";
                                worksheet.Cells[rowIndex, 5].Value = item.AddressVi ?? "";
                                worksheet.Cells[rowIndex, 6].Value = item.AddressEn ?? "";
                                worksheet.Cells[rowIndex, 7].Value = item.TaxCode ?? "";
                                worksheet.Cells[rowIndex, 8].Value = item.Phone ?? "";
                                worksheet.Cells[rowIndex, 9].Value = item.Email ?? "";
                                worksheet.Cells[rowIndex, 10].Value = item.Website ?? "";
                                worksheet.Cells[rowIndex, 11].Value = item.Note ?? "";
                                worksheet.Cells[rowIndex, 12].Value = item.Rating;
                                worksheet.Cells[rowIndex, 13].Value = item.FlagFavorite;
                                worksheet.Cells[rowIndex, 14].Value = item.Idbank;
                                worksheet.Cells[rowIndex, 15].Value = item.BankAccountNumber ?? "";
                                worksheet.Cells[rowIndex, 16].Value = item.BankBranchName ?? "";
                                worksheet.Cells[rowIndex, 17].Value = item.BankAddress ?? "";
                                worksheet.Cells[rowIndex, 18].Value = item.FlagCrm;
                                worksheet.Cells[rowIndex, 19].Value = item.IdnhanVienSale;
                                worksheet.Cells[rowIndex, 20].Value = item.IduserCreate;
                                worksheet.Cells[rowIndex, 21].Value = item.DateCreate != null ? string.Format("{0:yyyy-MM-dd}", item.DateCreate) : "";
                                worksheet.Cells[rowIndex, 22].Value = item.FlagActive;
                                worksheet.Cells[rowIndex, 23].Value = item.MaChiNhanh ?? "";
                                worksheet.Cells[rowIndex, 24].Value = item.EnumLoaiKhachHang;
                                worksheet.Cells[rowIndex, 25].Value = item.FlagDel;
                                worksheet.Cells[rowIndex, 26].Value = item.IdloaiDoanhNghiep;
                                worksheet.Cells[rowIndex, 27].Value = item.IduserDelete;
                                worksheet.Cells[rowIndex, 28].Value = item.DateDelete != null ? string.Format("{0:yyyy-MM-dd}", item.DateDelete) : "";
                                worksheet.Cells[rowIndex, 29].Value = item.LyDoXoa ?? "";
                                worksheet.Cells[rowIndex, 30].Value = item.NgayTuongTac != null ? string.Format("{0:yyyy-MM-dd}", item.NgayTuongTac) : "";
                                worksheet.Cells[rowIndex, 31].Value = item.NgayChonKhach != null ? string.Format("{0:yyyy-MM-dd}", item.NgayChonKhach) : "";
                                worksheet.Cells[rowIndex, 32].Value = item.NgayTraVe != null ? string.Format("{0:yyyy-MM-dd}", item.NgayTraVe) : "";
                                worksheet.Cells[rowIndex, 33].Value = item.NgayChotKhach != null ? string.Format("{0:yyyy-MM-dd}", item.NgayChotKhach) : "";
                                worksheet.Cells[rowIndex, 34].Value = item.SttmaxTacNghiep;
                                worksheet.Cells[rowIndex, 35].Value = item.NgayTacNghiep != null ? string.Format("{0:yyyy-MM-dd}", item.NgayTacNghiep) : "";
                                worksheet.Cells[rowIndex, 36].Value = item.EnumGiaoNhan != null ? item.EnumGiaoNhan : 0;
                                worksheet.Cells[rowIndex, 37].Value = item.NgayGiao != null ? string.Format("{0:yyyy-MM-dd}", item.NgayGiao) : "";
                                worksheet.Cells[rowIndex, 38].Value = item.NgayNhan != null ? string.Format("{0:yyyy-MM-dd}", item.NgayNhan) : "";
                                worksheet.Cells[rowIndex, 39].Value = item.IduserGiaoViec;
                                worksheet.Cells[rowIndex, 40].Value = item.IduserTraKhach;
                                worksheet.Cells[rowIndex, 41].Value = item.NgayKetThucNhan != null ? string.Format("{0:yyyy-MM-dd}", item.NgayKetThucNhan) : "";
                                worksheet.Cells[rowIndex, 42].Value = item.ListTacNghiepText ?? "";
                                worksheet.Cells[rowIndex, 43].Value = item.ListTuyenHangText ?? "";
                                worksheet.Cells[rowIndex, 44].Value = item.ListPhanHoiText ?? "";
                                worksheet.Cells[rowIndex, 45].Value = item.NgayTuTraKhach != null ? string.Format("{0:yyyy-MM-dd}", item.NgayTuTraKhach) : "";
                                worksheet.Cells[rowIndex, 46].Value = item.ThongTinGiaoViec ?? "";
                                worksheet.Cells[rowIndex, 47].Value = item.LyDoTuChoi ?? "";
                                worksheet.Cells[rowIndex, 48].Value = item.IdtacNghiepCuoi;

                                rowIndex++;
                            }
                        }
                        
                        pageNumber++;
                    }

                    // Save the Excel file
                    excelPackage.SaveAs(new FileInfo(filePath));

                    response.Status = true;
                    response.Message = "Bạn đã export dữ liệu khách hàng thành công";
                    response.Data = new
                    {
                        downloadUrl = Url.Action("DownloadFile", new { fileName = Path.GetFileName(filePath) })
                    };

                    return Ok(response);
                }
            } catch
            {
                response.Status = false;
                response.Message = "Lỗi export dữ liệu khách hàng trên hệ thống";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/customer/export/download/{fileName}
        * Description: Người dùng thực hiện tải file export dữ liệu khách hàng trên hệ thống
        */
        [HttpGet]
        [Route("export/download/{fileName}")]
        public IActionResult DownloadFile([FromRoute] string fileName)
        {
            var filePath = Path.Combine(Path.GetTempPath(), fileName);

            if (System.IO.File.Exists(filePath))
            {
                try
                {
                    // Open the file stream without using the 'using' statement
                    var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                    // Return the file to the client
                    var fileResult = File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);

                    // Delete the file after serving the response
                    Response.OnCompleted(() =>
                    {
                        stream.Dispose(); // Dispose of the stream
                        System.IO.File.Delete(filePath);
                        return Task.CompletedTask;
                    });

                    return fileResult;
                }
                catch
                {
                    return BadRequest();
                }
            }

            return NotFound();
        }
    }
}
