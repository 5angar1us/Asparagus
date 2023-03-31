using Asparagus.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Asparagus.Persistance
{
    public class UserConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(user => user.Id);
            builder.HasIndex(user => user.Id).IsUnique();

            builder.Property(user => user.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(user => user.Email)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(user => user.DateTime);

        }
    }
}
