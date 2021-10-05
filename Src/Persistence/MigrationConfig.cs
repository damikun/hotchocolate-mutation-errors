using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ErrorHandling.Persistence {


    public class ApiDbContext_DesignContextFactory : IDesignTimeDbContextFactory<AppDbContext> {
        
        public AppDbContext CreateDbContext(string[] args) {

           var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseSqlite("Data Source=./appDB.db");
            return new AppDbContext(builder.Options);
        }
    }
}
