using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Models;  // تأكد من استيراد الـ models الصحيحة

namespace Infrastructure.Configurations
{
    public class EmployeesProjectsConfiguration : IEntityTypeConfiguration<EmployeesProjects>
    {
        public void Configure(EntityTypeBuilder<EmployeesProjects> builder)
        {
            builder.ToTable("EmployeesProjects");

            // تعريف الخصائص
            builder.HasKey(ep => new { ep.EmployeeID, ep.ProjectID });

            /* // تعيين العلاقات (إذا كانت موجودة) مع Employee و Project
            builder.HasOne(ep => ep.Employee)  // Assuming EmployeesProjects has an Employee navigation property
                .WithMany(e => e.EmployeesProjects) // Assuming Employee has a collection of EmployeesProjects
                .HasForeignKey(ep => ep.EmployeeID)
                .OnDelete(DeleteBehavior.Cascade); // تعيين سلوك الحذف

            builder.HasOne(ep => ep.Project)  // Assuming EmployeesProjects has a Project navigation property
                .WithMany(p => p.EmployeesProjects) // Assuming Project has a collection of EmployeesProjects
                .HasForeignKey(ep => ep.ProjectID)
                .OnDelete(DeleteBehavior.Cascade); // تعيين سلوك الحذف */
        }
    }
}
