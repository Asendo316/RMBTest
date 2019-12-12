using LessCardAPI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RMBTestRestAPI.Services.Interfaces.v1.user.accounts;
using RMBTestRestAPI.Services.Interfaces.v1.user.profile;

namespace RMBTestRestAPI.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<DataContext>();

            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IUserAccountService, UserAccountService>();
        }
    }
}
