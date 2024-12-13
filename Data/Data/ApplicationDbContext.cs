using Microsoft.EntityFrameworkCore;
using WebData.Model;

namespace WebData.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> users { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Category> categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-7KKDTTL\\SQLEXPRESS;Database=Pactice_C#_Moi;Trusted_Connection=True;TrustServerCertificate =True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData
            (
                new Role { Id = 1, Name = "Admin", Description = "Administrator Role" },
                new Role { Id = 2, Name = "Customer", Description = "Customer Role" }
            );
        }
    }
}
