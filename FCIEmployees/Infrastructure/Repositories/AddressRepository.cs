

namespace Infrastructure.Repositories
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        public AddressRepository(EmployeesDbContext dbContext) : base(dbContext)
        {
        }

        // يمكن إضافة وظائف إضافية خاصة بـ Project هنا إذا لزم الأمر.
    }
}
