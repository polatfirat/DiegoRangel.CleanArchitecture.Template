using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories;

namespace DiegoRangel.CleanArchitecture.Domain.Example.Repositories
{
    /// <summary>
    /// A simple example of a repository which has a lot of useful generic interfaces.
    /// IRepository, ICrudRepository,
    /// ICreatableRepository, IDeletableRepository, IFindableRepository, ISearchableRepository, ISoftDeletableRepository, IUpdatableRepository,
    /// IAuditedRepository, ICreationAuditedRepository, IDeletionAuditedRepository, IFullAuditedRepository, IModificationAuditedRepository.
    /// </summary>
    public interface IExampleRepository : ICrudRepository<Example>
    {
        
    }
}