using LessCardAPI.Data;
using Microsoft.EntityFrameworkCore;
using RMBTestRestAPI.Domain.v1.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMBTestRestAPI.Services.Interfaces.v1.user.profile
{
    public class UserProfileService : IUserProfileService
    {
        private readonly DataContext _dataContext;

        public UserProfileService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<UserProfile>> GetProfilesAsync()
        {
            return await _dataContext.UserProfile.ToListAsync();
        }

        public async Task<UserProfile> GetProfileByIdAsync(Guid userId)
        {
            return await _dataContext.UserProfile.SingleOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<UserProfile> GetProfileByIdEmail(string email)
        {
            return await _dataContext.UserProfile.SingleOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
        }

        public async Task<UserProfile> GetProfileByIdAcNum(int accountNum)
        {
            return await _dataContext.UserProfile.SingleOrDefaultAsync(x => x.AccountNumber == accountNum);
        }
        public async Task<bool> CreateProfileAsync(UserProfile profile)
        {
            await _dataContext.UserProfile.AddAsync(profile);


            var created = await _dataContext.SaveChangesAsync();


            return created > 0;
        }

        public async Task<bool> UpdateProfileAsync(UserProfile profileToUpdate)
        {
            _dataContext.UserProfile.Update(profileToUpdate);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteProfileAsync(Guid profileId)
        {
            var profile = await GetProfileByIdAsync(profileId);

            if (profile == null)
                return false;

            _dataContext.UserProfile.Remove(profile);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<bool> UserOwnsProfileAsync(Guid profileId, string userId)
        {
            var profile = await _dataContext.UserProfile.AsNoTracking().SingleOrDefaultAsync(x => x.Id == profileId);
            if (profile == null)
            {
                return false;
            }

            if (profile.Id != profileId)
            {
                return false;
            }

            return true;
        }

    }
}
