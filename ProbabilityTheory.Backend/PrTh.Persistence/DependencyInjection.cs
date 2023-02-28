using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using PrTh.Application.Interfaces;

namespace PrTh.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence
        (this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];
            services.AddDbContext<PrThDbContext>(options =>
            {
                options.UseSqlite(connectionString);
            });
            services.AddScoped<IPrThDbContext>(provider =>
                provider.GetService<PrThDbContext>());
            return services;
        }
    }
}
