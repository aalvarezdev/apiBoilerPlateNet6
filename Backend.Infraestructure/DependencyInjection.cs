
using Backend.Application.Interfaces;
using Backend.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Backend.Infraestructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {


            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("AzureDb"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));


            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            

            return services;
        }

    }
}
