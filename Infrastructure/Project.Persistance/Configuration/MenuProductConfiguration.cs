using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.Entities.Concretes;

public class MenuProductConfiguration : IEntityTypeConfiguration<MenuProduct>
{
    public void Configure(EntityTypeBuilder<MenuProduct> builder)
    {
        builder.HasKey(mp => mp.Id);

        builder.Property(mp => mp.UnitPrice)
           .HasPrecision(18, 2)
           .IsRequired();


        builder.Property(mp => mp.IsActive)
               .IsRequired();

        builder.HasOne(mp => mp.Menu)
               .WithMany(m => m.MenuProducts)
               .HasForeignKey(mp => mp.MenuId);

        builder.HasOne(mp => mp.Product)
        .WithMany(p => p.MenuProducts)
        .HasForeignKey(mp => mp.ProductId);


    }
}