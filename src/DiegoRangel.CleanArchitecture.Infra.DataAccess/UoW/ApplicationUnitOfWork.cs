using System.Threading.Tasks;
using DiegoRangel.CleanArchitecture.Domain.Common.UoW;
using DiegoRangel.CleanArchitecture.Infra.DataAccess.Contexts;
using DiegoRangel.DotNet.Framework.CQRS.Infra.Data.EFCore.Services;

namespace DiegoRangel.CleanArchitecture.Infra.DataAccess.UoW
{
    public class ApplicationUnitOfWork : IApplicationUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IChangeTrackerAuditer _changeTrackerAuditer;

        public ApplicationUnitOfWork(AppDbContext context, IChangeTrackerAuditer changeTrackerAuditer)
        {
            _context = context;
            _changeTrackerAuditer = changeTrackerAuditer;
        }

        public async Task<bool> CommitAsync()
        {
            await _changeTrackerAuditer.AuditAsync(_context.ChangeTracker);

            var changes = await _context.SaveChangesAsync();
            return changes > 0;
        }
        
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}