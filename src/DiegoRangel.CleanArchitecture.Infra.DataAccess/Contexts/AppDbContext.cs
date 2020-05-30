using DiegoRangel.CleanArchitecture.Domain.Example;
using DiegoRangel.DotNet.Framework.CQRS.Infra.Data.EFCore.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DiegoRangel.CleanArchitecture.Infra.DataAccess.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Example> Examples { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyMappings(GetType().Assembly);
            builder.ApplyDateTimeConvention();
            builder.ApplyVarcharConvention();
        }
    }
}