using RMBTestRestAPI.Domain.v1.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMBTestRestAPI.Services.Interfaces.v1.user.accounts
{
   public interface IUserAccountService
    {
        Task<List<AccountData>> GetAccountsAsync();

        Task<bool> CreateAccountAsync(AccountData accountData);

        Task<AccountData> GetAccountByAcNumAsync(int accountNum);

        Task<bool> UpdateAccountAsynce(AccountData accountData);
    }
}
