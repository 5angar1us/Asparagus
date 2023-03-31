using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Asparagus.Persistance
{
    public static class DBContextDependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configration)
        {
            var connectionString = configration["DBconnection"];
            services.AddDbContext<ApplicationDBcontext>(
                options =>
                    {
                        options.UseSqlite(connectionString);
                    });

            services.AddScoped<IApplicationDBcontext>(provider => provider.GetService<ApplicationDBcontext>());
            return services;
        }
    }
}
