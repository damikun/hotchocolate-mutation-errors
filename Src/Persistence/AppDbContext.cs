using ErrorHandling.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ErrorHandling.Persistence {

        public class AppDbContext : DbContext {
            public DbSet<User> Users { get; set; }
        // Asset
        public AppDbContext(
            DbContextOptions<AppDbContext> options)
            : base(options) {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            modelBuilder.Entity<User>().HasData(
                new User() { 
                    Guid = System.Guid.NewGuid().ToString(),
                    NickName = "Zlobor",
                    Age = 20,
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}