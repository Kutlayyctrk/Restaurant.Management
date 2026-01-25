using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.Entities.Concretes;

namespace Project.Persistance.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.ProductName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(p => p.UnitPrice)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            // Boolean alanlar için default değerler
            builder.Property(p => p.IsSellable).HasDefaultValue(true);
            builder.Property(p => p.IsExtra).HasDefaultValue(false);
            builder.Property(p => p.CanBeProduced).HasDefaultValue(false);
            builder.Property(p => p.IsReadyMade).HasDefaultValue(false);

            // İlişkiler
            builder.HasOne(p => p.Category)
                   .WithMany(c => c.Products)
                   .HasForeignKey(p => p.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Unit)
                   .WithMany(u => u.Products)
                   .HasForeignKey(p => p.UnitId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Opsiyonel Recipe
            builder.HasOne(p => p.Recipe)
                   .WithOne(r => r.Product)
                   .HasForeignKey<Recipe>(r => r.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.Property(x=>x.Quantity).HasColumnType("decimal(18,2)").IsRequired().HasDefaultValue(0);

            builder.ToTable("Products");
        }
    }
}