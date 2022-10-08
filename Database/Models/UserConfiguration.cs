using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MoretechBack.Database.Models;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);
        builder
            .HasMany(user => user.Achievements)
            .WithOne(achievement => achievement.Owner)
            .OnDelete(DeleteBehavior.Cascade)
            .HasForeignKey("OwnerId");
    }
}