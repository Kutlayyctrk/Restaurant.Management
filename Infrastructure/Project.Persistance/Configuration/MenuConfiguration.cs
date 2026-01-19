using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.Entities.Concretes;

public class MenuConfiguration : IEntityTypeConfiguration<Menu>
{
    public void Configure(EntityTypeBuilder<Menu> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.MenuName).IsRequired().HasMaxLength(100);

        builder.HasMany(m => m.MenuProducts)
               .WithOne(mp => mp.Menu)
               .HasForeignKey(mp => mp.MenuId);
    }
}