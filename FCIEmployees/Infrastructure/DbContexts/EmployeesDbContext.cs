namespace Infrastructure.DbContexts
{
    public class EmployeesDbContext : DbContext
    {
        public EmployeesDbContext(DbContextOptions<EmployeesDbContext> options) 
            : base(options)
        {
        
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new TownConfiguration());
            modelBuilder.ApplyConfiguration(new AddressConfiguration());
            base.OnModelCreating(modelBuilder);
        }
        //BaseEntityConfiguration
        // نقل دالة SaveChangesAsync هنا
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			var entries = ChangeTracker.Entries()
				.Where(e => e.Entity is BaseEntity &&
							(e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted));

			foreach (var entry in entries)
			{
				var entity = (BaseEntity)entry.Entity;

				if (entry.State == EntityState.Added)
				{
					entity.CreatedBy = Guid.NewGuid(); // أو تعيين المستخدم الحالي
					entity.CreatedOn = DateTime.UtcNow;
				}
				else if (entry.State == EntityState.Modified || entry.State == EntityState.Deleted)
				{
					entity.UpdatedBy = Guid.NewGuid(); // أو تعيين المستخدم الحالي
					entity.UpdatedOn = DateTime.UtcNow;

					if (entry.State == EntityState.Deleted)
					{
						entity.IsDeleted = true;
						entry.State = EntityState.Modified; // تحديث الكائن بدلاً من حذفه
					}
				}
			}

			return await base.SaveChangesAsync(cancellationToken);
		}

    }
}
