using DiegoRangel.CleanArchitecture.Infra.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DiegoRangel.CleanArchitecture.Infra.IoC
{
    public static class AppInitializer
    {
        public static void RegisterMyApplicationModules(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
        }
    }
}