using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCWorkshop.Data.Entities;

namespace MVCWorkshop.Data.DbConfigurations
{
    public class CategoryConfigurations : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(200);

            builder.HasMany(x => x.News).WithOne(x => x.Category).OnDelete(DeleteBehavior.Cascade) ;

        }
    }
}
