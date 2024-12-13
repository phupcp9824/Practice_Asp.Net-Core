using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebData.Model;

namespace WebData.Configuration
{
    public class ConfigCategory : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .HasMany(p => p.Products)
                .WithMany(ps => ps.Category)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductCategory", // Join table name
                    j => j
                        .HasOne<Product>()
                        .WithMany()
                        .HasForeignKey("ProductId"),
                    j => j
                        .HasOne<Category>()
                        .WithMany()
                        .HasForeignKey("CategoryId"),
                    j =>
                    {
                        j.HasKey("ProductId", "CategoryId"); // Primary key for the join table
                    });
        }
    }
}
