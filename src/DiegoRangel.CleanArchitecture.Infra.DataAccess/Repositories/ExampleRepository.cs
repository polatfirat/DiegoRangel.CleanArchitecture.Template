using DiegoRangel.CleanArchitecture.Domain.Example;
using DiegoRangel.CleanArchitecture.Domain.Example.Repositories;
using DiegoRangel.CleanArchitecture.Infra.DataAccess.Contexts;
using DiegoRangel.DotNet.Framework.CQRS.Infra.Data.EFCore.Repositories;

namespace DiegoRangel.CleanArchitecture.Infra.DataAccess.Repositories
{
    public class ExampleRepository : CrudRepository<Example>, IExampleRepository
    {
        public ExampleRepository(AppDbContext context) : base(context)
        {
        }
    }
}