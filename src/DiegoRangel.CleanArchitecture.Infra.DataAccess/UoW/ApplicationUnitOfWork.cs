using System.Threading.Tasks;
using DiegoRangel.CleanArchitecture.Domain.Common.UoW;
using DiegoRangel.CleanArchitecture.Infra.DataAccess.Contexts;

namespace DiegoRangel.CleanArchitecture.Infra.DataAccess.UoW
{
    public class ApplicationUnitOfWork : IApplicationUnitOfWork
    {
        private readonly AppDbContext _context;
        public ApplicationUnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CommitAsync()
        {
            var changes = await _context.SaveChangesAsync();
            return changes > 0;
        }
        
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}