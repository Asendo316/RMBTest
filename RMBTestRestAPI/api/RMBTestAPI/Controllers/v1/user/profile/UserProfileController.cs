using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RMBTestRestAPI.Contracts;
using RMBTestRestAPI.Contracts.v1.Requests.user.profile;
using RMBTestRestAPI.Contracts.v1.Response.abs;
using RMBTestRestAPI.Contracts.v1.Response.user;
using RMBTestRestAPI.Extensions;
using RMBTestRestAPI.Services.Interfaces.v1.user.profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMBTestRestAPI.Controllers.v1.user.profile
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserProfileController : Controller
    {
        private readonly IUserProfileService _profileService;
        DateTime currentDate = DateTime.Today;



        public UserProfileController(IUserProfileService profileService)
        {
            _profileService = profileService;
        }


        /// <summary>
        /// Get All User Profiles
        /// </summary>
        [HttpGet(ApiRoutes.UserProfile.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _profileService.GetProfilesAsync());
        }


        /// <summary>
        /// Get User Profile Details
        /// </summary>
        [HttpGet(ApiRoutes.UserProfile.Get)]
        public async Task<IActionResult> GetById([FromRoute]Guid profileId)
        {
            bool isSubActive;
            var profile = await _profileService.GetProfileByIdAsync(profileId);
            bool freeTrial = false;

            if (profile == null)
                return NotFound(new GenericResponse { Status = "Error", Message = "Oops, User does not exist" });


            var validateCompletion =
                ValidateProfile(profile.FirstName, profile.LastName, profile.Email, profile.Address,
                profile.ProfilePicture, profile.DOB);

            var expiryDate = profile.DateCreated.AddDays(7);
            if (currentDate < expiryDate)
            {
                freeTrial = true;
            }


            var response = new UserProfileResponse
            {
                Id = profile.Id,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Email = profile.Email,
                Address = profile.Address,
                ProfilePcture = profile.ProfilePicture,
                DOB = profile.DOB,
                Status = validateCompletion
            };
            return Ok(response);
        }

        private bool ValidateProfile(string firstName, string lastName, string email, string address,
                                     string profilePcture, string dOB)
        {
            if (firstName != null && lastName != null && email != null && address != null && profilePcture != null && dOB != null)
                return true;

            return false;
        }



        /// <summary>
        /// Update User Profile
        /// </summary>
        [HttpPut(ApiRoutes.UserProfile.Update)]
        public async Task<IActionResult> UpdateProfile([FromRoute]Guid profileId, [FromBody] UpdateProfileRequest request)
        {
            var userOwnsPost = await _profileService.UserOwnsProfileAsync(profileId, HttpContext.GetUserId());

            if (!userOwnsPost)
            {
                return BadRequest(new { error = "You do not own this post" });
            }

            var profile = await _profileService.GetProfileByIdAsync(profileId);

            profile.FirstName = request.FirstName;
            profile.LastName = request.LastName;
            profile.Address = request.Address;
            profile.ProfilePicture = request.ProfilePcture;
            profile.DOB = request.DOB;

            var updated = await _profileService.UpdateProfileAsync(profile);

            var validateCompletion = ValidateProfile(profile.FirstName, profile.LastName, profile.Email, profile.Address, profile.ProfilePicture, profile.DOB);

            var response = new UserProfileResponse
            {
                Id = profile.Id,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Email = profile.Email,
                Address = profile.Address,
                ProfilePcture = profile.ProfilePicture,
                DOB = profile.DOB,
                Status = validateCompletion
            };


            if (updated)
                return Ok(new GenericResponse { Status = "Success", Message = "Your profile has been updated" });


            return NotFound(new GenericResponse { Status = "Error", Message = "Oops, User does not exist" });
        }


        /// <summary>
        /// Delete User Profile
        /// </summary>
        [HttpDelete(ApiRoutes.UserProfile.Delete)]
        public async Task<IActionResult> Delete([FromRoute]Guid profileId)
        {
            var userOwnsPost = await _profileService.UserOwnsProfileAsync(profileId, HttpContext.GetUserId());

            if (!userOwnsPost)
            {
                return BadRequest(new { error = "You do not own this post" });
            }

            var deleted = await _profileService.DeleteProfileAsync(profileId);

            if (deleted)
                return Ok(new GenericResponse { Status = "Success", Message = "Your User has been cancelled successfully" });

            return NotFound(new GenericResponse { Status = "Error", Message = "User, Card does not exist" });
        }
    }
}
