using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RMBTestRestAPI.Domain.v1.auth;
using RMBTestRestAPI.Domain.v1.user;

namespace LessCardAPI.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<UserProfile> UserProfile { get; set; }

        public DbSet<AccountData> AccountData { get; set; }

    }
}
