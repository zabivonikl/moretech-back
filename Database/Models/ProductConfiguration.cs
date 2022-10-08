using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MoretechBack.Database.Models;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(product => product.Id);
        builder.Property(product => product.Images)
            .HasConversion(
                images => images.Aggregate(string.Empty, (acc, image) => $"{acc}{image}; ", r => r.Remove(r.Length - 2)),
                str => str.Split("; ", StringSplitOptions.RemoveEmptyEntries).ToList());
    }
}