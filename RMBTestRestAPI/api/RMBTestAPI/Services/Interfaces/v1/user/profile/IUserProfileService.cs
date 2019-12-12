using RMBTestRestAPI.Domain.v1.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMBTestRestAPI.Services.Interfaces.v1.user.profile
{
    public interface IUserProfileService
    {
        Task<List<UserProfile>> GetProfilesAsync();

        Task<bool> CreateProfileAsync(UserProfile profile);

        Task<UserProfile> GetProfileByIdAsync(Guid profileId);

        Task<UserProfile> GetProfileByIdEmail(string email);

        Task<UserProfile> GetProfileByIdAcNum(int accountNum);


        Task<bool> UpdateProfileAsync(UserProfile profileToUpdate);

        Task<bool> DeleteProfileAsync(Guid profileId);

        Task<bool> UserOwnsProfileAsync(Guid profileId, string userId);
    }
}
