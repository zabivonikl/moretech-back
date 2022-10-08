using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MoretechBack.Database.Models;

public class GlobalAchievementConfiguration : IEntityTypeConfiguration<GlobalAchievement>
{
    public void Configure(EntityTypeBuilder<GlobalAchievement> builder)
    {
        builder.HasKey(achievement => achievement.Id);
    }
}