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
        
        builder
            .HasMany(user => user.Notification)
            .WithOne(notification => notification.Owner)
            .OnDelete(DeleteBehavior.Cascade)
            .HasForeignKey("OwnerId");

        builder.HasMany(user => user.Orders)
            .WithOne(order => order.Owner)
            .OnDelete(DeleteBehavior.Cascade)
            .HasForeignKey("OwnerId");
    }
}