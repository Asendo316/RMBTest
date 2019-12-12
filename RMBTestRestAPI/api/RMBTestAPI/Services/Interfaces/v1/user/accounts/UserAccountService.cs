using LessCardAPI.Data;
using Microsoft.EntityFrameworkCore;
using RMBTestRestAPI.Domain.v1.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMBTestRestAPI.Services.Interfaces.v1.user.accounts
{
    public class UserAccountService : IUserAccountService
    {
        private readonly DataContext _dataContext;

        public UserAccountService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> CreateAccountAsync(AccountData accountData)
        {
            await _dataContext.AccountData.AddAsync(accountData);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<AccountData> GetAccountByAcNumAsync(int accountNum)
        {
            return await _dataContext.AccountData.SingleOrDefaultAsync(x => x.AccountNumber == accountNum);
        }

        public async Task<List<AccountData>> GetAccountsAsync()
        {
            return await _dataContext.AccountData.ToListAsync();
        }

        public async Task<bool> UpdateAccountAsynce(AccountData accountData)
        {
            _dataContext.AccountData.Update(accountData);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }
    }
}
