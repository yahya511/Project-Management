using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Models;

namespace Infrastructure.Configurations
{
    public class TownConfiguration : IEntityTypeConfiguration<Town>
    {
        public void Configure(EntityTypeBuilder<Town> builder)
        {
            builder.ToTable("Towns");

            // تعريف الخصائص
            builder.HasKey(t => t.TownID);

            // تعيين TownID كـ INT واستخدام IDENTITY لتوليد قيمته تلقائيًا في SQL
            builder.Property(t => t.TownID)
                .ValueGeneratedOnAdd()  // تأكيد أن القيمة ستولد تلقائيًا في قاعدة البيانات (IDENTITY)
                .IsRequired();           // تأكيد أن الحقل مطلوب

            // تعديل خصائص Name للسماح بالنصوص الطويلة
            builder.Property(t => t.Name)
                .IsRequired()
                .HasColumnType("nvarchar(max)");  // تعيين nvarchar(max) كما في إعدادات الجدول في SQL
        }
    }
}
