using FundRaising.Core.Data;
using FundRaising.Core.Interfaces;
using FundRaising.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaising.Core
{
    public static class DepedencyInjection
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IRewardService, RewardService>();
            services.AddScoped<IFundService, FundService>();

            return services;
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FundRaisingDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(FundRaisingDbContext).Assembly.FullName))
            );

            services.AddScoped<IFundRaisingDbContext>(provider => provider.GetService<FundRaisingDbContext>());

            return services;
        }
    }
}
