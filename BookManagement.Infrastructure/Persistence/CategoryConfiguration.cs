using BookManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookManagement.Infrastructure.Persistence
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // required property
            builder.Property(c => c.Name)
                .IsRequired();

            // Unique property
            builder.HasIndex(c => c.Name)
                .IsUnique();
        }
    }
}
