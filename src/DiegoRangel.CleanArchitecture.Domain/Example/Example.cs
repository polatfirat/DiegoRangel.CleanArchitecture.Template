using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;

namespace DiegoRangel.CleanArchitecture.Domain.Example
{
    /// <summary>
    /// A simple example of an entity which has a lot of useful generic abstractions.
    /// Abstractions: Entity, CreationAuditedEntity, AuditedEntity, FullAuditedEntity
    /// Interfaces: IEntity, IPassivable, IAudited, ICreationAudited, IModificationAudited, IFullAudited, IDeletionAudited, IHasCreationTime, IHasDeletionTime, IHasModificationTime 
    /// </summary>
    public class Example : Entity
    {
        public string Title { get; set; }
    }
}