

public class UserRoleSessionRepository : GenericRepository<UserRoleSession>, IUserRoleSessionRepository
{
    public UserRoleSessionRepository(ApplicationDbContext context) : base(context)
    {
    }
}
