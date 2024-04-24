// ** library **
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
// ** architecture **
using locy_api.Interfaces;
using locy_api.Models.Response;
using locy_api.Models.Requests;

namespace locy_api.Controllers
{
    /*
    * URL: https://localhost:portnumber/api/v1/profile
    */
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;
        private readonly IAuthService _authService;
        private readonly IEmployeeService _employeeService;

        public ProfileController(IProfileService profileService, IAuthService authService, IEmployeeService employeeService)
        {
            _profileService = profileService;
            _authService = authService;
            _employeeService = employeeService;
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/profile/{id}
        * Description: Lấy dữ liệu hồ sơ của một nhân viên trên hệ thống
        */
        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetProfileByIdAccount([FromRoute] long id)
        {
            var response = new Response();

            try
            {
                var profile = await _profileService.GetProfileByIdAccount(id);

                // Nếu hồ sơ người dùng không tồn tại
                if (profile == null)
                {
                    response.Status = false;
                    response.Message = "Tài khoản không tồn tại";
                    response.Data = null;

                    return NotFound(response);
                }

                response.Status = true;
                response.Message = "Lấy hồ sơ người dùng thành công";
                response.Data = profile;

                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Lỗi lấy hồ sơ người dùng";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [PUT] -> https://localhost:portnumber/api/v1/profile/{id}
        * Description: Chỉnh sửa dữ liệu hồ sơ của một nhân viên trên hệ thống
        */
        [Authorize]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateProfile([FromRoute] long id, [FromBody] UpdateProfileRequest req)
        {
            var response = new Response();

            try
            {
                if (id != req.Id)
                {
                    response.Status = false;
                    response.Message = "Lỗi cập nhật hồ sơ người dùng";
                    response.Data = null;

                    return BadRequest(response);
                }

                var data = await _employeeService.GetInfoById(id);

                if(data == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi cập nhật hồ sơ người dùng vì người dùng không tồn tại";
                    response.Data = null;

                    return NotFound(response);
                }

                await _profileService.UpdateProfile(data, req);

                response.Status = true;
                response.Message = "Bạn đã cập nhật hồ sơ thành công";
                response.Data = null;

                return Ok(response);
            } catch
            {
                response.Status = false;
                response.Message = "Lỗi cập nhật hồ sơ người dùng";
                response.Data = null;

                return StatusCode(500, response);
            }
        }

        /**
        * Method -> Url: [PUT] -> https://localhost:portnumber/api/v1/profile/{id}
        * Description: Chỉnh sửa dữ liệu hồ sơ của một nhân viên trên hệ thống
        */
        [Authorize]
        [HttpPut]
        [Route("{id}/change-password")]
        public async Task<IActionResult> ChangePassword([FromRoute] long id, [FromBody] ChangePasswordRequest req)
        {
            var response = new Response();

            try
            {
                if (id != req.Id)
                {
                    response.Status = false;
                    response.Message = "Lỗi đổi mật khẩu, vui lòng thử lại";
                    response.Data = null;

                    return BadRequest(response);
                }

                var data = await _employeeService.GetAccountById(id);

                if (data == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi đổi mật khẩu vì người dùng không tồn tại";
                    response.Data = null;

                    return NotFound(response);
                }

                await _authService.ChangePassword(data, req.NewPassword);

                response.Status = true;
                response.Message = "Bạn đã đổi mật khẩu thành công";
                response.Data = null;

                return Ok(response);
            } catch
            {
                response.Status = false;
                response.Message = "Lỗi đổi mật khẩu, vui lòng thử lại";
                response.Data = null;

                return StatusCode(500, response);
            }
        }
    }
}
