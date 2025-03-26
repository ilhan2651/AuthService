using Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.DisabilityType)
                .HasConversion<string>(); // ✅ Enum'u string olarak sakla
        }

    }


}
