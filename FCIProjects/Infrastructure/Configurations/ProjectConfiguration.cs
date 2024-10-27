using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Models;  // تأكد من استيراد الـ models الصحيحة

namespace Infrastructure.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Projects");

            // تعريف الخصائص
            builder.HasKey(p => p.ProjectID);

            // تعيين ProjectID كـ INT واستخدام IDENTITY لتوليد قيمته تلقائيًا في SQL
            builder.Property(p => p.ProjectID)
                .ValueGeneratedOnAdd()  // تأكيد أن القيمة ستولد تلقائيًا في قاعدة البيانات (IDENTITY)
                .IsRequired();          // تأكيد أن الحقل مطلوب

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("nvarchar(max)");  // تأكيد أن نوع العمود هو nvarchar(max)

            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(500)
                .HasColumnType("nvarchar(max)");  // تأكيد أن نوع العمود هو nvarchar(max)

            builder.Property(p => p.StartDate)
                .IsRequired()
                .HasColumnType("datetime2(7)");  // تحديد نوع العمود كـ datetime2

            builder.Property(p => p.EndDate)
                .HasColumnType("datetime2(7)");  // تحديد نوع العمود كـ datetime2
        }
    }
}
