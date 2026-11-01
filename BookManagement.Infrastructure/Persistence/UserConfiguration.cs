using BookManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookManagement.Infrastructure.Persistence
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Primary Key
            builder.HasKey(u => u.Id);

            // Required Properties
            builder.Property(u => u.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.Email)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(u => u.PasswordHash)
                .IsRequired();

            builder.Property(u => u.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(u => u.MustChangePassword)
                .IsRequired()
                .HasDefaultValue(false);

            // Unique property
            builder.HasIndex(u => u.Email)
                .IsUnique()
                .HasDatabaseName("UX_Users_Email");

            // Many to one Relation
            builder.HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
