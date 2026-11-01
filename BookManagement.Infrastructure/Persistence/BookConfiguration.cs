using BookManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookManagement.Infrastructure.Persistence
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            // Primary Key
            builder.HasKey(b => b.Id);

            // Decimal Precision
            builder.Property(b => b.Price)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(b => b.UnitDiscountAmount)
                .HasPrecision(18, 2)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(b => b.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            // Required Properties
            builder.Property(b => b.Title)
                .IsRequired();

            builder.Property(b => b.ISBN)
                .IsRequired();

            builder.Property(b => b.Quantity)
                .IsRequired()
                .HasDefaultValue(0);

            // Many to one Relations
            builder.HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Unique property
            builder.HasAlternateKey(b => b.ISBN)
                .HasName("UQ_Books_ISBN");

            // Foreign Key Constraints
            builder.ToTable(table =>
            {
                table.HasCheckConstraint(
                    "CK_Books_Price",
                    "[Price] > 0");

                table.HasCheckConstraint(
                    "CK_Books_Quantity",
                    "[Quantity] >= 0");

                table.HasCheckConstraint(
                    "CK_Books_UnitDiscountAmount",
                    "[UnitDiscountAmount] >= 0 AND [UnitDiscountAmount] <= [Price]");

                table.HasCheckConstraint(
                    "CK_Books_MaxQuantity",
                    "[MaxQuantity] IS NULL OR [MaxQuantity] > 0");
            });
        }
    }
}
