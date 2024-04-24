// ** architecture **
using locy_api.Models.Domains;
using locy_api.Models.Requests;

namespace locy_api.Interfaces
{
    public interface IAuthService
    {
        Task<TblSysUser?> GetDataWithUserNameAndPassword(LogInRequest logInRequestDto);
        Task ChangePassword(TblSysUser account, string newPassword);
    }
}