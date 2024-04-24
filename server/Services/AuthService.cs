// ** library **
using Microsoft.EntityFrameworkCore;
// ** architecture **
using locy_api.Data;
using locy_api.Models.Domains;
using locy_api.Models.Requests;
using locy_api.Interfaces;

namespace locy_api.Services
{
    public class AuthService: IAuthService
    {
        private readonly BestCareDbContext _db;
        private readonly IAuthHelper _authHelpers;

        public AuthService(BestCareDbContext dbContext, IAuthHelper authHelpers)
        {
            _db = dbContext;
            _authHelpers = authHelpers;
        }

        // Method: Lấy dữ liệu với thông tin đăng nhập là tên đăng nhập và mật khẩu
        public async Task<TblSysUser?> GetDataWithUserNameAndPassword(LogInRequest req)
        {
            // Mã hoá mật khẩu
            var hashPassword = _authHelpers.Encrypt(req.Password);

            // Lấy dữ liệu từ db
            TblSysUser? account = await _db.TblSysUsers.Where(u => u.UserName == req.Username && u.Password == hashPassword).FirstOrDefaultAsync();

            return account;
        }

        // Method: Cập nhật mật khẩu
        public async Task ChangePassword(TblSysUser account, string newPassword)
        {
            // Mã hoá mật khẩu
            var hashPassword = _authHelpers.Encrypt(newPassword);

            // Cập nhật truòng thông tin
            account.Password = hashPassword;

            // Lưu vào cơ sở dữ liệu
            await _db.SaveChangesAsync();
        }
    }
}
