using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCWorkshop.Data.Entities;
using System;

namespace MVCWorkshop.Data.DbConfigurations
{
    public class NewsConfigurations : IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<News> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x=>x.Title).IsRequired();
            builder.Property(x=>x.Title).HasMaxLength(200);

            builder.Property(x => x.Body).IsRequired();
            builder.Property(x => x.Body).HasMaxLength(int.MaxValue);

            builder.Property(x => x.Date).HasDefaultValue(TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(TimeZone.CurrentTimeZone.StandardName)));

            builder.Property(x => x.CategoryId).IsRequired();


            builder.HasOne(x => x.Category).WithMany(x=>x.News).IsRequired();


        }
    }
}
