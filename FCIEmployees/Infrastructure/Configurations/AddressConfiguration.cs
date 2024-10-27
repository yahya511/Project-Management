using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Models;  // تأكد من استيراد الـ models الصحيحة

namespace Infrastructure.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Addresses");

            // تعريف الخصائص
            builder.HasKey(a => a.AddressID);

            // تعيين AddressID كـ INT واستخدام IDENTITY لتوليد قيمته تلقائيًا في SQL
            builder.Property(a => a.AddressID)
                .ValueGeneratedOnAdd()  // تأكيد أن القيمة ستولد تلقائيًا في قاعدة البيانات (IDENTITY)
                .IsRequired();           // تأكيد أن الحقل مطلوب

            builder.Property(a => a.AddressText)
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            // علاقة مع Town
            builder.HasOne(a => a.Town)
                .WithMany(t => t.Addresses)
                .HasForeignKey(a => a.TownID)
                .OnDelete(DeleteBehavior.Restrict);  // منع الحذف التلقائي المتسلسل
        }
    }
}
