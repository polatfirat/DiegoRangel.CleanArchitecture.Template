using DiegoRangel.CleanArchitecture.Infra.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DiegoRangel.CleanArchitecture.Infra.IoC
{
    public static class IocManager
    {
        public static void RegisterApplicationServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("MyInMemoryDatabase"));
        }
    }
}