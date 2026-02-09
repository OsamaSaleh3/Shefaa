using Microsoft.EntityFrameworkCore; // ضروري لـ UseSqlServer
using Microsoft.Extensions.Configuration; // ضروري لـ IConfiguration
using Microsoft.Extensions.DependencyInjection;
using Shefaa.Infrastructure.Common.Persistence; // ضروري لـ IServiceCollection

namespace Shefaa.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ShefaaDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}