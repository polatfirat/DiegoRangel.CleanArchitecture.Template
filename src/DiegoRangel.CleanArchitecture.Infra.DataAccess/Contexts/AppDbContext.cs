using DiegoRangel.DotNet.Framework.CQRS.Infra.Data.EFCore.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DiegoRangel.CleanArchitecture.Infra.DataAccess.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyMappings();
            builder.ApplyDateTimeConvention();
            builder.ApplyVarcharConvention();
        }
    }
}