// ** library **
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
// ** architecture **
using locy_api.Models.Requests;
using locy_api.Models.Response;
using locy_api.Models.DTOs;
using locy_api.Interfaces;

namespace locy_api.Controllers
{
    /*
    * URL: https://localhost:portnumber/api/v1/auth
    */
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthHelper _authHelpers;
        private readonly IAuthService _authService;
        private readonly IProfileService _profileService;

        public AuthController(IAuthService authService, IAuthHelper authHelpers, IProfileService profileService)
        {
            _authService = authService;
            _authHelpers = authHelpers;
            _profileService = profileService;
        }

        /**
        * Method -> Url: [POST] -> https://localhost:portnumber/api/v1/auth/login
        * Description: Người dùng thực hiện đăng nhập trên hệ thống
        */
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LogIn([FromBody] LogInRequest req)
        {
            var response = new Response();

            try
            {
                // lấy dữ liệu dựa theo username và password
                var account = await _authService.GetDataWithUserNameAndPassword(req);

                // Kiểm tra nếu dữ liệu không tồn tại
                if (account == null)
                {
                    response.Status = false;
                    response.Message = "Bạn đã nhập sai tên đăng nhập hoặc mật khẩu!";
                    response.Data = null;

                    return NotFound(response);
                }

                // - Tài khoản nguòi dùng có truòng active = false
                if(account.Active != true)
                {
                    response.Status = false;
                    response.Message = "Tài khoản này đã bị vô hiệu hoá.";
                    response.Data = null;

                    return StatusCode(403, response);
                }

                // Lấy dữ liệu thông tin dụa theo Id
                ProfileDto? profile = await _profileService.GetProfileByIdAccount(account.Id);

                // Kiểm tra nếu dữ liệu nhân viên bị xoá.
                if (profile != null && profile.flagDelete == true)
                {
                    response.Status = false;
                    response.Message = "Tài khoản này không sử dụng được vì nhân viên sở hữu tài khoản đã bị xoá.";
                    response.Data = null;

                    return NotFound(response);
                }

                // Tạo token
                var token = _authHelpers.createToken(profile);
                // Lưu token qua cookie phía client
                Response.Cookies.Append("token", token, _authHelpers.GetCookieOptions());

                response.Status = true;
                response.Message = "Bạn đã đăng nhập thành công vào hệ thống!";
                response.Data = new
                {
                    token = token,
                    profile = profile,
                };

                return Ok(response);
            } catch
            {
                response.Status = false;
                response.Message = "Lỗi đăng nhập hệ thống. Vui lòng thử lại sau!";
                response.Data = null;

                return StatusCode(500, response);
            }
        }
    }
}
