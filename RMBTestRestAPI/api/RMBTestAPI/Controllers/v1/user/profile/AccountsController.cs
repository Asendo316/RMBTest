using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RMBTestRestAPI.Contracts;
using RMBTestRestAPI.Contracts.v1.Response.abs;
using RMBTestRestAPI.Contracts.v1.Response.user;
using RMBTestRestAPI.Domain.v1.user;
using RMBTestRestAPI.Services.Interfaces.v1.user.accounts;
using RMBTestRestAPI.Services.Interfaces.v1.user.profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMBTestRestAPI.Controllers.v1.user.profile
{
    public class AccountsController: Controller
    {
        private readonly IUserAccountService _userAccountsService;
        private readonly IUserProfileService _userProfileService;



        public AccountsController(IUserAccountService userAccountService, IUserProfileService userProfileService)
        {
            _userAccountsService = userAccountService;
            _userProfileService = userProfileService;
    }

    /// <summary>
    /// Get All Accounts
    /// </summary>
    /// 
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet(ApiRoutes.Accounts.GetAllAccountDetails)]
        public async Task<IActionResult> GetAllAccounts()
        {
            return Ok(await _userAccountsService.GetAccountsAsync());
        }

        /// <summary>
        /// Check your account balance
        /// </summary>
        [HttpGet(ApiRoutes.Accounts.GetAccountById)]
        public async Task<IActionResult> GetFavouritesForUserId([FromRoute]int accountNumber)
        {
            var account = await _userAccountsService.GetAccountByAcNumAsync(accountNumber);
            if (account == null)
                return NotFound(new GenericResponse { Status = "Failed", Message = "You do not have any Accounts" });

            var profile = await _userProfileService.GetProfileByIdAcNum(accountNumber);

            var response = new AccountDetailsResponse
            {
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Email = profile.Email,
                Address = profile.Address,
                ProfilePcture = profile.ProfilePicture,
                DOB = profile.DOB,
                AccountData = new AccountData
                {
                    AccountNumber= accountNumber,
                    CurrentBalance = account.CurrentBalance,
                    LedgerBalance = account.LedgerBalance
                }
            };
            return Ok(response);
        }

    }
}
