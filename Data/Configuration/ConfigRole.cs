using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebData.Model;

namespace WebData.Configuration
{
    public class ConfigRole : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(255);

            builder.HasMany(u => u.users)
                   .WithOne(r => r.Role)
                   .HasForeignKey(r => r.RoleId);
        }
    }
}
