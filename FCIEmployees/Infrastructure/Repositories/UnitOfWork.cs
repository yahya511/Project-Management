

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EmployeesDbContext _dbContext;


        public IAddressRepository Addresses { get; private set; }
        public ITownRepository Towns { get; private set; }
        public IEmployeeRepository Employees { get; private set; }
        public UnitOfWork(EmployeesDbContext dbContext, 
                          IAddressRepository addressRepository,
                          ITownRepository townRepository,
                          IEmployeeRepository employeeRepository)
        {
            _dbContext = dbContext;
            Addresses = addressRepository;
            Towns = townRepository;
            Employees=employeeRepository;
        }

        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync(); // حفظ جميع التغييرات
        }

        public void Dispose()
        {
            _dbContext.Dispose(); // تحرير الموارد
        }
    }
}
