using DiegoRangel.CleanArchitecture.Domain.Example;
using DiegoRangel.DotNet.Framework.CQRS.Infra.Data.EFCore.Mappings;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiegoRangel.CleanArchitecture.Infra.DataAccess.Mappings
{
    /// <summary>
    /// A simple example of a repository implementation.
    /// Possible abstractions: EntityMap, CreationAuditedEntityMap, AuditedEntityMap, FullAuditedEntityMap.
    /// </summary>
    public class ExampleMap : EntityMap<Example>
    {
        public override void ConfigureEntityBuilder(EntityTypeBuilder<Example> builder)
        {
            builder.Property(x => x.Title).IsRequired();
        }
    }
}