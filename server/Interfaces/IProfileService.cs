using locy_api.Models.Domains;
using locy_api.Models.DTOs;
using locy_api.Models.Requests;

namespace locy_api.Interfaces
{
    public interface IProfileService
    {
        // Task<ProfileDto?> GetProfileById(long id);
        Task<ProfileDto?> GetProfileByIdAccount(long idAccount);
        Task UpdateProfile(TblNhanSu info, UpdateProfileRequest req);
        
        /*
        Task<List<ProfileDto>?> GetProfiles();
        Task CreateProfile(CreateProfileRequest req);
        Task UpdateProfile(TblNhanSu employee, UpdateProfileRequest req);
        Task DeleteEmployee(TblNhanSu employee, DeleteProfileRequest req);
        */
    }
}
