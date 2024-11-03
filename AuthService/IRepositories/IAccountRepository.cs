using AuthService.DbContexts;
using System.Threading.Tasks;

namespace AuthService.IRepositories
{
    public interface IAccountRepository : IGenericRepository<ApplicationUser>
    {
        Task<ApplicationUser> FindByUsernameAsync(string username);
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
        Task RegisterUserAsync(ApplicationUser user, string role);
        Task<string> GenerateJwtTokenAsync(ApplicationUser user, string role, bool isTemporary = false);
    }
}
