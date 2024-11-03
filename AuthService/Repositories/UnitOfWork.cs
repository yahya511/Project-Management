using AuthService.DbContexts;
using AuthService.IRepositories;
using System.Threading.Tasks;

namespace AuthService.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IAccountRepository AccountRepository { get; }

        public UnitOfWork(ApplicationDbContext context, IAccountRepository accountRepository)
        {
            _context = context;
            AccountRepository = accountRepository;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
