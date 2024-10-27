using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Enums;
using Domain.Models;  // تأكد من استيراد الـ models الصحيحة

namespace Infrastructure.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");

            // تعريف الخصائص
            builder.HasKey(e => e.EmployeeID);

            // تعيين EmployeeID كـ INT واستخدام IDENTITY لتوليد قيمته تلقائيًا في SQL
            builder.Property(e => e.EmployeeID)
                .ValueGeneratedOnAdd()  // تأكيد أن القيمة ستولد تلقائيًا في قاعدة البيانات (IDENTITY)
                .IsRequired();           // تأكيد أن الحقل مطلوب

            // تحويل الـ Enum JobTitle إلى string لتخزينه في قاعدة البيانات
            builder.Property(e => e.JobTitle)
                   .HasConversion(
                       v => v.ToString(),
                       v => (JobTitle)Enum.Parse(typeof(JobTitle), v))
                   .IsRequired();

            // خصائص الاسماء والبيانات الشخصية
            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.LastName)
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.MiddleName)
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.JobTitle)
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.HireDate)
                .IsRequired();

            builder.Property(e => e.Salary)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            // علاقة مع Manager
            builder.HasOne(e => e.Manager)
                .WithMany(e => e.Subordinates)
                .HasForeignKey(e => e.ManagerID)
                .OnDelete(DeleteBehavior.Restrict);  // منع الحذف التلقائي المتسلسل

            // علاقة مع Address
            builder.HasOne(e => e.Address)
                .WithMany(a => a.Employees)
                .HasForeignKey(e => e.AddressID);
        }
    }
}
