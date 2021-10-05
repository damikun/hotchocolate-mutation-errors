using ErrorHandling.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErrorHandling.Presistence {
    public class UserConfiguration : IEntityTypeConfiguration<User> {
        public void Configure(EntityTypeBuilder<User> builder) {

            builder.HasKey(e => e.Guid);

            builder.Property(e => e.Guid)
            .ValueGeneratedOnAdd();
        }
    }
}