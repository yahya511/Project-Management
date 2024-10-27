using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Models;  // تأكد من استيراد الـ models الصحيحة

namespace Infrastructure.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Departments");

            // تعريف الخصائص
            builder.HasKey(d => d.DepartmentID);

            // تعيين DepartmentID كـ int بدون توليد قيمة تلقائية، لأننا نستخدم IDENTITY في SQL
            builder.Property(d => d.DepartmentID)
                .ValueGeneratedOnAdd(); // تأكيد أن القيمة ستولد عند الإضافة

            // تعيين الاسم كـ nvarchar(max)
            builder.Property(d => d.DepartmentName)
                .IsRequired()
                .HasColumnType("nvarchar(max)"); // تحديد نوع العمود

            // تعيين ManagerID كـ int
            builder.Property(d => d.ManagerID)
                .IsRequired(); // يمكن أن يكون int
        }
    }
}
