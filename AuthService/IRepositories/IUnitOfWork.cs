namespace AuthService.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepository AccountRepository { get; }
        Task SaveChangesAsync();
    }
}
